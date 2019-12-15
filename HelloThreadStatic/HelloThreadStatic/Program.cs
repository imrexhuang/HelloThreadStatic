using System;
using System.Threading;

// 改寫自保哥上課範例
// C# 開發實戰：非同步程式開發技巧  https://www.accupass.com/event/1910240223481510631540

namespace HelloThreadStatic
{

    class Program
    {
        // counter 是一個共用資訊 不同執行緒的共用變數
        //private static int counter = 0;


        [ThreadStatic]
        private static int counter = 678;

        static object share = new object();

        public static void Main()
        {
            // 建立第一個執行緒，其會執行 非同步方法 方法
            Thread thread1 = new Thread(非同步方法C);

            // 建立第二個執行緒，其會執行 非同步方法 方法
            Thread thread2 = new Thread(非同步方法C);

            Thread thread3 = new Thread(非同步方法C);

            // 開啟啟動執行這兩個執行緒
            thread1.Start();
            thread2.Start();
            thread3.Start();

            // 等候這兩個執行緒結束執行，這個時候，主執行緒是在 封鎖 狀態下，也就是無法繼續執行任何程式碼
            thread1.Join();
            thread2.Join();
            thread3.Join();

            Console.WriteLine("已經處理完成...");
            Console.WriteLine("兩個執行緒聯合計算結果是:");
            Console.WriteLine(counter);

            Console.WriteLine("請按任一鍵，以結束執行");
            Console.ReadKey();
        }

        private static void 非同步方法A()
        {

            for (int index = 0; index < 10000000; index++)
            {
                // 底下指令會因為執行緒的 Context Switch，造成該命令尚未完全完成
                // 導致最後運算結果不正確
                counter++;
            }

        }

        private static void 非同步方法B()
        {
            lock (share)
            {
                for (int index = 0; index < 10000000; index++)
                {
                    counter++;
                }
            }

        }

        private static void 非同步方法C()
        {
            for (int index = 0; index < 10; index++)
            {
                Console.WriteLine("前->" + counter);
                counter++;
                Console.WriteLine("後->" + counter);

                Console.ReadKey();
                
            }
        }

    }
}
