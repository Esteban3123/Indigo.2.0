CREATE TABLE [Contract].[ProcedureCups] (
    [Id]                              INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ProceduresTemplateId]            INT NOT NULL,
    [CupsId]                          INT NOT NULL,
    [Contracted]                      BIT CONSTRAINT [DF_ProcedureCups_Contracted] DEFAULT ((1)) NOT NULL,
    [Quoted]                          BIT CONSTRAINT [DF_ProcedureCups_Quoted] DEFAULT ((0)) NOT NULL,
    [CUPSEntityContractDescriptionId] INT NULL,
    [ContractDescriptionId]           INT NULL,
    CONSTRAINT [PK_ProcedureCups__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProcedureCups_ContractDescriptions] FOREIGN KEY ([ContractDescriptionId]) REFERENCES [Contract].[ContractDescriptions] ([Id]),
    CONSTRAINT [FK_ProcedureCups_CupsEntity] FOREIGN KEY ([CupsId]) REFERENCES [Contract].[CUPSEntity] ([Id]),
    CONSTRAINT [FK_ProcedureCups_CUPSEntityContractDescriptions] FOREIGN KEY ([CUPSEntityContractDescriptionId]) REFERENCES [Contract].[CUPSEntityContractDescriptions] ([Id]),
    CONSTRAINT [FK_ProcedureCups_ProcedureTemplate] FOREIGN KEY ([ProceduresTemplateId]) REFERENCES [Contract].[ProcedureTemplate] ([Id])
);


GO
ALTER TABLE [Contract].[ProcedureCups] NOCHECK CONSTRAINT [FK_ProcedureCups_ContractDescriptions];


GO
ALTER TABLE [Contract].[ProcedureCups] NOCHECK CONSTRAINT [FK_ProcedureCups_CupsEntity];


GO
ALTER TABLE [Contract].[ProcedureCups] NOCHECK CONSTRAINT [FK_ProcedureCups_CUPSEntityContractDescriptions];


GO
ALTER TABLE [Contract].[ProcedureCups] NOCHECK CONSTRAINT [FK_ProcedureCups_ProcedureTemplate];

