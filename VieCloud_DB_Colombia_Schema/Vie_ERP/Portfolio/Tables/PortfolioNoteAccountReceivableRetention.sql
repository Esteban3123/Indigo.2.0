CREATE TABLE [Portfolio].[PortfolioNoteAccountReceivableRetention] (
    [Id]                               INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PortfolioNoteAccountReceivableId] INT             NOT NULL,
    [InvoiceCustomerRetentionId]       INT             NOT NULL,
    [Nature]                           TINYINT         NOT NULL,
    [RetentionRate]                    NUMERIC (6, 3)  NOT NULL,
    [BaseValue]                        NUMERIC (18, 2) NOT NULL,
    [Value]                            NUMERIC (18, 2) NOT NULL,
    CONSTRAINT [PK_InvoiceCustomerRetention] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PortfolioNoteAccountReceivableRetention_InvoiceCustomerRetention] FOREIGN KEY ([InvoiceCustomerRetentionId]) REFERENCES [Billing].[InvoiceCustomerRetention] ([Id]),
    CONSTRAINT [FK_PortfolioNoteAccountReceivableRetention_PortfolioNoteAccountReceivableAdvance] FOREIGN KEY ([PortfolioNoteAccountReceivableId]) REFERENCES [Portfolio].[PortfolioNoteAccountReceivableAdvance] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario final de la retención ajustado (NUMERIC 18,2). Monto deducido o acreditado en la factura según naturaleza de la nota.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableRetention', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de retención ajustado', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableRetention', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableRetention', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor base monetario (NUMERIC 18,2) sobre el cual se calcula el porcentaje de retención. Base para aplicar tasa de descuento o glosa.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableRetention', @level2type = N'COLUMN', @level2name = N'BaseValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor base (Valor ajustado) al cual se aplica la retención', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableRetention', @level2type = N'COLUMN', @level2name = N'BaseValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableRetention', @level2type = N'COLUMN', @level2name = N'BaseValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de retención o descuento aplicado (NUMERIC 6,3). Tasa de deducción sobre valor base de factura, contrato o glosa.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableRetention', @level2type = N'COLUMN', @level2name = N'RetentionRate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje de retención aplicado', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableRetention', @level2type = N'COLUMN', @level2name = N'RetentionRate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableRetention', @level2type = N'COLUMN', @level2name = N'RetentionRate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Naturaleza de movimiento contable: 1=Débito (aumenta retención) | 2=Crédito (disminuye retención). Tipo de nota de ajuste en cartera.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableRetention', @level2type = N'COLUMN', @level2name = N'Nature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Naturaleza de la nota  1 - Débito  2 - Crédito    Las nota de naturaleza débito aumentan el valor de la retención, caso contrario realiza las notas de naturaleza crédito', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableRetention', @level2type = N'COLUMN', @level2name = N'Nature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableRetention', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia (FK) a retención de factura en [Billing].[InvoiceCustomerRetention]. Identificación de la glosa o descuento aplicado al cliente.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableRetention', @level2type = N'COLUMN', @level2name = N'InvoiceCustomerRetentionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del la retención aplicada a la factura', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableRetention', @level2type = N'COLUMN', @level2name = N'InvoiceCustomerRetentionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableRetention', @level2type = N'COLUMN', @level2name = N'InvoiceCustomerRetentionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia (FK) a detalle de nota de cartera en [Portfolio].[PortfolioNoteAccountReceivableAdvance]. Vínculo a documento de ajuste de factura.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableRetention', @level2type = N'COLUMN', @level2name = N'PortfolioNoteAccountReceivableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle asociado a la factura de la nota de cartera', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableRetention', @level2type = N'COLUMN', @level2name = N'PortfolioNoteAccountReceivableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableRetention', @level2type = N'COLUMN', @level2name = N'PortfolioNoteAccountReceivableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY). Clave primaria del registro de retención en nota de cartera.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableRetention', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador único del registro.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableRetention', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableRetention', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Retenciones aplicadas a las cuentas por cobrar dentro de notas de cartera. Registra cada retención (tributaria, de ley o contractual) asociada a una factura de cliente, con su tarifa, base gravable y valor retenido.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableRetention';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableRetention';
