CREATE TABLE [Budget].[BudgetSequenceDetail] (
    [Id]                INT    IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdSequenseBudgetC] INT    NOT NULL,
    [IdSequense]        INT    NOT NULL,
    [IdOperatingUnit]   INT    NULL,
    [Next]              BIGINT CONSTRAINT [DF_SequenseBudgetD_Next] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_SequenseBudgetD] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SequenseBudgetD_OperatingUnit] FOREIGN KEY ([IdOperatingUnit]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_SequenseBudgetD_Sequense] FOREIGN KEY ([IdSequense]) REFERENCES [Common].[Sequense] ([Id]),
    CONSTRAINT [FK_SequenseBudgetD_SequenseBudgetC] FOREIGN KEY ([IdSequenseBudgetC]) REFERENCES [Budget].[BudgetSequence] ([Id])
);


GO
ALTER TABLE [Budget].[BudgetSequenceDetail] NOCHECK CONSTRAINT [FK_SequenseBudgetD_OperatingUnit];


GO
ALTER TABLE [Budget].[BudgetSequenceDetail] NOCHECK CONSTRAINT [FK_SequenseBudgetD_Sequense];


GO
ALTER TABLE [Budget].[BudgetSequenceDetail] NOCHECK CONSTRAINT [FK_SequenseBudgetD_SequenseBudgetC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Siguiente numero a generar con la secuenacia', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetSequenceDetail', @level2type = N'COLUMN', @level2name = N'Next';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad operativa asignada a la secuencia. Solo cuando el ambito es UO-Unidad Operativa, de lo contrario el campo es nulo', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la secuencia base', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequense';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequenseBudgetC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetSequenceDetail', @level2type = N'COLUMN', @level2name = N'Id';

