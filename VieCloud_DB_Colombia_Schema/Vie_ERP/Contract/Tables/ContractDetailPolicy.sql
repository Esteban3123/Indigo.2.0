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



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la póliza (activa/inactiva); BIT indicador de vigencia del registro de aseguramiento', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailPolicy', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailPolicy', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailPolicy', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones o notas adicionales sobre la póliza de seguros; campo de texto libre para aclaraciones', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailPolicy', @level2type = N'COLUMN', @level2name = N'Observation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailPolicy', @level2type = N'COLUMN', @level2name = N'Observation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailPolicy', @level2type = N'COLUMN', @level2name = N'Observation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de cobertura de la póliza (0-100); NUMERIC(5,2) que define la proporción asegurada del bien', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailPolicy', @level2type = N'COLUMN', @level2name = N'CoveragePercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje de cobertura', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailPolicy', @level2type = N'COLUMN', @level2name = N'CoveragePercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailPolicy', @level2type = N'COLUMN', @level2name = N'CoveragePercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto asegurado en pesos; NUMERIC(18,2) valor total protegido por la póliza de seguros', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailPolicy', @level2type = N'COLUMN', @level2name = N'AmountInsured';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Monto asegurado', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailPolicy', @level2type = N'COLUMN', @level2name = N'AmountInsured';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailPolicy', @level2type = N'COLUMN', @level2name = N'AmountInsured';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de emisión de la póliza; DATETIME de expedición o inicio de vigencia del seguro', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailPolicy', @level2type = N'COLUMN', @level2name = N'EmissionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de emisión', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailPolicy', @level2type = N'COLUMN', @level2name = N'EmissionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailPolicy', @level2type = N'COLUMN', @level2name = N'EmissionDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de póliza de seguros; VARCHAR(50) identificador único del asegurador, referencia de contrato de seguro', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailPolicy', @level2type = N'COLUMN', @level2name = N'PolicyNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'No. de poliza', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailPolicy', @level2type = N'COLUMN', @level2name = N'PolicyNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailPolicy', @level2type = N'COLUMN', @level2name = N'PolicyNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la aseguradora o compañía de seguros; FK a FixedAsset.FixedAssetInsurance, proveedor del seguro', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailPolicy', @level2type = N'COLUMN', @level2name = N'FixedAssetInsuranceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la aseguradora', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailPolicy', @level2type = N'COLUMN', @level2name = N'FixedAssetInsuranceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailPolicy', @level2type = N'COLUMN', @level2name = N'FixedAssetInsuranceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del tipo o clase de póliza de seguros; FK a FixedAsset.FixedAssetPolicy, categoría de cobertura', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailPolicy', @level2type = N'COLUMN', @level2name = N'FixedAssetPolicyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la poliza', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailPolicy', @level2type = N'COLUMN', @level2name = N'FixedAssetPolicyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailPolicy', @level2type = N'COLUMN', @level2name = N'FixedAssetPolicyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del detalle del contrato; FK a Contract.ContractDetail, línea específica del contrato vinculada', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailPolicy', @level2type = N'COLUMN', @level2name = N'ContractDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle del contato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailPolicy', @level2type = N'COLUMN', @level2name = N'ContractDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailPolicy', @level2type = N'COLUMN', @level2name = N'ContractDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de póliza de seguros; INT IDENTITY clave primaria de ContractDetailPolicy', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailPolicy', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailPolicy', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailPolicy', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Pólizas de seguros asociadas al detalle de un contrato. Registra la información de cada póliza vinculada a un activo fijo asegurado dentro de un contrato, incluyendo cobertura, monto asegurado y vigencia.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailPolicy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailPolicy';
