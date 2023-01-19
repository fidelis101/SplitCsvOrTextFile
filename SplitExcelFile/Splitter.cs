using System.Text;

namespace Splitfiles
{
    public partial class Form
    {
        public FileStream? fs;

        List<string> Packets = new List<string>();


        int counter = 0;
        string? line;

        public bool SplitFileByLines(string SourceFile, int nNoofFiles, bool hasHeader = false, bool addHeaderToEachFile = false)
        {
            bool Split = false;
            List<string> fileContents = new List<string>();
            try
            {

                System.IO.StreamReader file = new System.IO.StreamReader(SourceFile);
                FileInfo fs = new FileInfo(SourceFile);

                int SizeofEachFile = (int)Math.Ceiling((double)fs.Length / nNoofFiles);

                StringBuilder sb = new StringBuilder();

                string baseFileName = Path.GetFileNameWithoutExtension(SourceFile);
                string Extension = Path.GetExtension(SourceFile);

                string? header = null;
                bool headerAppended = false;

                
                while ((line = file.ReadLine()) != null)
                {
                    if (hasHeader && addHeaderToEachFile)
                    {
                        if (string.IsNullOrEmpty(header))
                        {
                            header = line;
                        }

                        if (!headerAppended && counter > 0)
                        {
                            sb.AppendLine(header);
                            headerAppended = true;
                        }
                    }

                    if (SizeofEachFile > sb.Length)
                    {
                        sb.AppendLine(line);
                    }
                    else
                    {
                        sb.AppendLine(line);
                        fileContents.Add(sb.ToString());
                        sb.Clear();
                        headerAppended = false;
                        counter++;
                    }
                }
                fileContents.Add(sb.ToString());
                sb.Clear();

                int i = 1;
                foreach (var content in fileContents)
                {
                    FileStream outputFile = new FileStream(Path.GetDirectoryName(SourceFile) + "\\" + "Splitted_" + baseFileName + "." +
                    i.ToString().PadLeft(5, Convert.ToChar("0")) + Extension, FileMode.Create, FileAccess.Write);
                    
                    byte[] bytes = Encoding.ASCII.GetBytes(content);
                    outputFile.Write(bytes, 0, content.Length);
                    string packet = "Splitted_"+baseFileName + "." + i.ToString().PadLeft(3, Convert.ToChar("0")) + Extension.ToString();
                    Packets.Add(packet);

                    i++;
                    outputFile.Close();
                }

            }
            catch (Exception Ex)
            {
                throw new ArgumentException(Ex.Message);
            }
            return Split;
        }
    }
}