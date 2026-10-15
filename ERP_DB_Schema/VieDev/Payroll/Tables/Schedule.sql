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
ALTER TABLE [Payroll].[Schedule] NOCHECK CONSTRAINT [FK_Schedule_Employee];


GO
ALTER TABLE [Payroll].[Schedule] NOCHECK CONSTRAINT [FK_Schedule_FunctionalUnit];


GO
ALTER TABLE [Payroll].[Schedule] NOCHECK CONSTRAINT [FK_Schedule_ScheduleDetail_01];


GO
ALTER TABLE [Payroll].[Schedule] NOCHECK CONSTRAINT [FK_Schedule_ScheduleDetail_02];


GO
ALTER TABLE [Payroll].[Schedule] NOCHECK CONSTRAINT [FK_Schedule_ScheduleDetail_03];


GO
ALTER TABLE [Payroll].[Schedule] NOCHECK CONSTRAINT [FK_Schedule_ScheduleDetail_04];


GO
ALTER TABLE [Payroll].[Schedule] NOCHECK CONSTRAINT [FK_Schedule_ScheduleDetail_05];


GO
ALTER TABLE [Payroll].[Schedule] NOCHECK CONSTRAINT [FK_Schedule_ScheduleDetail_06];


GO
ALTER TABLE [Payroll].[Schedule] NOCHECK CONSTRAINT [FK_Schedule_ScheduleDetail_07];


GO
ALTER TABLE [Payroll].[Schedule] NOCHECK CONSTRAINT [FK_Schedule_ScheduleDetail_08];


GO
ALTER TABLE [Payroll].[Schedule] NOCHECK CONSTRAINT [FK_Schedule_ScheduleDetail_09];


GO
ALTER TABLE [Payroll].[Schedule] NOCHECK CONSTRAINT [FK_Schedule_ScheduleDetail_10];


GO
ALTER TABLE [Payroll].[Schedule] NOCHECK CONSTRAINT [FK_Schedule_ScheduleDetail_11];


GO
ALTER TABLE [Payroll].[Schedule] NOCHECK CONSTRAINT [FK_Schedule_ScheduleDetail_12];


GO
ALTER TABLE [Payroll].[Schedule] NOCHECK CONSTRAINT [FK_Schedule_ScheduleDetail_13];


GO
ALTER TABLE [Payroll].[Schedule] NOCHECK CONSTRAINT [FK_Schedule_ScheduleDetail_14];


GO
ALTER TABLE [Payroll].[Schedule] NOCHECK CONSTRAINT [FK_Schedule_ScheduleDetail_15];


GO
ALTER TABLE [Payroll].[Schedule] NOCHECK CONSTRAINT [FK_Schedule_ScheduleDetail_16];


GO
ALTER TABLE [Payroll].[Schedule] NOCHECK CONSTRAINT [FK_Schedule_ScheduleDetail_17];


GO
ALTER TABLE [Payroll].[Schedule] NOCHECK CONSTRAINT [FK_Schedule_ScheduleDetail_18];


GO
ALTER TABLE [Payroll].[Schedule] NOCHECK CONSTRAINT [FK_Schedule_ScheduleDetail_19];


GO
ALTER TABLE [Payroll].[Schedule] NOCHECK CONSTRAINT [FK_Schedule_ScheduleDetail_20];


GO
ALTER TABLE [Payroll].[Schedule] NOCHECK CONSTRAINT [FK_Schedule_ScheduleDetail_21];


GO
ALTER TABLE [Payroll].[Schedule] NOCHECK CONSTRAINT [FK_Schedule_ScheduleDetail_22];


GO
ALTER TABLE [Payroll].[Schedule] NOCHECK CONSTRAINT [FK_Schedule_ScheduleDetail_23];


GO
ALTER TABLE [Payroll].[Schedule] NOCHECK CONSTRAINT [FK_Schedule_ScheduleDetail_24];


GO
ALTER TABLE [Payroll].[Schedule] NOCHECK CONSTRAINT [FK_Schedule_ScheduleDetail_25];


GO
ALTER TABLE [Payroll].[Schedule] NOCHECK CONSTRAINT [FK_Schedule_ScheduleDetail_26];


GO
ALTER TABLE [Payroll].[Schedule] NOCHECK CONSTRAINT [FK_Schedule_ScheduleDetail_27];


GO
ALTER TABLE [Payroll].[Schedule] NOCHECK CONSTRAINT [FK_Schedule_ScheduleDetail_28];


GO
ALTER TABLE [Payroll].[Schedule] NOCHECK CONSTRAINT [FK_Schedule_ScheduleDetail_29];


GO
ALTER TABLE [Payroll].[Schedule] NOCHECK CONSTRAINT [FK_Schedule_ScheduleDetail_30];


GO
ALTER TABLE [Payroll].[Schedule] NOCHECK CONSTRAINT [FK_Schedule_ScheduleDetail_31];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día treinta y uno del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D31';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día treinta del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D30';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día veintinueve del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D29';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día veintiocho del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D28';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día veintisiete del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D27';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día veintiséis del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D26';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día veinticinco del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D25';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día veinticuatro del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D24';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día veintitres del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D23';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día veintidos del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D22';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día veinteuno del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D21';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día veinte del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D20';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día diecinueve del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D19';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día dieciocho del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D18';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día diecisiete del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D17';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día dieciséis del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D16';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día quince del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D15';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día catorce del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D14';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día trece del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D13';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día doce del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D12';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día once del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D11';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día decimo del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D10';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día noveno del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D09';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día octavo del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D08';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día septimo del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D07';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día sexto del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D06';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día quinto del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D05';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día cuarto del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D04';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día tercero del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D03';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día segundo del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D02';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día primero del calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Schedule', @level2type = N'COLUMN', @level2name = N'D01';

