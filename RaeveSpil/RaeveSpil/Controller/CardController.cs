using RaeveSpil.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace RaeveSpil.Controller
{
    public static class CardController
    {
        public static string imagePath = @"D:\Code\RaeveSpil\RaeveSpil\images\";
        public static List<Card> ParseCards(string[] lines)
        {
            var cards = new List<Card>();

            var i = 0;
            while (i < lines.Length)
            {
                var card = new Card();
                card.Type = lines[i++];
                if (lines[i] == "Påbudt")
                {
                    card.IsMandatory = true;
                    i++;
                }
                card.Name = lines[i++];
                while (i < lines.Length && !string.IsNullOrWhiteSpace(lines[i]))
                {
                    card.Text.Add(lines[i++]);
                }
                while(i < lines.Length && string.IsNullOrWhiteSpace(lines[i]))
                {
                    i++;
                }

                cards.Add(card);
            }

            return cards;
        }

        public static void CreateCardImage(Card card)
        {
            var type = card.Type.Split(' ')[0];
            var backgroundType = type;
            if (card.IsMandatory)
            {
                backgroundType = "Påbudt " + type;
            }
            // Background
            var backgroundImage = Bitmap.FromFile(imagePath + @"Templates\Baggrund " + backgroundType + ".png");
            card.Image = new Bitmap(backgroundImage.Width, backgroundImage.Height);
            var g = Graphics.FromImage(card.Image);
            g.DrawImage(backgroundImage, 0, 0);

            // Template
            var template = Bitmap.FromFile(imagePath + @"Templates\Template.png");
            g.DrawImage(template, 0, 0);

            // Picture
            var picturePath = imagePath + @"Pictures\" + card.Name + ".png";
            Image picture = null;
            if (File.Exists(picturePath))
            {
                picture = Bitmap.FromFile(picturePath);
            }
            else
            {
                picture = Bitmap.FromFile(imagePath + @"Pictures\Ukendt " + card.Type.Split(' ')[0] + ".png");
            }
            g.DrawImage(picture, 16, 45, 293, 192);

            // Name
            g.DrawString(
                card.Name,
                new Font(FontFamily.GenericSansSerif, 16),
                Brushes.Black,
                new Rectangle(10, 12, 304, 25));

            // Type
            g.DrawString(
                card.Type,
                new Font(FontFamily.GenericSansSerif, 14),
                Brushes.Black,
                new Rectangle(10, 244, 304, 26));

            // Text
            g.DrawString(
                string.Join("\n", card.Text), 
                new Font(FontFamily.GenericSansSerif, 14), 
                Brushes.Black, 
                new Rectangle(16, 276, 292, 157));
        }

        public static void CreateCardImages(List<Card> cards)
        {
            foreach(var card in cards)
            {
                CreateCardImage(card);
            }
        }

        public static void PrintCardImage(Card card)
        {
            if(card.Image != null)
            {
                card.Image.Save(imagePath + @"Output\" + card.Name + ".png");
            }
        }

        public static void PrintCardImages(List<Card> cards)
        {
            foreach(var card in cards)
            {
                PrintCardImage(card);
            }
        }

        public static void PrintCardPrintOuts(List<Card> cards)
        {
            var printOuts = new List<Image>();
            
            for(int i = 0; i<cards.Count; i++)
            {
                if(i % 9 == 0)
                {
                    var img = new Bitmap(cards[i].Image.Width * 3, cards[i].Image.Height * 3);
                    printOuts.Add(img);
                }
                var x = (i % 9) % 3;
                var y = (i % 9) / 3;
                Graphics.FromImage(printOuts.Last()).DrawImage(cards[i].Image, x * cards[i].Image.Width, y * cards[i].Image.Height);
            }

            for(var i = 0; i<printOuts.Count; i++)
            {
                printOuts[i].Save(imagePath + @"Prints\" + i + ".png");
            }
        }
    }
}
