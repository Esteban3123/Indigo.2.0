CREATE TABLE [Budget].[BudgetModificationDetail] (
    [Id]             INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ModificationId] INT             NOT NULL,
    [BudgetId]       INT             NOT NULL,
    [RevenueTypeId]  INT             NOT NULL,
    [Nature]         TINYINT         NOT NULL,
    [Value]          NUMERIC (18, 2) NOT NULL,
    CONSTRAINT [PK_ModificationDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ModificationDetail_Budget] FOREIGN KEY ([BudgetId]) REFERENCES [Budget].[Budget] ([Id]),
    CONSTRAINT [FK_ModificationDetail_Modification] FOREIGN KEY ([ModificationId]) REFERENCES [Budget].[BudgetModification] ([Id]),
    CONSTRAINT [FK_ModificationDetail_RevenueType] FOREIGN KEY ([RevenueTypeId]) REFERENCES [Budget].[RevenueType] ([Id])
);


GO
ALTER TABLE [Budget].[BudgetModificationDetail] NOCHECK CONSTRAINT [FK_ModificationDetail_Budget];


GO
ALTER TABLE [Budget].[BudgetModificationDetail] NOCHECK CONSTRAINT [FK_ModificationDetail_Modification];


GO
ALTER TABLE [Budget].[BudgetModificationDetail] NOCHECK CONSTRAINT [FK_ModificationDetail_RevenueType];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la modificacion', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetModificationDetail', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Obtiene o establece la naturaleza de la modificacion ( Debito = 1 Credito = 2)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetModificationDetail', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tipo de ingreso / egreso', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetModificationDetail', @level2type = N'COLUMN', @level2name = N'RevenueTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del presupuesto inicial', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetModificationDetail', @level2type = N'COLUMN', @level2name = N'BudgetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la modificacion de contrato del cabecera', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetModificationDetail', @level2type = N'COLUMN', @level2name = N'ModificationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de la modificacion', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetModificationDetail', @level2type = N'COLUMN', @level2name = N'Id';

