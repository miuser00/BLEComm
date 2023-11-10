namespace BLEControl
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.ss_bottom = new System.Windows.Forms.StatusStrip();
            this.status_text = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.btn_Stop = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lv_device = new System.Windows.Forms.ListView();
            this.DevName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CanPaired = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IsPaired = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.RSSI = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pan_search = new System.Windows.Forms.Panel();
            this.chb_ShowUnconnectableDevice = new System.Windows.Forms.CheckBox();
            this.chb_ShowHidden = new System.Windows.Forms.CheckBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.btn_Refresh = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btn_Search = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btn_getData = new System.Windows.Forms.Button();
            this.btn_refreshCharacteristic = new System.Windows.Forms.Button();
            this.cmb_service = new System.Windows.Forms.ComboBox();
            this.lab_servicecount = new System.Windows.Forms.Label();
            this.cmb_characteristic = new System.Windows.Forms.ComboBox();
            this.lab_charcount = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.rtb_debug = new System.Windows.Forms.RichTextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.txt_callback_service = new System.Windows.Forms.TextBox();
            this.cb_display_callbackData = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_callback_characteristic = new System.Windows.Forms.TextBox();
            this.pan_read = new System.Windows.Forms.Panel();
            this.btn_Read = new System.Windows.Forms.Button();
            this.lab_Indicate = new System.Windows.Forms.Label();
            this.lab_Notify = new System.Windows.Forms.Label();
            this.panel18 = new System.Windows.Forms.Panel();
            this.panel16 = new System.Windows.Forms.Panel();
            this.pan_hex = new System.Windows.Forms.Panel();
            this.txt_HexResult = new System.Windows.Forms.TextBox();
            this.rad_hex = new System.Windows.Forms.RadioButton();
            this.panel15 = new System.Windows.Forms.Panel();
            this.pan_utf8 = new System.Windows.Forms.Panel();
            this.txt_UTF8Result = new System.Windows.Forms.TextBox();
            this.rad_utf8 = new System.Windows.Forms.RadioButton();
            this.panel17 = new System.Windows.Forms.Panel();
            this.pan_dec = new System.Windows.Forms.Panel();
            this.txt_DecResult = new System.Windows.Forms.TextBox();
            this.rad_dec = new System.Windows.Forms.RadioButton();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.panel11 = new System.Windows.Forms.Panel();
            this.pan_write = new System.Windows.Forms.Panel();
            this.txt_write = new System.Windows.Forms.ComboBox();
            this.panel9 = new System.Windows.Forms.Panel();
            this.rad_writeUTF8 = new System.Windows.Forms.RadioButton();
            this.rad_writeHex = new System.Windows.Forms.RadioButton();
            this.rad_writeDec = new System.Windows.Forms.RadioButton();
            this.btn_Write = new System.Windows.Forms.Button();
            this.panel10 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.pan_connection = new System.Windows.Forms.Panel();
            this.txt_deviceID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_rssi = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_devicename = new System.Windows.Forms.TextBox();
            this.panel13 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lab_connection = new System.Windows.Forms.Label();
            this.panel12 = new System.Windows.Forms.Panel();
            this.btn_pair = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ss_bottom.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pan_search.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.pan_read.SuspendLayout();
            this.pan_hex.SuspendLayout();
            this.pan_utf8.SuspendLayout();
            this.pan_dec.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.pan_write.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.pan_connection.SuspendLayout();
            this.SuspendLayout();
            // 
            // ss_bottom
            // 
            this.ss_bottom.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.status_text,
            this.toolStripStatusLabel1});
            this.ss_bottom.Location = new System.Drawing.Point(0, 794);
            this.ss_bottom.Name = "ss_bottom";
            this.ss_bottom.Size = new System.Drawing.Size(858, 22);
            this.ss_bottom.TabIndex = 15;
            this.ss_bottom.Text = "statusStrip1";
            // 
            // status_text
            // 
            this.status_text.IsLink = true;
            this.status_text.Name = "status_text";
            this.status_text.Size = new System.Drawing.Size(175, 17);
            this.status_text.Text = "User Manual && Source Code";
            this.status_text.Click += new System.EventHandler(this.status_text_Click);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripStatusLabel1.IsLink = true;
            this.toolStripStatusLabel1.LinkVisited = true;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(196, 17);
            this.toolStripStatusLabel1.Text = "Taobao：LuaFans miuser\'s shop";
            this.toolStripStatusLabel1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.toolStripStatusLabel1.Click += new System.EventHandler(this.toolStripStatusLabel1_Click);
            // 
            // btn_Stop
            // 
            this.btn_Stop.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_Stop.Location = new System.Drawing.Point(111, 0);
            this.btn_Stop.Name = "btn_Stop";
            this.btn_Stop.Size = new System.Drawing.Size(91, 26);
            this.btn_Stop.TabIndex = 20;
            this.btn_Stop.Text = "Stop";
            this.btn_Stop.UseVisualStyleBackColor = true;
            this.btn_Stop.Click += new System.EventHandler(this.btn_Stop_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lv_device);
            this.groupBox2.Controls.Add(this.panel1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(858, 159);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Devices";
            // 
            // lv_device
            // 
            this.lv_device.BackColor = System.Drawing.Color.White;
            this.lv_device.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.DevName,
            this.ID,
            this.CanPaired,
            this.IsPaired,
            this.RSSI});
            this.lv_device.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lv_device.FullRowSelect = true;
            this.lv_device.GridLines = true;
            this.lv_device.HideSelection = false;
            this.lv_device.Location = new System.Drawing.Point(3, 17);
            this.lv_device.Name = "lv_device";
            this.lv_device.Size = new System.Drawing.Size(852, 97);
            this.lv_device.TabIndex = 23;
            this.lv_device.UseCompatibleStateImageBehavior = false;
            this.lv_device.View = System.Windows.Forms.View.Details;
            this.lv_device.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lv_device_ColumnClick);
            this.lv_device.SelectedIndexChanged += new System.EventHandler(this.lv_device_SelectedIndexChanged);
            this.lv_device.SizeChanged += new System.EventHandler(this.lv_device_SizeChanged);
            this.lv_device.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lv_device_MouseDoubleClick);
            // 
            // DevName
            // 
            this.DevName.Text = "DevName";
            this.DevName.Width = 200;
            // 
            // ID
            // 
            this.ID.Text = "ID";
            this.ID.Width = 360;
            // 
            // CanPaired
            // 
            this.CanPaired.Text = "Can Paired";
            this.CanPaired.Width = 100;
            // 
            // IsPaired
            // 
            this.IsPaired.Text = "Is Paired";
            this.IsPaired.Width = 100;
            // 
            // RSSI
            // 
            this.RSSI.Text = "RSSI";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pan_search);
            this.panel1.Controls.Add(this.panel7);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(3, 114);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(852, 42);
            this.panel1.TabIndex = 24;
            // 
            // pan_search
            // 
            this.pan_search.Controls.Add(this.chb_ShowUnconnectableDevice);
            this.pan_search.Controls.Add(this.chb_ShowHidden);
            this.pan_search.Controls.Add(this.panel8);
            this.pan_search.Controls.Add(this.btn_Refresh);
            this.pan_search.Controls.Add(this.panel5);
            this.pan_search.Controls.Add(this.btn_Stop);
            this.pan_search.Controls.Add(this.panel6);
            this.pan_search.Controls.Add(this.btn_Search);
            this.pan_search.Controls.Add(this.panel4);
            this.pan_search.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pan_search.Location = new System.Drawing.Point(0, 9);
            this.pan_search.Name = "pan_search";
            this.pan_search.Size = new System.Drawing.Size(852, 26);
            this.pan_search.TabIndex = 23;
            // 
            // chb_ShowUnconnectableDevice
            // 
            this.chb_ShowUnconnectableDevice.AutoSize = true;
            this.chb_ShowUnconnectableDevice.Dock = System.Windows.Forms.DockStyle.Left;
            this.chb_ShowUnconnectableDevice.Location = new System.Drawing.Point(344, 0);
            this.chb_ShowUnconnectableDevice.Name = "chb_ShowUnconnectableDevice";
            this.chb_ShowUnconnectableDevice.Size = new System.Drawing.Size(138, 26);
            this.chb_ShowUnconnectableDevice.TabIndex = 31;
            this.chb_ShowUnconnectableDevice.Text = "Show Offline Device";
            this.chb_ShowUnconnectableDevice.UseVisualStyleBackColor = true;
            this.chb_ShowUnconnectableDevice.CheckedChanged += new System.EventHandler(this.chb_ShowUnconnectableDevice_CheckedChanged);
            // 
            // chb_ShowHidden
            // 
            this.chb_ShowHidden.AutoSize = true;
            this.chb_ShowHidden.Dock = System.Windows.Forms.DockStyle.Left;
            this.chb_ShowHidden.Location = new System.Drawing.Point(212, 0);
            this.chb_ShowHidden.Name = "chb_ShowHidden";
            this.chb_ShowHidden.Size = new System.Drawing.Size(132, 26);
            this.chb_ShowHidden.TabIndex = 30;
            this.chb_ShowHidden.Text = "Show Hidden Device";
            this.chb_ShowHidden.UseVisualStyleBackColor = true;
            // 
            // panel8
            // 
            this.panel8.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel8.Location = new System.Drawing.Point(202, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(10, 26);
            this.panel8.TabIndex = 29;
            // 
            // btn_Refresh
            // 
            this.btn_Refresh.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_Refresh.Location = new System.Drawing.Point(751, 0);
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.Size = new System.Drawing.Size(91, 26);
            this.btn_Refresh.TabIndex = 27;
            this.btn_Refresh.Text = "Read RSSI";
            this.btn_Refresh.UseVisualStyleBackColor = true;
            this.btn_Refresh.Click += new System.EventHandler(this.btn_Refresh_Click);
            // 
            // panel5
            // 
            this.panel5.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel5.Location = new System.Drawing.Point(842, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(10, 26);
            this.panel5.TabIndex = 28;
            // 
            // panel6
            // 
            this.panel6.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel6.Location = new System.Drawing.Point(101, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(10, 26);
            this.panel6.TabIndex = 26;
            // 
            // btn_Search
            // 
            this.btn_Search.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_Search.Location = new System.Drawing.Point(10, 0);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(91, 26);
            this.btn_Search.TabIndex = 19;
            this.btn_Search.Text = "Search";
            this.btn_Search.UseVisualStyleBackColor = true;
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // panel4
            // 
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(10, 26);
            this.panel4.TabIndex = 24;
            // 
            // panel7
            // 
            this.panel7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel7.Location = new System.Drawing.Point(0, 35);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(852, 7);
            this.panel7.TabIndex = 24;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBox3);
            this.groupBox3.Controls.Add(this.textBox2);
            this.groupBox3.Controls.Add(this.textBox1);
            this.groupBox3.Controls.Add(this.button3);
            this.groupBox3.Controls.Add(this.button2);
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox3.Location = new System.Drawing.Point(0, 694);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(858, 100);
            this.groupBox3.TabIndex = 23;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Control";
            this.groupBox3.Visible = false;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(562, 20);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(71, 21);
            this.textBox3.TabIndex = 24;
            this.textBox3.Text = "180";
            this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(347, 20);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(71, 21);
            this.textBox2.TabIndex = 23;
            this.textBox2.Text = "90";
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(155, 20);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(71, 21);
            this.textBox1.TabIndex = 22;
            this.textBox1.Text = "0";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(521, 47);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(143, 38);
            this.button3.TabIndex = 21;
            this.button3.Text = "Do not disturb";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(319, 47);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(143, 38);
            this.button2.TabIndex = 20;
            this.button2.Text = "Working";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(119, 47);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(143, 38);
            this.button1.TabIndex = 19;
            this.button1.Text = "Free";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btn_getData);
            this.groupBox4.Controls.Add(this.btn_refreshCharacteristic);
            this.groupBox4.Controls.Add(this.cmb_service);
            this.groupBox4.Controls.Add(this.lab_servicecount);
            this.groupBox4.Controls.Add(this.cmb_characteristic);
            this.groupBox4.Controls.Add(this.lab_charcount);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox4.Location = new System.Drawing.Point(0, 209);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(858, 54);
            this.groupBox4.TabIndex = 24;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Parameters";
            // 
            // btn_getData
            // 
            this.btn_getData.Location = new System.Drawing.Point(788, 17);
            this.btn_getData.Name = "btn_getData";
            this.btn_getData.Size = new System.Drawing.Size(59, 24);
            this.btn_getData.TabIndex = 29;
            this.btn_getData.Text = "Get";
            this.btn_getData.UseVisualStyleBackColor = true;
            this.btn_getData.Click += new System.EventHandler(this.btn_getData_Click);
            // 
            // btn_refreshCharacteristic
            // 
            this.btn_refreshCharacteristic.Location = new System.Drawing.Point(344, 17);
            this.btn_refreshCharacteristic.Name = "btn_refreshCharacteristic";
            this.btn_refreshCharacteristic.Size = new System.Drawing.Size(59, 24);
            this.btn_refreshCharacteristic.TabIndex = 28;
            this.btn_refreshCharacteristic.Text = "Get";
            this.btn_refreshCharacteristic.UseVisualStyleBackColor = true;
            this.btn_refreshCharacteristic.Click += new System.EventHandler(this.btn_refreshCharacteristic_Click);
            // 
            // cmb_service
            // 
            this.cmb_service.Font = new System.Drawing.Font("宋体", 12F);
            this.cmb_service.FormattingEnabled = true;
            this.cmb_service.Location = new System.Drawing.Point(85, 17);
            this.cmb_service.Name = "cmb_service";
            this.cmb_service.Size = new System.Drawing.Size(253, 24);
            this.cmb_service.TabIndex = 4;
            this.cmb_service.SelectedIndexChanged += new System.EventHandler(this.cmb_service_SelectedIndexChanged);
            // 
            // lab_servicecount
            // 
            this.lab_servicecount.AutoSize = true;
            this.lab_servicecount.Location = new System.Drawing.Point(12, 23);
            this.lab_servicecount.Name = "lab_servicecount";
            this.lab_servicecount.Size = new System.Drawing.Size(47, 12);
            this.lab_servicecount.TabIndex = 3;
            this.lab_servicecount.Text = "Service";
            // 
            // cmb_characteristic
            // 
            this.cmb_characteristic.Font = new System.Drawing.Font("宋体", 12F);
            this.cmb_characteristic.FormattingEnabled = true;
            this.cmb_characteristic.Location = new System.Drawing.Point(536, 17);
            this.cmb_characteristic.Name = "cmb_characteristic";
            this.cmb_characteristic.Size = new System.Drawing.Size(246, 24);
            this.cmb_characteristic.TabIndex = 2;
            this.cmb_characteristic.SelectedIndexChanged += new System.EventHandler(this.cmb_characteristic_SelectedIndexChanged);
            // 
            // lab_charcount
            // 
            this.lab_charcount.AutoSize = true;
            this.lab_charcount.Location = new System.Drawing.Point(420, 24);
            this.lab_charcount.Name = "lab_charcount";
            this.lab_charcount.Size = new System.Drawing.Size(89, 12);
            this.lab_charcount.TabIndex = 1;
            this.lab_charcount.Text = "Characteristic";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.rtb_debug);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox5.Location = new System.Drawing.Point(0, 550);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(858, 144);
            this.groupBox5.TabIndex = 25;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Log";
            // 
            // rtb_debug
            // 
            this.rtb_debug.BackColor = System.Drawing.SystemColors.Control;
            this.rtb_debug.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtb_debug.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_debug.Location = new System.Drawing.Point(3, 17);
            this.rtb_debug.Name = "rtb_debug";
            this.rtb_debug.ReadOnly = true;
            this.rtb_debug.Size = new System.Drawing.Size(852, 124);
            this.rtb_debug.TabIndex = 11;
            this.rtb_debug.Text = "";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.groupBox9);
            this.groupBox6.Controls.Add(this.pan_read);
            this.groupBox6.Controls.Add(this.panel16);
            this.groupBox6.Controls.Add(this.pan_hex);
            this.groupBox6.Controls.Add(this.panel15);
            this.groupBox6.Controls.Add(this.pan_utf8);
            this.groupBox6.Controls.Add(this.panel17);
            this.groupBox6.Controls.Add(this.pan_dec);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox6.Location = new System.Drawing.Point(3, 17);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(852, 211);
            this.groupBox6.TabIndex = 26;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Read";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.txt_callback_service);
            this.groupBox9.Controls.Add(this.cb_display_callbackData);
            this.groupBox9.Controls.Add(this.label5);
            this.groupBox9.Controls.Add(this.label4);
            this.groupBox9.Controls.Add(this.txt_callback_characteristic);
            this.groupBox9.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox9.Location = new System.Drawing.Point(3, 156);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(846, 50);
            this.groupBox9.TabIndex = 46;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Subscribed Characteristic";
            // 
            // txt_callback_service
            // 
            this.txt_callback_service.Location = new System.Drawing.Point(65, 20);
            this.txt_callback_service.Name = "txt_callback_service";
            this.txt_callback_service.ReadOnly = true;
            this.txt_callback_service.Size = new System.Drawing.Size(248, 21);
            this.txt_callback_service.TabIndex = 6;
            // 
            // cb_display_callbackData
            // 
            this.cb_display_callbackData.AutoSize = true;
            this.cb_display_callbackData.Location = new System.Drawing.Point(653, 23);
            this.cb_display_callbackData.Name = "cb_display_callbackData";
            this.cb_display_callbackData.Size = new System.Drawing.Size(180, 16);
            this.cb_display_callbackData.TabIndex = 44;
            this.cb_display_callbackData.Text = "Show value on Read Section";
            this.cb_display_callbackData.UseVisualStyleBackColor = true;
            this.cb_display_callbackData.Click += new System.EventHandler(this.cb_display_callbackData_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(328, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "Characteristic";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "Service";
            // 
            // txt_callback_characteristic
            // 
            this.txt_callback_characteristic.Location = new System.Drawing.Point(423, 20);
            this.txt_callback_characteristic.Name = "txt_callback_characteristic";
            this.txt_callback_characteristic.ReadOnly = true;
            this.txt_callback_characteristic.Size = new System.Drawing.Size(212, 21);
            this.txt_callback_characteristic.TabIndex = 7;
            // 
            // pan_read
            // 
            this.pan_read.Controls.Add(this.btn_Read);
            this.pan_read.Controls.Add(this.lab_Indicate);
            this.pan_read.Controls.Add(this.lab_Notify);
            this.pan_read.Controls.Add(this.panel18);
            this.pan_read.Dock = System.Windows.Forms.DockStyle.Top;
            this.pan_read.Location = new System.Drawing.Point(3, 120);
            this.pan_read.Name = "pan_read";
            this.pan_read.Size = new System.Drawing.Size(846, 36);
            this.pan_read.TabIndex = 39;
            // 
            // btn_Read
            // 
            this.btn_Read.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Read.Location = new System.Drawing.Point(226, 0);
            this.btn_Read.Name = "btn_Read";
            this.btn_Read.Size = new System.Drawing.Size(101, 30);
            this.btn_Read.TabIndex = 43;
            this.btn_Read.Text = "Read";
            this.btn_Read.UseVisualStyleBackColor = true;
            this.btn_Read.Click += new System.EventHandler(this.btn_Read_Click);
            // 
            // lab_Indicate
            // 
            this.lab_Indicate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lab_Indicate.Location = new System.Drawing.Point(12, 0);
            this.lab_Indicate.Name = "lab_Indicate";
            this.lab_Indicate.Size = new System.Drawing.Size(101, 29);
            this.lab_Indicate.TabIndex = 40;
            this.lab_Indicate.Text = "Indicate";
            this.lab_Indicate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lab_Notify
            // 
            this.lab_Notify.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lab_Notify.Location = new System.Drawing.Point(119, 0);
            this.lab_Notify.Name = "lab_Notify";
            this.lab_Notify.Size = new System.Drawing.Size(101, 29);
            this.lab_Notify.TabIndex = 41;
            this.lab_Notify.Text = "Notify";
            this.lab_Notify.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel18
            // 
            this.panel18.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel18.Location = new System.Drawing.Point(835, 0);
            this.panel18.Name = "panel18";
            this.panel18.Size = new System.Drawing.Size(11, 36);
            this.panel18.TabIndex = 39;
            // 
            // panel16
            // 
            this.panel16.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel16.Location = new System.Drawing.Point(3, 110);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(846, 10);
            this.panel16.TabIndex = 37;
            // 
            // pan_hex
            // 
            this.pan_hex.Controls.Add(this.txt_HexResult);
            this.pan_hex.Controls.Add(this.rad_hex);
            this.pan_hex.Dock = System.Windows.Forms.DockStyle.Top;
            this.pan_hex.Location = new System.Drawing.Point(3, 81);
            this.pan_hex.Name = "pan_hex";
            this.pan_hex.Size = new System.Drawing.Size(846, 29);
            this.pan_hex.TabIndex = 34;
            // 
            // txt_HexResult
            // 
            this.txt_HexResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_HexResult.Font = new System.Drawing.Font("宋体", 12F);
            this.txt_HexResult.Location = new System.Drawing.Point(65, 0);
            this.txt_HexResult.Name = "txt_HexResult";
            this.txt_HexResult.Size = new System.Drawing.Size(781, 26);
            this.txt_HexResult.TabIndex = 14;
            // 
            // rad_hex
            // 
            this.rad_hex.AutoSize = true;
            this.rad_hex.Dock = System.Windows.Forms.DockStyle.Left;
            this.rad_hex.Location = new System.Drawing.Point(0, 0);
            this.rad_hex.Name = "rad_hex";
            this.rad_hex.Size = new System.Drawing.Size(65, 29);
            this.rad_hex.TabIndex = 20;
            this.rad_hex.TabStop = true;
            this.rad_hex.Text = "Hex    ";
            this.rad_hex.UseVisualStyleBackColor = true;
            // 
            // panel15
            // 
            this.panel15.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel15.Location = new System.Drawing.Point(3, 76);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(846, 5);
            this.panel15.TabIndex = 35;
            // 
            // pan_utf8
            // 
            this.pan_utf8.Controls.Add(this.txt_UTF8Result);
            this.pan_utf8.Controls.Add(this.rad_utf8);
            this.pan_utf8.Dock = System.Windows.Forms.DockStyle.Top;
            this.pan_utf8.Location = new System.Drawing.Point(3, 48);
            this.pan_utf8.Name = "pan_utf8";
            this.pan_utf8.Size = new System.Drawing.Size(846, 28);
            this.pan_utf8.TabIndex = 33;
            // 
            // txt_UTF8Result
            // 
            this.txt_UTF8Result.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_UTF8Result.Font = new System.Drawing.Font("宋体", 12F);
            this.txt_UTF8Result.Location = new System.Drawing.Point(65, 0);
            this.txt_UTF8Result.Name = "txt_UTF8Result";
            this.txt_UTF8Result.Size = new System.Drawing.Size(781, 26);
            this.txt_UTF8Result.TabIndex = 31;
            // 
            // rad_utf8
            // 
            this.rad_utf8.AutoSize = true;
            this.rad_utf8.Dock = System.Windows.Forms.DockStyle.Left;
            this.rad_utf8.Location = new System.Drawing.Point(0, 0);
            this.rad_utf8.Name = "rad_utf8";
            this.rad_utf8.Size = new System.Drawing.Size(65, 28);
            this.rad_utf8.TabIndex = 21;
            this.rad_utf8.TabStop = true;
            this.rad_utf8.Text = "UTF8   ";
            this.rad_utf8.UseVisualStyleBackColor = true;
            // 
            // panel17
            // 
            this.panel17.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel17.Location = new System.Drawing.Point(3, 43);
            this.panel17.Name = "panel17";
            this.panel17.Size = new System.Drawing.Size(846, 5);
            this.panel17.TabIndex = 36;
            // 
            // pan_dec
            // 
            this.pan_dec.Controls.Add(this.txt_DecResult);
            this.pan_dec.Controls.Add(this.rad_dec);
            this.pan_dec.Dock = System.Windows.Forms.DockStyle.Top;
            this.pan_dec.Location = new System.Drawing.Point(3, 17);
            this.pan_dec.Name = "pan_dec";
            this.pan_dec.Size = new System.Drawing.Size(846, 26);
            this.pan_dec.TabIndex = 32;
            // 
            // txt_DecResult
            // 
            this.txt_DecResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_DecResult.Font = new System.Drawing.Font("宋体", 12F);
            this.txt_DecResult.Location = new System.Drawing.Point(65, 0);
            this.txt_DecResult.Name = "txt_DecResult";
            this.txt_DecResult.Size = new System.Drawing.Size(781, 26);
            this.txt_DecResult.TabIndex = 30;
            // 
            // rad_dec
            // 
            this.rad_dec.AutoSize = true;
            this.rad_dec.Dock = System.Windows.Forms.DockStyle.Left;
            this.rad_dec.Location = new System.Drawing.Point(0, 0);
            this.rad_dec.Name = "rad_dec";
            this.rad_dec.Size = new System.Drawing.Size(65, 26);
            this.rad_dec.TabIndex = 19;
            this.rad_dec.TabStop = true;
            this.rad_dec.Text = "Integer";
            this.rad_dec.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.panel11);
            this.groupBox7.Controls.Add(this.pan_write);
            this.groupBox7.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox7.Location = new System.Drawing.Point(3, 228);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(852, 55);
            this.groupBox7.TabIndex = 32;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Write";
            // 
            // panel11
            // 
            this.panel11.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel11.Location = new System.Drawing.Point(839, 47);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(10, 5);
            this.panel11.TabIndex = 28;
            // 
            // pan_write
            // 
            this.pan_write.Controls.Add(this.txt_write);
            this.pan_write.Controls.Add(this.panel9);
            this.pan_write.Controls.Add(this.rad_writeUTF8);
            this.pan_write.Controls.Add(this.rad_writeHex);
            this.pan_write.Controls.Add(this.rad_writeDec);
            this.pan_write.Controls.Add(this.btn_Write);
            this.pan_write.Controls.Add(this.panel10);
            this.pan_write.Dock = System.Windows.Forms.DockStyle.Top;
            this.pan_write.Location = new System.Drawing.Point(3, 17);
            this.pan_write.Name = "pan_write";
            this.pan_write.Size = new System.Drawing.Size(846, 30);
            this.pan_write.TabIndex = 0;
            // 
            // txt_write
            // 
            this.txt_write.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_write.Font = new System.Drawing.Font("宋体", 15F);
            this.txt_write.FormattingEnabled = true;
            this.txt_write.Location = new System.Drawing.Point(0, 0);
            this.txt_write.Name = "txt_write";
            this.txt_write.Size = new System.Drawing.Size(578, 28);
            this.txt_write.TabIndex = 32;
            // 
            // panel9
            // 
            this.panel9.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel9.Location = new System.Drawing.Point(578, 0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(10, 30);
            this.panel9.TabIndex = 27;
            // 
            // rad_writeUTF8
            // 
            this.rad_writeUTF8.AutoSize = true;
            this.rad_writeUTF8.Checked = true;
            this.rad_writeUTF8.Dock = System.Windows.Forms.DockStyle.Right;
            this.rad_writeUTF8.Location = new System.Drawing.Point(588, 0);
            this.rad_writeUTF8.Name = "rad_writeUTF8";
            this.rad_writeUTF8.Size = new System.Drawing.Size(47, 30);
            this.rad_writeUTF8.TabIndex = 31;
            this.rad_writeUTF8.TabStop = true;
            this.rad_writeUTF8.Text = "UTF8";
            this.rad_writeUTF8.UseVisualStyleBackColor = true;
            // 
            // rad_writeHex
            // 
            this.rad_writeHex.AutoSize = true;
            this.rad_writeHex.Dock = System.Windows.Forms.DockStyle.Right;
            this.rad_writeHex.Location = new System.Drawing.Point(635, 0);
            this.rad_writeHex.Name = "rad_writeHex";
            this.rad_writeHex.Size = new System.Drawing.Size(47, 30);
            this.rad_writeHex.TabIndex = 30;
            this.rad_writeHex.Text = "Hex ";
            this.rad_writeHex.UseVisualStyleBackColor = true;
            // 
            // rad_writeDec
            // 
            this.rad_writeDec.AutoSize = true;
            this.rad_writeDec.Dock = System.Windows.Forms.DockStyle.Right;
            this.rad_writeDec.Location = new System.Drawing.Point(682, 0);
            this.rad_writeDec.Name = "rad_writeDec";
            this.rad_writeDec.Size = new System.Drawing.Size(53, 30);
            this.rad_writeDec.TabIndex = 29;
            this.rad_writeDec.Text = "Int32";
            this.rad_writeDec.UseVisualStyleBackColor = true;
            // 
            // btn_Write
            // 
            this.btn_Write.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_Write.Location = new System.Drawing.Point(735, 0);
            this.btn_Write.Name = "btn_Write";
            this.btn_Write.Size = new System.Drawing.Size(101, 30);
            this.btn_Write.TabIndex = 22;
            this.btn_Write.Text = "Write";
            this.btn_Write.UseVisualStyleBackColor = true;
            this.btn_Write.Click += new System.EventHandler(this.btn_Write_Click);
            // 
            // panel10
            // 
            this.panel10.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel10.Location = new System.Drawing.Point(836, 0);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(10, 30);
            this.panel10.TabIndex = 26;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox7);
            this.groupBox1.Controls.Add(this.groupBox6);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 263);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(858, 287);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Characteristic data";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.pan_connection);
            this.groupBox8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox8.Location = new System.Drawing.Point(0, 159);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(858, 50);
            this.groupBox8.TabIndex = 26;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Connection";
            // 
            // pan_connection
            // 
            this.pan_connection.Controls.Add(this.txt_deviceID);
            this.pan_connection.Controls.Add(this.label3);
            this.pan_connection.Controls.Add(this.txt_rssi);
            this.pan_connection.Controls.Add(this.label2);
            this.pan_connection.Controls.Add(this.txt_devicename);
            this.pan_connection.Controls.Add(this.panel13);
            this.pan_connection.Controls.Add(this.label1);
            this.pan_connection.Controls.Add(this.lab_connection);
            this.pan_connection.Controls.Add(this.panel12);
            this.pan_connection.Controls.Add(this.btn_pair);
            this.pan_connection.Controls.Add(this.panel2);
            this.pan_connection.Dock = System.Windows.Forms.DockStyle.Top;
            this.pan_connection.Location = new System.Drawing.Point(3, 17);
            this.pan_connection.Name = "pan_connection";
            this.pan_connection.Size = new System.Drawing.Size(852, 21);
            this.pan_connection.TabIndex = 44;
            // 
            // txt_deviceID
            // 
            this.txt_deviceID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_deviceID.Font = new System.Drawing.Font("宋体", 9F);
            this.txt_deviceID.Location = new System.Drawing.Point(354, 0);
            this.txt_deviceID.Name = "txt_deviceID";
            this.txt_deviceID.Size = new System.Drawing.Size(194, 21);
            this.txt_deviceID.TabIndex = 50;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.label3.Location = new System.Drawing.Point(548, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 21);
            this.label3.TabIndex = 52;
            this.label3.Text = " RSSI";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txt_rssi
            // 
            this.txt_rssi.Dock = System.Windows.Forms.DockStyle.Right;
            this.txt_rssi.Font = new System.Drawing.Font("宋体", 9F);
            this.txt_rssi.Location = new System.Drawing.Point(588, 0);
            this.txt_rssi.Name = "txt_rssi";
            this.txt_rssi.Size = new System.Drawing.Size(29, 21);
            this.txt_rssi.TabIndex = 53;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Location = new System.Drawing.Point(288, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 21);
            this.label2.TabIndex = 51;
            this.label2.Text = " DeviceID";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txt_devicename
            // 
            this.txt_devicename.Dock = System.Windows.Forms.DockStyle.Left;
            this.txt_devicename.Font = new System.Drawing.Font("宋体", 9F);
            this.txt_devicename.Location = new System.Drawing.Point(78, 0);
            this.txt_devicename.Name = "txt_devicename";
            this.txt_devicename.Size = new System.Drawing.Size(210, 21);
            this.txt_devicename.TabIndex = 47;
            // 
            // panel13
            // 
            this.panel13.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel13.Location = new System.Drawing.Point(617, 0);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(21, 21);
            this.panel13.TabIndex = 48;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 21);
            this.label1.TabIndex = 46;
            this.label1.Text = "Device Name ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lab_connection
            // 
            this.lab_connection.BackColor = System.Drawing.SystemColors.Control;
            this.lab_connection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lab_connection.Dock = System.Windows.Forms.DockStyle.Right;
            this.lab_connection.ForeColor = System.Drawing.Color.White;
            this.lab_connection.Location = new System.Drawing.Point(638, 0);
            this.lab_connection.Name = "lab_connection";
            this.lab_connection.Size = new System.Drawing.Size(92, 21);
            this.lab_connection.TabIndex = 44;
            this.lab_connection.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel12
            // 
            this.panel12.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel12.Location = new System.Drawing.Point(730, 0);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(21, 21);
            this.panel12.TabIndex = 45;
            // 
            // btn_pair
            // 
            this.btn_pair.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btn_pair.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_pair.Location = new System.Drawing.Point(751, 0);
            this.btn_pair.Name = "btn_pair";
            this.btn_pair.Size = new System.Drawing.Size(91, 21);
            this.btn_pair.TabIndex = 54;
            this.btn_pair.Text = "Pair";
            this.btn_pair.UseVisualStyleBackColor = false;
            this.btn_pair.Click += new System.EventHandler(this.btn_pair_Click);
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(842, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(10, 21);
            this.panel2.TabIndex = 55;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(858, 816);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.ss_bottom);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "BLEComm v0.3";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.ss_bottom.ResumeLayout(false);
            this.ss_bottom.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.pan_search.ResumeLayout(false);
            this.pan_search.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.pan_read.ResumeLayout(false);
            this.pan_hex.ResumeLayout(false);
            this.pan_hex.PerformLayout();
            this.pan_utf8.ResumeLayout(false);
            this.pan_utf8.PerformLayout();
            this.pan_dec.ResumeLayout(false);
            this.pan_dec.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.pan_write.ResumeLayout(false);
            this.pan_write.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.pan_connection.ResumeLayout(false);
            this.pan_connection.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip ss_bottom;
        private System.Windows.Forms.Button btn_Stop;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView lv_device;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox cmb_service;
        private System.Windows.Forms.Label lab_servicecount;
        private System.Windows.Forms.ComboBox cmb_characteristic;
        private System.Windows.Forms.Label lab_charcount;
        private System.Windows.Forms.ColumnHeader DevName;
        private System.Windows.Forms.ColumnHeader ID;
        private System.Windows.Forms.ColumnHeader CanPaired;
        private System.Windows.Forms.ColumnHeader IsPaired;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_Search;
        private System.Windows.Forms.Panel pan_search;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.ColumnHeader RSSI;
        private System.Windows.Forms.Button btn_Refresh;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.CheckBox chb_ShowHidden;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.CheckBox chb_ShowUnconnectableDevice;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RichTextBox rtb_debug;
        private System.Windows.Forms.ToolStripStatusLabel status_text;
        private System.Windows.Forms.Button btn_refreshCharacteristic;
        private System.Windows.Forms.Button btn_getData;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Panel pan_read;
        private System.Windows.Forms.Label lab_Indicate;
        private System.Windows.Forms.Label lab_Notify;
        private System.Windows.Forms.Panel panel18;
        private System.Windows.Forms.Panel panel16;
        private System.Windows.Forms.Panel pan_hex;
        private System.Windows.Forms.TextBox txt_HexResult;
        private System.Windows.Forms.RadioButton rad_hex;
        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.Panel pan_utf8;
        private System.Windows.Forms.TextBox txt_UTF8Result;
        private System.Windows.Forms.RadioButton rad_utf8;
        private System.Windows.Forms.Panel panel17;
        private System.Windows.Forms.Panel pan_dec;
        private System.Windows.Forms.TextBox txt_DecResult;
        private System.Windows.Forms.RadioButton rad_dec;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Panel pan_write;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Button btn_Write;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.RadioButton rad_writeDec;
        private System.Windows.Forms.RadioButton rad_writeUTF8;
        private System.Windows.Forms.RadioButton rad_writeHex;
        private System.Windows.Forms.ComboBox txt_write;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Button btn_Read;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Panel pan_connection;
        private System.Windows.Forms.TextBox txt_devicename;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lab_connection;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.TextBox txt_deviceID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_rssi;
        private System.Windows.Forms.Button btn_pair;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.TextBox txt_callback_service;
        private System.Windows.Forms.CheckBox cb_display_callbackData;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_callback_characteristic;
    }
}

