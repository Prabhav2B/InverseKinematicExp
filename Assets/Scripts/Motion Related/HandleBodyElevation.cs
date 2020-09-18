
using UnityEngine;

public class HandleBodyElevation : MonoBehaviour
{
    [SerializeField]private Transform targetParent;
    private HandleTargetting[] targets;
    
    float meanTargetInitalElevation;
    SpyderMovement spyderMovement;


    private void Start()
    {
       
        targets = targetParent.GetComponentsInChildren<HandleTargetting>();

        float sum = 0;
        foreach (var target in targets)
        {
            sum += target.transform.position.y;
        }

        meanTargetInitalElevation = sum / targets.Length;

        spyderMovement = GetComponent<SpyderMovement>();
    }

    private void FixedUpdate()
    {
        float elevation = CalculateAverageElevation();
        spyderMovement.Elevation = elevation;
    }

    private float CalculateAverageElevation()
    {
        float sum = 0;
        foreach (var target in targets)
        {
            sum += (target.transform.position.y - meanTargetInitalElevation);
        }

       return(sum / targets.Length);
    }
}
