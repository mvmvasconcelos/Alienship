using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public GameObject player;

    public float playerSpeed = 5f;
    public float hoverForce = 2500.0f;
    public float hoverHeight = 15f;


    private Vector3 pos;
    private float horizontalInput;
    private Vector3 normalizeDirection;



    void Awake()
    {
        playerRb = player.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        
        ////normalizeDirection = (pos - transform.position).normalized;
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            Ray posicaoMouse = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit infoClick;
            if (Physics.Raycast(posicaoMouse, out infoClick))
            {
                Debug.Log(infoClick.point);
                Debug.DrawLine(player.transform.position, infoClick.point, Color.white);
                player.transform.position = Vector3.MoveTowards(player.transform.position, infoClick.point, Time.deltaTime * playerSpeed);
            }

        }

        //Cria um raio partindo da posição do player, em direção para baixo
        Ray ray = new Ray(player.transform.position, -player.transform.up);
        //Recebe a informação do hit do raio
        RaycastHit hit;

        //Se o raio que foi criado naquela direção atingiu algo até a altura do hover
        if (Physics.Raycast(ray, out hit, hoverHeight))
        {
            Debug.DrawLine(player.transform.position, hit.transform.position, Color.blue);
            float proportionalHeight = (hoverHeight - hit.distance) / hoverHeight;
            Vector3 appliedHoverForce = Vector3.up * proportionalHeight * hoverForce;
            playerRb.AddForce(appliedHoverForce, ForceMode.Acceleration);
        }

        

        playerRb.AddForce(horizontalInput * playerSpeed, 0f, 0.0f);
    }
}
