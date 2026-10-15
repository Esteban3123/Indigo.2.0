CREATE TABLE [Contract].[GroupersCups] (
    [Id]                              INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [GrouperId]                       INT NOT NULL,
    [CUPSEntityId]                    INT NOT NULL,
    [CUPSEntityContractDescriptionId] INT NULL,
    [ContractDescriptionId]           INT NULL,
    CONSTRAINT [PK_GroupersCups] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GroupersCups_ContractDescriptions] FOREIGN KEY ([ContractDescriptionId]) REFERENCES [Contract].[ContractDescriptions] ([Id]),
    CONSTRAINT [FK_GroupersCups_CUPSEntity] FOREIGN KEY ([CUPSEntityId]) REFERENCES [Contract].[CUPSEntity] ([Id]),
    CONSTRAINT [FK_GroupersCups_CUPSEntityContractDescriptions] FOREIGN KEY ([CUPSEntityContractDescriptionId]) REFERENCES [Contract].[CUPSEntityContractDescriptions] ([Id]),
    CONSTRAINT [FK_GroupersCups_Groupers] FOREIGN KEY ([GrouperId]) REFERENCES [Contract].[Groupers] ([Id])
);


GO
ALTER TABLE [Contract].[GroupersCups] NOCHECK CONSTRAINT [FK_GroupersCups_ContractDescriptions];


GO
ALTER TABLE [Contract].[GroupersCups] NOCHECK CONSTRAINT [FK_GroupersCups_CUPSEntity];


GO
ALTER TABLE [Contract].[GroupersCups] NOCHECK CONSTRAINT [FK_GroupersCups_CUPSEntityContractDescriptions];


GO
ALTER TABLE [Contract].[GroupersCups] NOCHECK CONSTRAINT [FK_GroupersCups_Groupers];

