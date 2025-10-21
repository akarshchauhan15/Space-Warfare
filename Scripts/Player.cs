using Godot;

public partial class Player : CharacterBody2D
{
    [Signal]
    public delegate void OnPlayerHitEventHandler(bool Dead);

    Parallax2D StarsParallax;
    Parallax2D Stars2Parallax;

    float MaxVelocity = 500f;
    float Acceleration = 2000f;
    float Friction = 1400f;

    Timer Cooldown;
    public static int Health = 3;
    public static int Score = 0;

    public override void _Ready()
    {
        base._Ready();

        StarsParallax = GetNode<Parallax2D>("../../Stars");
        Stars2Parallax = GetNode<Parallax2D>("../../Stars2");
        Cooldown = GetNode<Timer>("CooldownTimer");

        StarsParallax.ScrollOffset = new Vector2(GlobalPosition.X * 0.08f, StarsParallax.ScrollOffset.Y);
        Stars2Parallax.ScrollOffset = new Vector2(GlobalPosition.X * 0.04f, Stars2Parallax.ScrollOffset.Y);
    }
    public void Reset()
    {
        Health = 3;
        Modulate = Colors.White;
        GlobalPosition = new Vector2(640, 648); 
        EmitSignal(SignalName.OnPlayerHit, false);
    }
    public override void _Process(double delta)
    {
        if (!Main.IsPlaying) return;

        float InputDirection = Input.GetAxis("Left", "Right");

        if (InputDirection != 0)
            Velocity = Velocity.MoveToward(new Vector2(InputDirection, 0) * MaxVelocity, Acceleration * (float) delta);

        else
            Velocity = Velocity.MoveToward(Vector2.Zero, Friction * (float)delta);

        GlobalPosition = new Vector2(Mathf.Clamp(GlobalPosition.X, 30, 1250), GlobalPosition.Y);

        StarsParallax.ScrollOffset = new Vector2(GlobalPosition.X * 0.08f, StarsParallax.ScrollOffset.Y);
        Stars2Parallax.ScrollOffset = new Vector2(GlobalPosition.X * 0.04f, Stars2Parallax.ScrollOffset.Y);

        MoveAndSlide();
    }
    public override void _Input(InputEvent @event)
    {
        base._Input(@event);

        if (!Main.IsPlaying) return;

        if (@event.IsActionPressed("Shoot") && Cooldown.TimeLeft == 0)
            ShootBullet();
    }
    public void OnHit()
    {
        Health--;

        bool Dead = Health <= 0;
        EmitSignal(SignalName.OnPlayerHit, Dead);

        Hud.AddScore(-20);

        if (!Dead) return;

        Tween tween = CreateTween();
        tween.TweenProperty(this, "modulate:a", 0, 0.4f).SetTrans(Tween.TransitionType.Quad);
    }
    private void ShootBullet()
    {
        Area2D Bullet = Main.BulletScene.Instantiate<Area2D>();
        Bullet.GlobalPosition = GlobalPosition + new Vector2(0, -20);
        Bullet.SetCollisionMaskValue(1, false);

        GetNode("../Bullets").AddChild(Bullet);
        Cooldown.Start();
    }
}
