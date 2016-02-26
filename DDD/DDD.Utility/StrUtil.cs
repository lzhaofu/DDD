using System;
using System.Linq;
using System.Drawing;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.IO;
using System.Drawing.Imaging;
using System.Text;
using System.Web.Security;
using System.Collections.Generic;
using DDD.Utility;

namespace DDD.Utility
{
    /// <summary>
    /// ��һЩ�ַ������в�������
    /// </summary>
    public class StrUtil : PageBaseUtil
    {

        private static string passWord;	//�����ַ���

        /// <summary>
        /// �ж������Ƿ�����
        /// </summary>
        /// <param name="num">Ҫ�жϵ��ַ���</param>
        /// <returns></returns>
        static public bool VldInt(string num)
        {
            #region
            try
            {
                Convert.ToInt32(num);
                return true;
            }
            catch { return false; }
            #endregion
        }


        /// <summary>
        /// �޸������ַ�
        /// </summary>
        /// <param name="str">Ҫ�滻���ַ���</param>
        /// <returns></returns>
        static public string CheckStr(string str)
        {
            #region
            return str.Replace("&", "&amp;").Replace("'", "&apos;").Replace(@"""", "&quot;").Replace("<", "&lt;").Replace(">", "&gt;").Replace(" where ", " wh&#101;re ").
                Replace(" select ", " sel&#101;ct ").Replace(" insert ", " ins&#101;rt ").Replace(" create ", " cr&#101;ate ").Replace(" drop ", " dro&#112 ").
                Replace(" alter ", " alt&#101;r ").Replace(" delete ", " del&#101;te ").Replace(" update ", " up&#100;ate ").Replace(" or ", " o&#114; ").Replace("\"", @"&#34;")
                .Replace("[ft^", "<img src='" + PageBaseUtil.WebAppPath() + "images/FileTypeIcon/").Replace("^ft]", ".gif' />");
            #endregion
        }

        /// <summary>
        /// �ָ������ַ�
        /// </summary>
        /// <param name="str">Ҫ�滻���ַ���</param>
        /// <returns></returns>
        static public string UnCheckStr(string str)
        {
            #region
            return str.Replace("&amp;", "&").Replace("&apos;", "'").Replace("&quot;", @"""").Replace("&lt;", "<").Replace("&gt;", ">").Replace(" wh&#101;re ", " where ").
                Replace(" sel&#101;ct ", " select ").Replace(" ins&#101;rt ", " insert ").Replace(" cr&#101;ate ", " create ").Replace(" dro&#112 ", " drop ").
                Replace(" alt&#101;r ", " alter ").Replace(" del&#101;te ", " delete ").Replace(" up&#100;ate ", " update ").Replace(" o&#114; ", " or ").Replace(@"&#34;", "\"");
            #endregion
        }


        /// <summary>
        /// �滻html�е������ַ�
        /// </summary>
        /// <param name="theString">��Ҫ�����滻���ı���</param>
        /// <returns>�滻����ı���</returns>
        public static string HtmlEncode(string theString)
        {
            if (string.IsNullOrEmpty(theString))
                return string.Empty;
            theString = theString.Replace(">", "&gt;");
            theString = theString.Replace("<", "&lt;");
            theString = theString.Replace("  ", " &nbsp;");
            theString = theString.Replace("\"", "&quot;");
            theString = theString.Replace("\'", "&#39;");
            theString = theString.Replace("\n", "<br/> ");
            return theString;
        }

        /// <summary>
        /// �滻ָ���ַ�������html�е������ַ�
        /// </summary>
        /// <param name="theString"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static string HtmlEncodeLength(string theString, int maxLength)
        {
            if (string.IsNullOrEmpty(theString))
                return string.Empty;
            if (theString.Length > maxLength)
                theString = theString.Substring(0, maxLength);
            theString = theString.Replace(">", "&gt;");
            theString = theString.Replace("<", "&lt;");
            theString = theString.Replace("  ", " &nbsp;");
            theString = theString.Replace("\"", "&quot;");
            theString = theString.Replace("\'", "&#39;");
            theString = theString.Replace("\n", "<br/> ");
            return theString;
        }


        /// <summary>
        /// �ָ�html�е������ַ�(�༭�ı���Excel��ʾʱ���ã�webҳ����ʾ����Ҫ����)
        /// </summary>
        /// <param name="theString">��Ҫ�ָ����ı���</param>
        /// <returns>�ָ��õ��ı���</returns>
        public static string HtmlDiscode(string theString)
        {
            if (string.IsNullOrEmpty(theString))
                return string.Empty;
            theString = theString.Replace("&gt;", ">");
            theString = theString.Replace("&lt;", "<");
            theString = theString.Replace("&nbsp;", " ");
            theString = theString.Replace(" &nbsp;", "  ");
            theString = theString.Replace("&quot;", "\"");
            theString = theString.Replace("&#39;", "\'");
            theString = theString.Replace("<br/> ", "\n");
            return theString;
        }

        /// <summary>
        /// ����ָ�����ȵ�������Ϣ��һ�������û��������±���ȹ��ˣ�
        /// </summary>
        /// <param name="text">����</param>
        /// <param name="maxLength">��󳤶�</param>
        /// <returns></returns>
        public static string ChkInputTextLength(string text, int maxLength)
        {
            #region

            if (string.IsNullOrEmpty(text))
                return string.Empty;
            text = text.Trim();
            if (text.Length > maxLength)
                text = text.Substring(0, maxLength);
            text = Regex.Replace(text, "[\\s]{2,}", " ");	//two or more spaces
            text = Regex.Replace(text, "(<[b|B][r|R]/*>)+|(<[p|P](.|\\n)*?>)", "\n");	//<br>
            text = Regex.Replace(text, "(\\s*&[n|N][b|B][s|S][p|P];\\s*)+", " ");	//&nbsp;
            text = Regex.Replace(text, "<(.|\\n)*?>", string.Empty);	//any other tags
            text = text.Replace("'", "''");
            return text;
            #endregion
        }

        /// <summary>
        /// ����������Ϣ
        /// </summary>
        /// <param name="text">����</param>
        /// <returns></returns>
        public static string ChkInputText(string text)
        {
            #region

            if (string.IsNullOrEmpty(text))
                return string.Empty;
            text = text.Trim();
            text = Regex.Replace(text, "[\\s]{2,}", " ");	//two or more spaces
            text = Regex.Replace(text, "(<[b|B][r|R]/*>)+|(<[p|P](.|\\n)*?>)", "\n");	//<br>
            text = Regex.Replace(text, "(\\s*&[n|N][b|B][s|S][p|P];\\s*)+", " ");	//&nbsp;
            text = Regex.Replace(text, "<(.|\\n)*?>", string.Empty);	//any other tags
            text = text.Replace("'", "''");
            return text;
            #endregion
        }

        /// <summary>
        /// �������������дcookie
        /// </summary>
        /// <returns></returns>
        static public string GenerateCheckCode()
        {
            #region
            int number;
            char code;
            string checkCode = String.Empty;

            System.Random random = new Random();

            for (int i = 0; i < 5; i++)
            {
                number = random.Next();

                if (number % 2 == 0)
                    code = (char)('0' + (char)(number % 10));
                else
                    code = (char)('A' + (char)(number % 26));

                checkCode += code.ToString();
            }

            HttpContext.Current.Response.Cookies.Add(new HttpCookie("CheckCode", checkCode));

            return checkCode;
            #endregion
        }

        /// <summary>
        /// ��ȡ���ֵ�һ��ƴ��
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        static public string getSpells(string input)
        {
            #region
            int len = input.Length;
            string reVal = "";
            for (int i = 0; i < len; i++)
            {
                reVal += getSpell(input.Substring(i, 1));
            }
            return reVal;
            #endregion
        }

        /// <summary>
        /// ���ֱ���ת��,���IE��ַ������
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        static public string Strencode(string input)
        {
            #region
            return System.Web.HttpUtility.UrlEncode(ChkInputTextLength(input, 100));
            #endregion
        }

        static public string getSpell(string cn)
        {
            #region
            byte[] arrCN = Encoding.Default.GetBytes(cn);
            if (arrCN.Length > 1)
            {
                int area = (short)arrCN[0];
                int pos = (short)arrCN[1];
                int code = (area << 8) + pos;
                int[] areacode = { 45217, 45253, 45761, 46318, 46826, 47010, 47297, 47614, 48119, 48119, 49062, 49324, 49896, 50371, 50614, 50622, 50906, 51387, 51446, 52218, 52698, 52698, 52698, 52980, 53689, 54481 };
                for (int i = 0; i < 26; i++)
                {
                    int max = 55290;
                    if (i != 25) max = areacode[i + 1];
                    if (areacode[i] <= code && code < max)
                    {
                        return Encoding.Default.GetString(new byte[] { (byte)(65 + i) });
                    }
                }
                return "?";
            }
            else return cn;
            #endregion
        }


        /// <summary>
        /// ���תȫ��
        /// </summary>
        /// <param name="BJstr"></param>
        /// <returns></returns>
        static public string GetQuanJiao(string BJstr)
        {
            #region
            char[] c = BJstr.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                byte[] b = System.Text.Encoding.Unicode.GetBytes(c, i, 1);
                if (b.Length == 2)
                {
                    if (b[1] == 0)
                    {
                        b[0] = (byte)(b[0] - 32);
                        b[1] = 255;
                        c[i] = System.Text.Encoding.Unicode.GetChars(b)[0];
                    }
                }
            }

            string strNew = new string(c);
            return strNew;

            #endregion
        }

