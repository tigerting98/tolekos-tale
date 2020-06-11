
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonPointer : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] GameObject pointer;

    public void OnDeselect(BaseEventData eventData)
    {
        pointer.SetActive(false);
    }

    public void OnSelect(BaseEventData eventData)
    {
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
