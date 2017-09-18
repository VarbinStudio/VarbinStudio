///-----------------///
///VarbinStudio@2017///
///-----------------///
///Этот код является магическии. Он работает чисто на магическом топливе.
///Если ты хочешь что бы он и дальше излучал магию, просто его нетрогай.
///Ну в если ты сам являешься магом, то ты безусловно должен облодать магией не ниже 80лвл'а.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class FadeEffectTool : MonoBehaviour
{

    [Header("Fade Group")]
    public GameObject FadeGroupPrefab;
    Transform FadeGroup;

    [Header("Fade Speed Value")]
    [Range(0.01f, 0.5f)]
    public float FadeSpeed = 0.1f;

    //[Header("Enable Object On Fade(Only SubGroup)")]
    bool ObjectEnable = false;
    GameObject SubFadeGroup;

    //[Header("FirstFloor")]


    [Header("Enabled Object on This fade group")]
    public bool EnabledObjects;
    public List<GameObject> ListObjectGroup = new List<GameObject>();

    float AlphaChannel = 1;
    bool IsFade = false;
    bool FirstEnter;

    List<Renderer> ListRender = new List<Renderer>();
    List<Material> ListMaterial = new List<Material>();
    List<Light> ListLight = new List<Light>();

    private void Awake()
    {
        

         if(FadeGroupPrefab == null)
         {
            FadeGroupPrefab = gameObject;
         }

        FadeGroup = FadeGroupPrefab.GetComponent<Transform>();

        foreach (Transform TransformObj in FadeGroup.GetComponentsInChildren<Transform>())
        {
            if (TransformObj.GetComponent<Light>() != null)
            {
                ListLight.Add(TransformObj.GetComponent<Light>());
            }

            if (TransformObj.GetComponent<Renderer>() != null)
            {
                ListRender.Add(TransformObj.GetComponent<Renderer>());
                ListMaterial.Add(TransformObj.GetComponent<Renderer>().material);
            }
        }
    }

    private void Start()
    {
        foreach (Renderer Rend in ListRender)
        {
            SetMaterialOpaque();
            Rend.material.color = new Color(Rend.material.color.r, Rend.material.color.g, Rend.material.color.b, 1);
        }
        foreach (Light SetLight in ListLight)
        {
            SetLight.enabled = false;
        }
        if (ObjectEnable && SubFadeGroup != null) {

                SubFadeGroup.SetActive(true);
        }
        if (ListObjectGroup != null && EnabledObjects != false)
        {
            foreach (GameObject Obj in ListObjectGroup)
            {
                Obj.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
             StartCoroutine("FadeIn");
            if (ListObjectGroup != null && EnabledObjects !=false)
            {
                foreach (GameObject Obj in ListObjectGroup)
                {
                    Obj.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject Player = GameObject.Find("Character");
        if (Player.transform.position.y > transform.position.y)
            {
            if (ListObjectGroup != null && EnabledObjects != false)
            {
                foreach (GameObject Obj in ListObjectGroup)
                {
                    Obj.SetActive(false);
                }
            }
            }else{
                if (other.tag == "Player")
                {
                    StartCoroutine("FadeOut");
                }
            }
        }

    private void SetMaterialFade()
    {
        foreach (Material CaseMaterial in ListMaterial)
        {
            CaseMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            CaseMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            CaseMaterial.SetInt("_ZWrite", 0);
            CaseMaterial.DisableKeyword("_ALPHATEST_ON");
            CaseMaterial.EnableKeyword("_ALPHABLEND_ON");
            CaseMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            CaseMaterial.renderQueue = 3000;

        }
    }

    private void SetMaterialOpaque()
    {
        foreach (Material CaseMaterial in ListMaterial)
        {
            
            CaseMaterial.SetInt("_ScrBlend", (int)UnityEngine.Rendering.BlendMode.One);
            CaseMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            CaseMaterial.SetInt("_ZWrite", 1);
            CaseMaterial.DisableKeyword("_ALPHATEST_ON");
            CaseMaterial.DisableKeyword("_ALPHABLEND_ON");
            CaseMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            CaseMaterial.renderQueue = -1;

        }
    }

    public IEnumerator FadeInNotControl()
    {

        if (ObjectEnable && SubFadeGroup != null)
        {
            SubFadeGroup.SetActive(true);
        }
        SetMaterialFade();
        foreach (Light SetLight in ListLight)
        {
            SetLight.enabled = true;
        }

        while (AlphaChannel <= 1)
        {

            AlphaChannel += FadeSpeed;
            foreach (Renderer Rend in ListRender)
            {
                Rend.material.color = new Color(Rend.material.color.r, Rend.material.color.g, Rend.material.color.b, AlphaChannel);
            }
            yield return new WaitForSeconds(0.01f);
        }
        if (AlphaChannel > 1)
        {
            foreach (Renderer Rend in ListRender)
            {
                Rend.material.color = new Color(Rend.material.color.r, Rend.material.color.g, Rend.material.color.b, 1);
            }
            SetMaterialOpaque();

            StopCoroutine("FadeIn");

        }

    }

    public IEnumerator FadeIn()
    {
        if (gameObject.tag == "FirstFloor" && FirstEnter == false)
        {
            FirstEnter = false;
        }
        else
        {
            if (ObjectEnable && SubFadeGroup != null)
            {
                SubFadeGroup.SetActive(true);
            }
            SetMaterialFade();
            foreach (Light SetLight in ListLight)
            {
                SetLight.enabled = true;
            }

            while (AlphaChannel <= 1)
            {
                IsFade = true;
                AlphaChannel += FadeSpeed;
                foreach (Renderer Rend in ListRender)
                {
                    Rend.material.color = new Color(Rend.material.color.r, Rend.material.color.g, Rend.material.color.b, AlphaChannel);
                }
                IsFade = true;
                yield return new WaitForSeconds(0.01f);
            }
            if (AlphaChannel > 1)
            {
                foreach (Renderer Rend in ListRender)
                {
                    Rend.material.color = new Color(Rend.material.color.r, Rend.material.color.g, Rend.material.color.b, 1);
                }
                IsFade = false;
                SetMaterialOpaque();

                StopCoroutine("FadeIn");

            }
        }
        
    }

    public IEnumerator FadeOut()
    {
            foreach (Light SetLight in ListLight)
            {
                SetLight.enabled = false;
            }

            SetMaterialFade();
            while (AlphaChannel >= 0)
            {
                if (IsFade)
                {
                    StopCoroutine("FadeOut");
                }
                AlphaChannel -= FadeSpeed;
                foreach (Renderer Rend in ListRender)
                {
                    Rend.material.color = new Color(Rend.material.color.r, Rend.material.color.g, Rend.material.color.b, AlphaChannel);
                }


                yield return new WaitForSeconds(0.01f);
            }
            if (AlphaChannel < 0)
            {
                foreach (Renderer Rend in ListRender)
                {
                    Rend.material.color = new Color(Rend.material.color.r, Rend.material.color.g, Rend.material.color.b, 0);
                }

                if (ObjectEnable && SubFadeGroup != null)
                {
                    SubFadeGroup.SetActive(false);
                }
            }
            StopCoroutine("FadeOut");
        }
}