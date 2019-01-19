using UnityEngine;



[ExecuteInEditMode]
public class ImageFx : MonoBehaviour
{


#pragma warning disable 649
	[SerializeField] private Material _material;
#pragma warning restore 649


	public Material material => _material; // provide material to inspector



	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (_material == null)
		{
			Graphics.Blit(source, destination);
			return;
		}

		Graphics.Blit(source, destination, _material);
	}


}
