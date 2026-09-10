CREATE TABLE [Contract].[BillingItemsRestrictionDetail] (
    [Id]                        INT     IDENTITY (1, 1) NOT NULL,
    [BillingItemsRestrictionId] INT     NOT NULL,
    [RuleType]                  TINYINT NOT NULL,
    [CUPSEntityId]              INT     NULL,
    [CUPSSubgroupId]            INT     NULL,
    [CUPSGroupId]               INT     NULL,
    [ProductId]                 INT     NULL,
    [ProductSubGroupId]         INT     NULL,
    [ProductGroupId]            INT     NULL,
    [ConditionType]             TINYINT NOT NULL,
    [LogicalOperator]           TINYINT CONSTRAINT [DF_BillingItemsRestrictionDetail_LogicalOperator] DEFAULT ((1)) NOT NULL,
    [ConditionType2]            TINYINT CONSTRAINT [DF_BillingItemsRestrictionDetail_ConditionType2] DEFAULT ((5)) NOT NULL,
    [IncludeToCUPSEntityId]     INT     CONSTRAINT [DF__BillingIt__Inclu__00DC25E5] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_BillingItemsRestrictionDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BillingItemsRestrictionDetail_BillingItemsRestriction] FOREIGN KEY ([BillingItemsRestrictionId]) REFERENCES [Contract].[BillingItemsRestriction] ([Id]),
    CONSTRAINT [FK_BillingItemsRestrictionDetail_CUPSEntity] FOREIGN KEY ([CUPSEntityId]) REFERENCES [Contract].[CUPSEntity] ([Id]),
    CONSTRAINT [FK_BillingItemsRestrictionDetail_CupsGroup] FOREIGN KEY ([CUPSGroupId]) REFERENCES [Contract].[CupsGroup] ([Id]),
    CONSTRAINT [FK_BillingItemsRestrictionDetail_CupsSubgroup] FOREIGN KEY ([CUPSSubgroupId]) REFERENCES [Contract].[CupsSubgroup] ([Id]),
    CONSTRAINT [FK_BillingItemsRestrictionDetail_IncludeCUPSEntity] FOREIGN KEY ([IncludeToCUPSEntityId]) REFERENCES [Contract].[CUPSEntity] ([Id])
);




GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del CUPS destino donde se incluirán/aplicarán los servicios o productos de la restricción (clave foránea a CUPSEntity); default=1.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetail', @level2type = N'COLUMN', @level2name = N'IncludeToCUPSEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del CUPS en el que se incluiran los servicio o productos', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetail', @level2type = N'COLUMN', @level2name = N'IncludeToCUPSEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetail', @level2type = N'COLUMN', @level2name = N'IncludeToCUPSEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Condición de liquidación secundaria (si LogicalOperator≠1): 1=Ninguna, 2=Unidad Funcional, 3=Tipo Unidad, 4=Tipo Estancia, 5=Tipo Manual, 6=Grupo Qx, 7=Rango UVR. Default=5.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetail', @level2type = N'COLUMN', @level2name = N'ConditionType2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la segunda condicion de la liquidacion  1) Ninguna  2) Unidad Funcional  3) Tipo de Unidad  4) Tipo de Estancia  5) Tipo de Manual  6) Grupo Qx  7) Rango UVR', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetail', @level2type = N'COLUMN', @level2name = N'ConditionType2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetail', @level2type = N'COLUMN', @level2name = N'ConditionType2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Operador lógico entre condiciones de liquidación: 1=Ninguna (aplica solo ConditionType), 2=Y (AND), 3=O (OR). Default=1.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetail', @level2type = N'COLUMN', @level2name = N'LogicalOperator';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Operador Logico entre las Condiciones  1 - Ninguna  2 - "Y"  3 - "O"    Cuando el operador logico es 1 quiere decir que solo maneja un condicion de lo contrario son dos condiciones', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetail', @level2type = N'COLUMN', @level2name = N'LogicalOperator';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetail', @level2type = N'COLUMN', @level2name = N'LogicalOperator';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Condición de liquidación principal: 1=Ninguna, 2=Unidad Funcional, 3=Tipo de Unidad, 4=Tipo de Estancia, 5=Tipo de Manual, 6=Grupo Qx, 7=Rango UVR.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetail', @level2type = N'COLUMN', @level2name = N'ConditionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la condicion de la liquidacion  1) Ninguna  2) Unidad Funcional  3) Tipo de Unidad  4) Tipo de Estancia  5) Tipo de Manual  6) Grupo Qx  7) Rango UVR', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetail', @level2type = N'COLUMN', @level2name = N'ConditionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetail', @level2type = N'COLUMN', @level2name = N'ConditionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del grupo de producto (clasificación principal de medicamentos, insumos, materiales); null si RuleType no aplica.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetail', @level2type = N'COLUMN', @level2name = N'ProductGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del grupo del producto.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetail', @level2type = N'COLUMN', @level2name = N'ProductGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetail', @level2type = N'COLUMN', @level2name = N'ProductGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del subgrupo de producto (clasificación intermedia de bienes/insumos); null si RuleType no aplica a productos.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetail', @level2type = N'COLUMN', @level2name = N'ProductSubGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del subgrupo del producto.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetail', @level2type = N'COLUMN', @level2name = N'ProductSubGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetail', @level2type = N'COLUMN', @level2name = N'ProductSubGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del producto farmacéutico, insumo o bien de consumo; null si RuleType no es producto.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetail', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del producto.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetail', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetail', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del grupo CUPS (clasificación principal de servicios, procedimientos, diagnósticos); clave foránea a CupsGroup; null si RuleType no aplica.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetail', @level2type = N'COLUMN', @level2name = N'CUPSGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del grupo del CUPS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetail', @level2type = N'COLUMN', @level2name = N'CUPSGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetail', @level2type = N'COLUMN', @level2name = N'CUPSGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del subgrupo CUPS (clasificación intermedia de servicios/procedimientos); clave foránea a CupsSubgroup; null si RuleType no aplica.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetail', @level2type = N'COLUMN', @level2name = N'CUPSSubgroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del subgrupo del cups', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetail', @level2type = N'COLUMN', @level2name = N'CUPSSubgroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetail', @level2type = N'COLUMN', @level2name = N'CUPSSubgroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del CUPS (servicio, procedimiento, atención) de referencia; clave foránea a CUPSEntity; puede ser nulo si RuleType no es CUPS.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetail', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del CUPS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetail', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetail', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de regla de restricción contractual: 1=CUPS individual, 2=Subgrupos CUPS, 3=Grupo CUPS, 4=Producto, 5=Subgrupo producto, 6=Grupo producto, 7=Ninguno.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetail', @level2type = N'COLUMN', @level2name = N'RuleType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de regla de la restriccion  1 - CUPS  2 - SubGrupos CUPS  3 - Grupo CUPS  4 - Producto  5 - Subgrupo de producto  6 - Grupo de producto  7 - Ninguno', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetail', @level2type = N'COLUMN', @level2name = N'RuleType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetail', @level2type = N'COLUMN', @level2name = N'RuleType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la restricción de elementos de facturación (servicio, producto, CUPS); clave foránea a BillingItemsRestriction.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetail', @level2type = N'COLUMN', @level2name = N'BillingItemsRestrictionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la restricción de elementos de facturación.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetail', @level2type = N'COLUMN', @level2name = N'BillingItemsRestrictionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetail', @level2type = N'COLUMN', @level2name = N'BillingItemsRestrictionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del detalle de restricción de facturación de servicios/productos en contrato.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de las restricciones de ítems de facturación por contrato. Define las reglas específicas que determinan qué servicios CUPS, productos o grupos están incluidos o excluidos en cada restricción de facturación, con sus condiciones lógicas de evaluación.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetail';
