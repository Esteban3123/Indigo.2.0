CREATE TABLE [Common].[ThirdPartyFiscalResponsibility] (
    [Id]                     INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ThirdPartyId]           INT NOT NULL,
    [FiscalResponsibilityId] INT NOT NULL,
    CONSTRAINT [PK_ThirdPartyFiscalResponsibility] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ThirdPartyFiscalResponsibility_FiscalResponsibility] FOREIGN KEY ([FiscalResponsibilityId]) REFERENCES [Common].[FiscalResponsibility] ([Id]),
    CONSTRAINT [FK_ThirdPartyFiscalResponsibility_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Common].[ThirdPartyFiscalResponsibility] NOCHECK CONSTRAINT [FK_ThirdPartyFiscalResponsibility_FiscalResponsibility];


GO
ALTER TABLE [Common].[ThirdPartyFiscalResponsibility] NOCHECK CONSTRAINT [FK_ThirdPartyFiscalResponsibility_ThirdParty];


GO
CREATE NONCLUSTERED INDEX [IX_ThirdPartyFiscalResponsibility_ThirdPartyId]
    ON [Common].[ThirdPartyFiscalResponsibility]([ThirdPartyId] ASC);

