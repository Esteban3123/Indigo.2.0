CREATE TABLE [dbo].[HCESCALDREMOD] (
    [ID]          INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]   VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]   CHAR (10)                                                                        NOT NULL,
    [CODCENATE]   CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]   CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]   CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECREGSIS]   DATETIME                                                                         NOT NULL,
    [ACTIVIDAD]   TINYINT                                                                          NOT NULL,
    [RESPIRACION] TINYINT                                                                          NOT NULL,
    [CIRCULACION] TINYINT                                                                          NOT NULL,
    [NIVCONCIEN]  TINYINT                                                                          NOT NULL,
    [SATUOXIGE]   TINYINT                                                                          NOT NULL,
    [APOQUIRUR]   TINYINT                                                                          NOT NULL,
    [DOLOR]       TINYINT                                                                          NOT NULL,
    [DEAMBULAC]   TINYINT                                                                          NOT NULL,
    [ALIMENTAC]   TINYINT                                                                          NOT NULL,
    [MICCION]     TINYINT                                                                          NOT NULL,
    [RESULTADO]   INT                                                                              NOT NULL,
    CONSTRAINT [PK_HCESCALDREMOD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCESCALDREMOD_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCESCALDREMOD_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCESCALDREMOD].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCESCALDREMOD].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado final de la escala de Aldrete modificada (puntuación numérica INT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'RESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'RESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'RESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Micción: escala 2-1-0 (función urinaria post-operatoria o post-procedimiento)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'MICCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Micción 2-1-0', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'MICCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'MICCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Alimentación: escala 2-1-0 (capacidad de ingesta oral post-operatoria)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'ALIMENTAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Alimentación 2-1-0', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'ALIMENTAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'ALIMENTAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deambulación: escala 2-1-0 (movilidad y marcha del paciente)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'DEAMBULAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Deambulación 2-1-0', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'DEAMBULAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'DEAMBULAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dolor: escala 2-1-0 (evaluación de dolor post-operatorio o post-procedimiento)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'DOLOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dolor 2-1-0', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'DOLOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'DOLOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Aposito quirúrgico: escala 2-1-0 (estado del vendaje y herida quirúrgica)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'APOQUIRUR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Aposito Qirurgico 2-1-0', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'APOQUIRUR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'APOQUIRUR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saturación de oxígeno: escala 2-1-0 (nivel de oxigenación en sangre)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'SATUOXIGE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Saturacion Oxigeno 2-1-0', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'SATUOXIGE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'SATUOXIGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel de conciencia: escala 2-1-0 (estado mental y orientación del paciente)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'NIVCONCIEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nivel Conciencia 2-1-0', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'NIVCONCIEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'NIVCONCIEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Circulación: escala 2-1-0 (estado cardiovascular y perfusión periférica)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'CIRCULACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Circulación 2-1-0', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'CIRCULACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'CIRCULACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Respiración: escala 2-1-0 (función respiratoria y ventilación)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'RESPIRACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Respiracion 2-1-0', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'RESPIRACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'RESPIRACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Actividad: escala 2-1-0 (nivel funcional y movilidad general)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'ACTIVIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Actividad 2-1-0', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'ACTIVIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'ACTIVIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de registro en el sistema (DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'FECREGSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha registro sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'FECREGSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'FECREGSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud (PII ofuscado, médico/enfermero responsable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del profesional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional de atención (sala, UCI, urgencia, etc.)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención (institución, sede o clínica)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código Centro Atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso del paciente (FK a ADINGRESO)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificación del paciente: cédula/documento PII ofuscado (FK a INPACIENT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo identificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de registro de la escala de Aldrete modificada (INT IDENTITY)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de evaluaciones de la escala de Aldrete modificada aplicada a pacientes durante su ingreso hospitalario. Mide el nivel de recuperación post-anestésica o post-procedimiento a través de parámetros clínicos como actividad, respiración, circulación, conciencia, saturación de oxígeno, dolor, deambulación, alimentación y micción, generando un puntaje total que determina si el paciente está en condiciones de ser dado de alta de la unidad de recuperación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDREMOD';
