using Godot;

public partial class Enemy : CharacterBody2D
{
    [Signal]
    public delegate void EnemyDownEventHandler();

    public Texture2D Texture { get; set; }

    public override void _Ready()
    {
        base._Ready();
        GetNode<Sprite2D>("Enemy").Texture = Texture;
    }
    public override void _Process(double delta)
    {
        if (Main.IsPlaying)
        Position += Main.EnemyDirection * Main.EnemySpeed * (float)delta;
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
        if (Main.EnemyDownCheck && (GetParent().GetChildCount() <= 1))
            GetNode<Hud>("../../../HUD").EndGame(true);

        Main.EnemySpeed += 3;

        Tween tween = CreateTween();
        tween.TweenProperty(this, "modulate:a", 0, 0.2).SetTrans(Tween.TransitionType.Quad);
        tween.TweenCallback(Callable.From(() =>  QueueFree()));
    }
}
