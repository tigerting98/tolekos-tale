
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonPointer : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] GameObject pointer;
    [SerializeField] SFX buttonSFX;
    public void OnDeselect(BaseEventData eventData)
    {
        pointer.SetActive(false);
        
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (buttonSFX)
        {
            
           AudioManager.current.PlaySFX(buttonSFX);

        
        }
        pointer.SetActive(true);
    }

    // Start is called before the first frame update



    void Start()
    {
        pointer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
