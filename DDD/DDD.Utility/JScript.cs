using System;
using System.Web;
using System.Web.UI;

namespace DDD.Utility
{

    /// <summary>
    /// һЩ���õ�Js����(Response.Write ���ƻ�w3c��׼��css��ʽ)
    /// </summary>
    public class JScript
    {

        /// <summary>
        /// ����JavaScriptС����
        /// </summary>
        /// <param name="js">������Ϣ</param>
        public static void Alert(string message)
        {
            #region
            string js = @"<Script language='JavaScript'>
                    alert('" + message + "');</Script>";
            HttpContext.Current.Response.Write(js);
            #endregion
        }

        /// <summary>
        /// ����JavaScriptС���ڲ�ˢ�µ�ǰҳ
        /// </summary>
        /// <param name="js">������Ϣ</param>
        public static void AlertAndRefresh(string message)
        {
            #region
            string js = @"<Script language='JavaScript'>
                    alert('" + message + "');window.location = window.location;</Script>";
            HttpContext.Current.Response.Write(js);
            #endregion
        }

        /// <summary>
        /// ����JavaScriptС���ڲ��رյ�ǰҳ��
        /// </summary>
        /// <param name="js">������Ϣ</param>
        public static void AlertAndClose(string message)
        {
            #region
            string js = @"<Script language='JavaScript'>
                    alert('" + message + "');window.opener=null;top.close();</Script>";
            HttpContext.Current.Response.Write(js);
            #endregion
        }

        /// <summary>
        /// ����JavaScriptС���ڲ�����һ�ַ���ֵ
        /// </summary>
        /// <param name="message"></param>
        /// <param name="rValue"></param>
        public static void AlertAndRValue(string message, string rValue)
        {
            #region
            string js = "<script language=javascript>alert('{0}');window.returnValue={1};</script>";
            HttpContext.Current.Response.Write(string.Format(js, message, rValue));
            #endregion
        }

        /// <summary>
        /// ����JavaScriptС���ڲ�����һ�ַ���ֵ���ر��Ӵ���
        /// </summary>
        /// <param name="message"></param>
        /// <param name="rValue"></param>
        public static void AlertAndRValueClose(string message, string rValue)
        {
            #region
            string js = "<script language=javascript>alert('{0}');window.returnValue='{1}';window.opener=null;top.close();</script>";
            HttpContext.Current.Response.Write(string.Format(js, message, rValue));
            #endregion
        }

        /// <summary>
        /// ������Ϣ����ת���µ�URL
        /// </summary>
        /// <param name="message">��Ϣ����</param>
        /// <param name="toURL">���ӵ�ַ</param>
        public static void AlertAndRedirect(string message, string toURL)
        {
            #region
            string js = "<script language=javascript>alert('{0}');window.location.replace('{1}')</script>";
            HttpContext.Current.Response.Write(string.Format(js, message, toURL));
            #endregion
        }


        /// <summary>
        /// ����JavaScriptȷ��С���ڲ�����һ�ַ���ֵ
        /// </summary>
        /// <param name="message">�ǣ������������񣺹رմ���</param>
        /// <param name="rValue"></param>
        public static void ConfirmAndRValue(string message, string rValue)
        {
            #region
            string js = "<script language=javascript>window.returnValue={0};if(!confirm('{1}')){{window.opener=null;top.close();}}</script>";
            HttpContext.Current.Response.Write(string.Format(js,rValue, message));
            //string js = @"<script language=javascript>window.returnValue="+rValue+";if(!confirm('"+message+"')){window.opener=null;top.close();}</script>";
            //HttpContext.Current.Response.Write(js);
            #endregion
        }

        /// <summary>
        /// ����JavaScriptȷ���Ƿ�ر�С���ڲ�����һ�ַ���ֵ
        /// </summary>
        /// <param name="message">�ǣ��رմ��ڣ��񣺼�������</param>
        /// <param name="rValue"></param>
        public static void ConfirmCloseAndRValue(string message, string rValue)
        {
            #region
            string js = "<script language=javascript>window.returnValue={0};if(confirm('{1}')){{window.opener=null;top.close();}}</script>";
            HttpContext.Current.Response.Write(string.Format(js, rValue, message));
            #endregion
        }

        /// <summary>
        /// ����JavaScriptȷ��С���ڲ�����һ�ַ���ֵ���ǣ�ͬʱˢ�µ�ǰҳ��
        /// </summary>
        /// <param name="message">�ǣ������������񣺹رմ���</param>
        /// <param name="rValue"></param>
        public static void ConfirmAndRValueRefresh(string message, string rValue)
        {
            #region
            string js = "<script language=javascript>window.returnValue={0};if(!confirm('{1}')){{window.opener=null;top.close();}}else{{window.location=window.location;}}</script>";
            HttpContext.Current.Response.Write(string.Format(js, rValue, message));
            #endregion
        }

