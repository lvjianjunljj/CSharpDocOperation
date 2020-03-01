namespace PDFOperation
{
    using iTextSharp.text.pdf;
    using iTextSharp.text.pdf.parser;
    using System;

    public class PdfFileRead
    {
        public string GetPdfContent(string filePath)
        {
            try
            {
                string pdffilename = filePath;
                PdfReader pdfReader = new PdfReader(pdffilename);
                int numberOfPages = pdfReader.NumberOfPages;
                string text = string.Empty;

                for (int i = 1; i <= numberOfPages; ++i)
                {
                    ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                    text += PdfTextExtractor.GetTextFromPage(pdfReader, i, strategy);
                }
                pdfReader.Close();

                return text;
            }
            catch (Exception ex)
            {
                //StreamWriter wlog = File.AppendText(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\mylog.log");
                //wlog.WriteLine("出错文件：" + "原因：" + ex.ToString());
                //wlog.Flush();
                //wlog.Close();
                return null;
            }
        }
    }
}
