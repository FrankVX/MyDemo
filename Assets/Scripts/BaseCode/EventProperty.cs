using System;
using System.Collections.Generic;
using UnityEngine;

public class EventProperty
{
    public event System.Action<object> OnValueChange;

    public EventProperty() { }

    public EventProperty(object value)
    {
        this.value = value;
    }
    public object Value
    {
        get
        {
            return value;
        }
        set
        {
            if (!this.value.Equals(value))
            {
                this.value = value;
            }
        }
    }

    protected object value;

    void ValueChange()
    {
        if (OnValueChange != null)
            OnValueChange(value);
    }
}

public class EventProperty<T> : EventProperty
{
    public new event System.Action<T> OnValueChange;

    public EventProperty(T value) : base(value) { }

    public new T Value
    {
        get
        {
            return (T)value;
        }
        set
        {
            if (!this.value.Equals(value))
            {
                this.value = value;
                ValueChange();
            }
        }
    }

    void ValueChange()
    {
        if (OnValueChange != null)
            OnValueChange((T)value);
    }
}


