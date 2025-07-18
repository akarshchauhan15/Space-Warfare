using Godot;

public partial class Enemy : CharacterBody2D
{
    [Signal]
    public delegate void EnemyDownEventHandler();

    public Texture2D Texture { get; set; }
    public static float Speed;

    public override void _Ready()
    {
        base._Ready();
        GetNode<Sprite2D>("Enemy").Texture = Texture;
    }
    public override void _Process(double delta)
    {
        if (Main.IsPlaying)
        Position += Main.EnemyDirection * Speed * (float)delta;
    }
    public void Shoot()
    {
        Bullet bullet = Main.BulletScene.Instantiate<Bullet>();
        bullet.GlobalPosition = GlobalPosition + Vector2.Down * 20;
        bullet.Direction = Vector2.Down;
        bullet.Speed *= 1 - GD.Randf()/2;

        bullet.SetCollisionMaskValue(2, false);
        GetNode("../../Bullets").AddChild(bullet);
    }
    public void OnHit()
    {
        if (Main.EnemyDownCheck && (GetParent().GetChildCount() <= 1) && (GetNode("../../Extras").GetChildCount() == 0) )
            GetNode<Hud>("../../../HUD").EndGame(true);

        GetTree().Root.GetNode<Hud>("Main/HUD").PlaySound("EnemyHit");
        GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred(CollisionShape2D.PropertyName.Disabled, true);

        Speed += Main.Stage.EnemyAcceleration;
        Hud.AddScore(10);

        Tween tween = CreateTween();
        tween.TweenProperty(this, "modulate:a", 0, 0.2).SetTrans(Tween.TransitionType.Quad);
        tween.TweenCallback(Callable.From(() =>  QueueFree()));
    }
}
