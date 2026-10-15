CREATE TABLE [Treasury].[CheckCashingControlDetail] (
    [Id]                    INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdCheckCashingControl] INT     NOT NULL,
    [IdVoucherTransaction]  INT     NOT NULL,
    [IdCheckBook]           INT     NOT NULL,
    [CheckNumber]           BIGINT  NOT NULL,
    [PreviousCheckStatus]   TINYINT NOT NULL,
    [CurrentCheckStatus]    TINYINT NOT NULL,
    CONSTRAINT [PK_CheckCashingControlDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CheckCashingControlDetail_Checkbooks] FOREIGN KEY ([IdCheckBook]) REFERENCES [Treasury].[Checkbooks] ([Id]),
    CONSTRAINT [FK_CheckCashingControlDetail_CheckCashingControl] FOREIGN KEY ([IdCheckCashingControl]) REFERENCES [Treasury].[CheckCashingControl] ([Id]),
    CONSTRAINT [FK_CheckCashingControlDetail_VoucherTransaction] FOREIGN KEY ([IdVoucherTransaction]) REFERENCES [Treasury].[VoucherTransaction] ([Id])
);


GO
ALTER TABLE [Treasury].[CheckCashingControlDetail] NOCHECK CONSTRAINT [FK_CheckCashingControlDetail_Checkbooks];


GO
ALTER TABLE [Treasury].[CheckCashingControlDetail] NOCHECK CONSTRAINT [FK_CheckCashingControlDetail_CheckCashingControl];


GO
ALTER TABLE [Treasury].[CheckCashingControlDetail] NOCHECK CONSTRAINT [FK_CheckCashingControlDetail_VoucherTransaction];


GO
CREATE NONCLUSTERED INDEX [IX_CheckCashingControlDetail_IdCheckCashingControl]
    ON [Treasury].[CheckCashingControlDetail]([IdCheckCashingControl] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de cheque actual', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashingControlDetail', @level2type = N'COLUMN', @level2name = N'CurrentCheckStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de cheque anterior', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashingControlDetail', @level2type = N'COLUMN', @level2name = N'PreviousCheckStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de cheque', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashingControlDetail', @level2type = N'COLUMN', @level2name = N'CheckNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de chequera', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashingControlDetail', @level2type = N'COLUMN', @level2name = N'IdCheckBook';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de comprobante de egreso de administración de efectivo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashingControlDetail', @level2type = N'COLUMN', @level2name = N'IdVoucherTransaction';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de control de cheques', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashingControlDetail', @level2type = N'COLUMN', @level2name = N'IdCheckCashingControl';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashingControlDetail', @level2type = N'COLUMN', @level2name = N'Id';

