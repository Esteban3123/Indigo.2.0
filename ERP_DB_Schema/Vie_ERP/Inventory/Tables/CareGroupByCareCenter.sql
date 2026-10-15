CREATE TABLE [Inventory].[CareGroupByCareCenter] (
    [Id]             INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CareGroupId]    INT       NOT NULL,
    [CareCenterCode] CHAR (10) NOT NULL,
    CONSTRAINT [PK_CareGroupByCareCenter__Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_CareGroupByCareCenter__CareGroupId__CareCenterCode]
    ON [Inventory].[CareGroupByCareCenter]([CareGroupId] ASC, [CareCenterCode] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id o código del centro de atención', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'CareGroupByCareCenter', @level2type = N'COLUMN', @level2name = N'CareCenterCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del grupo de atención', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'CareGroupByCareCenter', @level2type = N'COLUMN', @level2name = N'CareGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'CareGroupByCareCenter', @level2type = N'COLUMN', @level2name = N'Id';

