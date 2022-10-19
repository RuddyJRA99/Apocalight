using Cinemachine;
using Helpers;
using StarterAssets;
using UnityEngine;

public class FireController : MonoBehaviour
{
    #region -Properties-
    [Header("Cameras")]
    [SerializeField]
    CinemachineVirtualCamera aimCamera;
    [SerializeField]
    CinemachineVirtualCamera thirdPersonCamera;

    [Header("Fire")]
    [SerializeField] Transform bullet;
    [SerializeField] Transform firePoint;
    [SerializeField] float distance = 5;
    [SerializeField] [Range(0, 1)] float sensitivity;
    [SerializeField] LayerMask aimColliderMask;
    [SerializeField] float cadenceTime = 1f;

    [Header("GUI")]
    [SerializeField]
    GameObject CrossHair;
    [SerializeField]
    Transform Point;

    InputController input;
    ThirdPersonController thirdPersonController;
    float currentTime;
    StarterAssetsInputs _input;
    Animator animator;
    ShadowRayController _shadowRayController;
    #endregion

    void Awake()
    {
        _shadowRayController = GetComponent<ShadowRayController>();
        _input = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        animator = GetComponent<Animator>();
        currentTime = 0f;
    }

    void Update()
    {

        if (_input.aim && _shadowRayController.IsPowerShadow() && !_input.shadowPower)
        {
            aimCamera.gameObject.SetActive(true);
            thirdPersonController.SetSensitivity(sensitivity);
            _input.sprint = false;
            animator.SetLayerWeight(
                AnimatorLayer.AIM,
                Mathf.Lerp(
                    animator.GetLayerWeight(AnimatorLayer.AIM),
                    1f,
                    Time.deltaTime * 10f
                    )
                );
            animator.SetFloat("Axis_X", _input.move.x);
            animator.SetFloat("Axis_Y", _input.move.y);

            Fire();

            thirdPersonController.SetRotateOnMove(false);
            thirdPersonCamera.gameObject.SetActive(false);
            CrossHair.SetActive(true);
        }
        else
        {
            aimCamera.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(1f);
            animator.SetLayerWeight(
                AnimatorLayer.AIM,
                Mathf.Lerp(
                    animator.GetLayerWeight(AnimatorLayer.AIM),
                    0f,
                    Time.deltaTime * 10f
                    )
                );
            thirdPersonController.SetRotateOnMove(true);
            thirdPersonCamera.gameObject.SetActive(true);
            CrossHair.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        if (_shadowRayController.IsPowerShadow() && !_input.shadowPower)
        {
            if (_input.aim)
            {
                Vector3 mouseWorldPosition = Vector3.zero;
                Vector3 fireDirection = Vector3.zero;

                Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);

                Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
                if (Physics.Raycast(ray, out RaycastHit raycastHit, distance, aimColliderMask))
                {
                    mouseWorldPosition = raycastHit.point;

                    Point.position = raycastHit.point;
                    fireDirection = (raycastHit.point - firePoint.position).normalized;

                }


                Vector3 worldAimTarget = mouseWorldPosition;
                worldAimTarget.y = transform.position.y;

                Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
                transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
                firePoint.forward = fireDirection;
            }
        }
    }

    void Fire()
    {
        if (currentTime < cadenceTime)
        {
            currentTime += Time.deltaTime;
        }
        else
        {
            if (_input.fire)
            {
                Instantiate(bullet, firePoint.position, firePoint.rotation);
                currentTime = 0f;
                animator.SetTrigger("Fire");
            }
        }
    }
}
