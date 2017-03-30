


using System.Reflection;

namespace MC.CheatNs
{
    public class ConsoleItem
    {

        private string _commandList;

        public ConsoleItem()
        {


            MethodInfo[] mths = this.GetType().GetMethods();
            for (int i = 0; i < mths.Length; i++)
            {
                CommandInfo exp = (CommandInfo)System.Attribute.GetCustomAttribute(mths[i], typeof(CommandInfo));
                if (exp != null)
                    _commandList += "<color=#00FF00>" + mths[i].Name + "</color> : " + exp.Explain + " \n";
            }
        }

        public string GetCommandList()
        {
            return _commandList;
        }
    }
}