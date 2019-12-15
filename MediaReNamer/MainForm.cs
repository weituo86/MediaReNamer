using ExifToolWrap;
using System;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace MediaReNamer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            startReNamer(true);

            MessageBox.Show("媒体处理完成");
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            startReNamer(false);

            MessageBox.Show("媒体处理完成");
        }

        private void startReNamer(bool moveFile)
        {
            //枚举所有文件
            DirectoryInfo diTop = new DirectoryInfo(srcText.Text);

            try
            {
                foreach (var fi in diTop.EnumerateFiles("*.*"))
                {
                    try
                    {
                        ReNameFile(fi, moveFile);
                    }
                    catch (UnauthorizedAccessException unAuthTop)
                    {
                        Console.WriteLine($"{unAuthTop.Message}");
                    }
                }

                foreach (var di in diTop.EnumerateDirectories("*"))
                {
                    try
                    {
                        foreach (var fi in di.EnumerateFiles("*.*", SearchOption.AllDirectories))
                        {
                            try
                            {
                                ReNameFile(fi, moveFile);
                            }
                            catch (UnauthorizedAccessException unAuthFile)
                            {
                                Console.WriteLine($"unAuthFile: {unAuthFile.Message}");
                            }
                        }
                    }
                    catch (UnauthorizedAccessException unAuthSubDir)
                    {
                        Console.WriteLine($"unAuthSubDir: {unAuthSubDir.Message}");
                    }
                }
            }
            catch (DirectoryNotFoundException dirNotFound)
            {
                Console.WriteLine($"{dirNotFound.Message}");
            }
            catch (UnauthorizedAccessException unAuthDir)
            {
                Console.WriteLine($"unAuthDir: {unAuthDir.Message}");
            }
            catch (PathTooLongException longPath)
            {
                Console.WriteLine($"{longPath.Message}");
            }
        }

        private void ReNameFile(FileInfo srcFileInfo, bool moveFile = true)
        {
            string file = srcFileInfo.FullName;

            ExifToolWrapper exif = new ExifToolWrapper();

            exif.Run(file);

            string cameraModel = string.Empty;

            string cameraDateTime = string.Empty;

            ExifTagItem model = exif.Find("Camera Model Name");

            if (string.IsNullOrEmpty(model.value))
            {
                model = exif.Find("Model");
            }

            cameraModel = model.value;

            ExifTagItem creationDate = exif.Find("Date/Time Original");

            if (string.IsNullOrEmpty(creationDate.value))
            {
                creationDate = exif.Find("Creation Date");

                if (string.IsNullOrEmpty(creationDate.value))
                {
                    creationDate = exif.Find("Create Date");

                    if (string.IsNullOrEmpty(creationDate.value))
                    {
                        creationDate = exif.Find("Date Created");
                    }
                }
            }

            cameraDateTime = creationDate.value;

            if (string.IsNullOrEmpty(cameraModel))
            {
                cameraModel = "未知";
            }

            if (string.IsNullOrEmpty(cameraDateTime))
            {
                return;
            }

            if (cameraDateTime.Contains("+08:00"))
            {
                cameraDateTime = cameraDateTime.Replace("+08:00", "");
            }

            //文件名形式 照相机型号\年\月\日\年月日_时分秒_00

            IFormatProvider ifp = new CultureInfo("zh-CN", true);

            DateTime dateTime = DateTime.ParseExact(cameraDateTime, "yyyy:MM:dd HH:mm:ss", ifp);

            string fileName = $"{cameraModel}\\{dateTime.Year}\\{dateTime.Month.ToString().PadLeft(2, '0')}\\{dateTime.Day.ToString().PadLeft(2, '0')}\\{dateTime.Year}{dateTime.Month.ToString().PadLeft(2, '0')}{dateTime.Day.ToString().PadLeft(2, '0')}_{dateTime.Hour.ToString().PadLeft(2, '0')}{dateTime.Minute.ToString().PadLeft(2, '0')}{dateTime.Second.ToString().PadLeft(2, '0')}_00";

            string dstFile = Path.Combine(dstText.Text, fileName + srcFileInfo.Extension);

            FileInfo dstFileInfo = new FileInfo(dstFile);

            if (!dstFileInfo.Directory.Exists)
            {
                dstFileInfo.Directory.Create();
            }

            if (!dstFileInfo.Exists)
            {
                if (moveFile)
                {
                    File.Move(srcFileInfo.FullName, dstFileInfo.FullName);
                }
                else
                {
                    File.Copy(srcFileInfo.FullName, dstFileInfo.FullName);
                }
            }
            else
            {
                //比较md5
                string srcMd5 = md5File(srcFileInfo.FullName);

                string dstMd5 = md5File(dstFileInfo.FullName);

                if (srcMd5 == dstMd5)
                {
                    if (moveFile)
                    {
                        srcFileInfo.Delete();
                    }

                    return;
                }

                //文件添加后缀
                int num = 0;

                while (true)
                {
                    num++;

                    dstFile = Path.Combine(dstText.Text, fileName + num.ToString().PadLeft(2, '0') + srcFileInfo.Extension);

                    dstFileInfo = new FileInfo(dstFile);

                    if (!dstFileInfo.Exists)
                    {
                        if (moveFile)
                        {
                            File.Move(srcFileInfo.FullName, dstFileInfo.FullName);
                        }
                        else
                        {
                            File.Copy(srcFileInfo.FullName, dstFileInfo.FullName);
                        }

                        return;
                    }
                    else
                    {
                        dstMd5 = md5File(dstFileInfo.FullName);

                        if (srcMd5 == dstMd5)
                        {
                            if (moveFile)
                            {
                                srcFileInfo.Delete();
                            }

                            return;
                        }
                    }
                }
            }
        }

        public static string md5File(string filename)
        {
            if (!File.Exists(filename))
            {
                return string.Empty;
            }

            FileStream inputStream = new FileStream(filename, FileMode.Open, FileAccess.Read);

            byte[] buffer = MD5.Create().ComputeHash(inputStream);

            inputStream.Close();

            StringBuilder builder = new StringBuilder(32);

            for (int i = 0; i < buffer.Length; i++)
            {
                builder.Append(buffer[i].ToString("X2"));
            }

            return builder.ToString();
        }

        private void Text_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Link;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void srcText_DragDrop(object sender, DragEventArgs e)
        {
            string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();

            srcText.Text = path;
        }

        private void dstText_DragDrop(object sender, DragEventArgs e)
        {
            string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();

            dstText.Text = path;
        }
    }
}
