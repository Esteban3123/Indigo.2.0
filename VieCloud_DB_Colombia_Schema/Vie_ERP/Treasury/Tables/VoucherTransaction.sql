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
ADD SENSITIVITY CLASSIFICATION TO
    [Treasury].[VoucherTransaction].[BeneficiaryIdentification]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');

GO
ADD SENSITIVITY CLASSIFICATION TO
    [Treasury].[VoucherTransaction].[Beneficiary]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');

GO
ADD SENSITIVITY CLASSIFICATION TO
    [Treasury].[VoucherTransaction].[BankAccountNumber]
    WITH (LABEL = 'Sensitive - Financial', INFORMATION_TYPE = 'Financial');

GO
ADD SENSITIVITY CLASSIFICATION TO
    [Treasury].[VoucherTransaction].[BankName]
    WITH (LABEL = 'Confidential - Financial', INFORMATION_TYPE = 'Financial');

GO
ALTER TABLE [Treasury].[VoucherTransaction] NOCHECK CONSTRAINT [CK_VoucherTransaction];

GO

ALTER TABLE [Treasury].[VoucherTransaction] NOCHECK CONSTRAINT [CK_VoucherTransaction];

GO

ALTER TABLE [Treasury].[VoucherTransaction] NOCHECK CONSTRAINT [CK_VoucherTransaction];
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT FK. Identificador de la cuenta bancaria del proveedor o acreedor. Vinculado a SupplierBankAccount para trazabilidad de pagos a terceros.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'SupplierBankAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta bancaria del proveedor.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'SupplierBankAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'SupplierBankAccountId';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT FK, default 1. Identificador de la moneda (divisa) de la transacción: pesos, dólares, etc. Referencia a tabla Currency para conversiones.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'CurrencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la moneda de la caja o cuenta bancaria', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'CurrencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'CurrencyId';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT (0/1). Indica si el comprobante maneja documento soporte electrónico (DSE) obligatorio en RIPS y contabilidad. 1=Sí maneja, 0=No.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'HandlesDocumentSupport';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Maneja Documento Soporte Electronico 1=true,0=false', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'HandlesDocumentSupport';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'HandlesDocumentSupport';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT. Identificador del estado de cobro/procesamiento del cheque: pendiente, cobrado, rechazado, etc.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'IdCheckCashingStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de estado de cheque', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'IdCheckCashingStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'IdCheckCashingStatus';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT (0/1). Marca si se envió correo electrónico de notificación del comprobante de egreso al beneficiario o gestor.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'EmailSent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identifica si ya se envió correo de ese comprobante de egreso', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'EmailSent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'EmailSent';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TIMESTAMP. Marca temporal automática de creación, modificación o registro del comprobante. Auditoria del instante exacto del evento.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'TimeStamp';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME. Fecha y hora en que se revirtió (reversó) el comprobante de egreso. Anula efectos contables y tesorería.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'ReversedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la fecha en que se reverso el documento', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'ReversedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'ReversedDate';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(20). Usuario del sistema que ejecutó la reversión del comprobante. Trazabilidad de cambios.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'ReversedUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Usuario que reverso el documento', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'ReversedUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'ReversedUser';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME. Fecha y hora de anulación del comprobante. Registra cuándo se anuló el documento.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Anulación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(20). Usuario que anuló el comprobante. Responsable del cambio de estado.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Anulación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME. Fecha y hora en que se confirmó (autorizó) el comprobante de egreso. Transición a estado aprobado.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Confirmación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(20). Usuario autorizado que confirmó el comprobante. Responsable de la aprobación.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Confirmación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME. Fecha y hora de la última modificación del registro. Auditoria de cambios.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'ModificationDate';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(20). Usuario que modificó el comprobante. Trazabilidad de ediciones.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'ModificationUser';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME. Fecha y hora de creación inicial del comprobante de egreso.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'CreationDate';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(20). Usuario que creó el comprobante. Responsable del registro original.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'CreationUser';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT. Estado del comprobante: 1=Registrado, 2=Confirmado, 3=Anulado, 4=Reversado. Control de ciclo de vida.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado (registrado = 1,confirmado = 2,anulado = 3, Reversado = 4)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'Status';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT FK. Identificador del reembolso asociado (cuando VoucherClass=2). Vincula a tabla Refunds.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'IdRefund';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del reembolso', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'IdRefund';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'IdRefund';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT FK NOT NULL. Identificador de la unidad operativa, centro de atención o sucursal donde se generó el egreso.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'IdUnitOperative';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad operativa.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'IdUnitOperative';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'IdUnitOperative';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(100). Detalles o metadata de la interfaz de presupuesto. Información de sincronización con módulo de presupuestación.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'DetailsInterfaceBudget';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Detalles de la interface de presupuesto', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'DetailsInterfaceBudget';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'DetailsInterfaceBudget';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(100) MASKED. Nombre del banco destino (PII ofuscado). Identifica la institución financiera de depósito.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'BankName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del banco', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'BankName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'BankName';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(50) MASKED. Número de cuenta bancaria destino (PII ofuscado). Cuenta donde se consigna o transfiere el pago.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'BankAccountNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de la cuenta que consigna', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'BankAccountNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'BankAccountNumber';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DECIMAL(18,2). Valor de otros impuestos o retenciones no categorizados (diferente a RTE, IVA, ICA).', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'OtherValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Otros valores', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'OtherValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'OtherValue';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DECIMAL(18,2). Valor de Impuesto de Industria y Comercio retenido. Impuesto municipal sobre transacciones.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'ICAValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del ICA', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'ICAValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'ICAValue';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DECIMAL(18,2). Valor de Impuesto al Valor Agregado retenido. Retención tributaria del 8%, 16%, etc.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'IVAValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del IVA', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'IVAValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'IVAValue';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DECIMAL(18,2). Valor de Retención en la Fuente (RTE). Retención sobre servicios, honorarios, compras según porcentaje.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'RTEValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la retencion', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'RTEValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'RTEValue';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT (0/1). Marca si el comprobante fue impreso. Control de impresiones (mecanismo a definir).', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'Printed';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Comprobante impreso - Por definir mecanismo de control de impresiones', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'Printed';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'Printed';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT FK. Identificador de la orden de pago (OP) asociada. Vinculación con proceso de pagos programados.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'IdPaymentOrder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la orden de pago', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'IdPaymentOrder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'IdPaymentOrder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT (0/1). Indica si es un egreso de relación de giro (transferencia entre cuentas internas).', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'TransactionRelationship';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Egreso de relacion de giro', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'TransactionRelationship';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'TransactionRelationship';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(100) MASKED. Nombre del beneficiario del cheque, nota o transferencia (PII ofuscado).', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'Beneficiary';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del beneficiario del cheque o la nota', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'Beneficiary';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'Beneficiary';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(20) MASKED. Cédula, RUC o identificación del beneficiario (PII ofuscado). Documento de identidad.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'BeneficiaryIdentification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificacion del beneficiario', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'BeneficiaryIdentification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'BeneficiaryIdentification';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT FK. Identificador de la programación de pagos (cabecera). Vincula comprobante a lote de pagos programados.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'SchedulePaymentId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'este campo se relaciona con la cabecera de la programacion de pagos, esto con el fin de saber que comprobantes de egreso fueron generados con una programacion de pagos', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'SchedulePaymentId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'SchedulePaymentId';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT (0/1). Marca reembolso de egreso de caja menor. Devolución de dinero de caja de mano.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'RefundCashRegisterExpense';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Reembolso de egreso de caja menor', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'RefundCashRegisterExpense';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'RefundCashRegisterExpense';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT (0/1). Marca si es egreso de caja menor (fondo fijo). 1=Es caja menor, 0=No afecta caja menor.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'CashRegisterExpense';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Egreso de caja menor', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'CashRegisterExpense';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'CashRegisterExpense';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DECIMAL(18,2). Valor del impuesto por mil (arancel de tesorería). Comisión por transacción bancaria.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'TaxByMilValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del impuesto por mil', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'TaxByMilValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'TaxByMilValue';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT (0/1). Indica si aplica impuesto por mil. 1=Se aplica, 0=No se aplica.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'TaxByMil';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Aplica impuesto por mil', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'TaxByMil';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'TaxByMil';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME. Fecha y hora de la consignación o depósito en banco. Diferente de DocumentDate.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'TransactionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha la consignación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'TransactionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'TransactionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT FK. Identificador de la chequera (talonario). Referencia a tabla Checkbooks.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'IdChecks';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la chequera', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'IdChecks';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'IdChecks';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(50). Número de nota débito o crédito. Correlativo del documento de transporte bancario.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'NoteNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de la nota', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'NoteNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'NoteNumber';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT. Método de pago: 1=Cheque, 2=Nota débito. Tipo de instrumento de pago utilizado.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'PaymentMethod';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Método de pago. Cheque = 1, Nota débito = 2', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'PaymentMethod';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'PaymentMethod';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DECIMAL(18,2) NOT NULL. Valor total pagado en la transacción. Monto del comprobante de egreso.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor pagado', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'Value';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT FK. Identificador de la cuenta bancaria de la entidad (origen). Vinculación a EntityBankAccounts.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'IdEntityBankAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta bancaria de la entidad', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'IdEntityBankAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'IdEntityBankAccount';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT FK. Identificador de la caja registradora o caja de caudales. Referencia a CashRegisters.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'IdCashRegister';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la caja', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'IdCashRegister';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'IdCashRegister';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME NOT NULL. Fecha del documento comprobante. Día en que se expide el egreso.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del documento', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'DocumentDate';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(MAX). Detalle o concepto del comprobante de egreso. Descripción del motivo del pago.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'Detail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Detalle del comprobante', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'Detail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'Detail';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT. Tipo de egreso: 1=Afecta Banco, 2=Afecta Caja Menor, 3=Afecta Caja Mayor, 4=Producto Bancario.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'ExpenseType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Egreso: 1-Afecta Banco; 2- Afecta Caja Menor; 3 - Afecta Caja Mayor; 4 - Producto Bancario', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'ExpenseType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'ExpenseType';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT default 1. Tipo de comprobante: 1=Pago, 2=Reembolso, 3=Traslado. Clasificación del documento.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'VoucherClass';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Comprobante (1 - Pago; 2 - Reembolso; 3 - Traslado)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'VoucherClass';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'VoucherClass';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT FK. Identificador del centro de costo (departamento, proyecto). Distribución de gastos contables.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'IdCostCenter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del centro de costo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'IdCostCenter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'IdCostCenter';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT FK NOT NULL. Identificador de la cuenta contable principal. Vinculación a tabla MainAccounts en Libro Mayor.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'IdMainAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuanta contable', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'IdMainAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'IdMainAccount';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT FK. Identificador del tercero (proveedor, acreedor, beneficiario). Referencia a tabla ThirdParty.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'IdThirdParty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'IdThirdParty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'IdThirdParty';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(20) UNIQUE NOT NULL. Código o secuencia numérica única del comprobante de egreso. Correlativo del sistema.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene la secuencia numerica', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'Code';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT PK IDENTITY. Identificador único (primary key) del registro de transacción de comprobante. Clave de la tabla.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'Id';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de transacciones contables de tesorería (comprobantes de egreso, pagos, reembolsos y movimientos de caja). Cada fila representa un movimiento financiero con su beneficiario, cuenta contable, centro de costo, retenciones, impuestos y estado del proceso de pago.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction';
