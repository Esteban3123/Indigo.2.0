CREATE TABLE [Common].[DistributionLinesDetail] (
    [Id]                      INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DistributionLineId]      INT     NOT NULL,
    [AccountPayableConceptId] INT     NOT NULL,
    [ConceptType]             TINYINT NOT NULL,
    CONSTRAINT [PK_DistributionLinesDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DistributionLinesDetail_AccountPayableConcepts] FOREIGN KEY ([AccountPayableConceptId]) REFERENCES [Payments].[AccountPayableConcepts] ([Id]),
    CONSTRAINT [FK_DistributionLinesDetail_DistributionLines] FOREIGN KEY ([DistributionLineId]) REFERENCES [Common].[DistributionLines] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación del concepto de retención: 1=Declarante (obligado a declarar retención), 2=No Declarante (exento de declaración). Tipo TINYINT, determina si el concepto requiere reporte en RIPS o declaración de retenciones.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLinesDetail', @level2type = N'COLUMN', @level2name = N'ConceptType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo del concepto  1 - Declarante  2 - No Declarante', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLinesDetail', @level2type = N'COLUMN', @level2name = N'ConceptType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLinesDetail', @level2type = N'COLUMN', @level2name = N'ConceptType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del concepto de cuentas por pagar (FK a Payments.AccountPayableConcepts). Referencia exclusiva a conceptos tipo retención (descuentos, impuestos retenidos, contribuciones). Tipo INT.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLinesDetail', @level2type = N'COLUMN', @level2name = N'AccountPayableConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de Pagos, Solo pueden ser conceptos de tipo de Retencion', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLinesDetail', @level2type = N'COLUMN', @level2name = N'AccountPayableConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLinesDetail', @level2type = N'COLUMN', @level2name = N'AccountPayableConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la línea de distribución padre (FK a Common.DistributionLines). Vincula el detalle a su línea matriz de distribución de pagos o facturación. Tipo INT.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLinesDetail', @level2type = N'COLUMN', @level2name = N'DistributionLineId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la linea de distribucion', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLinesDetail', @level2type = N'COLUMN', @level2name = N'DistributionLineId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLinesDetail', @level2type = N'COLUMN', @level2name = N'DistributionLineId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave primaria del detalle de distribución. Identificador único (IDENTITY) de cada registro en la tabla de detalles de listas de distribución. Tipo INT.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLinesDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de las listas de distribucion', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLinesDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLinesDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de las líneas de distribución contable: registra el desglose de cada línea de distribución asociando los conceptos de cuentas por pagar y su tipo de concepto (débito, crédito, etc.).', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLinesDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLinesDetail';
