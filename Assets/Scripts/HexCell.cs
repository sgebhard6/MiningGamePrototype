using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HexCell : MonoBehaviour {

    public HexCoordinates coordinates;
    Quaternion rot;
    public TextMeshProUGUI coordText;

    public void Flip()
    {
        if(transform.rotation.z % 180 == 0)
            transform.rotation = new Quaternion(0, 0, transform.rotation.z + 180, 0);
        else
            transform.rotation = Quaternion.identity;
    }
}