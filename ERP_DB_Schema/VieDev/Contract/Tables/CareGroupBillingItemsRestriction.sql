CREATE TABLE [Contract].[CareGroupBillingItemsRestriction] (
    [Id]                        INT IDENTITY (1, 1) NOT NULL,
    [CareGroupId]               INT NOT NULL,
    [BillingItemsRestrictionId] INT NOT NULL,
    CONSTRAINT [PK_CareGroupBillingItemsRestriction] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CareGroupBillingItemsRestriction_BillingItemsRestriction] FOREIGN KEY ([BillingItemsRestrictionId]) REFERENCES [Contract].[BillingItemsRestriction] ([Id]),
    CONSTRAINT [FK_CareGroupBillingItemsRestriction_CareGroup] FOREIGN KEY ([CareGroupId]) REFERENCES [Contract].[CareGroup] ([Id])
);


GO
ALTER TABLE [Contract].[CareGroupBillingItemsRestriction] NOCHECK CONSTRAINT [FK_CareGroupBillingItemsRestriction_BillingItemsRestriction];


GO
ALTER TABLE [Contract].[CareGroupBillingItemsRestriction] NOCHECK CONSTRAINT [FK_CareGroupBillingItemsRestriction_CareGroup];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la restricción del articulo de facturación.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroupBillingItemsRestriction', @level2type = N'COLUMN', @level2name = N'BillingItemsRestrictionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Grupo de Atencion', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroupBillingItemsRestriction', @level2type = N'COLUMN', @level2name = N'CareGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroupBillingItemsRestriction', @level2type = N'COLUMN', @level2name = N'Id';

