using Godot;
using System;

public partial class Hud : Control
{
    Label HealthLabel;
    public override void _Ready()
    {
        base._Ready();

        HealthLabel = GetNode<Label>("HealthInfo/Label");
        GetNode<Player>("../Playground/Player").OnPlayerHit += () => HealthLabel.Text = "x" + Player.Health;
    }
        
}
