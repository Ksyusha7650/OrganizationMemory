using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationMemory
{
    public static class FileWork
    {

        private const string pathApp = "../AppealsFile.txt";
        private const string pathPB = "../PageBlocksFile.txt";

        public static void ReadFile(ref List<Appeal> appeals)
        {
            using (StreamReader sr = File.OpenText(pathApp))
            {
                string line = "";
                int currIndex = 0; int currPage = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] nums = line.Split('-');
                    currIndex = Int32.Parse(nums[0]);
                    currPage = Int32.Parse(nums[1]);

                    appeals.Add(new Appeal(currIndex, currPage));
                }
            }
            Console.WriteLine("Обращения: ");
            foreach (Appeal appeal in appeals)
            {
                Console.WriteLine(appeal.index + "-" + appeal.page);
            }
            Console.WriteLine();
        }


        public static void SetBlocks(ref List<PageBlocks> pageBlocks)
        {
            pageBlocks.Add(new PageBlocks());
            using (StreamReader sr = File.OpenText(pathPB))
            {
                string line = "";
                int currIndex = 0; char currData;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] nums = line.Split(':');
                    currIndex = Int32.Parse(nums[0]);
                    currData = nums[1].First<Char>();
                    pageBlocks.Add(new PageBlocks(currIndex, currData));
                }
            }
            Console.WriteLine("Страничные блоки: ");
            var outputPB = pageBlocks.Where(x => x.index >= 1);
            foreach (PageBlocks page in outputPB)
            {
                Console.WriteLine(page.index + "-" + page.data);
            }
            Console.WriteLine();
        }
    }
}
