using pyramid;

var inp = File.ReadAllLines("input.txt");
int height = int.Parse(inp[0]);

if (height == 0)
{
    return;
}

var head = new Tree()
{
    Value = int.Parse(inp[1].Trim()),
};

Dictionary<(int, int), Tree> trees = new();
trees.Add((0, 0), head);

for (int i = 1; i < height; i++)
{
    var row = inp[i + 1].Split(" ").Select(x => int.Parse(x)).ToList();
    for (int j = 0; j < row.Count; j++)
    {
        var node = new Tree()
        {
            Value = row[j],
            H = i
        };

        if (trees.ContainsKey((i - 1, j - 1)))
        {
            trees[(i - 1, j - 1)].Right = node;
        }
        if (trees.ContainsKey((i - 1, j)))
        {
            trees[(i - 1, j)].Left = node;
        }

        trees.Add((i, j), node);
    }
}

List<int> path = new();
Dictionary<int, List<int>> paths = new Dictionary<int, List<int>>();
int temp = 0;

Sum(head);

void Sum(Tree node)
{
    checked
    {
        temp += node.Value;
    }
    path.Add(node.Value);
    if (node.Right == null || node.Left == null)
    {
        paths.TryAdd(temp, new List<int>(path));
        return;
    }
    Sum(node.Left!);
    temp -= node.Left.Value;
    path.RemoveAt(path.Count - 1);
    Sum(node.Right!);
    temp -= node.Right.Value;
    path.RemoveAt(path.Count - 1);
}
string result = paths.Keys.Max().ToString();
result += '\n';
result += string.Join(" ", paths[paths.Keys.Max()]);

File.WriteAllText("output.txt", result);