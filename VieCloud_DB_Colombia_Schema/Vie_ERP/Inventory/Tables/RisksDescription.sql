CREATE TABLE [Inventory].[RisksDescription] (
    [Id]                     INT           IDENTITY (1, 1) NOT NULL,
    [DciId]                  INT           NOT NULL,
    [TagRiskType]            INT           NOT NULL,
    [TagIncludedDescription] VARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_RisksDescriptionPLLR_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RisksDescription_DciId] FOREIGN KEY ([DciId]) REFERENCES [Inventory].[DCI] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción incluida en la etiqueta de riesgo; texto VARCHAR(MAX) que especifica advertencias, contraindicaciones o precauciones del medicamento/insumo para poblaciones vulnerables', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RisksDescription', @level2type = N'COLUMN', @level2name = N'TagIncludedDescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción incluida en la etiqueta', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RisksDescription', @level2type = N'COLUMN', @level2name = N'TagIncludedDescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RisksDescription', @level2type = N'COLUMN', @level2name = N'TagIncludedDescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de riesgo etiquetado (categoría PLLR-FDA): 1=Embarazo, 2=Lactancia, 3=Mujeres y hombres en edad reproductiva; INT que clasifica poblaciones en riesgo según regulación farmacéutica', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RisksDescription', @level2type = N'COLUMN', @level2name = N'TagRiskType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de riesgo etiquetado                   1- Embarazo                   2- Lactancia                   3- Mujeres y hombres en edad reproductiva', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RisksDescription', @level2type = N'COLUMN', @level2name = N'TagRiskType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RisksDescription', @level2type = N'COLUMN', @level2name = N'TagRiskType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del DCI (Denominación Común Internacional) asociado; clave foránea INT que referencia [Inventory].[DCI]([Id]) para vincular descripción de riesgos al medicamento/principio activo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RisksDescription', @level2type = N'COLUMN', @level2name = N'DciId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del DCI asociado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RisksDescription', @level2type = N'COLUMN', @level2name = N'DciId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RisksDescription', @level2type = N'COLUMN', @level2name = N'DciId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de descripción de riesgos PLLR-FDA; clave primaria INT IDENTITY que autentica cada entrada de advertencia regulatoria en inventario farmacéutico', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RisksDescription', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro de descripción de riesgos PLLR- FDA', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RisksDescription', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RisksDescription', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripciones de riesgos asociados a medicamentos por su DCI (Denominación Común Internacional). Registra los textos de advertencia, precauciones o alertas de seguridad clasificados por tipo de riesgo, utilizados para informar sobre peligros en el uso de medicamentos en el inventario farmacéutico.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RisksDescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RisksDescription';
