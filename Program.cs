using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OrganizationMemory
{
    class Program
    {
        public static List<Appeal> appeals = new List<Appeal>();
        public static List<PageBlocks> pageBlocks = new List<PageBlocks>();
        public static void Main()
        {
            FileWork.ReadFile(ref appeals);
            FileWork.SetBlocks(ref pageBlocks);
            Algorithms.FIFO4(appeals, pageBlocks);
            Algorithms.FIFO5(appeals, pageBlocks);
            Algorithms.BestAlgorithm();
        }
    }
}

