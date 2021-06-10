using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadObject : MonoBehaviour
{

    [SerializeField] private GameObject obj;
    private GameObject _obj;

    [System.Serializable] public enum Room { Room_3 = 3, Room_4, Room_5, Room_6, Room_7 };

    public Room room;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        PlaceOBJ();
        
    }
   


    public void ReloadRoom()
    {
        Destroy(_obj);
        PlaceOBJ();
    }

    private void PlaceOBJ()
    {
        _obj = Instantiate(obj) as GameObject;
        if (room == Room.Room_3)
        {
            _obj.transform.position = new Vector3(90, 0, 180);
        }
        else if (room == Room.Room_4)
        {
            _obj.transform.position = new Vector3(137.0f, 0, 101.9000015258789f);
            _obj.transform.Rotate(0, 180, 0);
        }
        else if (room == Room.Room_5)
        {
            _obj.transform.position = new Vector3(160, 0, 21);
        }
        else if (room == Room.Room_6)
        {
            _obj.transform.position = new Vector3(100, 0, 0);
        }
        else if (room == Room.Room_7)
        {
            _obj.transform.position = new Vector3(0, 0, 0);
        }
    }

}
