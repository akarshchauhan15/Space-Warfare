using Godot;
using Godot.Collections;
using System;

public partial class Main : CanvasLayer
{
    [Signal]
    public delegate void GameStartedEventHandler();

    PackedScene EnemyScene;
    public static PackedScene BulletScene;

    Array<Texture2D> EnemyTextures = new Array<Texture2D>();

    Node EnemyContainer;
    Timer ShiftCooldownTimer;
    Timer RandomShootTimer;

    Vector2 TotalEnemies = new Vector2(8, 6);
    Vector2 EnemySize = new Vector2(100, 50);

    public static Vector2 Direction = Vector2.Right;
    public static float Speed = 80;

    public override void _Ready()
    {
        base._Ready();

        EnemyScene = ResourceLoader.Load<PackedScene>("res://Scenes/enemy.tscn");
        BulletScene = ResourceLoader.Load<PackedScene>("res://Scenes/bullet.tscn");

        for (int i = 1; i < 4; i++)
            EnemyTextures.Add(GD.Load<Texture2D>($"res://Assets/Art/Exported/Enemy{i}.png"));

        EnemyContainer = GetNode<Node>("Playground/Enemies");
        ShiftCooldownTimer = GetNode<Timer>("Playground/ShiftCooldownTimer");
        RandomShootTimer = GetNode<Timer>("Playground/RandomShootTimer");

        GetNode<Area2D>("Playground/Boundaries").BodyEntered += ShiftEnemies;
        RandomShootTimer.Timeout += MakeRandomEnemyShoot;

        SetEnemies();
        RandomShootTimer.Start(1);
    }

    private void SetEnemies()
    {
        int Row = 0;

        while (Row < TotalEnemies.X)
        {
            int Column = 0;

            while (Column < TotalEnemies.Y)
            {
                InitializeEnemies(Row, Column);
                Column++;
            }
            Row++;
        }
    }
    private void InitializeEnemies(int Row, int Column)
    {
        Enemy Enemy = EnemyScene.Instantiate<Enemy>();
        Enemy.GlobalPosition = Vector2.One * 100 + new Vector2(Row * EnemySize.X, Column * EnemySize.Y);
        Enemy.Texture = EnemyTextures[Column%3];

        EnemyContainer.AddChild(Enemy);
    }
    private void ShiftEnemies(Node2D Body)
    {
        if (ShiftCooldownTimer.TimeLeft != 0)
            return;

        ShiftCooldownTimer.Start();
        Direction = Direction * -1;
        foreach (Enemy enemy in EnemyContainer.GetChildren())
            enemy.Position += Vector2.Down * 50;
    }
    private void MakeRandomEnemyShoot()
    {
        Random Roll = new Random();
        int Index =  Roll.Next(EnemyContainer.GetChildCount());
        Enemy ChoosenOne = EnemyContainer.GetChildOrNull<Enemy>(Index);

        if (ChoosenOne != null)
        ChoosenOne.Shoot();

        RandomShootTimer.Start(0.6 + Roll.NextDouble() * 2);
    }
}
