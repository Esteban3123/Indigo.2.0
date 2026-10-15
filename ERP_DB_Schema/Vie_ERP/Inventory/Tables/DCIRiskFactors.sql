CREATE TABLE [Inventory].[DCIRiskFactors] (
    [Id]           INT IDENTITY (1, 1) NOT NULL,
    [DciId]        INT NOT NULL,
    [RiskFactorId] INT NOT NULL,
    CONSTRAINT [PK_DCIRiskFactors_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RiskFactors_DciId] FOREIGN KEY ([DciId]) REFERENCES [Inventory].[DCI] ([Id]),
    CONSTRAINT [FK_RiskFactors_RiskFactorId] FOREIGN KEY ([RiskFactorId]) REFERENCES [dbo].[RiskFactor] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del factor de riesgo asociado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DCIRiskFactors', @level2type = N'COLUMN', @level2name = N'RiskFactorId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del DCI asociado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DCIRiskFactors', @level2type = N'COLUMN', @level2name = N'DciId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro de los factores de riesgo del DCI', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DCIRiskFactors', @level2type = N'COLUMN', @level2name = N'Id';

