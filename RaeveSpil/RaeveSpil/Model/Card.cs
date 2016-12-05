using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaeveSpil.Model
{
    public class Card
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string SubType { get; set; }
        public List<string> Text { get; set; }
        public bool IsMandatory { get; set; }
        public Image Image { get; set; }
        public bool IsAction ()
        {
            return Type.StartsWith("Handling");
        }
        public Card ()
        {
            Text = new List<string>();
        }
    }
}
