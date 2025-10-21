using Godot;

public partial class Hud : Control
{
    Label HealthLabel;
    static Label ScoreLabel;
    Label Declaration;
    Button ContinueButton;

    bool LastRoundWon = false;
    public override void _Ready()
    {
        base._Ready();

        HealthLabel = GetNode<Label>("HealthInfo/Label");
        ScoreLabel = GetNode<Label>("ScoreInfo/Score");
        Declaration = GetNode<Label>("DeclarationLabel");
        ContinueButton = GetNode<Button>("ContinueButton");

        GetNode<Player>("../Playground/Player").OnPlayerHit += PlayerHit;
        ContinueButton.Pressed += ContinueButtonPressed;
        GetNode<Button>("InitialStart/StartButton").Pressed += () => GetNode<AnimationPlayer>("InitialStart/AnimationPlayer").Play("Clicked");
    }
    public void EndGame(bool Win)
    {
        Main.IsPlaying = false;

        if (Win)
        {
            bool StageLeft = Main.CurrentStage < Stages.stages.Count;
            Declaration.Text = (StageLeft) ? "STAGE CLEARED!" : "VICTORY!";
            if (StageLeft) LastRoundWon = true;
        }
        else
            Declaration.Text = "GAME OVER!";
        Declaration.Show();
        
        GetParent<Main>().RandomShootTimer.Stop();

        Tween tween = CreateTween();
        tween.TweenCallback(Callable.From(() => ContinueButton.Show())).SetDelay(0.4f);
    }
    public void PlaySound(string SoundName)
    {
        AudioStreamPlayer Audio = GetNodeOrNull<AudioStreamPlayer>("Sounds/" + SoundName);
        if (Audio != null) Audio.Play();
        else GD.PrintErr("Audio stream "+ SoundName + " not found!");
    }
    public static void AddScore(int Score)
    {
        Player.Score += Score;
        Player.Score = Mathf.Max(Player.Score, 0);
        ScoreLabel.Text = Player.Score.ToString();
    }
    private void InitialStartAnimationEnded()
    {
        GetParent<Main>().StartGame();
    }
    private void PlayerHit(bool Dead)
    {
        if (Dead) EndGame(false);

        HealthLabel.Text = "x" + Player.Health;
    }
    private void ContinueButtonPressed()
    {
        if (LastRoundWon) Main.CurrentStage++;

        else 
        { 
            Main.CurrentStage = 1;
            AddScore(-Player.Score);
            GetNode<Player>("../Playground/Player").Reset();
        }

        LastRoundWon = false;

        Declaration.Hide();
        ContinueButton.Hide();

        foreach (Node Enemy in GetParent<Main>().EnemyContainer.GetChildren())
            Enemy.Free();
        foreach (Node Bullet in GetNode("../Playground/Bullets").GetChildren())
            Bullet.Free();
        foreach (Node Bunker in GetNode("../Playground/Bunkers").GetChildren())
            Bunker.Free();
        foreach (Node Extra in GetNode("../Playground/Extras").GetChildren())
            Extra.Free();

        Main.Stage = Stages.stages[Main.CurrentStage - 1];

        GetParent<Main>().StartGame();
    }
}
