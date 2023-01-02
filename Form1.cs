using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace SeleniumTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

        }
        ChromeDriver drv;
        Thread th;

        private void button1_Click(object sender, EventArgs e)
        {            
            th = new Thread(Result);
            th.Start();
        }

        private void Result()
        {
            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;
            drv = new ChromeDriver(@"C:\Users\tugayo\Desktop");
            drv.Navigate().GoToUrl("https://www.facebook.com/login");//Selenium üzerinden açılacak site linki
            Thread.Sleep(3000);
            Login(userNametxt.Text, passwordtxt.Text);   
            if (TestAccount())
                MessageBox.Show("Successfully", "Infotmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Username or Password is wrong!", "Warring", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        private bool TestAccount()
        {
            if (drv.Url == "https://www.facebook.com/")//Başarılı giriş sonrası açılacak sayfa
                return true;
            else
                return false;
        }

        private void Login(string userName, string password)
        {
            try
            {
                drv.FindElements(By.XPath("//input[@class='inputtext _55r1 inputtext _1kbt inputtext _1kbt']"))[0].SendKeys(userName);//Kaynak kod içerisinden username adresi
                Thread.Sleep(3000);
                drv.FindElements(By.XPath("//input[@class='inputtext _55r1 inputtext _9npi inputtext _9npi']"))[0].SendKeys(password);//Kaynak kod içerisinden password adresi
                Thread.Sleep(3000);
                drv.FindElement(By.XPath("//button[@class='_42ft _4jy0 _52e0 _4jy6 _4jy1 selected _51sy']")).Click();//Kaynak kod içerisinden login button adresi
                Thread.Sleep(1000);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            drv.Quit();
        }        
        
    }
}
