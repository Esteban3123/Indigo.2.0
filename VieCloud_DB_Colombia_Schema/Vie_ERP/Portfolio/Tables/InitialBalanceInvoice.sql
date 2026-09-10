CREATE TABLE [Portfolio].[InitialBalanceInvoice] (
    [Id]                       INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AccountReceivableId]      INT             NOT NULL,
    [InvoiceId]                INT             NULL,
    [ObligatedPartyDocument]   VARCHAR (20)    NOT NULL,
    [InvoiceNumber]            VARCHAR (20)    NOT NULL,
    [CosmosId]                 VARCHAR (100)   NULL,
    [CUV]                      VARCHAR (MAX)   NULL,
    [Status]                   TINYINT         NOT NULL,
    [CreationUser]             VARCHAR (20)    NOT NULL,
    [CreationDate]             DATETIME        NOT NULL,
    [ModificationUser]         VARCHAR (20)    NULL,
    [ModificationDate]         DATETIME        NULL,
    CONSTRAINT [PK_InitialBalanceInvoice] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InitialBalanceInvoice_AccountReceivable] FOREIGN KEY ([AccountReceivableId]) REFERENCES [Portfolio].[AccountReceivable] ([Id]),
    CONSTRAINT [FK_InitialBalanceInvoice_Invoice] FOREIGN KEY ([InvoiceId]) REFERENCES [Billing].[Invoice] ([Id]),
    CONSTRAINT [UQ_InitialBalanceInvoice_AccountReceivableId] UNIQUE ([AccountReceivableId])
);


GO
ALTER TABLE [Portfolio].[InitialBalanceInvoice] NOCHECK CONSTRAINT [FK_InitialBalanceInvoice_AccountReceivable];


GO
ALTER TABLE [Portfolio].[InitialBalanceInvoice] NOCHECK CONSTRAINT [FK_InitialBalanceInvoice_Invoice];


GO
CREATE NONCLUSTERED INDEX [IX_InitialBalanceInvoice_InvoiceNumber]
    ON [Portfolio].[InitialBalanceInvoice]([InvoiceNumber] ASC)
    INCLUDE([Id], [CosmosId], [Status]) WITH (FILLFACTOR = 90);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Header de factura de saldo inicial. Cardinalidad 1:1 con Portfolio.AccountReceivable. Padre de Portfolio.InitialBalanceInvoiceDetail. Reúne metadata RIPS no presente en Billing.Invoice.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'InitialBalanceInvoice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador único del registro', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'InitialBalanceInvoice', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'FK a Portfolio.AccountReceivable. UNIQUE: una sola factura saldo inicial por CxC.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'InitialBalanceInvoice', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'FK a Billing.Invoice (factura shadow creada al confirmar). NULL cuando el saldo inicial proviene de Excel sin RIPS validado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'InitialBalanceInvoice', @level2type = N'COLUMN', @level2name = N'InvoiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'NIT del prestador obligado a facturar electrónicamente. Corresponde al campo "numDocumentoIdObligado" raíz del JSON RIPS.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'InitialBalanceInvoice', @level2type = N'COLUMN', @level2name = N'ObligatedPartyDocument';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de la factura. Corresponde al campo "numFactura" raíz del JSON RIPS.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'InitialBalanceInvoice', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Atajo al Id del documento en CosmosDB (campo "id" del envelope: numFactura-GUID). Redundante con Billing.ElectronicsRIPS.CosmoDBId. NULL si el saldo inicial fue cargado sin RIPS validado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'InitialBalanceInvoice', @level2type = N'COLUMN', @level2name = N'CosmosId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código Único de Validación RIPS emitido por MinSalud al validar exitosamente el RIPS electrónico. NULL hasta que se reciba.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'InitialBalanceInvoice', @level2type = N'COLUMN', @level2name = N'CUV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del registro: 1 - Cargado, 2 - Confirmado, 3 - Anulado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'InitialBalanceInvoice', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'InitialBalanceInvoice', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'InitialBalanceInvoice', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'InitialBalanceInvoice', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'InitialBalanceInvoice', @level2type = N'COLUMN', @level2name = N'ModificationDate';
