using System;
using System.Text;
using System.Text.RegularExpressions;

namespace DDD.Utility
{
    /// <summary>
    /// ����ת��
    /// </summary>
    public class TypeParse
    {
        /// <summary>
        /// string��ת��Ϊbool��
        /// </summary>
        /// <param name="strValue">Ҫת�����ַ���</param>
        /// <param name="defValue">ȱʡֵ</param>
        /// <returns>ת�����bool���ͽ��</returns>
        public static bool StrToBool(object Expression, bool defValue)
        {
            if (Expression != null)
            {
                if (string.Compare(Expression.ToString(), "true", true) == 0)
                {
                    return true;
                }
                else if (string.Compare(Expression.ToString(), "false", true) == 0)
                {
                    return false;
                }
            }
            return defValue;
        }

        /// <summary>
        /// ������ת��ΪInt32����
        /// </summary>
        /// <param name="strValue">Ҫת�����ַ���</param>
        /// <param name="defValue">ȱʡֵ</param>
        /// <returns>ת�����int���ͽ��</returns>
        public static int StrToInt(object Expression, int defValue)
        {

            if (Expression != null)
            {
                string str = Expression.ToString();
                if (str.Length > 0 && str.Length <= 11 && Regex.IsMatch(str, @"^[-]?[0-9]*$"))
                {
                    if ((str.Length < 10) || (str.Length == 10 && str[0] == '1') || (str.Length == 11 && str[0] == '-' && str[1] == '1'))
                    {
                        return Convert.ToInt32(str);
                    }
                }
            }
            return defValue;
        }


        /// <summary>
        /// ������ת��ΪInt32����������
        /// </summary>
        /// <param name="Expression"></param>
        /// <param name="defValue"></param>
        /// <returns></returns>
        public static int StrToPosInt(object Expression, int defValue)
        {

            if (Expression != null)
            {
                string str = Expression.ToString();
                if (str.Length > 0 && str.Length <= 10 && Regex.IsMatch(str, @"^[0-9]*[1-9][0-9]*$"))
                {
                    if ((str.Length < 10) || (str.Length == 10 && str[0] == '1'))
                    {
                        return Convert.ToInt32(str);
                    }
                }
            }
            return defValue;
        }

        /// <summary>
        /// ������ת��ΪInt32�Ǹ���������
        /// </summary>
        /// <param name="Expression"></param>
        /// <param name="defValue"></param>
        /// <returns></returns>
        public static int StrTo0PosInt(object Expression, int defValue)
        {

            if (Expression != null)
            {
                string str = Expression.ToString();
                if (str.Length > 0 && str.Length <= 10 && Regex.IsMatch(str, @"^\d+$"))
                {
                    if ((str.Length < 10) || (str.Length == 10 && str[0] == '1'))
                    {
                        return Convert.ToInt32(str);
                    }
                }
            }
            return defValue;
        }
        
        /// <summary>
        /// ������ת��Ϊ�޷���byte����
        /// </summary>
        /// <param name="Expression"></param>
        /// <param name="defValue"></param>
        /// <returns></returns>
        public static byte StrToByte(object Expression, byte defValue)
        {

            if (Expression != null)
            {
                string str = Expression.ToString();
                if (str.Length > 0 && str.Length <= 3 && Regex.IsMatch(str, @"^\d+$"))
                {
                    int i = Convert.ToInt32(str);
                    if (i >= 0 && i <= 255) return Convert.ToByte(i);
                }
            }
            return defValue;
        }

        /// <summary>
        /// string��ת��Ϊfloat��
        /// </summary>
        /// <param name="strValue">Ҫת�����ַ���</param>
        /// <param name="defValue">ȱʡֵ</param>
        /// <returns>ת�����int���ͽ��</returns>
        public static float StrToFloat(object strValue, float defValue)
        {
            if ((strValue == null) || (strValue.ToString().Length > 10))
            {
                return defValue;
            }

            float intValue = defValue;
            if (strValue != null)
            {
                bool IsFloat = Regex.IsMatch(strValue.ToString(), @"^([-]|[0-9])[0-9]*(\.\w*)?$");
                if (IsFloat)
                {
                    intValue = Convert.ToSingle(strValue);
                }
            }
            return intValue;
        }

        public static bool? StrToBool(object strValue, bool? defValue)
        {
            if (strValue == null) return defValue;
            bool re;
            if (bool.TryParse(strValue.ToString(), out re)) return re;
            else return defValue;
        }
    
    }
}
