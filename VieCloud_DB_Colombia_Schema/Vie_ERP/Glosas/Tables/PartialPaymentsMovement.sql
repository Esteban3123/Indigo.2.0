CREATE TABLE [Glosas].[PartialPaymentsMovement] (
    [Id]                   INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PartialPaymentsDId]   INT          NOT NULL,
    [GlosaMovementGlosaId] INT          NOT NULL,
    [ValueEAPB]            MONEY        NULL,
    [CreationUser]         VARCHAR (20) NOT NULL,
    [CreationDate]         DATETIME     NOT NULL,
    [TimeStamp]            ROWVERSION   NOT NULL,
    CONSTRAINT [PK_Value] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ConciliationPartialPayments_GlosaMovementGlosa] FOREIGN KEY ([GlosaMovementGlosaId]) REFERENCES [Glosas].[GlosaMovementGlosa] ([Id]),
    CONSTRAINT [FK_PartialPaymentsMovement_PartialPaymentsD] FOREIGN KEY ([PartialPaymentsDId]) REFERENCES [Glosas].[PartialPaymentsD] ([Id])
);


GO
ALTER TABLE [Glosas].[PartialPaymentsMovement] NOCHECK CONSTRAINT [FK_ConciliationPartialPayments_GlosaMovementGlosa];


GO
ALTER TABLE [Glosas].[PartialPaymentsMovement] NOCHECK CONSTRAINT [FK_PartialPaymentsMovement_PartialPaymentsD];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal (TIMESTAMP SQL) del evento de movimiento de pago parcial. Registra automáticamente el instante exacto de creación, modificación o procesamiento del registro en la glosa.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsMovement', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsMovement', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsMovement', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del movimiento de pago parcial (DATETIME). Indica cuándo se registró el evento de conciliación o aplicación del pago en la glosa.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsMovement', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsMovement', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsMovement', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o identificador del usuario (VARCHAR 20) que creó o registró el movimiento de pago parcial. Rastrea quién procesó la conciliación de la glosa.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsMovement', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Usuario de creacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsMovement', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsMovement', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario (MONEY) del pago parcial aplicado por la EAPB (aseguradora). Monto reconocido o pagado parcialmente en la factura disputada.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsMovement', @level2type = N'COLUMN', @level2name = N'ValueEAPB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del pago parcial', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsMovement', @level2type = N'COLUMN', @level2name = N'ValueEAPB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsMovement', @level2type = N'COLUMN', @level2name = N'ValueEAPB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) del movimiento de glosa asociado. Vincula este pago parcial al registro específico de movimiento en la tabla GlosaMovementGlosa.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsMovement', @level2type = N'COLUMN', @level2name = N'GlosaMovementGlosaId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Relacion del Movimiento de Glosa al que se le aplicará el pago parcial', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsMovement', @level2type = N'COLUMN', @level2name = N'GlosaMovementGlosaId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsMovement', @level2type = N'COLUMN', @level2name = N'GlosaMovementGlosaId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) del detalle de pago parcial. Referencia la factura o detalle de reclamación (PartialPaymentsD) a la cual se aplica el pago reconocido.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsMovement', @level2type = N'COLUMN', @level2name = N'PartialPaymentsDId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Relacion de factura del detalle del pago parcial', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsMovement', @level2type = N'COLUMN', @level2name = N'PartialPaymentsDId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsMovement', @level2type = N'COLUMN', @level2name = N'PartialPaymentsDId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del movimiento de pago parcial. Consecutivo que numera cada transacción de aplicación de pago parcial en la conciliación de glosas.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsMovement', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de Movimientos de Pago parciales', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsMovement', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsMovement', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra los movimientos de pagos parciales asociados a glosas, vinculando cada abono o pago parcial con su glosa correspondiente y el valor reconocido por la EAPB (aseguradora). Permite hacer seguimiento del historial de pagos parciales dentro del proceso de gestión de glosas.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsMovement';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsMovement';
