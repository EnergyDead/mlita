using System;
using System.Collections.Generic;
using System.Text;

namespace _3_4
{
    public class Cache
    {
        private readonly ulong?[] _items;
        private int _maxX;
        private int _maxY;

        public Cache(int mX, int mY, int mZ)
        {
            _maxX = mX;
            _maxY = mY;
            _items = new ulong?[mX * mY * mZ];
        }

        public ulong? Get(int x, int y, int z)
        {
            var pos = GetPos(x, y, z);

            return _items[pos];
        }

        public void Set(int x, int y, int z, ulong? value)
        {
            var pos = GetPos(x, y, z);

            _items[pos] = value;
        }

        private int GetPos(int x, int y, int z)
        {
            return z * _maxY * _maxX + y * _maxX + x;
        }
    }
}
