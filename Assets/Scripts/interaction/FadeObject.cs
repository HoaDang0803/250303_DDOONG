using UnityEngine;

public class RoofHouse : MonoBehaviour
{
    public GameObject Roof = null;
    public GameObject Chuck = null;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            SetMaterialTransparent();
            iTween.FadeTo(Roof, 0, 1);
            iTween.FadeTo(Chuck, 0, 1);
        }
    }


    private void SetMaterialTransparent()
    {
        foreach (Material m in Chuck.GetComponent<Renderer>().materials)
        {
            m.SetFloat("_Surface", 1);

            m.SetFloat("_SrcBlend", (float)UnityEngine.Rendering.BlendMode.SrcAlpha);
            m.SetFloat("_DstBlend", (float)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            m.SetFloat("_ZWrite", 0); // Không ghi vào Z-buffer để tránh lỗi hiển thị
            m.SetFloat("_BlendModePreserveSpecular", 0); // Không giữ specular để hòa trộn tốt hơn
            m.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;

            // Giảm Alpha để làm trong suốt
            Color color = m.color;
            color.a = 0.5f; // Làm mờ 50%
            m.color = color;

        }
        foreach (Material m in Roof.GetComponent<Renderer>().materials)
        {
            m.SetFloat("_Surface", 1);
            m.SetFloat("_SrcBlend", (float)UnityEngine.Rendering.BlendMode.SrcAlpha);
            m.SetFloat("_DstBlend", (float)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            m.SetFloat("_ZWrite", 0); // Không ghi vào Z-buffer để tránh lỗi hiển thị
            m.SetFloat("_BlendModePreserveSpecular", 0); // Không giữ specular để hòa trộn tốt hơn
            m.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
            // Giảm Alpha để làm trong suốt
            Color color = m.color;
            color.a = 0.5f; // Làm mờ 50%
            m.color = color;
        }
    }



    private void SetMaterialOpaque()
    {
        foreach (Material m in Chuck.GetComponent<Renderer>().materials)
        {
            m.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            m.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            m.SetInt("_ZWrite", 1);
            m.SetInt("_BlendModePreserveSpecular", 1);
            m.renderQueue = -1; // Đặt lại render queue về mặc định
            Color color = m.color;
            color.a = 1; // Làm mờ 50%
            m.color = color;
        }

        foreach (Material m in Roof.GetComponent<Renderer>().materials)
        {
            m.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            m.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            m.SetInt("_ZWrite", 1);
            m.SetInt("_BlendModePreserveSpecular", 1);
            m.renderQueue = -1; // Đặt lại render queue về mặc định
            Color color = m.color;
            color.a = 1; // Làm mờ 50%
            m.color = color;
        }

    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            iTween.FadeTo(Roof, 1, 1);
            Invoke("SetMaterialOpaque", 1.0f);

        }

    }

}
