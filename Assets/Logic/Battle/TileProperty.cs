using UnityEngine;
using static GameManager;

public class TileProperty : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private OWNER owner;

    public int GetId()
    {
        return id;
    }
    public void SetId(int new_id)
    {
        id = new_id;
    }

    public OWNER GetOwner()
    {
        return owner;
    }
    public void SetOwner(OWNER new_owner)
    {
        owner = new_owner;
    }
}