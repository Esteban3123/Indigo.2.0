CREATE TABLE [Inventory].[DCIRiskFactors] (
    [Id]           INT IDENTITY (1, 1) NOT NULL,
    [DciId]        INT NOT NULL,
    [RiskFactorId] INT NOT NULL,
    CONSTRAINT [PK_DCIRiskFactors_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RiskFactors_DciId] FOREIGN KEY ([DciId]) REFERENCES [Inventory].[DCI] ([Id]),
    CONSTRAINT [FK_RiskFactors_RiskFactorId] FOREIGN KEY ([RiskFactorId]) REFERENCES [dbo].[RiskFactor] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del factor de riesgo asociado (FK a RiskFactor). Referencia a condiciones, antecedentes o características que incrementan riesgo clínico del paciente', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DCIRiskFactors', @level2type = N'COLUMN', @level2name = N'RiskFactorId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del factor de riesgo asociado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DCIRiskFactors', @level2type = N'COLUMN', @level2name = N'RiskFactorId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DCIRiskFactors', @level2type = N'COLUMN', @level2name = N'RiskFactorId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del DCI (Documento Clínico Integrado) asociado (FK a Inventory.DCI). Vincula el factor de riesgo al registro clínico del paciente', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DCIRiskFactors', @level2type = N'COLUMN', @level2name = N'DciId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del DCI asociado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DCIRiskFactors', @level2type = N'COLUMN', @level2name = N'DciId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DCIRiskFactors', @level2type = N'COLUMN', @level2name = N'DciId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK) del registro de asociación entre DCI y factor de riesgo. Clave de relación muchos-a-muchos entre documento clínico y factores de riesgo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DCIRiskFactors', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro de los factores de riesgo del DCI', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DCIRiskFactors', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DCIRiskFactors', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona los medicamentos o principios activos (DCI) con sus factores de riesgo asociados, permitiendo identificar alertas clínicas y contraindicaciones según el perfil del paciente.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DCIRiskFactors';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DCIRiskFactors';
