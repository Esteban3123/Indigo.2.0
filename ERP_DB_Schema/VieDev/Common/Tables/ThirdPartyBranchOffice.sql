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

