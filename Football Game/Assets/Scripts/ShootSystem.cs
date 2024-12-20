using UnityEngine;
using UnityEngine.UI;

public class ShootSystem : MonoBehaviour
{
    public Transform arrow;
    public Slider powerSlider;
    public GameObject projectilePrefab; 
    public Transform firePoint; 

    [Range(0, 100)] public float angleSpeed = 50f; 
    [Range(0, 100)] public float maxPower = 100f;
    [Range(0, 100)] public float powerIncreaseRate = 50f;

    private bool isAdjustingAngle = true;
    private float currentPower = 0f; 
    private bool isCharging = false; 
    private float currentAngle = 0f; 

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
            currentAngle = Mathf.Clamp(currentAngle, 0f, 100f);

            arrow.localRotation = Quaternion.Euler(0, 0, currentAngle);

            if (currentAngle >= 100f || currentAngle <= 0f)
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
                CloseBallVisibility();
                Fire();
            }
        }
    }

    void CloseBallVisibility()
    {
        GetComponent<MeshRenderer>().enabled = false;
        arrow.gameObject.SetActive(false);
        Invoke(nameof(AddBallVisibility), 0.5f);
    }

    void AddBallVisibility()
    {
        GetComponent<MeshRenderer>().enabled = true;
        arrow.gameObject.SetActive(true);
        ResetSystem();
    }

    void Fire()
    {
        float power = currentPower;

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        Vector3 direction = Quaternion.Euler(0, 0, currentAngle) * Vector3.right; 
        rb.linearVelocity = direction * power;
    }
    
    void ResetSystem()
    {
        isAdjustingAngle = true;
        isCharging = false;
        currentPower = 0f;
        powerSlider.value = 0f;
    }
}
