CREATE TABLE [Contract].[ContractDetailPolicy] (
    [Id]                    INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ContractDetailId]      INT             NOT NULL,
    [FixedAssetPolicyId]    INT             NOT NULL,
    [FixedAssetInsuranceId] INT             NOT NULL,
    [PolicyNumber]          VARCHAR (50)    NOT NULL,
    [EmissionDate]          DATETIME        NOT NULL,
    [AmountInsured]         NUMERIC (18, 2) CONSTRAINT [DF_ContractDetailPolicy_AmountInsured] DEFAULT ((0)) NOT NULL,
    [CoveragePercentage]    NUMERIC (5, 2)  CONSTRAINT [DF_ContractDetailPolicy_CoveragePercentage] DEFAULT ((0)) NOT NULL,
    [Observation]           VARCHAR (500)   NULL,
    [Status]                BIT             NOT NULL,
    CONSTRAINT [PK_ContractDetailPolicy] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ContractDetailPolicy_ContractDetail] FOREIGN KEY ([ContractDetailId]) REFERENCES [Contract].[ContractDetail] ([Id]),
    CONSTRAINT [FK_ContractDetailPolicy_FixedAssetPolicy] FOREIGN KEY ([FixedAssetPolicyId]) REFERENCES [FixedAsset].[FixedAssetPolicy] ([Id])
);


GO
ALTER TABLE [Contract].[ContractDetailPolicy] NOCHECK CONSTRAINT [FK_ContractDetailPolicy_ContractDetail];


GO
ALTER TABLE [Contract].[ContractDetailPolicy] NOCHECK CONSTRAINT [FK_ContractDetailPolicy_FixedAssetPolicy];

