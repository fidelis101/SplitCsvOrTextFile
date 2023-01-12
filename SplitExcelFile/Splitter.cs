using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;

namespace Splitfiles
{
    public partial class Form
    {
        public FileStream fs;
        string mergeFolder;

        List<string> Packets = new List<string>();
        //Merge file is stored in drive
        string SaveFileFolder = @"c:\";

        int counter = 0;
        string line;

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

                string header = null;
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

        public bool SplitFileByCharacter(string SourceFile, int nNoofFiles)
        {
            bool Split = false;
            try
            {
                FileInfo fileInfo = new FileInfo(SourceFile);
                FileStream fs = new FileStream(SourceFile, FileMode.Open, FileAccess.Read);
                int SizeofEachFile = (int)Math.Ceiling((double)fileInfo.Length / nNoofFiles);
                for (int i = 0; i < nNoofFiles; i++)
                {
                    string baseFileName = Path.GetFileNameWithoutExtension(SourceFile);
                    string Extension = Path.GetExtension(SourceFile);
                    FileStream outputFile = new FileStream(Path.GetDirectoryName(SourceFile) + "\\" + baseFileName + "." +
                        i.ToString().PadLeft(5, Convert.ToChar("0")) + Extension + ".csv", FileMode.Create, FileAccess.Write);
                    mergeFolder = Path.GetDirectoryName(SourceFile);
                    int bytesRead = 0;
                    byte[] buffer = new byte[SizeofEachFile];
                    if ((bytesRead = fs.Read(buffer, 0, SizeofEachFile)) > 0)
                    {
                        outputFile.Write(buffer, 0, bytesRead);
                        //outp.Write(buffer, 0, BytesRead);
                        string packet = baseFileName + "." + i.ToString().PadLeft(3, Convert.ToChar("0")) + Extension.ToString();
                        Packets.Add(packet);
                    }
                    outputFile.Close();
                }
                fs.Close();
            }
            catch (Exception Ex)
            {
                throw new ArgumentException(Ex.Message);
            }
            return Split;
        }

        public bool MergeFile(string inputfoldername1)
        {
            bool Output = false;
            try
            {
                string[] tmpfiles = Directory.GetFiles(inputfoldername1, "*.tmp");
                FileStream outPutFile = null;
                string PrevFileName = "";
                foreach (string tempFile in tmpfiles)
                {
                    string fileName = Path.GetFileNameWithoutExtension(tempFile);
                    string baseFileName = fileName.Substring(0, fileName.IndexOf(Convert.ToChar(".")));
                    string extension = Path.GetExtension(fileName);
                    if (!PrevFileName.Equals(baseFileName))
                    {
                        if (outPutFile != null)
                        {
                            outPutFile.Flush();
                            outPutFile.Close();
                        }
                        outPutFile = new FileStream(SaveFileFolder + "\\" + baseFileName + extension, FileMode.OpenOrCreate, FileAccess.Write);
                    }
                    int bytesRead = 0;
                    byte[] buffer = new byte[1024];
                    FileStream inputTempFile = new FileStream(tempFile, FileMode.OpenOrCreate, FileAccess.Read);
                    while ((bytesRead = inputTempFile.Read(buffer, 0, 1024)) > 0)
                        outPutFile.Write(buffer, 0, bytesRead);
                    inputTempFile.Close();
                    File.Delete(tempFile);
                    PrevFileName = baseFileName;
                }
                outPutFile.Close();
            }
            catch
            {
            }
            return Output;

        }
    }
}