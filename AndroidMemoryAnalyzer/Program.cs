using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using BigEndianExtension;
using AndroidMemoryAnalyzer.HeapAnalyzer;

namespace AndroidMemoryAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"F:\工作项目\2java内存提取与信息分析\com.tencent.mobileqq.hprof";
            AnalysisManager HeapAnalyzer = new AnalysisManager(path,null);
            HeapAnalyzer.DoWork();

            Console.ReadLine();
        }
    }
}
