using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace UncleWilfred
{
    public class AnimationHelper : MonoBehaviour
    {
        public Action onFinish;
        public void OnAnimationFinished()
        {
            if(onFinish!=null)
                onFinish();
        }
    }
}