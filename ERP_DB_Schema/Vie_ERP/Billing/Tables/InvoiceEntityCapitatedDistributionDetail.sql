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
ALTER TABLE [Billing].[InvoiceEntityCapitatedDistributionDetail] NOCHECK CONSTRAINT [FK_InvoiceEntityCapitatedDistributionDetail_HealthAdministrator];


GO
ALTER TABLE [Billing].[InvoiceEntityCapitatedDistributionDetail] NOCHECK CONSTRAINT [FK_InvoiceEntityCapitatedDistributionDetail_Invoice];


GO
ALTER TABLE [Billing].[InvoiceEntityCapitatedDistributionDetail] NOCHECK CONSTRAINT [FK_InvoiceEntityCapitatedDistributionDetail_InvoiceCategories];


GO
ALTER TABLE [Billing].[InvoiceEntityCapitatedDistributionDetail] NOCHECK CONSTRAINT [FK_InvoiceEntityCapitatedDistributionDetail_InvoiceEntityCapitatedDistribution];


GO
CREATE NONCLUSTERED INDEX [IX_InvoiceEntityCapitatedDistributionDetail_InvoiceId]
    ON [Billing].[InvoiceEntityCapitatedDistributionDetail]([InvoiceId] ASC)
    INCLUDE([InvoiceEntityCapitatedDistributionId]);


GO
CREATE NONCLUSTERED INDEX [IX_InvoiceEntityCapitatedDistributionDetail__InvoiceId]
    ON [Billing].[InvoiceEntityCapitatedDistributionDetail]([InvoiceId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Total del control', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedDistributionDetail', @level2type = N'COLUMN', @level2name = N'InvoiceValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del control', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedDistributionDetail', @level2type = N'COLUMN', @level2name = N'InvoiceDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del control de capitacion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedDistributionDetail', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador de la categoria de la factura', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedDistributionDetail', @level2type = N'COLUMN', @level2name = N'InvoiceCategoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador de la entidad administradora asociada', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedDistributionDetail', @level2type = N'COLUMN', @level2name = N'HealthAdministratorId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador de la factura (Control de Capitacion)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedDistributionDetail', @level2type = N'COLUMN', @level2name = N'InvoiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador de la distribución de la factura Monto Fijo', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedDistributionDetail', @level2type = N'COLUMN', @level2name = N'InvoiceEntityCapitatedDistributionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del Registro', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedDistributionDetail', @level2type = N'COLUMN', @level2name = N'Id';

