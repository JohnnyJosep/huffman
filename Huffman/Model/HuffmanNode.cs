using System;

namespace Huffman.Model
{
    public class HuffmanNode : IComparable<HuffmanNode>
    {
        public int Frequency { get; set; }
        public char? Character { get; set; }

        public HuffmanNode ZeroChild { get; set; }
        public HuffmanNode OneChild { get; set; }

        public int CompareTo(HuffmanNode other)
        {
            return Frequency.CompareTo(other.Frequency);
        }

        public override string ToString() => $"'{Character}'.-\t{Frequency}";
    }
}
