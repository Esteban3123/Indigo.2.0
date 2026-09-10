CREATE TABLE [Taxes].[TaxesMagneticMedia] (
    [Id]                   INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NameOrBusinessName]   NVARCHAR (200) NOT NULL,
    [IdentificationNumber] NVARCHAR (20)  NOT NULL,
    [Year]                 INT            NOT NULL,
    [Date]                 DATE           NULL,
    [Bimester]             TINYINT        NULL,
    CONSTRAINT [PK_MagneticMedia] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bimestre (período de dos meses) del año fiscal para declaración de impuestos; rango 1-6. Tipo: TINYINT, permite NULL.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesMagneticMedia', @level2type = N'COLUMN', @level2name = N'Bimester';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesMagneticMedia', @level2type = N'COLUMN', @level2name = N'Bimester';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesMagneticMedia', @level2type = N'COLUMN', @level2name = N'Bimester';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de generación, radicación o procesamiento del archivo de medios magnéticos para reportes tributarios. Tipo: DATE, permite NULL.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesMagneticMedia', @level2type = N'COLUMN', @level2name = N'Date';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesMagneticMedia', @level2type = N'COLUMN', @level2name = N'Date';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesMagneticMedia', @level2type = N'COLUMN', @level2name = N'Date';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Año fiscal o período contable asociado al reporte de medios magnéticos; usado para RIPS, impuestos y declaraciones. Tipo: INT, obligatorio.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesMagneticMedia', @level2type = N'COLUMN', @level2name = N'Year';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Año', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesMagneticMedia', @level2type = N'COLUMN', @level2name = N'Year';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesMagneticMedia', @level2type = N'COLUMN', @level2name = N'Year';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de identificación del contribuyente, empresa o persona (cédula, NIT, cédula de extranjería). PII - Identification_Ofuscado. Tipo: NVARCHAR(20), obligatorio.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesMagneticMedia', @level2type = N'COLUMN', @level2name = N'IdentificationNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de identificación', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesMagneticMedia', @level2type = N'COLUMN', @level2name = N'IdentificationNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesMagneticMedia', @level2type = N'COLUMN', @level2name = N'IdentificationNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Razón social, nombre comercial o nombre completo del contribuyente persona natural o jurídica. Tipo: NVARCHAR(200), obligatorio.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesMagneticMedia', @level2type = N'COLUMN', @level2name = N'NameOrBusinessName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre o nombre comercial', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesMagneticMedia', @level2type = N'COLUMN', @level2name = N'NameOrBusinessName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesMagneticMedia', @level2type = N'COLUMN', @level2name = N'NameOrBusinessName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (IDENTITY) de cada registro de medios magnéticos en la tabla TaxesMagneticMedia. Clave primaria. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesMagneticMedia', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesMagneticMedia', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesMagneticMedia', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registros de medios magnéticos tributarios por contribuyente y período fiscal. Guarda la información de personas naturales o empresas reportadas a la DIAN, asociando nombre o razón social, NIT o cédula, año gravable y bimestre del reporte.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesMagneticMedia';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesMagneticMedia';
