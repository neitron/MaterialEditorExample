using UnityEditor;



[CustomEditor(typeof(ImageFx))]
public class ImageFxEditor : Editor
{


	private ImageFx _imageFx;
	private MaterialEditor _materialEditor;



	private void OnEnable()
	{
		_imageFx = (ImageFx)target;

		if (_imageFx.material != null)
		{
			_materialEditor = (MaterialEditor)CreateEditor(_imageFx.material);
		}
	}


	public override void OnInspectorGUI()
	{
		EditorGUI.BeginChangeCheck();
		{
			// Draw the material field of ImageFx.cs
			SerializedProperty materialProperty = serializedObject.FindProperty("_material");
			EditorGUILayout.PropertyField(materialProperty);
		}
		if (EditorGUI.EndChangeCheck())
		{
			// Free the memory used by the previous MaterialEditor
			serializedObject.ApplyModifiedProperties();

			if (_materialEditor != null)
			{
				// Free the memory used by the previous MaterialEditor
				DestroyImmediate(_materialEditor);
			}

			if (_imageFx.material != null)
			{
				// Create a new instance of the default MaterialEditor
				_materialEditor = (MaterialEditor)CreateEditor(_imageFx.material);
			}
		}

		if (_materialEditor == null)
		{
			return;
		}

		// Draw the material's foldout and the material shader field
		// Required to call _materialEditor.OnInspectorGUI ();
		_materialEditor.DrawHeader();

		//  We need to prevent the user to edit Unity default materials
		bool isDefaultMaterial = !AssetDatabase.GetAssetPath(_imageFx.material).StartsWith("Assets");
		using (new EditorGUI.DisabledGroupScope(isDefaultMaterial))
		{
			// Draw the material properties
			// Works only if the foldout of _materialEditor.DrawHeader () is open
			_materialEditor.OnInspectorGUI();
		}
	}


	public void OnDisable()
	{
		if (_materialEditor != null)
		{
			DestroyImmediate(_materialEditor);
		}
	}


}
