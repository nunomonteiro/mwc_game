using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMessage : MonoBehaviour {

    public void OnAnimationDone( ) {
        Destroy(this.gameObject);
    }
}
