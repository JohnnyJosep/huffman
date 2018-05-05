using System.Collections.Generic;
using System.Linq;

namespace Huffman.Model
{
    public class HuffmanTree
    {
        private readonly HuffmanHeap heap;
        public Dictionary<char, string> Code { get; private set; }

        public HuffmanTree(HuffmanHeap huffmanHeap)
        {
            heap = huffmanHeap;
            Code = new Dictionary<char, string>();
        }

        public void CreateCode()
        {
            while (heap.Size() > 1)
            {
                DeleteTwoAndInsertOne();
            }

            PreOrder(heap.GetMax());
        }

        private void DeleteTwoAndInsertOne()
        {
            var zero = heap.DeleteMax();
            var one = heap.DeleteMax();

            heap.Insert(new HuffmanNode
            {
                Character = null,
                Frequency = zero.Frequency + one.Frequency,
                ZeroChild = zero,
                OneChild = one
            });
        }

        public override string ToString() => string.Join("\n", Code.OrderBy(kv => kv.Value.Length).Select(kv => $"'{kv.Key.ToString().Replace("\n", "\\n").Replace("\r", "\\r") }'.-\t{kv.Value}"));

        private void PreOrder(HuffmanNode node, string prefix = "")
        {
            if (node != null)
            {
                if (node.Character.HasValue)
                {
                    Code.Add(node.Character.Value, prefix);
                }
                else
                {
                    PreOrder(node.ZeroChild, prefix + "0");
                    PreOrder(node.OneChild, prefix + "1");
                }
            }
        }
    }
}
