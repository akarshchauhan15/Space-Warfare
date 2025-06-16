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
    }
    public override void _Process(double delta) => Position += Direction * Speed * (float) delta;
    private void OnCollision(Node2D Body)
    {
        if (Body is Player || Body is Enemy)
        Body.Call("OnHit");

        Hide();
        QueueFree();
    }
}
