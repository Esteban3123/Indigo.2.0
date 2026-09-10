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



GO



GO



GO



GO



GO



GO



GO





GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_EquipmentRegistration__FixedAssetPhysicalAssetId]
    ON [Maintenance].[EquipmentRegistration]([FixedAssetPhysicalAssetId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado activo/inactivo del equipo: 1=Activo, 0=Inactivo. Indica si el equipo está operativo en el centro de atención.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del consumible 1-Activo 0-Inactivo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fotografía o imagen digital del equipo registrado. Documento visual para identificación y auditoría del bien.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'Photo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Foto del Registro del equipo.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'Photo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'Photo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la historia/historial del equipo. Referencia a Maintenance.EquipmentHistory para seguimiento de eventos y cambios.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'IdEquipmentHistory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Historia del equipo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'IdEquipmentHistory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'IdEquipmentHistory';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del requisito técnico o normativo del equipo. Referencia a Maintenance.EquipmentRequirement para cumplimiento regulatorio.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'IdEquipmentRequirement';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id. Requisito de equipo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'IdEquipmentRequirement';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'IdEquipmentRequirement';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del riesgo físico asociado al equipo. Referencia a Maintenance.PhysicalRisk para clasificación de peligros bioquímicos, eléctricos o mecánicos.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'IdPhysicalRisk';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del riesgo físico', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'IdPhysicalRisk';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'IdPhysicalRisk';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Uso clínico del equipo: 1=Médico (diagnóstico/tratamiento), 2=Básico (administrativo), 3=Apoyo (infraestructura). Categoría de utilización en el centro.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'Use';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Uso del equipo 1=Medico;2=Basico;3=Apoyo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'Use';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'Use';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación biomédica internacional: 1=Diagnóstico, 2=Tratamiento/mantenimiento vida, 3=Rehabilitación, 4=Prevención, 5=Análisis laboratorio. Según regulaciones INVIMA.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'IdEquipmentFunction';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clasificacion Biomedica 1=Diagnóstico;2=Tratamiento y Mantenimiento de la vida;3=Rehabilitación;4=Prevención;5=Analisis de Laboratorio', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'IdEquipmentFunction';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'IdEquipmentFunction';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tecnología predominante del equipo: 1=Eléctrico, 2=Electrónico, 3=Mecánico, 4=Electromecánico, 5=Hidráulico, 6=Neumático, 7=Vapor, 8=Solar. Base para mantenimiento especializado.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'PredominantTechnology';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tecnologia Predominante  1=Electrico;2=Electrónico;3=Mecánico;4=Electromecánico;5=Hidraulico;6=Neumático;7=Vapor;8=Solar', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'PredominantTechnology';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'PredominantTechnology';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Frecuencia de uso del equipo: 1=<2 días, 2=2-3 días, 3=>5 días. Determina intervalo mantenimiento preventivo en centro de atención.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'FrequencyComputerUse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Frecuencia de Uso del Equipo  1 : < 2 Dias  2 : 2 y 3 Dias.  3 : > 5 Dias.  ', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'FrequencyComputerUse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'FrequencyComputerUse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fuente de alimentación/energía: 1=Agua, 2=Aire, 3=Gas, 4=Vapor, 5=Derivados petróleo, 6=Electricidad, 7=Energía solar, 8=Otros. Infraestructura requerida.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'FeedingSource';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fuente de alimentacion 1=Agua;2=Aire;3=Gas;4=Vapor;5=Derivados del Petroleo;6=Electricidad;7=Energia Solar;8=Otros', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'FeedingSource';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'FeedingSource';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de fabricación (DATETIME). Determina antigüedad real del equipo y cálculo de vida útil remanente.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'ManufactureDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la fabricacion del equipo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'ManufactureDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'ManufactureDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de vencimiento de la garantía (DATETIME). Define cobertura del fabricante para reparación/reemplazo sin costo.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'WarrantyExpirationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de vencimiento de la garantia', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'WarrantyExpirationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'WarrantyExpirationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha inicial de operación (DATETIME). Marca inicio de uso clínico efectivo en el centro de atención.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'InitialOperationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Inicial de la operacion', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'InitialOperationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'InitialOperationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de instalación (DATETIME). Registro de puesta en servicio y ubicación asignada del equipo.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'InstallationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la instalacion', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'InstallationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'InstallationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tipo de equipo (FK a FixedAsset.FixedAssetItemType). Clasificación: ventilador, monitor, quirófano, laboratorio, etc.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'EquipmentTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tipo de equipo asociado al registro del equipo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'EquipmentTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'EquipmentTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del vendedor/distribuidor (FK a Common.Supplier). Contacto para garantía, repuestos y actualizaciones.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'IdSeller';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del vendedor ', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'IdSeller';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'IdSeller';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del fabricante (FK a Common.Supplier). Proveedor para soporte técnico, actualizaciones normativas y documentación.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'IdManufacturer';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del fabricante relacionado', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'IdManufacturer';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'IdManufacturer';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del responsable del registro (FK a FixedAsset.FixedAssetResponsible). Profesional/unidad funcional encargada del equipo.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'IdResponsible';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del responsable del registro del equipo.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'IdResponsible';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'IdResponsible';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida de vida útil: 1=Año, 2=Meses, 3=Días. Normalización para cálculo de depreciación y reemplazo.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'MeasurementUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de medida de la vida Util 1-Año 2- Meses 3-Dias', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'MeasurementUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'MeasurementUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vida útil del equipo en unidades (MeasurementUnit). Años esperados de operación según fabricante y regulación técnica.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'LifeTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Vida util del equipo esta clasificada en años', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'LifeTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'LifeTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tipo de inventario (FK a FixedAsset.FixedAssetInventoryType). Clasificación contable: activo fijo, consumible, prestado.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'InventoryTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tipo de inventario asociado a la recepcion del equipo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'InventoryTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'InventoryTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo/identificador único del bien físico (FK a FixedAsset.FixedAssetPhysicalAsset). Enlace con registro de activo fijo.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'FixedAssetPhysicalAssetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'FixedAssetPhysicalAssetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'FixedAssetPhysicalAssetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (INT IDENTITY) del registro de equipo. Clave primaria para auditoría y trazabilidad en mantenimiento biomedico.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla de Registro del Equipo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de equipos biomédicos y activos fijos de la institución. Contiene la ficha técnica de cada equipo: vida útil, fechas de instalación y garantía, fabricante, vendedor, responsable, tipo de tecnología y estado operativo.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentRegistration';
