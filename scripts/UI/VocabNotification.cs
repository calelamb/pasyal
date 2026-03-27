using Godot;
using System.Collections.Generic;

namespace Pasyal.UI;

public partial class VocabNotification : Control
{
    private PanelContainer _notifPanel = null!;
    private Label _wordLabel = null!;

    private Node _vocabManager = null!;
    private readonly Queue<string> _pendingWords = new();
    private bool _isShowing;

    public override void _Ready()
    {
        _notifPanel = GetNode<PanelContainer>("NotifPanel");
        _wordLabel = GetNode<Label>("NotifPanel/WordLabel");

        _vocabManager = GetNode("/root/VocabManager");
        _vocabManager.Connect("VocabDiscovered", Callable.From<string>(OnVocabDiscovered));

        _notifPanel.Modulate = new Color(1, 1, 1, 0);
        _notifPanel.Position = new Vector2(_notifPanel.Position.X, -30);
    }

    private void OnVocabDiscovered(string tagalogWord)
    {
        _pendingWords.Enqueue(tagalogWord);

        if (!_isShowing)
        {
            ShowNext();
        }
    }

    private void ShowNext()
    {
        if (_pendingWords.Count == 0)
        {
            _isShowing = false;
            return;
        }

        _isShowing = true;
        string word = _pendingWords.Dequeue();

        _wordLabel.Text = $"Bagong salita! \u2605 {word}";
        _wordLabel.AddThemeColorOverride("font_color", new Color("E8A838"));

        float startY = -30.0f;
        float targetY = 4.0f;

        _notifPanel.Position = new Vector2(_notifPanel.Position.X, startY);
        _notifPanel.Modulate = new Color(1, 1, 1, 0);

        var tween = CreateTween();
        tween.SetEase(Tween.EaseType.Out);
        tween.SetTrans(Tween.TransitionType.Back);

        // Slide in and fade in
        tween.TweenProperty(_notifPanel, "position:y", targetY, 0.3f);
        tween.Parallel().TweenProperty(_notifPanel, "modulate:a", 1.0f, 0.2f);

        // Hold for 2 seconds
        tween.TweenInterval(2.0f);

        // Fade out
        tween.TweenProperty(_notifPanel, "modulate:a", 0.0f, 0.4f);

        tween.Finished += ShowNext;
    }
}
