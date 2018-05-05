using Huffman.Model;
using Microsoft.Win32;
using System;
using System.Windows;

namespace Huffman
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string FileName;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Buscar_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                FileName = openFileDialog.FileName;
                File.Text = FileName;
                Comprimir.IsEnabled = true;
            }
        }

        private void Comprimir_Click(object sender, RoutedEventArgs e)
        {
            var text = System.IO.File.ReadAllText(FileName);
            int characters = 0;


            var freq = new HuffmanNode[char.MaxValue];
            foreach (char c in text)
            {
                if (freq[c] == null)
                {
                    characters++;
                    freq[c] = new HuffmanNode { Character = c, Frequency = 1 };
                }
                else
                {
                    freq[c].Frequency++;
                }
            }

            var heap = new HuffmanHeap();
            foreach (HuffmanNode node in freq)
            {
                if (node != null)
                {
                    heap.Insert(node);
                }
            }

            var tree = new HuffmanTree(heap);
            tree.CreateCode();

            int bits = (int)Math.Floor(Math.Log(characters, 2)) + 1;
            int original = bits * text.Length;

            Original.Text = $"{original} bits";

            int huff = 0;
            foreach (HuffmanNode node in freq)
            {
                if (node != null)
                {
                    huff += node.Frequency * tree.Code[node.Character.Value].Length;
                }
            }

            Huffman.Text = $"{huff} bits";

            double percent = 100.0 - ((double)huff / original * 100);
            Percent.Text = $"{percent}%";

            Code.Text = tree.ToString();
        }
    }
}
