CREATE TABLE [dbo].[HCANTPACH] (
    [IPCODPACI]         VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')      NOT NULL,
    [ANTMEDPAC]         VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTQUIPAC]         VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTTRAPAC]         VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTINMPAC]         VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTALEPAC]         VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTTRUPAC]         VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTPSIPAC]         VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTFARPAC]         VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTFAMPAC]         VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTTOXPAC]         VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTOTRPAC]         VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [INDAUDFOR]         NUMERIC (18)                                                                          NOT NULL,
    [ANTANESTE]         VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTHABVID]         VARCHAR (MAX)                                                                         NULL,
    [ANTESCOLARES]      VARCHAR (MAX)                                                                         NULL,
    [ANTLABORALES]      VARCHAR (MAX)                                                                         NULL,
    [ANTNUTRICION]      VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTODONTOLOG]      VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTSOCIOECON]      VARCHAR (MAX)                                                                         NULL,
    [ANTUROLOGIASEXUAL] VARCHAR (MAX)                                                                         NULL,
    [Ophthalmological]  VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    CONSTRAINT [PK_HCANTPACH] PRIMARY KEY CLUSTERED ([IPCODPACI] ASC),
    CONSTRAINT [FK_HCANTPACH_INPacient] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPACH].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPACH].[ANTMEDPAC]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPACH].[ANTQUIPAC]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPACH].[ANTTRAPAC]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPACH].[ANTINMPAC]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPACH].[ANTALEPAC]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPACH].[ANTTRUPAC]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPACH].[ANTPSIPAC]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPACH].[ANTFARPAC]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPACH].[ANTFAMPAC]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPACH].[ANTTOXPAC]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPACH].[ANTOTRPAC]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPACH].[ANTANESTE]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPACH].[ANTNUTRICION]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPACH].[ANTODONTOLOG]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPACH].[Ophthalmological]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes oftalmológicos, historia de enfermedades oculares y visuales del paciente (VARCHAR MAX, PII enmascarado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'Ophthalmological';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Oftalmológicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'Ophthalmological';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'Ophthalmological';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes urológicos y sexuales, historia de afecciones urogenitales y función sexual (VARCHAR MAX, PII enmascarado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTUROLOGIASEXUAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Urologia Sexual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTUROLOGIASEXUAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTUROLOGIASEXUAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes socioeconómicos, condiciones sociales y económicas del paciente (VARCHAR MAX)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTSOCIOECON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes SocioEconómicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTSOCIOECON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTSOCIOECON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes odontológicos, historia dental y tratamientos bucales (VARCHAR MAX, PII enmascarado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTODONTOLOG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Odontológicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTODONTOLOG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTODONTOLOG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes nutricionales, estado nutricional e historial dietético del paciente (VARCHAR MAX, PII enmascarado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTNUTRICION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Nutricionales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTNUTRICION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTNUTRICION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes laborales, historia ocupacional y exposiciones laborales (VARCHAR MAX)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTLABORALES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes  Laborales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTLABORALES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTLABORALES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes escolares, historial educativo del paciente (VARCHAR MAX)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTESCOLARES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Escolares', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTESCOLARES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTESCOLARES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes de hábitos de vida, consumo de alcohol, tabaco y otros hábitos (VARCHAR MAX)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTHABVID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Habitos de Vida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTHABVID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTHABVID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes anestésicos, historia de anestesias previas y reacciones adversas (VARCHAR MAX, PII enmascarado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTANESTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Anestésicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTANESTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTANESTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de auditoría, identificador numérico para trazabilidad de registros (NUMERIC 18)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes otros, información clínica adicional no categorizada (VARCHAR MAX, PII enmascarado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTOTRPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Otros', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTOTRPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTOTRPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes tóxicos, exposición a sustancias tóxicas y consumo de drogas (VARCHAR MAX, PII enmascarado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTTOXPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Toxicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTTOXPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTTOXPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes familiares, historia de enfermedades hereditarias en familia (VARCHAR MAX, PII enmascarado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTFAMPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Familiares', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTFAMPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTFAMPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes farmacológicos, medicamentos utilizados y alergias medicamentosas (VARCHAR MAX, PII enmascarado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTFARPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Farmacologicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTFARPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTFARPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes psicológicos y psiquiátricos, historia de trastornos mentales y emocionales (VARCHAR MAX, PII enmascarado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTPSIPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Psicologicos y Psiquiatricos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTPSIPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTPSIPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes traumáticos, lesiones y traumatismos previos del paciente (VARCHAR MAX, PII enmascarado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTTRUPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Traumaticos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTTRUPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTTRUPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes alérgicos, alergias conocidas a medicamentos, alimentos y sustancias (VARCHAR MAX, PII enmascarado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTALEPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Alergicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTALEPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTALEPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes inmunológicos, historia de inmunizaciones, VIH y enfermedades infecciosas (VARCHAR MAX, PII enmascarado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTINMPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Inmunologicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTINMPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTINMPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes transfusionales, historia de transfusiones de sangre y productos sanguíneos (VARCHAR MAX, PII enmascarado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTTRAPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Transfucionales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTTRAPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTTRAPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes quirúrgicos, historia de cirugías y procedimientos quirúrgicos previos (VARCHAR MAX, PII enmascarado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTQUIPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Quirurgicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTQUIPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTQUIPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes médicos, historia clínica general de enfermedades y tratamientos (VARCHAR MAX, PII enmascarado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTMEDPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Medicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTMEDPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'ANTMEDPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente, identificador único equivalente a cédula/documento/DNI (VARCHAR 25, PK, FK referencia INPACIENT, PII enmascarado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes personales y clínicos del paciente registrados en la historia clínica. Consolida todos los tipos de antecedentes: médicos, quirúrgicos, traumáticos, familiares, farmacológicos, alérgicos, psicológicos, toxicológicos, nutricionales, odontológicos, laborales, socioeconómicos, urológicos y oftalmológicos de cada paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACH';
