using Godot;
using System.Collections.Generic;
using Pasyal.Systems;

namespace Pasyal.UI;

public partial class VocabNotification : Control
{
    private PanelContainer _notifPanel = null!;
    private Label _wordLabel = null!;

    private VocabManager _vocabManager = null!;
    private readonly Queue<string> _pendingWords = new();
    private bool _isShowing;

    public override void _Ready()
    {
        ProcessMode = ProcessModeEnum.Always;

        _notifPanel = GetNodeOrNull<PanelContainer>("NotifPanel") ?? CreateNotificationPanel();
        _wordLabel = GetNodeOrNull<Label>("NotifPanel/WordLabel") ?? CreateWordLabel();

        _vocabManager = GetNode<VocabManager>("/root/VocabManager");
        _vocabManager.VocabDiscovered += OnVocabDiscovered;

        _notifPanel.Modulate = new Color(1, 1, 1, 0);
        _notifPanel.Position = new Vector2(_notifPanel.Position.X, -30);
    }

    public override void _ExitTree()
    {
        if (_vocabManager is not null)
        {
            _vocabManager.VocabDiscovered -= OnVocabDiscovered;
        }
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

        _wordLabel.Text = $"Bagong salita: {word}";
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

    private PanelContainer CreateNotificationPanel()
    {
        var panel = new PanelContainer
        {
            Name = "NotifPanel",
            Size = new Vector2(180, 28),
            Position = new Vector2(8, -30)
        };
        AddChild(panel);
        return panel;
    }

    private Label CreateWordLabel()
    {
        var label = new Label
        {
            Name = "WordLabel",
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            AutowrapMode = TextServer.AutowrapMode.WordSmart
        };
        _notifPanel.AddChild(label);
        return label;
    }
}
