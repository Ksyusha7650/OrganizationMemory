using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationMemory
{
    enum NAME_ALGORITHM
    {
        FIFO4, FIFO5, LRU, SECOND_CHANCE
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

    public struct PageBlockWithFlags
    {
        public PageBlocks block;
        public bool flag = false;

        public PageBlockWithFlags(PageBlocks block, bool flag)
        {
            this.block = block;
            this.flag = flag;
        }
    }
    public struct Algorithms
    {
        public static List<Int32> amountInterraptions = new List<Int32>();

        private static void OutputResult(Appeal appeal, List<PageBlocks> blocksList)
        {
            Console.ResetColor();
            Console.Write(appeal.index + "(" + appeal.page + "): ");
            if (blocksList.Any(x => x.data == 'X')) Console.ForegroundColor = ConsoleColor.Red;
            else Console.ResetColor();
            foreach (PageBlocks block in blocksList)
            {
                if (block.Equals(new PageBlocks())) Console.Write(" ");
                Console.Write(block.data + " ");
            }
            Console.ResetColor();
            Console.WriteLine();
        }
        private static void OutputResult(Appeal appeal, List<PageBlockWithFlags> blocksList)
        {
            Console.ResetColor();
            Console.Write(appeal.index + "(" + appeal.page + "): ");
            if (blocksList.Any(x => x.block.data == 'X')) Console.ForegroundColor = ConsoleColor.Red;
            else Console.ResetColor();
            foreach (PageBlockWithFlags block in blocksList)
            {
                if (block.Equals(new PageBlockWithFlags())) Console.Write(" ");
                Console.Write(block.block.data + " ");
            }
            Console.WriteLine();
            Console.ResetColor();
        }
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
                OutputResult(appeal, blocksList);
            }
            Console.WriteLine("Количество прерываний: " + amountInterraptions[amountInterraptions.Count - 1]+ Environment.NewLine);
        }

        public static void LRU(List<Appeal> appeals, List<PageBlocks> blocks, int amountBlocks)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("LRU " + amountBlocks + ": ");
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
                else
                {
                    blocksList[0] = blockEmpty;
                    Shift(ref blocksList, appeal.page);
                }
                OutputResult(appeal, blocksList);
            }
            Console.WriteLine("Количество прерываний: " + amountInterraptions[amountInterraptions.Count - 1] + Environment.NewLine);
        }

        public static void SecondChance(List<Appeal> appeals, List<PageBlocks> blocks, int amountBlocks)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Второй шанс " + amountBlocks + ": ");
            amountInterraptions.Add(0);
            Console.ResetColor();
            Console.Write(0 + ":     ");
            foreach (PageBlocks block in blocks)
            {
                Console.Write(block.data + " ");
            }
            Console.WriteLine();
            List<PageBlockWithFlags> blockWithFlags = new List<PageBlockWithFlags>();
            foreach (PageBlocks block in blocks)
            {
                blockWithFlags.Add(new PageBlockWithFlags(block, false));
            }
            PageBlockWithFlags blockInterruptionFlag = new PageBlockWithFlags(new PageBlocks(0, 'X'), false);
            PageBlockWithFlags empty = new PageBlockWithFlags();
            foreach (Appeal appeal in appeals)
            {
                if (!blockWithFlags.Any(x => x.block.data == appeal.page.ToString().First<Char>()))
                {
                    blockWithFlags[0] = blockInterruptionFlag;
                    if ((appeal.index == 1) && (amountBlocks == 5)) blockWithFlags.
                            Add(new PageBlockWithFlags(new PageBlocks(0, Convert.ToChar(appeal.page.ToString())), false));
                    else
                    {
                        Shift(ref blockWithFlags, appeal.page);
                    }
                    amountInterraptions[amountInterraptions.Count - 1] += 1;
                }
                else
                {
                    blockWithFlags[0] = empty;
                    Shift(ref blockWithFlags, appeal.page);
                    PageBlockWithFlags tempBlock = blockWithFlags[blockWithFlags.Count - 1];
                    tempBlock.flag = true;
                    blockWithFlags[blockWithFlags.Count - 1] = tempBlock;
                }
                OutputResult(appeal, blockWithFlags);
            }
            Console.WriteLine("Количество прерываний: " + amountInterraptions[amountInterraptions.Count - 1] + Environment.NewLine);
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
            pageBlocks[pageBlocks.Count - 1] = new PageBlocks(pageBlocks.Count - 1, Convert.ToChar(newData.ToString()));
        }
        private static void Shift(ref List<PageBlockWithFlags> pageBlocks, int newData)
        {
            PageBlockWithFlags firstPB = pageBlocks[1];
            if (firstPB.flag)
            {
                firstPB.flag = false;
                pageBlocks[1] = firstPB;
                Shift(ref pageBlocks, Int32.Parse(firstPB.block.data.ToString()));
            }
            for (int i = 1; i < pageBlocks.Count - 1; i++)
            {
                PageBlockWithFlags currPB = pageBlocks[i];
                int tempIndex = i + 1;
                currPB.block.data = pageBlocks[tempIndex].block.data;
                currPB.block.index = i;
                currPB.flag = pageBlocks[tempIndex].flag;
                pageBlocks[i] = currPB;
            }
            pageBlocks[pageBlocks.Count - 1] = new PageBlockWithFlags
                (new PageBlocks(pageBlocks.Count - 1, Convert.ToChar(newData.ToString())), false);
        }
        public static void BestAlgorithm()
        {
            Console.WriteLine(Environment.NewLine + "Наиболее оптимальный алгоритм: ");
            int indexAlgorithm = amountInterraptions.FindIndex(x => x == amountInterraptions.Min());
            NAME_ALGORITHM name = (NAME_ALGORITHM)indexAlgorithm;
            Console.WriteLine(name.ToString() + " количество прерываний: " + amountInterraptions.Min());
        }
    }
}
