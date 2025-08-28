using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Camera mainCam;
    private Health health;
    private float currentHealth;
    private float maxHealth;
    [SerializeField] Image healthBarImage;

    private void Awake()
    {
        mainCam = Camera.main;
        health = GetComponentInParent<Health>();
    }

    private void Start()
    {
        maxHealth = health.MaxHealth;
        currentHealth = maxHealth;
    }

    private void OnEnable()
    {
        health.OnHealthChange += ChangeBar;
    }
    private void OnDisable()
    {
        health.OnHealthChange -= ChangeBar;
    }

    private void LateUpdate()
    {
        Vector3 toCam = (mainCam.transform.position - transform.position);
        if (toCam.sqrMagnitude > 0.0001f)
            transform.rotation = Quaternion.LookRotation(toCam * -1f, Vector3.up);
    }

    void ChangeBar(float currentHealth)
    {
        Debug.Log(transform.root.name + ": " + currentHealth);
        this.currentHealth = currentHealth;
        healthBarImage.fillAmount = this.currentHealth / maxHealth;
    }
}
