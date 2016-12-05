using RaeveSpil.Controller;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaeveSpil
{
    class Program
    {
        static void Main(string[] args)
        {
            CardController.imagePath = Environment.CurrentDirectory + @"\images\";

            var lines = File.ReadAllLines(CardController.imagePath + "Cards.txt", Encoding.UTF7);
            var cards = CardController.ParseCards(lines);
            Console.WriteLine("Read " + cards.Count + " cards.");
            CardController.CreateCardImages(cards);
            Console.WriteLine("Done generating images.");
            CardController.PrintCardImages(cards);
            Console.WriteLine("Done printing.");
            CardController.PrintCardPrintOuts(cards);
            Console.WriteLine("Done making printouts.");
            Console.Read();
        }
    }
}
