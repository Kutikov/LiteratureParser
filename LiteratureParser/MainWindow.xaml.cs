using System;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using Syncfusion.Windows.Controls.RichTextBoxAdv;

namespace LiteratureParser
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<LiteratureSourceUI> literatureSourceUIs;
        public MainWindow()
        {
            InitializeComponent();
        }

        public List<string> GetAllShadows(LiteratureSourceUI literatureSourceUI)
        {
            List<string> ret = new List<string>
            {
                literatureSourceUI.literatureSource.shadowText
            };
            for (int i = 0; i < literatureSourceUI.literatureSource.linkedIds.Count; i++)
            {
                ret.Add(literatureSourceUIs.FirstOrDefault(x => x.literatureSource.id == literatureSourceUI.literatureSource.linkedIds[i]).literatureSource.shadowText);
            }
            return ret;
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "All supported files (*.docx,*.doc,*.htm,*.html,*.rtf,*.txt,*.xaml)|*.docx;*.doc;*.htm;*.html;*.rtf;*.txt;*.xaml|Word Document (*.docx)|*.docx|Word 97 - 2003 Document (*.doc)|*.doc|Web Page (*.htm,*.html)|*.htm;*.html|Rich Text File (*.rtf)|*.rtf|Text File (*.txt)|*.txt|Xaml File (*.xaml)|*.xaml",
                FilterIndex = 1,
                Multiselect = false
            };

            if ((bool)openFileDialog.ShowDialog())
            {
                // Loads the file into RichTextBoxAdv.
                richTextBoxAdv.Load(openFileDialog.FileName);
                // Sets the File name as Document Title.
                richTextBoxAdv.DocumentTitle = openFileDialog.FileName.Remove(openFileDialog.FileName.LastIndexOf("."));
            }
        }

        private void Scan_Click(object sender, RoutedEventArgs e)
        {
            literatureSourceUIs = new List<LiteratureSourceUI>();
            SourcesList.Children.Clear();
            TextSearchResults textSearchResults = richTextBoxAdv.FindAll(new Regex(@"\[.*?\]"), FindOptions.None);
            if(textSearchResults != null)
            {
                int index = 1;
                for(int i = 0; i < textSearchResults.Count; i++)
                {
                    TextSearchResult textSearchResult = textSearchResults[i];
                    string[] sublinks = textSearchResult.Text.Substring(1, textSearchResult.Text.Length - 1).Split('#');
                    for(int j = 0; j < sublinks.Length; j++)
                    {
                        string text = sublinks[j].Trim();
                        try
                        {
                            literatureSourceUIs.First(x => x.literatureSource.shadowText == text);
                        }
                        catch
                        {
                            LiteratureSourceUI literatureSourceUI = new LiteratureSourceUI(new LiteratureSource(index, text), this);
                            literatureSourceUIs.Add(literatureSourceUI);
                            index++;
                            SourcesList.Children.Add(literatureSourceUI);
                        }
                    }                    
                }
            }
        }
    }
}
