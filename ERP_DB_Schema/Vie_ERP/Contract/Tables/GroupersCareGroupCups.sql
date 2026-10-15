CREATE TABLE [Contract].[GroupersCareGroupCups] (
    [Id]                  INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [GroupersCareGroupId] INT NOT NULL,
    [CUPSEntityId]        INT NOT NULL,
    CONSTRAINT [PK_GroupersCareGroupCups] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GroupersCareGroupCups_CUPSEntity] FOREIGN KEY ([CUPSEntityId]) REFERENCES [Contract].[CUPSEntity] ([Id]),
    CONSTRAINT [FK_GroupersCareGroupCups_GroupersCareGroup] FOREIGN KEY ([GroupersCareGroupId]) REFERENCES [Contract].[GroupersCareGroup] ([Id])
);


GO
ALTER TABLE [Contract].[GroupersCareGroupCups] NOCHECK CONSTRAINT [FK_GroupersCareGroupCups_CUPSEntity];


GO
ALTER TABLE [Contract].[GroupersCareGroupCups] NOCHECK CONSTRAINT [FK_GroupersCareGroupCups_GroupersCareGroup];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de entidad de CUPS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroupCups', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID del grupo de atención', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroupCups', @level2type = N'COLUMN', @level2name = N'GroupersCareGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroupCups', @level2type = N'COLUMN', @level2name = N'Id';

