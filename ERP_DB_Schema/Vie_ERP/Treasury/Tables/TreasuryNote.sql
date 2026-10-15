CREATE TABLE [Treasury].[TreasuryNote] (
    [Id]                   INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                 VARCHAR (20)    NOT NULL,
    [NoteDate]             DATETIME        CONSTRAINT [DF_TreasuryNote_NoteDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [NoteType]             TINYINT         NOT NULL,
    [OperatingUnitId]      INT             CONSTRAINT [DF_TreasuryNote_OperatingUnitId] DEFAULT ((9)) NOT NULL,
    [CashRegisterId]       INT             NULL,
    [EntityBankAccountId]  INT             NULL,
    [VoucherTransactionId] INT             NULL,
    [CashReceiptId]        INT             NULL,
    [MainAccountId]        INT             NULL,
    [CostCenterId]         INT             NULL,
    [Description]          VARCHAR (300)   NOT NULL,
    [Nature]               TINYINT         NULL,
    [Value]                DECIMAL (18, 2) NULL,
    [Status]               TINYINT         NOT NULL,
    [CreationUser]         VARCHAR (20)    NOT NULL,
    [CreationDate]         DATETIME        NOT NULL,
    [ModificationUser]     VARCHAR (20)    NULL,
    [ModificationDate]     DATETIME        NULL,
    [ConfirmationUser]     VARCHAR (20)    NULL,
    [ConfirmationDate]     DATETIME        NULL,
    [AnnulmentUser]        VARCHAR (20)    NULL,
    [AnnulmentDate]        DATETIME        NULL,
    [TimeStamp]            ROWVERSION      NOT NULL,
    [ConsignmentId]        INT             NULL,
    [CrossingAccountId]    INT             NULL,
    [CurrencyId]           INT             CONSTRAINT [DF__TreasuryN__Curre__460F9A7B] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_TreasuryNote] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TreasuryNote_CashReceipts] FOREIGN KEY ([CashReceiptId]) REFERENCES [Treasury].[CashReceipts] ([Id]),
    CONSTRAINT [FK_TreasuryNote_CashRegisters] FOREIGN KEY ([CashRegisterId]) REFERENCES [Treasury].[CashRegisters] ([Id]),
    CONSTRAINT [FK_TreasuryNote_Consignment] FOREIGN KEY ([ConsignmentId]) REFERENCES [Treasury].[Consignment] ([Id]),
    CONSTRAINT [FK_TreasuryNote_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_TreasuryNote_CrossingAccount] FOREIGN KEY ([CrossingAccountId]) REFERENCES [Treasury].[CrossingAccount] ([Id]),
    CONSTRAINT [FK_TreasuryNote_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_TreasuryNote_EntityBankAccounts] FOREIGN KEY ([EntityBankAccountId]) REFERENCES [Treasury].[EntityBankAccounts] ([Id]),
    CONSTRAINT [FK_TreasuryNote_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_TreasuryNote_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_TreasuryNote_VoucherTransaction] FOREIGN KEY ([VoucherTransactionId]) REFERENCES [Treasury].[VoucherTransaction] ([Id])
);


GO
ALTER TABLE [Treasury].[TreasuryNote] NOCHECK CONSTRAINT [FK_TreasuryNote_CashReceipts];


GO
ALTER TABLE [Treasury].[TreasuryNote] NOCHECK CONSTRAINT [FK_TreasuryNote_CashRegisters];


GO
ALTER TABLE [Treasury].[TreasuryNote] NOCHECK CONSTRAINT [FK_TreasuryNote_Consignment];


GO
ALTER TABLE [Treasury].[TreasuryNote] NOCHECK CONSTRAINT [FK_TreasuryNote_CostCenter];


GO
ALTER TABLE [Treasury].[TreasuryNote] NOCHECK CONSTRAINT [FK_TreasuryNote_CrossingAccount];


GO
ALTER TABLE [Treasury].[TreasuryNote] NOCHECK CONSTRAINT [FK_TreasuryNote_Currency];


GO
ALTER TABLE [Treasury].[TreasuryNote] NOCHECK CONSTRAINT [FK_TreasuryNote_EntityBankAccounts];


GO
ALTER TABLE [Treasury].[TreasuryNote] NOCHECK CONSTRAINT [FK_TreasuryNote_MainAccounts];


GO
ALTER TABLE [Treasury].[TreasuryNote] NOCHECK CONSTRAINT [FK_TreasuryNote_OperatingUnit];


GO
ALTER TABLE [Treasury].[TreasuryNote] NOCHECK CONSTRAINT [FK_TreasuryNote_VoucherTransaction];


GO
CREATE NONCLUSTERED INDEX [IX_TreasuryNote__CrossingAccountId]
    ON [Treasury].[TreasuryNote]([CrossingAccountId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_TreasuryNote__Code]
    ON [Treasury].[TreasuryNote]([Code] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TreasuryNote_NoteType]
    ON [Treasury].[TreasuryNote]([NoteType] ASC)
    INCLUDE([Code], [NoteDate], [Description], [Status], [CreationUser], [ConsignmentId]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la moneda del contrato', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryNote', @level2type = N'COLUMN', @level2name = N'CurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador de CrossingAccount', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryNote', @level2type = N'COLUMN', @level2name = N'CrossingAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la consignación, solo se llena si el tipo de nota es "Reversión de Consignación"', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryNote', @level2type = N'COLUMN', @level2name = N'ConsignmentId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryNote', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Anulación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryNote', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Anulación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryNote', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Confirmación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryNote', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Confirmación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryNote', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryNote', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryNote', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryNote', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryNote', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado (registrado = 1,confirmado = 2,anulado = 3)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryNote', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryNote', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Naturaleza de la nota   1 - Debito  2 - Credito', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryNote', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena informacion sobre la nota de tesoreria.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryNote', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id centro de costo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryNote', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la cuenta principal.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryNote', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del recibo de caja, solo se llena si el tipo de nota es "Anulacion de recibos de caja"', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryNote', @level2type = N'COLUMN', @level2name = N'CashReceiptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del comprobante de egreso, solo se llena si el tipo de nota es "Anulacion de comprobantes de egreso"', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryNote', @level2type = N'COLUMN', @level2name = N'VoucherTransactionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta bancaria, solo se llena si el tipo de la nota es "Nota a Cuenta Bancaria"', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryNote', @level2type = N'COLUMN', @level2name = N'EntityBankAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la caja a la cual se va hacer la nota, esta se llena solo si el tipo de nota es "Nota de Caja"', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryNote', @level2type = N'COLUMN', @level2name = N'CashRegisterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad operativa', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryNote', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de la nota   1 - Nota a Cuenta Bancaria  2 - Nota a Caja  3 - Anulación de Comprobante de Egreso  4 - Anulación de Recibo de caja  5 - Anulación de Consignacion  6- Reversión cruce CxC vs CxP  7- Devolucion de Recibo de Caja', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryNote', @level2type = N'COLUMN', @level2name = N'NoteType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la nota', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryNote', @level2type = N'COLUMN', @level2name = N'NoteDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la nota', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryNote', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla de notas', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryNote', @level2type = N'COLUMN', @level2name = N'Id';

