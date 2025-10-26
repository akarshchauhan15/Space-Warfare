using Godot;
using Godot.Collections;

public partial class EnemyDrop : Area2D
{
    public int Speed;
    public EnemyDrops.Types DropType;

    public static float EnemySpeedMultiplier = 1;
    public static Array<EnemyDrops.Types> ActiveDropEffects = new Array<EnemyDrops.Types>();

    public override void _Ready()
    {
        Speed = GD.RandRange(50, 80);
        BodyEntered += OnCollision;
        GetNode<VisibleOnScreenNotifier2D>("VisibleOnScreenNotifier2D").ScreenExited += QueueFree;
    }
    public override void _Process(double delta) => Position += Vector2.Down * Speed * (float) delta;

    public void OnCollision(Node2D Body)
    {     
        QueueFree();
        Hud.AddScore(GameData.ScoreValues[GameData.ScoreEnum.DropPickup]);

        Player Player = GetNode<Player>("../../Player");
        Node2D EnemyContainer = GetNode<Node2D>("../../Enemies");
        Node Timers = GetNode<Node>("../../Timers");
        Timer RandomShootTimer = GetNode<Timer>("../../Timers/RandomShootTimer");

        switch (DropType) 
        {
            case EnemyDrops.Types.Health:
                Player.Health += 1;
                Player.Health = Mathf.Min(Player.Health, 3);
                Hud.HealthLabel.Text = "x" + Player.Health;
                break;

            case EnemyDrops.Types.Freeze:

                if (ActiveDropEffects.Contains(EnemyDrops.Types.Freeze) || ActiveDropEffects.Contains(EnemyDrops.Types.Slowdown)) return;

                Timer FreezeTimer = new Timer();
                FreezeTimer.OneShot = true;
                Timers.AddChild(FreezeTimer);

                FreezeTimer.Timeout += () =>
                {
                    EnemySpeedMultiplier = 1;
                    EnemyContainer.Modulate = Colors.White;
                    foreach (Enemy Enemy in EnemyContainer.GetChildren())
                        Enemy.GetNode<AnimationPlayer>("AnimationPlayer").Play();
                    RandomShootTimer.Start();
                    FreezeTimer.QueueFree();
                    ActiveDropEffects.Remove(EnemyDrops.Types.Freeze);
                };

                EnemySpeedMultiplier = 0;
                EnemyContainer.Modulate = new Color(0.557f, 0.678f, 1.0f);
                foreach (Enemy Enemy in EnemyContainer.GetChildren()) 
                    Enemy.GetNode<AnimationPlayer>("AnimationPlayer").Pause();
                RandomShootTimer.Stop();

                FreezeTimer.Start(2);
                ActiveDropEffects.Add(EnemyDrops.Types.Freeze);
                break;

            case EnemyDrops.Types.Slowdown:

                if (ActiveDropEffects.Contains(EnemyDrops.Types.Freeze) || ActiveDropEffects.Contains(EnemyDrops.Types.Slowdown)) return;

                Timer SlowdownTimer = new Timer();
                SlowdownTimer.OneShot = true;
                Timers.AddChild(SlowdownTimer);

                float SlowdownAmount = 0.5f;

                SlowdownTimer.Timeout += () =>
                {
                    EnemySpeedMultiplier = 1;
                    RandomShootTimer.WaitTime *= 0.5;
                    foreach (Enemy Enemy in EnemyContainer.GetChildren())
                        Enemy.GetNode<AnimationPlayer>("AnimationPlayer").SpeedScale = 1;
                    SlowdownTimer.QueueFree();
                    ActiveDropEffects.Remove(EnemyDrops.Types.Slowdown);
                };
                EnemySpeedMultiplier = SlowdownAmount;
                RandomShootTimer.WaitTime /= 0.5;
                foreach (Enemy Enemy in EnemyContainer.GetChildren())
                    Enemy.GetNode<AnimationPlayer>("AnimationPlayer").SpeedScale = SlowdownAmount;

                SlowdownTimer.Start(3f);
                ActiveDropEffects.Add(EnemyDrops.Types.Slowdown);
                break;

            case EnemyDrops.Types.BunkerHealth:
                Node BunkerContainer = GetNode("../../Bunkers");
                foreach (Bunker Bunker in BunkerContainer.GetChildren())
                {
                    if (Bunker.Health > 4) return;
                    Bunker.Health++;
                    Sprite2D Sprite = Bunker.GetNode<Sprite2D>("Sprite2D");
                    Sprite.Modulate = Sprite.Modulate.Lightened(0.2f);
                }
                break;
        }
    }
}
