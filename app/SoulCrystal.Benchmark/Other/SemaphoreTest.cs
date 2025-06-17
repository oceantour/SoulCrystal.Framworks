namespace SoulCrystal.Other
{
    public class SemaphoreTest
    {
        private static SemaphoreSlim semaphore;
        // A padding interval to make the output more orderly.
        private static int padding;

        /// <summary> 异步任务测试 </summary>
        public void SemaphorePro()
        {
            // 创建信号量对象 为阻塞状态
            semaphore = new SemaphoreSlim(0, 1);
            Console.WriteLine("{0} 任务可进入信号量对象.", semaphore.CurrentCount);
            Task[] tasks = new Task[5];

            // 创建并启动五个任务
            for (int i = 0; i <= 4; i++)
            {
                tasks[i] = Task.Run(() =>
                {
                    // 每个任务都从请求信号量开始
                    Console.WriteLine("任务 {0} 开始并等待信号量", Task.CurrentId);

                    int semaphoreCount;
                    semaphore.Wait(); // 开始等待
                    try
                    {
                        Interlocked.Add(ref padding, 100);

                        Console.WriteLine("任务 {0} 进入信号量.", Task.CurrentId);

                        // 任务休眠一秒后执行.
                        Thread.Sleep(1000 + padding);
                    }
                    finally
                    {
                        semaphoreCount = semaphore.Release(); // 释放信号量
                    }
                    Console.WriteLine("任务 {0} 释放信号量; 上一个计数值: {1}.", Task.CurrentId, semaphoreCount);
                });
            }

            // 等待半秒后让所有的任务开始并阻塞
            Thread.Sleep(500);

            semaphore.Release(1); // 释放信号量，让任务开始
            Console.WriteLine("{0} 任务可以进入信号量.", semaphore.CurrentCount);
            // 等待主线程完成任务
            Task.WaitAll(tasks);

            Console.WriteLine("主线程结束");
        }
    }
}
