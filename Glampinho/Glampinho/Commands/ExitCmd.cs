using System;


namespace Glampinho.Commands
{
   public class ExitCmd : ICmd
    {
        public readonly string Description;
        public ExitCmd(string desc) {
            Description=desc;
        }
        public override string ToString()
        {
            return Description;
        }
           

        public void ExecuteEnt()
        {
            Environment.Exit(0);
        }

        public void Execute(string con)
        {
            Environment.Exit(0);
        }
    }
}
