CREATE TABLE [Treasury].[VoucherTransaction] (
    [Id]                        INT                                                                            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                      VARCHAR (20)                                                                   NOT NULL,
    [IdThirdParty]              INT                                                                            NULL,
    [IdMainAccount]             INT                                                                            NOT NULL,
    [IdCostCenter]              INT                                                                            NULL,
    [VoucherClass]              INT                                                                            CONSTRAINT [DF_VoucherTransaction_VoucherClass] DEFAULT ((1)) NOT NULL,
    [ExpenseType]               TINYINT                                                                        CONSTRAINT [DF_VoucherTransaction_AffectBank] DEFAULT ((0)) NOT NULL,
    [Detail]                    VARCHAR (MAX)                                                                  CONSTRAINT [DF_VoucherTransaction_Detail] DEFAULT ('-') NOT NULL,
    [DocumentDate]              DATETIME                                                                       NOT NULL,
    [IdCashRegister]            INT                                                                            NULL,
    [IdEntityBankAccount]       INT                                                                            NULL,
    [Value]                     DECIMAL (18, 2)                                                                NOT NULL,
    [PaymentMethod]             TINYINT                                                                        NULL,
    [NoteNumber]                VARCHAR (50)                                                                   NULL,
    [IdChecks]                  INT                                                                            NULL,
    [CheckNumber]               BIGINT                                                                         NULL,
    [TransactionDate]           DATETIME                                                                       NULL,
    [TaxByMil]                  BIT                                                                            CONSTRAINT [DF_VoucherTransaction_TaxByMil] DEFAULT ((0)) NOT NULL,
    [TaxByMilValue]             DECIMAL (18, 2)                                                                NULL,
    [CashRegisterExpense]       BIT                                                                            CONSTRAINT [DF_VoucherTransaction_CashRegisterExpense] DEFAULT ((0)) NOT NULL,
    [RefundCashRegisterExpense] BIT                                                                            CONSTRAINT [DF_VoucherTransaction_RefundCashRegisterExpense] DEFAULT ((0)) NOT NULL,
    [SchedulePaymentId]         INT                                                                            NULL,
    [BeneficiaryIdentification] VARCHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identity_Ofuscado", 0)')     NULL,
    [Beneficiary]               VARCHAR (100) MASKED WITH (FUNCTION = 'partial(0, "Beneficiary_Ofuscado", 0)') NULL,
    [TransactionRelationship]   BIT                                                                            CONSTRAINT [DF_VoucherTransaction_TransactionRelationship] DEFAULT ((0)) NULL,
    [CheckReconciled]           BIT                                                                            CONSTRAINT [DF_VoucherTransaction_CheckReconciled] DEFAULT ((0)) NULL,
    [IdPaymentOrder]            INT                                                                            NULL,
    [Printed]                   BIT                                                                            CONSTRAINT [DF_VoucherTransaction_Printed] DEFAULT ((0)) NULL,
    [RTEValue]                  DECIMAL (18, 2)                                                                CONSTRAINT [DF_VoucherTransaction_RTEValue] DEFAULT ((0.0)) NULL,
    [IVAValue]                  DECIMAL (18, 2)                                                                CONSTRAINT [DF_VoucherTransaction_IVAValue] DEFAULT ((0.0)) NULL,
    [ICAValue]                  DECIMAL (18, 2)                                                                CONSTRAINT [DF_VoucherTransaction_ICAValue] DEFAULT ((0.0)) NULL,
    [OtherValue]                DECIMAL (18, 2)                                                                CONSTRAINT [DF_VoucherTransaction_OtherValue] DEFAULT ((0.0)) NULL,
    [BankAccountNumber]         VARCHAR (50) MASKED WITH (FUNCTION = 'partial(0, "Account_Ofuscado", 0)')      NULL,
    [BankName]                  VARCHAR (100) MASKED WITH (FUNCTION = 'partial(0, "Bank_Ofuscado", 0)')        NULL,
    [DetailsInterfaceBudget]    VARCHAR (100)                                                                  NULL,
    [IdUnitOperative]           INT                                                                            NOT NULL,
    [IdRefund]                  INT                                                                            NULL,
    [Status]                    TINYINT                                                                        NOT NULL,
    [CreationUser]              VARCHAR (20)                                                                   NOT NULL,
    [CreationDate]              DATETIME                                                                       NOT NULL,
    [ModificationUser]          VARCHAR (20)                                                                   NULL,
    [ModificationDate]          DATETIME                                                                       NULL,
    [ConfirmationUser]          VARCHAR (20)                                                                   NULL,
    [ConfirmationDate]          DATETIME                                                                       NULL,
    [AnnulmentUser]             VARCHAR (20)                                                                   NULL,
    [AnnulmentDate]             DATETIME                                                                       NULL,
    [ReversedUser]              VARCHAR (20)                                                                   NULL,
    [ReversedDate]              DATETIME                                                                       NULL,
    [TimeStamp]                 ROWVERSION                                                                     NOT NULL,
    [EmailSent]                 BIT                                                                            CONSTRAINT [DF_VoucherTransaction_EmailSent] DEFAULT ((0)) NOT NULL,
    [IdCheckCashingStatus]      TINYINT                                                                        NULL,
    [HandlesDocumentSupport]    BIT                                                                            CONSTRAINT [DF__VoucherTr__Handl__452597E3] DEFAULT ((0)) NOT NULL,
    [CurrencyId]                INT                                                                            CONSTRAINT [DF__VoucherTr__Curre__77F10ED2] DEFAULT ((1)) NULL,
    [SupplierBankAccountId]     INT                                                                            NULL,
    CONSTRAINT [PK_VoucherTransaction__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CK_VoucherTransaction] CHECK (case when [Status]=(2) AND [ConfirmationUser] IS NULL then (0) else (1) end=(1)),
    CONSTRAINT [FK_VoucherTransaction_CashRegisters] FOREIGN KEY ([IdCashRegister]) REFERENCES [Treasury].[CashRegisters] ([Id]),
    CONSTRAINT [FK_VoucherTransaction_Checkbooks] FOREIGN KEY ([IdChecks]) REFERENCES [Treasury].[Checkbooks] ([Id]),
    CONSTRAINT [FK_VoucherTransaction_CostCenter] FOREIGN KEY ([IdCostCenter]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_VoucherTransaction_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_VoucherTransaction_EntityBankAccounts] FOREIGN KEY ([IdEntityBankAccount]) REFERENCES [Treasury].[EntityBankAccounts] ([Id]),
    CONSTRAINT [FK_VoucherTransaction_MainAccounts] FOREIGN KEY ([IdMainAccount]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_VoucherTransaction_OperatingUnit] FOREIGN KEY ([IdUnitOperative]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_VoucherTransaction_Refunds] FOREIGN KEY ([IdRefund]) REFERENCES [Treasury].[Refunds] ([Id]),
    CONSTRAINT [FK_VoucherTransaction_SchedulePayment] FOREIGN KEY ([SchedulePaymentId]) REFERENCES [Treasury].[SchedulePayment] ([Id]),
    CONSTRAINT [FK_VoucherTransaction_SupplierBankAccount] FOREIGN KEY ([SupplierBankAccountId]) REFERENCES [Common].[SupplierBankAccount] ([Id]),
    CONSTRAINT [FK_VoucherTransaction_ThirdParty] FOREIGN KEY ([IdThirdParty]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [UQ_VoucherTransaction__Code] UNIQUE NONCLUSTERED ([Code] ASC)
);


GO
ALTER TABLE [Treasury].[VoucherTransaction] NOCHECK CONSTRAINT [CK_VoucherTransaction];


GO
ALTER TABLE [Treasury].[VoucherTransaction] NOCHECK CONSTRAINT [FK_VoucherTransaction_CashRegisters];


GO
ALTER TABLE [Treasury].[VoucherTransaction] NOCHECK CONSTRAINT [FK_VoucherTransaction_Checkbooks];


GO
ALTER TABLE [Treasury].[VoucherTransaction] NOCHECK CONSTRAINT [FK_VoucherTransaction_CostCenter];


GO
ALTER TABLE [Treasury].[VoucherTransaction] NOCHECK CONSTRAINT [FK_VoucherTransaction_Currency];


GO
ALTER TABLE [Treasury].[VoucherTransaction] NOCHECK CONSTRAINT [FK_VoucherTransaction_EntityBankAccounts];


GO
ALTER TABLE [Treasury].[VoucherTransaction] NOCHECK CONSTRAINT [FK_VoucherTransaction_MainAccounts];


GO
ALTER TABLE [Treasury].[VoucherTransaction] NOCHECK CONSTRAINT [FK_VoucherTransaction_OperatingUnit];


GO
ALTER TABLE [Treasury].[VoucherTransaction] NOCHECK CONSTRAINT [FK_VoucherTransaction_Refunds];


GO
ALTER TABLE [Treasury].[VoucherTransaction] NOCHECK CONSTRAINT [FK_VoucherTransaction_SchedulePayment];


GO
ALTER TABLE [Treasury].[VoucherTransaction] NOCHECK CONSTRAINT [FK_VoucherTransaction_SupplierBankAccount];


GO
ALTER TABLE [Treasury].[VoucherTransaction] NOCHECK CONSTRAINT [FK_VoucherTransaction_ThirdParty];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la cuenta bancaria del proveedor.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'SupplierBankAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la moneda de la caja o cuenta bancaria', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'CurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Maneja Documento Soporte Electronico 1=true,0=false', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'HandlesDocumentSupport';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de estado de cheque', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'IdCheckCashingStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identifica si ya se envió correo de ese comprobante de egreso', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'EmailSent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la fecha en que se reverso el documento', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'ReversedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Usuario que reverso el documento', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'ReversedUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Anulación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Anulación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Confirmación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Confirmación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado (registrado = 1,confirmado = 2,anulado = 3, Reversado = 4)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del reembolso', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'IdRefund';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la unidad operativa.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'IdUnitOperative';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Detalles de la interface de presupuesto', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'DetailsInterfaceBudget';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del banco', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'BankName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de la cuenta que consigna', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'BankAccountNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Otros valores', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'OtherValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del ICA', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'ICAValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del IVA', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'IVAValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la retencion', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'RTEValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Comprobante impreso - Por definir mecanismo de control de impresiones', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'Printed';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la orden de pago', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'IdPaymentOrder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cheque conciliado', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'CheckReconciled';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Egreso de relacion de giro', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'TransactionRelationship';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del beneficiario del cheque o la nota', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'Beneficiary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificacion del beneficiario', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'BeneficiaryIdentification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'este campo se relaciona con la cabecera de la programacion de pagos, esto con el fin de saber que comprobantes de egreso fueron generados con una programacion de pagos', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'SchedulePaymentId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Reembolso de egreso de caja menor', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'RefundCashRegisterExpense';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Egreso de caja menor', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'CashRegisterExpense';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del impuesto por mil', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'TaxByMilValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Aplica impuesto por mil', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'TaxByMil';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha la consignación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'TransactionDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del cheque', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'CheckNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la chequera', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'IdChecks';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de la nota', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'NoteNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Método de pago. Cheque = 1, Nota débito = 2', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'PaymentMethod';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor pagado', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta bancaria de la entidad', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'IdEntityBankAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la caja', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'IdCashRegister';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del documento', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Detalle del comprobante', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'Detail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Egreso: 1-Afecta Banco; 2- Afecta Caja Menor; 3 - Afecta Caja Mayor; 4 - Producto Bancario', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'ExpenseType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Comprobante (1 - Pago; 2 - Reembolso; 3 - Traslado)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'VoucherClass';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del centro de costo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'IdCostCenter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuanta contable', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'IdMainAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tercero', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'IdThirdParty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene la secuencia numerica', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'Id';

