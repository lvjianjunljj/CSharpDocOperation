namespace GatherDoc
{
    using PDFOperation;
    using System.Windows;
    using System.Windows.Input;
    using WordOperation;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PdfFileRead pdfFileRead;
        private WordFileRead wordFileRead;
        private FileOperations fileOperations;
        public MainWindow()
        {
            InitializeComponent();
            this.pdfFileRead = new PdfFileRead();
            this.wordFileRead = new WordFileRead();
            this.fileOperations = new FileOperations();
        }
        //private bool textboxHasText = false;

        private void SearchMouseEnter(object sender, MouseEventArgs e)
        {
            //if (textboxHasText == false)
            //    SearchKeys.Text = "";

            //SearchKeys.Foreground = Brushes.Black;
        }

        private void SearchMouseLeave(object sender, MouseEventArgs e)
        {
            //if (SearchKeys.Text == "")
            //{
            //    SearchKeys.Text = "提示内容";
            //    SearchKeys.Foreground = Brushes.LightGray;
            //    textboxHasText = false;
            //}
            //else
            //    textboxHasText = true;
        }

        private void Gather_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(SourcePath.Text))
            {
                MessageBox.Show("Source path cannot be empty...");
                return;
            }

            if (string.IsNullOrEmpty(TargetPath.Text))
            {
                MessageBox.Show("Target path cannot be empty...");
                return;
            }

            if (string.IsNullOrEmpty(SearchKeys.Text))
            {
                MessageBox.Show("Search keys cannot be empty...");
                return;
            }

            var searchKeys = SearchKeys.Text.Split(new char[] { ';' });

            var filePaths = fileOperations.GetAllFile(SourcePath.Text);
            foreach (var filePath in filePaths)
            {
                var filePathSplit = filePath.Split(new char[] { '\\', '/' });
                string fileName = filePathSplit[filePathSplit.Length - 1];
                string content = null;
                if (filePath.EndsWith("pdf"))
                {
                    content = this.pdfFileRead.GetPdfContent(filePath);
                }
                else if (filePath.EndsWith("doc") || filePath.EndsWith("docx"))
                {
                    content = this.wordFileRead.GetWordContent(filePath);
                }
                else
                {
                    continue;
                }

                bool mapping = true;
                foreach (var searchKey in searchKeys)
                {
                    if (!content.Contains(searchKey))
                    {
                        mapping = false;
                    }
                }

                if (mapping)
                {
                    var targetFileName = System.IO.Path.Combine(TargetPath.Text, fileName);
                    System.IO.File.Copy(filePath, targetFileName);
                }

            }



        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            SourcePath.Text = "";
            TargetPath.Text = "";
            SearchKeys.Text = "";
        }
    }
}
