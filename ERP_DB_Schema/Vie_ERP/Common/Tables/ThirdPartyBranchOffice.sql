CREATE TABLE [Common].[ThirdPartyBranchOffice] (
    [Id]             INT        IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ThirdPartyId]   INT        NOT NULL,
    [BranchOfficeId] INT        NOT NULL,
    [TimeStamp]      ROWVERSION NOT NULL,
    CONSTRAINT [PK_ThirdPartyBranchOffice] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ThirdPartyBranchOffice_BranchOffice] FOREIGN KEY ([BranchOfficeId]) REFERENCES [Payroll].[BranchOffice] ([Id]),
    CONSTRAINT [FK_ThirdPartyBranchOffice_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Common].[ThirdPartyBranchOffice] NOCHECK CONSTRAINT [FK_ThirdPartyBranchOffice_BranchOffice];


GO
ALTER TABLE [Common].[ThirdPartyBranchOffice] NOCHECK CONSTRAINT [FK_ThirdPartyBranchOffice_ThirdParty];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyBranchOffice', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la sucursal', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyBranchOffice', @level2type = N'COLUMN', @level2name = N'BranchOfficeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tercero', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyBranchOffice', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyBranchOffice', @level2type = N'COLUMN', @level2name = N'Id';

