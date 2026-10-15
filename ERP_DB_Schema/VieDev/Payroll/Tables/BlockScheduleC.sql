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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Nómina:  1 - Mensual  2 - Quincenal', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockScheduleC', @level2type = N'COLUMN', @level2name = N'PayrollType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Bloqueo:  1 - Automático  2 - Manual', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockScheduleC', @level2type = N'COLUMN', @level2name = N'BlockType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockScheduleC', @level2type = N'COLUMN', @level2name = N'Id';

