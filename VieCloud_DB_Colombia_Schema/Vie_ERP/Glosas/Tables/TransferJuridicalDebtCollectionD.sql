CREATE TABLE [Glosas].[TransferJuridicalDebtCollectionD] (
    [Id]                                 INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TransferJuridicalDebtCollectionCId] INT          NOT NULL,
    [PortfolioGlosaId]                   INT          NULL,
    [AccountReceivableId]                INT          NULL,
    [LegalTransferValue]                 MONEY        CONSTRAINT [DF__TransferJ__Legal__385FCE4B] DEFAULT ((0)) NULL,
    [InvoiceNumber]                      VARCHAR (50) NOT NULL,
    [AccountReceivableDate]              DATETIME     NULL,
    [TimeStamp]                          ROWVERSION   NOT NULL,
    [ReversedUser]                       VARCHAR (20) NULL,
    [ReversedDate]                       DATETIME     NULL,
    CONSTRAINT [PK_TransferJuridicalDebtCollectionD] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [AccountReceivable] FOREIGN KEY ([AccountReceivableId]) REFERENCES [Portfolio].[AccountReceivable] ([Id]),
    CONSTRAINT [FK_TransferJuridicalDebtCollectionD_GlosaObjectionsReceptionC] FOREIGN KEY ([PortfolioGlosaId]) REFERENCES [Glosas].[GlosaPortfolioGlosada] ([Id]),
    CONSTRAINT [FK_TransferJuridicalDebtCollectionD_TransferJuridicalDebtCollectionC] FOREIGN KEY ([TransferJuridicalDebtCollectionCId]) REFERENCES [Glosas].[TransferJuridicalDebtCollectionC] ([Id])
);




GO



GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_TransferJuridicalDebtCollectionD_InvoiceNumber]
    ON [Glosas].[TransferJuridicalDebtCollectionD]([InvoiceNumber] ASC)
    INCLUDE([ReversedUser], [TransferJuridicalDebtCollectionCId]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se revirtió o anuló el registro de traslado a cobro jurídico; timestamp de reversión, cancelación o rechazo del proceso legal.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionD', @level2type = N'COLUMN', @level2name = N'ReversedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de reversion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionD', @level2type = N'COLUMN', @level2name = N'ReversedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionD', @level2type = N'COLUMN', @level2name = N'ReversedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que ejecutó la reversión, anulación o rechazo del traslado a cobro jurídico; identificación del analista o gestor que realizó la acción.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionD', @level2type = N'COLUMN', @level2name = N'ReversedUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de reversion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionD', @level2type = N'COLUMN', @level2name = N'ReversedUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionD', @level2type = N'COLUMN', @level2name = N'ReversedUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal de auditoría que registra el instante exacto de creación, modificación o cambio de estado del detalle de factura en el traslado a cobro jurídico.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionD', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionD', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionD', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de origen, registro o vencimiento de la cuenta por cobrar (deuda) asociada al detalle; fecha de la obligación económica.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionD', @level2type = N'COLUMN', @level2name = N'AccountReceivableDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la cuenta por cobrar', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionD', @level2type = N'COLUMN', @level2name = N'AccountReceivableDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionD', @level2type = N'COLUMN', @level2name = N'AccountReceivableDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número o identificador único de la factura de origen; referencia al comprobante de venta que genera la deuda a cobro jurídico.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionD', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Factura', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionD', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionD', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario trasladado a cobro jurídico; monto de dinero en disputa o deuda que se envía a gestión legal; MONEY, puede ser NULL con defecto 0.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionD', @level2type = N'COLUMN', @level2name = N'LegalTransferValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'valor a cobro juridico', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionD', @level2type = N'COLUMN', @level2name = N'LegalTransferValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionD', @level2type = N'COLUMN', @level2name = N'LegalTransferValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta por cobrar (FK a Portfolio.AccountReceivable); referencia a la deuda específica vinculada al detalle.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionD', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id de la cuenta por cobrar', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionD', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionD', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de relación con la glosa de cartera (FK a Glosas.GlosaPortfolioGlosada); vinculación a la glosa u objeción en cartera.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionD', @level2type = N'COLUMN', @level2name = N'PortfolioGlosaId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Relacion de cartera de glosa', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionD', @level2type = N'COLUMN', @level2name = N'PortfolioGlosaId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionD', @level2type = N'COLUMN', @level2name = N'PortfolioGlosaId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del encabezado del oficio de traslado a cobro jurídico (FK a Glosas.TransferJuridicalDebtCollectionC); relación con el acto legal maestro.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionD', @level2type = N'COLUMN', @level2name = N'TransferJuridicalDebtCollectionCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Relacion de cabecera del oficio de traslado a cobro juridico', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionD', @level2type = N'COLUMN', @level2name = N'TransferJuridicalDebtCollectionCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionD', @level2type = N'COLUMN', @level2name = N'TransferJuridicalDebtCollectionCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico (IDENTITY) del detalle de factura dentro del traslado a cobro jurídico; clave primaria de la línea de deuda legal.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionD', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico del detalle de facturas a traslado a cobro juridico', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionD', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionD', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de las transferencias de deudas a cobro jurídico: registra cada cuenta por cobrar o glosa individual que fue trasladada a un proceso de cobranza jurídica, incluyendo el valor transferido, la factura asociada y los datos de reversión si aplica.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionD';
