using Godot;
using System;

public partial class Enemy : CharacterBody2D
{
    public override void _Ready()
    {
        base._Ready();
    }
    public override void _Process(double delta)
    {
        Position += Main.Direction * Main.Speed * (float)delta;
    }
    public void OnHit()
    {
        Main.Speed += 5;
        QueueFree();
    }
}
