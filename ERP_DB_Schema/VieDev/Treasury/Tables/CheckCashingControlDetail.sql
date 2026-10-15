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

