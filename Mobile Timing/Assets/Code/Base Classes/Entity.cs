using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    // Public:
    public SpriteRenderer Sr { get { return sr; } }
    public Vector2 Position { get { return position; } }
    public Vector2 Scale { get { return scale; } }
    public Vector2 Rotation { get { return rotation; } }

    // Protected: 
    protected SpriteRenderer sr;
    protected Vector2 position;
    protected Vector2 scale;
    protected Vector3 rotation;

    protected void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    protected void Update()
    {
        position = transform.position;
        scale = transform.localScale;
        rotation = transform.rotation.eulerAngles;
    }

    public Vector2 GetSize_Half()
    {
        if (sr == null)
            sr = GetComponent<SpriteRenderer>();

        Vector2 size;
        size.x = (sr.sprite.bounds.size.x) * transform.localScale.x / 2;
        size.y = (sr.sprite.bounds.size.y) * transform.localScale.y / 2;

        return size;
    }
}
