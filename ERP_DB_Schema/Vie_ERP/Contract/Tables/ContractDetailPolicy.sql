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


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailPolicy', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailPolicy', @level2type = N'COLUMN', @level2name = N'Observation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje de cobertura', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailPolicy', @level2type = N'COLUMN', @level2name = N'CoveragePercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Monto asegurado', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailPolicy', @level2type = N'COLUMN', @level2name = N'AmountInsured';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de emisión', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailPolicy', @level2type = N'COLUMN', @level2name = N'EmissionDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'No. de poliza', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailPolicy', @level2type = N'COLUMN', @level2name = N'PolicyNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la aseguradora', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailPolicy', @level2type = N'COLUMN', @level2name = N'FixedAssetInsuranceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la poliza', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailPolicy', @level2type = N'COLUMN', @level2name = N'FixedAssetPolicyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle del contato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailPolicy', @level2type = N'COLUMN', @level2name = N'ContractDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailPolicy', @level2type = N'COLUMN', @level2name = N'Id';

