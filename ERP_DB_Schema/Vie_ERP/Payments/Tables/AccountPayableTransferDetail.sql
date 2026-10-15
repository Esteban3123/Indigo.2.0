CREATE TABLE [Payments].[AccountPayableTransferDetail] (
    [Id]                       INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AccountPayableTransferId] INT           NOT NULL,
    [AccountPayableId]         INT           NULL,
    [RefundId]                 INT           NULL,
    [Status]                   TINYINT       NOT NULL,
    [CreationUser]             VARCHAR (20)  NOT NULL,
    [CreationDate]             DATETIME      NOT NULL,
    [ModificationUser]         VARCHAR (20)  NULL,
    [ModificationDate]         DATETIME      NULL,
    [AcceptanceUser]           VARCHAR (20)  NULL,
    [AcceptanceDate]           DATETIME      NULL,
    [RejectionUser]            VARCHAR (20)  NULL,
    [RejectionDate]            DATETIME      NULL,
    [RejectionReasonId]        INT           NULL,
    [RejectionDescription]     VARCHAR (500) NULL,
    [TimeStamp]                ROWVERSION    NOT NULL,
    CONSTRAINT [PK_AccountPayableTransferDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccountPayableTransferDetail_AccountPayable] FOREIGN KEY ([AccountPayableId]) REFERENCES [Payments].[AccountPayable] ([Id]),
    CONSTRAINT [FK_AccountPayableTransferDetail_AccountPayableRejectionReason] FOREIGN KEY ([RejectionReasonId]) REFERENCES [Payments].[AccountPayableRejectionReason] ([Id]),
    CONSTRAINT [FK_AccountPayableTransferDetail_AccountPayableTransfer] FOREIGN KEY ([AccountPayableTransferId]) REFERENCES [Payments].[AccountPayableTransfer] ([Id]),
    CONSTRAINT [FK_AccountPayableTransferDetail_Refunds] FOREIGN KEY ([RefundId]) REFERENCES [Treasury].[Refunds] ([Id])
);


GO
ALTER TABLE [Payments].[AccountPayableTransferDetail] NOCHECK CONSTRAINT [FK_AccountPayableTransferDetail_AccountPayable];


GO
ALTER TABLE [Payments].[AccountPayableTransferDetail] NOCHECK CONSTRAINT [FK_AccountPayableTransferDetail_AccountPayableRejectionReason];


GO
ALTER TABLE [Payments].[AccountPayableTransferDetail] NOCHECK CONSTRAINT [FK_AccountPayableTransferDetail_AccountPayableTransfer];


GO
ALTER TABLE [Payments].[AccountPayableTransferDetail] NOCHECK CONSTRAINT [FK_AccountPayableTransferDetail_Refunds];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del rechazo de la factura', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'RejectionDescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del motivo de rechazo de la factura', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'RejectionReasonId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha en que se rechazo la factura', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'RejectionDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que rechazo la factura', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'RejectionUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de aceptacion de la factura', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'AcceptanceDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que acepto la factura', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'AcceptanceUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el estado del traslado de la factura en especifico  1 - Pendiente por Aceptacion  2 - Aceptada  3 - Rechazada  4 - Anulada    El estado es 4 cuando se anula el traslado de factura.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del reembolso', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'RefundId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta por pagar(Factura) que se esta trasladando', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'AccountPayableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de transferencia de cuentas por pagar', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'AccountPayableTransferId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'Id';

