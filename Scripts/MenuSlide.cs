using Godot;

public partial class MenuSlide : Slide
{
    TextureButton FullscreenButton;
    TextureButton SoundButton;
    TextureButton ExitButton;

    bool ExitConfirmation = false;

    public override void _Ready()
    {
        base._Ready();
        FullscreenButton = GetNode<TextureButton>("ColorRect/Buttons/FullscreenButton");
        SoundButton = GetNode<TextureButton>("ColorRect/Buttons/SoundButton");
        ExitButton = GetNode<TextureButton>("ColorRect/Buttons/ExitButton");

        FullscreenButton.Toggled += ToggleWindowType;
        SoundButton.Toggled += SetSound;
        ExitButton.Pressed += ExitButtonPressed;
        Hidden += () => ExitConfirmation = false;

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
    private void ExitButtonPressed()
    {
        if (!ExitConfirmation) { ExitConfirmation = true; return; }
        GetTree().Quit();
    }
}
