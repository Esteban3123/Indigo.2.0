CREATE TABLE [FixedAsset].[ResponsibleFunctionalUnit] (
    [Id]               INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdResponsible]    INT NOT NULL,
    [IdFunctionalUnit] INT NOT NULL,
    CONSTRAINT [PK_ResponsibleFunctionalUnit] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ResponsibleFunctionalUnit_FunctionalUnit] FOREIGN KEY ([IdFunctionalUnit]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [FK_ResponsibleFunctionalUnit_ResponsibleFunctionalUnit] FOREIGN KEY ([IdResponsible]) REFERENCES [FixedAsset].[FixedAssetResponsible] ([Id])
);


GO
ALTER TABLE [FixedAsset].[ResponsibleFunctionalUnit] NOCHECK CONSTRAINT [FK_ResponsibleFunctionalUnit_FunctionalUnit];


GO
ALTER TABLE [FixedAsset].[ResponsibleFunctionalUnit] NOCHECK CONSTRAINT [FK_ResponsibleFunctionalUnit_ResponsibleFunctionalUnit];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la Unidad Funcional Responsable ', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'ResponsibleFunctionalUnit', @level2type = N'COLUMN', @level2name = N'Id';

