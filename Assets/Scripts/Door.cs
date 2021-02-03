using UnityEngine;

public class Door : MonoBehaviour {
    public bool opened = false;
    Animator anim;
    bool entered = false;

    GuideMaster guideMaster;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        guideMaster = GuideMaster.instance;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) SetState(true);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) SetState(false);
    }

    void SetState(bool state)
    {
        if (state == entered) return;
        entered = state;
        guideMaster.TweakInteractText(state);
    }

    void Update()
    {
        if(entered && !opened && Input.GetKeyDown("e"))
        {
            Open();
        }
    }

    protected virtual void Open()
    {
        opened = true;
        anim.SetTrigger("Open");
        GetComponent<SphereCollider>().enabled = false;
    }
}
