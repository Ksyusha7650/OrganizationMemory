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
            Algorithms.FIFO(appeals, pageBlocks,4);
            Algorithms.FIFO(appeals, pageBlocks,5);
            Algorithms.LRU(appeals, pageBlocks, 4);
            Algorithms.SecondChance(appeals, pageBlocks, 4);
            Algorithms.BestAlgorithm();
        }
    }
}

