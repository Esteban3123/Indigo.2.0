CREATE TABLE [Common].[Seller] (
    [Id]             INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]           VARCHAR (20) NOT NULL,
    [ThirdPartyId]   INT          NOT NULL,
    [Classification] TINYINT      NOT NULL,
    [State]          BIT          NOT NULL,
    CONSTRAINT [PK_Seller] PRIMARY KEY CLUSTERED ([Id] ASC)
);

