Шаблон FadeEffect для создания исчезнавения при входе в здание

switch (blendMode) 
{ 
case BlendMode.Opaque: 
material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One); 
material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero); 
material.SetInt("_ZWrite", 1); 
material.DisableKeyword("_ALPHATEST_ON"); 
material.DisableKeyword("_ALPHABLEND_ON"); 
material.DisableKeyword("_ALPHAPREMULTIPLY_ON"); 
material.renderQueue = -1; 
break; 
case BlendMode.Cutout: 
material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One); 
material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero); 
material.SetInt("_ZWrite", 1); 
material.EnableKeyword("_ALPHATEST_ON"); 
material.DisableKeyword("_ALPHABLEND_ON"); 
material.DisableKeyword("_ALPHAPREMULTIPLY_ON"); 
material.renderQueue = 2450; 
break; 
case BlendMode.Fade: 
material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha); 
material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha); 
material.SetInt("_ZWrite", 0); 
material.DisableKeyword("_ALPHATEST_ON"); 
material.EnableKeyword("_ALPHABLEND_ON"); 
material.DisableKeyword("_ALPHAPREMULTIPLY_ON"); 
material.renderQueue = 3000; 
break; 
case BlendMode.Transparent: 
material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One); 
material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha); 
material.SetInt("_ZWrite", 0); 
material.DisableKeyword("_ALPHATEST_ON"); 
material.DisableKeyword("_ALPHABLEND_ON"); 
material.EnableKeyword("_ALPHAPREMULTIPLY_ON"); 
material.renderQueue = 3000; 
break; 
}