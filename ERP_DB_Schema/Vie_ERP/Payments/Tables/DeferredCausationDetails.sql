CREATE TABLE [Payments].[DeferredCausationDetails] (
    [Id]                  INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdDeferredCausation] INT             NOT NULL,
    [IdMainAccount]       INT             NOT NULL,
    [IdCostCenter]        INT             NULL,
    [Value]               DECIMAL (20, 2) NOT NULL,
    [Nature]              TINYINT         NOT NULL,
    [DateNextPeriod]      DATETIME        NOT NULL,
    CONSTRAINT [PK_DeferredCausationD] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DeferredCausationD_CostCenter] FOREIGN KEY ([IdCostCenter]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_DeferredCausationD_PUC] FOREIGN KEY ([IdMainAccount]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_DeferredCausationDetails_DeferredCausation] FOREIGN KEY ([IdDeferredCausation]) REFERENCES [Payments].[DeferredCausation] ([Id])
);


GO
ALTER TABLE [Payments].[DeferredCausationDetails] NOCHECK CONSTRAINT [FK_DeferredCausationD_CostCenter];


GO
ALTER TABLE [Payments].[DeferredCausationDetails] NOCHECK CONSTRAINT [FK_DeferredCausationD_PUC];


GO
ALTER TABLE [Payments].[DeferredCausationDetails] NOCHECK CONSTRAINT [FK_DeferredCausationDetails_DeferredCausation];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Siguiente Período', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationDetails', @level2type = N'COLUMN', @level2name = N'DateNextPeriod';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Naturaleza: 1 - Débito, 2 - Crédito', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationDetails', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationDetails', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Centro de costo', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationDetails', @level2type = N'COLUMN', @level2name = N'IdCostCenter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta de gasto', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationDetails', @level2type = N'COLUMN', @level2name = N'IdMainAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la causación diferida cabecera', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationDetails', @level2type = N'COLUMN', @level2name = N'IdDeferredCausation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationDetails', @level2type = N'COLUMN', @level2name = N'Id';

