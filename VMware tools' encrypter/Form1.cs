﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using System.Runtime.InteropServices;

namespace VMware_tools__encrypter
{
   
    
    public partial class Form1 : Form
    {
        [DllImport("user32")]
        public static extern void LockWorkStation();

        public static string pasw= "123",str="",encstr="",destr="";
        public Form1()
        {
            bool lll;
            InitializeComponent();
            pasw = ribbonTextBox1.TextBoxText;
            str = textBox1.Text;
          
        }
   
        public delegate void ProgressHandler(object sender, ProgressEventArgs e);
        #region AES加密

        public static byte[] TextEncrypt(string content, string secretKey)
        {
            if (secretKey.Length != 32)
            {
                MessageBox.Show("请输入32位密钥");
                return null;
            }
            byte[] data = Encoding.UTF8.GetBytes(content);
            byte[] key = Encoding.UTF8.GetBytes(secretKey);

            for (int i = 0; i < data.Length; i++)
            {
                data[i] ^= key[i % key.Length];
            }

            return data;
        }

        #endregion AES加密

        #region AES解密
 
    public static string TextDecrypt(byte[] data, string secretKey)
    {
            if (secretKey.Length!=32)
            {
                MessageBox.Show("请输入32位密钥");
                return null;
            }
        byte[] key = Encoding.UTF8.GetBytes(secretKey);

        for (int i = 0; i < data.Length; i++)
        {
            data[i] ^= key[i % key.Length];
        }

        return Encoding.UTF8.GetString(data, 0, data.Length);
    }

    #endregion AES解密
    private void ribbonButton3_Click(object sender, EventArgs e)
        {
            str = textBox1.Text;
            pasw = ribbonTextBox1.TextBoxText;
            if (pasw.Length != 8)
            {
                MessageBox.Show("密钥必须为8位", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            progressBar1.Style = ProgressBarStyle.Marquee;
            edc();
            textBox2.Text = destr;
            progressBar1.Style = ProgressBarStyle.Continuous;
            for (int i = 0; i < 100; i++)
            {
                progressBar1.Value = i;
            }
        }
        
        public static byte[] mkbyteArray = System.Text.Encoding.Default.GetBytes(str);
        public static int i=0;
        private void ribbonTextBox1_TextBoxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar < '0' || e.KeyChar > '9')
            {
                e.Handled = true;
            }
            else
            {
                i++;
                if (i > 16)
                {
                    e.Handled = true;//不处理
                }
            }



        }

        private void ribbonButton5_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(textBox1.SelectedText);
        }

