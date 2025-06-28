using Godot;

public partial class Bullet : Area2D
{
    public float Speed = 1000f;
    public Vector2 Direction = Vector2.Up;

    public override void _Ready()
    {
        base._Ready();
        GetNode<Timer>("Timer").Timeout += QueueFree;

        BodyEntered += OnCollision;
        AreaEntered += OnCollision;

        GetTree().Root.GetNode<Hud>("Main/HUD").PlaySound("LaserShoot");

        Tween tween = CreateTween();
        tween.TweenProperty(this, "modulate:a", 1, 0.04).From(0);
    }
    public override void _Process(double delta) => Position += Direction * Speed * (float) delta;
    private void OnCollision(Node2D Body)
    {
        if ((Body is Player || Body is Enemy || Body is Bunker) && Body.HasMethod("OnHit"))
        Body.Call("OnHit");

        Particles Particles = Main.DestructionParticleScene.Instantiate<Particles>();
        Particles.GlobalPosition = GlobalPosition;

        GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
        

        if (Body is Bunker) Particles.SetStyle(50f, -Direction);
        else Particles.SetStyle(180f);

        Particles.Emitting = true;
        GetNode<Node>("../../").AddChild(Particles);

        Tween tween = CreateTween();
        tween.TweenProperty(this, "modulate:a", 0, 0.02);
        tween.TweenCallback(Callable.From(QueueFree));
    }
}
