CREATE TABLE [Maintenance].[EquipmentRegistration] (
    [Id]                        INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FixedAssetPhysicalAssetId] INT      NOT NULL,
    [InventoryTypeId]           INT      NOT NULL,
    [LifeTime]                  INT      NOT NULL,
    [MeasurementUnit]           CHAR (1) NOT NULL,
    [IdResponsible]             INT      NOT NULL,
    [IdManufacturer]            INT      NOT NULL,
    [IdSeller]                  INT      NOT NULL,
    [EquipmentTypeId]           INT      NOT NULL,
    [InstallationDate]          DATETIME NOT NULL,
    [InitialOperationDate]      DATETIME NOT NULL,
    [WarrantyExpirationDate]    DATETIME NOT NULL,
    [ManufactureDate]           DATETIME NOT NULL,
    [FeedingSource]             CHAR (1) NULL,
    [FrequencyComputerUse]      CHAR (1) NULL,
    [PredominantTechnology]     CHAR (1) NULL,
    [IdEquipmentFunction]       SMALLINT NULL,
    [Use]                       CHAR (1) NULL,
    [IdPhysicalRisk]            INT      NULL,
    [IdEquipmentRequirement]    INT      NULL,
    [IdEquipmentHistory]        SMALLINT NULL,
    [Photo]                     IMAGE    NULL,
    [State]                     BIT      NOT NULL,
    CONSTRAINT [PK_EquipmentRegistration__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EquipmentRegistration_EquipmentFunction] FOREIGN KEY ([IdEquipmentFunction]) REFERENCES [Maintenance].[EquipmentFunction] ([Id]),
    CONSTRAINT [FK_EquipmentRegistration_EquipmentHistory] FOREIGN KEY ([IdEquipmentHistory]) REFERENCES [Maintenance].[EquipmentHistory] ([Id]),
    CONSTRAINT [FK_EquipmentRegistration_EquipmentRequirement] FOREIGN KEY ([IdEquipmentRequirement]) REFERENCES [Maintenance].[EquipmentRequirement] ([Id]),
    CONSTRAINT [FK_EquipmentRegistration_EquipmentType] FOREIGN KEY ([EquipmentTypeId]) REFERENCES [FixedAsset].[FixedAssetItemType] ([Id]),
    CONSTRAINT [FK_EquipmentRegistration_FixedAssetInventoryType] FOREIGN KEY ([InventoryTypeId]) REFERENCES [FixedAsset].[FixedAssetInventoryType] ([Id]),
    CONSTRAINT [FK_EquipmentRegistration_FixedAssetPhysicalAsset] FOREIGN KEY ([FixedAssetPhysicalAssetId]) REFERENCES [FixedAsset].[FixedAssetPhysicalAsset] ([Id]),
    CONSTRAINT [FK_EquipmentRegistration_PhysicalRisk] FOREIGN KEY ([IdPhysicalRisk]) REFERENCES [Maintenance].[PhysicalRisk] ([Id]),
    CONSTRAINT [FK_EquipmentRegistration_Supplier] FOREIGN KEY ([IdManufacturer]) REFERENCES [Common].[Supplier] ([Id]),
    CONSTRAINT [FK_EquipmentRegistration_Supplier1] FOREIGN KEY ([IdSeller]) REFERENCES [Common].[Supplier] ([Id]),
    CONSTRAINT [FK_EquipmentRegistration_Supplier2] FOREIGN KEY ([IdResponsible]) REFERENCES [FixedAsset].[FixedAssetResponsible] ([Id])
);


GO
ALTER TABLE [Maintenance].[EquipmentRegistration] NOCHECK CONSTRAINT [FK_EquipmentRegistration_EquipmentFunction];


GO
ALTER TABLE [Maintenance].[EquipmentRegistration] NOCHECK CONSTRAINT [FK_EquipmentRegistration_EquipmentHistory];


GO
ALTER TABLE [Maintenance].[EquipmentRegistration] NOCHECK CONSTRAINT [FK_EquipmentRegistration_EquipmentRequirement];


GO
ALTER TABLE [Maintenance].[EquipmentRegistration] NOCHECK CONSTRAINT [FK_EquipmentRegistration_EquipmentType];


GO
ALTER TABLE [Maintenance].[EquipmentRegistration] NOCHECK CONSTRAINT [FK_EquipmentRegistration_FixedAssetInventoryType];


GO
ALTER TABLE [Maintenance].[EquipmentRegistration] NOCHECK CONSTRAINT [FK_EquipmentRegistration_FixedAssetPhysicalAsset];


GO
ALTER TABLE [Maintenance].[EquipmentRegistration] NOCHECK CONSTRAINT [FK_EquipmentRegistration_PhysicalRisk];


GO
ALTER TABLE [Maintenance].[EquipmentRegistration] NOCHECK CONSTRAINT [FK_EquipmentRegistration_Supplier];


GO
ALTER TABLE [Maintenance].[EquipmentRegistration] NOCHECK CONSTRAINT [FK_EquipmentRegistration_Supplier1];


GO
ALTER TABLE [Maintenance].[EquipmentRegistration] NOCHECK CONSTRAINT [FK_EquipmentRegistration_Supplier2];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_EquipmentRegistration__FixedAssetPhysicalAssetId]
    ON [Maintenance].[EquipmentRegistration]([FixedAssetPhysicalAssetId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del consumible 1-Activo 0-Inactivo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Foto del Registro del equipo.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'Photo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Historia del equipo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'IdEquipmentHistory';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id. Requisito de equipo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'IdEquipmentRequirement';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID del riesgo físico', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'IdPhysicalRisk';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Uso del equipo 1=Medico;2=Basico;3=Apoyo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'Use';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clasificacion Biomedica 1=Diagnóstico;2=Tratamiento y Mantenimiento de la vida;3=Rehabilitación;4=Prevención;5=Analisis de Laboratorio', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'IdEquipmentFunction';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tecnologia Predominante  1=Electrico;2=Electrónico;3=Mecánico;4=Electromecánico;5=Hidraulico;6=Neumático;7=Vapor;8=Solar', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'PredominantTechnology';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Frecuencia de Uso del Equipo  1 : < 2 Dias  2 : 2 y 3 Dias.  3 : > 5 Dias.  ', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'FrequencyComputerUse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fuente de alimentacion 1=Agua;2=Aire;3=Gas;4=Vapor;5=Derivados del Petroleo;6=Electricidad;7=Energia Solar;8=Otros', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'FeedingSource';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la fabricacion del equipo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'ManufactureDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de vencimiento de la garantia', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'WarrantyExpirationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Inicial de la operacion', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'InitialOperationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la instalacion', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'InstallationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tipo de equipo asociado al registro del equipo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'EquipmentTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del vendedor ', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'IdSeller';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del fabricante relacionado', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'IdManufacturer';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del responsable del registro del equipo.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'IdResponsible';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de medida de la vida Util 1-Año 2- Meses 3-Dias', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'MeasurementUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Vida util del equipo esta clasificada en años', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'LifeTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tipo de inventario asociado a la recepcion del equipo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'InventoryTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'FixedAssetPhysicalAssetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla de Registro del Equipo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'Id';

