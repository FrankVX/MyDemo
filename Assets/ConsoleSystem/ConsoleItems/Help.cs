//创建作者：Wangjiaying



using UnityEngine;

namespace MC.CheatNs
{
    [CommandInfo("帮助命令，主要用于命令帮助等信息")]
    public class Help : ConsoleItem
    {
        [CommandInfo("显示指定命令的详细信息")]
        public string Detail(object cmdName)
        {
            return "\n" + ConsoleSystemManager.GetInstance.GetTargetCommandHelp(cmdName.ToString());
        }

        [CommandInfo("显示所有可用命令列表及解释")]
        public string ShowAllCommand()
        {
            return "\n" + ConsoleSystemManager.GetInstance.GetCommandList();
        }
        [CommandInfo("Log")]
        public void Log(string text)
        {
            Debug.Log(text);
        }

    }
}