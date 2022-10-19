using UnityEngine;

public class ShadowRayController : MonoBehaviour
{
    [SerializeField]
    Transform directionalLight;

    [SerializeField]
    Transform shadowPoint;

    [SerializeField]
    float lightDistance = 50;

    [SerializeField]
    float timeToHidden = 2f;
    float currentTimeToHidden = 0f;

    [SerializeField]
    LayerMask ignoreMask;

    bool isPowerShadow;

    void Update()
    {
        // compute and set rotation and position
        shadowPoint.SetPositionAndRotation(transform.position, Quaternion.Euler(
            directionalLight.rotation.eulerAngles.x * -1,
            directionalLight.rotation.eulerAngles.y + 180, 0f));
    }

    void FixedUpdate()
    {
        
        if (!computeShadowRay())
        {
            currentTimeToHidden += Time.fixedDeltaTime;
            Debug.Log(currentTimeToHidden);
            if (currentTimeToHidden > timeToHidden)
            {
                isPowerShadow = false;
                currentTimeToHidden = 0f;
            }
        }
        else
        {
            isPowerShadow = true;
            currentTimeToHidden = 0f;
        }
    }

    bool computeShadowRay()
    {
        RaycastHit[] hitList;
        hitList = Physics.RaycastAll(shadowPoint.position, shadowPoint.forward * lightDistance);

        foreach (RaycastHit hit in hitList)
        {
            if (ignoreMask != (ignoreMask | (1 << hit.collider.gameObject.layer)))
            {
                
                if (hit.collider.gameObject.tag != transform.tag)
                {
                    //
                    //if(
                    return true;
                }
            }
            
        }
        return false;
    }

    public bool IsPowerShadow()
    {
        return isPowerShadow;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(shadowPoint.position, shadowPoint.forward * lightDistance);
    }
}
