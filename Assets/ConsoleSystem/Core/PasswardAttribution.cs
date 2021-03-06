


using System;
using System.Security.Cryptography;

namespace MC.CheatNs
{
    public class Passward : System.Attribute
    {

        private string _name;
        private string _passward;
        public bool CheckPassward(string clearPassward)
        {
            if (string.IsNullOrEmpty(_passward)) return true;

            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] res = md5.ComputeHash(System.Text.Encoding.Default.GetBytes(clearPassward));
            //UnityEngine.Debug.Log(_passward + "    " + BitConverter.ToString(res).Replace("-", ""));
            return _passward == BitConverter.ToString(res).Replace("-", "");
        }

        public string Name { get { return _name; } }

        /// <summary>
        /// 传入密码为MD值
        /// </summary>
        /// <param name="pass"></param>
        public Passward(string pass, string name)
        {
            _passward = pass;
            _name = name;
        }
    }
}