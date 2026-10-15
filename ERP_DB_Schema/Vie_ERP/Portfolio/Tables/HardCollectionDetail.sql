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


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_HardCollectionDetail]
    ON [Portfolio].[HardCollectionDetail]([HardCollectionId] ASC, [AccountReceivableId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Saldo', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'HardCollectionDetail', @level2type = N'COLUMN', @level2name = N'Balance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'HardCollectionDetail', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id dificil recudo ', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'HardCollectionDetail', @level2type = N'COLUMN', @level2name = N'HardCollectionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'HardCollectionDetail', @level2type = N'COLUMN', @level2name = N'Id';

