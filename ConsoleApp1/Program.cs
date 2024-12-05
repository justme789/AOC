using System.Text.RegularExpressions;

public class Program
{
    private static string path = @"..\..\..\AOCIn\";
    public static void Main(string[] args)
    {
        Console.WriteLine("Day 3 Part A: ");
        Day3(true);
        Console.WriteLine("Day 3 Part B: ");
        Day3(false);
        Console.WriteLine("Day 4 Part A: ");
        Day4(true);
        Console.WriteLine("Day 4 Part B: ");
        Day4(false);
        Console.WriteLine("Day 5 Part A: ");
        Day5(true);
        Console.WriteLine("Day 5 Part B: ");
        Day5(false);

    }
    public static void Day5(bool partA)
    {
        List<string> lines = File.ReadAllLines(path + "Aoc5.txt").ToList();
        bool paging = false;
        int res = 0;
        Dictionary<int, List<int>> sequencce = new Dictionary<int, List<int>>();
        foreach (string line in lines)
        {
            if (line.Trim().Length == 0)
            {
                paging = true;
                continue;
            }
            if (!paging)
            {
                string[] xy = line.Split("|");
                int x = int.Parse(xy[0]);
                int y = int.Parse(xy[1]);
                if (sequencce.ContainsKey(y))
                {
                    sequencce[y].Add(x);
                }
                else
                {
                    sequencce.Add(y, new List<int> { x });
                }
            }
            else
            {
                string[] vals = line.Split(",");
                bool goodLine = true;
                for (int i = 0; i < vals.Length; i++)
                {
                    int val = int.Parse(vals[i]);
                    if (sequencce.ContainsKey(val))
                    {
                        List<int> prev = sequencce[val];
                        for (int j = i + 1; j < vals.Length; j++)
                        {
                            if (prev.Contains(int.Parse(vals[j])))
                            {
                                goodLine = false;
                                break;
                            }
                        }
                    }
                    if (!goodLine)
                    {
                        break;
                    }
                }
                if (partA)
                {
                    if (goodLine)
                    {
                        res += int.Parse(vals[vals.Length / 2]);
                    }
                }
                else
                {
                    if (goodLine)
                    {
                        continue;
                    }
                    goodLine = false;
                    while (!goodLine)
                    {
                        goodLine = true;
                        for (int i = 0; i < vals.Length; i++)
                        {
                            int val = int.Parse(vals[i]);
                            if (sequencce.ContainsKey(val))
                            {
                                List<int> prev = sequencce[val];
                                for (int j = i + 1; j < vals.Length; j++)
                                {
                                    int temp = int.Parse(vals[j]);
                                    if (prev.Contains(temp))
                                    {
                                        vals[i] = temp.ToString();
                                        vals[j] = val.ToString();
                                        goodLine = false;
                                        break;
                                    }
                                }
                            }
                            if (!goodLine)
                            {
                                break;
                            }
                        }
                        if (goodLine)
                        {
                            res += int.Parse(vals[vals.Length / 2]);
                        }
                    }
                }
            }
        }
        Console.WriteLine(res);
    }
    public static void Day4(bool partA)
    {
        List<string> s = File.ReadAllLines(path + "Aoc4.txt").ToList();
        int res = 0;
        int n = s.Count;
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (partA)
                {
                    if (s[i][j] == 'X')
                    {
                        res += IndexIterator(s, i, j, 0, 1);
                        res += IndexIterator(s, i, j, 0, -1);
                        res += IndexIterator(s, i, j, 1, 0);
                        res += IndexIterator(s, i, j, -1, 0);
                        res += IndexIterator(s, i, j, 1, 1);
                        res += IndexIterator(s, i, j, 1, -1);
                        res += IndexIterator(s, i, j, -1, 1);
                        res += IndexIterator(s, i, j, -1, -1);
                    }
                }
                else
                {
                    if (s[i][j] == 'A')
                    {
                        if (i + 1 < n && i - 1 >= 0 && j + 1 < n && j - 1 >= 0)
                        {
                            string d1 = s[i - 1][j - 1].ToString() + s[i][j] + s[i + 1][j + 1];
                            string d2 = s[i - 1][j + 1].ToString() + s[i][j] + s[i + 1][j - 1];
                            if ((d1 == "MAS" || d1 == "SAM")
                            && (d2 == "MAS" || d2 == "SAM"))
                            {
                                res++;
                            }
                        }
                    }
                }
            }
        }

        Console.WriteLine(res);
    }
    public static int IndexIterator(List<string> s, int i, int j, int dirI, int dirJ)
    {
        string temp = "";
        for (int adder = 0; adder < 4; adder++)
        {
            int iIndex = i + adder * dirI;
            int jIndex = j + adder * dirJ;
            if (iIndex >= 0 && iIndex < s.Count && jIndex >= 0 && jIndex < s.Count)
            {
                temp += s[iIndex][jIndex];
            }
            else { return 0; }
        }
        if (temp == "XMAS")
        {
            return 1;
        }
        return 0;
    }
    public static void Day3(bool partA)
    {
        string s = File.ReadAllText(path + "Aoc3.txt");
        Regex x = new Regex(@"mul\((\d+),(\d+)\)");
        MatchCollection matches = x.Matches(s);
        long res = 0;
        foreach (Match m in matches)
        {
            if (partA)
            {
                res += int.Parse(m.Groups.Values.ToList()[^1].Value) * int.Parse(m.Groups.Values.ToList()[^2].Value);
            }
            else
            {
                int matchIndex = m.Index;
                int lastDo = s.LastIndexOf("do()", matchIndex, matchIndex);
                int lastDont = s.LastIndexOf("don't()", matchIndex, matchIndex);
                if (lastDont == -1 || lastDont < lastDo)
                {
                    res += int.Parse(m.Groups.Values.ToList()[^1].Value) * int.Parse(m.Groups.Values.ToList()[^2].Value);
                }
            }

        }
        Console.WriteLine(res);

    }
}