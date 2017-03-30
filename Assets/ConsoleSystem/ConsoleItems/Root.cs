

namespace MC.CheatNs
{
    [CommandInfo("权限命令，主要用于控制台权限变更等")]
    public class Root : ConsoleItem
    {
        [CommandInfo("获取权限")]
        public string Get(object passward)
        {
            int max = (int)EnumRootLevel.Max;
            for (int i = max; i > 0; i--)
            {
                Passward p = ConsoleSystemManager.GetInstance.GetRootLevelDetail((EnumRootLevel)i);
                if (p != null)
                {
                    if (p.CheckPassward(passward.ToString()))
                    {
                        ConsoleSystemManager.GetInstance.CurrenLevel = (EnumRootLevel)i;
                        UIConsoleSystem.GetInstance.RefreshTips();
                        return "权限变更为 <color=#00FF00>" + p.Name + "</color>";
                    }
                }
            }

            return "密码错误，权限变更失败！";
        }

    }
}