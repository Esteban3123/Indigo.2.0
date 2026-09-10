CREATE TABLE [Inventory].[InventorySequenceDetail] (
    [Id]                  INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [InventorySequenceId] INT         NOT NULL,
    [IdSequense]          INT         NOT NULL,
    [IdOperatingUnit]     INT         NULL,
    [Next]                BIGINT      CONSTRAINT [DF_InventorySequenceDetail_Next] DEFAULT ((1)) NOT NULL,
    [Prefix]              VARCHAR (4) NULL,
    [Type]                TINYINT     NULL,
    CONSTRAINT [PK_InventorySequenceDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InventorySequenceDetail_InventorySequence] FOREIGN KEY ([InventorySequenceId]) REFERENCES [Inventory].[InventorySequence] ([Id]),
    CONSTRAINT [FK_InventorySequenceDetail_OperatingUnit] FOREIGN KEY ([IdOperatingUnit]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_InventorySequenceDetail_Sequense] FOREIGN KEY ([IdSequense]) REFERENCES [Common].[Sequense] ([Id])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo adicional de secuencia (TINYINT): clasificación o categoría de comportamiento especial en la generación de números secuenciales para inventario o facturación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySequenceDetail', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipos Adicionales', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySequenceDetail', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySequenceDetail', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prefijo alfanumérico (VARCHAR 4) usado en resolución de facturación: caracteres antecedentes al número secuencial (ej: FACT, RES, NC para números de factura, resolución o nota crédito)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySequenceDetail', @level2type = N'COLUMN', @level2name = N'Prefix';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Prefijo usado en la resolución de Facturación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySequenceDetail', @level2type = N'COLUMN', @level2name = N'Prefix';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySequenceDetail', @level2type = N'COLUMN', @level2name = N'Prefix';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Siguiente número a generar en la secuencia (BIGINT): contador progresivo que determina el próximo valor numérico a asignar en el documento o movimiento de inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySequenceDetail', @level2type = N'COLUMN', @level2name = N'Next';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Siguiente numero a generar con la secuenacia', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySequenceDetail', @level2type = N'COLUMN', @level2name = N'Next';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySequenceDetail', @level2type = N'COLUMN', @level2name = N'Next';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la unidad operativa (FK a OperatingUnit): identifica centro de atención, sede o unidad funcional asignada a la secuencia; nulo cuando el ámbito es global (no restringido a UO específica)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySequenceDetail', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad operativa asignada a la secuencia. Solo cuando el ambito es UO-Unidad Operativa, de lo contrario el campo es nulo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySequenceDetail', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySequenceDetail', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la secuencia base (FK a Sequense): referencia a la configuración maestra de secuencia que define rangos, formatos y reglas de numeración', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySequenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequense';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la secuencia base', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySequenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequense';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySequenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequense';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la cabecera de secuencia de inventario (FK a InventorySequence): referencia al registro padre que agrupa los detalles de configuración secuencial', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySequenceDetail', @level2type = N'COLUMN', @level2name = N'InventorySequenceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySequenceDetail', @level2type = N'COLUMN', @level2name = N'InventorySequenceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySequenceDetail', @level2type = N'COLUMN', @level2name = N'InventorySequenceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de detalle de secuencia de inventario (INT IDENTITY, PK): clave primaria de la fila en la tabla InventorySequenceDetail', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySequenceDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySequenceDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySequenceDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de las secuencias de numeración del inventario: guarda la configuración de cada serie o consecutivo utilizado para numerar documentos de inventario (entradas, salidas, ajustes), incluyendo el prefijo, el próximo número a usar y la unidad operativa a la que aplica.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySequenceDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySequenceDetail';
