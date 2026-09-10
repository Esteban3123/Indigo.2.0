CREATE TABLE [EHR].[OncologycalSchemeByWeeksPrescription] (
    [Id]                          INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [OncologycalSchemasbyDrugsId] INT       NOT NULL,
    [CODPRODUC]                   CHAR (20) NOT NULL,
    [WeekNumber]                  INT       NOT NULL,
    CONSTRAINT [PK_OncologycalSchemeByWeeksPrescription] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OncologycalSchemeByWeeksPrescription_IHLISTPRO] FOREIGN KEY ([CODPRODUC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC])
);




GO


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Semanas de prescripción dentro de un esquema oncológico: indica en qué semanas del ciclo de tratamiento se debe administrar cada medicamento oncológico.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByWeeksPrescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByWeeksPrescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador interno único del registro de semana de prescripción oncológica.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByWeeksPrescription', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByWeeksPrescription', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia al medicamento dentro del esquema oncológico al que pertenece esta semana de prescripción.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByWeeksPrescription', @level2type = N'COLUMN', @level2name = N'OncologycalSchemasbyDrugsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByWeeksPrescription', @level2type = N'COLUMN', @level2name = N'OncologycalSchemasbyDrugsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del producto o medicamento oncológico que se prescribe en la semana indicada.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByWeeksPrescription', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByWeeksPrescription', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de semana del ciclo oncológico en la que se debe administrar el medicamento.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByWeeksPrescription', @level2type = N'COLUMN', @level2name = N'WeekNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByWeeksPrescription', @level2type = N'COLUMN', @level2name = N'WeekNumber';
