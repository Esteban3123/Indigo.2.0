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
ALTER TABLE [Contract].[BillingItemsRestrictionDetail] NOCHECK CONSTRAINT [FK_BillingItemsRestrictionDetail_BillingItemsRestriction];


GO
ALTER TABLE [Contract].[BillingItemsRestrictionDetail] NOCHECK CONSTRAINT [FK_BillingItemsRestrictionDetail_CUPSEntity];


GO
ALTER TABLE [Contract].[BillingItemsRestrictionDetail] NOCHECK CONSTRAINT [FK_BillingItemsRestrictionDetail_CupsGroup];


GO
ALTER TABLE [Contract].[BillingItemsRestrictionDetail] NOCHECK CONSTRAINT [FK_BillingItemsRestrictionDetail_CupsSubgroup];


GO
ALTER TABLE [Contract].[BillingItemsRestrictionDetail] NOCHECK CONSTRAINT [FK_BillingItemsRestrictionDetail_IncludeCUPSEntity];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del CUPS en el que se incluiran los servicio o productos', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetail', @level2type = N'COLUMN', @level2name = N'IncludeToCUPSEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la segunda condicion de la liquidacion  1) Ninguna  2) Unidad Funcional  3) Tipo de Unidad  4) Tipo de Estancia  5) Tipo de Manual  6) Grupo Qx  7) Rango UVR', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetail', @level2type = N'COLUMN', @level2name = N'ConditionType2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Operador Logico entre las Condiciones  1 - Ninguna  2 - "Y"  3 - "O"    Cuando el operador logico es 1 quiere decir que solo maneja un condicion de lo contrario son dos condiciones', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetail', @level2type = N'COLUMN', @level2name = N'LogicalOperator';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la condicion de la liquidacion  1) Ninguna  2) Unidad Funcional  3) Tipo de Unidad  4) Tipo de Estancia  5) Tipo de Manual  6) Grupo Qx  7) Rango UVR', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetail', @level2type = N'COLUMN', @level2name = N'ConditionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del grupo del producto.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetail', @level2type = N'COLUMN', @level2name = N'ProductGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del subgrupo del producto.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetail', @level2type = N'COLUMN', @level2name = N'ProductSubGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del producto.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetail', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del grupo del CUPS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetail', @level2type = N'COLUMN', @level2name = N'CUPSGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del subgrupo del cups', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetail', @level2type = N'COLUMN', @level2name = N'CUPSSubgroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del CUPS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetail', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de regla de la restriccion  1 - CUPS  2 - SubGrupos CUPS  3 - Grupo CUPS  4 - Producto  5 - Subgrupo de producto  6 - Grupo de producto  7 - Ninguno', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetail', @level2type = N'COLUMN', @level2name = N'RuleType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la restricción de elementos de facturación.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetail', @level2type = N'COLUMN', @level2name = N'BillingItemsRestrictionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetail', @level2type = N'COLUMN', @level2name = N'Id';

