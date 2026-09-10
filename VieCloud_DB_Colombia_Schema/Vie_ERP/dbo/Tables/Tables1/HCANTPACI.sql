CREATE TABLE [dbo].[HCANTPACI] (
    [IDETIPHIS]         CHAR (9)                                                                              NOT NULL,
    [NUMEFOLIO]         NCHAR (10)                                                                            NOT NULL,
    [IPCODPACI]         VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')      NOT NULL,
    [NUMINGRES]         CHAR (10)                                                                             NOT NULL,
    [CODCENATE]         CHAR (10)                                                                             NOT NULL,
    [UFUCODIGO]         CHAR (10)                                                                             NOT NULL,
    [CODPROSAL]         CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')         NOT NULL,
    [FECHISPAC]         DATETIME                                                                              NOT NULL,
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
    [ANTGINPAC]         VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTOTRPAC]         VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTPERINA]         VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
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
    CONSTRAINT [PK_HCANTPACI] PRIMARY KEY CLUSTERED ([IDETIPHIS] ASC, [NUMEFOLIO] ASC, [IPCODPACI] ASC, [NUMINGRES] ASC),
    CONSTRAINT [FK_HCANTPACI_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCANTPACI_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCANTPACI_INPacient] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCANTPACI_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCANTPACI_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPACI].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPACI].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPACI].[ANTMEDPAC]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPACI].[ANTQUIPAC]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPACI].[ANTTRAPAC]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPACI].[ANTINMPAC]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPACI].[ANTALEPAC]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPACI].[ANTTRUPAC]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPACI].[ANTPSIPAC]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPACI].[ANTFARPAC]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPACI].[ANTFAMPAC]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPACI].[ANTTOXPAC]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPACI].[ANTGINPAC]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPACI].[ANTOTRPAC]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPACI].[ANTPERINA]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPACI].[ANTANESTE]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPACI].[ANTNUTRICION]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPACI].[ANTODONTOLOG]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPACI].[Ophthalmological]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
CREATE NONCLUSTERED INDEX [IX_HCANTPACI_NUMINGRES]
    ON [dbo].[HCANTPACI]([NUMINGRES] ASC)
    INCLUDE([ANTALEPAC], [ANTFAMPAC], [ANTFARPAC], [ANTGINPAC], [ANTINMPAC], [ANTMEDPAC], [ANTOTRPAC], [ANTPERINA], [ANTPSIPAC], [ANTQUIPAC], [ANTTOXPAC], [ANTTRAPAC], [ANTTRUPAC]);


