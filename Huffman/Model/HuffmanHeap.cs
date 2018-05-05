namespace Huffman.Model
{
    public class HuffmanHeap
    {
        private HuffmanNode[] queue;
        private int n;

        public HuffmanHeap()
        {
            queue = new HuffmanNode[char.MaxValue];
            n = 0;
        }

        public bool IsEmpty() => n == 0;

        public int Size() => n;

        public void Insert(HuffmanNode node)
        {
            queue[++n] = node;
            Swim(n);
        }

        public HuffmanNode DeleteMax()
        {
            HuffmanNode max = queue[1];
            Exchange(1, n--);
            queue[n + 1] = null;
            Sink(1);
            return max;
        }

        public HuffmanNode GetMax()
        {
            return queue[1];
        }

        private void Swim(int k)
        {
            while (k > 1 && Cmp(k / 2, k))
            {
                Exchange(k / 2, k);
                k = k / 2;
            }
        }

        private void Sink(int k)
        {
            while (2 * k <= n)
            {
                int j = 2 * k;
                if (j < n && Cmp(j, j + 1)) j++;
                if (!Cmp(k, j)) break;
                Exchange(k, j);
                k = j;
            }
        }

        private bool Cmp(int i, int j) => queue[i].CompareTo(queue[j]) > 0;

        private void Exchange(int i, int j)
        {
            var temp = queue[i];
            queue[i] = queue[j];
            queue[j] = temp;
        }
    }
}
