using Godot;

public partial class Bunker : Area2D
{
    int Health = 5;

    public override void _Ready()
    {
        base._Ready();
        Modulate = new Color(1, 1, 1, 0);
    }
    public void OnHit()
    {
        Health--;
        GetNode<Sprite2D>("Sprite2D").Modulate = GetNode<Sprite2D>("Sprite2D").Modulate.Darkened(0.2f); //new Color(1, 1, 1, 0.2f * Health);

        if (Health > 0) return;

        GetTree().Root.GetNode<Hud>("Main/HUD").PlaySound("BunkerExplosion");
        GetNode<CollisionShape2D>("Collision").SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
        QueueFree();
    }
    public void Fade()
    {
        Tween tween = CreateTween();
        tween.TweenProperty(this, "modulate:a", 1, 0.4f).SetTrans(Tween.TransitionType.Quad);
    }
}