GO
CREATE NONCLUSTERED INDEX [_dta_index_HCANTPACI_6_256719967__K3_K2_K4]
    ON [dbo].[HCANTPACI]([IPCODPACI] ASC, [NUMEFOLIO] ASC, [NUMINGRES] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes oftalmológicos, historia de problemas visuales, oftalmía, refracción, patología ocular (VARCHAR MAX, PII enmascarado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'Ophthalmological';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Oftalmológicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'Ophthalmological';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'Ophthalmological';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes urológicos y sexuales, disfunción eréctil, infecciones urinarias, historia sexual (VARCHAR MAX, clínico sensible)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTUROLOGIASEXUAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Urologia Sexual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTUROLOGIASEXUAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTUROLOGIASEXUAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes socioeconómicos, situación laboral, vivienda, acceso a servicios, estrato social (VARCHAR MAX, sin mask)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTSOCIOECON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes SocioEconómicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTSOCIOECON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTSOCIOECON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes odontológicos, salud dental, caries, tratamientos dentales, prótesis (VARCHAR MAX, PII enmascarado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTODONTOLOG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Odontológicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTODONTOLOG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTODONTOLOG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes nutricionales, dieta, estado nutricional, alergias alimentarias, suplementos (VARCHAR MAX, PII enmascarado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTNUTRICION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Nutricionales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTNUTRICION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTNUTRICION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes laborales, historial ocupacional, exposición ocupacional, accidentes de trabajo (VARCHAR MAX, sin mask)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTLABORALES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes  Laborales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTLABORALES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTLABORALES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes escolares, desempeño académico, nivel educativo, comportamiento escolar (VARCHAR MAX, sin mask)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTESCOLARES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Escolares', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTESCOLARES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTESCOLARES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes hábitos de vida, tabaquismo, alcoholismo, drogas, sedentarismo, actividad física (VARCHAR MAX, sin mask)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTHABVID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Habitos de Vida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTHABVID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTHABVID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes anestésicos, reacciones a anestesia, complicaciones anestésicas, alergias a fármacos anestésicos (VARCHAR MAX, PII enmascarado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTANESTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Anestesicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTANESTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTANESTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de auditoría, identificador auditoría, referencia de control, número auditoría clínica (NUMERIC 18, PK)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes perinatales, embarazo, parto, complicaciones perinatales, historia neonatal (VARCHAR MAX, PII enmascarado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTPERINA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Perinatales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTPERINA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTPERINA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes otros, historia clínica adicional, observaciones complementarias, datos relevantes sin clasificación (VARCHAR MAX, PII enmascarado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTOTRPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Otros', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTOTRPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTOTRPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes ginecológicos, ciclo menstrual, paridad, menopausia, patología ginecológica (VARCHAR MAX, PII enmascarado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTGINPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Ginecologicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTGINPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTGINPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes tóxicos, exposición a toxinas, drogas, sustancias peligrosas, intoxicaciones previas (VARCHAR MAX, PII enmascarado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTTOXPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Toxicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTTOXPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTTOXPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes familiares, historia familiar, enfermedades hereditarias, genealogía clínica (VARCHAR MAX, PII enmascarado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTFAMPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Familiares', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTFAMPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTFAMPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes farmacológicos, medicamentos previos, reacciones adversas, alergias medicamentosas (VARCHAR MAX, PII enmascarado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTFARPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Farmacologicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTFARPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTFARPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes psicológicos y psiquiátricos, depresión, ansiedad, trastornos mentales, tratamientos psiquiátricos (VARCHAR MAX, PII enmascarado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTPSIPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Psicologicos y Psiquiatricos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTPSIPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTPSIPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes traumáticos, traumatismos previos, lesiones, accidentes, historia de trauma (VARCHAR MAX, PII enmascarado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTTRUPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Traumaticos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTTRUPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTTRUPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes alérgicos, alergias conocidas, reacciones alérgicas, hipersensibilidades (VARCHAR MAX, PII enmascarado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTALEPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Alergicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTALEPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTALEPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes inmunológicos, vacunación, infecciones previas, inmunodeficiencias (VARCHAR MAX, PII enmascarado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTINMPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Inmunologicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTINMPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTINMPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes transfusionales, transfusiones previas, hemoderivados, reacciones transfusionales (VARCHAR MAX, PII enmascarado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTTRAPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Transfucionales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTTRAPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTTRAPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes quirúrgicos, cirugías previas, intervenciones quirúrgicas, complicaciones quirúrgicas (VARCHAR MAX, PII enmascarado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTQUIPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Quirurgicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTQUIPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTQUIPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes médicos, enfermedades crónicas, patologías previas, hospitalizaciones anteriores (VARCHAR MAX, PII enmascarado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTMEDPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Medicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTMEDPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'ANTMEDPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de historia clínica del paciente, fecha de registro, apertura de expediente (DATETIME, clave primaria)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'FECHISPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Historia Clinica Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'FECHISPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'FECHISPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de la salud, médico, especialista, responsable atención (VARCHAR 20, FK→INPROFSAL, PII enmascarado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional, servicio, área clínica, departamento (CHAR 10, FK→INUNIFUNC)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención, hospital, clínica, institución sanitaria (CHAR 10, FK→ADCENATEN)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del ingreso, admisión, episodio de atención, cita médica (CHAR 10, FK→ADINGRESO)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente, cédula, documento de identidad, equivalente a DNI/RUT (VARCHAR 25, FK→INPACIENT, PII enmascarado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio, número de página, foliación de historia (NCHAR 10, clave primaria)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador tipo historia clínica, categoría de expediente, clasificación interna (CHAR 9, clave primaria)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes clínicos del paciente registrados en la historia clínica. Contiene los diferentes tipos de antecedentes médicos, quirúrgicos, farmacológicos, familiares, ginecológicos, toxicológicos, perinatales, anestésicos, nutricionales, odontológicos, laborales, escolares, socioeconómicos, urológicos y oftalmológicos documentados por el profesional de la salud en cada atención o ingreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI';
