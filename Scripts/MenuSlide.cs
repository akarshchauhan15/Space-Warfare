using Godot;

public partial class MenuSlide : Slide
{
    TextureButton FullscreenButton;
    TextureButton SoundButton;
    TextureButton PauseButton;
    TextureButton ScoreButton;
    TextureButton InfoButton;
    TextureButton ExitButton;

    bool ExitConfirmation = false;

    public override void _Ready()
    {
        base._Ready();
        FullscreenButton = GetNode<TextureButton>("ColorRect/Buttons/FullscreenButton");
        SoundButton = GetNode<TextureButton>("ColorRect/Buttons/SoundButton");
        PauseButton = GetNode<TextureButton>("ColorRect/Buttons/PauseButton");
        ScoreButton = GetNode<TextureButton>("ColorRect/Buttons/ScoreButton");
        InfoButton = GetNode<TextureButton>("ColorRect/Buttons/InfoButton");
        ExitButton = GetNode<TextureButton>("ColorRect/Buttons/ExitButton");

        FullscreenButton.Toggled += ToggleWindowType;
        SoundButton.Toggled += SetSound;
        PauseButton.Toggled += PauseGame;
        ScoreButton.Pressed += () => { PauseGame(true); GetNode<AnimatedPanel>("../ScorePanel").Appear("CloseButton");};
        InfoButton.Pressed += () => { PauseGame(true); GetNode<AnimatedPanel>("../InfoPage").Appear("Control/InitialTutorialButton");};
        ExitButton.Pressed += ExitButtonPressed;
        MotionCompleted += (bool Hidden) => { if (Hidden) ExitConfirmation = false; };

        GetNode<Main>("../../").GameStarted += () => { PauseButton.Visible = true; };
        GetNode<Hud>("../").GameEnded += () => { PauseButton.Visible = false; };

        SetSettings();
    }
    private void SetSettings()
    {
        FullscreenButton.ButtonPressed = (bool)ConfigController.config.GetValue("Settings", "Fullscreen", false);
        Engine.MaxFps = Mathf.Min(Mathf.Max((int)ConfigController.config.GetValue("Settings", "MaxFps", 60), 24), 480);
        DisplayServer.WindowSetVsyncMode((bool)ConfigController.config.GetValue("Settings", "Vsync", true) ? DisplayServer.VSyncMode.Enabled : DisplayServer.VSyncMode.Disabled);
    }
    private void ToggleWindowType(bool Toggled)
    {
        DisplayServer.WindowSetMode(Toggled ? DisplayServer.WindowMode.Fullscreen : DisplayServer.WindowMode.Windowed);
        ConfigController.SaveSetting("Settings", "Fullscreen", Toggled);
    }
    private void SetSound(bool Muted)
    {
        AudioServer.SetBusMute(0, Muted);
        ConfigController.SaveSetting("Settings", "Sound", Muted);
    }
    private void PauseGame(bool Pause)
    {
        if (!Main.IsPlaying) return;

        GetTree().Paused = Pause;

        GetNode<Label>("../DeclarationLabel").Visible = Pause ? true : false;
        GetNode<Label>("../DeclarationLabel").Text = Pause ? "PAUSED" : "";
    }
    private void ExitButtonPressed()
    {
        if (!ExitConfirmation) { ExitConfirmation = true; return; }
        GetTree().Quit();
    }
}
