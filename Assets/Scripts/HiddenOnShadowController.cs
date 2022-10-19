using StarterAssets;
using UnityEngine;

public class HiddenOnShadowController : MonoBehaviour
{
    [SerializeField]
    Transform Geometry;
    [SerializeField]
    Transform Skeleton;
    [SerializeField]
    GameObject ShadowIndicator;

    ShadowRayController _shadowRayController;
    CharacterController _controller;
    StarterAssetsInputs _input;
    float _height;
    Vector3 _center;

    void Start()
    {
        _shadowRayController = GetComponent<ShadowRayController>();
        _controller = GetComponent<CharacterController>();
        _input = GetComponent<StarterAssetsInputs>();
        _height = _controller.height;
        _center = _controller.center;
    }

    void Update()
    {
        if (!_shadowRayController.IsPowerShadow() || _input.shadowPower)
        {
            //cuando esta oculto
            Geometry.GetComponentInChildren<SkinnedMeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
            _controller.height = 0.5f;
            _controller.center = new Vector3(0f, 0.3f, 0f);
            Geometry.gameObject.SetActive(false);
            Skeleton.gameObject.SetActive(false);
            ShadowIndicator.SetActive(true);

        }
        else
        {
            //cuando se ve
            if (ShadowIndicator.activeSelf)
            {
                Geometry.GetComponentInChildren<SkinnedMeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                Geometry.gameObject.SetActive(true);
                Skeleton.gameObject.SetActive(true);
                _controller.height = _height;
                _controller.center = _center;
                ShadowIndicator.SetActive(false);
            }

        }

    }
}
