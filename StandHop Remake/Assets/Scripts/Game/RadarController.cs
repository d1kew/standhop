using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadarController : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private Sprite Schema;
    [SerializeField] private GameObject Camera;

    private void Start()
    {
        renderer.sprite = Schema;
    }

    private void Update()
    {
        Camera.transform.localPosition = new Vector3(Player.transform.position.x, 30, Player.transform.position.z);
        Camera.transform.localRotation = Quaternion.Euler(90, Player.transform.eulerAngles.y, 0);
    }
}
