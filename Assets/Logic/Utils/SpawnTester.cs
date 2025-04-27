using UnityEngine;

public class SpawnTester : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject piecePrefab;
    public int numberToSpawn = 10;
    public float spreadRadius = 5f;
    public SpawnShape spawnShape = SpawnShape.Circle;

    public enum SpawnShape
    {
        Circle,
        XPattern
    }

    private void Start()
    {
        if (piecePrefab == null)
        {
            Debug.LogError("SpawnTester: No PiecePrefab assigned!");
            return;
        }

        switch (spawnShape)
        {
            case SpawnShape.Circle:
                SpawnCircle();
                break;
            case SpawnShape.XPattern:
                SpawnXPattern();
                break;
        }
    }

    private void SpawnCircle()
    {
        for (int i = 0; i < numberToSpawn; i++)
        {
            float angle = i * Mathf.PI * 2 / numberToSpawn;
            Vector3 spawnPos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * spreadRadius;
            SpawnPiece(spawnPos);
        }
    }

    private void SpawnXPattern()
    {
        for (int i = 0; i < numberToSpawn; i++)
        {
            float offset = (i - numberToSpawn / 2) * 1.5f;
            Vector3 spawnPos = new Vector3(offset, 0, offset);
            SpawnPiece(spawnPos);
        }
    }

    private void SpawnPiece(Vector3 position)
    {
        GameObject piece = Instantiate(piecePrefab, position, Quaternion.Euler(0, Random.Range(0f, 360f), 0));
        piece.name = $"Piece_{Random.Range(1000, 9999)}";

        SaveableEntity saveable = piece.GetComponent<SaveableEntity>();
        if (saveable != null)
        {
            typeof(SaveableEntity)
                .GetField("stableID", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(saveable, System.Guid.NewGuid().ToString());
        }
    }
}
