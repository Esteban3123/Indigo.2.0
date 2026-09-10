CREATE TABLE [dbo].[HCPPYPPAC] (
    [CODCONCEC]       NUMERIC (18)                                                                     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]       VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]       CHAR (10)                                                                        NOT NULL,
    [CODCENATE]       CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]       CHAR (10)                                                                        NOT NULL,
    [CODPROPYP]       CHAR (4)                                                                         NOT NULL,
    [CODPROSAL]       CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [JUSNOREPP]       VARCHAR (4000)                                                                   NULL,
    [FECREGSIS]       DATETIME                                                                         NOT NULL,
    [IDRIAS]          INT                                                                              NULL,
    [DoctorSelection] BIT                                                                              NOT NULL,
    CONSTRAINT [PK_HCPPYPPAC] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC),
    CONSTRAINT [FK_HCPPYPPAC_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCPPYPPAC_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCPPYPPAC_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCPPYPPAC_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_HCPPYPPAC_RIAS] FOREIGN KEY ([IDRIAS]) REFERENCES [dbo].[RIAS] ([ID])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCPPYPPAC].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCPPYPPAC].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');




GO



GO



GO



GO



GO



GO
CREATE NONCLUSTERED INDEX [_dta_index_HCPPYPPAC_6_1248111587__K2_K4_K9_K5_3_6]
    ON [dbo].[HCPPYPPAC]([IPCODPACI] ASC, [CODCENATE] ASC, [FECREGSIS] ASC, [UFUCODIGO] ASC)
    INCLUDE([CODPROPYP], [NUMINGRES]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de relación con tabla RIAS (Red Integrada de Atención en Salud); vinculación a solicitudes/seguimiento de atenciones coordinadas. FK → [dbo].[RIAS]([ID])', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPPYPPAC', @level2type = N'COLUMN', @level2name = N'IDRIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con la tabla de RIAS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPPYPPAC', @level2type = N'COLUMN', @level2name = N'IDRIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPPYPPAC', @level2type = N'COLUMN', @level2name = N'IDRIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de registro del programa de PyP en el sistema; timestamp de creación del concepto/atención (DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPPYPPAC', @level2type = N'COLUMN', @level2name = N'FECREGSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha registro en el sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPPYPPAC', @level2type = N'COLUMN', @level2name = N'FECREGSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPPYPPAC', @level2type = N'COLUMN', @level2name = N'FECREGSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación narrativa de por qué no se recomienda o se rechaza un Programa de Promoción y Prevención al paciente; razones clínicas o administrativas (VARCHAR 4000, texto libre)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPPYPPAC', @level2type = N'COLUMN', @level2name = N'JUSNOREPP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justificacion porque no se recomienda un Programa de PyP al Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPPYPPAC', @level2type = N'COLUMN', @level2name = N'JUSNOREPP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPPYPPAC', @level2type = N'COLUMN', @level2name = N'JUSNOREPP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de la salud (médico, enfermero, promotor) que registra/aprueba el programa PyP; PII ofuscado. FK → [dbo].[??PROSAL] (VARCHAR 20 MASKED)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPPYPPAC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPPYPPAC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPPYPPAC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Programa de Promoción y Prevención: 0=Detección temprana alteraciones crecimiento/desarrollo <10 años, 1=Desarrollo joven 10-29 años, 2=Embarazo, 3=Adulto, 4=Cáncer cuello uterino, 5=Cáncer seno, 6=Agudeza visual, 9=Ninguno (CHAR 4)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPPYPPAC', @level2type = N'COLUMN', @level2name = N'CODPROPYP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Programa de promocion y prevencion  0: DETECCION TEMPRANA DE ALTERACIONES DE CRECIMIENTO Y DESARROLLO EN EL MENOR DE 10 AÑOS  1:DETECCION TEMPRANA DE ALTERACIONES DEL DESARROLLO DEL JOVEN 10 A 29 AÑOS  2:DETECCION TEMPRANA DE ALTERACIONES DEL EMBARAZO  3:DETECCIÓN TEMPRANA DE LAS ALTERACIONES DEL ADULTO  4:DETECCIÓN TEMPRANA DEL CANCER DE CUELLO UTERINO  5:DETECCIÓN TEMPRANA DEL CANCER DE SENO  6:DETECCIÓN TEMPRANA DE LAS ALTERACIONES DE LA AGUDEZA VISUAL  9:NINGUNO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPPYPPAC', @level2type = N'COLUMN', @level2name = N'CODPROPYP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPPYPPAC', @level2type = N'COLUMN', @level2name = N'CODPROPYP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la Unidad Funcional donde se atiende el paciente en PyP; departamento/consultorio/área de atención. FK → [dbo].[INUNIFUNC]([UFUCODIGO]) (CHAR 10)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPPYPPAC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPPYPPAC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPPYPPAC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Centro de Atención (clínica, hospital, puesto de salud) donde se registra el programa PyP. FK → [dbo].[ADCENATEN]([CODCENATE]) (CHAR 10)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPPYPPAC', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPPYPPAC', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPPYPPAC', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso/atención del paciente asociado al registro de PyP; referencia a historia clínica. FK → [dbo].[ADINGRESO]([NUMINGRES]) (CHAR 10)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPPYPPAC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPPYPPAC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPPYPPAC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente (cédula, identificación, documento); identificador único PII ofuscado en búsquedas. FK → [dbo].[INPACIENT]([IPCODPACI]) (VARCHAR 25 MASKED)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPPYPPAC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPPYPPAC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPPYPPAC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo interno único de la tabla; identificador surrogate autoincremental del concepto/registro de PyP (NUMERIC 18 IDENTITY)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPPYPPAC', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo interno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPPYPPAC', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPPYPPAC', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de actividades de promoción y prevención (PyP) realizadas a pacientes durante un ingreso. Guarda qué intervenciones preventivas o de promoción de salud se ejecutaron, por qué profesional, en qué centro de atención y unidad funcional, incluyendo la justificación cuando no se realizaron.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPPYPPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPPYPPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el profesional de salud que registró la actividad de promoción y prevención fue seleccionado manualmente por el usuario (verdadero/falso).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPPYPPAC', @level2type = N'COLUMN', @level2name = N'DoctorSelection';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPPYPPAC', @level2type = N'COLUMN', @level2name = N'DoctorSelection';
