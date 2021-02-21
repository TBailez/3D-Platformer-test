using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
//mira a camera no personagem
    public Transform target;
//quão longe do personagem a camera deve estar
    public Vector3 offset;
//desligar o offset
    public bool useOffsetValues;
//rodar a camera em volta do jogador
    public float rotateSpeed;
//pivot usado para não rotacionar o personagem ao mexer na camera
    public Transform pivot;
//Variaveis para alterar os angulos da camera direto no unity
    public float maxViewAngle;
    public float minViewAngle;
//Variavel para inverter o eixo Y
    public bool invertY;

    // Start is called before the first frame update
    void Start()
    {
        if(!useOffsetValues){
        offset = target.position - transform.position;
        }
        //Isso faz com que o pivot passa a estar no target ao inves de a camera (para melhorar movimento)
        pivot.transform.position = target.transform.position;
        pivot.transform.parent = target.transform;
        //Mantem o mouse travado no meio da dela, a seta desaparece da tela
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    // Alterei para Late Update para deixar mais liso o jogo
    void LateUpdate()
    {
        //Pegar a posição x do mouse e rodar o target(personagem)
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        target.Rotate(0, horizontal, 0);
        //Pegar a posição y do mouse e rodar o pivot(que agora está no personagem)
        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
       // pivot.Rotate(-vertical, 0, 0);
        //inverter o eixo y
        if(invertY){
            pivot.Rotate(vertical, 0, 0);
        
        }
        else
            {
                pivot.Rotate(-vertical, 0, 0);
            }
        //Limitar a rotação da camera para cima e para baixo 
        if(pivot.rotation.eulerAngles.x > maxViewAngle && pivot.rotation.eulerAngles.x < 180f){
            pivot.rotation = Quaternion.Euler(maxViewAngle, 0, 0);
        }

        //Limitar para a camera nao ir e volatar ao aproximar do target
        if(pivot.rotation.eulerAngles.x > 180f && pivot.rotation.eulerAngles.x < 360f + minViewAngle){
            pivot.rotation = Quaternion.Euler(360f + minViewAngle, 0, 0);
        }



        //Mover a camera baseado na atual rotação do target e do offset original, relacionado ao horizontal
        float desiredYAngle = target.eulerAngles.y;
        //Mover a camera baseado na atual rotação do target(agora pivot) e do offset original, relacionado ao vertical
        float desiredXAngle = pivot.eulerAngles.x;
        //Utilizar a dimensão quaternaria W para a rotação do target(ver Debug)
        Quaternion rotation  = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);
        transform.position = target.position - (rotation * offset);

        //Impede que a camera entre em baixo da terra
        if(transform.position.y < target.position.y){
            transform.position = new Vector3(transform.position.x, target.position.y - 0.5f, transform.position.z);
        }

        /*(offset automatico baseado na posição do terget e no offset dado)
        transform.position = target.position - offset;*/
        transform.LookAt(target);
    }
}
