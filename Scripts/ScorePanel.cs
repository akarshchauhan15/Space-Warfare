using Godot;

public partial class ScorePanel : Panel
{
    Button CloseButton;
    float InitialSizeX;
    float InitialPositionX;

    public override void _Ready()
    {
        InitialSizeX = Size.X;
        InitialPositionX = Position.X;
        CloseButton = GetNode<Button>("CloseButton");
        CloseButton.Pressed += Dissappear;
    }
    public void Appear()
    {
        Show();
        Tween tween = CreateTween();
        tween.SetParallel(true);
        tween.TweenProperty(this, "modulate:a", 1f, 0.3f).SetTrans(Tween.TransitionType.Sine).SetEase(Tween.EaseType.Out).From(0f);
        tween.TweenProperty(this, "position:x", InitialPositionX, 0.3f).SetTrans(Tween.TransitionType.Sine).SetEase(Tween.EaseType.Out);
        tween.TweenProperty(this, "size:x", InitialSizeX, 0.3f).SetTrans(Tween.TransitionType.Sine).SetEase(Tween.EaseType.Out);
    }
    private void Dissappear()
    {
        Tween tween = CreateTween();
        tween.SetParallel(true);
        tween.TweenProperty(this, "modulate:a", 0.4f, 0.3f).SetTrans(Tween.TransitionType.Sine).SetEase(Tween.EaseType.In);
        tween.TweenProperty(this, "position:x", (Position.X + Size.X)/2, 0.3f).SetTrans(Tween.TransitionType.Sine).SetEase(Tween.EaseType.In);
        tween.TweenProperty(this, "size:x", 0f, 0.3f).SetTrans(Tween.TransitionType.Sine).SetEase(Tween.EaseType.In);
        tween.SetParallel(false);
        tween.TweenCallback(Callable.From(() => Hide()));
    }
}
