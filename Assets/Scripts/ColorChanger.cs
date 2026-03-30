using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public void SetRandomColor(Renderer renderer) =>
         renderer.material.color = Random.ColorHSV();
    public void SetStartColor(Renderer renderer) =>
        renderer.material.color = Color.white;
}