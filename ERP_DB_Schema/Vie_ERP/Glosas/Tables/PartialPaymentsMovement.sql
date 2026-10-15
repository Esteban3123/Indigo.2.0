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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsMovement', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsMovement', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Usuario de creacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsMovement', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del pago parcial', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsMovement', @level2type = N'COLUMN', @level2name = N'ValueEAPB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Relacion del Movimiento de Glosa al que se le aplicará el pago parcial', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsMovement', @level2type = N'COLUMN', @level2name = N'GlosaMovementGlosaId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Relacion de factura del detalle del pago parcial', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsMovement', @level2type = N'COLUMN', @level2name = N'PartialPaymentsDId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de Movimientos de Pago parciales', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'PartialPaymentsMovement', @level2type = N'COLUMN', @level2name = N'Id';

