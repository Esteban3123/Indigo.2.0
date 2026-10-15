CREATE TABLE [GeneralLedger].[VieBot] (
    [Id]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Form]                VARCHAR (200) NOT NULL,
    [HandlesHomologation] BIT           CONSTRAINT [DF_VieBot_HandlesHomologation] DEFAULT ((1)) NOT NULL,
    [LegalBookId]         INT           NOT NULL,
    [Allow]               BIT           NOT NULL,
    [CreationUser]        VARCHAR (20)  NOT NULL,
    [CreationDate]        DATETIME      NOT NULL,
    [ModificationUser]    VARCHAR (20)  NULL,
    [ModificationDate]    DATETIME      NULL,
    CONSTRAINT [PK_VieBot__Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el formulario que se va a configurar    /************* Tesoreria *************/  CashReceipts - Recibos de Caja  VoucherTransaction - Comprobante de egreso  TreasuryNote - Notas de tesoreria  Consignment - Consignaciones  CrossingAccount - Cruce de cuentas    /************* Cuentas por Pagar *************/  DeferredCausation - Amortizacion Mensual  AccountPayable - Cuentas por Pagar  PaymentNotes - Notas Debito/Credito  PaymentTransfer - Cruce de Anticipos Vs CxP    /************* Cuentas por Cobrar ************/  RadicateInvoiceC - Radicacion de Cuentas  PortfolioNote - Notas Debito/Credito  PortfolioTransfer - Cruce de Anticipo Vs CxC  AccountReceivableDocument - Documento Cuenta por Cobrar    /************* Facturacion *******************/  Invoice - Factura  DocumentInvoiceProductSales - Factura de Productos  InvoiceEntityCapitated - Factura Entidad Capitada    /************* Inventarios ********************/  RemissionEntrance - Remision de Entrada  RemissionDevolution - Devolucion de Remision  InventoryAdjustment - Ajuste de Inventario  PharmaceuticalDispensing - Dispensacion Farmaceutica  PharmaceuticalDispensingDevolution - Devolucion de Dispensacion  TransferOrder - Orden de Traslado  TransferOrderDevolution - Devolucion de Orden de Traslado  EntranceVoucher - Comprobate de Entrada  EntranceVoucherDevolution - Devolucion de Comprobate de Entrada  LoanMerchandise - Prestamo de Mercancia  LoanMerchandiseDevolution - Devolucion de Prestamo de Mercancia  ', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'VieBot', @level2type = N'COLUMN', @level2name = N'Form';

