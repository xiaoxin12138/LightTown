using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour
{
    public int rows = 5;  // 行数
    public int columns = 5;  // 列数
    public float spacing = 1;  // 格子之间的间隔
    public GameObject soilPrefab;  // 土壤
    private GameObject[,] grid;  // 用于存储网格中的每个格子的情况
    private Camera mainCamera;  // 主摄像机
    void Start()
    {
        grid = new GameObject[rows, columns];
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                // 设置元素的xyz轴位置
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

    // 点击时进行种植
    void Update()
    {
        if (Input.GetMouseButtonDown(0))  // 鼠标左键点击
        {
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

            if (hit.collider != null)
            {
                GameObject clickedSoil = hit.collider.gameObject;
                PlantCrop(clickedSoil);  // 点击时种植作物
            }
        }
    }

    // 在格子上种植作物
    void PlantCrop(GameObject soil)
    {
        Debug.Log($"Planted on {soil.name}");
        SpriteRenderer renderer = soil.GetComponent<SpriteRenderer>();
        renderer.color = Color.green;  // 代表作物已种植（例如改变土壤的颜色）
    }
}
