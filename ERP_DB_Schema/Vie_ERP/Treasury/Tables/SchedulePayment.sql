CREATE TABLE [Treasury].[SchedulePayment] (
    [Id]                  INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                VARCHAR (20) NOT NULL,
    [ScheduledDate]       DATE         NOT NULL,
    [EntityBankAccountId] INT          NULL,
    [CostCenterId]        INT          NULL,
    [PaymentMethod]       TINYINT      NULL,
    [CheckId]             INT          NULL,
    [NumberNote]          VARCHAR (50) NULL,
    [TaxByMil]            BIT          CONSTRAINT [DF_SchedulePayment_TaxByMil] DEFAULT ((0)) NULL,
    [OperativeUnitId]     INT          NOT NULL,
    [Status]              TINYINT      NOT NULL,
    [CreationUser]        VARCHAR (20) NOT NULL,
    [CreationDate]        DATETIME     NOT NULL,
    [ModificationUser]    VARCHAR (20) NULL,
    [ModificationDate]    DATETIME     NULL,
    [ConfirmationUser]    VARCHAR (20) NULL,
    [ConfirmationDate]    DATETIME     NULL,
    [AnnulmentUser]       VARCHAR (20) NULL,
    [AnnulmentDate]       DATETIME     NULL,
    [PartialPaymentUser]  VARCHAR (20) NULL,
    [PartialPaymentDate]  DATETIME     NULL,
    [FullPaymentUser]     VARCHAR (20) NULL,
    [FullPaymentDate]     DATETIME     NULL,
    [TimeStamp]           ROWVERSION   NOT NULL,
    [PaymentDate]         DATETIME     CONSTRAINT [DF__ScheduleP__Payme__6A8139C4] DEFAULT ('2022-01-01') NOT NULL,
    CONSTRAINT [PK_SchedulePayment] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SchedulePayment_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_SchedulePayment_EntityBankAccounts] FOREIGN KEY ([EntityBankAccountId]) REFERENCES [Treasury].[EntityBankAccounts] ([Id]),
    CONSTRAINT [FK_SchedulePayment_OperatingUnit] FOREIGN KEY ([OperativeUnitId]) REFERENCES [Common].[OperatingUnit] ([Id])
);


GO
ALTER TABLE [Treasury].[SchedulePayment] NOCHECK CONSTRAINT [FK_SchedulePayment_CostCenter];


GO
ALTER TABLE [Treasury].[SchedulePayment] NOCHECK CONSTRAINT [FK_SchedulePayment_EntityBankAccounts];


GO
ALTER TABLE [Treasury].[SchedulePayment] NOCHECK CONSTRAINT [FK_SchedulePayment_OperatingUnit];


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	Impedir que se actualicen incorrectamente la programacion de pagos, para validacion
-- =============================================
CREATE TRIGGER [Treasury].[tgg_SchedulePaymentUpdate]
   ON  [Treasury].[SchedulePayment] 
   AFTER UPDATE
AS 
BEGIN
	SET NOCOUNT ON

	IF EXISTS
	(
		SELECT 1
		FROM DELETED d
		JOIN INSERTED i ON d.Id = i.Id
		WHERE d.Status > i.Status
	)
	BEGIN
		THROW 51000, 'Error generado por control de actualizacion en estado de la programacion de pagos', 1
	END
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Pago', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'PaymentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha en que se realizo el pago total', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'FullPaymentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que realizo el pago Total', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'FullPaymentUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha en que se realizo el pago parcial', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'PartialPaymentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que realizo el pago parcial', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'PartialPaymentUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Anulación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Anulación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Confirmación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Confirmación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la programacion de pagos  1 - Registrado  2 - Confirmado  3 - Anulado  4 - Pagado Parcial  5 - Pagado Total', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad operativa ', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'OperativeUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Aplica impuesto por mil', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'TaxByMil';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de nota', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'NumberNote';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id verificación ', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'CheckId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Método de pago. Cheque = 1, Nota débito = 2', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'PaymentMethod';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del centro de costo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta bancaria de la entidad', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'EntityBankAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha programada de pago', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'ScheduledDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código secuencial', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'Id';

