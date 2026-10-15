CREATE TABLE [Inventory].[UpdateExpirationDate] (
    [Id]                INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [BatchSerialId]     INT           NOT NULL,
    [OldExpirationDate] DATE          NOT NULL,
    [NewExpirationDate] DATE          NOT NULL,
    [Observation]       VARCHAR (MAX) NOT NULL,
    [CreationUser]      VARCHAR (20)  CONSTRAINT [DF_UpdateExpirationDate_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]      DATETIME      CONSTRAINT [DF_UpdateExpirationDate_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    CONSTRAINT [PK_UpdateExpirationDate] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UpdateExpirationDate_BatchSerial] FOREIGN KEY ([BatchSerialId]) REFERENCES [Inventory].[BatchSerial] ([Id])
);


GO
ALTER TABLE [Inventory].[UpdateExpirationDate] NOCHECK CONSTRAINT [FK_UpdateExpirationDate_BatchSerial];

