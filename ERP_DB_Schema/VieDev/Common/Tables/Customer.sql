CREATE TABLE [Common].[Customer] (
    [Id]                      INT                                                                         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Nit]                     VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Nit_Ofuscado", 0)')       NOT NULL,
    [Name]                    VARCHAR (100) MASKED WITH (FUNCTION = 'partial(0, "Customer_Ofuscado", 0)') NOT NULL,
    [EPSCode]                 VARCHAR (20)                                                                NULL,
    [ThirdPartyId]            INT                                                                         CONSTRAINT [DF_Customer_ThirdPartyId] DEFAULT ((4)) NOT NULL,
    [MainAccountReceivableId] INT                                                                         NOT NULL,
    [Term]                    INT                                                                         NOT NULL,
    [State]                   BIT                                                                         NOT NULL,
    [CreationUser]            VARCHAR (20)                                                                NOT NULL,
    [CreationDate]            DATETIME                                                                    NOT NULL,
    [ModificationUser]        VARCHAR (20)                                                                NULL,
    [ModificationDate]        DATETIME                                                                    NULL,
    [TimeStamp]               ROWVERSION                                                                  NOT NULL,
    CONSTRAINT [PK_Customer__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Customer_MainAccounts] FOREIGN KEY ([MainAccountReceivableId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_Customer_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [UQ_Customer__Nit] UNIQUE NONCLUSTERED ([Nit] ASC)
);


GO
ALTER TABLE [Common].[Customer] NOCHECK CONSTRAINT [FK_Customer_MainAccounts];


GO
ALTER TABLE [Common].[Customer] NOCHECK CONSTRAINT [FK_Customer_ThirdParty];


GO
CREATE NONCLUSTERED INDEX [IX_Customer_ThirdPartyId]
    ON [Common].[Customer]([ThirdPartyId] ASC);

