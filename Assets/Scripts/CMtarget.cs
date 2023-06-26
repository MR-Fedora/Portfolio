using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMtarget : MonoBehaviour
{
    public CinemachineVirtualCamera cine;

    private void Awake()
    {
        cine = GetComponent<CinemachineVirtualCamera>();
    }
}
