using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5LABASIAKOD
{
    class Program
    {
        static int[] GetPrefix(string s)
        {
            int[] result = new int[s.Length];
            result[0] = 0;
            int index = 0;

            for (int i = 1; i < s.Length; i++)
            {
                while (index >= 0 && s[index] != s[i]) { index--; }
                index++;
                result[i] = index;
            }
            return result;
        }

        static int FindSubstring(string pattern, string text)
        {
            int[] pf = GetPrefix(pattern);
            int index = 0;

            for (int i = 0; i < text.Length; i++)
            {
                while (index > 0 && pattern[index] != text[i]) { index = pf[index - 1]; }
                if (pattern[index] == text[i]) index++;
                if (index == pattern.Length)
                {
                    Console.Write($"Образ--{pattern}--найден в строке под индексом: ");
                    return i - index + 1;//индекс в котором был найден образ в строке
                }
            }
            return -1;
        }
        public static int[] SearchString(string str, string pat)
        {
            List<int> retVal = new List<int>();
            int m = pat.Length;
            int n = str.Length;

            int[] badChar = new int[256];

            BadCharHeuristic(pat, m, ref badChar);

            int s = 0;
            while (s <= (n - m))
            {
                int j = m - 1;

                while (j >= 0 && pat[j] == str[s + j])
                    --j;

                if (j < 0)
                {
                    retVal.Add(s);
                    s += (s + m < n) ? m - badChar[str[s + m]] : 1;
                }
                else
                {
                    s += Math.Max(1, j - badChar[str[s + j]]);
                }
            }
            return retVal.ToArray();
        }

        private static void BadCharHeuristic(string str, int size, ref int[] badChar)
        {
            int i;

            for (i = 0; i < 256; i++)
                badChar[i] = -1;

            for (i = 0; i < size; i++)
                badChar[(int)str[i]] = i;
        }

        static void Main(string[] args)
        {
            string mas = "Москва город большой";
            string sym = "город";
            Console.WriteLine("Прямой поиск:");
            Console.WriteLine(Line2(mas, sym));
            Console.WriteLine();

            Console.WriteLine("Алгоритм Кнута — Морриса — Пратта:");
            Console.WriteLine();
            Console.WriteLine(FindSubstring(sym, mas));
            Console.WriteLine();

            Console.WriteLine("Алгоритм Боеура и Мура: ");
            string data = "word hello word";
            int[] value = SearchString(data, "word");
            foreach (var item in value)
            {
                Console.Write("образ--word--найден в строке: ");
                Console.WriteLine(item + " ");
            }
        }
        static string Line2(string str, string img)
        {
            List<int> list = new List<int>();

            int i = -1;
            int j;
            do
            {
                i++;
                j = 0;
                while (j < img.Length && str[i + j] == img[j])
                {
                    j++;
                    list.Add(i);
                }
            } while ((j != img.Length) && (i < str.Length - img.Length));
            string stroka = $"Образ--{img}--найден в строке под индексом: {list[0]}";
            return stroka;
        }



    }
}