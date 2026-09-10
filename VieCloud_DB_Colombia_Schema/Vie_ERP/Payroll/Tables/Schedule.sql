CREATE TABLE [Payroll].[Schedule] (
    [Id]               INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [EmployeeId]       INT            NOT NULL,
    [Period]           VARCHAR (7)    NOT NULL,
    [FunctionalUnitId] INT            NOT NULL,
    [D01]              INT            NULL,
    [D02]              INT            NULL,
    [D03]              INT            NULL,
    [D04]              INT            NULL,
    [D05]              INT            NULL,
    [D06]              INT            NULL,
    [D07]              INT            NULL,
    [D08]              INT            NULL,
    [D09]              INT            NULL,
    [D10]              INT            NULL,
    [D11]              INT            NULL,
    [D12]              INT            NULL,
    [D13]              INT            NULL,
    [D14]              INT            NULL,
    [D15]              INT            NULL,
    [D16]              INT            NULL,
    [D17]              INT            NULL,
    [D18]              INT            NULL,
    [D19]              INT            NULL,
    [D20]              INT            NULL,
    [D21]              INT            NULL,
    [D22]              INT            NULL,
    [D23]              INT            NULL,
    [D24]              INT            NULL,
    [D25]              INT            NULL,
    [D26]              INT            NULL,
    [D27]              INT            NULL,
    [D28]              INT            NULL,
    [D29]              INT            NULL,
    [D30]              INT            NULL,
    [D31]              INT            NULL,
    [TotalHour]        NUMERIC (5, 2) NOT NULL,
    [State]            BIT            NOT NULL,
    CONSTRAINT [PK_Schedule__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Schedule_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id]),
    CONSTRAINT [FK_Schedule_FunctionalUnit] FOREIGN KEY ([FunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [FK_Schedule_ScheduleDetail_01] FOREIGN KEY ([D01]) REFERENCES [Payroll].[ScheduleDetail] ([Id]),
    CONSTRAINT [FK_Schedule_ScheduleDetail_02] FOREIGN KEY ([D02]) REFERENCES [Payroll].[ScheduleDetail] ([Id]),
    CONSTRAINT [FK_Schedule_ScheduleDetail_03] FOREIGN KEY ([D03]) REFERENCES [Payroll].[ScheduleDetail] ([Id]),
    CONSTRAINT [FK_Schedule_ScheduleDetail_04] FOREIGN KEY ([D04]) REFERENCES [Payroll].[ScheduleDetail] ([Id]),
    CONSTRAINT [FK_Schedule_ScheduleDetail_05] FOREIGN KEY ([D05]) REFERENCES [Payroll].[ScheduleDetail] ([Id]),
    CONSTRAINT [FK_Schedule_ScheduleDetail_06] FOREIGN KEY ([D06]) REFERENCES [Payroll].[ScheduleDetail] ([Id]),
    CONSTRAINT [FK_Schedule_ScheduleDetail_07] FOREIGN KEY ([D07]) REFERENCES [Payroll].[ScheduleDetail] ([Id]),
    CONSTRAINT [FK_Schedule_ScheduleDetail_08] FOREIGN KEY ([D08]) REFERENCES [Payroll].[ScheduleDetail] ([Id]),
    CONSTRAINT [FK_Schedule_ScheduleDetail_09] FOREIGN KEY ([D09]) REFERENCES [Payroll].[ScheduleDetail] ([Id]),
    CONSTRAINT [FK_Schedule_ScheduleDetail_10] FOREIGN KEY ([D10]) REFERENCES [Payroll].[ScheduleDetail] ([Id]),
    CONSTRAINT [FK_Schedule_ScheduleDetail_11] FOREIGN KEY ([D11]) REFERENCES [Payroll].[ScheduleDetail] ([Id]),
    CONSTRAINT [FK_Schedule_ScheduleDetail_12] FOREIGN KEY ([D12]) REFERENCES [Payroll].[ScheduleDetail] ([Id]),
    CONSTRAINT [FK_Schedule_ScheduleDetail_13] FOREIGN KEY ([D13]) REFERENCES [Payroll].[ScheduleDetail] ([Id]),
    CONSTRAINT [FK_Schedule_ScheduleDetail_14] FOREIGN KEY ([D14]) REFERENCES [Payroll].[ScheduleDetail] ([Id]),
    CONSTRAINT [FK_Schedule_ScheduleDetail_15] FOREIGN KEY ([D15]) REFERENCES [Payroll].[ScheduleDetail] ([Id]),
    CONSTRAINT [FK_Schedule_ScheduleDetail_16] FOREIGN KEY ([D16]) REFERENCES [Payroll].[ScheduleDetail] ([Id]),
    CONSTRAINT [FK_Schedule_ScheduleDetail_17] FOREIGN KEY ([D17]) REFERENCES [Payroll].[ScheduleDetail] ([Id]),
    CONSTRAINT [FK_Schedule_ScheduleDetail_18] FOREIGN KEY ([D18]) REFERENCES [Payroll].[ScheduleDetail] ([Id]),
    CONSTRAINT [FK_Schedule_ScheduleDetail_19] FOREIGN KEY ([D19]) REFERENCES [Payroll].[ScheduleDetail] ([Id]),
    CONSTRAINT [FK_Schedule_ScheduleDetail_20] FOREIGN KEY ([D20]) REFERENCES [Payroll].[ScheduleDetail] ([Id]),
    CONSTRAINT [FK_Schedule_ScheduleDetail_21] FOREIGN KEY ([D21]) REFERENCES [Payroll].[ScheduleDetail] ([Id]),
    CONSTRAINT [FK_Schedule_ScheduleDetail_22] FOREIGN KEY ([D22]) REFERENCES [Payroll].[ScheduleDetail] ([Id]),
    CONSTRAINT [FK_Schedule_ScheduleDetail_23] FOREIGN KEY ([D23]) REFERENCES [Payroll].[ScheduleDetail] ([Id]),
    CONSTRAINT [FK_Schedule_ScheduleDetail_24] FOREIGN KEY ([D24]) REFERENCES [Payroll].[ScheduleDetail] ([Id]),
    CONSTRAINT [FK_Schedule_ScheduleDetail_25] FOREIGN KEY ([D25]) REFERENCES [Payroll].[ScheduleDetail] ([Id]),
    CONSTRAINT [FK_Schedule_ScheduleDetail_26] FOREIGN KEY ([D26]) REFERENCES [Payroll].[ScheduleDetail] ([Id]),
    CONSTRAINT [FK_Schedule_ScheduleDetail_27] FOREIGN KEY ([D27]) REFERENCES [Payroll].[ScheduleDetail] ([Id]),
    CONSTRAINT [FK_Schedule_ScheduleDetail_28] FOREIGN KEY ([D28]) REFERENCES [Payroll].[ScheduleDetail] ([Id]),
    CONSTRAINT [FK_Schedule_ScheduleDetail_29] FOREIGN KEY ([D29]) REFERENCES [Payroll].[ScheduleDetail] ([Id]),
    CONSTRAINT [FK_Schedule_ScheduleDetail_30] FOREIGN KEY ([D30]) REFERENCES [Payroll].[ScheduleDetail] ([Id]),
    CONSTRAINT [FK_Schedule_ScheduleDetail_31] FOREIGN KEY ([D31]) REFERENCES [Payroll].[ScheduleDetail] ([Id])
);


GO
ALTER TABLE [Payroll].[Schedule] NOCHECK CONSTRAINT [FK_Schedule_ScheduleDetail_02];


GO
ALTER TABLE [Payroll].[Schedule] NOCHECK CONSTRAINT [FK_Schedule_ScheduleDetail_06];


GO
ALTER TABLE [Payroll].[Schedule] NOCHECK CONSTRAINT [FK_Schedule_ScheduleDetail_08];


GO
ALTER TABLE [Payroll].[Schedule] NOCHECK CONSTRAINT [FK_Schedule_ScheduleDetail_16];




GO



GO



GO



GO
ALTER TABLE [Payroll].[Schedule] NOCHECK CONSTRAINT [FK_Schedule_ScheduleDetail_02];


GO



GO



GO



GO
ALTER TABLE [Payroll].[Schedule] NOCHECK CONSTRAINT [FK_Schedule_ScheduleDetail_06];


GO



GO
ALTER TABLE [Payroll].[Schedule] NOCHECK CONSTRAINT [FK_Schedule_ScheduleDetail_08];


GO



GO



GO



GO



GO



GO



GO



GO
ALTER TABLE [Payroll].[Schedule] NOCHECK CONSTRAINT [FK_Schedule_ScheduleDetail_16];


GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_Schedule__EmployeeId]
    ON [Payroll].[Schedule]([EmployeeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Schedule__FunctionalUnitId_Period]
    ON [Payroll].[Schedule]([FunctionalUnitId] ASC, [Period] ASC)
    INCLUDE ([EmployeeId], [TotalHour], [State]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la agenda, activo o inactivo, indicador BIT de vigencia del cronograma', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la agenda', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sumatoria total de horas trabajadas en el período, acumulativo de días (tipo NUMERIC 5,2)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'TotalHour';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sumatoria del total de horas', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'TotalHour';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'TotalHour';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día treinta y uno del calendario, referencia a ScheduleDetail con turno/jornada asignada', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D31';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Día treinta y uno del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D31';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D31';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día treinta del calendario, referencia a ScheduleDetail con turno/jornada asignada', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D30';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Día treinta del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D30';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D30';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día veintinueve del calendario, referencia a ScheduleDetail con turno/jornada asignada', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D29';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Día veintinueve del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D29';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D29';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día veintiocho del calendario, referencia a ScheduleDetail con turno/jornada asignada', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D28';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Día veintiocho del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D28';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D28';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día veintisiete del calendario, referencia a ScheduleDetail con turno/jornada asignada', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D27';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Día veintisiete del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D27';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D27';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día veintiséis del calendario, referencia a ScheduleDetail con turno/jornada asignada', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D26';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Día veintiséis del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D26';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D26';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día veinticinco del calendario, referencia a ScheduleDetail con turno/jornada asignada', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D25';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Día veinticinco del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D25';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D25';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día veinticuatro del calendario, referencia a ScheduleDetail con turno/jornada asignada', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D24';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Día veinticuatro del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D24';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D24';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día veintitrés del calendario, referencia a ScheduleDetail con turno/jornada asignada', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D23';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Día veintitres del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D23';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D23';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día veintidós del calendario, referencia a ScheduleDetail con turno/jornada asignada', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D22';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Día veintidos del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D22';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D22';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día veintiuno del calendario, referencia a ScheduleDetail con turno/jornada asignada', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D21';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Día veinteuno del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D21';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D21';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día veinte del calendario, referencia a ScheduleDetail con turno/jornada asignada', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D20';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Día veinte del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D20';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D20';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día diecinueve del calendario, referencia a ScheduleDetail con turno/jornada asignada', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D19';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Día diecinueve del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D19';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D19';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día dieciocho del calendario, referencia a ScheduleDetail con turno/jornada asignada', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D18';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Día dieciocho del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D18';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D18';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día diecisiete del calendario, referencia a ScheduleDetail con turno/jornada asignada', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D17';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Día diecisiete del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D17';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D17';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día dieciséis del calendario, referencia a ScheduleDetail con turno/jornada asignada', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D16';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Día dieciséis del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D16';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D16';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día quince del calendario, referencia a ScheduleDetail con turno/jornada asignada', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D15';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Día quince del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D15';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D15';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día catorce del calendario, referencia a ScheduleDetail con turno/jornada asignada', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D14';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Día catorce del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D14';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D14';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día trece del calendario, referencia a ScheduleDetail con turno/jornada asignada', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D13';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Día trece del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D13';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D13';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día doce del calendario, referencia a ScheduleDetail con turno/jornada asignada', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D12';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Día doce del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D12';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D12';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día once del calendario, referencia a ScheduleDetail con turno/jornada asignada', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D11';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Día once del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D11';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D11';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día diez del calendario, referencia a ScheduleDetail con turno/jornada asignada', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D10';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Día decimo del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D10';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D10';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día nueve del calendario, referencia a ScheduleDetail con turno/jornada asignada', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D09';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Día noveno del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D09';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D09';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día ocho del calendario, referencia a ScheduleDetail con turno/jornada asignada', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D08';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Día octavo del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D08';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D08';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día siete del calendario, referencia a ScheduleDetail con turno/jornada asignada', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D07';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Día septimo del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D07';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D07';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día seis del calendario, referencia a ScheduleDetail con turno/jornada asignada', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D06';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Día sexto del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D06';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D06';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día cinco del calendario, referencia a ScheduleDetail con turno/jornada asignada', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D05';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Día quinto del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D05';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D05';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día cuatro del calendario, referencia a ScheduleDetail con turno/jornada asignada', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D04';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Día cuarto del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D04';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D04';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día tres del calendario, referencia a ScheduleDetail con turno/jornada asignada', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D03';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Día tercero del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D03';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D03';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día dos del calendario, referencia a ScheduleDetail con turno/jornada asignada', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D02';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Día segundo del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D02';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D02';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día uno del calendario, referencia a ScheduleDetail con turno/jornada asignada', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D01';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Día primero del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D01';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D01';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK Id de unidad funcional/centro de costo donde labora el empleado en el período', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'FK Id centro de costo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Período de nómina en formato MM/YYYY (mes/año) del cronograma de turnos', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'Period';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Periodo del turno (mm/yyyy)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'Period';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'Period';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK Id del empleado, trabajador o profesional de la salud al que pertenece la agenda', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'EmployeeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'FK Id del empleado ', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'EmployeeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'EmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumerico (INT IDENTITY) de cada registro de cronograma de nómina', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'Id';


GO


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Turno o programación laboral mensual de cada empleado por unidad funcional. Registra, día a día, el tipo de turno o jornada asignado durante un período (mes/año), junto con el total de horas programadas y el estado de vigencia del horario.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule';
