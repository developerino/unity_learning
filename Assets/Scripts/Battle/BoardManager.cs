using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Test;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private List<TileProperty> TilePropsList;
    [SerializeField] private GameObject Piece;

    private void Start()
    {
        TilePropsList = new List<TileProperty>();
        int id = 1;
        foreach (Transform tileChild in this.transform)
        {
            TileProperty tempRef = tileChild.gameObject.GetComponent<TileProperty>();
            tempRef.set_id(id);
            tempRef.set_owner(OWNER.NONE);

            Debug.Log($"Child id is: {tempRef.get_id()} and owner is: {tempRef.get_owner()} and name: {tempRef.gameObject.name}");
            TilePropsList.Add(tempRef);
            id++;
        }
        string pos_vector3 = JsonConvert.SerializeObject(new SerializeVector3(Piece.GetComponent<Transform>().position));
        PlayerData.PlayerPreferences.SetPref<string>("position", pos_vector3);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log($"Raycasted object is: {hit.collider.gameObject.name}");

            }
        }
        if (Input.GetKey("l"))
        {
            string saved = PlayerData.PlayerPreferences.GetPref<string>("position", "{}");
            SerializeVector3 loaded = JsonConvert.DeserializeObject<SerializeVector3>(saved);
            Debug.Log($"Loaded position: {loaded.x}, {loaded.y}, {loaded.z}");
            Piece.transform.position = loaded.ToVector3();
        }

    }
}

class SerializeVector3
{
    public float x, y, z;

    public SerializeVector3(Vector3 position)
    {
        x = position.x;
        y = position.y;
        z = position.z;
    }

    public Vector3 ToVector3() => new Vector3(x, y, z);
}