using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using MSerialization;

namespace MSerialization

{

    public class S_TEXT
    {
        public string Text { get; set; }
    }
    public class S_CHECKED
    {
        public bool Checked { get; set; }
    }
    public class S_ITEMS
    {
        public string[] items { get; set; }
    }
    public enum SAVETYPE
    {
        TEXT = 1,
        CHECKED = 2,
        ITEMS = 3
    }
    public class MSaveControl
    {
        //保存，恢复 控件的如下属性
        //Support 
        //存储状态      Checkbox RadioButton 
        //存储文本      TextBox ComboBox
        //存储ITEMS数组 ListBox

        public static int Save_All_SupportedControls(System.Windows.Forms.Control.ControlCollection controls)
        {

            try
            {
                foreach (System.Windows.Forms.Control control in controls)
                {
                    if (!control.HasChildren)
                    {
                        if (control.Name != "")
                        {
                            if (control is System.Windows.Forms.CheckBox)
                            {
                                SaveControl(controls, control.Name, SAVETYPE.CHECKED);
                            }
                            if (control is System.Windows.Forms.RadioButton)
                            {
                                SaveControl(controls, control.Name, SAVETYPE.CHECKED);
                            }
                            if (control is System.Windows.Forms.TextBox)
                            {
                                SaveControl(controls, control.Name, SAVETYPE.TEXT);
                            }
                            if (control is System.Windows.Forms.ComboBox)
                            {
                                SaveControl(controls, control.Name, SAVETYPE.ITEMS);
                            }
                            if (control is System.Windows.Forms.ListBox)
                            {
                                SaveControl(controls, control.Name, SAVETYPE.ITEMS);
                            }
                        }
                    }
                    else
                    {
                        Save_All_SupportedControls(control.Controls);
                    }
                }
            }
            catch (Exception ee)
            {
                //MessageBox.Show(ee.StackTrace, ee.Message);
                return 0;
            }
            return 1;
        }
        public static int Load_All_SupportedControls(System.Windows.Forms.Control.ControlCollection controls)
        {
            try
            {

                foreach (System.Windows.Forms.Control control in controls)
                {
                    if (!control.HasChildren)
                    {
                        if (control.Name != "")
                        {
                            if (control is System.Windows.Forms.CheckBox)
                            {
                                LoadControl(controls, control.Name, SAVETYPE.CHECKED);
                            }
                            if (control is System.Windows.Forms.RadioButton)
                            {
                                LoadControl(controls, control.Name, SAVETYPE.CHECKED);
                            }
                            if (control is System.Windows.Forms.TextBox)
                            {
                                LoadControl(controls, control.Name, SAVETYPE.TEXT);
                            }
                            if (control is System.Windows.Forms.ComboBox)
                            {
                                LoadControl(controls, control.Name, SAVETYPE.ITEMS);
                            }
                            if (control is System.Windows.Forms.ListBox)
                            {
                                LoadControl(controls, control.Name, SAVETYPE.ITEMS);
                            }
                        }
                    }
                    else
                    {
                        Load_All_SupportedControls(control.Controls);
                    }
                }
            }
            catch (Exception ee)
            {
                //MessageBox.Show(ee.StackTrace, ee.Message);
                return 0;
            }
            return 1;
        }
        public static int SaveControl(System.Windows.Forms.Control.ControlCollection controls, string s_control, SAVETYPE savetype)
        {
            try
            {
                object control = controls.Find(s_control, true)[0];
                //如果要保存的是Text属性，可以直接用基类Control保存，无需识别控件类型
                if (savetype == SAVETYPE.TEXT)
                {
                    SavetoFile(Application.StartupPath + "\\control_" + s_control + ".xml", (System.Windows.Forms.Control)control);
                }
                //如果要保存的是CHECKED属性，需要先判别控件具体类型，做强制转换后再处理
                else if (savetype == SAVETYPE.CHECKED)
                {
                    if (control is System.Windows.Forms.CheckBox)
                    {
                        SavetoFile(Application.StartupPath + "\\control_" + s_control + ".xml", (System.Windows.Forms.CheckBox)control);
                    }
                    else if (control is System.Windows.Forms.RadioButton)
                    {
                        SavetoFile(Application.StartupPath + "\\control_" + s_control + ".xml", (System.Windows.Forms.RadioButton)control);
                    }
                }
                //如果要保存的是ITEMS属性，需要先判别控件具体类型，做强制转换后再处理
                else if (savetype == SAVETYPE.ITEMS)
                {
                    if (control is System.Windows.Forms.ListBox)
                    {
                        SavetoFile(Application.StartupPath + "\\control_" + s_control + ".xml", (System.Windows.Forms.ListBox)control);
                    }
                    else if (control is System.Windows.Forms.ComboBox)
                    {
                        SavetoFile(Application.StartupPath + "\\control_" + s_control + ".xml", (System.Windows.Forms.ComboBox)control);
                    }

                }

            }
            catch (Exception ee)
            {
               // MessageBox.Show(ee.StackTrace, ee.Message);
                return 0;
            }
            return 1;
        }
        public static int LoadControl(System.Windows.Forms.Control.ControlCollection controls, string s_control, SAVETYPE savetype)
        {
            try
            {

                if (savetype == SAVETYPE.TEXT)
                {
                    S_TEXT sptr;

                    XmlSerializer serializer = new XmlSerializer(typeof(S_TEXT));
                    TextReader reader = new StreamReader(Application.StartupPath + "\\control_" + s_control + ".xml");
                    sptr = (S_TEXT)(serializer.Deserialize(reader));
                    reader.Close();
                    System.Windows.Forms.Control control = controls.Find(s_control, true)[0];
                    control.Text = sptr.Text;
                }
                else if (savetype == SAVETYPE.CHECKED)
                {
                    object control = controls.Find(s_control, true)[0];
                    if (control is System.Windows.Forms.CheckBox)
                    {
                        System.Windows.Forms.CheckBox acontrol = (System.Windows.Forms.CheckBox)control;
                        LoadfromFile(Application.StartupPath + "\\control_" + s_control + ".xml", ref acontrol);
                    }
                    else if (control is System.Windows.Forms.RadioButton)
                    {
                        System.Windows.Forms.RadioButton acontrol = (System.Windows.Forms.RadioButton)control;
                        LoadfromFile(Application.StartupPath + "\\control_" + s_control + ".xml", ref acontrol);
                    }
                }
                else if (savetype == SAVETYPE.ITEMS)
                {
                    object control = controls.Find(s_control, true)[0];
                    if (control is System.Windows.Forms.ListBox)
                    {
                        System.Windows.Forms.ListBox acontrol = (System.Windows.Forms.ListBox)control;
                        LoadfromFile(Application.StartupPath + "\\control_" + s_control + ".xml", ref acontrol);
                    }
                    else if (control is System.Windows.Forms.ComboBox)
                    {
                        System.Windows.Forms.ComboBox acontrol = (System.Windows.Forms.ComboBox)control;
                        LoadfromFile(Application.StartupPath + "\\control_" + s_control + ".xml", ref acontrol);
                    }

                }
            }
            catch (Exception ee)
            {
                //MessageBox.Show(ee.StackTrace, ee.Message);
                return 0;
            }
            return 1;
        }

