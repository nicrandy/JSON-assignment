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
        bool tableExists = false;
        string searchJSON;
        bool searchPath = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tableExists == true)
            {
                DialogResult dialog = MessageBox.Show("Loading new file will overwrite current table data. Are you sure you want to do this?",
                       "Load new file", MessageBoxButtons.OKCancel);
                if (dialog == DialogResult.OK)
                {
                    tableExists = false;

                }
            }
            if (tableExists == false)
            {

                //clear data if populated
                ClearDataTable();

                openFileDialog1.ShowDialog();
                string pathJSON = openFileDialog1.FileName;
                string fileExt = Path.GetExtension(pathJSON);

                // verify that file is .json
                if (fileExt == ".json")
                {
                    string newJSONdata = File.ReadAllText(pathJSON);
                    searchJSON = newJSONdata;
                    ParseJsonData(newJSONdata);
                    tableExists = true;

                }
                else
                {
                    string text = "File selected is not .json file";
                    MessageBox.Show(text);
                }



            }
        }

        private void ReadExistingFile_Click(object sender, EventArgs e)
        {
            if (tableExists == true)
            {
                DialogResult dialog = MessageBox.Show("Loading new file will overwrite current table data. Are you sure you want to do this?",
                       "Load new file", MessageBoxButtons.OKCancel);
                if (dialog == DialogResult.OK)
                {
                    tableExists = false;
                }
            }
            if (tableExists == false)
            {
                if (File.Exists("NEWjson.json"))
                {
                    //clear data if populated
                    ClearDataTable();

                    string oldJSONdata = File.ReadAllText("NEWjson.json");
                    ParseJsonData(oldJSONdata);
                    searchJSON = oldJSONdata;
                    tableExists = true;
                }
                else
                {
                    string text = "File doesn't exist.....yet!";
                    MessageBox.Show(text);
                }
            }
        }

        private void ClearDataTable()
        {
            // clear datatable if data already exists
            table.Rows.Clear();
            table.Columns.Clear();
            dataGridView1.DataSource = null;
            dataGridView1.Refresh();
        }

        public void ParseJsonData(string JsonDataSend)
        {
            // send JSON string to be parsed
            var employee = Employee.FromJson(JsonDataSend);

            table.Columns.Add("FirstName");
            table.Columns.Add("LastName");
            table.Columns.Add("Age");
            table.Columns.Add("EmployeeType");
            table.Columns.Add("Title");
            table.Columns.Add("Salary");

            if (searchPath == false)
            {
                // populate rows with data from classes
                foreach (var emp in employee)
                {
                    table.Rows.Add(emp.FirstName, emp.LastName, emp.Age, emp.EmployeeType, emp.Title, emp.Salary);
                }

            }
            if (searchPath == true)
            {
                var last =
                from e in employee
                where e.LastName == lastSearch.Text
                select e;

                foreach (var emp in last)
                {
                    table.Rows.Add(emp.FirstName, emp.LastName, emp.Age, emp.EmployeeType, emp.Title, emp.Salary);
                }

            }

            dataGridView1.DataSource = table;
            searchPath = false;

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
            try
            {
                newDataRow.Cells[2].Value = int.Parse(age.Text);

            }
            catch (FormatException)
            {

                string text = "Enter numbers only for age";
                MessageBox.Show(text);
            }
            newDataRow.Cells[3].Value = type.Text;
            newDataRow.Cells[4].Value = title.Text;
            try
            {
                newDataRow.Cells[5].Value = int.Parse(salary.Text);

            }
            catch (FormatException)
            {

                string text = "Enter numbers only for salary. No , or $ ";
                MessageBox.Show(text);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // add new record to table
            table = dataGridView1.DataSource as DataTable;
            DataRow newDataRow = table.NewRow();

            newDataRow[0] = firstName.Text;
            newDataRow[1] = lastName.Text;
            try
            {
                newDataRow[2] = int.Parse(age.Text);
            }
            catch (FormatException)
            {

                string text = "Enter numbers only for age";
                MessageBox.Show(text);
            }
            newDataRow[3] = type.Text;
            newDataRow[4] = title.Text;
            try
            {
                newDataRow[5] = int.Parse(salary.Text);

            }
            catch (FormatException)
            {

                string text = "Enter numbers only for salary. No , or $ ";
                MessageBox.Show(text);
            }

            table.Rows.Add(newDataRow);

        }


        private void serializeJSON_Click(object sender, EventArgs e)
        {
            BuildTable(table);
        }

        public string BuildTable(DataTable table)
        {
            // create new JSON string
            var JSONString = new StringBuilder();
            if (table.Rows.Count > 0)
            {
                JSONString.Append("[");
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    JSONString.Append("{");
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        if (j < table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\",");
                        }
                        else if (j == table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\"");
                        }
                    }
                    if (i == table.Rows.Count - 1)
                    {
                        JSONString.Append("}");
                    }
                    else
                    {
                        JSONString.Append("},");
                    }
                }
                JSONString.Append("]");
            }
            string result = JSONString.ToString();
            JSONstringBox.Text = result;

            return JSONString.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // write JSON data to JSON file
            if (string.IsNullOrWhiteSpace(JSONstringBox.Text))
            {
                string text = "Select data to serialize first!";
                MessageBox.Show(text);
            }
            else
            {
                DialogResult dialog = MessageBox.Show("Saving file will overwite existing file. Are you sure you want to do this?",
                                                       "Save and overwrite", MessageBoxButtons.OKCancel);
                if (dialog == DialogResult.OK)
                {
                    File.WriteAllText("NEWjson.json", JSONstringBox.Text);

                }
                else if (dialog == DialogResult.Cancel)
                {
                }
            }
        }

        private void clearTextBoxes_Click(object sender, EventArgs e)
        {
            firstName.Clear();
            lastName.Clear();
            age.Clear();
            type.Clear();
            title.Clear();
            salary.Clear();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // set search to true
            searchPath = true;
            ClearDataTable();
            ParseJsonData(searchJSON);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            searchPath = false;
            ClearDataTable();
            ParseJsonData(searchJSON);

        }
    }
}
