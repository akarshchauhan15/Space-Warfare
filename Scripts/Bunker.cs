using Godot;

public partial class Bunker : Area2D
{
    int Health = 5;

    public void OnHit()
    {
        Health--;
        GetNode<Sprite2D>("Sprite2D").Modulate = new Color(1, 1, 1, 0.2f * Health);

        if (Health > 0) return;

        GetTree().Root.GetNode<Hud>("Main/HUD").PlaySound("BunkerExplosion");
        GetNode<CollisionShape2D>("Collision").SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
    }
    public void Reset()
    {
        GetNode<Sprite2D>("Sprite2D").Modulate = Colors.White;
        GetNode<CollisionShape2D>("Collision").SetDeferred(CollisionShape2D.PropertyName.Disabled, false);
    }
}
