string res = string.Empty;
Random r = new Random();

for (int i = 2; i < 30; i++)
{
    var list = new List<int>();
    for (int j = 0; j < i; j++)
    {
        list.Add(r.Next(0, 10000));
    }
    res += $"{string.Join(" ", list)}\n";
}
File.WriteAllText("out.txt", res);