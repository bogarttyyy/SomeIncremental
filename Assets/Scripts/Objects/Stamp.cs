using Interfaces;
using UnityEngine;

public class Stamp : MonoBehaviour, IDraggable
{
    public void OnDragged(Vector3 position)
    {
        transform.position = position;
    }
}
