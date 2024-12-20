using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShootSystem : MonoBehaviour
{
    public GameObject ballPrefab;
    public Transform firePoint;

    private Transform arrow; 
    private Slider powerSlider;

    [SerializeField] [Range(0, 100)] private float angleSpeed = 50f;
    [SerializeField] [Range(0, 100)] private float maxPower = 100f;
    [SerializeField] [Range(0, 100)] private float powerIncreaseRate = 50f;

    private bool readyForFire = true;
    private bool isAdjustingAngle = true;
    private bool isCharging = false;
    public float currentPower = 0f; 
    private float currentAngle = 0f;

    private void Start()
    {
        arrow = GetComponentInChildren<Slider>().gameObject.transform;
        powerSlider = GetComponentInChildren<Slider>();
        this.transform.position = firePoint.position;
        ballPrefab.GetComponent<Renderer>().material = this.GetComponent<Renderer>().material;
    }

    void Update()
    {
        AngleSelection();
        PowerSelection();
    }

    void AngleSelection()
    {
        if (isAdjustingAngle)
        {
            currentAngle += angleSpeed * Time.deltaTime;
            currentAngle = Mathf.Clamp(currentAngle, 0f, 110f);

            arrow.localRotation = Quaternion.Euler(0, 0, currentAngle);

            if (currentAngle >= 110f || currentAngle <= 0f)
            {
                angleSpeed = -angleSpeed;
            }
        }

        if (Input.GetMouseButtonDown(0)) 
        {
            isAdjustingAngle = false;
        }
    }

    void PowerSelection()
    {
        if (!isAdjustingAngle)
        {
            if (Input.GetMouseButton(0)) 
            {
                isCharging = true;
                currentPower += powerIncreaseRate * Time.deltaTime;
                currentPower = Mathf.Clamp(currentPower, 0, maxPower); 
                powerSlider.value = currentPower / maxPower; 
            }

            if (Input.GetMouseButtonUp(0) && isCharging)
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
    void Fire()
    {
        
        GameObject projectile = Instantiate(ballPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        Vector3 direction = Quaternion.Euler(0, 0, currentAngle) * Vector3.right;
        rb.linearVelocity = direction * currentPower;
        
    }

    void CloseBallVisibility()
    {
        readyForFire = false;
        GetComponent<MeshRenderer>().enabled = false;
        arrow.gameObject.SetActive(false);
        Invoke(nameof(AddBallVisibility), 0.5f);
    }

    void AddBallVisibility()
    {
        readyForFire = true;
        GetComponent<MeshRenderer>().enabled = true;
        arrow.gameObject.SetActive(true);
        ResetSystem();
    }

    
    void ResetSystem()
    {
        isAdjustingAngle = true;
        isCharging = false;
        currentPower = 0f;
        powerSlider.value = 0f;
    }
}
