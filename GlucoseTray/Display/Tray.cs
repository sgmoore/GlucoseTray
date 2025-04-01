﻿namespace GlucoseTray.Display;

public interface ITray
{
    void Refresh(GlucoseReading result);
}

public class Tray : ITray
{
    private readonly ITrayIcon _icon;
    private readonly IGlucoseDisplayMapper _mapper;
    private readonly IScheduler _scheduler;

    public Tray(ITrayIcon icon, IGlucoseDisplayMapper mapper, IScheduler scheduler)
    {
        _icon = icon;
        _mapper = mapper;
        _scheduler = scheduler;

        RebuildContextMenu();
    }

    private void RebuildContextMenu()
    {
        _icon.ClearMenu();
        _icon.AddAutoRunMenu(_scheduler.HasTaskEnabled(), ToggleAutoRun);
        _icon.AddSettingsMenu();
        _icon.AddExitMenu();
    }

    private void ToggleAutoRun(object? sender, EventArgs e)
    {
        _scheduler.ToggleTask(!_scheduler.HasTaskEnabled());
        RebuildContextMenu();
    }

    public void Refresh(GlucoseReading result)
    {
        var display = _mapper.Map(result);
        _icon.RefreshIcon(display);
    }
}
