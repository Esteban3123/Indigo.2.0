CREATE TABLE [GeneralLedger].[JournalVouchers] (
    [Id]                   INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AccountingMovementId] INT           NOT NULL,
    [Consecutive]          BIGINT        NOT NULL,
    [LegalBookId]          INT           CONSTRAINT [DF_JournalVouchers_LegalBookId] DEFAULT ((1)) NOT NULL,
    [IdJournalVoucher]     INT           NOT NULL,
    [VoucherDate]          DATETIME      NOT NULL,
    [Imported]             BIT           CONSTRAINT [DF_JournalVouchers_Imported] DEFAULT ((0)) NOT NULL,
    [Status]               TINYINT       NOT NULL,
    [Detail]               VARCHAR (MAX) CONSTRAINT [DF_Accounting_Detail] DEFAULT ('-') NULL,
    [EntityCode]           VARCHAR (20)  NULL,
    [EntityId]             INT           NULL,
    [EntityName]           VARCHAR (250) NULL,
    [IsClosedYear]         TINYINT       CONSTRAINT [DF_Accounting_IsClosedYear] DEFAULT ((0)) NOT NULL,
    [CreationUser]         VARCHAR (20)  NOT NULL,
    [CreationDate]         DATETIME      NOT NULL,
    [ModificationUser]     VARCHAR (20)  NULL,
    [ModificationDate]     DATETIME      NULL,
    [ConfirmationUser]     VARCHAR (20)  NULL,
    [ConfirmationDate]     DATETIME      NULL,
    [AnnulmentUser]        VARCHAR (20)  NULL,
    [AnnulmentDate]        DATETIME      NULL,
    [TimeStamp]            ROWVERSION    NOT NULL,
    [YearMovement]         INT           NOT NULL,
    CONSTRAINT [PK_JournalVouchers__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Accounting_DocumentType] FOREIGN KEY ([IdJournalVoucher]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_JournalVouchers_AccountingMovement] FOREIGN KEY ([AccountingMovementId]) REFERENCES [GeneralLedger].[AccountingMovement] ([Id]),
    CONSTRAINT [FK_JournalVouchers_LegalBook] FOREIGN KEY ([LegalBookId]) REFERENCES [GeneralLedger].[LegalBook] ([Id])
);


GO
ALTER TABLE [GeneralLedger].[JournalVouchers] NOCHECK CONSTRAINT [FK_Accounting_DocumentType];


GO
ALTER TABLE [GeneralLedger].[JournalVouchers] NOCHECK CONSTRAINT [FK_JournalVouchers_AccountingMovement];


GO
ALTER TABLE [GeneralLedger].[JournalVouchers] NOCHECK CONSTRAINT [FK_JournalVouchers_LegalBook];