        /// <summary>
        /// ȫ��ת���
        /// </summary>
        /// <param name="QJstr"></param>
        /// <returns></returns>
        static public string GetBanJiao(string QJstr)
        {
            #region
            char[] c = QJstr.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                byte[] b = System.Text.Encoding.Unicode.GetBytes(c, i, 1);
                if (b.Length == 2)
                {
                    if (b[1] == 255)
                    {
                        b[0] = (byte)(b[0] + 32);
                        b[1] = 0;
                        c[i] = System.Text.Encoding.Unicode.GetChars(b)[0];
                    }
                }
            }
            string strNew = new string(c);
            return strNew;
            #endregion
        }

        #region ���ܵ�����
        /// <summary>
        /// ���ܵ�����
        /// </summary>
        public enum PasswordType
        {
            SHA1,
            MD5
        }
        #endregion


        /// <summary>
        /// �ַ�������
        /// </summary>
        /// <param name="PasswordString">Ҫ���ܵ��ַ���</param>
        /// <param name="PasswordFormat">Ҫ���ܵ����</param>
        /// <returns></returns>
        static public string EncryptPassword(string PasswordString, string PasswordFormat)
        {
            #region
            switch (PasswordFormat)
            {
                case "SHA1":
                    {
                        passWord = FormsAuthentication.HashPasswordForStoringInConfigFile(PasswordString, "SHA1");
                        break;
                    }
                case "MD5":
                    {
                        passWord = FormsAuthentication.HashPasswordForStoringInConfigFile(PasswordString, "MD5");
                        break;
                    }
                default:
                    {
                        passWord = string.Empty;
                        break;
                    }
            }
            return passWord;
            #endregion
        }

