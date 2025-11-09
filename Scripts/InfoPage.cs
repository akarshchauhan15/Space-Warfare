using Godot;

public partial class InfoPage : AnimatedPanel
{
    Button InitialTutorialButton;

    public override void _Ready()
    {
        base._Ready();
        InitialTutorialButton = GetNode<Button>("Control/InitialTutorialButton");
        InitialTutorialButton.Pressed += InitialTutorialButtonPressed;
    }
    private void InitialTutorialButtonPressed()
    {
        GetParent().AddChild(GD.Load<PackedScene>("res://Scenes/initial_page.tscn").Instantiate<InitialPage>());
    }

}
