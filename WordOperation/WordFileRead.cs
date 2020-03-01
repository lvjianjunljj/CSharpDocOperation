namespace WordOperation
{
    using Microsoft.Office.Interop.Word;
    using System;
    using System.Text;

    public class WordFileRead
    {
        public string GetWordContent(string filePath)
        {
            ////加载Word文档
            //Document doc = new Document();
            //Document.LoadFromFile(@"测试文档.docx");

            ////使用GetText方法获取文档中的所有文本
            //string s = doc.GetText();

            //File.WriteAllText("文本1.txt", s.ToString());
            try
            {
                Application app = new Application();
                Type wordType = app.GetType();
                Document doc = null;
                object unknow = Type.Missing;
                app.Visible = false;

                object file = filePath;
                doc = app.Documents.Open(ref file,
                ref unknow, ref unknow, ref unknow, ref unknow,
                ref unknow, ref unknow, ref unknow, ref unknow,
                ref unknow, ref unknow, ref unknow, ref unknow,
                ref unknow, ref unknow, ref unknow);
                int count = doc.Paragraphs.Count;
                StringBuilder sb = new StringBuilder();
                for (int i = 1; i <= count; i++)
                {
                    sb.Append(doc.Paragraphs[i].Range.Text.Trim());
                }

                doc.Close(ref unknow, ref unknow, ref unknow);
                //wordType.InvokeMember("Quit", System.Reflection.BindingFlags.InvokeMethod, null, app, null);
                doc = null;
                app = null;
                //垃圾回收
                GC.Collect();
                GC.WaitForPendingFinalizers();

                return sb.ToString();
            }
            catch
            {
                return "";
            }
        }
    }
}
