using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour
{
    public int rows = 5;  // ����
    public int columns = 5;  // ����
    public float spacing = 1;  // ����֮��ļ��
    public GameObject soilPrefab;  // ����
    private GameObject[,] grid;  // ���ڴ洢�����е�ÿ�����ӵ����
    private Camera mainCamera;  // �������
    void Start()
    {
        grid = new GameObject[rows, columns];
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                // ����Ԫ�ص�xyz��λ��
                Vector3 position = new Vector3(col * spacing - 3, row * spacing - 3, -9);
                grid[row, col] = Instantiate(soilPrefab, position, Quaternion.identity);
                grid[row, col].name = $"Soil_{row}_{col}";
                BoxCollider2D collider = grid[row, col].GetComponent<BoxCollider2D>();
                if (collider == null)
                {
                    collider = grid[row, col].AddComponent<BoxCollider2D>();
                }
                collider.enabled = true;
            }
        }
    }

    // ���ʱ������ֲ
    void Update()
    {
        if (Input.GetMouseButtonDown(0))  // ���������
        {
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

            if (hit.collider != null)
            {
                GameObject clickedSoil = hit.collider.gameObject;
                PlantCrop(clickedSoil);  // ���ʱ��ֲ����
            }
        }
    }

    // �ڸ�������ֲ����
    void PlantCrop(GameObject soil)
    {
        Debug.Log($"Planted on {soil.name}");
        SpriteRenderer renderer = soil.GetComponent<SpriteRenderer>();
        renderer.color = Color.green;  // ������������ֲ������ı���������ɫ��
    }
}
