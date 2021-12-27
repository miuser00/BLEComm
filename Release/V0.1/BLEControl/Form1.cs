using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Collections.ObjectModel;
using System.Diagnostics;
using Windows.Devices.Bluetooth;
using Windows.Devices.Enumeration;
using Windows.Foundation;
using System.Collections;
using LvYeCommon;
using Windows.Devices.Bluetooth.Advertisement;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Security.Cryptography;
using Windows.Storage.Streams;
using System.Runtime.InteropServices.WindowsRuntime;
using MSerialization;

namespace BLEControl
{

    public partial class Form1 : Form
    {
        readonly uint E_BLUETOOTH_ATT_WRITE_NOT_PERMITTED = 0x80650003;
        readonly uint E_BLUETOOTH_ATT_INVALID_PDU = 0x80650004;
        readonly uint E_ACCESSDENIED = 0x80070005;
        readonly uint E_DEVICE_NOT_AVAILABLE = 0x800710df; // HRESULT_FROM_WIN32(ERROR_DEVICE_NOT_AVAILABLE)

        //找到的设备
        private Dictionary<string,DeviceInformation> devices = new Dictionary<string,DeviceInformation>();
        private DeviceWatcher deviceWatcher;
        ListViewColumnSorter lvwColumnSorter;
        //当前选中的BLE设备
        BluetoothLEDevice ble_Device;
        //当前设备提供的服务组
        IReadOnlyList<GattDeviceService> ble_Services = null;
        //当前选中的BLE服务
        GattDeviceService ble_Service = null;
        //当前选中的BLE属性组
        IReadOnlyList<GattCharacteristic> ble_Characteristics = null;
        //当前选中的BLE属性
        GattCharacteristic ble_Characteristic = null;
        //数据格式
        GattPresentationFormat presentationFormat;
        //数据结果
        IBuffer ble_result;
        public Form1()
        {
            InitializeComponent();
            lvwColumnSorter = new ListViewColumnSorter();
            this.lv_device.ListViewItemSorter = lvwColumnSorter;
            this.ss_bottom.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStripStatusLabel1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;

        }
        //发现新设备
        private void DeviceWatcher_Added(DeviceWatcher sender, DeviceInformation deviceInfo)
        {
            try
            {
                this?.Invoke(new Action(() =>
                {
                    if (sender == deviceWatcher)
                    {
                        // Make sure device isn't already present in the list.
                        if (!devices.ContainsKey(deviceInfo.Id))
                        {
                            if (deviceInfo.Name != "" || chb_ShowHidden.Checked == true)
                            {
                                string id = deviceInfo.Id;

                                devices.Add(deviceInfo.Id, deviceInfo);
                                //添加到列表
                                ListViewItem lvi = new ListViewItem();
                                lvi.Text = deviceInfo.Name;
                                lvi.Name = id;
                                lvi.SubItems.Add(id);
                                lvi.SubItems.Add(deviceInfo.Pairing.CanPair.ToString());
                                lvi.SubItems.Add(deviceInfo.Pairing.IsPaired.ToString());
                                lvi.SubItems.Add("");
                                lv_device.Items.Add(lvi);
                            }
                        }
                    }
                }));
            }
            catch { }
        }

        //设备信息被更新
        private void DeviceWatcher_Updated(DeviceWatcher sender, DeviceInformationUpdate deviceInfoUpdate)
        {
            try
            {
                this?.Invoke(new Action(() =>
                {

                    // Protect against race condition if the task runs after the app stopped the deviceWatcher.
                    if (sender == deviceWatcher)
                    {
                        //存在该设备
                        if (devices.Keys.Contains(deviceInfoUpdate.Id) && devices[deviceInfoUpdate.Id] != null)
                        {
                            string id = deviceInfoUpdate.Id;
                            devices[deviceInfoUpdate.Id].Update(deviceInfoUpdate);
                            lv_device.Items[id].SubItems[0].Text = devices[deviceInfoUpdate.Id].Name;
                            lv_device.Items[id].SubItems[2].Text = devices[deviceInfoUpdate.Id].Pairing.CanPair.ToString();
                            lv_device.Items[id].SubItems[3].Text = devices[deviceInfoUpdate.Id].Pairing.IsPaired.ToString();
                        }
                    }

                }));
            }
            catch { }
        }

