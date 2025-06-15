using Godot;
using System;

public partial class Enemy : CharacterBody2D
{
    public override void _Ready()
    {
        base._Ready();
    }
    public void OnHit()
    {
        QueueFree();
    }
}
