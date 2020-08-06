
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//This sets the behavior for the UI button pointer
public class ButtonPointer : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] GameObject pointer;
    [SerializeField] SFX buttonSFX;
    [SerializeField] GameObject onclickparticle;
    [SerializeField] float size = 2;
    GameObject sys;
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

    private void OnEnable()
    {
        if (sys) {
            Destroy(sys);
        }
    
    }

    void Start()
    {
        pointer.SetActive(false);
        if (onclickparticle)
        {
            GetComponent<Button>().onClick.AddListener(SpawnParticle);
        }

    }
    void SpawnParticle() {

        if (onclickparticle)
        {
            sys = Instantiate(onclickparticle, pointer.transform.position+ new Vector3(size*0.5f, 0,0), Quaternion.identity,this.transform);
            ParticleSystem.MainModule partmain = sys.GetComponent<ParticleSystem>().main;
            partmain.startSizeX = size;
            partmain.startSizeY = size;
            partmain.startSpeed = 10*size;
            Destroy(sys, 2f);

        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
