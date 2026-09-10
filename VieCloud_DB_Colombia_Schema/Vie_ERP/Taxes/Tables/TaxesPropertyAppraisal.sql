CREATE TABLE [Taxes].[TaxesPropertyAppraisal] (
    [Id]              INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TaxesPropertyId] INT          NOT NULL,
    [Validity]        INT          NOT NULL,
    [Appraisal]       NUMERIC (18) NOT NULL,
    [CreationDate]    DATETIME     NOT NULL,
    CONSTRAINT [PK_TaxesPropertyAppraisal] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TaxesPropertyAppraisal_TaxesProperty] FOREIGN KEY ([TaxesPropertyId]) REFERENCES [Taxes].[TaxesProperty] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de avalúo (DATETIME). Timestamp de cuándo se registró la tasación en el sistema.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyAppraisal', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creacion', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyAppraisal', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyAppraisal', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico (NUMERIC 18) del avalúo catastral de la propiedad. Monto de tasación o valorización del bien inmueble para impuestos.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyAppraisal', @level2type = N'COLUMN', @level2name = N'Appraisal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Avaluo', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyAppraisal', @level2type = N'COLUMN', @level2name = N'Appraisal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyAppraisal', @level2type = N'COLUMN', @level2name = N'Appraisal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Año o vigencia fiscal en que se registró o modificó el avalúo de la propiedad. Define el período de validez del valor tasado.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyAppraisal', @level2type = N'COLUMN', @level2name = N'Validity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la vigencia en que se modifico el avaluo', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyAppraisal', @level2type = N'COLUMN', @level2name = N'Validity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyAppraisal', @level2type = N'COLUMN', @level2name = N'Validity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del predio (FK) que referencia Taxes.TaxesProperty. Vincula el avalúo a la propiedad catastral específica.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyAppraisal', @level2type = N'COLUMN', @level2name = N'TaxesPropertyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del predio', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyAppraisal', @level2type = N'COLUMN', @level2name = N'TaxesPropertyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyAppraisal', @level2type = N'COLUMN', @level2name = N'TaxesPropertyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico (INT IDENTITY) de la tabla TaxesPropertyAppraisal, generado automáticamente para cada registro de avalúo de propiedad.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyAppraisal', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyAppraisal', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyAppraisal', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de avalúos o valoraciones catastrales de predios para efectos tributarios. Guarda el valor del avalúo de un predio por vigencia fiscal, utilizado para el cálculo de impuestos prediales.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyAppraisal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyAppraisal';
