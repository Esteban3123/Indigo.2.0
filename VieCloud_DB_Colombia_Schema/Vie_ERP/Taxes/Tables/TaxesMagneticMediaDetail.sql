CREATE TABLE [Taxes].[TaxesMagneticMediaDetail] (
    [Id]                   INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [MagneticMediaId]      INT            NOT NULL,
    [NameOrBusinessName]   NVARCHAR (200) NOT NULL,
    [IdentificationNumber] NVARCHAR (20)  NOT NULL,
    [ValueOperation]       NUMERIC (18)   NOT NULL,
    [TaxWithheld]          NUMERIC (18)   NOT NULL,
    CONSTRAINT [PK_MagneticMediaDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MagneticMediaDetail_MagnetiMedia] FOREIGN KEY ([MagneticMediaId]) REFERENCES [Taxes].[TaxesMagneticMedia] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Impuesto retenido, monto de retención fiscal en operación tributaria (NUMERIC 18), PII-Fiscal', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesMagneticMediaDetail', @level2type = N'COLUMN', @level2name = N'TaxWithheld';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesMagneticMediaDetail', @level2type = N'COLUMN', @level2name = N'TaxWithheld';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesMagneticMediaDetail', @level2type = N'COLUMN', @level2name = N'TaxWithheld';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de operación, monto bruto o base de cálculo de la transacción tributaria (NUMERIC 18)', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesMagneticMediaDetail', @level2type = N'COLUMN', @level2name = N'ValueOperation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Valor de Operación', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesMagneticMediaDetail', @level2type = N'COLUMN', @level2name = N'ValueOperation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesMagneticMediaDetail', @level2type = N'COLUMN', @level2name = N'ValueOperation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de identificación, cédula/RUC/documento del tercero/contribuyente (NVARCHAR 20), PII-Identification', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesMagneticMediaDetail', @level2type = N'COLUMN', @level2name = N'IdentificationNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de identificación', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesMagneticMediaDetail', @level2type = N'COLUMN', @level2name = N'IdentificationNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesMagneticMediaDetail', @level2type = N'COLUMN', @level2name = N'IdentificationNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o nombre comercial, razón social del sujeto tributario o entidad (NVARCHAR 200), PII-PersonalData', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesMagneticMediaDetail', @level2type = N'COLUMN', @level2name = N'NameOrBusinessName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre o nombre comercial', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesMagneticMediaDetail', @level2type = N'COLUMN', @level2name = N'NameOrBusinessName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesMagneticMediaDetail', @level2type = N'COLUMN', @level2name = N'NameOrBusinessName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de medios magnéticos, referencia FK a TaxesMagneticMedia para archivo de retenciones RIPS/impuestos (INT)', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesMagneticMediaDetail', @level2type = N'COLUMN', @level2name = N'MagneticMediaId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de medios magnéticos', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesMagneticMediaDetail', @level2type = N'COLUMN', @level2name = N'MagneticMediaId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesMagneticMediaDetail', @level2type = N'COLUMN', @level2name = N'MagneticMediaId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID autonumérico de detalle, identificador único de registro de retención en medio magnético (INT IDENTITY 1,1)', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesMagneticMediaDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesMagneticMediaDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesMagneticMediaDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de los conceptos reportados en los medios magnéticos tributarios (información exógena DIAN). Registra por cada tercero el valor de las operaciones y las retenciones en la fuente practicadas o recibidas dentro de un período fiscal.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesMagneticMediaDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesMagneticMediaDetail';
