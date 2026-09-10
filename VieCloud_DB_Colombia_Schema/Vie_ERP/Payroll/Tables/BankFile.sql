CREATE TABLE [Payroll].[BankFile] (
    [Id]                     INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                   VARCHAR (20) NOT NULL,
    [OperatingUnitId]        INT          NOT NULL,
    [CompanyId]              INT          NOT NULL,
    [LiquidationDate]        DATE         NOT NULL,
    [EntityBankAccountId]    INT          NOT NULL,
    [ExpenseConceptId]       INT          NOT NULL,
    [VoucherTransactionId]   INT          NULL,
    [Value]                  NUMERIC (18) NOT NULL,
    [Status]                 TINYINT      NOT NULL,
    [CreationUser]           VARCHAR (20) NOT NULL,
    [CreationDate]           DATETIME     NOT NULL,
    [ModificationUser]       VARCHAR (20) NULL,
    [ModificationDate]       DATETIME     NULL,
    [ConfirmationUser]       VARCHAR (20) NULL,
    [ConfirmationDate]       DATETIME     NULL,
    [AnnulmentUser]          VARCHAR (20) NULL,
    [AnnulmentDate]          DATETIME     NULL,
    [TimeStamp]              ROWVERSION   NOT NULL,
    [Process]                TINYINT      CONSTRAINT [DF__BankFile__Proces__43E8263E] DEFAULT ((2)) NOT NULL,
    [PeriodIncentivePayment] TINYINT      NULL,
    [YearIncentivePayment]   INT          NULL,
    [IncentivePaymentId]     INT          NULL,
    CONSTRAINT [PK_BankFile] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BankFile_Company] FOREIGN KEY ([CompanyId]) REFERENCES [Payroll].[Company] ([Id]),
    CONSTRAINT [FK_BankFile_EntityBankAccounts] FOREIGN KEY ([EntityBankAccountId]) REFERENCES [Treasury].[EntityBankAccounts] ([Id]),
    CONSTRAINT [FK_BankFile_ExpenseConcepts] FOREIGN KEY ([ExpenseConceptId]) REFERENCES [Treasury].[ExpenseConcepts] ([Id]),
    CONSTRAINT [FK_BankFile_IncentivePayment] FOREIGN KEY ([IncentivePaymentId]) REFERENCES [Payroll].[IncentivePayment] ([Id]),
    CONSTRAINT [FK_BankFile_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_BankFile_VoucherTransaction] FOREIGN KEY ([VoucherTransactionId]) REFERENCES [Treasury].[VoucherTransaction] ([Id])
);


GO
ALTER TABLE [Payroll].[BankFile] NOCHECK CONSTRAINT [FK_BankFile_ExpenseConcepts];




GO



GO



GO
ALTER TABLE [Payroll].[BankFile] NOCHECK CONSTRAINT [FK_BankFile_ExpenseConcepts];


GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del registro de incentivo de pago (FK a IncentivePayment). Vincula el archivo bancario con la liquidación de bonificaciones, primas o incentivos al personal.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'IncentivePaymentId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del incentivo de pago', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'IncentivePaymentId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'IncentivePaymentId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Año fiscal o período anual del pago de incentivos. Agrupa liquidaciones de bonificaciones, gratificaciones o beneficios extraordinarios por ejercicio contable.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'YearIncentivePayment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Pago de incentivo anual', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'YearIncentivePayment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'YearIncentivePayment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Período o mes específico del pago de incentivos (TINYINT). Identifica el ciclo temporal (mensual, bimestral, trimestral) de la liquidación de bonos, primas o compensaciones variables.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'PeriodIncentivePayment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Pago de incentivos por período', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'PeriodIncentivePayment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'PeriodIncentivePayment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del proceso de generación del archivo bancario. Controla el flujo de creación, validación y transmisión del archivo plano a entidades financieras (default=2).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'Process';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Proceso', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'Process';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'Process';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca de tiempo automática de auditoría. Registra el instante exacto (creación, modificación, confirmación o anulación) del evento en el archivo bancario para trazabilidad.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de anulación del archivo bancario. Identifica cuándo se canceló, revocó o anuló el registro de pago (DATE, auditoría).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Anulación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la anulación del archivo bancario. Nombre o ID del colaborador que canceló o revocó el pago (VARCHAR 20, auditoría PII).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Anulación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de confirmación del archivo bancario. Marca cuándo se validó y confirmó la transmisión al banco o entidad financiera (DATE, auditoría).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Confirmación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que confirmó el archivo bancario. Nombre o ID del colaborador que validó y autorizó el envío al banco (VARCHAR 20, auditoría PII).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Confirmación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de última modificación del archivo bancario. Registra cuándo se actualizaron datos, montos o referencias (DATETIME, auditoría).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación. Nombre o ID del colaborador que editó el registro (VARCHAR 20, auditoría PII).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación del archivo bancario. Marca el instante de generación inicial del registro de pago (DATETIME, auditoría).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el archivo bancario. Nombre o ID del colaborador que originó el registro (VARCHAR 20, auditoría PII).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del archivo bancario (TINYINT): 1=Registrado, 2=Confirmado, 3=Anulado. Refleja el ciclo de vida del pago en la transmisión bancaria.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado      1. Registrado      2. Confirmado      3. Anulado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto total pagado en el archivo bancario (NUMERIC 18). Importe en moneda corriente del pago de nómina, incentivos o egreso correspondiente.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Total Pagado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del comprobante contable de egreso (FK VoucherTransaction). Se genera solo si el pago se registró en asientos contables.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'VoucherTransactionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Comprobante de Egreso (Se llena si se generó)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'VoucherTransactionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'VoucherTransactionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del concepto de egreso (FK ExpenseConcepts). Clasifica el tipo de gasto: nómina, prestaciones, bonos, deducciones o incentivos.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'ExpenseConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id concepto de Egreso', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'ExpenseConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'ExpenseConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta bancaria de la entidad (FK EntityBankAccounts). Especifica el banco y cuenta desde la cual se efectuará el desembolso.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'EntityBankAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Entidad Bancaria con la cual se realizara el pago', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'EntityBankAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'EntityBankAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de liquidación a pagar (DATE). Indica la fecha valor o vencimiento del pago que será transmitido al banco.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'LiquidationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la liquidacion a pagar', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'LiquidationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'LiquidationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la compañía o empresa (FK Company). Agrupa el archivo bancario bajo la entidad jurídica responsable del pago.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'CompanyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la compañia', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'CompanyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'CompanyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad operativa (FK OperatingUnit). Centro de atención, sede, sucursal o centro de costos que origina el pago.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad operativa', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del archivo plano. Identificador alfanumérico que referencia el nombre o secuencia del archivo transmitido a entidades bancarias (VARCHAR 20).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Archivo Plano', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico de la tabla (INT IDENTITY). Clave primaria única que identifica cada registro de archivo bancario en el sistema.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de archivos planos de pago a bancos generados en el proceso de nómina. Contiene los pagos liquidados por concepto de gasto (salarios, incentivos, deducciones) asociados a una unidad operativa y empresa, con su estado de procesamiento, confirmación o anulación.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile';
