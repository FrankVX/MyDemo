
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MC.CheatNs
{
    public class UIConsoleSystem : MonoBehaviour, UnityEngine.EventSystems.IPointerClickHandler
    {

        public static UIConsoleSystem _instance;
        public static UIConsoleSystem GetInstance
        {
            get
            {
                if (!_instance)
                {
                    GameObject o = Resources.Load<GameObject>("[ConsoleSystem]");
                    o = GameObject.Instantiate<GameObject>(o);
                    _instance = o.GetComponent<UIConsoleSystem>();
                    o.SetActive(false);
                }
                return _instance;
            }
        }

        private void Awake()
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            gameObject.SetActive(false);
        }

        [SerializeField]
        private Text _targetText;
        [SerializeField]
        private Text _tipsText;
        [SerializeField]
        private UI.UIScrollViewText _contentText;
        [SerializeField]
        private UI.UIScrollViewText logContent;
        [SerializeField]
        private InputField _input;

        private List<string> _commandTenpList = new List<string>();

        public bool isShowLog = true;

        void Start()
        {
            RefreshTips();
            _contentText.text = "";
            Application.logMessageReceived += OnLogEvent;
            ConsoleSystemManager.GetInstance.OnTargetChange += () => { RefreshTips(); };

            _input.onEndEdit.AddListener((command) =>
            {
                if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
                {
                    _input.text = "";
                    //if (!_return) return;

                    if (string.IsNullOrEmpty(command)) return;

                    if (_commandTenpList.Count < 1 || command != _commandTenpList[_commandTenpList.Count - 1])
                    {
                        _commandTenpList.Add(command);
                        //仅记录三十条
                        if (_commandTenpList.Count > 30)
                            _commandTenpList.RemoveAt(0);
                    }

                    _historyCmdIndex = _commandTenpList.Count - 1;

                    string res = ConsoleSystemManager.GetInstance.RunCommand(command);
                    //若是密码，这儿的回显也处理一下
                    if (_input.contentType == InputField.ContentType.Password)
                        command = new string('*', command.Length);
                    if (!string.IsNullOrEmpty(res))
                        _contentText.text = _contentText.text + "\n->" + command + "\n->" + res;

                    //while (_contentText.preferredHeight > _contentText.rectTransform.sizeDelta.y)
                    //{
                    //    _contentText.text = _contentText.text.Remove(0, _contentText.text.Length > 10 ? 10 : _contentText.text.Length);
                    //}

                    _input.ActivateInputField();

                    //_return = false;
                }
            });
        }
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        private void OnLogEvent(string condition, string stackTrace, LogType type)
        {
            if (!isShowLog) return;
            sb.Length = 0;
            sb.Append(string.Concat(type.ToString(), "     "));
            sb.AppendLine(condition);
            sb.AppendLine(stackTrace);
            logContent.AddText(sb.ToString());
        }

        //private bool _return = false;
        private int _historyCmdIndex = -1;
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (_commandTenpList.Count < 1) return;

                _historyCmdIndex--;
                if (_historyCmdIndex < 0)
                    _historyCmdIndex = 0;
                _input.text = _commandTenpList[_historyCmdIndex];
                _input.selectionFocusPosition = _input.text.Length;
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (_commandTenpList.Count < 1) return;
                _historyCmdIndex++;
                if (_historyCmdIndex >= _commandTenpList.Count)
                    _historyCmdIndex = _commandTenpList.Count - 1;
                _input.text = _commandTenpList[_historyCmdIndex];
                _input.selectionFocusPosition = _input.text.Length;
            }

            if (Input.GetMouseButtonDown(0))
                ConsoleSystemManager.GetInstance.RayCheckTarget();
            //if (Input.GetKeyDown(KeyCode.Return))
            //    _return = true;
        }

        public void Active()
        {
            ConsoleSystemManager.GetInstance.ResetTarget();
            RefreshTips();
            //_input.text = "";
            _input.ActivateInputField();
            gameObject.SetActive(!gameObject.activeSelf);

        }

        public void RefreshTips()
        {
            _targetText.text = "当前目标：" + (ConsoleSystemManager.GetInstance.Target == null ? "无" : ConsoleSystemManager.GetInstance.Target.Name);
            _tipsText.text = ConsoleSystemManager.GetInstance.GetCheatCommandTips();
        }

        public void ClearText()
        {
            _contentText.text = "";
        }

        public string GetText()
        {
            return _contentText.text;
        }

        public void Passward(bool p)
        {
            _input.contentType = p ? InputField.ContentType.Password : InputField.ContentType.Standard;
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            _input.ActivateInputField();
        }
    }
}