        private void ribbonButton6_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(textBox1.SelectedText);
            if (textBox1.SelectedText==null)
            {
                return;
            }
            textBox1.Text = null;
        }

        private void ribbonButton4_Click(object sender, EventArgs e)
        {
          IDataObject iData = Clipboard.GetDataObject();
           
              int  idx = textBox1.SelectionStart;
        
            if (iData.GetDataPresent(DataFormats.Text))
            {
                textBox2.Text.Insert(idx,(String)iData.GetData(DataFormats.Text));
            }




        }

        private void ribbonButton7_Click(object sender, EventArgs e)
        {
            textBox1.Text = null;
        }

        private void ribbonButton8_Click(object sender, EventArgs e)
        {
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.Value = 0;
            for (int i = 0; i < 101; i++)
            {
                progressBar1.Value = i;
                Thread.Sleep(5);
            }
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.Value = 0;
            try
            {
 textBox2.Text=System.Text.Encoding.Default.GetString(  TextEncrypt(textBox1.Text, ribbonTextBox2.TextBoxText));
            }
            catch (Exception ex)
            {

                textBox2.Text = Convert.ToString(ex);
            }
           
        }

        private void ribbonButton9_Click(object sender, EventArgs e)
        {
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.Value = 0;
            for (int i = 0; i < 101; i++)
            {
                progressBar1.Value = i;
                Thread.Sleep(5);
            }
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.Value = 0;
            byte[] byteArray = System.Text.Encoding.Default.GetBytes(textBox1.Text);
            byte[] fuckbyte = System.Text.Encoding.Default.GetBytes(ribbonTextBox2.TextBoxText);
            textBox2.Text = TextDecrypt( byteArray,ribbonTextBox2.TextBoxText);
        }

        private void ribbon1_Click(object sender, EventArgs e)
        {

        }

        private void ribbonOrbRecentItem1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://space.bilibili.com/398679679");
        }

        private void ribbonOrbMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ribbonTextBox2_TextBoxKeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void ribbonButton10_Click(object sender, EventArgs e)
        {
            progressBar1.Style = ProgressBarStyle.Marquee;
            if (ribbonTextBox3.TextBoxText.Length!=8)
            {
                MessageBox.Show("请确认密钥为八位", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                progressBar1.Style = ProgressBarStyle.Continuous;
                progressBar1.Value = 0;
                return;
            }
            textBox2.Text =mD5.MD5Encrypt(textBox1.Text, ribbonTextBox3.TextBoxText);
            progressBar1.Style = ProgressBarStyle.Continuous;
            for (int i = 0; i < 101; i++)
            {
                progressBar1.Value = i;
                Thread.Sleep(5);
            }
            progressBar1.Value = 0;
        }

        private void ribbonButton11_Click(object sender, EventArgs e)
        {

            progressBar1.Style = ProgressBarStyle.Marquee;
            if (ribbonTextBox3.TextBoxText.Length != 8)
            {
                MessageBox.Show("请确认密钥为八位", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                progressBar1.Style = ProgressBarStyle.Continuous;
                progressBar1.Value = 0;
                return;
            }
            textBox2.Text = mD5.MD5Decrypt(textBox1.Text, ribbonTextBox3.TextBoxText);
            progressBar1.Style = ProgressBarStyle.Continuous;
            for (int i = 0; i < 101; i++)
            {
                progressBar1.Value = i;
                Thread.Sleep(5);
            }
            progressBar1.Value = 0;
        }
        /// <summary>
        /// 设置进度条为运行状态
        /// </summary>
        public void progbs()
        {
            progressBar1.Value = 0;
            progressBar1.Style = ProgressBarStyle.Marquee;
        }
        /// <summary>
        /// 进度条完成时设置
        /// </summary>
        public void progbe()
        {
            progressBar1.Style = ProgressBarStyle.Continuous;
            for (int i = 0; i < 101; i++)
            {
                progressBar1.Value = i;
                Thread.Sleep(5);
            }
            progressBar1.Value = 0;
        }
        private void ribbonButton12_Click(object sender, EventArgs e)
        {
            string ming = textBox1.Text, te, mi = textBox2.Text;
            if (ribbonCheckBox1.Checked == true)
            {
                Random rd = new Random();
                ribbonTextBox4.TextBoxText = Convert.ToString(rd.Next(10000000, 99999999));
            }
            progbs();
      
                if (ribbonTextBox4.TextBoxText.Length != 8)
            {
                MessageBox.Show("请确认密钥为八位", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                progressBar1.Style = ProgressBarStyle.Continuous;
                progressBar1.Value = 0;
                return;
            }
           
            te = ming;
            progbs();
            for (int i = 0; i < 5; i++)
            {
               te=mD5.MD5Encrypt(te, ribbonTextBox4.TextBoxText);
            }
            for (int i = 0; i < 5; i++)
            {
                te =VMware_tools__encrypter.CryptClass.EncryptDES(te, ribbonTextBox4.TextBoxText);
            }
            progbe();
            mi = te;
                Form1.CheckForIllegalCrossThreadCalls = false;
            textBox2.Text = mi;

            Clipboard.SetDataObject(ribbonTextBox4.TextBoxText);
            //想破解算法？？没门！
        }

        private void ribbonButton1_CanvasChanged(object sender, EventArgs e)
        {

        }

        private void ribbonButton14_Click(object sender, EventArgs e)
        {
            string ming=textBox1.Text , te, mi ;

            progbs();

            if (ribbonTextBox4.TextBoxText.Length != 8)
            {
                MessageBox.Show("请确认密钥为八位", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                progressBar1.Style = ProgressBarStyle.Continuous;
                progressBar1.Value = 0;
                return;
            }

            te = ming;
            progbs();
            for (int i = 0; i < 5; i++)
            {
                te = VMware_tools__encrypter.CryptClass.DecryptDES(te, ribbonTextBox4.TextBoxText);
            }
            for (int i = 0; i < 5; i++)
            {
                te = mD5.MD5Decrypt(te, ribbonTextBox4.TextBoxText);
            }
            progbe();
            mi = te;
            Form1.CheckForIllegalCrossThreadCalls = false;
            textBox2.Text = mi;


        }
        bool lll=false;
        private void ribbonButton15_Click(object sender, EventArgs e)
        {
            if (ribbonCheckBox4.Checked==true)
            {
                RegistryKey Huser = Registry.CurrentUser;
                RegistryKey zcb = Huser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\", true);
                RegistryKey ssub = zcb.CreateSubKey(@"Policies\System", RegistryKeyPermissionCheck.ReadWriteSubTree);

                ssub.SetValue("DisableTaskMgr", "1", RegistryValueKind.DWord);
            }
            else
            {
                  if (ribbonCheckBox4.Checked==false)
                {
                    RegistryKey Huser = Registry.CurrentUser;
                    RegistryKey zcb = Huser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\", true);
                    RegistryKey ssub = zcb.CreateSubKey(@"Policies\System", RegistryKeyPermissionCheck.ReadWriteSubTree);

                    ssub.SetValue("DisableTaskMgr", "0", RegistryValueKind.DWord);
                }
            }
            ThreadStart threadStart = new ThreadStart(Calculate);
            Thread thread = new Thread(threadStart);
            void Calculate()
            {
                while (lll == true)
                {
                    if (System.Diagnostics.Process.GetProcessesByName("taskmgr.exe").ToList().Count > 0)
                    {
                        LockWorkStation();
                    }
                }

            }
            if (ribbonCheckBox3.Checked == true)
            {
                lll = true;

                thread.Start();
               
            }
            if (ribbonCheckBox3.Checked==false)
            {
                lll = false;
                thread.Abort();
            }

            
        }

        public static void edc()
        {
            destr = CryptClass.DecryptDES(str, pasw);
        }
        public  void sten()
        {
           
           encstr= CryptClass.EncryptDES(str, pasw);
            VMware_tools__encrypter.Form1.CheckForIllegalCrossThreadCalls = false;
            Action act = delegate () {
                
};
            this.Invoke(act);
        }
        private void ribbonButton1_Click(object sender, EventArgs e)
        {
            pasw = ribbonTextBox1.TextBoxText;
            if (pasw.Length!=8)
            {
                MessageBox.Show("密钥必须为8位", "提示", MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                return;
            }
            str = textBox1.Text;
            progressBar1.Style = ProgressBarStyle.Marquee;
            /* Thread encr = new Thread(new ThreadStart(sten));
             encr.Start();*/
            sten();
            progressBar1.Value = 0;
            textBox2.Text = encstr;
            progressBar1.Style = ProgressBarStyle.Continuous;
            for (int i = 0; i < 101; i++)
            {
                progressBar1.Value = i;
                Thread.Sleep(5);
            }
        }

    }
    /// <summary>
    /// 文本加密解密类
    /// </summary>
    public class CryptClass
    {
        // 默认密钥向量
        private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为8位</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        public static string EncryptDES(string encryptString, string encryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                VMware_tools__encrypter.Form1.encstr = Convert.ToBase64String(mStream.ToArray());
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch
            {
                return "无法加密\nEncryption failed !";
            }
        }

        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public static string DecryptDES(string decryptString, string decryptKey)
        {
            if (decryptString == "")
                return "";
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey);
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                VMware_tools__encrypter.Form1.destr = Encoding.UTF8.GetString(mStream.ToArray());
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return "无法解密\nDecryption failed !";
            }
        }
    }

}
