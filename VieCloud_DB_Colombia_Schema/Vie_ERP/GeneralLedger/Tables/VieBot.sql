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
CREATE UNIQUE NONCLUSTERED INDEX [UQ_VieBot__Form__LegalBookId]
    ON [GeneralLedger].[VieBot]([Form] ASC, [LegalBookId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del registro de configuración del formulario en el libro oficial (DATETIME, PII-Auditoría)', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'VieBot', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'VieBot', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'VieBot', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación de la configuración del formulario (VARCHAR 20, PII-Auditoría, referencia a usuario del sistema)', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'VieBot', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'VieBot', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'VieBot', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de configuración del formulario en el libro oficial (DATETIME, PII-Auditoría)', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'VieBot', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'VieBot', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'VieBot', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó la configuración del formulario (VARCHAR 20, PII-Auditoría, referencia a usuario del sistema)', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'VieBot', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'VieBot', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'VieBot', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano que determina si el formulario está habilitado (permitido) para su uso en el proceso contable (BIT: 1=Activo, 0=Inactivo)', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'VieBot', @level2type = N'COLUMN', @level2name = N'Allow';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permitir ', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'VieBot', @level2type = N'COLUMN', @level2name = N'Allow';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'VieBot', @level2type = N'COLUMN', @level2name = N'Allow';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del libro oficial contable asociado a esta configuración de formulario (INT, FK a tabla de libros)', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'VieBot', @level2type = N'COLUMN', @level2name = N'LegalBookId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del libro oficial', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'VieBot', @level2type = N'COLUMN', @level2name = N'LegalBookId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'VieBot', @level2type = N'COLUMN', @level2name = N'LegalBookId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica si el formulario o proceso puede ejecutarse mediante homologación; algunos procesos como Depreciación y Salida de Activos no aplican homologación (BIT: 1=Sí maneja, 0=No maneja)', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'VieBot', @level2type = N'COLUMN', @level2name = N'HandlesHomologation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el formulario o el Proceso se puede ejecutar a traves de Homologacion, Como por Ejemplo Depreciacion y Salida de Activos estos no manejan homologacion', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'VieBot', @level2type = N'COLUMN', @level2name = N'HandlesHomologation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'VieBot', @level2type = N'COLUMN', @level2name = N'HandlesHomologation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del formulario a configurar en el módulo contable: Tesorería (CashReceipts, VoucherTransaction, TreasuryNote, Consignment, CrossingAccount), Cuentas por Pagar (DeferredCausation, AccountPayable, PaymentNotes, PaymentTransfer), Cuentas por Cobrar (RadicateInvoiceC, PortfolioNote, PortfolioTransfer, AccountReceivableDocument), Facturación (Invoice, DocumentInvoiceProductSales, InvoiceEntityCapitated), Inventarios (RemissionEntrance, RemissionDevolution, InventoryAdjustment, PharmaceuticalDispensing, PharmaceuticalDispensingDevolution, TransferOrder, TransferOrderDevolution, EntranceVoucher, EntranceVoucherDevolution, LoanMerchandise, LoanMerchandiseDevolution) (VARCHAR 200)', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'VieBot', @level2type = N'COLUMN', @level2name = N'Form';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el formulario que se va a configurar    /************* Tesoreria *************/  CashReceipts - Recibos de Caja  VoucherTransaction - Comprobante de egreso  TreasuryNote - Notas de tesoreria  Consignment - Consignaciones  CrossingAccount - Cruce de cuentas    /************* Cuentas por Pagar *************/  DeferredCausation - Amortizacion Mensual  AccountPayable - Cuentas por Pagar  PaymentNotes - Notas Debito/Credito  PaymentTransfer - Cruce de Anticipos Vs CxP    /************* Cuentas por Cobrar ************/  RadicateInvoiceC - Radicacion de Cuentas  PortfolioNote - Notas Debito/Credito  PortfolioTransfer - Cruce de Anticipo Vs CxC  AccountReceivableDocument - Documento Cuenta por Cobrar    /************* Facturacion *******************/  Invoice - Factura  DocumentInvoiceProductSales - Factura de Productos  InvoiceEntityCapitated - Factura Entidad Capitada    /************* Inventarios ********************/  RemissionEntrance - Remision de Entrada  RemissionDevolution - Devolucion de Remision  InventoryAdjustment - Ajuste de Inventario  PharmaceuticalDispensing - Dispensacion Farmaceutica  PharmaceuticalDispensingDevolution - Devolucion de Dispensacion  TransferOrder - Orden de Traslado  TransferOrderDevolution - Devolucion de Orden de Traslado  EntranceVoucher - Comprobate de Entrada  EntranceVoucherDevolution - Devolucion de Comprobate de Entrada  LoanMerchandise - Prestamo de Mercancia  LoanMerchandiseDevolution - Devolucion de Prestamo de Mercancia  ', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'VieBot', @level2type = N'COLUMN', @level2name = N'Form';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'VieBot', @level2type = N'COLUMN', @level2name = N'Form';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (IDENTITY) que identifica de forma única cada configuración de formulario en el libro oficial (INT, PK)', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'VieBot', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'VieBot', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'VieBot', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuración del bot VieBot para el módulo de contabilidad general: define qué formularios o pantallas del sistema tienen permitida la asistencia automática del bot, a qué libro contable legal están asociados y si manejan homologación de cuentas.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'VieBot';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'VieBot';
