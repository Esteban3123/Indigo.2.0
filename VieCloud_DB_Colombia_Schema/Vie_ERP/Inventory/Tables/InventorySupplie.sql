CREATE TABLE [Inventory].[InventorySupplie] (
    [Id]                          INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                        VARCHAR (20)  NOT NULL,
    [SupplieName]                 VARCHAR (100) NOT NULL,
    [RiskLevelId]                 INT           NOT NULL,
    [PBSProduct]                  BIT           NOT NULL,
    [SupplieStatus]               BIT           NOT NULL,
    [CreationUser]                VARCHAR (20)  NOT NULL,
    [CreationDate]                DATETIME      NOT NULL,
    [ModificationUser]            VARCHAR (20)  NULL,
    [ModificationDate]            DATETIME      NULL,
    [TimeStamp]                   ROWVERSION    NOT NULL,
    [JustificationOfInputs]       BIT           CONSTRAINT [DF_InventorySupplie_JustificationOfInputs] DEFAULT ((0)) NOT NULL,
    [OsteosynthesisMaterial]      BIT           CONSTRAINT [DF_InventorySupplie_OsteosynthesisMaterial] DEFAULT ((0)) NOT NULL,
    [Consumption]                 BIT           CONSTRAINT [DF_InventorySupplie_Consumption] DEFAULT ((0)) NOT NULL,
    [OptometryDevice]             BIT           CONSTRAINT [DF_InventorySupplie_OptometryDevice] DEFAULT ((0)) NOT NULL,
    [MedicalDevice]               BIT           CONSTRAINT [DF__Inventory__Medic__68CF922A] DEFAULT ((0)) NOT NULL,
    [IsParenteralNutritionSupply] BIT           CONSTRAINT [DF_InventorySupplie_IsParenteralNutritionSupply] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_InventorySupplie] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InventorySupplie_InventoryRiskLevel] FOREIGN KEY ([RiskLevelId]) REFERENCES [Inventory].[InventoryRiskLevel] ([Id])
);




GO



GO
CREATE NONCLUSTERED INDEX [IX_InventorySupplie_RiskLevelId]
    ON [Inventory].[InventorySupplie]([RiskLevelId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_InventorySupplie_Code_SupplieName]
    ON [Inventory].[InventorySupplie]([Code] ASC)
    INCLUDE([SupplieName]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que identifica si el insumo/artículo funciona como dispositivo médico regulado. Tipo: BOOLEAN (1=Sí, 0=No)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'MedicalDevice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'define si es dispositivo medico   ', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'MedicalDevice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'MedicalDevice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera que determina si el insumo opera como dispositivo de optometría (lentes, marcos, equipos oftálmicos). Tipo: BOOLEAN (1=Sí, 0=No)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'OptometryDevice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite identificar si funciona como dispositivo de optometría.  ', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'OptometryDevice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'OptometryDevice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador que clasifica si el insumo es de consumo (fungible, desechable, de un solo uso). Tipo: BOOLEAN (1=Consumo, 0=No consumo)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'Consumption';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite saber si es de consumo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'Consumption';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'Consumption';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera que señala si el insumo corresponde a material de osteosíntesis (placas, tornillos, implantes óseos). Tipo: BOOLEAN (1=Sí, 0=No)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'OsteosynthesisMaterial';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite saber si es material osteosintesis', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'OsteosynthesisMaterial';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'OsteosynthesisMaterial';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Requerimiento de documentación: exige justificación obligatoria para autorización/consumo del insumo o dispositivo. Tipo: BOOLEAN (1=Requiere, 0=No requiere)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'JustificationOfInputs';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Exige justificacion de insumos / dispositivos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'JustificationOfInputs';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'JustificationOfInputs';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal automática (TIMESTAMP) que registra el instante exacto de creación, modificación o evento del registro. Uso interno de SQL Server para sincronización/auditoría', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) del último cambio registrado en el insumo. Nulo si nunca fue modificado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (VARCHAR 20) del usuario que realizó la última modificación del registro. Nulo si no hubo cambios', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que se creó el registro del insumo en el inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (VARCHAR 20) del usuario que originó la creación del registro del insumo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado actual del insumo/artículo en inventario. Tipo: BOOLEAN (1=Activo/Disponible, 0=Inactivo/Descontinuado)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'SupplieStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado:  1. Activo  0. Inactivo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'SupplieStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'SupplieStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador que verifica si el insumo está incluido en el Plan de Beneficios en Salud (PBS). Tipo: BOOLEAN (1=En PBS, 0=Fuera de PBS)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'PBSProduct';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si o no el insumo esta en el plan de beneficios de salud', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'PBSProduct';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'PBSProduct';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico (FK) del nivel de riesgo asociado al insumo. Referencia a [Inventory].[InventoryRiskLevel]. Niveles: bajo, medio, alto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'RiskLevelId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de nivel de riesgo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'RiskLevelId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'RiskLevelId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Denominación descriptiva del insumo, artículo o dispositivo médico (permite búsqueda por nombre comercial/genérico). Tipo: VARCHAR MAX', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'SupplieName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de insumo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'SupplieName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'SupplieName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único identificador del insumo (ej: código de farmacéutica, código interno, código INVIMA). Clave de búsqueda rápida. Tipo: VARCHAR 20', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de insumo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico único (IDENTITY INT) de la tabla InventorySupplie. Clave primaria auto-incremental. Referencia interna en relaciones', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo maestro de insumos y dispositivos médicos del inventario. Registra cada ítem con su código, nombre, nivel de riesgo, estado, clasificaciones especiales (material de osteosíntesis, dispositivo de optometría, dispositivo médico, nutrición parenteral) y datos de auditoría de creación y modificación.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el insumo es utilizado para nutrición parenteral (alimentación intravenosa). Valor verdadero significa que el ítem pertenece a la categoría de nutrición parenteral.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'IsParenteralNutritionSupply';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'IsParenteralNutritionSupply';
