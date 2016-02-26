using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Net;

namespace DDD.Utility
{
    public class FileHelper
    {
        public FileHelper()
        {
        }
        public static long GetFileSize(string sPath)
        {
            long i = 0;
            if (File.Exists(sPath))
            {

                try
                {
                    FileInfo myf = new FileInfo(sPath);
                    i = myf.Length;
                }
                catch
                {
                    i = 0;
                }
            }
            return i;
        }

        public static bool DelFile(string f)
        {
            bool re = false;
            if (!string.IsNullOrEmpty(f))
            {
                string furl = HttpContext.Current.Server.MapPath(f);
                if (File.Exists(furl))
                {
                    try
                    {
                        File.Delete(furl);
                        re = true;
                    }
                    catch
                    {
                        re = false;
                    }
                }
            }
            return re;
        }

        /// <summary>
        /// ɾ��ͼƬ�ļ���ͬʱɾ����΢ͼ
        /// </summary>
        /// <param name="f">���·���ļ���</param>
        /// <param name="tnPreName">��΢ͼ�ļ���ǰ׺</param>
        public static void DelPicFileAndTN(string f,string tnPreName)
        {
            if (!string.IsNullOrEmpty(f))
            {
                string furl = HttpContext.Current.Server.MapPath(f);
                string pictn = f.Insert(f.LastIndexOf("/") + 1, tnPreName);
                try
                {
                    File.Delete(furl);
                    File.Delete(HttpContext.Current.Server.MapPath(pictn));
                }
                catch
                {
                }
            } 
        }

        public static void CopyFiles(string varFromDirectory, string varToDirectory)
        {
            Directory.CreateDirectory(varToDirectory);

            if (!Directory.Exists(varFromDirectory)) return;

            string[] directories = Directory.GetDirectories(varFromDirectory);

            if (directories.Length > 0)
            {
                foreach (string d in directories)
                {
                    CopyFiles(d, varToDirectory + d.Substring(d.LastIndexOf("\\")));
                }
            }


            string[] files = Directory.GetFiles(varFromDirectory);

            if (files.Length > 0)
            {
                foreach (string s in files)
                {
                    File.Copy(s, varToDirectory + s.Substring(s.LastIndexOf("\\")), true);
                }
            }
        }
        //ɾ��ָ���ļ��ж�Ӧ�����ļ�������ļ�
        public static void DeleteFiles(string varFromDirectory, string varToDirectory)
        {
            Directory.CreateDirectory(varToDirectory);

            if (!Directory.Exists(varFromDirectory)) return;

            string[] directories = Directory.GetDirectories(varFromDirectory);

            if (directories.Length > 0)
            {
                foreach (string d in directories)
                {
                    DeleteFiles(d, varToDirectory + d.Substring(d.LastIndexOf("\\")));
                }
            }


            string[] files = Directory.GetFiles(varFromDirectory);

            if (files.Length > 0)
            {
                foreach (string s in files)
                {
                    File.Delete(varToDirectory + s.Substring(s.LastIndexOf("\\")));
                }
            }
        }

        /// <summary>
        /// ����ϴ����ļ��Ƿ���Ч�� true����Ч ; false����Ч
        /// </summary>
        /// <param name="FileUpload1"></param>
        /// <param name="mySize">��λ��KB</param>
        /// <param name="myFileExten">������ļ���׺����:{ ".gif", ".png", ".jpg", ".jpeg", ".bmp" }</param>
        /// <param name="UpTitle">�ϴ����ļ����ͣ�ͼƬ��������...</param>
        /// <returns></returns>
        public static bool ChkFileError(FileUpload FileUpload1, int mySize, string[] myFileExten, string UpTitle)
        {
            bool errorflag = false;

            if (FileUpload1.PostedFile == null)
            {
                HttpContext.Current.Response.Write("<script>alert(\"�ϴ�" + UpTitle + "ʧ�ܣ�����ѡ��һ���ļ�\");</script>");
                errorflag = true;
            }
            else
            {
                if (FileUpload1.PostedFile.FileName.Length < 3)
                {
                    HttpContext.Current.Response.Write("<script>alert(\"�ϴ�" + UpTitle + "ʧ�ܣ�����ѡ��һ���ļ�\");</script>");
                    errorflag = true;
                }
                else
                {
                    bool fileok = false;
                    string fileExtension = Path.GetExtension(FileUpload1.FileName).ToLower();
                    string[] allowedExtensions = myFileExten;
                    for (int i = 0; i < allowedExtensions.Length; i++)
                    {
                        if (fileExtension == allowedExtensions[i])
                        {
                            fileok = true;
                            break;
                        }

                    }
                    if (!fileok)
                    {
                        string j = "";
                        for (int i = 0; i < allowedExtensions.Length; i++)
                        {
                            j += allowedExtensions[i] + "/";
                        }
                        HttpContext.Current.Response.Write("<script>alert(\"�ϴ�" + UpTitle + "ʧ�ܣ�ֻ����" + j + "���ļ���\");</script>");
                        errorflag = true;
                    }
                    else
                    {
                        if (FileUpload1.PostedFile.ContentLength <= 0)
                        {
                            HttpContext.Current.Response.Write("<script>alert(\"�ϴ�" + UpTitle + "ʧ�ܣ�����ѡ��һ���ļ�\");</script>");
                            errorflag = true;
                        }
                        else
                        {
                            if (FileUpload1.PostedFile.ContentLength > mySize * 1024)
                            {
                                decimal mySizeMsg = (decimal)mySize / 1024;
                                HttpContext.Current.Response.Write("<script>alert(\"�ϴ�" + UpTitle + "ʧ�ܣ��ϴ�" + UpTitle + "��С�޶���" + mySizeMsg.ToString("0.0") + "M����\");</script>");
                                errorflag = true;
                            }

                        }
                    }
                }
            }
            return errorflag;
        }

