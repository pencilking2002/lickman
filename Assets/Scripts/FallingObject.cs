using UnityEngine;

public class FallingObject : MonoBehaviour
{
    public enum Type
    {
        good,
        bad
    }

    [SerializeField] private float pointWorth;
    [SerializeField] private Type type;
    public virtual void OnLick()
    {

    }


    public virtual float GetPointWorth() => pointWorth;
    public Type GetFallingObjectType() => type;
    
}