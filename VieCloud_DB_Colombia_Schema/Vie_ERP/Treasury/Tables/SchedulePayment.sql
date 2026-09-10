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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de pago efectivo, momento en que se ejecuta o registra la transacción monetaria (tipo DATETIME, default 2022-01-01)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'PaymentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Pago', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'PaymentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'PaymentDate';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal automática del sistema, captura el instante exacto de creación, modificación o evento crítico del registro (tipo TIMESTAMP, no editable)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'TimeStamp';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que se realizó el pago total o íntegro de la obligación programada (tipo DATETIME, auditoría financiera)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'FullPaymentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en que se realizo el pago total', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'FullPaymentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'FullPaymentDate';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del usuario que ejecutó y confirmó el pago total o completo de la programación (tipo VARCHAR, auditoría)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'FullPaymentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que realizo el pago Total', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'FullPaymentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'FullPaymentUser';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que se realizó un pago parcial o abono de la obligación programada (tipo DATETIME, auditoría de desembolsos)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'PartialPaymentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en que se realizo el pago parcial', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'PartialPaymentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'PartialPaymentDate';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del usuario que ejecutó el pago parcial o abono de la programación (tipo VARCHAR, auditoría)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'PartialPaymentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que realizo el pago parcial', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'PartialPaymentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'PartialPaymentUser';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que se anuló, canceló o revocó la programación de pago (tipo DATETIME, auditoría legal)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Anulación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del usuario que ejecutó la anulación o cancelación de la programación (tipo VARCHAR, auditoría)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Anulación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que se confirmó o validó la programación de pago para ejecución (tipo DATETIME, control de cambios)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Confirmación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del usuario que confirmó o validó la programación para procesamiento (tipo VARCHAR, aprobación)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Confirmación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que se modificaron o actualizaron datos de la programación (tipo DATETIME, control de cambios)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'ModificationDate';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del usuario que realizó cambios o actualizaciones en la programación (tipo VARCHAR, auditoría)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'ModificationUser';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación inicial del registro de programación de pago (tipo DATETIME, auditoría)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'CreationDate';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del usuario que creó originalmente el registro de programación (tipo VARCHAR, auditoría)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'CreationUser';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado actual de la programación: 1=Registrado, 2=Confirmado, 3=Anulado, 4=Pagado Parcial, 5=Pagado Total (tipo TINYINT, control de flujo)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la programacion de pagos  1 - Registrado  2 - Confirmado  3 - Anulado  4 - Pagado Parcial  5 - Pagado Total', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'Status';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad operativa, centro de atención o sucursal responsable del pago (tipo INT, FK a Common.OperatingUnit, segmentación)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'OperativeUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad operativa ', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'OperativeUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'OperativeUnitId';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador binario: aplica retención o impuesto por mil sobre el monto pagado (tipo BIT, cálculo tributario, default 0=No)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'TaxByMil';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Aplica impuesto por mil', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'TaxByMil';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'TaxByMil';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número o referencia de nota débito, comprobante o documento asociado al pago (tipo VARCHAR 50, trazabilidad)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'NumberNote';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de nota', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'NumberNote';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'NumberNote';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Forma de pago: 1=Cheque, 2=Nota Débito (tipo TINYINT, método de desembolso)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'PaymentMethod';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Método de pago. Cheque = 1, Nota débito = 2', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'PaymentMethod';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'PaymentMethod';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del centro de costo o unidad contable que carga el gasto (tipo INT, FK a Payroll.CostCenter, contabilidad)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del centro de costo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'CostCenterId';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta bancaria de la entidad proveedora o destino (tipo INT, FK a Treasury.EntityBankAccounts, tesorería)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'EntityBankAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta bancaria de la entidad', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'EntityBankAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'EntityBankAccountId';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha programada, planificada o estimada para ejecutar el pago (tipo DATE, planificación financiera)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'ScheduledDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha programada de pago', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'ScheduledDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'ScheduledDate';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código secuencial único o número correlativo asignado a la programación (tipo VARCHAR 20, identificación legible)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código secuencial', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'Code';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único, clave primaria del registro de programación de pago (tipo INT IDENTITY, auditoría)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'Id';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Pagos programados o agendados en tesorería. Registra cada programación de pago a proveedores o terceros, incluyendo la fecha pactada, el método de pago, la cuenta bancaria destino, el centro de costos y el ciclo de vida completo del pago (creación, confirmación, pago parcial, pago total y anulación).', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment';
