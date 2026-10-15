CREATE TABLE [Payments].[FactoringPreparationDetail] (
    [Id]                     INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdFactoringPreparation] INT NOT NULL,
    [IdAccountPayable]       INT NOT NULL,
    CONSTRAINT [PK_FactoringPreparationDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FactoringPreparationDetail_AccountPayable] FOREIGN KEY ([IdAccountPayable]) REFERENCES [Payments].[AccountPayable] ([Id]),
    CONSTRAINT [FK_FactoringPreparationDetail_FactoringPreparation] FOREIGN KEY ([IdFactoringPreparation]) REFERENCES [Payments].[FactoringPreparation] ([Id])
);


GO
ALTER TABLE [Payments].[FactoringPreparationDetail] NOCHECK CONSTRAINT [FK_FactoringPreparationDetail_AccountPayable];


GO
ALTER TABLE [Payments].[FactoringPreparationDetail] NOCHECK CONSTRAINT [FK_FactoringPreparationDetail_FactoringPreparation];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringPreparationDetail', @level2type = N'COLUMN', @level2name = N'IdAccountPayable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de la preparacion de factoring', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringPreparationDetail', @level2type = N'COLUMN', @level2name = N'IdFactoringPreparation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id incremental del detalle', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringPreparationDetail', @level2type = N'COLUMN', @level2name = N'Id';

