using Godot;

public partial class Mothership : CharacterBody2D
{
    float Speed = 100f;
    int Health = 4;
    public int Direction {  get; set; }
    VisibleOnScreenNotifier2D VisibleNotifier;

    public override void _Ready()
    {
        base._Ready();
        VisibleNotifier = GetNode<VisibleOnScreenNotifier2D>("VisibleOnScreenNotifier2D");

        VisibleNotifier.ScreenExited += () => { CheckEnemyLeft(); QueueFree(); };
    }
    public override void _Process(double delta)
    {
        if (Main.IsPlaying)
            Position += Vector2.Left * Direction * Speed * (float)delta;
    }
    public void OnHit()
    {
        Health--;
        Modulate = Modulate.Darkened(0.1f);

        if (Health > 0) return;

        GetTree().Root.GetNode<Hud>("Main/HUD").PlaySound("LaserShoot");
        GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred(CollisionShape2D.PropertyName.Disabled, true);

        Hud.AddScore(40);
        CheckEnemyLeft();

        Tween tween = CreateTween();
        tween.TweenProperty(this, "modulate:a", 0, 0.2).SetTrans(Tween.TransitionType.Quad);
        tween.TweenCallback(Callable.From(() => QueueFree()));
    }
    private void CheckEnemyLeft()
    {
        Main.IsMothershipInAction = false;
        if (Main.EnemyDownCheck && (GetParent().GetChildCount() <= 1) && (GetNode("../../Enemies").GetChildCount() == 0))
            GetNode<Hud>("../../../HUD").EndGame(true);
    }
}
