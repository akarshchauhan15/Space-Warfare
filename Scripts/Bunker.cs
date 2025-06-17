using Godot;

public partial class Bunker : Area2D
{
    int Health = 5;

    public void OnHit()
    {
        Health--;
        GetNode<Sprite2D>("Sprite2D").Modulate = new Color(1, 1, 1, 0.2f * Health);

        if (Health > 0) return;

        QueueFree();
    }
}
