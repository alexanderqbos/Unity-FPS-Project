using UnityEngine;

public class basePopUp : MonoBehaviour {
    public virtual void Open()
    {
        if(!IsActive())
        {
            gameObject.SetActive(true);
            Messenger.Broadcast(GameEvents.POPUP_OPEN);
        } else {
            Debug.Log("Attempt to open already open window");
        }
    }

    public virtual void Close()
    {
        if(IsActive())
        {
            gameObject.SetActive(false);
            Messenger.Broadcast(GameEvents.POPUP_CLOSE);
        } else {
            Debug.Log("Attempt to close inactive window");
        }
    }

    public virtual bool IsActive()
    {
        return gameObject.activeSelf;
    }
}