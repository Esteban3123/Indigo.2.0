CREATE TABLE [dbo].[HCEPICRIS] (
    [NUMEPICRI]    CHAR (10)                                                                        NOT NULL,
    [IDETIPHIS]    CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO]    CHAR (10)                                                                        NOT NULL,
    [IPCODPACI]    VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]    CHAR (10)                                                                        NOT NULL,
    [CODCENATE]    CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]    CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]    CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECREGEPI]    DATETIME                                                                         NOT NULL,
    [COMPLICAC]    VARCHAR (4000)                                                                   NOT NULL,
    [PRONOSTIC]    VARCHAR (4000) MASKED WITH (FUNCTION = 'partial(0, "Forecast_Ofuscado", 0)')     NOT NULL,
    [RECOMENDA]    VARCHAR (4000)                                                                   NOT NULL,
    [INDAUDFOR]    NUMERIC (18)                                                                     NOT NULL,
    [EPIRESUMIDA]  TEXT                                                                             NULL,
    [FECREGEPIRES] DATETIME                                                                         NULL,
    [FECMODEPIRES] DATETIME                                                                         NULL,
    [USUARIOCREA]  CHAR (20)                                                                        NULL,
    [USUARIOMOD]   CHAR (20)                                                                        NULL,
    CONSTRAINT [PK_HCEPICRIS] PRIMARY KEY CLUSTERED ([NUMEPICRI] ASC),
    CONSTRAINT [FK_HCEPICRIS_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCEPICRIS_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCEPICRIS_INPacient] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCEPICRIS_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCEPICRIS_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCEPICRIS] NOCHECK CONSTRAINT [FK_HCEPICRIS_INPROFSAL];


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCEPICRIS].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCEPICRIS].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCEPICRIS].[PRONOSTIC]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');

