CREATE TABLE [GeneralLedger].[AccountingMovementPending_OLD] (
    [Id]                   INT           IDENTITY (1, 1) NOT NULL,
    [AccountingMovementId] INT           NOT NULL,
    [JournalVoucherTypeId] INT           NOT NULL,
    [VoucherDate]          DATETIME      NOT NULL,
    [Detail]               VARCHAR (500) NOT NULL,
    [EntityCode]           VARCHAR (20)  NULL,
    [EntityId]             INT           NULL,
    [EntityName]           VARCHAR (250) NULL,
    [CreationUser]         VARCHAR (20)  NOT NULL,
    [CreationDate]         DATETIME      NOT NULL,
    [Failed]               BIT           CONSTRAINT [DF__Accountin__Faile__1191B09B] DEFAULT ((0)) NULL,
    [FailedMessage]        VARCHAR (MAX) NULL,
    CONSTRAINT [PK_AccountingMovementPending] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccountingMovementPending_AccountingMovement] FOREIGN KEY ([AccountingMovementId]) REFERENCES [GeneralLedger].[AccountingMovement] ([Id]),
    CONSTRAINT [FK_AccountingMovementPending_JournalVoucherTypes] FOREIGN KEY ([JournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id])
);




GO



GO



GO
-- =============================================
-- Author:		Miguel Angel Fonseca
-- Create date: 2020-02-12
-- Description:	Se valida que no se contabilice en libros no validos
-- =============================================
CREATE TRIGGER [GeneralLedger].[tgg_AccountingMovementPending_Unique]
   ON  [GeneralLedger].[AccountingMovementPending_OLD]
   AFTER INSERT, UPDATE
AS 
BEGIN
	SET NOCOUNT ON;

	IF EXISTS
	(
		SELECT 1
		FROM INSERTED i
		JOIN GeneralLedger.AccountingMovementPending_OLD jv 
			ON i.JournalVoucherTypeId = jv.JournalVoucherTypeId
				AND i.EntityName = jv.EntityName
				AND i.EntityCode = jv.EntityCode
				AND i.EntityId = jv.EntityId
				AND i.Detail = jv.Detail
		WHERE i.EntityName NOT IN ('JournalVouchers', 'PayrollLiquidation', 'GlosaObjectionsReceptionD', 'FixedAssetDepreciation', 'ConsignmentCostList')
		AND NOT (
        i.Detail LIKE '%Ingreso de Activo%' 
        OR i.Detail LIKE '%Salida de Activo%'
		OR i.Detail LIKE '%Cuenta por pagar generada desde Transacciones%'
			)
			AND i.Id <> jv.Id
	)
	BEGIN
		THROW 51000, 'Error generado por control desde trigger. Ya existe un documento contabilizado con la misma información', 1
	END
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guardan los mensajes de errores retornados en caso de que algo en la ejecucicion del proceso haya fallado, ya sea en el SP_SaveJournalVoucher_ByMovementId_Output ó de la azure function', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'AccountingMovementPending_OLD', @level2type = N'COLUMN', @level2name = N'FailedMessage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifica el estado del comprobante, si es 0 o null = Aun no se procesa, Si es 1 es porque algo del comporbante fallo, si no aparece es porque se proceso correctamente.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'AccountingMovementPending_OLD', @level2type = N'COLUMN', @level2name = N'Failed';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación del comprobante', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'AccountingMovementPending_OLD', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creo el comprobante', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'AccountingMovementPending_OLD', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifica de donde se creo el comprobante', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'AccountingMovementPending_OLD', @level2type = N'COLUMN', @level2name = N'EntityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la entidad', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'AccountingMovementPending_OLD', @level2type = N'COLUMN', @level2name = N'EntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la entidad', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'AccountingMovementPending_OLD', @level2type = N'COLUMN', @level2name = N'EntityCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle del comprobante contable ', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'AccountingMovementPending_OLD', @level2type = N'COLUMN', @level2name = N'Detail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del comprobante', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'AccountingMovementPending_OLD', @level2type = N'COLUMN', @level2name = N'VoucherDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del tipo de comprobante contable', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'AccountingMovementPending_OLD', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'id del movimeinto de la cuenta', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'AccountingMovementPending_OLD', @level2type = N'COLUMN', @level2name = N'AccountingMovementId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del movimiento de la cuenta pendiente', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'AccountingMovementPending_OLD', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla histórica (sufijo _OLD) que almacenaba comprobantes contables pendientes de procesar, vinculados a un movimiento contable y a un tipo de voucher del libro mayor. Registra el estado de procesamiento mediante el campo `Failed` (0/null = pendiente, 1 = fallido) y captura mensajes de error provenientes del SP `SaveJournalVoucher_ByMovementId_Output` o de una Azure Function. Un trigger AFTER INSERT/UPDATE impide duplicar comprobantes con igual tipo, entidad y detalle, exceptuando entidades como nómina, activos fijos y glosas.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'TABLE', @level1name=N'AccountingMovementPending_OLD';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'TABLE', @level1name=N'AccountingMovementPending_OLD';
GO
