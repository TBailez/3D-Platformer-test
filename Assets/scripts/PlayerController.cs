using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    //public Rigidbody theRB;
    public float jumpForce;
    public CharacterController controller;
    private Vector3 moveDirection;
    public float gravityScale;

    // Start is called before the first frame update
    void Start()
    {
      //  theRB = GetComponent<Rigidbody>();
      controller = GetComponent<CharacterController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //Utiliza Rigid Body para dar caracteristicas fisicas ao target(mais interessante usar controller para personagens jogaveis)
       /* theRB.velocity = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, theRB.velocity.y, Input.GetAxis("Vertical") * moveSpeed);
        
        if(Input.GetButtonDown("Jump")){
            theRB.velocity = new Vector3(theRB.velocity.x, jumpForce, theRB.velocity.z);
        }*/

        //Mover o target(personagem), baseado em qual for o movimento horizontal ou vertical(seta pra frente sempre move para o norte)
        //moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, moveDirection.y, Input.GetAxis("Vertical") * moveSpeed);

        //variavel auxiliar para lembrar o valor de y e não travar o pulo
        float yStore = moveDirection.y;
        //Mover o target com que a camera sempre seja a frente
        moveDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
        //normaliza a velocidade na diagonal
        moveDirection = moveDirection.normalized * moveSpeed;
        //restora o valor original de y
        moveDirection.y = yStore;

        if (controller.isGrounded){
            moveDirection.y = 0f;

        if(Input.GetButtonDown("Jump")){
            moveDirection.y = jumpForce;
        }
    }
        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale);
        controller.Move(moveDirection * Time.deltaTime);
    }
}
