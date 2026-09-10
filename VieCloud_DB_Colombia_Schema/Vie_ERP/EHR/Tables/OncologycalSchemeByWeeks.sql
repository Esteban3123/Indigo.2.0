CREATE TABLE [EHR].[OncologycalSchemeByWeeks] (
    [Id]                          INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [OncologycalSchemasbyDrugsId] INT NOT NULL,
    [WeekNumber]                  INT NOT NULL,
    CONSTRAINT [PK_OncologycalSchemeByWeeks] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OncologycalSchemeByWeeks_OncologycalSchemesByDrugs] FOREIGN KEY ([OncologycalSchemasbyDrugsId]) REFERENCES [EHR].[OncologycalSchemeByDrugs] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de semana (1-52) que identifica la semana del ciclo oncológico; secuencia temporal del esquema de quimioterapia o tratamiento del cáncer por semana de administración.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByWeeks', @level2type = N'COLUMN', @level2name = N'WeekNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el numero semana', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByWeeks', @level2type = N'COLUMN', @level2name = N'WeekNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByWeeks', @level2type = N'COLUMN', @level2name = N'WeekNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del esquema oncológico por fármacos; vincula a la tabla OncologycalSchemeByDrugs para referenciar el protocolo de medicamentos de cáncer/quimioterapia asociado a esta semana.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByWeeks', @level2type = N'COLUMN', @level2name = N'OncologycalSchemasbyDrugsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Esquema Oncológico Por Semanas', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByWeeks', @level2type = N'COLUMN', @level2name = N'OncologycalSchemasbyDrugsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByWeeks', @level2type = N'COLUMN', @level2name = N'OncologycalSchemasbyDrugsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK, IDENTITY); clave primaria que genera consecutivo automático para cada registro de semana en el esquema oncológico.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByWeeks', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByWeeks', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByWeeks', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de las semanas de aplicación asociadas a cada esquema oncológico por medicamento. Permite definir en qué semanas del ciclo de tratamiento oncológico se administra cada droga del esquema.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByWeeks';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByWeeks';