        private void DeviceWatcher_Removed(DeviceWatcher sender, DeviceInformationUpdate deviceInfoUpdate)
        {
            this?.Invoke(new Action(() =>
            {
                if (sender == deviceWatcher)
                {
                    string id = deviceInfoUpdate.Id;
                    if (chb_ShowUnconnectableDevice.Checked != true)
                    {
                        devices.Remove(deviceInfoUpdate.Id);
                        lv_device.Items.RemoveByKey(id);
                    }
                }
            }));
        }

        private void DeviceWatcher_EnumerationCompleted(DeviceWatcher sender, object e)
        {
            this?.Invoke(new Action(() =>
            {
                log("Search completed");
            }));
        }

        private void DeviceWatcher_Stopped(DeviceWatcher sender, object e)
        {
            this?.Invoke(new Action(() =>
            {
                log("Search was stopped");
            }));
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        private void StartBleDeviceWatcher()
        {
           //BLE device watch
           string[] requestedProperties = { "System.Devices.Aep.DeviceAddress", "System.Devices.Aep.IsConnected", "System.Devices.Aep.Bluetooth.Le.IsConnectable" };
            string aqsAllBluetoothLEDevices = "(System.Devices.Aep.ProtocolId:=\"{bb7bb05e-5972-42b5-94fc-76eaa7084d49}\")";

            deviceWatcher =
                    DeviceInformation.CreateWatcher(
                        aqsAllBluetoothLEDevices,
                        requestedProperties,
                        DeviceInformationKind.AssociationEndpoint);

            // Register event handlers before starting the watcher.
            deviceWatcher.Added += DeviceWatcher_Added;
            deviceWatcher.Updated += DeviceWatcher_Updated;
            deviceWatcher.Removed += DeviceWatcher_Removed;
            deviceWatcher.EnumerationCompleted += DeviceWatcher_EnumerationCompleted;
            deviceWatcher.Stopped += DeviceWatcher_Stopped;
            deviceWatcher.Start();

        }
        private void StartBleAdvertisementWatcher()
        {
            //BLE advertisement watcher
            var watcher = new BluetoothLEAdvertisementWatcher();
            watcher.Received += Watcher_Received;
            watcher.Start();
        }
        private void Watcher_Received(BluetoothLEAdvertisementWatcher sender, BluetoothLEAdvertisementReceivedEventArgs args)
        {
            string bleaddr = args.BluetoothAddress.ToString("x");
            bleaddr = string.Join(":", System.Text.RegularExpressions.Regex.Split(bleaddr, "(?<=\\G.{2})(?!$)"));
            this?.Invoke(new Action(() =>
            {
                foreach (ListViewItem lvi in lv_device.Items)
                {
                    if (lvi.Name.Contains(bleaddr))
                    {
                        Console.WriteLine(args.RawSignalStrengthInDBm.ToString());
                        lvi.SubItems[4].Text = args.RawSignalStrengthInDBm.ToString();
                    }
                }
            }));
        }
        private void StopBleDeviceWatcher()
        {
            if (deviceWatcher != null)
            {
                deviceWatcher.Stop();
                // Unregister the event handlers.
                deviceWatcher.Added -= DeviceWatcher_Added;
                deviceWatcher.Updated -= DeviceWatcher_Updated;
                deviceWatcher.Removed -= DeviceWatcher_Removed;
                deviceWatcher.EnumerationCompleted -= DeviceWatcher_EnumerationCompleted;
                deviceWatcher.Stopped -= DeviceWatcher_Stopped;
                deviceWatcher = null;
            }
        }
        private void btn_Search_Click(object sender, EventArgs e)
        {
            devices.Clear();
            lv_device.Items.Clear();
            log("Search begins");
            StartBleDeviceWatcher();

        }

        private void lv_device_ColumnClick(object sender, ColumnClickEventArgs e)
        {  
            // 检查点击的列是不是现在的排序列.
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // 重新设置此列的排序方法.
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // 设置排序列，默认为正向排序
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }
            // 用新的排序方法对ListView排序
            this.lv_device.Sort();
        }

