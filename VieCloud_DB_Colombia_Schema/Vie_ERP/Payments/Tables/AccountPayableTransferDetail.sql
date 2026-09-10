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



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal (TIMESTAMP) del evento de creación, registro o modificación del detalle de traslado. Registro automático del instante exacto de cualquier cambio en el registro.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual (VARCHAR 500) del motivo o justificación del rechazo de la factura/cuenta por pagar. Texto libre que documenta por qué se rechazó el traslado.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'RejectionDescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del rechazo de la factura', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'RejectionDescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'RejectionDescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) del motivo de rechazo catalogado en [Payments].[AccountPayableRejectionReason]. Referencia a la razón predefinida del rechazo de la factura.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'RejectionReasonId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del motivo de rechazo de la factura', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'RejectionReasonId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'RejectionReasonId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que se rechazó el traslado de la factura/cuenta por pagar. Marca temporal del evento de rechazo.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'RejectionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en que se rechazo la factura', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'RejectionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'RejectionDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 20) que ejecutó y registró el rechazo del traslado de factura. Identificación del operario que rechazó.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'RejectionUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que rechazo la factura', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'RejectionUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'RejectionUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que se aceptó el traslado de la factura/cuenta por pagar. Marca temporal de la aprobación.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'AcceptanceDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de aceptacion de la factura', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'AcceptanceDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'AcceptanceDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 20) que ejecutó y registró la aceptación del traslado de factura. Identificación del operario que aprobó.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'AcceptanceUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que acepto la factura', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'AcceptanceUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'AcceptanceUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) del último cambio o actualización del detalle de traslado. Timestamp de la última modificación.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 20) que realizó la última modificación del registro. Identificación del operario que editó.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de creación inicial del detalle de traslado. Timestamp del registro original.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 20) que creó originalmente el registro del detalle de traslado. Identificación del operario creador.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado (TINYINT) del traslado de factura: 1=Pendiente Aceptación, 2=Aceptada, 3=Rechazada, 4=Anulada. El estado 4 indica traslado anulado.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el estado del traslado de la factura en especifico  1 - Pendiente por Aceptacion  2 - Aceptada  3 - Rechazada  4 - Anulada    El estado es 4 cuando se anula el traslado de factura.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) del reembolso asociado en [Treasury].[Refunds]. Referencia al reembolso vinculado al traslado.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'RefundId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del reembolso', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'RefundId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'RefundId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) de la factura/cuenta por pagar siendo trasladada en [Payments].[AccountPayable]. Referencia a la deuda trasladada.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'AccountPayableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta por pagar(Factura) que se esta trasladando', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'AccountPayableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'AccountPayableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) del traslado padre en [Payments].[AccountPayableTransfer]. Clave foránea que agrupa este detalle.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'AccountPayableTransferId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de transferencia de cuentas por pagar', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'AccountPayableTransferId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'AccountPayableTransferId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del detalle de traslado de factura. Clave primaria autoincrementable de la tabla.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de cada cuenta por pagar incluida en una transferencia de pago. Registra el estado de aceptación o rechazo de cada ítem transferido, junto con la trazabilidad de usuarios y fechas de cada acción.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransferDetail';
