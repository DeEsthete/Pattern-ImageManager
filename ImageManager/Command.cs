using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageManager
{
    public abstract class Command
    {
        public abstract List<string> GetCommands();
        public abstract string GetCommand(int index);
        public abstract void AddCommand(string str);
        public abstract void ClearCommands();
    }
}
