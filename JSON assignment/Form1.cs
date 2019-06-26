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
        DataTable table = new DataTable();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            string pathJSON = openFileDialog1.FileName;
            string JSONdata = File.ReadAllText(pathJSON);
            // send JSON string to be parsed
            var employee = Employee.FromJson(JSONdata);


            //DataTable table = new DataTable();
            //DataView view;

            table.Columns.Add("First Name");
            table.Columns.Add("Last Name");
            table.Columns.Add("Age");
            table.Columns.Add("Type");
            table.Columns.Add("Title");
            table.Columns.Add("Salary");

            // populate rows with data from classes
            foreach (var emp in employee)
           {
               table.Rows.Add(emp.FirstName, emp.LastName, emp.Age, emp.EmployeeType, emp.Title, emp.Salary);
           }

            dataGridView1.DataSource = table;
        }


        int indexRow;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // click on row to display info in text boxes
            indexRow = e.RowIndex;
            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];

            firstName.Text = row.Cells[0].Value.ToString();
            lastName.Text = row.Cells[1].Value.ToString();
            age.Text = row.Cells[2].Value.ToString();
            type.Text = row.Cells[3].Value.ToString();
            title.Text = row.Cells[4].Value.ToString();
            salary.Text = row.Cells[5].Value.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // update information on click
            DataGridViewRow newDataRow = dataGridView1.Rows[indexRow];
            newDataRow.Cells[0].Value = firstName.Text;
            newDataRow.Cells[1].Value = lastName.Text;
            newDataRow.Cells[2].Value = age.Text;
            newDataRow.Cells[3].Value = type.Text;
            newDataRow.Cells[4].Value = title.Text;
            newDataRow.Cells[5].Value = salary.Text;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            table = dataGridView1.DataSource as DataTable;
            DataRow newDataRow = table.NewRow();

            newDataRow[0] = firstName.Text;
            newDataRow[1] = lastName.Text;
            newDataRow[2] = age.Text;
            newDataRow[3] = type.Text;
            newDataRow[4] = title.Text;
            newDataRow[5] = salary.Text;

            table.Rows.Add(newDataRow);

        }
    }
}
