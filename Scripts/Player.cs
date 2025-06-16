using Godot;

public partial class Player : CharacterBody2D
{
    float MaxVelocity = 500f;
    float Acceleration = 2000f;
    float Friction = 1400f;

    Timer Cooldown;
    int Health = 0;

    public override void _Ready()
    {
        base._Ready();

        Cooldown = GetNode<Timer>("CooldownTimer");
    }

    public override void _Process(double delta)
    {
        float InputDirection = Input.GetAxis("Left", "Right");

        if (InputDirection != 0)
            Velocity = Velocity.MoveToward(new Vector2(InputDirection, 0) * MaxVelocity, Acceleration * (float) delta);

        else
            Velocity = Velocity.MoveToward(Vector2.Zero, Friction * (float)delta);

        MoveAndSlide();
    }
    public override void _Input(InputEvent @event)
    {
        base._Input(@event);

        if (@event.IsActionPressed("Shoot") && Cooldown.TimeLeft == 0)
            ShootBullet();
    }
    public void OnHit()
    {
        Health--;
    }
    private void ShootBullet()
    {
        Area2D Bullet = Main.BulletScene.Instantiate<Area2D>();
        Bullet.GlobalPosition = GlobalPosition + new Vector2(0, -20);
        Bullet.SetCollisionMaskValue(1, false);

        GetParent().AddChild(Bullet);
        Cooldown.Start();
    }
}
