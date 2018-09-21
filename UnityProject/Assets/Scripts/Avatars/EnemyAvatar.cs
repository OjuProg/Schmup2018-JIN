// <copyright file="EnemyAvatar.cs" company="AAllard">Copyright AAllard. All rights reserved.</copyright>

using UnityEngine;

public class EnemyAvatar : BaseAvatar
{
    [SerializeField]
    private EnemyType type;

    public EnemyType Type
    {
        get
        {
            return this.type;
        }

        private set
        {
            this.type = value;
        }
    }

    public string PrefabPath
    {
        get;
        set;
    }
    
    protected override void Release()
    {
        EnemyFactory.Instance.Release(this);
    }

    protected override void Update()
    {
        base.Update();

        // Very simple out of bound test.
        if (this.Position.x > 14 || this.Position.x < -14 || this.Position.y > 20 || this.Position.y < -20)
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        BaseAvatar avatar = other.gameObject.GetComponent<BaseAvatar>();
        if (avatar != null)
        {
            avatar.TakeDamage(this.DamageDealthAtCollision);
            this.TakeDamage(avatar.DamageDealthAtCollision);
        }
    }
}
