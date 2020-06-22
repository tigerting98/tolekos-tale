using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTrigger<T> where T : Component
{
    // Start is called before the first frame update
    Func<T, bool> triggerCondition;
    public event Action<T> OnTriggerEvent;

    public ActionTrigger(Func<T, bool> triggerCondition) {

        this.triggerCondition = triggerCondition;

    }

    public void CheckTrigger(T trigger) {
        if (triggerCondition != null) {
            if (triggerCondition(trigger)) {
                OnTriggerEvent?.Invoke(trigger);
            }
        }
    }

    
}
