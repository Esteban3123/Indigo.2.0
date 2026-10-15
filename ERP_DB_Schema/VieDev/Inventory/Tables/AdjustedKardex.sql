CREATE TABLE [Inventory].[AdjustedKardex] (
    [Id]               INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [KardexId]         INT             NOT NULL,
    [QuantityPrevious] INT             NOT NULL,
    [Quantity]         INT             NOT NULL,
    [ValuePrevious]    DECIMAL (18, 2) NOT NULL,
    [Value]            DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_AdjustedKardex] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AdjustedKardex_Kardex] FOREIGN KEY ([KardexId]) REFERENCES [Inventory].[Kardex] ([Id])
);


GO
ALTER TABLE [Inventory].[AdjustedKardex] NOCHECK CONSTRAINT [FK_AdjustedKardex_Kardex];

