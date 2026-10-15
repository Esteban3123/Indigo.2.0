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


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la responsabilidad fiscal', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyFiscalResponsibility', @level2type = N'COLUMN', @level2name = N'FiscalResponsibilityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tercero', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyFiscalResponsibility', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyFiscalResponsibility', @level2type = N'COLUMN', @level2name = N'Id';

