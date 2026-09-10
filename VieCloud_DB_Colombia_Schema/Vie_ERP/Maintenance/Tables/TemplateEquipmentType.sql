CREATE TABLE [Maintenance].[TemplateEquipmentType] (
    [Id]                   INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                 VARCHAR (20)   NOT NULL,
    [IdEquipmentType]      INT            NOT NULL,
    [Terms]                VARCHAR (8000) NOT NULL,
    [GeneralData]          VARCHAR (8000) NOT NULL,
    [OperationDescription] VARCHAR (8000) NOT NULL,
    [HandlingPrecautions]  VARCHAR (8000) NOT NULL,
    [Cleaning]             VARCHAR (8000) NOT NULL,
    [State]                BIT            NOT NULL,
    [TimeStamp]            ROWVERSION     NOT NULL,
    CONSTRAINT [PK_TemplateEquipmentType__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TemplateEquipmentType_EquipmentType] FOREIGN KEY ([IdEquipmentType]) REFERENCES [FixedAsset].[FixedAssetItemType] ([Id])
);




GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_TemplateEquipmentType]
    ON [Maintenance].[TemplateEquipmentType]([IdEquipmentType] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TemplateEquipmentType__State]
    ON [Maintenance].[TemplateEquipmentType]([State] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_TemplateEquipmentType_1]
    ON [Maintenance].[TemplateEquipmentType]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Control de concurrencia (TIMESTAMP), auditoría de cambios en plantilla de equipo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TemplateEquipmentType', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Control de Concurrencia', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TemplateEquipmentType', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TemplateEquipmentType', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado activo (1) o inactivo (0) de la plantilla; determina disponibilidad del equipo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TemplateEquipmentType', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del area 1-activo 0-inactivo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TemplateEquipmentType', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TemplateEquipmentType', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Protocolos y procedimientos de limpieza, desinfección y mantenimiento del equipo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TemplateEquipmentType', @level2type = N'COLUMN', @level2name = N'Cleaning';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Limpieza del equipo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TemplateEquipmentType', @level2type = N'COLUMN', @level2name = N'Cleaning';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TemplateEquipmentType', @level2type = N'COLUMN', @level2name = N'Cleaning';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Precauciones de manipulación, manejo seguro y protecciones operacionales del equipo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TemplateEquipmentType', @level2type = N'COLUMN', @level2name = N'HandlingPrecautions';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Preucuaciones de manejo del equipo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TemplateEquipmentType', @level2type = N'COLUMN', @level2name = N'HandlingPrecautions';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TemplateEquipmentType', @level2type = N'COLUMN', @level2name = N'HandlingPrecautions';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de funcionamiento, modo de operación y procedimientos estándar del equipo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TemplateEquipmentType', @level2type = N'COLUMN', @level2name = N'OperationDescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de Funcionamiento', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TemplateEquipmentType', @level2type = N'COLUMN', @level2name = N'OperationDescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TemplateEquipmentType', @level2type = N'COLUMN', @level2name = N'OperationDescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Datos generales y características técnicas del tipo de equipo (especificaciones, marca, modelo)', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TemplateEquipmentType', @level2type = N'COLUMN', @level2name = N'GeneralData';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Datos generales del equipo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TemplateEquipmentType', @level2type = N'COLUMN', @level2name = N'GeneralData';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TemplateEquipmentType', @level2type = N'COLUMN', @level2name = N'GeneralData';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Condiciones del entorno de almacenamiento, temperatura, humedad y requisitos ambientales', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TemplateEquipmentType', @level2type = N'COLUMN', @level2name = N'Terms';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Condiciones Del entorno del almacenamiento', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TemplateEquipmentType', @level2type = N'COLUMN', @level2name = N'Terms';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TemplateEquipmentType', @level2type = N'COLUMN', @level2name = N'Terms';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador FK del tipo de equipo relacionado a la plantilla (FK a FixedAssetItemType)', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TemplateEquipmentType', @level2type = N'COLUMN', @level2name = N'IdEquipmentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tipo de equipo relacionado a la plantilla creada', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TemplateEquipmentType', @level2type = N'COLUMN', @level2name = N'IdEquipmentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TemplateEquipmentType', @level2type = N'COLUMN', @level2name = N'IdEquipmentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único o identificador de la plantilla de mantenimiento del tipo de equipo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TemplateEquipmentType', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la plantilla', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TemplateEquipmentType', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TemplateEquipmentType', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (IDENTITY) de la plantilla de tipo de equipo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TemplateEquipmentType', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla de plantillas de tipos de equipo.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TemplateEquipmentType', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TemplateEquipmentType', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Plantillas de mantenimiento asociadas a tipos de equipos. Contiene los textos estándar de términos, datos generales, descripción de operación, precauciones de manejo y limpieza que se aplican a cada tipo de equipo en los procesos de mantenimiento.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TemplateEquipmentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TemplateEquipmentType';
