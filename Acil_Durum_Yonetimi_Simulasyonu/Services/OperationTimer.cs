using System;
using System.Diagnostics;

namespace Acil_Durum_Yonetimi_Simulasyonu.Services
{
    public class OperationTimer
    {
        public static string MeasureTime(Action action)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                action();
            }
            finally
            {
                stopwatch.Stop();
            }

            return $"Süre: {stopwatch.Elapsed.TotalMilliseconds} ms";
        }
        public static string MeasureMemory(Action action)
        {
            long memoryBefore = GC.GetTotalMemory(true);
            long memoryUsed = 0;

            try
            {
                action();
            }
            finally
            {
                GC.Collect();
                long memoryAfter = GC.GetTotalMemory(true);
                memoryUsed = memoryAfter - memoryBefore;
            }

            return $"Bellek Kullanımı: {memoryUsed / 1024.0:F2} KB";
        }
        public static string MeasureTimeAndMemory(Action action)
        {
            Stopwatch stopwatch = new Stopwatch();
            long memoryBefore = GC.GetTotalMemory(true);
            long memoryUsed = 0;
            stopwatch.Start();

            try
            {
                action();
            }
            finally
            {
                stopwatch.Stop();
                GC.Collect();
                long memoryAfter = GC.GetTotalMemory(true);
                memoryUsed = memoryAfter - memoryBefore;
            }

            return $"Süre: {stopwatch.Elapsed.TotalMilliseconds} ms, Bellek: {memoryUsed / 1024.0:F2} KB";
        }
    }
}
