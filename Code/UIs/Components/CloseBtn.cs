using UnityEngine;
using Works.JES.Code.Farms;

namespace Works.JES.Code.UIs.Components
{
    public class CloseBtn : MonoBehaviour
    {
        public void Close()
        {
            FarmManager.Instance.Deselect();
        }
    }
}