        /// <summary>
        /// ����ϴ����ļ��Ƿ���Ч(JSReg-�����ƻ�css��ʽ)�� true����Ч ; false����Ч
        /// </summary>
        /// <param name="js"></param>
        /// <param name="FileUpload1"></param>
        /// <param name="mySize">��λ��KB</param>
        /// <param name="myFileExten">������ļ���׺����:{ ".gif", ".png", ".jpg", ".jpeg", ".bmp" }</param>
        /// <param name="UpTitle">�ϴ����ļ����ͣ�ͼƬ��������...</param>
        /// <returns></returns>
        public static bool ChkFileError(JScriptReg js, FileUpload FileUpload1, int mySize, string[] myFileExten, string UpTitle)
        {
            bool errorflag = false;

            if (FileUpload1.PostedFile == null)
            {
                js.Alert("�ϴ�" + UpTitle + "ʧ�ܣ�����ѡ��һ���ļ�");
                errorflag = true;
            }
            else
            {
                if (FileUpload1.PostedFile.FileName.Length < 3)
                {
                    js.Alert("�ϴ�" + UpTitle + "ʧ�ܣ�����ѡ��һ���ļ�");
                    errorflag = true;
                }
                else
                {
                    bool fileok = false;
                    string fileExtension = Path.GetExtension(FileUpload1.FileName).ToLower();
                    string[] allowedExtensions = myFileExten;
                    for (int i = 0; i < allowedExtensions.Length; i++)
                    {
                        if (fileExtension == allowedExtensions[i])
                        {
                            fileok = true;
                            break;
                        }

                    }
                    if (!fileok)
                    {
                        string j = "";
                        for (int i = 0; i < allowedExtensions.Length; i++)
                        {
                            j += allowedExtensions[i] + "/";
                        }
                        js.Alert("�ϴ�" + UpTitle + "ʧ�ܣ�ֻ����" + j + "���ļ���");
                        errorflag = true;
                    }
                    else
                    {
                        if (FileUpload1.PostedFile.ContentLength <= 0)
                        {
                            js.Alert("�ϴ�" + UpTitle + "ʧ�ܣ�����ѡ��һ���ļ�");
                            errorflag = true;
                        }
                        else
                        {
                            if (FileUpload1.PostedFile.ContentLength > mySize * 1024)
                            {
                                decimal mySizeMsg = (decimal)mySize / 1024;
                                js.Alert("�ϴ�" + UpTitle + "ʧ�ܣ��ϴ�" + UpTitle + "��С�޶���" + mySizeMsg.ToString("0.0") + "M����");
                                errorflag = true;
                            }

                        }
                    }
                }
            }
            return errorflag;
        }

