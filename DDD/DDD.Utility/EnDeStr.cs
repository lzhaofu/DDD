using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DDD.Utility
{
    /// <summary>
    /// EnDeStr ��ժҪ˵��
    /// </summary>
    public static class EnDeStr
    {
        /// <summary>
        /// ���� Cookie�ַ���  
        /// </summary>
        /// <param name="cookie">��Ҫ���ܵ�cookie�ַ���</param>
        /// <param name="type"></param>
        /// <returns>���ܹ���cookie�ַ���</returns>
        public static string EncryptCookie(string cookie, int type)
        {
            string str = En(cookie, type);
            StringBuilder sb = new StringBuilder();
            foreach (char a in str)
            {
                sb.Append(Convert.ToString(a, 16).PadLeft(4, '0'));
            }
            return sb.ToString();
        }

        private static string En(string cookie, int type)
        {
            string str;
            if (type%2 == 0)
            {
                str = Transform1(cookie);
            }
            else
            {
                str = Transform3(cookie);
            }

            str = Transform2(cookie);
            return str;
        }

        /// <summary>
        /// ����Cookie�ַ���
        /// </summary>
        /// <param name="cookie">��Ҫ���ܵ�cookie�ַ���</param>
        /// <param name="type"></param>
        /// <returns>���ܺ��cookie�ַ���</returns>
        public static string DecryptCookie(string cookie, int type)
        {
            StringBuilder sb = new StringBuilder();
            string[] strarr = new String[255];
            int i, j, count = cookie.Length/4;
            string strTmp;

            for (i = 0; i < count; i++)
            {
                for (j = 0; j < 4; j++)
                {
                    sb.Append(cookie.Substring(i*4 + j, 1));
                }
                strarr[i] = sb.ToString();
                sb.Remove(0, sb.Length);
            }

            for (i = 0; i < count; i++)
            {
                strTmp = uint.Parse(strarr[i], System.Globalization.NumberStyles.AllowHexSpecifier).ToString("D");
                char ch = (char) uint.Parse(strTmp);
                sb.Append(ch);
            }

            return De(sb.ToString(), type);
        }

        private static string De(string cookie, int type)
        {
            string str;
            if (type%2 == 0)
            {
                str = DeTransform1(cookie);
            }
            else
            {
                str = DeTransform3(cookie);
            }

            str = Transform2(cookie);
            return str;
        }

        public static string Transform1(string str)
        {
            int i = 0;
            StringBuilder sb = new StringBuilder();

            foreach (char a in str)
            {
                switch (i%6)
                {
                    case 0:
                        sb.Append((char) (a + 1));
                        break;
                    case 1:
                        sb.Append((char) (a + 5));
                        break;
                    case 2:
                        sb.Append((char) (a + 7));
                        break;
                    case 3:
                        sb.Append((char) (a + 2));
                        break;
                    case 4:
                        sb.Append((char) (a + 4));
                        break;
                    case 5:
                        sb.Append((char) (a + 9));
                        break;
                }
                i++;
            }

            return sb.ToString();
        }

        public static string Transform2(string str)
        {
            uint j = 0;
            StringBuilder sb = new StringBuilder();

            str = Reverse(str);
            foreach (char a in str)
            {
                j = a;
                if (j > 255)
                {
                    j = (uint) ((a >> 8) + ((a & 0x0ff) << 8));
                }
                else
                {
                    j = (uint) ((a >> 4) + ((a & 0x0f) << 4));
                }
                sb.Append((char) j);
            }

            return sb.ToString();
        }

        public static string Transform3(string str)
        {
            int i = 0;
            StringBuilder sb = new StringBuilder();

            foreach (char a in str)
            {
                switch (i%6)
                {
                    case 0:
                        sb.Append((char) (a + 3));
                        break;
                    case 1:
                        sb.Append((char) (a + 6));
                        break;
                    case 2:
                        sb.Append((char) (a + 8));
                        break;
                    case 3:
                        sb.Append((char) (a + 7));
                        break;
                    case 4:
                        sb.Append((char) (a + 5));
                        break;
                    case 5:
                        sb.Append((char) (a + 2));
                        break;
                }
                i++;
            }

            return sb.ToString();
        }

        public static string DeTransform1(string str)
        {
            int i = 0;
            StringBuilder sb = new StringBuilder();

            foreach (char a in str)
            {
                switch (i%6)
                {
                    case 0:
                        sb.Append((char) (a - 1));
                        break;
                    case 1:
                        sb.Append((char) (a - 5));
                        break;
                    case 2:
                        sb.Append((char) (a - 7));
                        break;
                    case 3:
                        sb.Append((char) (a - 2));
                        break;
                    case 4:
                        sb.Append((char) (a - 4));
                        break;
                    case 5:
                        sb.Append((char) (a - 9));
                        break;
                }
                i++;
            }

            return sb.ToString();
        }


        public static string DeTransform3(string str)
        {
            int i = 0;
            StringBuilder sb = new StringBuilder();

            foreach (char a in str)
            {
                switch (i%6)
                {
                    case 0:
                        sb.Append((char) (a - 3));
                        break;
                    case 1:
                        sb.Append((char) (a - 6));
                        break;
                    case 2:
                        sb.Append((char) (a - 8));
                        break;
                    case 3:
                        sb.Append((char) (a - 7));
                        break;
                    case 4:
                        sb.Append((char) (a - 5));
                        break;
                    case 5:
                        sb.Append((char) (a - 2));
                        break;
                }
                i++;
            }
            return sb.ToString();
        }

        public static string Reverse(string str)
        {
            int i;
            StringBuilder sb = new StringBuilder();

            for (i = str.Length - 1; i >= 0; i--)
            {
                sb.Append(str[i]);
            }

            return sb.ToString();
        }

        //======�����λ���ܣ����ܺ��ַ�ΪA-Z===================================================================================================================================

        public const UInt16 Var1 = 52845, Var2 = 22719; //������16λ��2�ֽ�

        public static string EncryptToLetter(string s)
        {
            return EncryptLittle(s, 903);
        }

        public static string DecryptFromLetter(string s)
        {
            return DecryptLittle(s, 903);
        }

        public static string EncryptLittle(string s, UInt16 key)
        {
            StringBuilder sb = new StringBuilder();
            string result = "";
            int p = 0;
            byte[] bt = Encoding.Default.GetBytes(s);
            for (int i = 0; i < bt.Length; i++)
            {
                sb.Append((char) (bt[i] ^ (key >> 8)));
                key = (UInt16) (((byte) sb[i] + key)*Var1 + Var2);

            }
            for (int i = 0; i < sb.Length; i++)
            {
                p = (int) sb[i];
                result = result + (char) (65 + (p/26)) + (char) (65 + (p%26));
            }
            return result;
        }

        public static string DecryptLittle(string s, UInt16 key)
        {
            int p = 0;
            byte[] bt = new byte[s.Length/2];

            for (int i = 0; i < s.Length/2; i++)
            {
                p = ((int) (s[2*i]) - 65)*26;
                p = p + (int) (s[2*i + 1]) - 65;
                bt[i] = (byte) p;
            }
            byte[] bt2 = new byte[bt.Length];
            for (int i = 0; i < bt.Length; i++)
            {
                bt2[i] = (byte) (bt[i] ^ (key >> 8));
                key = (UInt16) ((bt[i] + key)*Var1 + Var2);
            }
            return Encoding.Default.GetString(bt2);
        }

        //========================================================================================================================================

        #region �ԳƼ����㷨AES RijndaelManaged���ܽ���

        private static readonly string Default_AES_Key = "@#veteran911";
        private static byte[] Keys = { 0x41, 0x72, 0x65, 0x79, 0x6F, 0x75, 0x6D, 0x79,0x53,0x6E, 0x6F, 0x77, 0x6D, 0x61, 0x6E, 0x3F };

        /// <summary>
        /// �ԳƼ����㷨AES RijndaelManaged����(RijndaelManaged��AES���㷨�ǿ�ʽ�����㷨)
        /// </summary>
        /// <param name="encryptString">�������ַ���</param>
        /// <returns>���ܽ���ַ���</returns>
        public static string AES_Encrypt(string encryptString)
        {
            return AES_Encrypt(encryptString, Default_AES_Key);
        }

        /// <summary>
        /// �ԳƼ����㷨AES RijndaelManaged����(RijndaelManaged��AES���㷨�ǿ�ʽ�����㷨)
        /// </summary>
        /// <param name="encryptString">�������ַ���</param>
        /// <param name="encryptKey">������Կ�������ַ�</param>
        /// <returns>���ܽ���ַ���</returns>
        public static string AES_Encrypt(string encryptString, string encryptKey)
        {
            encryptKey = GetSubString(encryptKey, 32, "");
            encryptKey = encryptKey.PadRight(32, ' ');

            RijndaelManaged rijndaelProvider = new RijndaelManaged();
            rijndaelProvider.Key = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 32));
            rijndaelProvider.IV = Keys;
            ICryptoTransform rijndaelEncrypt = rijndaelProvider.CreateEncryptor();

            byte[] inputData = Encoding.UTF8.GetBytes(encryptString);
            byte[] encryptedData = rijndaelEncrypt.TransformFinalBlock(inputData, 0, inputData.Length);

            return Convert.ToBase64String(encryptedData);
        }

        /// <summary>
        /// �ԳƼ����㷨AES RijndaelManaged�����ַ���
        /// </summary>
        /// <param name="decryptString">�����ܵ��ַ���</param>
        /// <returns>���ܳɹ����ؽ��ܺ���ַ���,ʧ�ܷ�Դ��</returns>
        public static string AES_Decrypt(string decryptString)
        {
            return AES_Decrypt(decryptString, Default_AES_Key);
        }

        /// <summary>
        /// �ԳƼ����㷨AES RijndaelManaged�����ַ���
        /// </summary>
        /// <param name="decryptString">�����ܵ��ַ���</param>
        /// <param name="decryptKey">������Կ,�ͼ�����Կ��ͬ</param>
        /// <returns>���ܳɹ����ؽ��ܺ���ַ���,ʧ�ܷ��ؿ�</returns>
        public static string AES_Decrypt(string decryptString, string decryptKey)
        {
            try
            {
                decryptKey = GetSubString(decryptKey, 32, "");
                decryptKey = decryptKey.PadRight(32, ' ');

                RijndaelManaged rijndaelProvider = new RijndaelManaged();
                rijndaelProvider.Key = Encoding.UTF8.GetBytes(decryptKey);
                rijndaelProvider.IV = Keys;
                ICryptoTransform rijndaelDecrypt = rijndaelProvider.CreateDecryptor();

                byte[] inputData = Convert.FromBase64String(decryptString);
                byte[] decryptedData = rijndaelDecrypt.TransformFinalBlock(inputData, 0, inputData.Length);

                return Encoding.UTF8.GetString(decryptedData);
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// ���ֽڳ���(���ֽ�,һ������Ϊ2���ֽ�)ȡ��ĳ�ַ�����һ����
        /// </summary>
        /// <param name="sourceString">Դ�ַ���</param>
        /// <param name="length">��ȡ�ַ����ֽڳ���</param>
        /// <param name="tailString">�����ַ���(���ַ���������ʱ��β������ӵ��ַ�����һ��Ϊ"...")</param>
        /// <returns>ĳ�ַ�����һ����</returns>
        private static string GetSubString(string sourceString, int length, string tailString)
        {
            return GetSubString(sourceString, 0, length, tailString);
        }

        /// <summary>
        /// ���ֽڳ���(���ֽ�,һ������Ϊ2���ֽ�)ȡ��ĳ�ַ�����һ����
        /// </summary>
        /// <param name="sourceString">Դ�ַ���</param>
        /// <param name="startIndex">����λ�ã���0��ʼ</param>
        /// <param name="length">��ȡ�ַ����ֽڳ���</param>
        /// <param name="tailString">�����ַ���(���ַ���������ʱ��β������ӵ��ַ�����һ��Ϊ"...")</param>
        /// <returns>ĳ�ַ�����һ����</returns>
        private static string GetSubString(string sourceString, int startIndex, int length, string tailString)
        {
            string myResult = sourceString;

            //�������Ļ���ʱ(ע:���ĵķ�Χ:\u4e00 - \u9fa5, ������\u0800 - \u4e00, ����Ϊ\xAC00-\xD7A3)
            if (System.Text.RegularExpressions.Regex.IsMatch(sourceString, "[\u0800-\u4e00]+") ||
                System.Text.RegularExpressions.Regex.IsMatch(sourceString, "[\xAC00-\xD7A3]+"))
            {
                //����ȡ����ʼλ�ó����ֶδ�����ʱ
                if (startIndex >= sourceString.Length)
                {
                    return string.Empty;
                }
                else
                {
                    return sourceString.Substring(startIndex,
                                                   ((length + startIndex) > sourceString.Length) ? (sourceString.Length - startIndex) : length);
                }
            }

            //�����ַ�����"�й�����abcd123"
            if (length <= 0)
            {
                return string.Empty;
            }
            byte[] bytesSource = Encoding.Default.GetBytes(sourceString);

            //���ַ������ȴ�����ʼλ��
            if (bytesSource.Length > startIndex)
            {
                int endIndex = bytesSource.Length;

                //��Ҫ��ȡ�ĳ������ַ�������Ч���ȷ�Χ��
                if (bytesSource.Length > (startIndex + length))
                {
                    endIndex = length + startIndex;
                }
                else
                {   //��������Ч��Χ��ʱ,ֻȡ���ַ����Ľ�β
                    length = bytesSource.Length - startIndex;
                    tailString = "";
                }

                int[] anResultFlag = new int[length];
                int nFlag = 0;
                //�ֽڴ���127Ϊ˫�ֽ��ַ�
                for (int i = startIndex; i < endIndex; i++)
                {
                    if (bytesSource[i] > 127)
                    {
                        nFlag++;
                        if (nFlag == 3)
                        {
                            nFlag = 1;
                        }
                    }
                    else
                    {
                        nFlag = 0;
                    }
                    anResultFlag[i] = nFlag;
                }
                //���һ���ֽ�Ϊ˫�ֽ��ַ���һ��
                if ((bytesSource[endIndex - 1] > 127) && (anResultFlag[length - 1] == 1))
                {
                    length = length + 1;
                }

                byte[] bsResult = new byte[length];
                Array.Copy(bytesSource, startIndex, bsResult, 0, length);
                myResult = Encoding.Default.GetString(bsResult);
                myResult = myResult + tailString;

                return myResult;
            }

            return string.Empty;

        }

        /// <summary>
        /// �����ļ���
        /// </summary>
        /// <param name="fs"></param>
        /// <returns></returns>
        public static CryptoStream AES_EncryptStrream(FileStream fs, string decryptKey)
        {
            decryptKey = GetSubString(decryptKey, 32, "");
            decryptKey = decryptKey.PadRight(32, ' ');

            RijndaelManaged rijndaelProvider = new RijndaelManaged();
            rijndaelProvider.Key = Encoding.UTF8.GetBytes(decryptKey);
            rijndaelProvider.IV = Keys;

            ICryptoTransform encrypto = rijndaelProvider.CreateEncryptor();
            CryptoStream cytptostreamEncr = new CryptoStream(fs, encrypto, CryptoStreamMode.Write);
            return cytptostreamEncr;
        }

        /// <summary>
        /// �����ļ���
        /// </summary>
        /// <param name="fs"></param>
        /// <returns></returns>
        public static CryptoStream AES_DecryptStream(FileStream fs, string decryptKey)
        {
            decryptKey = GetSubString(decryptKey, 32, "");
            decryptKey = decryptKey.PadRight(32, ' ');

            RijndaelManaged rijndaelProvider = new RijndaelManaged();
            rijndaelProvider.Key = Encoding.UTF8.GetBytes(decryptKey);
            rijndaelProvider.IV = Keys;
            ICryptoTransform Decrypto = rijndaelProvider.CreateDecryptor();
            CryptoStream cytptostreamDecr = new CryptoStream(fs, Decrypto, CryptoStreamMode.Read);
            return cytptostreamDecr;
        }

        /// <summary>
        /// ��ָ���ļ�����
        /// </summary>
        /// <param name="InputFile"></param>
        /// <param name="OutputFile"></param>
        /// <returns></returns>
        public static bool AES_EncryptFile(string InputFile, string OutputFile)
        {
            try
            {
                string decryptKey = "www.wquan.cn";

                FileStream fr = new FileStream(InputFile, FileMode.Open);
                FileStream fren = new FileStream(OutputFile, FileMode.Create);
                CryptoStream Enfr = AES_EncryptStrream(fren, decryptKey);
                byte[] bytearrayinput = new byte[fr.Length];
                fr.Read(bytearrayinput, 0, bytearrayinput.Length);
                Enfr.Write(bytearrayinput, 0, bytearrayinput.Length);
                Enfr.Close();
                fr.Close();
                fren.Close();
            }
            catch
            {
                //�ļ��쳣
                return false;
            }
            return true;
        }

        /// <summary>
        /// ��ָ�����ļ���ѹ��
        /// </summary>
        /// <param name="InputFile"></param>
        /// <param name="OutputFile"></param>
        /// <returns></returns>
        public static bool AES_DecryptFile(string InputFile, string OutputFile)
        {
            try
            {
                string decryptKey = "www.wquan.cn";
                FileStream fr = new FileStream(InputFile, FileMode.Open);
                FileStream frde = new FileStream(OutputFile, FileMode.Create);
                CryptoStream Defr = AES_DecryptStream(fr, decryptKey);
                byte[] bytearrayoutput = new byte[1024];
                int m_count = 0;

                do
                {
                    m_count = Defr.Read(bytearrayoutput, 0, bytearrayoutput.Length);
                    frde.Write(bytearrayoutput, 0, m_count);
                    if (m_count < bytearrayoutput.Length)
                        break;
                } while (true);

                Defr.Close();
                fr.Close();
                frde.Close();
            }
            catch
            {
                //�ļ��쳣
                return false;
            }
            return true;
        }
        
        #endregion


    }
}