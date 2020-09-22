using UnityEngine;

[RequireComponent(typeof(Camera))]
public class LookAround : MonoBehaviour
{
    [SerializeField]private float sensitivityY = 15F;
    [SerializeField]private float sensitivityX = 15F;
    
    [SerializeField]private float minimumX = -360F;
    [SerializeField]private float maximumX = 360F;
    
    [SerializeField]private float minimumY = -60F;
    [SerializeField]private float maximumY = 60F;

    float rotationY = 0F;
    float rotationX = 0F;

    private void Update()
    {
        
            rotationX += Input.GetAxis("Mouse X") * sensitivityX;
            rotationX = Mathf.Clamp(rotationX, minimumX, maximumX);
                //= transform.localEulerAngles.y 

            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        
    }
}
