CREATE TABLE [Treasury].[BankConciliationConcepts] (
    [Id]                   INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                 VARCHAR (20)  NOT NULL,
    [Name]                 VARCHAR (100) NOT NULL,
    [Status]               BIT           NOT NULL,
    [CreationUser]         VARCHAR (20)  NOT NULL,
    [CreationDate]         DATETIME      NOT NULL,
    [ModificationUser]     VARCHAR (20)  NULL,
    [ModificationDate]     DATETIME      NULL,
    [TimeStamp]            ROWVERSION    NOT NULL,
    [VoucherTransactionId] INT           NULL,
    [ConsignmentId]        INT           NULL,
    [CashReceiptsId]       INT           NULL,
    [NoteConceptsId]       INT           NULL,
    [DocumentType]         INT           NULL,
    [VoucherType]          INT           NULL,
    CONSTRAINT [PK_BankConciliationConcepts__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Treasury_CashReceipts] FOREIGN KEY ([CashReceiptsId]) REFERENCES [Treasury].[CashReceipts] ([Id]),
    CONSTRAINT [FK_Treasury_Consignment] FOREIGN KEY ([ConsignmentId]) REFERENCES [Treasury].[Consignment] ([Id]),
    CONSTRAINT [FK_Treasury_NoteConcepts] FOREIGN KEY ([NoteConceptsId]) REFERENCES [Treasury].[NoteConcepts] ([Id]),
    CONSTRAINT [FK_Treasury_VoucherTransaction] FOREIGN KEY ([VoucherTransactionId]) REFERENCES [Treasury].[VoucherTransaction] ([Id])
);


GO
ALTER TABLE [Treasury].[BankConciliationConcepts] NOCHECK CONSTRAINT [FK_Treasury_CashReceipts];


GO
ALTER TABLE [Treasury].[BankConciliationConcepts] NOCHECK CONSTRAINT [FK_Treasury_Consignment];


GO
ALTER TABLE [Treasury].[BankConciliationConcepts] NOCHECK CONSTRAINT [FK_Treasury_NoteConcepts];


GO
ALTER TABLE [Treasury].[BankConciliationConcepts] NOCHECK CONSTRAINT [FK_Treasury_VoucherTransaction];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1: Nota 2: Cheque', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankConciliationConcepts', @level2type = N'COLUMN', @level2name = N'VoucherType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Documento:   1: Comprobante de Egreso   2: Consignaciones   3: Recibos de Caja   4: Notas', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankConciliationConcepts', @level2type = N'COLUMN', @level2name = N'DocumentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id para la relación de Notas', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankConciliationConcepts', @level2type = N'COLUMN', @level2name = N'NoteConceptsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id para la relación de Recibos de Caja', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankConciliationConcepts', @level2type = N'COLUMN', @level2name = N'CashReceiptsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id para la relación de Consignaciones', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankConciliationConcepts', @level2type = N'COLUMN', @level2name = N'ConsignmentId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id para la relación de Comprobante de Egreso', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankConciliationConcepts', @level2type = N'COLUMN', @level2name = N'VoucherTransactionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankConciliationConcepts', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankConciliationConcepts', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankConciliationConcepts', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankConciliationConcepts', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankConciliationConcepts', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado  1 - Activo 0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankConciliationConcepts', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del Concepto de Conciliación Bancaria', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankConciliationConcepts', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Concepto de Conciliación Bancaria', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankConciliationConcepts', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Concepto de Conciliación Bancaria', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankConciliationConcepts', @level2type = N'COLUMN', @level2name = N'Id';

