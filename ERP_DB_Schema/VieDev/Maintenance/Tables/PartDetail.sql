CREATE TABLE [Maintenance].[PartDetail] (
    [Id]              INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdPart]          INT     NOT NULL,
    [IdEquipmentType] TINYINT NOT NULL,
    CONSTRAINT [PK_PartDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PartDetail_EquipmentType] FOREIGN KEY ([IdEquipmentType]) REFERENCES [Maintenance].[EquipmentType] ([Id]),
    CONSTRAINT [FK_PartDetail_PartDetail] FOREIGN KEY ([IdPart]) REFERENCES [Maintenance].[Part] ([Id])
);


GO
ALTER TABLE [Maintenance].[PartDetail] NOCHECK CONSTRAINT [FK_PartDetail_EquipmentType];


GO
ALTER TABLE [Maintenance].[PartDetail] NOCHECK CONSTRAINT [FK_PartDetail_PartDetail];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tipo de Accesorio Relacionado', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'PartDetail', @level2type = N'COLUMN', @level2name = N'IdEquipmentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Accesorio relacionado', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'PartDetail', @level2type = N'COLUMN', @level2name = N'IdPart';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla de relacion de accesorios y equipos', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'PartDetail', @level2type = N'COLUMN', @level2name = N'Id';

