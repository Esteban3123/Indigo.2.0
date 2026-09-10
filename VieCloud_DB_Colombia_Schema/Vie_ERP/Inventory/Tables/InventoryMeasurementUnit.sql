CREATE TABLE [Inventory].[InventoryMeasurementUnit] (
    [Id]                     INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                   VARCHAR (20)    NOT NULL,
    [Name]                   VARCHAR (100)   NOT NULL,
    [Abbreviation]           VARCHAR (10)    NOT NULL,
    [UnitType]               TINYINT         NOT NULL,
    [AllowEditCostValue]     BIT             CONSTRAINT [DF_InventoryMeasurementUnit_AllowEditCostValue] DEFAULT ((0)) NOT NULL,
    [CostValue]              DECIMAL (18, 2) CONSTRAINT [DF_InventoryMeasurementUnit_CostValue] DEFAULT ((0)) NOT NULL,
    [CrystalMeasurementUnit] VARCHAR (20)    NULL,
    [Status]                 BIT             NOT NULL,
    [CreationUser]           VARCHAR (20)    CONSTRAINT [DF_UnitMeasure_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]           DATETIME        CONSTRAINT [DF_UnitMeasure_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]       VARCHAR (20)    NULL,
    [ModificationDate]       DATETIME        NULL,
    [TimeStamp]              ROWVERSION      NOT NULL,
    [RequiresStandardCode]   BIT             DEFAULT ((0)) NOT NULL,
    [StandardCode]           VARCHAR (5)     NULL,
    CONSTRAINT [PK_InventoryMeasurementUnit__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CK_STANDARD_CODE] CHECK ([RequiresStandardCode]=case when coalesce([StandardCode],'')<>'' then (1) else (0) end)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_InventoryMeasurementUnit__Code]
    ON [Inventory].[InventoryMeasurementUnit]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal binaria (TIMESTAMP) de control de concurrencia que registra automáticamente cada cambio en creación, modificación o eliminación del registro', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) del último cambio en la unidad de medida: nombre, tipo, costo. NULL si no ha sido modificada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del usuario (VARCHAR 20) que modificó últimamente la unidad de medida. NULL si no ha sido editada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de creación del registro de unidad de medida. Marca el inicio de vida del catálogo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del usuario (VARCHAR 20) que registró la unidad de medida en el sistema. Por defecto: 999 (sistema)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado binario de la unidad de medida (BIT): 1=Activa (disponible para uso), 0=Inactiva (deshabilitada en procesos de inventario y facturación)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del Documento 1 - Activo 0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de integración (VARCHAR 20) hacia sistemas de costos Crystal Reports o módulos de análisis. Puede ser NULL si no requiere sincronización', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'CrystalMeasurementUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de medida de cristal', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'CrystalMeasurementUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'CrystalMeasurementUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de costo o punto de referencia (DECIMAL 18,2) asociado a la unidad de medida para cálculos de costos de inventario y procedimientos. Por defecto: 0.00', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'CostValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Punto de valor o el valor del costo para la unidad de medida del sistema de costos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'CostValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'CostValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador binario (BIT): 1=permite editar el valor de costo/punto de la unidad, 0=bloquea modificaciones de CostValue en operaciones de inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'AllowEditCostValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor que indica si se permite la modificación de los puntos de valor para la unidad de medida', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'AllowEditCostValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'AllowEditCostValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación de la unidad (TINYINT): 1=Peso (kg, g), 2=Volumen (L, ml), 3=Unidad de Administración (dosis, comprimido, ampolla). Usado en costos y prescripciones', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'UnitType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de unidad  1- Peso 2- Volumen 3- Unidad Administracion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'UnitType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'UnitType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Abreviación estándar (VARCHAR 10) de la unidad de medida para reportes y etiquetas: kg, L, U, cja. Sinónimos: sigla, símbolo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'Abbreviation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Abreviación de la unidad de medida', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'Abbreviation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'Abbreviation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo completo (VARCHAR 100) de la unidad de medida: kilogramo, litro, unidad, caja, etc. Usado en documentos de inventario y RIPS', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la unidad de medida', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código alfanumérico (VARCHAR 20) que identifica unívocamente la unidad de medida en procesos de inventario, compras y facturación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la unidad de medida', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) de la unidad de medida en el catálogo de inventario del sistema', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad de medida', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de unidades de medida usadas en inventario (por ejemplo: unidad, caja, frasco, miligramo). Registra el código, nombre, abreviatura, tipo y valor de costo de cada unidad, así como su equivalencia con unidades estándar externas.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si esta unidad de medida debe tener asociado un código estándar externo (por ejemplo, código INVIMA, CUPS u otro estándar normativo). Valor sí/no.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'RequiresStandardCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'RequiresStandardCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código estándar externo asociado a la unidad de medida, requerido por normativas o integraciones con entidades externas (por ejemplo, código oficial INVIMA o estándar de salud).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'StandardCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'StandardCode';
