using Godot;
using Godot.Collections;
using System;

public partial class Main : CanvasLayer
{
    PackedScene EnemyScene;
    public static PackedScene BulletScene;

    Array<Texture2D> EnemyTextures = new Array<Texture2D>();

    public Node EnemyContainer;
    Timer ShiftCooldownTimer;
    public Timer RandomShootTimer;

    //Vector2 TotalEnemies = new Vector2(8, 6);
    Vector2 TotalEnemies = new Vector2(2, 2);
    Vector2 EnemySize = new Vector2(100, 50);

    public static Vector2 EnemyDirection = Vector2.Right;
    public static float EnemySpeed = 80;
    public static int CurrentStage = 1;

    public static bool IsPlaying = false;
    public static bool EnemyDownCheck = false;
    
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

        StartGame();
    }
    public void StartGame()
    {
        SetEnemies();
        IsPlaying = true;
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
        EnemyDirection = EnemyDirection * -1;
        foreach (Enemy enemy in EnemyContainer.GetChildren())
            enemy.Position += Vector2.Down * 50;
    }
    private void MakeRandomEnemyShoot()
    {
        Random Roll = new Random();
        
        int Enemies = EnemyContainer.GetChildCount();
        int Index = Roll.Next(Enemies);

        if (!EnemyDownCheck && (Enemies <= 5))
            EnemyDownCheck = true;

        if (Enemies <= 0)
            return;

        EnemyContainer.GetChild<Enemy>(Index).Shoot();

        RandomShootTimer.Start(0.6 + Roll.NextDouble() * 2);
    }
}
