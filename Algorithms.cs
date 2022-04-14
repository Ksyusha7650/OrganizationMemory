using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationMemory
{
    enum NAME_ALGORITHM
    {
        FIFO4, FIFO5
    }
   public struct Appeal
    {
        public int index;
        public int page;

       public Appeal(int index, int page)
        {
            this.index = index;
            this.page = page;
        }
    }

    public struct PageBlocks
    {
        public int index;
        public char data;

        public PageBlocks(int index, char data)
        {
            this.index = index;
            this.data = data;
        }
    }
    public struct Algorithms {
        public static List <Int32> amountInterraptions = new List <Int32>();

        public static void FIFO(List<Appeal> appeals, List<PageBlocks> blocks, int amountBlocks)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("FIFO " + amountBlocks + ": ");
            amountInterraptions.Add(0);
            Console.ResetColor();
            Console.Write(0 + ":     ");
            foreach (PageBlocks block in blocks)
            {
                Console.Write(block.data + " ");
            }
            Console.WriteLine();
            List<PageBlocks> blocksList = new List<PageBlocks>();
            blocksList.AddRange(blocks);
            PageBlocks blockEmpty = new PageBlocks();
            PageBlocks blockInterruption = new PageBlocks(0, 'X');
            foreach (Appeal appeal in appeals)
            {
                if (!blocksList.Any(x => x.data == appeal.page.ToString().First<Char>()))
                {
                    blocksList[0] = blockInterruption;
                    if ((appeal.index == 1) && (amountBlocks == 5)) blocksList.Add(new PageBlocks(0, Convert.ToChar(appeal.page.ToString())));
                    else Shift(ref blocksList, appeal.page);
                    amountInterraptions[amountInterraptions.Count - 1] += 1;
                }
                else blocksList[0] = blockEmpty;


                Console.ResetColor();
                Console.Write(appeal.index + "(" + appeal.page + "): ");
                if (blocksList.Any(x => x.data == 'X')) Console.ForegroundColor = ConsoleColor.Red;
                else Console.ResetColor();
                foreach (PageBlocks block in blocksList)
                {
                    if (block.Equals(blockEmpty)) Console.Write(" ");
                    Console.Write(block.data + " ");
                }
                Console.WriteLine();
            }
            Console.ResetColor();
            Console.WriteLine();
        }
        private static void Shift(ref List<PageBlocks> pageBlocks, int newData)
        {
            for (int i = 1; i < pageBlocks.Count - 1; i++)
            {
                int tempIndex = i + 1;
                PageBlocks currPB = pageBlocks[i];
                currPB.data = pageBlocks[tempIndex].data;
                currPB.index = i;
                pageBlocks[i] = currPB;

            }
            pageBlocks[pageBlocks.Count-1] = new PageBlocks(pageBlocks.Count-1, Convert.ToChar(newData.ToString()));
        }

       public static void BestAlgorithm()
        {
            Console.WriteLine(Environment.NewLine + "Наиболее оптимальный алгоритм: ");
            int indexAlgorithm =  amountInterraptions.FindIndex(x => x == amountInterraptions.Min());
            NAME_ALGORITHM name = (NAME_ALGORITHM)indexAlgorithm;
            Console.WriteLine(name.ToString() +": " + amountInterraptions.Min());
        }
    }
}