GO
CREATE NONCLUSTERED INDEX [IX_HCEPICRIS]
    ON [dbo].[HCEPICRIS]([IPCODPACI] ASC, [NUMEFOLIO] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_HCEPICRIS_NUMINGRES>]
    ON [dbo].[HCEPICRIS]([NUMINGRES] ASC)
    INCLUDE([FECREGEPI]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que modifica la epicrisis resumida; login/cuenta del profesional que realiza cambios en el documento de egreso (CHAR 20)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEPICRIS', @level2type = N'COLUMN', @level2name = N'USUARIOMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'usuario que modifica la epicrisis resumida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEPICRIS', @level2type = N'COLUMN', @level2name = N'USUARIOMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEPICRIS', @level2type = N'COLUMN', @level2name = N'USUARIOMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que crea la epicrisis resumida; login/cuenta del profesional que registra el documento de egreso inicial (CHAR 20)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEPICRIS', @level2type = N'COLUMN', @level2name = N'USUARIOCREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que crea la epicrisis resumida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEPICRIS', @level2type = N'COLUMN', @level2name = N'USUARIOCREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEPICRIS', @level2type = N'COLUMN', @level2name = N'USUARIOCREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de modificación de la epicrisis resumida; timestamp del último cambio en el resumen de egreso (DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEPICRIS', @level2type = N'COLUMN', @level2name = N'FECMODEPIRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fecha de modificacion de la epicrisis resumida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEPICRIS', @level2type = N'COLUMN', @level2name = N'FECMODEPIRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEPICRIS', @level2type = N'COLUMN', @level2name = N'FECMODEPIRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de registro/creación de la epicrisis resumida; timestamp de generación inicial del documento de egreso (DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEPICRIS', @level2type = N'COLUMN', @level2name = N'FECREGEPIRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fecha de registro de creacion de la epicrisis resumida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEPICRIS', @level2type = N'COLUMN', @level2name = N'FECREGEPIRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEPICRIS', @level2type = N'COLUMN', @level2name = N'FECREGEPIRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Epicrisis resumida; resumen clínico del egreso del paciente, hallazgos principales y estado al alta (TEXT, hasta 4000 caracteres)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEPICRIS', @level2type = N'COLUMN', @level2name = N'EPIRESUMIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Epicrisis resumida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEPICRIS', @level2type = N'COLUMN', @level2name = N'EPIRESUMIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEPICRIS', @level2type = N'COLUMN', @level2name = N'EPIRESUMIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicación auditoría/destino del paciente post-egreso: 1=Orden Hospitalización, 2=Urgencias, 3=Observación, 4=Cirugía, 5=Remisión, 6=Morgue, 7=Consulta Externa, 8=Alta (NUMERIC 18)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEPICRIS', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicacion Paciente:  1: Orden de Hospitalizacion  2: Urgencias  3: Dejar en Observacion  4: Cirugia  5: Remitir  6: Morgue  7: Remitir a Consulta Externa  8: Salida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEPICRIS', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEPICRIS', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recomendaciones clínicas post-egreso; revisión por sistemas, instrucciones de seguimiento y cuidados al paciente (VARCHAR 4000)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEPICRIS', @level2type = N'COLUMN', @level2name = N'RECOMENDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Revision por Sistemas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEPICRIS', @level2type = N'COLUMN', @level2name = N'RECOMENDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEPICRIS', @level2type = N'COLUMN', @level2name = N'RECOMENDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Pronóstico; descripción de la enfermedad actual, evolución clínica esperada y estado del paciente al egreso (VARCHAR 4000, datos sensibles enmascarados)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEPICRIS', @level2type = N'COLUMN', @level2name = N'PRONOSTIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Enfermedad Actual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEPICRIS', @level2type = N'COLUMN', @level2name = N'PRONOSTIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEPICRIS', @level2type = N'COLUMN', @level2name = N'PRONOSTIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Complicaciones y motivo de consulta; eventos adversos, hallazgos clínicos relevantes y razón principal de la atención (VARCHAR 4000)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEPICRIS', @level2type = N'COLUMN', @level2name = N'COMPLICAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivo de Consulta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEPICRIS', @level2type = N'COLUMN', @level2name = N'COMPLICAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEPICRIS', @level2type = N'COLUMN', @level2name = N'COMPLICAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de registro de epicrisis; timestamp de creación del documento de egreso en la historia clínica (DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEPICRIS', @level2type = N'COLUMN', @level2name = N'FECREGEPI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Registro de Epicrisis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEPICRIS', @level2type = N'COLUMN', @level2name = N'FECREGEPI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEPICRIS', @level2type = N'COLUMN', @level2name = N'FECREGEPI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de la salud; identificación del médico/especialista responsable del egreso, FK a INPROFSAL (VARCHAR 25, datos PII enmascarados)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEPICRIS', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEPICRIS', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEPICRIS', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional; área/servicio clínico donde se realizó la atención y egreso, FK a INUNIFUNC (CHAR 10)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEPICRIS', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEPICRIS', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEPICRIS', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención; institución/sede donde se registra el egreso, FK a ADCENATEN (CHAR 10)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEPICRIS', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEPICRIS', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEPICRIS', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del ingreso; identificador único del episodio de atención/hospitalización del paciente, FK a ADINGRESO (CHAR 10)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEPICRIS', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEPICRIS', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEPICRIS', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente; cédula, identificación o documento de identidad del paciente, FK a INPACIENT (VARCHAR 25, datos PII enmascarados)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEPICRIS', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEPICRIS', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEPICRIS', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio de la historia clínica; identificador del registro donde se realizó el egreso (CHAR 10)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEPICRIS', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Folio de la Historia en donde se realizo el egreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEPICRIS', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEPICRIS', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre interno del tipo de historia clínica; clasificación del formato/tipo de documento donde se realizó el egreso (CHAR 9)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEPICRIS', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Interno Tipo Historia en donde se realizo el Egreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEPICRIS', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEPICRIS', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de epicrisis; identificador único y consecutivo del documento de egreso, PK (CHAR 10, ej: 00000016)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEPICRIS', @level2type = N'COLUMN', @level2name = N'NUMEPICRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Epicrisis - Consecutivo 00000016 Inconsecu', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEPICRIS', @level2type = N'COLUMN', @level2name = N'NUMEPICRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEPICRIS', @level2type = N'COLUMN', @level2name = N'NUMEPICRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Epicrisis de historia clínica: registra el resumen de egreso del paciente al finalizar su hospitalización o atención, incluyendo complicaciones presentadas, pronóstico y recomendaciones emitidas por el profesional de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEPICRIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEPICRIS';
