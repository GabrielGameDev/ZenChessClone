using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPiece : MonoBehaviour
{
    public static ChessPiece selectedPiece;
    Vector3 targetPosition;
    bool isMoving;
    public float speed = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (isMoving)
		{
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
        
    }

	private void OnMouseDown()
	{
        Invoke("SelectPiece", 0.1f);
	}

    void SelectPiece()
	{
        selectedPiece = this;
    }

    public void MovePiece(Vector3 targetPos)
	{
        targetPosition = targetPos;
        isMoving = true;
	}
}
