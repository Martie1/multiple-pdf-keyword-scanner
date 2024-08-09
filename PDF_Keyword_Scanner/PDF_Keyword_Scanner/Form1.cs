
using System.Collections.Immutable;
using System.Runtime;
using System.Runtime.InteropServices;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.Graphics.Operations.PathPainting;

namespace CV_Scanner
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        //clicking on the label as well as the browse button will do the same thing
        private void label1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            DialogResult result = folder.ShowDialog();
            if (result == DialogResult.OK)
            {
                textDirectory.Text = folder.SelectedPath;
            }
        }
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            DialogResult result = folder.ShowDialog();
            if (result == DialogResult.OK)
            {
                textDirectory.Text = folder.SelectedPath;
            }
        }
        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                string path = textDirectory.Text; //selected dir for scanning
                string[] keywords = textKeywords.Text.Split(',').Select(k => k.Replace(" ", "")).Distinct().ToArray();
                Array.Sort(keywords);
                //creating new folder inside of path
                bool all = checkBox.Checked;
                //gets the value of checkbox
                string dirName = "";
                if (all) //all keywords(and) true, any keywords(or) false, 
                {
                    dirName = "PDF_allOf_" + string.Join("_", keywords);
                }
                else
                {
                    dirName = "PDF_anyOf_" + string.Join("_", keywords);
                }
                string newDirPath = Path.Combine(path, dirName);



                var pdfFiles = Directory.GetFiles(path, "*.pdf"); //could implement handling other formats later


                var count = 0;
                string newPath = "";
                //	var fun = any ? ContainsAnyKeywords : ContainsAllKeywords;
                if (!all)
                {
                    newPath = Path.Combine("ANY", newDirPath);
                    if (!Directory.Exists(newPath))
                    {
                        Directory.CreateDirectory(newPath);
                    }

                    foreach (var pdfFile in pdfFiles)
                    {
                        if (ContainsAnyKeywords(pdfFile, keywords))
                        {
                            count++;
                            string fileName = Path.GetFileName(pdfFile);
                            string destFilePath = Path.Combine(newDirPath, fileName);
                            File.Copy(pdfFile, destFilePath, true);
                        }
                    }
                }
                else if (all)
                {
                    newPath = Path.Combine("ALL", newDirPath);
                    if (!Directory.Exists(newPath))
                    {
                        Directory.CreateDirectory(newPath);
                    }
                    foreach (var pdfFile in pdfFiles)
                    {
                        if (ContainsAllKeywords(pdfFile, keywords))
                        {
                            count++;
                            string fileName = Path.GetFileName(pdfFile);
                            string destFilePath = Path.Combine(newDirPath, fileName);
                            File.Copy(pdfFile, destFilePath, true);
                        }
                    }
                   
                }
               // string finalMessage = "Scanning complete : "+count.ToString()+" matches found";
                string finalMessage = $"Scanning complete : {count} matches found"; //fancy way

                if (!Directory.EnumerateFileSystemEntries(newPath).Any()) //if the dir is empty (no matches found)
                {
                    Directory.Delete(newPath);
                }
                ShowCompletionDialog(newDirPath, count,finalMessage);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occured: {ex.Message}");
            }
        }

        //have to include if the keywords dont match anything
        private void ShowCompletionDialog(string folderPath, int count,string finalMessage)
        {
            Form dialog = new Form
            {
                Width = 300,
                Height = 150,
                Text = "Scanning Complete"
            };

            Label label = new Label
            {
                Left = 50,
                Top = 20,
                Width = 200,
                Text = finalMessage
            };
            Button goToFolderButton = new Button
            {
                Text = "Go to folder",
                Left = 50,
                Width = 200,
                Top = 50,
                DialogResult = DialogResult.OK
            };
            if (count > 0) { 
                
                goToFolderButton.Click += (sender, e) =>
                {
                    System.Diagnostics.Process.Start("explorer.exe", folderPath);
                };
            }
            else
            {
                goToFolderButton.Text = "Back";
                goToFolderButton.Click += (sender, e) =>
                {
                    dialog.Close(); //close the pop up window
                };
            }
            dialog.Controls.Add(label);
            dialog.Controls.Add(goToFolderButton);
            dialog.AcceptButton = goToFolderButton;

            dialog.ShowDialog();
        }
        private bool ContainsAnyKeywords(string pdfFile, string[] keywords)
        {
            using (PdfDocument document = PdfDocument.Open(pdfFile))
            {
                foreach (Page page in document.GetPages()) //Page from PdfPig.Content
                {
                    var text = page.Text;
                    using (StringReader reader = new StringReader(text))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            foreach (var keyword in keywords)
                            {
                                if (line.Contains(keyword.Trim(), StringComparison.OrdinalIgnoreCase))// letter case doesnt matter
                                {
                                    Console.WriteLine("znalazlem!");
                                    return true;
                                }

                            }
                        }
                    }
                }
            }
            return false;
        }
        private bool ContainsAllKeywords(string pdfFile, string[] keywords)
        {
            //map of wether the keywords was found
            var keywordMap = keywords.ToDictionary(keyword => keyword, keyword => false);

            using (PdfDocument document = PdfDocument.Open(pdfFile))
            {
                foreach (Page page in document.GetPages()) //Page from PdfPig.Content
                {
                    var text = page.Text;
                    using (StringReader reader = new StringReader(text))
                    {
                        string line;
                        var currentKeywords = keywordMap.Where(keyword => !keyword.Value).Select(keyword => keyword.Key).ToArray(); //or toList and then Remove
                        while ((line = reader.ReadLine()) != null)
                        {

                            foreach (var keyword in currentKeywords)
                            {
                                //if the keyword has already been found then dont search for it
                                if (line.Contains(keyword.Trim(), StringComparison.OrdinalIgnoreCase))// letter case doesnt matter
                                {
                                    keywordMap[keyword] = true;
                                    Console.WriteLine("znalazlem!");
                                    //updating currentKeywords when there is change.
                                    currentKeywords = keywordMap.Where(keyword => !keyword.Value).Select(keyword => keyword.Key).ToArray();
                                }

                            }

                            if (!currentKeywords.Any()) //if there are none left to check then all have been found
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return keywordMap.Values.All(found => found);//checks if all are true
        }

        private void label1_Click_2(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }



        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