        public static int SavetoFile(String filename, System.Windows.Forms.Control control)
        {
            S_TEXT stxt = new S_TEXT();
            stxt.Text = control.Text;

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(S_TEXT));
                TextWriter writer = new StreamWriter(filename);
                serializer.Serialize(writer, stxt);
                writer.Close();
            }

            catch (Exception ee)
            {
                //MessageBox.Show(ee.StackTrace, ee.Message);
                return 0;
            }

            return 1;
        }
        public static int SavetoFile(String filename, System.Windows.Forms.CheckBox checkbox)
        {
            S_CHECKED s_Checked = new S_CHECKED();
            s_Checked.Checked = checkbox.Checked;

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(S_CHECKED));
                TextWriter writer = new StreamWriter(filename);
                serializer.Serialize(writer, s_Checked);
                writer.Close();
            }

            catch (Exception ee)
            {
                //MessageBox.Show(ee.StackTrace, ee.Message);
                return 0;
            }

            return 1;
        }
        public static int SavetoFile(String filename, System.Windows.Forms.RadioButton radbox)
        {
            S_CHECKED s_Checked = new S_CHECKED();
            s_Checked.Checked = radbox.Checked;

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(S_CHECKED));
                TextWriter writer = new StreamWriter(filename);
                serializer.Serialize(writer, s_Checked);
                writer.Close();
            }

            catch (Exception ee)
            {
               // MessageBox.Show(ee.StackTrace, ee.Message);
                return 0;
            }

            return 1;
        }
        public static int SavetoFile(String filename, System.Windows.Forms.ListBox listbox)
        {
            S_ITEMS s_items = new S_ITEMS();
            s_items.items = new string[listbox.Items.Count];

            for (int i = 0; i < listbox.Items.Count; i++)
            {
                s_items.items[i] = (string)listbox.Items[i];
            }

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(S_ITEMS));
                TextWriter writer = new StreamWriter(filename);
                serializer.Serialize(writer, s_items);
                writer.Close();
            }

            catch (Exception ee)
            {
                //MessageBox.Show(ee.StackTrace, ee.Message);
                return 0;
            }

            return 1;
        }
        public static int SavetoFile(String filename, System.Windows.Forms.ComboBox combobox)
        {
            S_ITEMS s_items = new S_ITEMS();
            s_items.items = new string[combobox.Items.Count];

            for (int i = 0; i < combobox.Items.Count; i++)
            {
                s_items.items[i] = (string)combobox.Items[i];
            }

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(S_ITEMS));
                TextWriter writer = new StreamWriter(filename);
                serializer.Serialize(writer, s_items);
                writer.Close();
            }

            catch (Exception ee)
            {
                //MessageBox.Show(ee.StackTrace, ee.Message);
                return 0;
            }

            return 1;
        }
        public static int LoadfromFile(String filename, ref System.Windows.Forms.ListBox listbox)
        {
            try
            {
                S_ITEMS sptr;

                XmlSerializer serializer = new XmlSerializer(typeof(S_ITEMS));
                TextReader reader = new StreamReader(filename);
                sptr = (S_ITEMS)(serializer.Deserialize(reader));
                reader.Close();
                listbox.Items.Clear();
                for (int i = 0; i < sptr.items.Length; i++)
                {
                    listbox.Items.Add(sptr.items[i]);
                }

            }
            catch (Exception ee)
            {
                //MessageBox.Show(ee.StackTrace, ee.Message);
                return 0;
            }
            return 1;
        }
        public static int LoadfromFile(String filename, ref System.Windows.Forms.ComboBox combobox)
        {
            try
            {
                S_ITEMS sptr;

                XmlSerializer serializer = new XmlSerializer(typeof(S_ITEMS));
                TextReader reader = new StreamReader(filename);
                sptr = (S_ITEMS)(serializer.Deserialize(reader));
                reader.Close();
                combobox.Items.Clear();
                for (int i = 0; i < sptr.items.Length; i++)
                {
                    combobox.Items.Add(sptr.items[i]);
                }

            }
            catch (Exception ee)
            {
                //MessageBox.Show(ee.StackTrace, ee.Message);
                return 0;
            }
            return 1;
        }
        public static int LoadfromFile(String filename, ref System.Windows.Forms.Control control)
        {
            try
            {
                S_TEXT sptr;

                XmlSerializer serializer = new XmlSerializer(typeof(S_TEXT));
                TextReader reader = new StreamReader(filename);
                sptr = (S_TEXT)(serializer.Deserialize(reader));
                reader.Close();
                control.Text = sptr.Text;

            }
            catch (Exception ee)
            {
                //MessageBox.Show(ee.StackTrace, ee.Message);
                return 0;
            }
            return 1;
        }
        public static int LoadfromFile(String filename, ref System.Windows.Forms.CheckBox control)
        {

            try
            {
                S_CHECKED sptr;

                XmlSerializer serializer = new XmlSerializer(typeof(S_CHECKED));
                TextReader reader = new StreamReader(filename);
                sptr = (S_CHECKED)(serializer.Deserialize(reader));
                reader.Close();
                control.Checked = sptr.Checked;

            }
            catch (Exception ee)
            {
                //MessageBox.Show(ee.StackTrace, ee.Message);
                return 0;
            }
            return 1;
        }
        public static int LoadfromFile(String filename, ref System.Windows.Forms.RadioButton control)
        {
            try
            {
                S_CHECKED sptr;

                XmlSerializer serializer = new XmlSerializer(typeof(S_CHECKED));
                TextReader reader = new StreamReader(filename);
                sptr = (S_CHECKED)(serializer.Deserialize(reader));
                reader.Close();
                control.Checked = sptr.Checked;

            }
            catch (Exception ee)
            {
                //MessageBox.Show(ee.StackTrace, ee.Message);
                return 0;
            }
            return 1;
        }
    }
    public class Config_Sample
    {
        [CategoryAttribute("1.全局设置")]
        public bool is_Debug { get; set; }
        [CategoryAttribute("1.全局设置")]
        public DateTime curr_datetime { get; set; }
        [CategoryAttribute("1.全局设置")]
        public String curr_stage { get; set; }

        [CategoryAttribute("2.通达信设置")]
        public string MSTDX_IP { get; set; }
        [CategoryAttribute("2.通达信设置")]
        public string MSTDX_Port { get; set; }
        [CategoryAttribute("2.通达信设置")]
        public string TDXConnectionStatus { get; set; }

        [CategoryAttribute("3.CTP设置")]
        public string MCCTP_ConnectString { get; set; }

        [CategoryAttribute("3.CTP设置")]
        public string MCCTP_User { get; set; }
        [CategoryAttribute("3.CTP设置")]
        public string MCCTP_Pass { get; set; }

        [CategoryAttribute("3.CTP设置")]
        public string CTPConnectionStatus { get; set; }
        [CategoryAttribute("3.CTP设置")]
        public bool is_Simulation { get; set; }

        [CategoryAttribute("4.仿真设置")]
        public string MCCTP_Sim_ConnectString { get; set; }
        [CategoryAttribute("4.仿真设置")]
        public string MCCTP_Sim_User { get; set; }
        [CategoryAttribute("4.仿真设置")]
        public string MCCTP_Sim_Pass { get; set; }

        [CategoryAttribute("5.0资金策略")]
        public double FundperShare { get; set; }
        [CategoryAttribute("5.0资金策略")]
        public long ShareQTY { get; set; }
        [CategoryAttribute("5.1选股策略")]
        public bool SelectSH { get; set; }
        [CategoryAttribute("5.1选股策略")]
        public bool SelectSZ { get; set; }
        [CategoryAttribute("5.1选股策略")]
        public bool SelectCY { get; set; }
        [CategoryAttribute("5.1选股策略")]
        public string LasK { get; set; }

        [CategoryAttribute("5.2卖出策略")]
        public bool ZhangTing { get; set; }

        [CategoryAttribute("6.运行设置")]
        public string BuyTrigerTime { get; set; }
        [CategoryAttribute("6.运行设置")]
        public string SellTrigerTime { get; set; }

        public int SavetoFile(String filename)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Config_Sample));
                TextWriter writer = new StreamWriter(filename);
                serializer.Serialize(writer, this);
                writer.Close();
            }

            catch (Exception ee)
            {
                //MessageBox.Show(ee.StackTrace, ee.Message);
                return 0;
            }

            return 1;
        }
        public static Config_Sample LoadfromFile(String filename)
        {
            try
            {
                Config_Sample sptr;

                XmlSerializer serializer = new XmlSerializer(typeof(Config_Sample));
                TextReader reader = new StreamReader(filename);
                sptr = (Config_Sample)(serializer.Deserialize(reader));
                reader.Close();
                return sptr;

            }
            catch (Exception ee)
            {
                //MessageBox.Show(ee.StackTrace, ee.Message);
                return null;
            }

        }

    }
}