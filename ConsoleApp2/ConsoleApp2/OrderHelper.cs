using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2
{
    /// <summary>
    /// 订单助手
    /// </summary>
    public class OrderHelper
    {
        /// <summary>
        /// 防止创建类的实例
        /// </summary>
        private OrderHelper() { }

        private static readonly object locker = new object();
        private static int nownum = 0;

        /// <summary>
        /// 生成订单编号
        /// </summary>
        /// <returns></returns>
        public static string GenerateId()
        {
            lock (locker)  //lock 关键字可确保当一个线程位于代码的临界区时，另一个线程不会进入该临界区。
            {
                if (nownum == int.MaxValue)
                {
                    nownum = 0;
                }
                else
                {
                    nownum++;
                }

                return DateTime.Now.ToString("yyyyMMddHHmmss") + nownum.ToString().PadLeft(6, '0');
            }
        }
    }
}
