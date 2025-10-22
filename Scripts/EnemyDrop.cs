using Godot;

public partial class EnemyDrop : Area2D
{
    public int Speed;
    public EnemyDrops.Types DropType;

    public override void _Ready()
    { 
        Speed = GD.RandRange(50, 80);
        BodyEntered += OnCollision;
    }
    public override void _Process(double delta) => Position += Vector2.Down * Speed * (float) delta;

    public void OnCollision(Node2D Body)
    {     
        QueueFree();
        Hud.AddScore(20);

        Player Player = GetNode<Player>("../../Player");
        Node2D EnemyContainer = GetNode<Node2D>("../../Enemies");

        switch (DropType) 
        {
            case EnemyDrops.Types.Health:
                Player.Health += 1;
                Player.Health = Mathf.Min(Player.Health, 3);
                Hud.HealthLabel.Text = "x" + Player.Health;
                break;

            case EnemyDrops.Types.Freeze:
                Timer FreezeTimer = new Timer();
                FreezeTimer.OneShot = true;
                GetNode("../../Timers").AddChild(FreezeTimer);

                Timer RandomShootTimer = GetNode<Timer>("../../Timers/RandomShootTimer");

                Vector2 PreviousEnemyDirection = Main.EnemyDirection;

                FreezeTimer.Timeout += () =>
                {
                    Main.EnemyDirection = PreviousEnemyDirection;
                    EnemyContainer.Modulate = Colors.White;
                    foreach (Enemy Enemy in EnemyContainer.GetChildren())
                        Enemy.GetNode<AnimationPlayer>("AnimationPlayer").Play();
                    RandomShootTimer.Start();
                    FreezeTimer.QueueFree();
                };

                Main.EnemyDirection = Vector2.Zero;
                EnemyContainer.Modulate = new Color(0.557f, 0.678f, 1.0f);
                foreach (Enemy Enemy in EnemyContainer.GetChildren()) 
                    Enemy.GetNode<AnimationPlayer>("AnimationPlayer").Pause();
                RandomShootTimer.Stop();

                FreezeTimer.Start(2);
                break;
        }
    }
}
