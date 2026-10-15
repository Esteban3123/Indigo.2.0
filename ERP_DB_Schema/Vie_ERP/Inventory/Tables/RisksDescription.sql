CREATE TABLE [Inventory].[RisksDescription] (
    [Id]                     INT           IDENTITY (1, 1) NOT NULL,
    [DciId]                  INT           NOT NULL,
    [TagRiskType]            INT           NOT NULL,
    [TagIncludedDescription] VARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_RisksDescriptionPLLR_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RisksDescription_DciId] FOREIGN KEY ([DciId]) REFERENCES [Inventory].[DCI] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripción incluida en la etiqueta', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RisksDescription', @level2type = N'COLUMN', @level2name = N'TagIncludedDescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de riesgo etiquetado                   1- Embarazo                   2- Lactancia                   3- Mujeres y hombres en edad reproductiva', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RisksDescription', @level2type = N'COLUMN', @level2name = N'TagRiskType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del DCI asociado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RisksDescription', @level2type = N'COLUMN', @level2name = N'DciId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro de descripción de riesgos PLLR- FDA', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RisksDescription', @level2type = N'COLUMN', @level2name = N'Id';