        /// <summary>
        /// md5����
        /// </summary>
        /// <param name="str"></param>
        /// <param name="Is32or16">����32��16λ</param>
        /// <param name="toLower">�Ƿ�ת����Сд</param>
        /// <returns></returns>
        public static string Md5(string str, bool Is32or16, bool toLower)
        {
            string re = string.Empty;
            try
            {
                re = Is32or16 ? FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5") : FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").Substring(8, 16);
                if (toLower) re = re.ToLower();
            }
            catch (Exception)
            {
            }
            return re;
        }

        /// <summary>
        /// �Ƿ���10����С��
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static bool IsNumeric(string Value)
        {
            try
            {
                decimal i = Convert.ToDecimal(Value);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// �Ƿ��Ǵ���0��10����С��
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static bool IsPosNumeric(string Value)
        {
            try
            {
                decimal i = Convert.ToDecimal(Value);
                if (i > 0) return true;
                else return false;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// �Ƿ��Ǵ��ڵ���0��ʮ����С��
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static bool Is0PosNumeric(string Value)
        {
            try
            {
                decimal i = Convert.ToDecimal(Value);
                if (i >= 0) return true;
                else return false;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// �Ƿ��Ǵ���0����������(32λ)
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static bool IsPosInt(string Value)
        {
            try
            {
                int i = Convert.ToInt32(Value);
                if (i > 0)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// �Ƿ��Ǵ���0����������(64λ)
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static bool IsPosLong(string Value)
        {
            try
            {
                long i = Convert.ToInt64(Value);
                if (i > 0)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// �Ƿ���0-255���ֽ�
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static bool IsByte(string Value)
        {
            try
            {
                byte i = Convert.ToByte(Value);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// �Ƿ��������ַ�
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsHZ(string str)//�ж��Ƿ��������ַ�
        {
            bool result = false;
            ASCIIEncoding ascii = new ASCIIEncoding();
            byte[] s = ascii.GetBytes(str);
            for (int i = 0; i < s.Length; i++)
            {
                if ((int)s[i] == 63)
                {
                    result = true;
                }

            }
            return result;
        }

        /// <summary>
        /// ȡ���ַ������ȣ��������ģ�
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int StrLength(string str)
        {
            ASCIIEncoding ascii = new ASCIIEncoding();
            int Length = 0;
            byte[] s = ascii.GetBytes(str);
            for (int i = 0; i < s.Length; i++)
            {
                if ((int)s[i] == 63)
                {
                    Length += 2;
                }
                else
                {
                    Length += 1;
                }
            }
            return Length;
        }

        /// <summary>
        /// ��ȡ�ַ��������������ַ���
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string GetCutString(string inputString, int len)
        {
            ASCIIEncoding ascii = new ASCIIEncoding();
            int tempLen = 0;
            string tempString = "";
            byte[] s = ascii.GetBytes(inputString);
            for (int i = 0; i < s.Length; i++)
            {
                if ((int)s[i] == 63)
                {
                    tempLen += 2;
                }
                else
                {
                    tempLen += 1;
                }
                if (tempLen > len)
                    break;

                try
                {
                    tempString += inputString.Substring(i, 1);
                }
                catch
                {
                    break;
                }


            }

            return tempString;
        }

        /// <summary>
        /// ��ȡ�ַ���ָ������,��'...'(���������ַ�)
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string FormatContent(string inputString, int len)
        {

            ASCIIEncoding ascii = new ASCIIEncoding();
            int tempLen = 0;
            string tempString = "";
            byte[] s = ascii.GetBytes(inputString);
            for (int i = 0; i < s.Length; i++)
            {
                if ((int)s[i] == 63)
                {
                    tempLen += 2;
                }
                else
                {
                    tempLen += 1;
                }
                if (tempLen > len)
                    break;

                try
                {
                    tempString += inputString.Substring(i, 1);
                }
                catch
                {
                    break;
                }

            }
            byte[] mybyte = System.Text.Encoding.Default.GetBytes(inputString);
            if (mybyte.Length > len)
                tempString = GetCutString(tempString, len - 2) + "��";
            return tempString;

        }
        /// <summary>
        /// ��ȡ�ַ���,��'...'
        /// </summary>
        /// <param name="text"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static string SubMixText(string text, int maxLength)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }
            else
            {
                string strReturn = "";
                string strTemp = text;
                //���ȫ��
                if (Regex.Replace(strTemp, "[\u4e00-\u9fa5]", "zz", RegexOptions.IgnoreCase).Length <= maxLength)
                {
                    strReturn = strTemp;
                }
                else
                {
                    for (int i = strTemp.Length; i >= 0; i--)
                    {
                        strTemp = strTemp.Substring(0, i);
                        if (Regex.Replace(strTemp, "[\u4e00-\u9fa5]", "zz", RegexOptions.IgnoreCase).Length <= maxLength)
                        {
                            strReturn = strTemp + "��";
                            break;
                        }
                    }
                }
                return strReturn;
            }

        }


        /// <summary>
        /// ��ȡ�ַ���ָ������+...�������ַ����ո�(���������ַ�)
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string FormatContentEmpty(string inputString, int len)
        {

            ASCIIEncoding ascii = new ASCIIEncoding();
            int tempLen = 0;
            string tempString = "";
            byte[] s = ascii.GetBytes(inputString);
            for (int i = 0; i < s.Length; i++)
            {
                if ((int)s[i] == 63)
                {
                    tempLen += 2;
                }
                else
                {
                    tempLen += 1;
                }

                try
                {
                    tempString += inputString.Substring(i, 1);
                }
                catch
                {
                    break;
                }

                if (tempLen > len)
                    break;
            }
            byte[] mybyte = System.Text.Encoding.Default.GetBytes(inputString);
            if (mybyte.Length > len)
                tempString = GetCutString(tempString, len - 2) + "��";
            else
            {
                string nbsp = "";
                int subi = len - mybyte.Length;
                for (int i = 0; i < subi; i++)
                    nbsp += "&nbsp;";
                tempString += nbsp;
            }
            return tempString;

        }
        /// <summary>
        /// ȡ�м���ַ�(֧������)
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="start"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string MidString(string inputString, int start, int len)
        {
            ASCIIEncoding ascii = new ASCIIEncoding();
            int tempLen = 0;
            string tempString = "";
            byte[] s = ascii.GetBytes(inputString);
            for (int i = 0; i < s.Length; i++)
            {
                if ((int)s[i] == 63)
                {
                    tempLen += 2;
                }
                else
                {
                    tempLen += 1;
                }
                if (start <= tempLen)
                {
                    try
                    {
                        tempString += inputString.Substring(i, 1);
                    }
                    catch
                    {
                        break;
                    }
                }

                if (tempLen - start >= len)
                    break;
            }

            //���۵Ĳ���...��ʱ�䲻����
            if (StrLength(tempString) > len + 1)
                tempString = tempString.Substring(1);
            if (StrLength(tempString) == len + 1)
            {
                tempString = tempString.Substring(0, tempString.Length - 1);
                tempString += " ";
            }
            return tempString;
        }
        /// <summary>
        /// �Ƿ���ʱ������
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsDate(string input)
        {
            try
            {
                DateTime.Parse(input);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// �Ƿ���0��������
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool Is0PosInt(string s)
        {
            try
            {
                int i = int.Parse(s);
                if (0 <= i && i <= Int32.MaxValue) return true;
                else return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// �Ƿ���0����������
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public static bool Is0PosFloat(string f)
        {
            return (Regex.IsMatch(f, @"^(0?|[1-9]\d*)(\.\d{0,1})?$"));
        }



        /// <summary>
        /// ��ȡ����Html������ַ�����html���벻������
        /// </summary>
        /// <param name="AHtml"></param>
        /// <param name="ALength"></param>
        /// <returns></returns>
        public static string HtmlSubstring(string AHtml, int ALength)
        {
            string vReturn = "";
            int vLength = 0; // ���ӵ����ֳ���
            int vFlag = 0; // ��ǰɨ������� 0:��ͨ�� 1:����� // �������ڱ���г���<button value="<">���
            foreach (char vChar in AHtml)
            {
                switch (vFlag)
                {
                    case 0: // ��ͨ��
                        if (vChar == '<')
                        {
                            vReturn += vChar;
                            vFlag = 1;
                        }
                        else
                        {
                            vLength++;
                            if (vLength <= ALength)
                                vReturn += vChar;
                        }
                        break;
                    case 1: // �����
                        if (vChar == '>') vFlag = 0;
                        vReturn += vChar;
                        break;
                }
            }
            #region ɾ����Ч��� // "<span><b></b></span>" -> ""
            string vTemp = Regex.Replace(vReturn, @"<[^>^\/]*?><\/[^>]*?>", "", RegexOptions.IgnoreCase); // ɾ���ձ��
            while (vTemp != vReturn)
            {
                vReturn = vTemp;
                vTemp = Regex.Replace(vReturn, @"<[^>\/]*?><\/[^>]*?>", "", RegexOptions.IgnoreCase); // ɾ���ձ��
            }
            #endregion
            return vReturn;
        }

        /// <summary>
        /// �Ƴ�html����
        /// </summary>
        /// <param name="strHTML"></param>
        /// <returns></returns>
        public static string RemoveHTML(string strHTML)
        {
            if (string.IsNullOrWhiteSpace(strHTML))
            {
                return string.Empty;
            }
            Regex Regexp = new Regex("<.+?>");
            string strReturn = Regexp.Replace(strHTML, "");
            return strReturn;
        }


        /// <summary>
        /// ��ȡժҪ���Ƿ����HTML���� (������)
        /// </summary>
        /// <param name="content"></param>
        /// <param name="length"></param>
        /// <param name="StripHTML"></param>
        /// <returns></returns>
        public static string GetContentSummary(string content, int length, bool StripHTML)
        {
            if (string.IsNullOrEmpty(content) || length == 0)
                return "";
            if (StripHTML)
            {
                Regex re = new Regex("<[^>]*>");
                content = re.Replace(content, "");
                content = content.Replace("��", "").Replace(" ", "");
                if (content.Length <= length)
                    return content;
                else
                    return content.Substring(0, length) + "����";
            }
            else
            {
                if (content.Length <= length)
                    return content;

                int pos = 0, npos = 0, size = 0;
                bool firststop = false, notr = false, noli = false;
                StringBuilder sb = new StringBuilder();
                while (true)
                {
                    if (pos >= content.Length)
                        break;
                    string cur = content.Substring(pos, 1);
                    if (cur == "<")
                    {
                        string next = content.Substring(pos + 1, 3).ToLower();
                        if (next.IndexOf("p") == 0 && next.IndexOf("pre") != 0)
                        {
                            npos = content.IndexOf(">", pos) + 1;
                        }
                        else if (next.IndexOf("/p") == 0 && next.IndexOf("/pr") != 0)
                        {
                            npos = content.IndexOf(">", pos) + 1;
                            if (size < length)
                                sb.Append("<br/>");
                        }
                        else if (next.IndexOf("br") == 0)
                        {
                            npos = content.IndexOf(">", pos) + 1;
                            if (size < length)
                                sb.Append("<br/>");
                        }
                        else if (next.IndexOf("img") == 0)
                        {
                            npos = content.IndexOf(">", pos) + 1;
                            if (size < length)
                            {
                                sb.Append(content.Substring(pos, npos - pos));
                                size += npos - pos + 1;
                            }
                        }
                        else if (next.IndexOf("li") == 0 || next.IndexOf("/li") == 0)
                        {
                            npos = content.IndexOf(">", pos) + 1;
                            if (size < length)
                            {
                                sb.Append(content.Substring(pos, npos - pos));
                            }
                            else
                            {
                                if (!noli && next.IndexOf("/li") == 0)
                                {
                                    sb.Append(content.Substring(pos, npos - pos));
                                    noli = true;
                                }
                            }
                        }
                        else if (next.IndexOf("tr") == 0 || next.IndexOf("/tr") == 0)
                        {
                            npos = content.IndexOf(">", pos) + 1;
                            if (size < length)
                            {
                                sb.Append(content.Substring(pos, npos - pos));
                            }
                            else
                            {
                                if (!notr && next.IndexOf("/tr") == 0)
                                {
                                    sb.Append(content.Substring(pos, npos - pos));
                                    notr = true;
                                }
                            }
                        }
                        else if (next.IndexOf("td") == 0 || next.IndexOf("/td") == 0)
                        {
                            npos = content.IndexOf(">", pos) + 1;
                            if (size < length)
                            {
                                sb.Append(content.Substring(pos, npos - pos));
                            }
                            else
                            {
                                if (!notr)
                                {
                                    sb.Append(content.Substring(pos, npos - pos));
                                }
                            }
                        }
                        else
                        {
                            npos = content.IndexOf(">", pos) + 1;
                            sb.Append(content.Substring(pos, npos - pos));
                        }
                        if (npos <= pos)
                            npos = pos + 1;
                        pos = npos;
                    }
                    else
                    {
                        if (size < length)
                        {
                            sb.Append(cur);
                            size++;
                        }
                        else
                        {
                            if (!firststop)
                            {
                                sb.Append("����");
                                firststop = true;
                            }
                        }
                        pos++;
                    }

                }
                return sb.ToString();
            }
        }
        /// <summary>
        /// ����Ƿ���ȫ��Ψһ��ʶ
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool ChkIsGuid(string s)
        {
            Guid gv = Guid.Empty;
            try
            {
                gv = new Guid(s);
            }
            catch
            {

            }
            return (gv != Guid.Empty) ? true : false;

        }

        /// <summary>
        /// ��ʽ�������ַ���
        /// </summary>
        /// <param name="Sepstr">�����ַ�ʹ�õķָ���,��:"_"</param>
        /// <param name="ss"></param>
        /// <returns></returns>
        public static string FormatJoinStr(string Sepstr, params object[] ss)
        {
            string re = "";
            if (ss != null)
            {
                bool flag = false;
                for (int i = 0; i < ss.Length; i++)
                {
                    re += ss[i].ToString() + Sepstr;
                    flag = true;
                }
                if (flag)
                    re = re.Substring(0, re.LastIndexOf(Sepstr));
            }
            return re;
        }

        /// <summary>
        /// ���ַ���ǰ���0
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string AddZeroToStr(int str, int length)
        {
            string restr = str.ToString();

            for (int i = 0; i < length - str.ToString().Length; i++)
            {
                restr = "0" + restr;
            }

            return restr;
        }

        /// <summary>
        /// ������","�������ҹ����ظ����ַ���(by Linq)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FilterRepStr(string str)
        {
            string[] arrr = str.Split(',').Distinct().ToArray();
            return string.Join(",", arrr);
        }


        /// <summary>
        /// ���ַ���ת���������ַ���(9���Ƶ�)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ConvertStringToNumbers(string value)
        {
            StringBuilder sb = new StringBuilder();

            foreach (char c in value)
            {
                int cAscil = (int)c;
                sb.Append(Convert.ToString(cAscil, 8) + "9");
            }

            return sb.ToString();
        }
        /// <summary>
        /// �������ַ���ת������ͨ�ַ��ַ���
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ConvertNumbersToString(string value)
        {
            string[] splitInt = value.Split(new char[] { '9' }, StringSplitOptions.RemoveEmptyEntries);

            var splitChars = splitInt.Select(s => Convert.ToChar(
                                              Convert.ToInt32(s, 8)
                                            ).ToString());

            return string.Join("", splitChars);
        }

        /// <summary>
        /// ��ȡָ�����ȵ��Ʊ��
        /// </summary>
        /// <param name="tab"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetTabByLength(string tab, int length)
        {
            string tabs = string.Empty;
            for (int i = 0; i < length; i++)
            {
                tabs += tab;
            }
            return tabs;
        }


        /// <summary>
        /// ��ʽ������ʾΪ��0����С�������֧��4λС����
        /// </summary>
        /// <param name="oldValue"></param>
        /// <returns></returns>
        public static decimal AutoPrecision(decimal? oldValue)
        {
            if (!oldValue.HasValue)
            {
                return 0;
            }

            var value = oldValue.ToString();
            var precision = 0;
            var twoPart = value.Split(new char[] { '.' });
            if (twoPart.Length > 1)
            {
                var listNum = twoPart[1].Select(n => int.Parse(n.ToString())).ToList();
                if (listNum[3] > 0)
                {
                    precision = 4;
                }
                else if (listNum[2] > 0)
                {
                    precision = 3;
                }
                else if (listNum[1] > 0)
                {
                    precision = 2;
                }
                else if (listNum[0] > 0)
                {
                    precision = 1;
                }
            }
            else
            {
                precision = 0;
            }
            var formatStr = "{0:NX}".Replace("X", precision.ToString());
            return decimal.Parse(string.Format(formatStr, oldValue));
        }

        /// <summary>
        /// �ڱβ��ֹؼ��ַ�
        /// </summary>
        /// <param name="s"></param>
        /// <param name="startIndex">��ʼλ�ô�1��ʼ</param>
        /// <param name="maskLength">�ڱγ���</param>
        /// <param name="maskStr">�滻���ַ� Ĭ��Ϊ*</param>
        /// <returns></returns>
        public static string MaskKeyWordString(string s,int startIndex,int maskLength,string maskStr)
        {
            string re = s;
            if (string.IsNullOrWhiteSpace(s)) return re;
            if (string.IsNullOrWhiteSpace(maskStr)) maskStr = "*";
            var maskCh = maskStr[0];
            char[] ch = re.ToCharArray();
            var maxLength = ch.Length;
            if (startIndex >= maxLength) return re;
            var toMaskLength = maskLength + startIndex-1;
            toMaskLength = maxLength < toMaskLength ? maxLength : toMaskLength;
            var start = startIndex - 1;
            for (int i = start; i < toMaskLength; i++)
            {
                ch[i] = maskCh;
            }
            re = new string(ch);
            return re;
        }


        readonly static string[] _BlackInFormParamNames =
            {
               "&","��","#","%","+"
            };


        /// <summary>
        /// ��ƴ���ύ���Ƽ�� (��֧��������������)
        /// </summary>
        /// <param name="s"></param>
        /// <param name="alertName">�쳣�����������ƣ���Ʒ���� </param>
        public static void CheckFormParamName(string s, string alertName)
        {
            if (_BlackInFormParamNames.Any(s.Contains))
            {
                throw new InvalidOperationException(string.Format("{0}���зǷ��ַ���",alertName));
            }
        }


        private const string RegEmail =
            @"^[-0-9a-zA-Z~!$%^&*_=+}{\'?]+(\.[-0-9a-zA-Z~!$%^&*_=+}{\'?]+)*@([0-9a-zA-Z_][-0-9a-zA-Z_]*(\.[-0-9a-zA-Z_]+)*\.(aero|arpa|biz|com|coop|edu|gov|info|int|mil|museum|name|net|org|pro|travel|mobi|[0-9a-zA-Z_][-0-9a-zA-Z_]*)|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(:[0-9]{1,5})?$";
        /// <summary>
        /// �Ƿ��ǺϷ�������
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public static bool IsMail(string f)
        {
            return (Regex.IsMatch(f, RegEmail));
        }

        /// <summary>
        /// �滻�ֻ�����λ****
        /// </summary>
        /// <param name="phonenum"></param>
        /// <returns></returns>
        public static string GetEncryptPhone(string phonenum)
        {
            if (!string.IsNullOrEmpty(phonenum))
            {
                if (phonenum.Length == 11)
                {
                    var r = new Regex(@"(\d{3})\d{4}(\d{4})");
                    return r.Replace(phonenum, "$1****$2");
                }
            }
            return phonenum;
        }
    }
}
