CREATE TABLE [FixedAsset].[FixedAssetInventoryType] (
    [Id]               INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (20) NOT NULL,
    [Name]             VARCHAR (50) NOT NULL,
    [Type]             TINYINT      NOT NULL,
    [Status]           BIT          NOT NULL,
    [CreationUser]     VARCHAR (20) NOT NULL,
    [CreationDate]     DATETIME     NOT NULL,
    [ModificationUser] VARCHAR (20) NULL,
    [ModificationDate] DATETIME     NULL,
    [TimeStamp]        ROWVERSION   NOT NULL,
    CONSTRAINT [PK_InventoryType_1] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal de auditoría (TIMESTAMP SQL Server). Registra automáticamente el instante exacto de creación, modificación o cambio de estado del registro de tipo de inventario de activo fijo. Utilizado para control de versiones y trazabilidad.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInventoryType', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInventoryType', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInventoryType', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación (DATETIME). Indica cuándo fue actualizado por última vez el registro del tipo de inventario (código, nombre, tipo o estado).', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInventoryType', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Modificación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInventoryType', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInventoryType', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario responsable de la última modificación (VARCHAR 20, nullable). Identificador del usuario del sistema que realizó el cambio más reciente en el tipo de inventario.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInventoryType', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de Modificación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInventoryType', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInventoryType', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación (DATETIME). Marca el instante en que se registró por primera vez el tipo de inventario en el sistema.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInventoryType', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInventoryType', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInventoryType', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario responsable de la creación (VARCHAR 20). Identificador del usuario del sistema que originó el registro del tipo de inventario.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInventoryType', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de Creación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInventoryType', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInventoryType', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del tipo de inventario (BIT: 1=Activo, 0=Inactivo). Indica si el tipo está disponible para clasificar activos fijos o ha sido desactivado.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInventoryType', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado (1 - Activo, 0 - Inactivo)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInventoryType', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInventoryType', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación del tipo de inventario de activo fijo (TINYINT 1-10): 1=Equipo Biomédico, 2=Dispositivo Médico, 3=Equipo Industrial, 4=Construcciones y Edificaciones, 5=Mobiliario, 6=Equipos de Oficina, 7=Muebles de Oficina, 8=Terrenos, 9=Equipo de Transporte, 10=Otros.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInventoryType', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Inventario  1 - Equipo Biomédico  2 - Dispositivo Médico  3 - Equipo Industrial  4 - Construcciones y Edificaciones  5 - Moviliario  6 - Equipos de Oficina  7 - Muebles de Oficina  8 - Terrenos  9 - Equipo de transporte  10 - Otros    ', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInventoryType', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInventoryType', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo del tipo de inventario (VARCHAR 50). Etiqueta legible que identifica la categoría de activo fijo (ej: ''''Resonancia Magnética'''', ''''Quirófano'''', ''''Ambulancia'''').', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInventoryType', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del Tipo de Inventario', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInventoryType', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInventoryType', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del tipo de inventario (VARCHAR 20). Identificador alfanumérico para referencia rápida en procesos de clasificación y búsqueda de activos fijos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInventoryType', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del Tipo de Inventario', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInventoryType', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInventoryType', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (INT IDENTITY). Clave primaria que identifica unívocamente cada tipo de inventario de activo fijo en la tabla.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInventoryType', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInventoryType', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInventoryType', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipos de inventario de activos fijos: define las categorías o clasificaciones usadas para organizar los inventarios físicos de bienes (equipos, mobiliario, maquinaria, etc.), indicando su código, nombre, tipo y estado de vigencia.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInventoryType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInventoryType';
