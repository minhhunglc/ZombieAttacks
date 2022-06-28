using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Texture2D cursor;

    public Transform transformPlayer;

    float camOffSetZ;
    // Start is called before the first frame update
    void Start()
    {
        camOffSetZ = gameObject.transform.position.z - transformPlayer.position.z;

        //Cursor.visible = false;
        Cursor.SetCursor(cursor, Vector3.zero, CursorMode.ForceSoftware);
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        if (transformPlayer == null)
        {
            try
            {
                SceneManager.LoadScene("GamePlay");
            }
            catch (System.Exception)
            {
                throw;
            }

        }
        else
        {
            Vector3 cameraPos = new Vector3(transformPlayer.position.x, gameObject.transform.position.y, transformPlayer.position.z + camOffSetZ);
            gameObject.transform.position = cameraPos;
        }
    }
}
