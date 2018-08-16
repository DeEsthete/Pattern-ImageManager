using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageManager
{
    public class ImageCommand : Command
    {
        public List<string> Commands { get; set; }

        public ImageCommand()
        {
            Commands = new List<string>();
        }
        public override void AddCommand(string str)
        {
            Commands.Add(str);
        }

        public override void ClearCommands()
        {
            Commands = null;
            Commands = new List<string>();
        }

        public override string GetCommand(int index)
        {
            return Commands[index];
        }

        public override List<string> GetCommands()
        {
            return Commands;
        }
    }
}
