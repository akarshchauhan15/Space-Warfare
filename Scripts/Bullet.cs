using Godot;

public partial class Bullet : Area2D
{
    float Speed = 1000f;

    public override void _Ready()
    {
        base._Ready();
        GetNode<Timer>("Timer").Timeout += QueueFree;

        BodyEntered += OnCollision;
    }
    public override void _Process(double delta) => Position += new Vector2(0, -Speed) * (float) delta;
    private void OnCollision(Node2D Body)
    {
        if (Body is Player || Body is Enemy)
        Body.Call("OnHit");

        QueueFree();
    }
}
