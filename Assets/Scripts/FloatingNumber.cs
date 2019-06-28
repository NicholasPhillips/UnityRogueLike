using UnityEngine;
using UnityEngine.UI;

public class FloatingNumber : MonoBehaviour
{
	public Text UIText;
	public float Scroll = 1.0f;
	public float Duration = 1.5f;
	public float Alpha = 1;
	public Color TextColor = new Color(0.8f, 0.8f, 0.1f, 1f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (Alpha > 0)
		{
			var position = transform.position;
			position.y += Scroll * Time.deltaTime;
			transform.position = position;
			Alpha -= Time.deltaTime / Duration;
			TextColor = UIText.color;
			TextColor.a = Alpha;
			UIText.color = TextColor;
		}
		else
		{
			Destroy(gameObject); // text vanished - destroy itself
		}
	}
}
