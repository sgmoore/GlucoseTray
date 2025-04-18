﻿using GlucoseTray.Display;
using GlucoseTray.Read;
using Microsoft.Extensions.Options;
using NSubstitute;

namespace GlucoseTray.Tests.DSL.Display;

internal class DisplayProvider
{
    public readonly AppRunner Runner;
    public readonly ITray Tray;
    public readonly IGlucoseDisplayMapper GlucoseDisplayMapper;
    public readonly IGlucoseReadingMapper GlucoseReadingMapper;
    public readonly IAlertService AlertService;
    public readonly IScheduler Scheduler = Substitute.For<IScheduler>();
    public readonly ITrayIcon Icon = Substitute.For<ITrayIcon>();
    public readonly IGlucoseReader Reader = Substitute.For<IGlucoseReader>();
    public readonly IOptionsMonitor<AppSettings> Options = Substitute.For<IOptionsMonitor<AppSettings>>();
    public readonly IExternalCommunicationAdapter ExternalCommunicationAdapter = Substitute.For<IExternalCommunicationAdapter>();

    public DisplayProvider()
    {
        GlucoseDisplayMapper = new GlucoseDisplayMapper(Options);
        GlucoseReadingMapper = new GlucoseReadingMapper(Options);
        AlertService = new AlertService(Options);
        Tray = new Tray(Icon, GlucoseDisplayMapper, Scheduler, AlertService, Options);
        Runner = new AppRunner(Tray, Reader, Options);
    }
}
