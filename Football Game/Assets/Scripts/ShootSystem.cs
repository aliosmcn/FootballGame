using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ShootSystem: MonoBehaviour
{
    public List<GameObject> balls;
    private int sayi;
    private List<GameObject> lastObject;
    
    
    public GameObject ballPrefab;
    public Transform firePoint;
    
    protected Transform arrow;
    protected Slider powerSlider;

    [SerializeField] [Range(0, 100)] private float angleSpeed = 100f;
    [SerializeField] [Range(0, 100)] private float maxPower = 10f;
    [SerializeField] [Range(0, 100)] private float powerIncreaseRate = 10f;

    protected bool readyForFire = true;
    protected bool isAdjustingAngle = true;
    protected bool isCharging = false;

    protected float currentPower = 0f;
    protected float currentAngle;

    protected abstract KeyCode FireKey { get; }
    protected abstract float MinAngle { get; }
    protected abstract float MaxAngle { get; }

    private void Start()
    {
        arrow = GetComponentInChildren<Slider>().gameObject.transform;
        powerSlider = GetComponentInChildren<Slider>();
        transform.position = firePoint.position;
    }

    private void Update()
    {
        AngleSelection();
        PowerSelection();
    }

    private void AngleSelection()
    {
        if (isAdjustingAngle)
        {
            currentAngle += angleSpeed * Time.deltaTime;
            currentAngle = Mathf.Clamp(currentAngle, MinAngle, MaxAngle);
            arrow.localRotation = Quaternion.Euler(0, 0, currentAngle);

            if (currentAngle >= MaxAngle || currentAngle <= MinAngle)
            {
                angleSpeed = -angleSpeed;
            }
        }

        if (Input.GetKeyDown(FireKey))
        {
            isAdjustingAngle = false;
        }
    }

    private void PowerSelection()
    {
        if (!isAdjustingAngle)
        {
            if (Input.GetKey(FireKey))
            {
                isCharging = true;
                currentPower += powerIncreaseRate * Time.deltaTime;
                currentPower = Mathf.Clamp(currentPower, 0, maxPower);
                powerSlider.value = currentPower / maxPower;
            }

            if (Input.GetKeyUp(FireKey) && isCharging)
            {
                isAdjustingAngle = true;
                if (currentPower < 1f)
                {
                    ResetSystem();
                    return;
                }

                if (readyForFire) Fire();
                CloseBallVisibility();
            }
        }
    }

    public void Fire()
    {
        GameObject projectile = ObjectPool.Instance.GetObject();
        projectile.transform.position = firePoint.position;
        projectile.transform.rotation = firePoint.rotation;
        
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        Vector3 direction = Quaternion.Euler(0, 0, currentAngle) * Vector3.right;
        rb.linearVelocity = direction * currentPower;
    }

    private void CloseBallVisibility()
    {
        readyForFire = false;
        GetComponent<MeshRenderer>().enabled = false;
        arrow.gameObject.SetActive(false);
        Invoke(nameof(AddBallVisibility), 0.5f);
    }

    private void AddBallVisibility()
    {
        readyForFire = true;
        GetComponent<MeshRenderer>().enabled = true;
        arrow.gameObject.SetActive(true);
        ResetSystem();
    }

    private void ResetSystem()
    {
        isAdjustingAngle = true;
        isCharging = false;
        currentPower = 0f;
        powerSlider.value = 0f;
    }
}
