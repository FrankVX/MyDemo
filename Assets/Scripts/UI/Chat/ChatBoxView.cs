using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Text;

public class ChatBoxView : GameBehaviour
{
    public Text text;
    public InputField input;
    StringBuilder sb = new StringBuilder();

    protected override void Awake()
    {
        base.Awake();
    }

    void ShowChat(string text)
    {
        sb.AppendLine(text);
        this.text.text = sb.ToString();
    }
}
