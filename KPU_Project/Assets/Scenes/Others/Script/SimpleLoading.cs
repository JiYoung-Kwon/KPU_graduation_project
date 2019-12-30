using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Loading
{
    public class SimpleLoading : MonoBehaviour
    {
        private RectTransform rectComponent;
        private Image imageComp;
        public float rotateSpeed = 200f;

        // Use this for initialization
        void Start()
        {
            rectComponent = GetComponent<RectTransform>();
            imageComp = rectComponent.GetComponent<Image>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Manager.MainScene_Manager.mainscene_manager.Is_loading)
                rectComponent.Rotate(0f, 0f, -(rotateSpeed * Time.deltaTime));
        }
    }
}