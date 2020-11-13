using UnityEngine;

public class ApplyRootRotation : MonoBehaviour
{
    [SerializeField] private Transform root;

    private void Update()
    {
        this.transform.rotation = Quaternion.Euler(0, root.transform.rotation.y, 0);
    }
}
