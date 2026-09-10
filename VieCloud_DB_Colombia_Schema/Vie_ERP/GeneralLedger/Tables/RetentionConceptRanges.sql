CREATE TABLE [GeneralLedger].[RetentionConceptRanges] (
    [Id]             INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RetentionId]    INT             NOT NULL,
    [Percentage]     DECIMAL (18, 2) NOT NULL,
    [ValueInitial]   DECIMAL (18, 2) NOT NULL,
    [ValueFinish]    DECIMAL (18, 2) NOT NULL,
    [ValueDeducted]  DECIMAL (18, 2) NOT NULL,
    [ValueIncrement] DECIMAL (5, 2)  NOT NULL,
    [UVTIncrement]   DECIMAL (18, 2) CONSTRAINT [DF_RetentionConceptRanges_UVTIncrement] DEFAULT ((0)) NOT NULL,
    [TimeStamp]      ROWVERSION      NOT NULL,
    CONSTRAINT [PK_AccountingRetention] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccountingRetention_RetentionConcept] FOREIGN KEY ([RetentionId]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal TIMESTAMP (SQL Server) que registra el instante exacto de creación, modificación o auditoría del rango de retención. Utilizado para trazabilidad y control de cambios en conceptos de retención.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConceptRanges', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConceptRanges', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConceptRanges', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Incremento del impuesto/retención expresado en UVT (Unidad de Valor Tributario). Decimal(18,2), valor por defecto 0. Usado en cálculo de retenciones sobre renta, IVA o retención en la fuente según normativa fiscal colombiana.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConceptRanges', @level2type = N'COLUMN', @level2name = N'UVTIncrement';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el incremento expresado en UVT que se ñe va a realizar al impuesto', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConceptRanges', @level2type = N'COLUMN', @level2name = N'UVTIncrement';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConceptRanges', @level2type = N'COLUMN', @level2name = N'UVTIncrement';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de incremento aplicable al rango de retención. Decimal(5,2). Define el porcentaje adicional a aplicar sobre el valor base del impuesto o retención según concepto contable.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConceptRanges', @level2type = N'COLUMN', @level2name = N'ValueIncrement';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del incremento que debe ser expresado en porcentaje', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConceptRanges', @level2type = N'COLUMN', @level2name = N'ValueIncrement';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConceptRanges', @level2type = N'COLUMN', @level2name = N'ValueIncrement';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor deducido o descontado del rango, expresado en UVT. Decimal(18,2). Representa el monto que se resta en el cálculo de retenciones (ej: retención en la fuente, glosa, descuentos fiscales).', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConceptRanges', @level2type = N'COLUMN', @level2name = N'ValueDeducted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor deducido expresado en UVT', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConceptRanges', @level2type = N'COLUMN', @level2name = N'ValueDeducted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConceptRanges', @level2type = N'COLUMN', @level2name = N'ValueDeducted';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor final o límite superior del rango de retención, expresado en UVT. Decimal(18,2). Define el tope máximo del intervalo para aplicar el porcentaje de retención especificado.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConceptRanges', @level2type = N'COLUMN', @level2name = N'ValueFinish';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Final del rango expresado en UVT', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConceptRanges', @level2type = N'COLUMN', @level2name = N'ValueFinish';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConceptRanges', @level2type = N'COLUMN', @level2name = N'ValueFinish';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor inicial o límite inferior del rango de retención, expresado en UVT. Decimal(18,2). Define el tope mínimo del intervalo para aplicar el porcentaje de retención especificado.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConceptRanges', @level2type = N'COLUMN', @level2name = N'ValueInitial';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor inicial del rango expresado en UVT', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConceptRanges', @level2type = N'COLUMN', @level2name = N'ValueInitial';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConceptRanges', @level2type = N'COLUMN', @level2name = N'ValueInitial';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje o tarifa de retención/impuesto según concepto contable (códigos RIPS 383, 384 u otros). Decimal(18,2). Aplica sobre el rango [ValueInitial, ValueFinish] para cálculo de retención en la fuente, IVA o impuestos.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConceptRanges', @level2type = N'COLUMN', @level2name = N'Percentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del porcentaje o valor de la retencion segun el caso (383, 384)', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConceptRanges', @level2type = N'COLUMN', @level2name = N'Percentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConceptRanges', @level2type = N'COLUMN', @level2name = N'Percentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, FK) que referencia el concepto de retención asociado en tabla RetentionConcepts. Vincula el rango con su concepto contable padre (retención en la fuente, IVA, glosa, etc.).', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConceptRanges', @level2type = N'COLUMN', @level2name = N'RetentionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id retencion', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConceptRanges', @level2type = N'COLUMN', @level2name = N'RetentionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConceptRanges', @level2type = N'COLUMN', @level2name = N'RetentionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, IDENTITY) de cada rango de retención contable. Clave primaria. Identifica unívocamente cada intervalo de valores con su porcentaje y parámetros de retención aplicables.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConceptRanges', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la retencion de contabilizacion', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConceptRanges', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConceptRanges', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Rangos de retención tributaria: define los tramos de valores sobre los cuales se aplica un porcentaje de retención en la fuente para cada concepto de retención, incluyendo el valor inicial y final del rango, el valor deducido base, el incremento monetario y el incremento en UVT (Unidades de Valor Tributario).', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConceptRanges';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConceptRanges';
