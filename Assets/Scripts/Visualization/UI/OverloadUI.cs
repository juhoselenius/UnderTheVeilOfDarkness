using UnityEngine;
using UnityEngine.UI;

namespace Visualization
{
    public class OverloadUI : MonoBehaviour
    {
        public WeaponManager wManager;

        public Image filler;
        public Sprite overloadedFiller;
        public Sprite notOverloadedFiller;
        public Image overloadIcon;
        public Sprite overloadedIcon;
        public Sprite notOverloadedIcon;
        private float maxOverload;

        // Start is called before the first frame update
        void Start()
        {
            maxOverload = wManager.overLoadMax;
        }

        // Update is called once per frame
        void Update()
        {
            if(wManager.overLoaded)
            {
                filler.sprite = overloadedFiller;
                overloadIcon.sprite = overloadedIcon;
            }
            else
            {
                filler.sprite = notOverloadedFiller;
                overloadIcon.sprite = notOverloadedIcon;
            }

            filler.fillAmount = wManager.currentOverLoad / maxOverload;
        }
    }
}
