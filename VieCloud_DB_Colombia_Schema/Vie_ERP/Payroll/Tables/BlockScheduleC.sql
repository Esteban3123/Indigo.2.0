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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario Creación, identificador del usuario que creó el registro de bloqueo de nómina, VARCHAR(20), auditoría', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockScheduleC', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockScheduleC', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockScheduleC', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha Creación, timestamp DATETIME del registro de bloqueo, auditoría de nómina', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockScheduleC', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockScheduleC', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockScheduleC', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora Inicio Bloqueo Segunda Quincena, TIME, hora de bloqueo para la segunda quincena (días 16-31), cuadro de turnos', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockScheduleC', @level2type = N'COLUMN', @level2name = N'SecondFortnighHourBlockTyme';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora Inicio Bloqueo Segunda Quincena', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockScheduleC', @level2type = N'COLUMN', @level2name = N'SecondFortnighHourBlockTyme';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockScheduleC', @level2type = N'COLUMN', @level2name = N'SecondFortnighHourBlockTyme';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día Inicio Bloqueo Segunda Quincena, TINYINT, día del mes (16-31) cuando inicia el bloqueo de segunda quincena', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockScheduleC', @level2type = N'COLUMN', @level2name = N'SecondFortnightDayBlockTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dia Inicio Bloqueo Segunda Quincena', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockScheduleC', @level2type = N'COLUMN', @level2name = N'SecondFortnightDayBlockTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockScheduleC', @level2type = N'COLUMN', @level2name = N'SecondFortnightDayBlockTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora Inicio Bloqueo Primera Quincena, TIME, hora de bloqueo para la primera quincena (días 1-15), cuadro de turnos', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockScheduleC', @level2type = N'COLUMN', @level2name = N'FirstFortnighHourBlockTyme';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora Inicio Bloqueo Primera Quincena', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockScheduleC', @level2type = N'COLUMN', @level2name = N'FirstFortnighHourBlockTyme';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockScheduleC', @level2type = N'COLUMN', @level2name = N'FirstFortnighHourBlockTyme';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día Inicio Bloqueo Primera Quincena, TINYINT, día del mes (1-15) cuando inicia el bloqueo de primera quincena', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockScheduleC', @level2type = N'COLUMN', @level2name = N'FirstFortnightDayBlockTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Día Inicio Bloqueo Primera Quincena ', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockScheduleC', @level2type = N'COLUMN', @level2name = N'FirstFortnightDayBlockTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockScheduleC', @level2type = N'COLUMN', @level2name = N'FirstFortnightDayBlockTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora Inicio Bloqueo Nómina Mensual, TIME, hora de bloqueo para cuadro de turnos mensual, cierre de período', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockScheduleC', @level2type = N'COLUMN', @level2name = N'MonthInitialBlockTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora Inicio del Bloqueo de Cuadro de Turnos para Nómina Mensual', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockScheduleC', @level2type = N'COLUMN', @level2name = N'MonthInitialBlockTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockScheduleC', @level2type = N'COLUMN', @level2name = N'MonthInitialBlockTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día Inicio Bloqueo Nómina Mensual, TINYINT, día del mes en que inicia el bloqueo de nómina mensual', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockScheduleC', @level2type = N'COLUMN', @level2name = N'MonthBlockDay';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dia Inicio Bloqueo de Nómina Mensual', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockScheduleC', @level2type = N'COLUMN', @level2name = N'MonthBlockDay';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockScheduleC', @level2type = N'COLUMN', @level2name = N'MonthBlockDay';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de Nómina, TINYINT: 1=Mensual, 2=Quincenal, clasificación de período de pago de profesional de salud', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockScheduleC', @level2type = N'COLUMN', @level2name = N'PayrollType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Nómina:  1 - Mensual  2 - Quincenal', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockScheduleC', @level2type = N'COLUMN', @level2name = N'PayrollType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockScheduleC', @level2type = N'COLUMN', @level2name = N'PayrollType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de Bloqueo, TINYINT: 1=Automático, 2=Manual, método de cierre y bloqueo del cuadro de turnos', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockScheduleC', @level2type = N'COLUMN', @level2name = N'BlockType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Bloqueo:  1 - Automático  2 - Manual', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockScheduleC', @level2type = N'COLUMN', @level2name = N'BlockType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockScheduleC', @level2type = N'COLUMN', @level2name = N'BlockType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id Autoincrementable, INT IDENTITY(1,1), clave primaria única de configuración de bloqueo de nómina', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockScheduleC', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockScheduleC', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockScheduleC', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuración de bloqueos del calendario de nómina. Define los días y horarios en que se cierra (bloquea) el ciclo de liquidación, ya sea mensual o por quincena, según el tipo de nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockScheduleC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockScheduleC';
