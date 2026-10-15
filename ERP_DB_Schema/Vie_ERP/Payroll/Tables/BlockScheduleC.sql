CREATE TABLE [Payroll].[BlockScheduleC] (
    [Id]                          INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [BlockType]                   TINYINT      CONSTRAINT [DF_BlockScheduleC_BlockType] DEFAULT ((2)) NOT NULL,
    [PayrollType]                 TINYINT      NOT NULL,
    [MonthBlockDay]               TINYINT      NULL,
    [MonthInitialBlockTime]       TIME (7)     NULL,
    [FirstFortnightDayBlockTime]  TINYINT      NULL,
    [FirstFortnighHourBlockTyme]  TIME (7)     NULL,
    [SecondFortnightDayBlockTime] TINYINT      NULL,
    [SecondFortnighHourBlockTyme] TIME (7)     NULL,
    [CreationDate]                DATETIME     NOT NULL,
    [CreationUser]                VARCHAR (20) NOT NULL,
    CONSTRAINT [PK_BlockScheduleC] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockScheduleC', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockScheduleC', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora Inicio Bloqueo Segunda Quincena', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockScheduleC', @level2type = N'COLUMN', @level2name = N'SecondFortnighHourBlockTyme';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dia Inicio Bloqueo Segunda Quincena', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockScheduleC', @level2type = N'COLUMN', @level2name = N'SecondFortnightDayBlockTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora Inicio Bloqueo Primera Quincena', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockScheduleC', @level2type = N'COLUMN', @level2name = N'FirstFortnighHourBlockTyme';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Día Inicio Bloqueo Primera Quincena ', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockScheduleC', @level2type = N'COLUMN', @level2name = N'FirstFortnightDayBlockTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora Inicio del Bloqueo de Cuadro de Turnos para Nómina Mensual', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockScheduleC', @level2type = N'COLUMN', @level2name = N'MonthInitialBlockTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dia Inicio Bloqueo de Nómina Mensual', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockScheduleC', @level2type = N'COLUMN', @level2name = N'MonthBlockDay';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Nómina:  1 - Mensual  2 - Quincenal', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockScheduleC', @level2type = N'COLUMN', @level2name = N'PayrollType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Bloqueo:  1 - Automático  2 - Manual', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockScheduleC', @level2type = N'COLUMN', @level2name = N'BlockType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockScheduleC', @level2type = N'COLUMN', @level2name = N'Id';