        /// <summary>
        /// �ϴ��ļ���ѡ�����ɸ߻�����΢ͼ
        /// </summary>
        /// <param name="FileUpload1"></param>
        /// <param name="PicPath">����ļ����������ݿ��·��</param>
        /// <param name="dctpath">�����ļ���</param>
        /// <param name="CreatTNImg">�Ƿ�����"tn"��΢ͼ</param>
        /// <param name="TNImg_W">��΢ͼ��</param>
        /// <param name="TNImg_H">��΢ͼ��</param>
        /// <returns></returns>
        public static bool UploadFile(FileUpload FileUpload1, out string PicPath, string dctpath, bool CreatTNImg, int TNImg_W, int TNImg_H)
        {
            return UploadFile(FileUpload1, out PicPath, dctpath, CreatTNImg, TNImg_W, TNImg_H, "Cut", InterpolationMode.High, SmoothingMode.HighQuality);
        }

        /// <summary>
        /// �ϴ��ļ�
        /// </summary>
        /// <param name="FileUpload1"></param>
        /// <param name="PicPath">����ļ����������ݿ��·��</param>
        /// <param name="dctpath">�����ļ���</param>
        /// <param name="CreatTNImg">�Ƿ�����"tn"��΢ͼ</param>
        /// <param name="TNImg_W">��΢ͼ��</param>
        /// <param name="TNImg_H">��΢ͼ��</param>
        /// <param name="CutMode">��������ͼ�ķ�ʽ:
        /// HW ָ���߿����ţ����ܱ��Σ� 
        /// W ָ�����߰�����
        /// H ָ���ߣ�������
        /// Cut ָ���߿�ü��������Σ� </param>    
        /// <param name="interpolMode">��ֵ�㷨</param>
        /// <param name="smoothingMode">ƽ������</param>
        /// <returns></returns>
        public static bool UploadFile(FileUpload FileUpload1, out string PicPath, string dctpath, bool CreatTNImg, int TNImg_W, int TNImg_H, string CutMode, InterpolationMode interpolMode, SmoothingMode smoothingMode)
        {
            string datefile = DateTime.Now.ToString("yyyy-MM");
            string path = HttpContext.Current.Server.MapPath(dctpath + datefile);
            //string path =dctpath+datefile;// zyl�޸Ĺ�
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filename = DateTime.Now.ToString("yyyyMMddHHmmss");
            string allfilename = "";
            string extfilename = "";
            Random r = new Random();
            filename = filename + r.Next(1000);
            extfilename = FileUpload1.PostedFile.FileName.Substring(FileUpload1.PostedFile.FileName.LastIndexOf(".") + 1);
            allfilename = filename + "." + extfilename;
            PicPath = dctpath + datefile + "/" + allfilename;//���ݿⱣ��·��
            string PicUrl = path + "/" + allfilename;//�������·��
            string tnPicUrl = path + "/" + "tn" + filename + "." + extfilename;

            bool addflag = true;
            try
            {
                FileUpload1.PostedFile.SaveAs(PicUrl);
                if (CreatTNImg)
                    ImageHelper.MakeThumbnail(PicUrl, tnPicUrl, TNImg_W, TNImg_H, CutMode, interpolMode, smoothingMode);
            }
            catch
            {
                addflag = false;
            }
            return addflag;
        }

        public static bool UrlFileExists(string URL,out string FileContentType,int myTimeOut)
        {
            HttpWebRequest req = null;
            HttpWebResponse res = null;
            FileContentType = string.Empty;
            try
            {
                req = (HttpWebRequest)WebRequest.Create(URL);
                req.Method = "HEAD";
                req.Timeout = myTimeOut;
                res = (HttpWebResponse)req.GetResponse();
                FileContentType = res.ContentType;
                return (res.StatusCode == HttpStatusCode.OK);
            }
            catch
            {
                return false;
            }
            finally
            {
                if (res != null)
                {
                    res.Close();
                    res = null;
                }
                if (req != null)
                {
                    req.Abort();
                    req = null;
                }
            }

        }

