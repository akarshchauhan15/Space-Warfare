using Godot;
using System;

public partial class Hud : Control
{
    Label HealthLabel;
    Label Declaration;
    Button ContinueButton;

    bool LastRoundWon = false;
    public override void _Ready()
    {
        base._Ready();

        HealthLabel = GetNode<Label>("HealthInfo/Label");
        Declaration = GetNode<Label>("DeclarationLabel");
        ContinueButton = GetNode<Button>("ContinueButton");

        GetNode<Player>("../Playground/Player").OnPlayerHit += PlayerHit;
        ContinueButton.Pressed += ContinueButtonPressed;
    }
    public void EndGame(bool Win)
    {
        Main.IsPlaying = false;

        if (Win)
        {
            Declaration.Text = (Main.CurrentStage < 5) ? "STAGE CLEARED!" : "VICTORY!";
            LastRoundWon = true;
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
    private void PlayerHit(bool Dead)
    {
        if (Dead)
            EndGame(false);

        HealthLabel.Text = "x" + Player.Health;
    }
    private void ContinueButtonPressed()
    {
        if (LastRoundWon) Main.CurrentStage++;
        else Main.CurrentStage = 1;

        LastRoundWon = false;

        Declaration.Hide();
        ContinueButton.Hide();

        foreach (Node Enemy in GetParent<Main>().EnemyContainer.GetChildren())
            Enemy.Free();
        foreach (Node Bullet in GetNode("../Playground/Bullets").GetChildren())
            Bullet.Free();
        foreach (Node Bunker in GetNode("../Playground/Bunkers").GetChildren())
            Bunker.Free();

        Main.Stage = Stages.stages[Main.CurrentStage - 1];

        GetParent<Main>().StartGame();
    }
}
