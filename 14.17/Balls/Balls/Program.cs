char left = '<';
char right = '>';
int count = 0;
bool isOk = false;

var inp = File.ReadAllLines("input.txt");
var countBalls = int.Parse(inp[0]);
var row = inp[1].ToCharArray();

while (!isOk)
{
    isOk = true;
    for (int i = 0; i < countBalls - 1; i++)
    {
        if (row[i] == right&& row[i + 1] == left)
        {
            row[i] = left;
            row[i + 1] = right;
            count++;
            isOk = false;
        }
    }
}

File.WriteAllText("output.txt", count.ToString());