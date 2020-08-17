using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace TestsManGUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            button4.Enabled = false;
            listView2.View = View.Details;
            listView2.FullRowSelect = true;

            label1.Text = Properties.Settings.Default.N1;
            listView2.Items[0].Checked = Properties.Settings.Default.N2;
            listView2.Items[1].Checked = Properties.Settings.Default.N3;
            listView2.Items[2].Checked = Properties.Settings.Default.N4;
            checkBox1.Checked = Properties.Settings.Default.Ch1;
            checkBox2.Checked = Properties.Settings.Default.Ch2;
            checkBox3.Checked = Properties.Settings.Default.Ch3;
            checkBox4.Checked = Properties.Settings.Default.Ch4;
            checkBox5.Checked = Properties.Settings.Default.Ch5;
            checkBox6.Checked = Properties.Settings.Default.Ch6;
            checkBox7.Checked = Properties.Settings.Default.Ch7;
            checkBox8.Checked = Properties.Settings.Default.Ch8;
            checkBox9.Checked = Properties.Settings.Default.Ch9;
            checkBox10.Checked = Properties.Settings.Default.Ch10;
            checkBox11.Checked = Properties.Settings.Default.Ch11;
            checkBox12.Checked = Properties.Settings.Default.Ch12;
            checkBox14.Checked = Properties.Settings.Default.Ch14;
            

        }

     /// <summary>
     /// Запуск тестов, сохранение логов
     /// </summary>
     /// <param name="sender"></param>
     /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
   
            if (listView1.SelectedItems.Count > 0) 
            {



                
                var lvi = listView1.SelectedItems[0];




                SaveFileDialog saveFileDialog1 = new SaveFileDialog();

              //  saveFileDialog1.ShowDialog();
                saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";



                Process.Start("CLTestsStarter.exe", String.Format("-log={1}, -test={0}", lvi.Tag, saveFileDialog1.ShowDialog()));

                

                OpenFileDialog dg = new OpenFileDialog();
                dg.ShowDialog();

                string readfile = File.ReadAllText(dg.FileName);
                richTextBox2.Text = readfile;

               

                button8.Enabled = true;
                
            }

            else
            {
                button8.Enabled = false;
            }
           
        }
        /// <summary>
        /// Показать расположение тестов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click_1(object sender, EventArgs e)
        {
            string path = label1.Text;
  
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            FBD.SelectedPath = path;

            if (FBD.ShowDialog() == DialogResult.OK)
            {
                label1.Text = FBD.SelectedPath;
            }




/*
            var files = GetfilesinDirectorie(label1.Text);


            foreach (var filename in files)
            {

                var lines = File.ReadAllLines(filename).ToList();
                var index = lines.IndexOf("[categories]");
                if (index == -1)
                {
                    continue;
                }

                var nextItem = lines[index];
                var listCategory = new List<string>();

                while (!string.IsNullOrWhiteSpace(nextItem))
                {
                    if (index + 2 > lines.Count)
                    {
                        break;
                    }
                    nextItem = lines[index + 1];
                    listCategory.Add(nextItem);
                    index++;

                }

                var filterCategory = new List<String>();






            }*/




            }

        private void Form1_Load(object sender, EventArgs e)
        {
            button2.Enabled = false;
            button8.Enabled = false;
            button6.Enabled = false;

            splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
            splitContainer2.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
            ToolTip t = new ToolTip();
            t.SetToolTip(checkBox4, "platform acad");
            t.SetToolTip(checkBox5, "platform nano");
            t.SetToolTip(checkBox6, "platform inv");
            t.SetToolTip(checkBox7, "platform sw");
            t.SetToolTip(checkBox3, "product plantracer");
            t.SetToolTip(checkBox2, "product spds");
            t.SetToolTip(checkBox1, "product mehanics");
            t.SetToolTip(checkBox11, "configuration 3d");
            t.SetToolTip(checkBox10, "configuration enabler");
            t.SetToolTip(checkBox9, "configuration free");
            t.SetToolTip(checkBox8, "configuration toolkit");
            t.SetToolTip(checkBox12, "3dcore acis");
            t.SetToolTip(checkBox14, "3dcore c3d");
            
        }

        private List<String> GetfilesinDirectorie(string path)
        {
            var resultlist = new List<String>();
            string[] allfolders = Directory.GetDirectories(path);
            var files = Directory.GetFiles(path);
            resultlist.AddRange(files);

            foreach (var dir in allfolders)
            {
                resultlist.AddRange(GetfilesinDirectorie(dir));
            }
            return resultlist;
        }


        //==================================================================================================
        /// <summary>
        /// Отфильтровываем тесты по категориям и пропсам
        /// Переменная files получает все файлы в папке и подпапкак категории, которая указана в label1, 
        /// после чего мы считываем строки в этих файлах, если есть "[props]" и если в этой троке стоит "=" добавляем коллекцию
        /// Если включен чекбокс и в пропсах есть указанная строка, то добавляем ее вы listview1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {

            try
            {
                var files = GetfilesinDirectorie(label1.Text);

                //tag duplicates for removal
                List<ListViewItem> toRemove = new List<ListViewItem>();
                foreach (ListViewItem item1 in listView1.Items)
                {
                    foreach (ListViewItem item2 in listView1.Items)
                    {
                        //compare the two items
                        if (item1.Tag == item2.Tag)
                            toRemove.Add(item2);
                    }
                }

                //remove duplicates
                foreach (ListViewItem item in toRemove)
                {
                    listView1.Items.Remove(item);
                }

                foreach (var filename in files)
                {
                    var lines = File.ReadAllLines(filename).ToList();
                    var index = lines.IndexOf("[props]");
                    if (index == -1)
                    {
                        continue;
                    }
                    var nextItem = lines[index];
                    var listCategory = new List<string>();

                    while (!string.IsNullOrWhiteSpace(nextItem))
                    {

                        if (index + 2 > lines.Count)
                        {
                            break;
                        }
                        nextItem = lines[index + 1];
                        listCategory.Add(nextItem);
                        index++;

                    }

                    var filterCategory = new List<String>();

                    foreach (var category in listCategory)
                    {
                        var splited = category.Split('=');

                        if (splited.Count() == 2)
                        {
                            filterCategory.Add(splited[1].Trim());
                        }

                    }

                    var item = new ListViewItem(Path.GetFileName(filename));
                    item.Tag = filename;

                    //mcs spds pt
                    if (checkBox1.Checked && filterCategory.Any(c => c.Equals("mcs")))
                    {
                        listView1.Items.Add(item);
                    }

                    if (checkBox2.Checked && filterCategory.Any(c => c.Equals("spds")))
                    {
                        listView1.Items.Add(item);
                    }

                    if (checkBox3.Checked && filterCategory.Any(c => c.Equals("pt")))
                    {
                        listView1.Items.Add(item);
                    }

                    //acad nano inv sw

                    if (checkBox4.Checked && filterCategory.Any(c => c.Equals("acad")))
                    {
                        listView1.Items.Add(item);
                    }

                    if (checkBox5.Checked && filterCategory.Any(c => c.Equals("nano")))
                    {
                        listView1.Items.Add(item);
                    }

                    if (checkBox6.Checked && filterCategory.Any(c => c.Equals("inv")))
                    {
                        listView1.Items.Add(item);
                    }

                    if (checkBox7.Checked && filterCategory.Any(c => c.Equals("sw")))
                    {
                        listView1.Items.Add(item);
                    }

                    //3d enabler free toolkit
                    if (checkBox11.Checked && filterCategory.Any(c => c.Equals("3d")))
                    {
                        listView1.Items.Add(item);
                    }

                    if (checkBox10.Checked && filterCategory.Any(c => c.Equals("enabler")))
                    {
                        listView1.Items.Add(item);
                    }

                    if (checkBox9.Checked && filterCategory.Any(c => c.Equals("free")))
                    {
                        listView1.Items.Add(item);
                    }

                    if (checkBox8.Checked && filterCategory.Any(c => c.Equals("toolkit")))
                    {
                        listView1.Items.Add(item);
                    }

                    //acis c3d
                    if (checkBox12.Checked && filterCategory.Any(c => c.Equals("acis")))
                    {
                        listView1.Items.Add(item);
                    }

                    if (checkBox14.Checked && filterCategory.Any(c => c.Equals("c3d")))
                    {  
                        listView1.Items.Add(item);
                    }

                }


                foreach (var filename in files)
                {

                    var lines = File.ReadAllLines(filename).ToList();
                    var index = lines.IndexOf("[categories]");
                    if (index == -1)
                    {
                        continue;
                    }

                    var nextItem = lines[index];
                    var listCategory = new List<string>();

                    while (!string.IsNullOrWhiteSpace(nextItem))
                    {
                        if (index + 2 > lines.Count)
                        {
                            break;
                        }
                        nextItem = lines[index + 1];
                        listCategory.Add(nextItem);
                        index++;

                    }

                    var filterCategory = new List<String>();

                    if (listView2.Items[0].Checked == true && listCategory.Any(c => c.Equals("3D")))
                    {
                        var item = new ListViewItem(Path.GetFileName(filename));
                        item.Tag = filename;
                        listView1.Items.Add(item);
                    }

                    if (listView2.Items[1].Checked == true && listCategory.Any(c => c.Equals("3D Transform")))
                    {
                        var item = new ListViewItem(Path.GetFileName(filename));
                        item.Tag = filename;
                        listView1.Items.Add(item);
                    }

                    if (listView2.Items[2].Checked == true && listCategory.Any(c => c.Equals("Sheet Metal")))
                    {
                        var item = new ListViewItem(Path.GetFileName(filename));
                        item.Tag = filename;
                        listView1.Items.Add(item);
                    }

                }
            }


            catch (Exception)
            {

                string message = "Выберите папку с тестами";
                string caption = "Error";
                MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

        }


        //==================================================================================================

     
        [System.ComponentModel.Browsable(false)]
        public bool Selected { get; set; }




        /// <summary>
        /// Отображение содержимого тестов в richTextBox1 с юникодом
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button2.Enabled = false;
            button8.Enabled = false;
            
            if (listView1.SelectedItems.Count > 0)
            {

                button2.Enabled = true;

            }



            /*  var files = GetfilesinDirectorie(label1.Text);




              foreach (var filename in files)
              {

                  var lines = File.ReadAllLines(filename).ToList();
                  var index = lines.IndexOf("[main]");
                  if (index == -1)
                  {
                      continue;
                  }
                  var nextItem = lines[index];
                  var listCategory = new List<string>();

                  while (!string.IsNullOrWhiteSpace(nextItem))
                  {

                      if (index + 2 > lines.Count)
                      {
                          break;
                      }
                      nextItem = lines[index + 1];
                      listCategory.Add(nextItem);
                      index++;

                  }

                  var filterCategory = new List<String>();

                  foreach (var category in listCategory)
                  {
                      var splited = category.Split('=');

                      if (splited.Count() == 2)
                      {
                          filterCategory.Add(splited[1].Trim());
                      }

                  }

                  var item = new ListViewItem(Path.GetFileName(filename));
                  item.Tag = filename;

                  if (filterCategory.Any(c => c.Equals("cp=1251")))
                  {
                      var fullpathtofile = (String)lvi.Tag;
                      this.richTextBox1.Text = File.ReadAllText(file, Encoding.GetEncoding(1251));

                  }
                  if (filterCategory.Any(c => c.Equals("cp=65001")))
                  {
                      var fullpathtofile = (String)lvi.Tag;
                      this.richTextBox1.Text = File.ReadAllText(fullpathtofile, Encoding.GetEncoding(65001));
                  }

              }*/


            foreach (ListViewItem lvi in listView1.SelectedItems)
            {
                
                string ext = Path.GetExtension(lvi.Text).ToLower();



                if (ext == ".txt" || ext == ".tstcl")
                {

                    try
                    {                     
                        var fullpathtofile = (String)lvi.Tag;
                        this.richTextBox1.Text = File.ReadAllText(fullpathtofile, Encoding.GetEncoding(65001));
                      //  this.richTextBox1.Text = File.ReadAllText(fullpathtofile, Encoding.GetEncoding(1251));
                    }

                    catch (Exception ex)
                    {
                        return;
                    }

                }

            }

        }
       
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            button4.Enabled = false;
            button6.Enabled = false;
            if (richTextBox1.Text.Length > 0)
            {
                button4.Enabled = true;
                button6.Enabled = true;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

            SaveFileDialog saveFile1 = new SaveFileDialog();

            saveFile1.DefaultExt = "*.tstcl";

            saveFile1.Filter = "TSTCL Files|*.tstcl";

            if (saveFile1.ShowDialog() == DialogResult.OK && saveFile1.FileName.Length > 0)
            {
                richTextBox1.SaveFile(saveFile1.FileName, RichTextBoxStreamType.PlainText);
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            String[] ss = new String[0]; // Пустой массив
            richTextBox1.Lines = ss;
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }



        private void button8_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var lvi = listView1.SelectedItems[0];

                Process.Start("CLTestsStarter.exe", String.Format("-reset={0}", lvi.Tag));

            }
        }

       


        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            
                Properties.Settings.Default.N1 = label1.Text;
                Properties.Settings.Default.N2 = listView2.Items[0].Checked;
                Properties.Settings.Default.N3 = listView2.Items[1].Checked;
                Properties.Settings.Default.N4 = listView2.Items[2].Checked;
                Properties.Settings.Default.Ch1 = checkBox1.Checked;
                Properties.Settings.Default.Ch2 = checkBox2.Checked;
                Properties.Settings.Default.Ch3 = checkBox3.Checked;
                Properties.Settings.Default.Ch4 = checkBox4.Checked;
                Properties.Settings.Default.Ch5 = checkBox5.Checked;
                Properties.Settings.Default.Ch6 = checkBox6.Checked;
                Properties.Settings.Default.Ch7 = checkBox7.Checked;
                Properties.Settings.Default.Ch8 = checkBox8.Checked;
                Properties.Settings.Default.Ch9 = checkBox9.Checked;
                Properties.Settings.Default.Ch10 = checkBox10.Checked;
                Properties.Settings.Default.Ch11 = checkBox11.Checked;
                Properties.Settings.Default.Ch12 = checkBox12.Checked;
                Properties.Settings.Default.Ch14 = checkBox14.Checked;
                Properties.Settings.Default.Save();
                
        }




      

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            this.richTextBox1.Clear();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

