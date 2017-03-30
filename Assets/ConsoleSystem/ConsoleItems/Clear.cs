


namespace MC.CheatNs
{
    [CommandInfo("清除控制台当前所有文本信息")]
    public class Clear : ConsoleItem
    {

        public void SingleMethod()
        {
            UIConsoleSystem.GetInstance.ClearText();
        }

    }
}