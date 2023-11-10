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
using BLEComm;

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
        //上一个BLE设备
        BluetoothLEDevice last_ble_Device;
        //当前设备提供的服务组
        IReadOnlyList<GattDeviceService> ble_Services = null;
        //当前选中的BLE服务
        GattDeviceService ble_Service = null;
        //当前选中的BLE属性组
        IReadOnlyList<GattCharacteristic> ble_Characteristics = null;
        //当前选中的BLE属性
        GattCharacteristic ble_Characteristic = null;
        //上一个BLE属性
        GattCharacteristic last_ble_Characteristic = null;
        //订阅的Notify或IndicateBLE属性
        GattCharacteristic callback_Characteristic = null;
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
        //从设备列表移除设备
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

        //设备枚举完成
        private void DeviceWatcher_EnumerationCompleted(DeviceWatcher sender, object e)
        {
            this?.Invoke(new Action(() =>
            {
                log("Search completed");
            }));
        }

        //终止设备搜索
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
                if (txt_deviceID.Text.Contains(bleaddr))
                {
                    txt_rssi.Text = args.RawSignalStrengthInDBm.ToString();
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
            //清除设备列表
            devices.Clear();
            lv_device.Items.Clear();
            log("Search begins");
            //启动设备搜索
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
            //解除上一个设备的回调
            UnhookNotify();
            if (last_ble_Device!=null)
            {
                last_ble_Device.ConnectionStatusChanged -= CurrentDevice_ConnectionStatusChanged;
            }
            GetDeviceServices();
        }

        private void chb_ShowUnconnectableDevice_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void log(string text)
        {
            this.Invoke(new Action(()=>{
                status_text.Text = text;
                text = "[" + DateTime.Now.ToString() + "] " + text;
                rtb_debug.Text = rtb_debug.Text + text + "\n";
                rtb_debug.Select(rtb_debug.TextLength, 0);
                rtb_debug.ScrollToCaret();
                if (rtb_debug.TextLength > 2000)
                {
                    rtb_debug.Text = rtb_debug.Text.Substring(rtb_debug.TextLength - 2000, 2000);
                }
            }));
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
            txt_rssi.Text = "";
            lab_connection.BackColor = Color.White;
            BluetoothLEDevice bledevice = null;
            try
            {
                if (lv_device.Items.Count <= 0 || lv_device.SelectedItems.Count <= 0) return;
                lab_connection.Text = "Connecting...";
                lab_connection.ForeColor = Color.Black;
                string id = lv_device.SelectedItems[0].Name;
                string name = lv_device.SelectedItems[0].Text;
                txt_devicename.Text = name;
                txt_deviceID.Text = id;

                if (id != null)
                {
                    log("Pairing device " + id+"("+name+")");
                    bledevice = await BluetoothLEDevice.FromIdAsync(id);
                    DevicePairingResult result = await bledevice.DeviceInformation.Pairing.PairAsync();
                    //log(result.Status.ToString());
                    if (bledevice == null)
                    {
                        lab_connection.BackColor = Color.Red;
                        lab_connection.ForeColor = Color.White;
                        lab_connection.Text = "Disconnected...";
                        log("Failed to connect to device.");
                    }

                }
            }
            catch (Exception ex) when (ex.HResult == E_DEVICE_NOT_AVAILABLE)
            {
                log("Bluetooth radio is not on.");
            }
            if (bledevice != null && bledevice != last_ble_Device)
            {
                last_ble_Device = ble_Device;
                ble_Device = bledevice;
                ble_Device.ConnectionStatusChanged -= CurrentDevice_ConnectionStatusChanged;
                ble_Device.ConnectionStatusChanged += CurrentDevice_ConnectionStatusChanged;
                getService();
            }
        }
        private async void getService()
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
                lab_connection.BackColor = Color.DarkGreen;
                lab_connection.Text = "Connected";
                lab_connection.ForeColor = Color.White;
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

        //控件颜色闪烁
        private void BlinkControl(Control control)
        {

            control.BackColor = Color.Blue;
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 200;
            timer.AutoReset = false;
            timer.Elapsed += delegate
            {
                control.BackColor = SystemColors.Control;
            };
            timer.Start();


        }

        private void Characteristic_ValueChanged(GattCharacteristic sender, GattValueChangedEventArgs args)
        {
            if (callback_Characteristic == null) return;
            // BT_Code: An Indicate or Notify reported that the value has changed.
            // Display the new value with a timestamp.
            GattCharacteristicProperties properties = callback_Characteristic.CharacteristicProperties;
            if (properties.HasFlag(GattCharacteristicProperties.Indicate))
            {
                log("Incicate info received");
                BlinkControl(this.lab_Indicate);
            }
            if (properties.HasFlag(GattCharacteristicProperties.Notify))
            {
                log("Notify info received");
                BlinkControl(this.lab_Notify);
            }
            if (ble_Characteristic == null) return;
            this.Invoke(new Action(() =>
            {
                txt_DecResult.Text = "";
                txt_HexResult.Text = "";
                txt_UTF8Result.Text = "";
                var result = args.CharacteristicValue;
                string type = "";
                string formattedResult = BLE_Info.FormatValueByPresentation(result, presentationFormat, out type);
                txt_UTF8Result.Text = formattedResult;
                if (type == "HEX") { rad_hex.Checked = true; rad_dec.Checked = false; rad_utf8.Checked = false; }
                if (type == "Decimal") { rad_hex.Checked = false; rad_dec.Checked = true; rad_utf8.Checked = false; }
                if (type == "UTF8") { rad_hex.Checked = false; rad_dec.Checked = false; rad_utf8.Checked = true; }
                try
                {
                    byte[] data;
                    CryptographicBuffer.CopyToByteArray(result, out data);
                    txt_UTF8Result.Text = Encoding.UTF8.GetString(data);
                }
                catch
                {
                    txt_UTF8Result.Text = "";
                }
                try
                {
                    byte[] data;
                    CryptographicBuffer.CopyToByteArray(result, out data);
                    txt_HexResult.Text = BitConverter.ToString(data, 0).Replace("-", " ").ToUpper();
                }
                catch
                {
                    txt_HexResult.Text = "";
                }
                try
                {
                    byte[] data;
                    CryptographicBuffer.CopyToByteArray(result, out data);
                    if (data.Length == 1) txt_DecResult.Text = data[0].ToString();
                    if (data.Length == 2) txt_DecResult.Text = BitConverter.ToInt16(data, 0).ToString();
                    if (data.Length == 4) txt_DecResult.Text = BitConverter.ToInt32(data, 0).ToString();
                    if (data.Length == 8) txt_DecResult.Text = BitConverter.ToInt64(data, 0).ToString();

                }
                catch
                {
                    txt_DecResult.Text = "";
                }
            }));

        }
        private void btn_refreshCharacteristic_Click(object sender, EventArgs e)
        {
            cmb_characteristic.Items.Clear();
            cmb_characteristic.Text = "";
            lab_charcount.Text = "Characteristics[0]";
            getCharacteristic();
        }


        private void cmb_characteristic_SelectedIndexChanged(object sender, EventArgs e)
        {
            //GetData();
            if (cmb_characteristic.SelectedIndex >= 0)
            {
                last_ble_Characteristic = ble_Characteristic;
                ble_Characteristic = ble_Characteristics[cmb_characteristic.SelectedIndex];
            }
        }
        private async void UnhookNotify()
        {
            if (callback_Characteristic == null) return;
            GattCharacteristicProperties callback_properties = callback_Characteristic.CharacteristicProperties;
            //特征支持Nodify或者Indicate属性
            if (callback_properties.HasFlag(GattCharacteristicProperties.Notify) || callback_properties.HasFlag(GattCharacteristicProperties.Indicate))
            {
                //解除上一个属性的Notify和Indicate
                try
                {
                    // BT_Code: Must write the CCCD in order for server to send notifications.
                    // We receive them in the ValueChanged event handler.
                    // Note that this sample configures either Indicate or Notify, but not both.
                    var result = await
                            callback_Characteristic.WriteClientCharacteristicConfigurationDescriptorAsync(
                                GattClientCharacteristicConfigurationDescriptorValue.None);
                    if (result == GattCommunicationStatus.Success)
                    {
                        callback_Characteristic.ValueChanged -= Characteristic_ValueChanged;
                    }
                    callback_Characteristic = null;
                }
                catch (Exception ex)
                {
                    // This usually happens when a device reports that it support notify, but it actually doesn't.
                    log(ex.Message);
                }
            }
        }
        //为蓝牙的characteristic设置回调使能和回调函数
        private async void HookNotify()
        {
            if (ble_Characteristic == null) return;
            GattCharacteristicProperties properties = ble_Characteristic.CharacteristicProperties;
            //特征支持Nodify或者Indicate属性
            if (properties.HasFlag(GattCharacteristicProperties.Notify) || properties.HasFlag(GattCharacteristicProperties.Indicate))
            {
                callback_Characteristic = ble_Characteristic;
                txt_callback_service.Text = cmb_service.Text;
                txt_callback_characteristic.Text = cmb_characteristic.Text;
                cb_display_callbackData.Checked = true;

                // initialize status
                GattCommunicationStatus status = GattCommunicationStatus.Unreachable;
                var cccdValue = GattClientCharacteristicConfigurationDescriptorValue.None;
                if (properties.HasFlag(GattCharacteristicProperties.Indicate))
                {
                    cccdValue = GattClientCharacteristicConfigurationDescriptorValue.Indicate;
                }

                else if (properties.HasFlag(GattCharacteristicProperties.Notify))
                {
                    cccdValue = GattClientCharacteristicConfigurationDescriptorValue.Notify;
                }

                try
                {
                    //设置回调使能
                    // BT_Code: Must write the CCCD in order for server to send indications.
                    status = await callback_Characteristic.WriteClientCharacteristicConfigurationDescriptorAsync(cccdValue);

                    if (status == GattCommunicationStatus.Success)
                    {
                        //设置回调函数
                        ble_Characteristic.ValueChanged += Characteristic_ValueChanged;
                        log("Successfully subscribed for value changes");
                    }
                    else
                    {
                        log($"Error registering for value changes: {status}");
                    }
                }
                catch (Exception ex)
                {
                    // This usually happens when a device reports that it support indicate, but it actually doesn't.
                    log(ex.Message);
                }
            }
        }
        //读数characteristic的类型，如果支持read属性则读取值
        private async void btn_getData_Click(object sender, EventArgs e)
        {
            log("Acquiring characteristic function");
            txt_DecResult.Text = "";
            txt_HexResult.Text = "";
            txt_UTF8Result.Text = "";
            if (ble_Characteristic != null)
            {
                //解除上一个设备的回调
                //UnhookNotify();
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
                    log("Characteristic function acquired.");
                    GattCharacteristicProperties properties = ble_Characteristic.CharacteristicProperties;
                    btn_Read.Enabled = properties.HasFlag(GattCharacteristicProperties.Read);
                    btn_Write.Enabled = properties.HasFlag(GattCharacteristicProperties.Write);
                    lab_Indicate.Enabled = properties.HasFlag(GattCharacteristicProperties.Indicate);
                    lab_Notify.Enabled = properties.HasFlag(GattCharacteristicProperties.Notify);
                    log("Characteristic support function {" + properties.ToString() + "}");
                    //特征支持读属性
                    if (btn_Read.Enabled == true)
                    {
                        txt_DecResult.Enabled = true;
                        txt_HexResult.Enabled = true;
                        txt_UTF8Result.Enabled = true;
                        btn_Read_Click(null, null);
                    }
                    else
                    {
                        txt_DecResult.Text = "Read function is not supported for the characteristic";
                        txt_HexResult.Text = "Read function is not supported for the characteristic";
                        txt_UTF8Result.Text = "Read function is not supported for the characteristic";
                        txt_DecResult.Enabled = false;
                        txt_HexResult.Enabled = false;
                        txt_UTF8Result.Enabled = false;
                    }
                    HookNotify();
                }
                catch (Exception ee)
                {
                    log("Device unreachable");
                }

            }
        }
        //读取charactistic的值
        private async void btn_Read_Click(object sender, EventArgs e)
        {
            BlinkControl(this.btn_Read);
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
                    if (data == null)
                    {
                        log("Target read data is not existed");
                        return;
                    }
                    if (data.Length == 1) txt_DecResult.Text = data[0].ToString();
                    if (data.Length == 2) txt_DecResult.Text = BitConverter.ToInt16(data, 0).ToString();
                    if (data.Length == 4) txt_DecResult.Text = BitConverter.ToInt32(data, 0).ToString();
                    if (data.Length == 8) txt_DecResult.Text = BitConverter.ToInt64(data, 0).ToString();

                }
                catch 
                {
                    txt_DecResult.Text = "";
                }
            }else
            {
                log("Can't perform read action. System reply is "+result.Status.ToString());
            }

        }

        //写入characteristic的值
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
        //写入值到当前激活的charactistic
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


        private void lv_device_SizeChanged(object sender, EventArgs e)
        {
            //设备列表的尺寸发生变更则按比例改变列宽
            float factor = (float)this.Width / 874;
            lv_device.Columns[0].Width = (int)(200 * factor);
            lv_device.Columns[1].Width = (int)(360 * factor);
            lv_device.Columns[2].Width = (int)(100 * factor);
            lv_device.Columns[3].Width = (int)(100 * factor);
            lv_device.Columns[4].Width = (int)(60 * factor);    
        }

        private void lv_device_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //尝试配对设备
            btn_pair_Click(sender, e);
        }



        private void Form1_Shown(object sender, EventArgs e)
        {
            //MSerialization.MSaveControl.Load_All_SupportedControls(this.Controls);
            txt_deviceID.Text = "";
            txt_devicename.Text = "";
            txt_rssi.Text = "";
            lab_connection.ForeColor = Color.Gray;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            MSaveControl.Save_All_SupportedControls(this.Controls);
        }
        /// <summary>
        /// 设备连接状态发生变化的回调函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void CurrentDevice_ConnectionStatusChanged(BluetoothLEDevice sender, object args)
        {
            if (sender.ConnectionStatus == BluetoothConnectionStatus.Disconnected && ble_Device.DeviceId != null)
            {
                //log("Device disconnected");
                this.Invoke(new Action(() => {
                    lab_connection.BackColor = Color.Red;
                    lab_connection.ForeColor = Color.White;
                    lab_connection.Text = "Disconnected...";
                }));
 
            }
            else
            {
                //log("Device connected");
                this.Invoke(new Action(() => {
                    lab_connection.BackColor = Color.Green;
                    lab_connection.ForeColor = Color.White;
                    lab_connection.Text = "Connected";
                    ble_Device = sender;
                    
                }));
            }
        }
        /// <summary>
        /// 用户定制功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            Process.Start("https://shop319667793.taobao.com/index.htm?spm=2013.1.w5002-23636016701.2.82507181okoQR7");
        }

        private void cb_display_callbackData_Click(object sender, EventArgs e)
        {
            if (cb_display_callbackData.Checked)
            {
                HookNotify();
            }
            else
            {
                UnhookNotify();
            }
        }

        private void status_text_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.miuser.net/385.html");
        }
    }

}

