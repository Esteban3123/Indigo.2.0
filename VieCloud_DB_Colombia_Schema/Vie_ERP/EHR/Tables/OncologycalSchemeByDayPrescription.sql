CREATE TABLE [EHR].[OncologycalSchemeByDayPrescription] (
    [Id]                          INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [OncologycalSchemasbyDrugsId] INT       NOT NULL,
    [CODPRODUC]                   CHAR (20) NOT NULL,
    [DayNumber]                   INT       NOT NULL,
    CONSTRAINT [PK_OncologycalSchemeByDayPrescription] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OncologycalSchemeByDayPrescription_IHLISTPRO] FOREIGN KEY ([CODPRODUC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de día dentro del esquema oncológico de prescripción. Indica la secuencia temporal (día 1, día 2, etc.) en que se administra el medicamento dentro del ciclo de tratamiento oncológico.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDayPrescription', @level2type = N'COLUMN', @level2name = N'DayNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el numero de dia', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDayPrescription', @level2type = N'COLUMN', @level2name = N'DayNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDayPrescription', @level2type = N'COLUMN', @level2name = N'DayNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del producto/medicamento (FK a IHLISTPRO). Identificador único del fármaco oncológico prescrito en el esquema. Referencia a catálogo de productos farmacéuticos.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDayPrescription', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el codigo produción', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDayPrescription', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDayPrescription', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del esquema oncológico por fármaco. Vincula la prescripción diaria al esquema madre de tratamiento oncológico que agrupa múltiples medicamentos.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDayPrescription', @level2type = N'COLUMN', @level2name = N'OncologycalSchemasbyDrugsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el esquema oncologico', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDayPrescription', @level2type = N'COLUMN', @level2name = N'OncologycalSchemasbyDrugsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDayPrescription', @level2type = N'COLUMN', @level2name = N'OncologycalSchemasbyDrugsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PRIMARY KEY). Consecutivo autonumérico que identifica unívocamente cada registro de medicamento prescrito por día en el esquema oncológico.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDayPrescription', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDayPrescription', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDayPrescription', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalla los días específicos de administración de cada medicamento dentro de un esquema oncológico de tratamiento. Permite definir qué fármaco se aplica en qué día del ciclo de quimioterapia u oncología.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDayPrescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDayPrescription';
