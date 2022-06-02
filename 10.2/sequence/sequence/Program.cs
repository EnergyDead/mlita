var inFile = "input.txt";
var outFile = "output.txt";
ulong res = 0;
ulong count = 0;

var inp = File.ReadAllLines(inFile);
count = ulong.Parse(inp[1]);
var row = new List<ulong>();
row.AddRange(inp[0].Split(" ").Select(x => ulong.Parse(x)).ToList());

List<int> mas = new();
Dictionary<int, int> mem = new();
int it;
int base1 = -1;   // first period first element index
int perLen = -1; // period length
for (int i = 1; ; i++)
{
    mas[i] = (mas[i - 1] * mas[i - 1]) % 1111111111;
    it = mem.ContainsKey(mas[i]);
    if (it != mem.Last().Value)
    {
        base1 = it->second;
        perLen = i - base1;
        break;
    }
    else
        mem[mas[i]] = i;
}
if (n <= base)
    cout << mas[n];
else
{
    int index = base + (n - base) % perLen;
    cout << mas[index];
}

File.WriteAllText(outFile, res.ToString());