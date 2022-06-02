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
            Value = row[j]
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