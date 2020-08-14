using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class encompasses the action trigger where by a function is called when a certain condition is fulfilled
public class ActionTrigger<T> where T : Component
{
    // Start is called before the first frame update
    Func<T, bool> triggerCondition;
    public event Action<T> OnTriggerEvent;

    public ActionTrigger(Func<T, bool> triggerCondition) {

        this.triggerCondition = triggerCondition;

    }
    //Check if the condition is fulfilled
    public void CheckTrigger(T trigger) {
        if (triggerCondition != null) {
            if (triggerCondition(trigger)) {
                OnTriggerEvent?.Invoke(trigger);
            }
        }
    }

    
}
