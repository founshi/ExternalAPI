using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace APISManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            APIList _APIList = new APIList();
            _APIList.API_SerialKey = Guid.NewGuid().ToString();
            _APIList.API_Assemble = this.textBox2.Text.Trim();
            _APIList.API_ClassName = this.textBox4.Text.Trim();
            _APIList.API_CreateTime = DateTime.Now;
            _APIList.API_FunctionName = this.textBox5.Text.Trim();
            _APIList.API_IsUsed = true;
            _APIList.API_NameSpace = this.textBox3.Text.Trim();
            _APIList.API_Path = this.textBox1.Text.Trim();

            CoreAPIList _CoreAPIList = new CoreAPIList();
            _CoreAPIList.AddEntity(_APIList);

            MessageBox.Show("新增完成");

        }
    }
}