GO
CREATE NONCLUSTERED INDEX [IX_JournalVouchers_Consecutive_IdJournalVoucher_LegalBookId_YearMovement]
    ON [GeneralLedger].[JournalVouchers]([Consecutive] ASC, [IdJournalVoucher] ASC, [LegalBookId] ASC, [YearMovement] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_JournalVouchers_StatusAndIsClosedYear]
    ON [GeneralLedger].[JournalVouchers]([Status] ASC, [IsClosedYear] ASC)
    INCLUDE([EntityId], [EntityName], [VoucherDate]);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_JournalVouchers_AccountingMovementId_LegalBookId]
    ON [GeneralLedger].[JournalVouchers]([AccountingMovementId] ASC, [LegalBookId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_JournalVouchers__LegalBookId__Status__VoucherDate__INC__Consecutive__EntityCode__EntityName__Id__IdJournalVoucher]
    ON [GeneralLedger].[JournalVouchers]([LegalBookId] ASC, [Status] ASC, [VoucherDate] ASC)
    INCLUDE([Consecutive], [EntityCode], [EntityName], [Id], [IdJournalVoucher]);


GO
CREATE NONCLUSTERED INDEX [IX_JournalVouchers__EntityCode__EntityName]
    ON [GeneralLedger].[JournalVouchers]([EntityCode] ASC, [EntityName] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_JournalVouchers__AccountingMovementId]
    ON [GeneralLedger].[JournalVouchers]([AccountingMovementId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_JournalVouchers__LegalBookId__IdJournalVoucher__Status__VoucherDate__INC__Consecutive__EntityCode__EntityName__Id]
    ON [GeneralLedger].[JournalVouchers]([LegalBookId] ASC, [IdJournalVoucher] ASC, [Status] ASC, [VoucherDate] ASC)
    INCLUDE([Consecutive], [EntityCode], [EntityName], [Id]);


GO
CREATE NONCLUSTERED INDEX [IDX_JournalVouchers_Consecutive_LegalBookId_IdJournalVoucher_YearMovement]
    ON [GeneralLedger].[JournalVouchers]([Consecutive] ASC, [LegalBookId] ASC, [IdJournalVoucher] ASC, [YearMovement] ASC);


GO
-- Índice optimizado para consultas del SP_ReportAuxiliar con filtros combinados LegalBookId + IsClosedYear + VoucherDate
CREATE NONCLUSTERED INDEX [IX_JournalVouchers__LegalBookId__IsClosedYear__VoucherDate]
    ON [GeneralLedger].[JournalVouchers]([LegalBookId] ASC, [IsClosedYear] ASC, [VoucherDate] ASC)
    INCLUDE ([Id], [Consecutive], [Status], [IdJournalVoucher], [EntityCode], [EntityName], [Detail]);


GO
-- =============================================
-- Author:		Miguel Angel Fonseca
-- Create date: 2020-02-12
-- Description:	Se valida que no se contabilice en libros no validos
-- =============================================
CREATE TRIGGER [GeneralLedger].[tgg_JournalVouchers_ValidateLegalBook]
   ON  [GeneralLedger].[JournalVouchers]
   AFTER INSERT, UPDATE
AS 
BEGIN
	SET NOCOUNT ON;

	IF EXISTS
	(
		SELECT 1
		FROM INSERTED jv
		JOIN GeneralLedger.LegalBook lb ON jv.LegalBookId = lb.Id AND lb.TypeBook NOT IN (1,2)
	)
	BEGIN
		THROW 51000, 'Error generado por control desde trigger. Solo se puede contabilizar en un libro COLGAAP o NIIF', 1
	END
END
GO
-- =============================================
-- Author:		Miguel Angel Fonseca
-- Create date: 2020-02-12
-- Description:	Se valida que no se contabilice en libros no validos
-- =============================================
CREATE TRIGGER [GeneralLedger].[tgg_JournalVouchers_Unique]
   ON  [GeneralLedger].[JournalVouchers]
   AFTER INSERT, UPDATE
AS 
BEGIN
	SET NOCOUNT ON;

	IF EXISTS
	(
		SELECT 1
		FROM INSERTED i
		JOIN GeneralLedger.JournalVouchers jv 
			ON i.IdJournalVoucher = jv.IdJournalVoucher
				AND i.LegalBookId = jv.LegalBookId
				AND i.Status = jv.Status
				AND i.EntityName = jv.EntityName
				AND i.EntityCode = jv.EntityCode
				AND i.EntityId = jv.EntityId
				AND i.Detail = jv.Detail
		WHERE i.EntityName NOT IN ('JournalVouchers', 'PayrollLiquidation', 'GlosaObjectionsReceptionD', 'FixedAssetDepreciation', 'ConsignmentCostList')
			AND i.Status = 2
			AND i.Id <> jv.Id
	)
	BEGIN
		THROW 51000, 'Error generado por control desde trigger. Ya existe un documento contabilizado con la misma información', 1
	END
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Movimiento del año', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchers', @level2type = N'COLUMN', @level2name = N'YearMovement';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchers', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Anulación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchers', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Anulación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchers', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Confirmación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchers', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Confirmación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchers', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchers', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchers', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchers', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchers', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor que indica si el comprobante fue generado por el proceso de cierre de año  0 - No Aplica (Comprobante normal)  1 - Comprobante de saldo Inicial (Inserta en el mes 14 de los saldos)  2 - Comrpobante de cierre (Inserta en el mes 13 de los saldos)', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchers', @level2type = N'COLUMN', @level2name = N'IsClosedYear';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la entidad quien genera el documento', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchers', @level2type = N'COLUMN', @level2name = N'EntityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la entidad quien genera el documento', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchers', @level2type = N'COLUMN', @level2name = N'EntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la entidad quien genera el documento', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchers', @level2type = N'COLUMN', @level2name = N'EntityCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Detalle General', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchers', @level2type = N'COLUMN', @level2name = N'Detail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado 1-Registrado 2-Confirmado 3-Anulado', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchers', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el comprobante contable fue realizado por importacion', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchers', @level2type = N'COLUMN', @level2name = N'Imported';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de documento', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchers', @level2type = N'COLUMN', @level2name = N'VoucherDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id tipo de comprobante', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchers', @level2type = N'COLUMN', @level2name = N'IdJournalVoucher';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el libro contable al cual pertenece el comprobante', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchers', @level2type = N'COLUMN', @level2name = N'LegalBookId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número del consecutivo del comprobante', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchers', @level2type = N'COLUMN', @level2name = N'Consecutive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del movimiento contable', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchers', @level2type = N'COLUMN', @level2name = N'AccountingMovementId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchers', @level2type = N'COLUMN', @level2name = N'Id';