        /// <summary>
        /// //ʹ��OutputStream.Write�ֿ������ļ�  
        /// </summary>
        /// <param name="myFilePath">�ļ���Ե�ַ</param>
        /// <param name="myNewFileName">�ļ����������գ�Ĭ������</param>
        /// <param name="chunkSize">ָ�����С��-1��102400��100k��</param>
        public static bool DownFile(string myFilePath,string myNewFileName,long chunkSize)
        {
            bool re = false;

            myFilePath = HttpContext.Current.Server.MapPath(myFilePath);
            if (File.Exists(myFilePath))
            {
                FileInfo fileInfo = new FileInfo(myFilePath);
                if (fileInfo.Length == 0) return re;
                else
                {
                    if (myNewFileName == "")
                        myNewFileName = //HttpUtility.UrlPathEncode(myFilePath.Substring(myFilePath.LastIndexOf("/") + 1));
                            HttpUtility.UrlPathEncode(fileInfo.Name);
                    else myNewFileName = HttpUtility.UrlPathEncode(myNewFileName) + fileInfo.Extension;
                }
            }
            else return re;

            //ָ�����С   
            if (chunkSize == -1)
                chunkSize = 102400;
            //����һ��100K�Ļ�����   
            byte[] buffer = new byte[chunkSize];
            //�Ѷ����ֽ���   
            long dataToRead = 0;
            FileStream stream = null;
            try
            {
                //���ļ�   
                stream = new FileStream(myFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                dataToRead = stream.Length;

                //���Httpͷ   
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachement;filename=" + myNewFileName);
                HttpContext.Current.Response.AddHeader("Content-Length", dataToRead.ToString());

                while (dataToRead > 0)
                {
                    if (HttpContext.Current.Response.IsClientConnected)
                    {
                        int length = stream.Read(buffer, 0, Convert.ToInt32(chunkSize));
                        HttpContext.Current.Response.OutputStream.Write(buffer, 0, length);
                        HttpContext.Current.Response.Flush();
                        HttpContext.Current.Response.Clear();
                        dataToRead -= length;
                    }
                    else
                    {
                        //��ֹclientʧȥ����
                        dataToRead = -1;
                    }
                }
                re = true;
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write("Error:" + ex.Message);
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
                HttpContext.Current.Response.Close();
            }
            return re;
        }


        #region �ļ������ļ��������޸�

        /// <summary>
        /// Ϊ�ַ����еķ�Ӣ���ַ�����Encodes non-US-ASCII characters in a string.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        static string ToHexString(string s)
        {
            char[] chars = s.ToCharArray();
            StringBuilder builder = new StringBuilder();
            for (int index = 0; index < chars.Length; index++)
            {
                bool needToEncode = NeedToEncode(chars[index]);
                if (needToEncode)
                {
                    string encodedString = ToHexString(chars[index]);
                    builder.Append(encodedString);
                }
                else
                {
                    builder.Append(chars[index]);
                }
            }
            return builder.ToString();
        }
        /// <summary>
        ///ָ��һ���ַ��Ƿ�Ӧ�ñ����� Determines if the character needs to be encoded.
        /// </summary>
        /// <param name="chr"></param>
        /// <returns></returns>
        private static bool NeedToEncode(char chr)
        {
            string reservedChars = "$-_.+!*'(),@=&";
            if (chr > 127)
                return true;
            if (char.IsLetterOrDigit(chr) || reservedChars.IndexOf(chr) >= 0)
                return false;
            return true;
        }
        /// <summary>
        /// Ϊ��Ӣ���ַ�������Encodes a non-US-ASCII character.
        /// </summary>
        /// <param name="chr"></param>
        /// <returns></returns>
        private static string ToHexString(char chr)
        {
            UTF8Encoding utf8 = new UTF8Encoding();
            byte[] encodedBytes = utf8.GetBytes(chr.ToString());
            StringBuilder builder = new StringBuilder();
            for (int index = 0; index < encodedBytes.Length; index++)
            {
                builder.AppendFormat("%{0}", Convert.ToString(encodedBytes[index], 16));
            }
            return builder.ToString();
        }


        /// <summary>
        /// �ļ������ļ��������޸�
        ///1.���ָ�����ļ���������˿ո�,FireFox�ͻ��ȡ�ո�ǰ�Ĳ�����ΪĬ���ļ�����IE�ͻ��ڿո�λ��ͨ��+���
        ///2.�����ַ����룬׼ȷ���Ƿ� ASCII �ַ����룬��ԭ�ļ����ļ����к��з� ASCII �ַ�ʱ���������ͻ��˻�ȡ�����ļ�������
        ///3.һЩ�����ַ����ܱ������������Ȼ�����Ҳ�������Щ�������ķ��ţ����硰.����IE�¾ͻ��Ϊ��[1].��
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string FormatDownFileName(string fileName)
        {
            string re = string.Empty;
            string encodefileName = ToHexString(fileName);       //ʹ���Զ����
            if (HttpContext.Current.Request.Browser.Browser.Contains("IE"))
            {
                string ext = encodefileName.Substring(encodefileName.LastIndexOf('.'));//�õ���չ��
                string name = encodefileName.Remove(encodefileName.Length - ext.Length);//�õ��ļ�����
                name = name.Replace(".", "%2e"); //�ؼ�����
                re = name + ext;
            }
            else
            {
                re = encodefileName;
            }
            return re;
        }

        #endregion

    }
}
