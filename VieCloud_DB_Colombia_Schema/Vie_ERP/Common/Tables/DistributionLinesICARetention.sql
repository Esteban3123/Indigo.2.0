CREATE TABLE [Common].[DistributionLinesICARetention] (
    [Id]                      INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DistributionLineId]      INT NOT NULL,
    [OperatingUnitId]         INT NOT NULL,
    [AccountPayableConceptId] INT NOT NULL,
    CONSTRAINT [PK_DistributionLinesICARetention] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DistributionLinesICARetention_AccountPayableConcepts] FOREIGN KEY ([AccountPayableConceptId]) REFERENCES [Payments].[AccountPayableConcepts] ([Id]),
    CONSTRAINT [FK_DistributionLinesICARetention_DistributionLines] FOREIGN KEY ([DistributionLineId]) REFERENCES [Common].[DistributionLines] ([Id]),
    CONSTRAINT [FK_DistributionLinesICARetention_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del concepto de cuenta por pagar (FK a Payments.AccountPayableConcepts). Restringido a conceptos de tipo Retención ICA y sus variantes específicas. Usado para clasificar retenciones en la distribución de gastos.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLinesICARetention', @level2type = N'COLUMN', @level2name = N'AccountPayableConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de Pagos, Solo pueden ser conceptos de tipo de Retencion y de tipos especificos', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLinesICARetention', @level2type = N'COLUMN', @level2name = N'AccountPayableConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLinesICARetention', @level2type = N'COLUMN', @level2name = N'AccountPayableConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad operativa, centro de atención o entidad de salud (FK a Common.OperatingUnit). Vincula la retención ICA al centro donde se originó el gasto.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLinesICARetention', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad operativa', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLinesICARetention', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLinesICARetention', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la línea de distribución contable (FK a Common.DistributionLines). Referencia el renglón del comprobante o documento contable asociado a la retención.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLinesICARetention', @level2type = N'COLUMN', @level2name = N'DistributionLineId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la linea de distribucion', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLinesICARetention', @level2type = N'COLUMN', @level2name = N'DistributionLineId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLinesICARetention', @level2type = N'COLUMN', @level2name = N'DistributionLineId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (IDENTITY INT). Clave primaria de la tabla DistributionLinesICARetention. Identifica cada registro de retención ICA distribuida.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLinesICARetention', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLinesICARetention', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLinesICARetention', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra la retención de ICA (Impuesto de Industria y Comercio) asociada a cada línea de distribución contable, vinculando la unidad operativa y el concepto de cuentas por pagar correspondiente.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLinesICARetention';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLinesICARetention';
