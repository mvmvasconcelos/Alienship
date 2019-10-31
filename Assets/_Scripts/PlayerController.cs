using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;

    public float speed = 50f;
    public float hoverForce = 65.0f;
    public float hoverHeight = 15f;

    private float horizontalInput;
    

    void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        //Cria um raio partindo da posição do player, em direção para baixo
        Ray ray = new Ray(transform.position, -transform.up);
        //Recebe a informação do hit do raio
        RaycastHit hit;

        Debug.Log(Physics.Raycast(ray, out hit, hoverHeight));
        //Se o raio que foi criado naquela direção atingiu algo até a altura do hover
        if (Physics.Raycast(ray, out hit, hoverHeight))
        {
            Debug.Log("Sim");
            float proportionalHeight = (hoverHeight - hit.distance) / hoverHeight;
            Vector3 appliedHoverForce = Vector3.up * proportionalHeight * hoverForce;
            playerRb.AddForce(appliedHoverForce, ForceMode.Acceleration);
        }

        playerRb.AddForce(horizontalInput * speed, 0f, 0.0f);
    }
}
