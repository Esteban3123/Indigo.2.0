CREATE TABLE [AccountManagement].[FolioTransfer] (
    [Id]                     INT           IDENTITY (1, 1) NOT NULL,
    [PreviousUser]           VARCHAR (20)  NULL,
    [ReceivingUser]          VARCHAR (20)  NULL,
    [AdmissionNumber]        VARCHAR (50)  NOT NULL,
    [RevenueControlDetailId] INT           NOT NULL,
    [TransferStatus]         TINYINT       DEFAULT ((1)) NOT NULL,
    [CreationUser]           VARCHAR (20)  NOT NULL,
    [CreationDate]           DATETIME      NOT NULL,
    [ModificationUser]       VARCHAR (50)  NULL,
    [ModificationDate]       DATETIME      NULL,
    [RejectionReasonId]      INT           NULL,
    [RejectionObservation]   VARCHAR (500) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FolioTransfer_RejectionReason] FOREIGN KEY ([RejectionReasonId]) REFERENCES [AccountManagement].[RejectionReason] ([Id]),
    CONSTRAINT [FK_FolioTransfer_RevenueControlDetail] FOREIGN KEY ([RevenueControlDetailId]) REFERENCES [Billing].[RevenueControlDetail] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del traslado: 1 - Sin traslado, 2 - Pendiente de aceptación, 3 - Aceptada, 4 - Rechazada', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioTransfer', @level2type = N'COLUMN', @level2name = N'TransferStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del folio que se está trasladando', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioTransfer', @level2type = N'COLUMN', @level2name = N'RevenueControlDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso asociado al folio', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioTransfer', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario al que se transfiere el folio', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioTransfer', @level2type = N'COLUMN', @level2name = N'ReceivingUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que tenía el folio antes de la transferencia', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioTransfer', @level2type = N'COLUMN', @level2name = N'PreviousUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del traslado de folio', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioTransfer', @level2type = N'COLUMN', @level2name = N'Id';

