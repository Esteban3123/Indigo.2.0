CREATE TABLE [Glosas].[ConciliationD] (
    [Id]               INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ConciliationCId]  INT          NOT NULL,
    [GlosaPortfolioId] INT          NOT NULL,
    [InvoiceNumber]    VARCHAR (50) NOT NULL,
    [State]            CHAR (1)     NOT NULL,
    [TimeStamp]        ROWVERSION   NOT NULL,
    CONSTRAINT [PK_ConciliationD] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ConciliationD_ConciliationC] FOREIGN KEY ([ConciliationCId]) REFERENCES [Glosas].[ConciliationC] ([Id]),
    CONSTRAINT [FK_ConciliationD_GlosaPortfolioGlosada] FOREIGN KEY ([GlosaPortfolioId]) REFERENCES [Glosas].[GlosaPortfolioGlosada] ([Id])
);


GO
ALTER TABLE [Glosas].[ConciliationD] NOCHECK CONSTRAINT [FK_ConciliationD_ConciliationC];


GO
ALTER TABLE [Glosas].[ConciliationD] NOCHECK CONSTRAINT [FK_ConciliationD_GlosaPortfolioGlosada];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'ConciliationD', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - sin confirmado   2 confirmado ', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'ConciliationD', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'numero de factura', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'ConciliationD', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de relacion con saldos de facturas', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'ConciliationD', @level2type = N'COLUMN', @level2name = N'GlosaPortfolioId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de relacion con la cabecera de conciliacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'ConciliationD', @level2type = N'COLUMN', @level2name = N'ConciliationCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de detalle de conciliacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'ConciliationD', @level2type = N'COLUMN', @level2name = N'Id';

