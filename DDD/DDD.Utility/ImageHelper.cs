using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;
using System.Net;
using System.Web;

namespace DDD.Utility
{
    public class ImageHelper
    {
        public ImageHelper()
        {

        }

        /// <summary>
        /// Ĭ�����ɸ߻�������ͼ
        /// </summary>
        /// <param name="originalImagePath">Դͼ·��������·����</param>
        /// <param name="thumbnailPath">����ͼ·��������·����</param>
        /// <param name="width">����ͼ���</param>
        /// <param name="height">����ͼ�߶�</param>
        /// <param name="mode">��������ͼ�ķ�ʽ:HW W H Cut </param>    
        public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode)
        {
            MakeThumbnail(originalImagePath, thumbnailPath, width, height, mode, InterpolationMode.High, SmoothingMode.HighQuality);
        }

        /// <summary>
        /// ��������ͼ
        /// </summary>
        /// <param name="originalImagePath">Դͼ·��������·����</param>
        /// <param name="thumbnailPath">����ͼ·��������·����</param>
        /// <param name="width">����ͼ���</param>
        /// <param name="height">����ͼ�߶�</param>
        /// <param name="mode">��������ͼ�ķ�ʽ:HW W H Cut </param>    
        /// <param name="interpolMode">��ֵ�㷨</param>
        /// <param name="smoothingMode">ƽ������</param>
        public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode, InterpolationMode interpolMode, SmoothingMode smoothingMode)
        {
            Image originalImage = Image.FromFile(originalImagePath);
            int ow = originalImage.Width;
            int oh = originalImage.Height;
            int towidth = width > ow ? ow : width;
            int toheight = height > oh ? oh : height;

            int x = 0;
            int y = 0;


            switch (mode)
            {
                case "HW"://ָ���߿����ţ����ܱ��Σ�                
                    break;
                case "W"://ָ�����߰�����                    
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H"://ָ���ߣ�������
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "Cut"://ָ���߿�ü��������Σ�                
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                default:
                    break;
            }

            //�½�һ��bmpͼƬ
            Image bitmap = new System.Drawing.Bitmap(towidth, toheight, PixelFormat.Format32bppPArgb);
            Image bitmap2 = null;

            //�½�һ������
            Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            //���ø�������ֵ��
            g.InterpolationMode = interpolMode;

            //���ø�����,���ٶȳ���ƽ���̶�
            g.SmoothingMode = smoothingMode;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            //��ջ���
            g.Clear(Color.White);

            //��ָ��λ�ò��Ұ�ָ����С����ԭͼƬ��ָ������
            g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight), x, y, ow, oh, GraphicsUnit.Pixel);

            try
            {
                //��jpg��ʽ��������ͼ
                //bitmap.Save(thumbnailPath);ֱ�ӱ���̫߯��
                EncoderParameters parameters = new EncoderParameters(1);
                parameters.Param[0] = new EncoderParameter(Encoder.Quality, ((long)80));

                bitmap2 = new Bitmap(bitmap);
                bitmap.Dispose();
                g.Dispose();

                bitmap2.Save(thumbnailPath, ImageHelper.GetCodecInfo("image/" + ImageHelper.GetFormat(thumbnailPath).ToString().ToLower()), parameters);
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                if (bitmap2 != null)
                {
                    bitmap2.Dispose();
                }
                g.Dispose();
            }
        }

        /// <summary>
        /// �õ�ͼƬ��ʽ
        /// </summary>
        /// <param name="name">�ļ�����</param>
        /// <returns></returns>
        public static ImageFormat GetFormat(string name)
        {
            string ext = name.Substring(name.LastIndexOf(".") + 1);
            switch (ext.ToLower())
            {
                case "jpg":
                case "jpeg":
                    return ImageFormat.Jpeg;
                case "bmp":
                    return ImageFormat.Bmp;
                case "png":
                    return ImageFormat.Png;
                case "gif":
                    return ImageFormat.Gif;
                default:
                    return ImageFormat.Jpeg;
            }
        }

        /// <summary>
        /// ��ȡͼ���������������������Ϣ
        /// </summary>
        /// <param name="mimeType">��������������Ķ���;�����ʼ�����Э�� (MIME) ���͵��ַ���</param>
        /// <returns>����ͼ���������������������Ϣ</returns>
        public static ImageCodecInfo GetCodecInfo(string mimeType)
        {
            ImageCodecInfo[] CodecInfo = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo ici in CodecInfo)
            {
                if (ici.MimeType == mimeType) return ici;
            }
            return null;
        }

        /// <summary>
        /// �����³ߴ�
        /// </summary>
        /// <param name="width">ԭʼ���</param>
        /// <param name="height">ԭʼ�߶�</param>
        /// <param name="maxWidth">����¿��</param>
        /// <param name="maxHeight">����¸߶�</param>
        /// <returns></returns>
        public static Size ResizeImage(int width, int height, int maxWidth, int maxHeight)
        {
            decimal MAX_WIDTH = (decimal)maxWidth;
            decimal MAX_HEIGHT = (decimal)maxHeight;
            decimal ASPECT_RATIO = MAX_WIDTH / MAX_HEIGHT;

            int newWidth, newHeight;

            decimal originalWidth = (decimal)width;
            decimal originalHeight = (decimal)height;

            if (originalWidth > MAX_WIDTH || originalHeight > MAX_HEIGHT)
            {
                decimal factor;
                // determine the largest factor 
                if (originalWidth / originalHeight > ASPECT_RATIO)
                {
                    factor = originalWidth / MAX_WIDTH;
                    newWidth = Convert.ToInt32(originalWidth / factor);
                    newHeight = Convert.ToInt32(originalHeight / factor);
                }
                else
                {
                    factor = originalHeight / MAX_HEIGHT;
                    newWidth = Convert.ToInt32(originalWidth / factor);
                    newHeight = Convert.ToInt32(originalHeight / factor);
                }
            }
            else
            {
                newWidth = width;
                newHeight = height;
            }

            return new Size(newWidth, newHeight);

        }

        public static Size ResizeImage(string ff, int maxWidth, int maxHeight)
        {
            try
            {
                Bitmap bitmap = new Bitmap(ff);
                return ResizeImage(bitmap.Width, bitmap.Height, maxWidth, maxHeight);
            }
            catch
            {
                return new Size(maxWidth, maxHeight);
            }
        }

        public static int GetResizeImageW(string ff, int maxWidth, int maxHeight)
        {
            try
            {
                return ResizeImage(ff, maxWidth, maxHeight).Width;
            }
            catch
            {
                return maxWidth;
            }
        }

        public static int GetResizeImageH(string ff, int maxWidth, int maxHeight)
        {
            try
            {
                return ResizeImage(ff, maxWidth, maxHeight).Height;
            }
            catch
            {
                return maxHeight;
            }
        }

        public static string GetResizeImage4Html(string ff, int maxWidth, int maxHeight)
        {
            Size size = ResizeImage(ff, maxWidth, maxHeight);
            return "width=" + size.Width.ToString() + "px height=" + size.Height.ToString() + "px";
        }

        public static void CreateResizeImage(string temp_url, string oldurl, string _authKey)
        {
            #region ��������ͼ by 2015-7-24 TODO:�����пս��˲��ִ�����봩͸�㹤������
            WebRequest request = WebRequest.Create(oldurl);
            var response = (HttpWebResponse)request.GetResponse();
            var dataStream = response.GetResponseStream();
            Image thisImg = new Bitmap(dataStream);
            var proportion = 0.77;
            var w = thisImg.Width;
            var h = thisImg.Height;
            var t = 0;
            var l = 0;
            if (thisImg.Width > thisImg.Height)
            {
                w = (int)Math.Round((thisImg.Height) / proportion, 0);
                l = -(int)(thisImg.Width - w) / 2;
            }
            else
            {
                if (thisImg.Width < thisImg.Height)
                {
                    h = (int)Math.Round((thisImg.Width) * proportion, 0);
                    t = -(int)(thisImg.Height - h) / 2;
                }
            }
            var newimg = new Bitmap(w, h);
            Graphics g = Graphics.FromImage(newimg);
            g.DrawImage(thisImg, l, t);
            g.Save();
            var ms = new MemoryStream();
            newimg.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            //ǿ�ƻ���
            newimg.Dispose();
            g.Dispose();
            thisImg.Dispose();
            WebRequest temp_request = WebRequest.Create(temp_url);
            temp_request.Method = "POST";
            temp_request.Timeout = 1000 * 60 * 3; //����ִ�лɨ���־����ʱ��3����
            temp_request.Headers.Add("Authorization", _authKey);
            var temp_bt = ms.ToArray();
            temp_request.ContentLength = temp_bt.Length;
            var temp_dataStream = temp_request.GetRequestStream();
            temp_dataStream.Write(temp_bt, 0, temp_bt.Length);
            ms.Dispose();
            GC.Collect();
            #endregion
        }
        #region GetTnImg

        /// <summary>
        /// Ĭ��ȡ�ø߻�����΢ͼ��ַ������������������
        /// </summary>
        /// <param name="orgImg">Դͼ·�������·��[���ݿ��ŵ�ַ]��</param>
        /// <param name="tnPrefix">��΢ͼǰ׺����</param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="mode">��������ͼ�ķ�ʽ:HW W H Cut </param>
        /// <param name="interpolMode">��ֵ�㷨</param>
        /// <param name="smoothingMode">ƽ������</param>
        /// <returns></returns>
        public static string GetTNimg(string orgImg, string tnPrefix, int width, int height, string mode)
        {
            return GetTNimg(orgImg, tnPrefix, width, height, mode, InterpolationMode.High, SmoothingMode.HighQuality);
        }

        /// <summary>
        /// ȡ����΢ͼ��ַ������������������
        /// </summary>
        /// <param name="orgImg">Դͼ·�������·��[���ݿ��ŵ�ַ]��</param>
        /// <param name="tnPrefix">��΢ͼǰ׺����</param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="mode">��������ͼ�ķ�ʽ:HW W H Cut </param>
        /// <returns></returns>
        public static string GetTNimg(string orgImg, string tnPrefix, int width, int height, string mode, InterpolationMode interpolMode, SmoothingMode smoothingMode)
        {
            string tnImg = orgImg.Insert(orgImg.LastIndexOf("/") + 1, tnPrefix);
            if (File.Exists(HttpContext.Current.Server.MapPath(orgImg)))
            {
                if (!File.Exists(HttpContext.Current.Server.MapPath(tnImg)))
                {
                    ImageHelper.MakeThumbnail(HttpContext.Current.Server.MapPath(orgImg), HttpContext.Current.Server.MapPath(tnImg), width, height, mode, interpolMode, smoothingMode);
                }
            }
            return tnImg;
        }

        /// <summary>
        /// ȡ����΢ͼ��ַ�����ж��Ƿ���ڶ���������
        /// </summary>
        /// <param name="orgImg">Դͼ·�������·��[���ݿ��ŵ�ַ]��</param>
        /// <param name="tnPrefix">��΢ͼǰ׺����</param>
        /// <returns></returns>
        public static string GetTNimg(string orgImg, string tnPrefix)
        {
            string tnImg = orgImg.Insert(orgImg.LastIndexOf("/") + 1, tnPrefix);

            return tnImg;
        }

        #endregion

    }


}
