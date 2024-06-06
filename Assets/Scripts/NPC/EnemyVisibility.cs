#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
#endif

// detects when a given target is visible to this object. A target is 
// visible when it's both in range and in front of the target. Both the
// range and the angle of visibility are configurable.

public class EnemyVisibility : MonoBehaviour
{

    // The object we're looking for.
    public Transform target = null;

    // If the object is more than this distance away, we can't see it.
    public float maxDistance = 10f;

    // The angle of our arc of visibility.
    [Range(0f, 360f)]
    public float angle = 45f;

    // If true, visualize changes in visibility by changing
    // material colour
    [SerializeField] bool visualize = true;

    // A property that other classes can access to determine if we can
    // currently see our target.
    public bool TargetIsVisible { get; private set; }

    // Check to see if we can see the target every frame.
