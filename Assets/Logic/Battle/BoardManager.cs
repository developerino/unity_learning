using System;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private List<TileProperty> TilePropsList;
    [SerializeField] private GameObject Piece;

    public void InitializeBoard()
    {
        Debug.Log("BoardManager: initialized by GameManager.");

        TilePropsList = new List<TileProperty>();
        int id = 1;
        foreach (Transform tileChild in transform)
        {
            TileProperty tempRef = tileChild.GetComponent<TileProperty>();
            tempRef.SetId(id);
            tempRef.SetOwner(OWNER.NONE);

            TilePropsList.Add(tempRef);
            id++;
        }

        // Save the piece position using generic SetPref
        PlayerData.PlayerPreferences.SetPref("position", Piece.transform.position);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log($"BoardManager: Raycasted object is: {hit.collider.gameObject.name}");
            }
        }

        //if (Input.GetKey("l"))
        //{
        //    // Load the position using generic GetPref
        //    Vector3 loaded = PlayerData.PlayerPreferences.GetPref("position", Vector3.zero);
        //    Debug.Log($"BoardManager: Loaded position: {loaded.x}, {loaded.y}, {loaded.z}");
        //    Piece.transform.position = loaded;
        //}
    }
}
