using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject board;
    public ChessPiece correctPiece;
    public Vector3 correctPosition;
    public LayerMask boardLayer;
    public AudioClip pieceSound, winSound;
    bool clicked;
    Vector3 targetPosition;

    AudioSource audioSource;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        audioSource = GetComponent<AudioSource>();
        yield return new WaitForSeconds(3);
        board.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

        if (clicked)
            return;

        Ray screenRay = Camera.main.ScreenPointToRay(Input.mousePosition);

		if (Input.GetMouseButtonDown(0))
		{
            //se selecionou a peça
            if(ChessPiece.selectedPiece != null)
			{
                RaycastHit hit;
                if (Physics.Raycast(screenRay, out hit, Mathf.Infinity, boardLayer))
                {
                    audioSource.PlayOneShot(pieceSound);
                    clicked = true;
                    float minX = Mathf.Floor(hit.point.x);
                    float maxX = Mathf.Ceil(hit.point.x);
                    float newX = (minX + maxX) / 2;

                    float minZ = Mathf.Floor(hit.point.z);
                    float maxZ = Mathf.Ceil(hit.point.z);
                    float newZ = (minZ + maxZ) / 2;

                    targetPosition = new Vector3(newX, 0, newZ);
                    Debug.Log(targetPosition);
                    ChessPiece.selectedPiece.MovePiece(targetPosition);
                    Invoke("CheckMate", 1);
                }
            }
            
		}
    }

    void CheckMate()
	{
        if(targetPosition == correctPosition && correctPiece == ChessPiece.selectedPiece)
		{
            Debug.Log("Check mate!");
            audioSource.PlayOneShot(winSound);
            Invoke("LoadNextScene", 1);
        }
		else
		{
            Debug.Log("Perdeu");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
	}

    void LoadNextScene()
	{
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
