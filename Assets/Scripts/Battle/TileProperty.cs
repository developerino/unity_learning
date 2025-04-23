using UnityEngine;

public class TileProperty : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private OWNER owner;

    public int get_id()
    {
        return id;
    }
    public void set_id(int new_id)
    {
        id = new_id;
    }

    public OWNER get_owner()
    {
        return owner;
    }
    public void set_owner(OWNER new_owner)
    {
        owner = new_owner;
    }
}

public enum OWNER
{
    NONE,
    PLAYER,
    NPC
}
