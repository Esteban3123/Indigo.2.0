CREATE TABLE [Payroll].[PayrollSequenceDetail] (
    [Id]                INT    IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PayrollSequenceId] INT    NOT NULL,
    [IdSequense]        INT    NOT NULL,
    [IdOperatingUnit]   INT    NULL,
    [Next]              BIGINT CONSTRAINT [DF_PayrollSequenceDetail_Next] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_PayrollSequenceDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PayrollSequenceDetail_OperatingUnit] FOREIGN KEY ([IdOperatingUnit]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_PayrollSequenceDetail_PayrollSequence] FOREIGN KEY ([PayrollSequenceId]) REFERENCES [Payroll].[PayrollSequence] ([Id]),
    CONSTRAINT [FK_PayrollSequenceDetail_Sequense] FOREIGN KEY ([IdSequense]) REFERENCES [Common].[Sequense] ([Id])
);


GO
ALTER TABLE [Payroll].[PayrollSequenceDetail] NOCHECK CONSTRAINT [FK_PayrollSequenceDetail_OperatingUnit];


GO
ALTER TABLE [Payroll].[PayrollSequenceDetail] NOCHECK CONSTRAINT [FK_PayrollSequenceDetail_PayrollSequence];


GO
ALTER TABLE [Payroll].[PayrollSequenceDetail] NOCHECK CONSTRAINT [FK_PayrollSequenceDetail_Sequense];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Siguiente numero a generar con la secuenacia', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSequenceDetail', @level2type = N'COLUMN', @level2name = N'Next';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad operativa asignada a la secuencia. Solo cuando el ambito es UO-Unidad Operativa, de lo contrario el campo es nulo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la secuencia base', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequense';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSequenceDetail', @level2type = N'COLUMN', @level2name = N'PayrollSequenceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSequenceDetail', @level2type = N'COLUMN', @level2name = N'Id';

