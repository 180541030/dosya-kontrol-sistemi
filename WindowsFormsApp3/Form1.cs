using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Spire.Doc;
using Spire.Doc.Documents;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        private TextBox vertical;
        private string errorText;

        public Form1()
        {
            InitializeComponent();
        }

        private void SetFourDifferentScrollBars()
        {

            this.vertical = new System.Windows.Forms.TextBox();
           
           
            string startString = errorText;

            
            vertical.Location = new Point(10, 70);
            vertical.ScrollBars = ScrollBars.Vertical;
            vertical.Multiline = true;
            vertical.Text = startString + ScrollBars.Vertical.ToString();

           
            this.Controls.Add(this.vertical);
           
        }


        private void button1_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                }
            }

            Document document = new Document();
            document.LoadFromFile(filePath);

            Section section = document.Sections[0];

            Paragraph p1 = section.Paragraphs[0];
            Paragraph p2 = section.Paragraphs[1];
            Paragraph pOnsoz = section.Paragraphs[25];
            

            int pKaynakcaNumber = 0;
            

                string errorText = "";
           

                for (int i = section.Paragraphs.Count - 1; i >= 0; i--)
            {
                if (section.Paragraphs[i].Text.Trim() == "KAYNAKÇA")
                {
                    pKaynakcaNumber = i;
                }
       
            }

         

            Paragraph pKaynakca = section.Paragraphs[pKaynakcaNumber];

         

            string fontType = "Times New Roman";
            string FontSize = "16";
            string fontType2 = "Calibri";
            string FontSize2 = "18";
            









            if (p1.BreakCharacterFormat.FontName != fontType)
            {
                errorText += "* Üniversite adı yazı tipi hatalıdır ve " + p1.BreakCharacterFormat.FontName + " dir. " + fontType + " olmalıdır.";
            }
            if (p1.BreakCharacterFormat.FontSize != 16)
            {
                errorText += "\n* Üniversite adı yazı boyutu hatalıdır ve " + p1.BreakCharacterFormat.FontSize + " dir. " + FontSize + " olmalıdır.";
            }
           





            if (p2.BreakCharacterFormat.FontName != fontType2)
            {
                errorText += "\n* yazı tipi hatalıdır ve " + p2.BreakCharacterFormat.FontName + " dir. " + fontType2 + " olmalıdır.";
            }
            if (p2.BreakCharacterFormat.FontSize != 16)
            {
                errorText += "\n*  yazı boyutu hatalıdır ve " + p2.BreakCharacterFormat.FontSize + " dir. " + FontSize2 + " olmalıdır.";
            }



            if (pOnsoz.BreakCharacterFormat.FontName != fontType2)
            {
                errorText += "\n* Önsöz yazı tipi hatalıdır ve " + pOnsoz.BreakCharacterFormat.FontName + " dir. " + fontType2 + " olmalıdır.";
            }
            if (pOnsoz.BreakCharacterFormat.FontSize != 14)
            {
                errorText += "\n* Önsöz yazı boyutu hatalıdır ve " + pOnsoz.BreakCharacterFormat.FontSize + " dir. " + "14" + " olmalıdır.";
            }




            if (pKaynakca.BreakCharacterFormat.FontName != fontType)
            {
                errorText += "\n* Kaynakça yazı tipi hatalıdır ve " + pKaynakca.BreakCharacterFormat.FontName + " dir. " + fontType + " olmalıdır.";
            }
            if (pKaynakca.BreakCharacterFormat.FontSize != 18)
            {
                errorText += "\n* Kaynakça yazı boyutu hatalıdır ve " + pKaynakca.BreakCharacterFormat.FontSize + " dir. " + "18" + " olmalıdır.";
            }




                for (int i = pKaynakcaNumber + 1; i <= section.Paragraphs.Count - 1; i++)
                {
                if (section.Paragraphs[i].Text.Trim() != "")
                {
                    if (section.Paragraphs[i].Text.Substring(0, 1) == "[")
                    {
                        if (section.Paragraphs[i].Text.IndexOf(",") == -1)
                        {
                            errorText += "\n* Şu kaynakça hatalıdır. Kaynakça tipi hatalıdır. İsim veya adres belirtmede hatalar var -> " + section.Paragraphs[i].Text;
                        }
                    }
                    else
                    {
                        errorText += "\n* Şu kaynakça hatalıdır. Başında parantez  yok -> " + section.Paragraphs[i].Text;
                    }
                }
            }

            for (int i = 50; i < pKaynakcaNumber; i++)
            {
                if (section.Paragraphs[i].Text.Trim() != "")
                {
                    if (section.Paragraphs[i].BreakCharacterFormat.FontName != fontType2)
                    {
                        errorText += "\n* İçerikte yazı tipi hatası var. " + section.Paragraphs[i].BreakCharacterFormat.FontName + " yazı tipini -> " + fontType2 + " olarak değiştirin";
                    }
                    if (section.Paragraphs[i].BreakCharacterFormat.FontSize != 12)
                    {
                        errorText += "\n* İçerikte yazı boyutu hatası var. " + section.Paragraphs[i].BreakCharacterFormat.FontSize + " yazı boyutu -> " + "12" + " olarak değiştirin";
                    }

                }
            }
            errorText += "\n\n**** LÜTFEN BELİRTİLEN HATALARINIZI DÜZELTİNİZ...**** ";
            MessageBox.Show(errorText, "!!! Hata Raporu");

            // MessageBox.Show(fileContent, "File Content at path: " + filePath, MessageBoxButtons.OK);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
