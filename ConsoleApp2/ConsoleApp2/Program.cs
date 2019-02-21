using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            AsyncGenOrderId("订单号AA", 100);
            AsyncGenOrderId("订单号BB", 100);
            AsyncGenOrderId("订单号CC", 100);

            //for (int i = 0; i < 1000; i++)
            //{
            //    Console.WriteLine(UniqueData.Gener("HD",2,1));
            //    Console.WriteLine(UniqueData.GenerLong("BD",2,1));
            //}
            Console.ReadKey();
        }

        private static object locker = new object();
        private static int nownum = 0;
        // <summary>
        // 批量生成订单
        // </summary>
        // <param name = "c" > 前缀 </ param >
        // < param name="nums">生成的订单数量</param>
        public static void GetOrderNumbers(string c, int nums)
        {
            for (int i = 0; i < nums; i++)
            {
                lock (locker)
                {
                    if (nownum == int.MaxValue)
                        nownum = 0;
                    else
                        nownum++;

                    string t = c + DateTime.Now.ToString("yyyyMMddHHmmss") + nownum.ToString().PadLeft(4, '0');
                    Console.WriteLine(t);
                }
            }
        }

        public static async void AsyncGenOrderId(string c, int nums)
        {
            await Task.Run(() => GetOrderNumbers(c, nums));
        }

        /// <summary>  
        /// 根据GUID获取16位的唯一字符串  
        /// </summary>  
        /// <param name="guid"></param>  
        /// <returns></returns>  
        public static string GuidTo16String()
        {
            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
                i *= ((int)b + 1);
            return string.Format("{0:x}", i - DateTime.Now.Ticks);
        }

        /// <summary>  
        /// 根据GUID获取19位的唯一数字序列  
        /// </summary>  
        /// <returns></returns>  
        public static long GuidToLongID()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            return BitConverter.ToInt64(buffer, 0);
        }
    }


}
