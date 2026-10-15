CREATE TABLE [Budget].[SuspensionDetail] (
    [Id]           INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [SuspensionId] INT          NOT NULL,
    [BudgetId]     INT          NOT NULL,
    [InitialValue] NUMERIC (18) NOT NULL,
    [RaisedValue]  NUMERIC (18) NOT NULL,
    [Balance]      NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_SuspensionDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SuspensionDetail_Budget] FOREIGN KEY ([BudgetId]) REFERENCES [Budget].[Budget] ([Id]),
    CONSTRAINT [FK_SuspensionDetail_Suspension] FOREIGN KEY ([SuspensionId]) REFERENCES [Budget].[Suspension] ([Id])
);


GO
ALTER TABLE [Budget].[SuspensionDetail] NOCHECK CONSTRAINT [FK_SuspensionDetail_Budget];


GO
ALTER TABLE [Budget].[SuspensionDetail] NOCHECK CONSTRAINT [FK_SuspensionDetail_Suspension];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el saldo de la suspencion, cuando el saldo este en 0 es porque ya se levanto toda la suspencion  InitialValue - RaisedValue', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SuspensionDetail', @level2type = N'COLUMN', @level2name = N'Balance';

