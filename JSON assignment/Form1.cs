using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;

using System.Windows.Forms;
using System.IO;

namespace JSON_assignment
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            string pathJSON = openFileDialog1.FileName;
            string JSONdata = File.ReadAllText(pathJSON);
            //parseJSON(pathJSON);

            //using Employee;
            //
            var employee = Employee.FromJson(JSONdata);


            DataTable table = new DataTable();
            DataView view;

            foreach (var emp in employee)
           {
               table.Rows.Add(emp.FirstName, emp.LastName, emp.Age, emp.EmployeeType, emp.Title, emp.Salary);

           }


            view = new DataView(table);
            dataGridView1.DataSource = view;

        }

        public void parseJSON(string path)
        {

        }

        public void addRows()
        {


        }

    }
}
