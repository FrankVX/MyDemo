

namespace MC.CheatNs
{
    public class CommandInfo : System.Attribute
    {

        private string _exp;
        public string Explain { get { return _exp; } }

        private EnumRootLevel _rootLevel;
        public bool CanExecute
        {
            get
            {
                return (int)ConsoleSystemManager.GetInstance.CurrenLevel >= (int)_rootLevel;
            }
        }

        public string LevelName { get { return ConsoleSystemManager.GetInstance.GetRootLevelDetail(_rootLevel).Name; } }

        public CommandInfo(string str, EnumRootLevel level = EnumRootLevel.Player)
        {
            _exp = str;
            _rootLevel = level;
        }
    }
}