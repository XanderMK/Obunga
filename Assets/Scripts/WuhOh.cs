using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WuhOh : MonoBehaviour
{
    public Transform player;
    [SerializeField] private float speed;

    void Update()
    {
        transform.LookAt(player, Vector3.up);
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
}
