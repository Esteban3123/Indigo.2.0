CREATE TABLE [Taxes].[TaxesPropertyOwner] (
    [Id]              INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TaxesPropertyId] INT            NOT NULL,
    [ThirdPartyId]    INT            NOT NULL,
    [OrderOwner]      INT            NOT NULL,
    [Percentage]      NUMERIC (5, 2) NOT NULL,
    CONSTRAINT [PK_TaxesPropertyOwner] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TaxesPropertyOwner_TaxesProperty] FOREIGN KEY ([TaxesPropertyId]) REFERENCES [Taxes].[TaxesProperty] ([Id]),
    CONSTRAINT [FK_TaxesPropertyOwner_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [UQ_TaxesPropertyOwner__TaxesPropertyId__ThirdPartyId] UNIQUE NONCLUSTERED ([TaxesPropertyId] ASC, [ThirdPartyId] ASC)
);


GO
ALTER TABLE [Taxes].[TaxesPropertyOwner] NOCHECK CONSTRAINT [FK_TaxesPropertyOwner_TaxesProperty];


GO
ALTER TABLE [Taxes].[TaxesPropertyOwner] NOCHECK CONSTRAINT [FK_TaxesPropertyOwner_ThirdParty];

