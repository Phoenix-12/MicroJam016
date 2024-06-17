using UnityEngine;
using UnityEngine.UI;

public class GemsViewCounter : MonoBehaviour
{
    [SerializeField] private Text _gemsCounter;

    public void UpdateGems(int gems, int gemsNeed) => _gemsCounter.text = gems.ToString() + "/" + gemsNeed.ToString();
}