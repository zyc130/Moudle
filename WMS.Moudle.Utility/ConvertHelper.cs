namespace WMS.Moudle.Utility
{
    public class ConvertHelper
    {
        /// <summary>
        /// ToInt
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="Default">默认值</param>
        /// <returns></returns>
        public static int ToInt(object obj, int Default = 0)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return Default;
            }

            int result;
            try
            {
                result = Convert.ToInt32(obj);
            }
            catch
            {
                result = Default;
            }

            return result;
        }

        /// <summary>
        /// ToInt
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="Default">默认值</param>
        /// <returns></returns>
        public static long ToLong(object obj, long Default = 0)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return Default;
            }

            long result;
            try
            {
                result = Convert.ToInt64(obj);
            }
            catch
            {
                result = Default;
            }

            return result;
        }

        /// <summary>
        /// ToFloat
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="Default"></param>
        /// <returns></returns>
        public static float ToFloat(object obj, float Default = 0)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return Default;
            }

            float result;
            try
            {
                result = Convert.ToSingle(obj);
            }
            catch
            {
                result = Default;
            }

            return result;
        }

        /// <summary>
        /// ToDouble
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="Default"></param>
        /// <returns></returns>
        public static double ToDouble(object obj, double Default = 0)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return Default;
            }

            double result;
            try
            {
                result = Convert.ToDouble(obj);
            }
            catch
            {
                result = Default;
            }

            return result;
        }

        /// <summary>
        /// ToDecimal
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="Default"></param>
        /// <returns></returns>
        public static decimal ToDecimal(object obj, decimal Default = 0)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return Default;
            }

            decimal result;
            try
            {
                result = Convert.ToDecimal(obj);
            }
            catch
            {
                result = Default;
            }

            return result;
        }
    }
}
