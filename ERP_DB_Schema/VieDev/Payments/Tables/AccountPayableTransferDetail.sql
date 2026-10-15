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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el estado del traslado de la factura en especifico  1 - Pendiente por Aceptacion  2 - Aceptada  3 - Rechazada  4 - Anulada    El estado es 4 cuando se anula el traslado de factura.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'Status';

