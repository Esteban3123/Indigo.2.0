CREATE TABLE [Portfolio].[HardCollectionDetail] (
    [Id]                  INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [HardCollectionId]    INT             NOT NULL,
    [AccountReceivableId] INT             NOT NULL,
    [Balance]             NUMERIC (18, 2) NOT NULL,
    CONSTRAINT [PK_HardCollectionDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_HardCollectionDetail_AccountReceivable] FOREIGN KEY ([AccountReceivableId]) REFERENCES [Portfolio].[AccountReceivable] ([Id]),
    CONSTRAINT [FK_HardCollectionDetail_HardCollection] FOREIGN KEY ([HardCollectionId]) REFERENCES [Portfolio].[HardCollection] ([Id])
);


GO
ALTER TABLE [Portfolio].[HardCollectionDetail] NOCHECK CONSTRAINT [FK_HardCollectionDetail_AccountReceivable];


GO
ALTER TABLE [Portfolio].[HardCollectionDetail] NOCHECK CONSTRAINT [FK_HardCollectionDetail_HardCollection];

