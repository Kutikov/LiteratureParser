using Syncfusion.Windows.Controls.RichTextBoxAdv;
using System;
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

namespace LiteratureParser
{
    /// <summary>
    /// Логика взаимодействия для LiteratureSourceUI.xaml
    /// </summary>
    public partial class LiteratureSourceUI : UserControl
    {
        public LiteratureSource literatureSource;
        MainWindow mainWindow;
        public LiteratureSourceUI(LiteratureSource literatureSource, MainWindow mainWindow)
        {
            InitializeComponent();
            this.literatureSource = literatureSource;
            this.mainWindow = mainWindow;
            FrontText.Text = literatureSource.frontText;
            ShadowText.Text = literatureSource.shadowText;
            LinkNumber.Text = literatureSource.numberInList.ToString();
        }

        private void ShowAll_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.richTextBoxAdv.Selection.CharacterFormat.HighlightColor = HighlightColor.NoColor;
            List<string> allShadows = mainWindow.GetAllShadows(this);
            foreach(string shadow in allShadows)
            {
                TextSearchResults textSearchResults = mainWindow.richTextBoxAdv.FindAll(shadow, FindOptions.None);
                if (textSearchResults != null)
                {
                    for (int j = 0; j < textSearchResults.Count; j++)
                    {
                        TextSearchResult textSearchResult = textSearchResults[j];
                        mainWindow.richTextBoxAdv.Selection.SelectionRanges.Add(textSearchResult.Start, textSearchResult.End);
                    }
                    mainWindow.richTextBoxAdv.Selection.CharacterFormat.HighlightColor = HighlightColor.Gray25;
                }
            }            
        }

        private void RelatedList_DropDownOpened(object sender, EventArgs e)
        {

        }

        private void RelatedList_DropDownClosed(object sender, EventArgs e)
        {

        }
    }
}
