CREATE TABLE [Billing].[InvoiceEntityCapitatedDistributionDetail] (
    [Id]                                   INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [InvoiceEntityCapitatedDistributionId] INT             NOT NULL,
    [InvoiceId]                            INT             NOT NULL,
    [HealthAdministratorId]                INT             NOT NULL,
    [InvoiceCategoryId]                    INT             NULL,
    [InvoiceNumber]                        VARCHAR (20)    NOT NULL,
    [InvoiceDate]                          DATETIME        NOT NULL,
    [InvoiceValue]                         NUMERIC (18, 2) NOT NULL,
    CONSTRAINT [PK_InvoiceEntityCapitatedDistributionDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InvoiceEntityCapitatedDistributionDetail_HealthAdministrator] FOREIGN KEY ([HealthAdministratorId]) REFERENCES [Contract].[HealthAdministrator] ([Id]),
    CONSTRAINT [FK_InvoiceEntityCapitatedDistributionDetail_Invoice] FOREIGN KEY ([InvoiceId]) REFERENCES [Billing].[Invoice] ([Id]),
    CONSTRAINT [FK_InvoiceEntityCapitatedDistributionDetail_InvoiceCategories] FOREIGN KEY ([InvoiceCategoryId]) REFERENCES [Billing].[InvoiceCategories] ([Id]),
    CONSTRAINT [FK_InvoiceEntityCapitatedDistributionDetail_InvoiceEntityCapitatedDistribution] FOREIGN KEY ([InvoiceEntityCapitatedDistributionId]) REFERENCES [Billing].[InvoiceEntityCapitatedDistribution] ([Id])
);




GO



GO



GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_InvoiceEntityCapitatedDistributionDetail_InvoiceId]
    ON [Billing].[InvoiceEntityCapitatedDistributionDetail]([InvoiceId] ASC)
    INCLUDE([InvoiceEntityCapitatedDistributionId]);


GO
CREATE NONCLUSTERED INDEX [IX_InvoiceEntityCapitatedDistributionDetail__InvoiceId]
    ON [Billing].[InvoiceEntityCapitatedDistributionDetail]([InvoiceId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total del control de capitación (monto en NUMERIC 18,2); importe facturado por la entidad administradora de salud', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedDistributionDetail', @level2type = N'COLUMN', @level2name = N'InvoiceValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Total del control', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedDistributionDetail', @level2type = N'COLUMN', @level2name = N'InvoiceValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedDistributionDetail', @level2type = N'COLUMN', @level2name = N'InvoiceValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del control de capitación (DATETIME); registro temporal del documento facturado', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedDistributionDetail', @level2type = N'COLUMN', @level2name = N'InvoiceDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del control', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedDistributionDetail', @level2type = N'COLUMN', @level2name = N'InvoiceDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedDistributionDetail', @level2type = N'COLUMN', @level2name = N'InvoiceDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del control de capitación (VARCHAR 20); identificador único del comprobante fiscal emitido por la EPS/administradora', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedDistributionDetail', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del control de capitacion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedDistributionDetail', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedDistributionDetail', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la categoría de la factura (INT, FK a InvoiceCategories); tipo de servicio o prestación facturada', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedDistributionDetail', @level2type = N'COLUMN', @level2name = N'InvoiceCategoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador de la categoria de la factura', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedDistributionDetail', @level2type = N'COLUMN', @level2name = N'InvoiceCategoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedDistributionDetail', @level2type = N'COLUMN', @level2name = N'InvoiceCategoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la entidad administradora asociada (INT, FK a HealthAdministrator); EPS, entidad de salud responsable de la capitación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedDistributionDetail', @level2type = N'COLUMN', @level2name = N'HealthAdministratorId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador de la entidad administradora asociada', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedDistributionDetail', @level2type = N'COLUMN', @level2name = N'HealthAdministratorId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedDistributionDetail', @level2type = N'COLUMN', @level2name = N'HealthAdministratorId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la factura/control de capitación (INT, FK a Invoice); referencia al documento principal de facturación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedDistributionDetail', @level2type = N'COLUMN', @level2name = N'InvoiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador de la factura (Control de Capitacion)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedDistributionDetail', @level2type = N'COLUMN', @level2name = N'InvoiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedDistributionDetail', @level2type = N'COLUMN', @level2name = N'InvoiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la distribución de la factura monto fijo (INT, FK a InvoiceEntityCapitatedDistribution); detalle de asignación del rubro capitado', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedDistributionDetail', @level2type = N'COLUMN', @level2name = N'InvoiceEntityCapitatedDistributionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador de la distribución de la factura Monto Fijo', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedDistributionDetail', @level2type = N'COLUMN', @level2name = N'InvoiceEntityCapitatedDistributionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedDistributionDetail', @level2type = N'COLUMN', @level2name = N'InvoiceEntityCapitatedDistributionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro (INT IDENTITY, PK); clave primaria del detalle de distribución de capitación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedDistributionDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del Registro', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedDistributionDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedDistributionDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de la distribución de facturas por entidad en contratos de capitación. Registra cada factura asociada a una distribución capitada, incluyendo la entidad administradora de salud, la categoría, el número, la fecha y el valor facturado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedDistributionDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedDistributionDetail';
