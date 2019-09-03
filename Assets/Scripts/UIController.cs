using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public GameObject playerObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeText (string textName, string textString)
    {
        foreach (Text tx in this.GetComponentsInChildren<Text>())
        {
            if (tx.name == textName)
            {
                tx.text = textString;
            }
        }
    }
}