        /// <summary>
        /// ����JavaScriptȷ���Ƿ�ر�С���ڲ�����һ�ַ���ֵ����ͬʱˢ�µ�ǰҳ��
        /// </summary>
        /// <param name="message">�ǣ��رմ��ڣ��񣺼�������</param>
        /// <param name="rValue"></param>
        public static void ConfirmCloseAndRValueRefresh(string message, string rValue)
        {
            #region
            string js = "<script language=javascript>window.returnValue={0};if(confirm('{1}')){{window.opener=null;top.close();}}else{{window.location=window.location;}}</script>";
            HttpContext.Current.Response.Write(string.Format(js, rValue, message));
            #endregion
        }

        /// <summary>
        /// �ص���ʷҳ��
        /// </summary>
        /// <param name="value">-1/1</param>
        public static void GoHistory(int value)
        {
            #region
            string js = @"<Script language='JavaScript'>
                    history.go({0});  
                  </Script>";
            HttpContext.Current.Response.Write(string.Format(js, value));
            #endregion
        }

        /// <summary>
        /// �رյ�ǰ����
        /// </summary>
        public static void CloseWindow()
        {
            #region
            string js = @"<Script language='JavaScript'>
                    window.opener=null;top.close();  
                  </Script>";
            HttpContext.Current.Response.Write(js);
            HttpContext.Current.Response.End();
            #endregion
        }

        /// <summary>
        /// ˢ�µ�ǰҳ��
        /// </summary>
        public static void Refresh()
        {
            #region
            string js = @"<Script language='JavaScript'>
                    window.location = window.location  
                  </Script>";
            HttpContext.Current.Response.Write(js);
            #endregion
        }

        /// <summary>
        /// ˢ�´򿪴���
        /// </summary>
        public static void RefreshOpener(string url)
        {
            #region
            string js = @"<script>try{top.location=""" + url + @"""}catch(e){location=""" + url + @"""}</script>";
            HttpContext.Current.Response.Write(js);
            #endregion
        }


        /// <summary>
        /// ˢ�¸�����
        /// </summary>
        public static void RefreshParent()
        {
            #region
            string js = @"<Script language='JavaScript'>
                    parent.location=parent.location;
                  </Script>";
            HttpContext.Current.Response.Write(js);
            #endregion
        }

        /// <summary>
        /// ��������ˢ�¸�����
        /// </summary>
        public static void RefreshParentByOpener()
        {
            #region
            string js = @"<Script language='JavaScript'>
                    parent.opener.document.location=parent.opener.document.location;
                  </Script>";
            HttpContext.Current.Response.Write(js);
            #endregion
        }
         
        /// <summary>
        /// ��������ָ��������ҳ����ת
        /// </summary>
        public static void ParentJumpByOpener(string newUrl)
        {
            #region
            string js = @"<Script language='JavaScript'>
                    parent.opener.document.location="+newUrl+";</Script>";
            HttpContext.Current.Response.Write(js);
            #endregion
        }

        /// <summary>
        /// ��ָ����С���´���
        /// </summary>
        /// <param name="url">��ַ</param>
        /// <param name="width">��</param>
        /// <param name="heigth">��</param>
        /// <param name="top">ͷλ��</param>
        /// <param name="left">��λ��</param>
        public static void OpenWebFormSize(string url, int width, int heigth, int top, int left)
        {
            #region
            string js = @"<Script language='JavaScript'>window.open('" + url + @"','','height=" + heigth + ",width=" + width + ",top=" + top + ",left=" + left + ",location=no,menubar=no,resizable=yes,scrollbars=yes,status=yes,titlebar=no,toolbar=no,directories=no');</Script>";

            HttpContext.Current.Response.Write(js);
            #endregion
        }


        /// <summary>
        /// ת��Url�ƶ���ҳ��
        /// </summary>
        /// <param name="url">���ӵ�ַ</param>
        public static void JavaScriptLocationHref(string url)
        {
            #region
            string js = @"<Script language='JavaScript'>
                    window.location.replace('{0}');
                  </Script>";
            js = string.Format(js, url);
            HttpContext.Current.Response.Write(js);
            #endregion
        }

        /// <summary>
        /// ��ָ����Сλ�õ�ģʽ�Ի���
        /// </summary>
        /// <param name="webFormUrl">���ӵ�ַ</param>
        /// <param name="width">��</param>
        /// <param name="height">��</param>
        /// <param name="top">������λ��</param>
        /// <param name="left">������λ��</param>
        public static void ShowModalDialogWindow(string webFormUrl, int width, int height, int top, int left)
        {
            #region
            string features = "dialogWidth:" + width.ToString() + "px"
                + ";dialogHeight:" + height.ToString() + "px"
                + ";dialogLeft:" + left.ToString() + "px"
                + ";dialogTop:" + top.ToString() + "px"
                + ";center:yes;help=no;resizable:no;status:no;scroll=yes";
            ShowModalDialogWindow(webFormUrl, features);
            #endregion
        }

        public static void ShowModalDialogWindow(string webFormUrl, string features)
        {
            string js = ShowModalDialogJavascript(webFormUrl, features);
            HttpContext.Current.Response.Write(js);
        }

        public static string ShowModalDialogJavascript(string webFormUrl, string features)
        {
            #region
            string js = @"<script language=javascript>							
							showModalDialog('" + webFormUrl + "','','" + features + "');</script>";
            return js;
            #endregion
        }


    }
}
