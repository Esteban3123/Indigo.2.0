CREATE TABLE [Admissions].[RecommendationsInunifunc] (
    [UFUCODIGO] VARCHAR (10)  NOT NULL,
    [CODCENATE] VARCHAR (10)  NULL,
    [UFUDESCRI] VARCHAR (100) NULL,
    [ID]        INT           IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK__Recommen__3214EC276844A29D] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK__Recommend__CODCE__1DAD6EBD] FOREIGN KEY ([CODCENATE]) REFERENCES [Admissions].[Recommendations] ([CODCENATE])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidades funcionales habilitadas para recibir recomendaciones de traslado o atención dentro del proceso de admisiones. Relaciona cada unidad funcional con su centro de atención correspondiente.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'RecommendationsInunifunc';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'RecommendationsInunifunc';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional (piso, servicio o área de atención) donde se pueden dirigir recomendaciones de ingreso.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'RecommendationsInunifunc', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'RecommendationsInunifunc', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención o sede a la que pertenece la unidad funcional.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'RecommendationsInunifunc', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'RecommendationsInunifunc', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o descripción de la unidad funcional, por ejemplo ''''Urgencias'''', ''''Hospitalización Piso 3'''', ''''UCI''''.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'RecommendationsInunifunc', @level2type = N'COLUMN', @level2name = N'UFUDESCRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'RecommendationsInunifunc', @level2type = N'COLUMN', @level2name = N'UFUDESCRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador interno único del registro, generado automáticamente por el sistema.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'RecommendationsInunifunc', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'RecommendationsInunifunc', @level2type = N'COLUMN', @level2name = N'ID';
