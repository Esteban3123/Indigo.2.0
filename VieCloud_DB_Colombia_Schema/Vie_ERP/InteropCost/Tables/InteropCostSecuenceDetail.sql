CREATE TABLE [InteropCost].[InteropCostSecuenceDetail] (
    [Id]                    INT    IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [SequenseInteropCostId] INT    NOT NULL,
    [IdSequense]            INT    NOT NULL,
    [IdOperatingUnit]       INT    NULL,
    [Next]                  BIGINT CONSTRAINT [DF_SequenseInteropCostD_Next] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_SequenseInteropCostD] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InteropCostSecuenceDetail_Sequense] FOREIGN KEY ([IdSequense]) REFERENCES [Common].[Sequense] ([Id]),
    CONSTRAINT [FK_SequenseInteropCostD_OperatingUnit] FOREIGN KEY ([IdOperatingUnit]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_SequenseInteropCostD_SequenseInteropCostC] FOREIGN KEY ([SequenseInteropCostId]) REFERENCES [InteropCost].[InteropCostSecuence] ([Id])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Siguiente número secuencial a generar en la secuencia de interoperabilidad de costos (BIGINT, valor por defecto 1). Rastrea el próximo identificador único para documentos, facturas o transacciones de costos.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'InteropCostSecuenceDetail', @level2type = N'COLUMN', @level2name = N'Next';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Siguiente numero a generar con la secuenacia', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'InteropCostSecuenceDetail', @level2type = N'COLUMN', @level2name = N'Next';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'InteropCostSecuenceDetail', @level2type = N'COLUMN', @level2name = N'Next';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la Unidad Operativa (centro de atención, sede, clínica) asignada a esta secuencia de costos. Se completa solo cuando el ámbito es por Unidad Operativa; nulo si la secuencia aplica a nivel institucional o global.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'InteropCostSecuenceDetail', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad operativa asignada a la secuencia. Solo cuando el ambito es UO-Unidad Operativa, de lo contrario el campo es nulo', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'InteropCostSecuenceDetail', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'InteropCostSecuenceDetail', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la Secuencia base o plantilla configurada en Common.Sequense. Define el tipo y las reglas de generación de números secuenciales (RIPS, facturas, recetas, etc.).', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'InteropCostSecuenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequense';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la secuencia base', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'InteropCostSecuenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequense';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'InteropCostSecuenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequense';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera o configuración madre en InteropCost.InteropCostSecuence. Vincula este detalle a su secuencia de costos de interoperabilidad padre.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'InteropCostSecuenceDetail', @level2type = N'COLUMN', @level2name = N'SequenseInteropCostId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'InteropCostSecuenceDetail', @level2type = N'COLUMN', @level2name = N'SequenseInteropCostId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'InteropCostSecuenceDetail', @level2type = N'COLUMN', @level2name = N'SequenseInteropCostId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de detalle (INT, clave primaria IDENTITY). Cada fila representa una combinación secuencia-unidad operativa con su contador de generación.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'InteropCostSecuenceDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'InteropCostSecuenceDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'InteropCostSecuenceDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de secuencias de costos de interoperabilidad. Registra los pasos o elementos individuales que componen una secuencia de costos entre sistemas, indicando la unidad operativa involucrada y el siguiente valor correlativo de la secuencia.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'InteropCostSecuenceDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'InteropCostSecuenceDetail';
