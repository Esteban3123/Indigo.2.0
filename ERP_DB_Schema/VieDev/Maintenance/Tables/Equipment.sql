CREATE TABLE [Maintenance].[Equipment] (
    [Id]                       INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                     VARCHAR (20)   NOT NULL,
    [Name]                     VARCHAR (100)  NOT NULL,
    [Comments]                 VARCHAR (4000) NOT NULL,
    [UnitMetric]               TINYINT        NOT NULL,
    [IdEquipmentType]          INT            NOT NULL,
    [LifeTime]                 TINYINT        NOT NULL,
    [State]                    BIT            NOT NULL,
    [TimeStamp]                ROWVERSION     NOT NULL,
    [IdTecnicalEquipmentSheet] INT            NOT NULL,
    CONSTRAINT [PK_Equipment] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Equipment_EquipmentType] FOREIGN KEY ([IdEquipmentType]) REFERENCES [FixedAsset].[FixedAssetItemType] ([Id]),
    CONSTRAINT [FK_Equipment_TecnicalEquipmentSheet] FOREIGN KEY ([IdTecnicalEquipmentSheet]) REFERENCES [Maintenance].[TechnicalEquipmentSheet] ([Id])
);


GO
ALTER TABLE [Maintenance].[Equipment] NOCHECK CONSTRAINT [FK_Equipment_EquipmentType];


GO
ALTER TABLE [Maintenance].[Equipment] NOCHECK CONSTRAINT [FK_Equipment_TecnicalEquipmentSheet];


GO
CREATE NONCLUSTERED INDEX [IX_Equipment__State]
    ON [Maintenance].[Equipment]([State] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_Equipment__Code]
    ON [Maintenance].[Equipment]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id hoja de equipo técnico', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Equipment', @level2type = N'COLUMN', @level2name = N'IdTecnicalEquipmentSheet';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Control de concurrencia', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Equipment', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del equipo 1-Activo 0-Inactivo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Equipment', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Vida util del equipo esta claisificada en años', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Equipment', @level2type = N'COLUMN', @level2name = N'LifeTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id el tipo de equipo relacionado al equipo.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Equipment', @level2type = N'COLUMN', @level2name = N'IdEquipmentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de Medida', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Equipment', @level2type = N'COLUMN', @level2name = N'UnitMetric';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Comentarios acerca del equipo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Equipment', @level2type = N'COLUMN', @level2name = N'Comments';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del equipo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Equipment', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del equipo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Equipment', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla de equipos', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Equipment', @level2type = N'COLUMN', @level2name = N'Id';

