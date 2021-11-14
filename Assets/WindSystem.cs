using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSystem : MonoBehaviour
{
    private void Awake()
    {
        Rotate();
    }

    private void Rotate()
    {
        gameObject.LeanRotateY(Random.Range(0, 360), 10f).setOnComplete(() =>
        {
            Rotate();
        });
    }
}
