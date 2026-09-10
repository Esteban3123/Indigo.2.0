CREATE TABLE [dbo].[HCPLAOTRPROC] (
    [ID]                INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCHISPACA]       INT                                                                              NOT NULL,
    [IPCODPACI]         VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]         CHAR (10)                                                                        NOT NULL,
    [NUMEFOLIO]         NCHAR (10)                                                                       NOT NULL,
    [CODCENATE]         CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]         CHAR (10)                                                                        NOT NULL,
    [FECHAREGISTRO]     DATETIME                                                                         NOT NULL,
    [CODPROSAL]         CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [NOMBREACOM]        VARCHAR (100)                                                                    NULL,
    [PARENTESCO]        INT                                                                              NULL,
    [MEDICO]            VARCHAR (100)                                                                    NULL,
    [ESPECIALIDAD]      CHAR (3)                                                                         NULL,
    [SEDACION]          BIT                                                                              NULL,
    [LOCAL]             BIT                                                                              NULL,
    [GENERAL]           BIT                                                                              NULL,
    [SINANESTESIA]      BIT                                                                              NULL,
    [HORAINICIO]        DATETIME                                                                         NULL,
    [HORAFINAL]         DATETIME                                                                         NULL,
    [ANESTESIOLOGO]     CHAR (20)                                                                        NULL,
    [DATOSCLINICOS]     VARCHAR (MAX)                                                                    NOT NULL,
    [AMBITO]            INT                                                                              NOT NULL,
    [ANALISIS]          VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "Analysis_Ofuscado", 0)')      NOT NULL,
    [RESULCOLONOSCOPIA] INT                                                                              NULL,
    [ACOMPANANTE]       INT                                                                              NOT NULL,
    [PLANTILLA]         VARCHAR (MAX)                                                                    NULL,
    CONSTRAINT [PK_HCPLAOTRPROC] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCPLAOTRPROC_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCPLAOTRPROC_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCPLAOTRPROC].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCPLAOTRPROC].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCPLAOTRPROC].[ANALISIS]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Plantilla de otros procedimientos; contenido VARCHAR(MAX) reutilizable para registros de intervenciones, endoscopias, biopsias u otros actos quirúrgicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'PLANTILLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la plantilla de otros procedimientos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'PLANTILLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'PLANTILLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT): 0=Sin acompañante, 1=Con acompañante presente en procedimiento; información de control de acceso a sala.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'ACOMPANANTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'viene con acompañante:  0-NO  1-SI', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'ACOMPANANTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'ACOMPANANTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado endoscópico de colonoscopia (INT): 0=No aplica, 2=Pólipos hiperplásicos, 3=Preneoplásico, 4=Neoplásico, 5=Normal, 6=Otros hallazgos, 21=Riesgo no evaluado; hallazgo de diagnóstico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'RESULCOLONOSCOPIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el resultado de la colonoscopia (0. no aplica, 2. Hallazgos compatibles con pólipos hiperplásicos, 3. Hallazgos sugestivos de proceso preneoplásico , 4. Hallazgos sugestivos de proceso neoplásico, 5. Colonoscopia normal, 6. Otros hallazgos, 21. Riesgo no evaluado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'RESULCOLONOSCOPIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'RESULCOLONOSCOPIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Análisis y conclusiones clínicas del procedimiento (VARCHAR MAX, PII ofuscado); interpretación médica y recomendaciones diagnósticas o terapéuticas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'ANALISIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Analisis y Conclusiones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'ANALISIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'ANALISIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ámbito de realización del procedimiento (INT): 1=Ambulatorio, 2=Hospitalario, 3=Urgencias; contexto de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'AMBITO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ambito de realizacion: 1.Ambulatorio 2.Hospitalario 3.Urgencias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'AMBITO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'AMBITO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Datos clínicos relevantes preoperatorios y antecedentes (VARCHAR MAX); historia clínica condensada para justificación del procedimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'DATOSCLINICOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Datos Clinicos Relevantes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'DATOSCLINICOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'DATOSCLINICOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o identificación del profesional anestesiólogo (CHAR 20); profesional de la salud responsable de la anestesia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'ANESTESIOLOGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'ANESTESIOLOGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'ANESTESIOLOGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora final del procedimiento (DATETIME, obsoleto por refactoring); timestamp de término de intervención quirúrgica o endoscópica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'HORAFINAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y Hora Final (columna obsolota por refactoring)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'HORAFINAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'HORAFINAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de inicio del procedimiento (DATETIME, obsoleto por refactoring); timestamp de comienzo de intervención quirúrgica o endoscópica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'HORAINICIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y Hora de inicio (columna obsolota por refactoring)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'HORAINICIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'HORAINICIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) de procedimiento SIN anestesia; tipo de anestesia aplicada (0=No, 1=Sin anestesia).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'SINANESTESIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de anestesia: Sin Anestesia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'SINANESTESIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'SINANESTESIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) de anestesia general aplicada; tipo de anestesia aplicada (0=No, 1=Anestesia general).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'GENERAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de anestesia: General', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'GENERAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'GENERAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) de anestesia local aplicada; tipo de anestesia aplicada (0=No, 1=Anestesia local).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'LOCAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de anestesia: Local', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'LOCAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'LOCAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) de sedación aplicada; tipo de anestesia/sedación (0=No, 1=Sedación).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'SEDACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de anestesia: Sedacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'SEDACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'SEDACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de especialidad médica (CHAR 3, obsoleto por refactoring); clasificación de procedimiento especializado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'ESPECIALIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de especialidad (columna obsolota por refactoring)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'ESPECIALIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'ESPECIALIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del médico ordenante o ejecutor del procedimiento (VARCHAR 100, obsoleto por refactoring); profesional de la salud responsable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'MEDICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Medico que ordena el procedimiento (columna obsolota por refactoring)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'MEDICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'MEDICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación familiar del acompañante con el paciente (INT); vínculo de parentesco o relación de convivencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'PARENTESCO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Parentesco del acompañante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'PARENTESCO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'PARENTESCO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo del acompañante del paciente (VARCHAR 100); responsable o familiar presente durante procedimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'NOMBREACOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del acompañante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'NOMBREACOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'NOMBREACOM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud ejecutor (VARCHAR 25, PII ofuscado); identificación de médico, cirujano o especialista.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del profesional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del registro de la historia clínica (DATETIME); timestamp de creación del documento clínico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de unidad funcional (CHAR 10); departamento, servicio o área clínica donde se realiza procedimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención (CHAR 10); institución, clínica u hospital donde se ejecuta procedimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio clínico (NCHAR 10); identificador secuencial del registro en expediente del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso u admisión (CHAR 10); identificador de episodio de atención hospitalaria o urgencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente, cédula o documento de identificación (VARCHAR 25, PII ofuscado); equivalente a identificación, RUT, NUI.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cabecera en tabla HCHISPACA (INT); FK relacional a historia clínica del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'IDHCHISPACA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id tabla HCHISPACA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'IDHCHISPACA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'IDHCHISPACA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico (INT, PK clustered); clave primaria de cabecera de historia clínica de otros procedimientos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id autonumerico cabecera de HC Otros Procedimientos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de otros procedimientos clínicos realizados a pacientes durante un ingreso, incluyendo datos del procedimiento, tipo de anestesia utilizada, acompañante, médico tratante, hallazgos clínicos y resultados (por ejemplo colonoscopias u otros procedimientos especiales).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC';
