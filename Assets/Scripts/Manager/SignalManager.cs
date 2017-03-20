using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class SignalManager : Singleton<SignalManager>
{
    Dictionary<Type, SignalBase> signals = new Dictionary<Type, SignalBase>();

    public override void Init()
    {
        base.Init();
    }


    public new T GetSignal<T>() where T : SignalBase
    {
        var type = typeof(T);
        if(signals.ContainsKey(type))
        {
            return signals[type] as T;
        }
        else
        {
            var signal = Activator.CreateInstance(type) as T;
            signals[type] = signal;
            return signal;
        }
    }
}