        private void lv_device_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btn_Stop_Click(object sender, EventArgs e)
        {
            StopBleDeviceWatcher();
        }


        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            StartBleAdvertisementWatcher();
            log("Acquiring RSSI");
        }

        private void btn_pair_Click(object sender, EventArgs e)
        {

            GetDeviceServices();
        }

        private void chb_ShowUnconnectableDevice_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void log(string text)
        {
            status_text.Text=text;
            text = "["+DateTime.Now.ToString() + "] " + text;
            rtb_debug.Text = rtb_debug.Text + text + "\n";
            rtb_debug.Select(rtb_debug.TextLength, 0);
            rtb_debug.ScrollToCaret();
            if (rtb_debug.TextLength>2000)
            {
                rtb_debug.Text = rtb_debug.Text.Substring(rtb_debug.TextLength - 2000, 2000);
            }
        }

        private void cmb_service_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmb_characteristic.Items.Clear();
            cmb_characteristic.Text = "";
            lab_charcount.Text = "Characteristics[0]";
            //getCharacteristic();
        }
        private async void GetDeviceServices()
        {
            log("Acquiring services");
            lab_servicecount.Text = "Services[0]";
            cmb_service.Text = "";
            lab_charcount.Text = "Characteristics[0]";
            cmb_characteristic.Items.Clear();
            cmb_characteristic.Text = "";
            try
            {
                if (lv_device.Items.Count <= 0 || lv_device.SelectedItems.Count <= 0) return;
                string id = lv_device.SelectedItems[0].Name;
                string name = lv_device.SelectedItems[0].Text;
                if (id != null)
                {
                    log("Pairing device " + id+"("+name+")");
                    ble_Device = await BluetoothLEDevice.FromIdAsync(id);
                    DevicePairingResult result = await ble_Device.DeviceInformation.Pairing.PairAsync();
                    //log(result.Status.ToString());
                    if (ble_Device == null)
                    {
                        log("Failed to connect to device.");
                    }

                }
            }
            catch (Exception ex) when (ex.HResult == E_DEVICE_NOT_AVAILABLE)
            {
                log("Bluetooth radio is not on.");
            }
            if (ble_Device != null)
            {
                GattDeviceServicesResult result;
                try
                {
                    result = await ble_Device.GetGattServicesAsync(BluetoothCacheMode.Uncached);
                }
                catch
                {
                    return;
                }
                if (result.Status == GattCommunicationStatus.Success)
                {
                    var services = result.Services;
                    log(String.Format("Found {0} services", services.Count));
                    lab_servicecount.Text = "Services[" + services.Count + "]";
                    cmb_service.Items.Clear();
                    ble_Services = services;
                    foreach (var service in services)
                    {
                        string servicename = BLE_Info.GetServiceName(service);
                        cmb_service.Items.Add(servicename);
                    }
                    if (services.Count > 0)
                    {
                        cmb_service.Text = cmb_service.Items[0].ToString();
                        ble_Service = services[0];
                    }

                }
                else
                {
                    log("Device unreachable");

                }
            }
        }
        private async void getCharacteristic()
        {
            log("Acquiring characteristics");

            IReadOnlyList<GattCharacteristic> characteristics = null;
            if (cmb_service.SelectedIndex >= 0) 
                try
                {
                    ble_Service = ble_Services[cmb_service.SelectedIndex];
                    // Ensure we have access to the device.
                    var accessStatus = await ble_Service.RequestAccessAsync();
                    if (accessStatus == DeviceAccessStatus.Allowed)
                    {
                        // BT_Code: Get all the child characteristics of a service. Use the cache mode to specify uncached characterstics only 
                        // and the new Async functions to get the characteristics of unpaired devices as well. 
                        var result = await ble_Service.GetCharacteristicsAsync(BluetoothCacheMode.Uncached);
                        if (result.Status == GattCommunicationStatus.Success)
                        {
                            characteristics = result.Characteristics;
                            log(String.Format("Found {0} characteristics", characteristics.Count));
                            lab_charcount.Text = "Characteristics[" + characteristics.Count + "]";
                            ble_Characteristics = characteristics;
                            foreach (var characteristic in characteristics)
                            {
                                string characteristicname = BLE_Info.GetCharacteristicName(characteristic);
                                cmb_characteristic.Items.Add(characteristicname);
                            }
                            if (ble_Characteristics.Count > 0)
                            {
                                cmb_characteristic.Text = cmb_characteristic.Items[0].ToString();
                                ble_Characteristic = ble_Characteristics[0];
                            }
                        }
                        else
                        {
                            log(result.Status.ToString());

                            // On error, act as if there are no characteristics.
                            characteristics = new List<GattCharacteristic>();
                            lab_charcount.Text = "Characteristics[0]";
                        }
                    }
                    else
                    {
                        // Not granted access
                        log("Error accessing service.");

                        // On error, act as if there are no characteristics.
                        ble_Characteristics = new List<GattCharacteristic>();
                        lab_charcount.Text = "Characteristics[0]";

                    }
                }
                catch (Exception ex)
                {
                    log("Restricted service. Can't read characteristics: " + ex.Message);
                    // On error, act as if there are no characteristics.
                    characteristics = new List<GattCharacteristic>();
                    lab_charcount.Text = "Characteristics[0]";
                }

        }
        private async void GetData()
        {
            log("Acquiring Data Interface");
            txt_DecResult.Text = "";
            txt_HexResult.Text = "";
            txt_UTF8Result.Text = "";
            if (ble_Characteristic != null)
            {
                try
                {
                    // Get all the child descriptors of a characteristics. 
                    var result = await ble_Characteristic.GetDescriptorsAsync(BluetoothCacheMode.Uncached);
                    if (result.Status != GattCommunicationStatus.Success)
                    {
                        log("Descriptor read failure: " + result.Status.ToString());
                    }
                    // BT_Code: There's no need to access presentation format unless there's at least one. 
                    presentationFormat = null;
                    if (ble_Characteristic.PresentationFormats.Count > 0)
                    {

                        if (ble_Characteristic.PresentationFormats.Count.Equals(1))
                        {
                            // Get the presentation format since there's only one way of presenting it
                            presentationFormat = ble_Characteristic.PresentationFormats[0];
                        }
                        else
                        {
                            // It's difficult to figure out how to split up a characteristic and encode its different parts properly.
                            // In this case, we'll just encode the whole thing to a string to make it easy to print out.
                        }
                    }
                    log("Characteristic capability acquired.");
                    GattCharacteristicProperties properties = ble_Characteristic.CharacteristicProperties;
                    btn_Read.Enabled = properties.HasFlag(GattCharacteristicProperties.Read);
                    btn_Write.Enabled = properties.HasFlag(GattCharacteristicProperties.Write);
                    lab_Indicate.Enabled = properties.HasFlag(GattCharacteristicProperties.Indicate);
                    lab_Notify.Enabled = properties.HasFlag(GattCharacteristicProperties.Notify);
                    log("Data Interface acquired");
                    if (btn_Read.Enabled==true)
                    {
                        btn_Read_Click(null, null);
                    }
                }
                catch (Exception ee)
                    {
                    
                    log("Device unreachable"); 
                    }

            }
        }
        private void btn_refreshCharacteristic_Click(object sender, EventArgs e)
        {

            getCharacteristic();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void cmb_characteristic_SelectedIndexChanged(object sender, EventArgs e)
        {
            //GetData();
            if (cmb_characteristic.SelectedIndex > 0)
            {
                ble_Characteristic = ble_Characteristics[cmb_characteristic.SelectedIndex];
            }
        }

        private void btn_refreshData_Click(object sender, EventArgs e)
        {
            GetData();
        }

        private async void btn_Read_Click(object sender, EventArgs e)
        {
            log("Reading data");
            if (ble_Characteristic == null) return;
            txt_DecResult.Text = "";
            txt_HexResult.Text = "";
            txt_UTF8Result.Text = "";
            // BT_Code: Read the actual value from the device by using Uncached.
            GattReadResult result = await ble_Characteristic.ReadValueAsync(BluetoothCacheMode.Uncached);
            if (result.Status == GattCommunicationStatus.Success)
            {
                ble_result = result.Value;
                string type = "";
                string formattedResult =BLE_Info.FormatValueByPresentation(result.Value, presentationFormat,out type);
                txt_UTF8Result.Text = formattedResult;
                if (type == "HEX") { rad_hex.Checked = true; rad_dec.Checked = false; rad_utf8.Checked = false; }
                if (type == "Decimal") { rad_hex.Checked = false; rad_dec.Checked = true ; rad_utf8.Checked = false; }
                if (type == "UTF8") { rad_hex.Checked = false; rad_dec.Checked = false; rad_utf8.Checked = true; }
                log("Successfully read value from device");
                try
                {
                    byte[] data;
                    CryptographicBuffer.CopyToByteArray(ble_result, out data);
                    txt_UTF8Result.Text = Encoding.UTF8.GetString(data);
                }
                catch 
                {
                    txt_UTF8Result.Text ="";
                }
                try
                {
                    byte[] data;
                    CryptographicBuffer.CopyToByteArray(ble_result, out data);
                    txt_HexResult.Text = BitConverter.ToString(data, 0).Replace("-", " ").ToUpper();
                }
                catch 
                {
                    txt_HexResult.Text ="";
                }
                try
                {
                    byte[] data;
                    CryptographicBuffer.CopyToByteArray(ble_result, out data);
                    if (data.Length == 1) txt_DecResult.Text = data[0].ToString();
                    if (data.Length == 2) txt_DecResult.Text = BitConverter.ToInt16(data, 0).ToString();
                    if (data.Length == 4) txt_DecResult.Text = BitConverter.ToInt32(data, 0).ToString();
                    if (data.Length == 8) txt_DecResult.Text = BitConverter.ToInt64(data, 0).ToString();

                }
                catch 
                {
                    txt_DecResult.Text = "";
                }
            }

        }

        private async void btn_Write_Click(object sender, EventArgs e)
        {
            try
            {
                //存储发送过的指令
                bool foundinlist = false;
                string txt = txt_write.Text;
                foreach (string cmbitem in txt_write.Items)
                {
                    if (cmbitem == txt) foundinlist = true;
                }
                if (!foundinlist) txt_write.Items.Add(txt);

                log("Writing data");
                if (!String.IsNullOrEmpty(txt_write.Text))
                {
                    bool writeSuccessful = false;
                    if (rad_writeUTF8.Checked == true)
                    {
                        var writeBuffer = CryptographicBuffer.ConvertStringToBinary(txt_write.Text, BinaryStringEncoding.Utf8);
                        writeSuccessful = await WriteBufferToSelectedCharacteristicAsync(writeBuffer);
                    }
                    else if (rad_writeDec.Checked == true)
                    {
                        var isValidValue = Int32.TryParse(txt_write.Text, out int readValue);
                        if (isValidValue)
                        {
                            var writer = new DataWriter();
                            writer.ByteOrder = ByteOrder.LittleEndian;
                            writer.WriteInt32(readValue);

                            writeSuccessful = await WriteBufferToSelectedCharacteristicAsync(writer.DetachBuffer());
                        }
                        else
                        {
                            log("Data to write has to be an int32");
                        }
                    }
                    else
                    {
                        try
                        {
                            string s = txt_write.Text.Replace(" ", "");
                            byte[] buffer = new byte[s.Length / 2];
                            for (int i = 0; i < s.Length; i += 2)
                            {
                                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
                            }
                            writeSuccessful = await WriteBufferToSelectedCharacteristicAsync(WindowsRuntimeBufferExtensions.AsBuffer(buffer, 0, buffer.Length));
                        }
                        catch { }
                    }
                    //if (writeSuccessful) log("Write operation ok"); else log("Write operation failed");
                }
                else
                {
                    log("No data to write to device");
                }
            }catch
            {
                log("Error on write");
            }
        }
        private async Task<bool> WriteBufferToSelectedCharacteristicAsync(IBuffer buffer)
        {
            try
            {
                if (ble_Characteristic == null) return false ;
                // BT_Code: Writes the value from the buffer to the characteristic.
                var result = await ble_Characteristic.WriteValueWithResultAsync(buffer);

                if (result.Status == GattCommunicationStatus.Success)
                {
                    log("Successfully wrote value to device");
                    return true;
                }
                else
                {
                    log($"Write failed: {result.Status}");
                    return false;
                }
            }
            catch (Exception ex) when (ex.HResult == E_BLUETOOTH_ATT_INVALID_PDU)
            {
                log(ex.Message);
                return false;
            }
            catch (Exception ex) when (ex.HResult == E_BLUETOOTH_ATT_WRITE_NOT_PERMITTED || ex.HResult == E_ACCESSDENIED)
            {
                // This usually happens when a device reports that it support writing, but it actually doesn't.
                log(ex.Message);
                return false;
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {

        }

        private void lv_device_Resize(object sender, EventArgs e)
        {

        }

        private void lv_device_SizeChanged(object sender, EventArgs e)
        {
            float factor = (float)this.Width / 874;
            lv_device.Columns[0].Width = (int)(200 * factor);
            lv_device.Columns[1].Width = (int)(360 * factor);
            lv_device.Columns[2].Width = (int)(100 * factor);
            lv_device.Columns[3].Width = (int)(100 * factor);
            lv_device.Columns[4].Width = (int)(60 * factor);    
        }

        private void lv_device_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btn_pair_Click(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txt_write.Text = "steer," + textBox1.Text;
            btn_Write_Click(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txt_write.Text = "steer," + textBox2.Text;
            btn_Write_Click(sender, e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txt_write.Text = "steer," + textBox3.Text;
            btn_Write_Click(sender, e);
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            MSerialization.MSaveControl.Load_All_SupportedControls(this.Controls);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            MSaveControl.Save_All_SupportedControls(this.Controls);
        }
    }
    class ListViewSort : IComparer
    {
        private int col;
        private bool descK;
        public ListViewSort()
        {
            col = 0;
        }
        public ListViewSort(int column, object Desc)
        {
            descK = (bool)Desc;
            col = column; //当前列,0,1,2...,参数由ListView控件的ColumnClick事件传递 
        }
        public int Compare(object x, object y)
        {
            int tempInt = String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
            if (descK) return -tempInt;
            else return tempInt;
        }
    }
    class BLE_Info
    {

        /// <summary>
        ///     This enum assists in finding a string representation of a BT SIG assigned value for Service UUIDS
        ///     Reference: https://developer.bluetooth.org/gatt/services/Pages/ServicesHome.aspx
        /// </summary>
        public enum GattNativeServiceUuid : ushort
        {
            None = 0,
            AlertNotification = 0x1811,
            Battery = 0x180F,
            BloodPressure = 0x1810,
            CurrentTimeService = 0x1805,
            CyclingSpeedandCadence = 0x1816,
            DeviceInformation = 0x180A,
            GenericAccess = 0x1800,
            GenericAttribute = 0x1801,
            Glucose = 0x1808,
            HealthThermometer = 0x1809,
            HeartRate = 0x180D,
            HumanInterfaceDevice = 0x1812,
            ImmediateAlert = 0x1802,
            LinkLoss = 0x1803,
            NextDSTChange = 0x1807,
            PhoneAlertStatus = 0x180E,
            ReferenceTimeUpdateService = 0x1806,
            RunningSpeedandCadence = 0x1814,
            ScanParameters = 0x1813,
            TxPower = 0x1804,
            SimpleKeyService = 0xFFE0
        }

        /// <summary>
        ///     This enum is nice for finding a string representation of a BT SIG assigned value for Characteristic UUIDs
        ///     Reference: https://developer.bluetooth.org/gatt/characteristics/Pages/CharacteristicsHome.aspx
        /// </summary>
        public enum GattNativeCharacteristicUuid : ushort
        {
            None = 0,
            AlertCategoryID = 0x2A43,
            AlertCategoryIDBitMask = 0x2A42,
            AlertLevel = 0x2A06,
            AlertNotificationControlPoint = 0x2A44,
            AlertStatus = 0x2A3F,
            Appearance = 0x2A01,
            BatteryLevel = 0x2A19,
            BloodPressureFeature = 0x2A49,
            BloodPressureMeasurement = 0x2A35,
            BodySensorLocation = 0x2A38,
            BootKeyboardInputReport = 0x2A22,
            BootKeyboardOutputReport = 0x2A32,
            BootMouseInputReport = 0x2A33,
            CSCFeature = 0x2A5C,
            CSCMeasurement = 0x2A5B,
            CurrentTime = 0x2A2B,
            DateTime = 0x2A08,
            DayDateTime = 0x2A0A,
            DayofWeek = 0x2A09,
            DeviceName = 0x2A00,
            DSTOffset = 0x2A0D,
            ExactTime256 = 0x2A0C,
            FirmwareRevisionString = 0x2A26,
            GlucoseFeature = 0x2A51,
            GlucoseMeasurement = 0x2A18,
            GlucoseMeasurementContext = 0x2A34,
            HardwareRevisionString = 0x2A27,
            HeartRateControlPoint = 0x2A39,
            HeartRateMeasurement = 0x2A37,
            HIDControlPoint = 0x2A4C,
            HIDInformation = 0x2A4A,
            IEEE11073_20601RegulatoryCertificationDataList = 0x2A2A,
            IntermediateCuffPressure = 0x2A36,
            IntermediateTemperature = 0x2A1E,
            LocalTimeInformation = 0x2A0F,
            ManufacturerNameString = 0x2A29,
            MeasurementInterval = 0x2A21,
            ModelNumberString = 0x2A24,
            NewAlert = 0x2A46,
            PeripheralPreferredConnectionParameters = 0x2A04,
            PeripheralPrivacyFlag = 0x2A02,
            PnPID = 0x2A50,
            ProtocolMode = 0x2A4E,
            ReconnectionAddress = 0x2A03,
            RecordAccessControlPoint = 0x2A52,
            ReferenceTimeInformation = 0x2A14,
            Report = 0x2A4D,
            ReportMap = 0x2A4B,
            RingerControlPoint = 0x2A40,
            RingerSetting = 0x2A41,
            RSCFeature = 0x2A54,
            RSCMeasurement = 0x2A53,
            SCControlPoint = 0x2A55,
            ScanIntervalWindow = 0x2A4F,
            ScanRefresh = 0x2A31,
            SensorLocation = 0x2A5D,
            SerialNumberString = 0x2A25,
            ServiceChanged = 0x2A05,
            SoftwareRevisionString = 0x2A28,
            SupportedNewAlertCategory = 0x2A47,
            SupportedUnreadAlertCategory = 0x2A48,
            SystemID = 0x2A23,
            TemperatureMeasurement = 0x2A1C,
            TemperatureType = 0x2A1D,
            TimeAccuracy = 0x2A12,
            TimeSource = 0x2A13,
            TimeUpdateControlPoint = 0x2A16,
            TimeUpdateState = 0x2A17,
            TimewithDST = 0x2A11,
            TimeZone = 0x2A0E,
            TxPowerLevel = 0x2A07,
            UnreadAlertStatus = 0x2A45,
            AggregateInput = 0x2A5A,
            AnalogInput = 0x2A58,
            AnalogOutput = 0x2A59,
            CyclingPowerControlPoint = 0x2A66,
            CyclingPowerFeature = 0x2A65,
            CyclingPowerMeasurement = 0x2A63,
            CyclingPowerVector = 0x2A64,
            DigitalInput = 0x2A56,
            DigitalOutput = 0x2A57,
            ExactTime100 = 0x2A0B,
            LNControlPoint = 0x2A6B,
            LNFeature = 0x2A6A,
            LocationandSpeed = 0x2A67,
            Navigation = 0x2A68,
            NetworkAvailability = 0x2A3E,
            PositionQuality = 0x2A69,
            ScientificTemperatureinCelsius = 0x2A3C,
            SecondaryTimeZone = 0x2A10,
            String = 0x2A3D,
            TemperatureinCelsius = 0x2A1F,
            TemperatureinFahrenheit = 0x2A20,
            TimeBroadcast = 0x2A15,
            BatteryLevelState = 0x2A1B,
            BatteryPowerState = 0x2A1A,
            PulseOximetryContinuousMeasurement = 0x2A5F,
            PulseOximetryControlPoint = 0x2A62,
            PulseOximetryFeatures = 0x2A61,
            PulseOximetryPulsatileEvent = 0x2A60,
            SimpleKeyState = 0xFFE1
        }

        /// <summary>
        ///     This enum assists in finding a string representation of a BT SIG assigned value for Descriptor UUIDs
        ///     Reference: https://developer.bluetooth.org/gatt/descriptors/Pages/DescriptorsHomePage.aspx
        /// </summary>
        public enum GattNativeDescriptorUuid : ushort
        {
            CharacteristicExtendedProperties = 0x2900,
            CharacteristicUserDescription = 0x2901,
            ClientCharacteristicConfiguration = 0x2902,
            ServerCharacteristicConfiguration = 0x2903,
            CharacteristicPresentationFormat = 0x2904,
            CharacteristicAggregateFormat = 0x2905,
            ValidRange = 0x2906,
            ExternalReportReference = 0x2907,
            ReportReference = 0x2908
        }
        public static ushort ConvertUuidToShortId(Guid uuid)
        {
            // Get the short Uuid
            var bytes = uuid.ToByteArray();
            var shortUuid = (ushort)(bytes[0] | (bytes[1] << 8));
            return shortUuid;
        }
        public static string GetServiceName(GattDeviceService service)
        {
            BLE_Info.GattNativeServiceUuid serviceName;
            if (Enum.TryParse(ConvertUuidToShortId(service.Uuid).ToString(), out serviceName))
            {
                return serviceName.ToString();
            }
            return "Custom Service: " + service.Uuid;
        }
        public static string GetCharacteristicName(GattCharacteristic characteristic)
        {

            GattNativeCharacteristicUuid characteristicName;
            if (Enum.TryParse(ConvertUuidToShortId(characteristic.Uuid).ToString(),
                out characteristicName))
            {
                return characteristicName.ToString();
            }

            if (!string.IsNullOrEmpty(characteristic.UserDescription))
            {
                return characteristic.UserDescription;
            }

            else
            {
                return "Custom Characteristic: " + characteristic.Uuid;
            }
        }
        public static string FormatValueByPresentation(IBuffer buffer, GattPresentationFormat format,out string datatype)
        {
            // BT_Code: For the purpose of this sample, this function converts only UInt32 and
            // UTF-8 buffers to readable text. It can be extended to support other formats if your app needs them.
            byte[] data;
            CryptographicBuffer.CopyToByteArray(buffer, out data);
            if (format != null)
            {
                if (format.FormatType == GattPresentationFormatTypes.UInt32 && data.Length >= 4)
                {
                    datatype = "Decimal";
                    return BitConverter.ToInt32(data, 0).ToString();
                }
                else if (format.FormatType == GattPresentationFormatTypes.UInt16 && data.Length >= 2)
                {
                    datatype = "Decimal";
                    return BitConverter.ToInt16(data, 0).ToString();
                }
                else if (format.FormatType == GattPresentationFormatTypes.Utf8)
                {
                    try
                    {
                        datatype = "UTF8";
                        return Encoding.UTF8.GetString(data);
                    }
                    catch (ArgumentException)
                    {
                        datatype = "known";
                        return "(Invalid UTF-8 string)";
                    }
                }
                else
                {
                    datatype = "HEX";
                    // Add support for other format types as needed.
                    return CryptographicBuffer.EncodeToHexString(buffer);
                }
            }
            else if (data != null)
            {
                datatype = "HEX";
                return CryptographicBuffer.EncodeToHexString(buffer);
            }
            else
            {
                datatype = "HEX";
                return "Empty data received";
            }
        }

    }

}

