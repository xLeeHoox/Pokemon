using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimationChange : MonoBehaviour
{
    
    public void Awake()
    {
        
    }
    public void Update()
    {
     /*   if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("Change Sprite");
            ChangeSprite(GetAnimationClip("PlayerIdle"), GameResources.Instance.pokemonItem.itemInfor[2].idleImages);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Change Sprite back");
            ChangeSprite(GetAnimationClip("PlayerIdle"), GameResources.Instance.pokemonItem.itemInfor[0].idleImages);
        }*/
    }
    //public AnimationClip GetAnimationClip(Animator animator, string name)
    //{
    //    RuntimeAnimatorController runtimeAnimatorController = animator.runtimeAnimatorController;
    //    AnimationClip[] animationClips = runtimeAnimatorController.animationClips;
    //    foreach (var item in animationClips)
    //    {
    //        if (item.name == name)
    //        {
    //            return item;
    //        }
    //    }
    //    return null;
    //}
    //public void ChangeSprite(AnimationClip animation, Sprite[] sprites)
    //{
    //    //thiet lap cau hinh editorCurveBinding de loc ra sprite cua keyframe
    //    EditorCurveBinding editorCurveBinding = new EditorCurveBinding();
    //    editorCurveBinding.type = typeof(SpriteRenderer);
    //    editorCurveBinding.path = "";
    //    editorCurveBinding.propertyName = "m_Sprite";

    //    //lay tat ca cac sprite cua key frame
    //    ObjectReferenceKeyframe[] keyframes = AnimationUtility.GetObjectReferenceCurve(animation, editorCurveBinding);
    //    for (int i = 0; i < keyframes.Length; i++)
    //    {
    //        keyframes[i].value = sprites[i]; //change sprite cua key frame
    //    }
    //    AnimationUtility.SetObjectReferenceCurve(animation, editorCurveBinding, keyframes);
    //}
}
