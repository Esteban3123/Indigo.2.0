CREATE TABLE [dbo].[HCEDITABL] (
    [IDETIPHIS] CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO] NCHAR (10)                                                                       NOT NULL,
    [CONSFOLIO] CHAR (10)                                                                        NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO] CHAR (10)                                                                        NOT NULL,
    [FECINIATE] DATETIME                                                                         NOT NULL,
    [SUBJETIVO] VARCHAR (MAX)                                                                    NULL,
    [ANALISISP] VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "Analysis_Ofuscado", 0)')      NULL,
    [INDICAPAC] CHAR (2)                                                                         NOT NULL,
    [INDICAMED] VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "Instructions_Ofuscado", 0)')  NOT NULL,
    [TRAINTUNI] BIT                                                                              NOT NULL,
    [MOTTRAINT] VARCHAR (2000)                                                                   NULL,
    [INDAUDFOR] NUMERIC (18)                                                                     NOT NULL,
    [MOSEPICRI] BIT                                                                              NULL,
    [CODESPTRA] CHAR (3)                                                                         NULL,
    CONSTRAINT [PK_HCEDITABL_1] PRIMARY KEY CLUSTERED ([NUMEFOLIO] ASC, [IPCODPACI] ASC),
    CONSTRAINT [FK_HCEDITABL_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCEDITABL_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCEDITABL_INESPECIA] FOREIGN KEY ([CODESPTRA]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_HCEDITABL_INPacient] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCEDITABL_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCEDITABL].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCEDITABL].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCEDITABL].[ANALISISP]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCEDITABL].[INDICAMED]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la especialidad tratante en historia clínica (VARCHAR 3, FK→INESPECIA). Válido solo para hospitalizaciones, se actualiza con interconsultas. Refleja especialidad actual, sin histórico. Referencia: HCHISPACA.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEDITABL', @level2type = N'COLUMN', @level2name = N'CODESPTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Historias Clinicas: Codigo de la Especialidad Tratante  Opcion valida solo para unidades funcionales de tipo Hospitalizacion - Se actualiza con Interconsultas. Muestra la especialidad acutal tratante, no se tiene opcion para guardar historico de especialidades tratantes, opcionalmente se puede obtener de HCHISPACA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEDITABL', @level2type = N'COLUMN', @level2name = N'CODESPTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEDITABL', @level2type = N'COLUMN', @level2name = N'CODESPTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) para mostrar especialidad/datos en epicrisis (resumen clínico de egreso).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEDITABL', @level2type = N'COLUMN', @level2name = N'MOSEPICRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mostrar en  Epicrisis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEDITABL', @level2type = N'COLUMN', @level2name = N'MOSEPICRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEDITABL', @level2type = N'COLUMN', @level2name = N'MOSEPICRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicación de auditoría/destino del paciente post-atención (NUMERIC 18): 1=Orden hospitalización, 2=Urgencias, 3=Observación, 4=Cirugía, 5=Remisión, 6=Morgue, 7=Consulta externa, 8=Salida.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEDITABL', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicacion Paciente:  1: Orden de Hospitalizacion  2: Urgencias  3: Dejar en Observacion  4: Cirugia  5: Remitir  6: Morgue  7: Remitir a Consulta Externa  8: Salida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEDITABL', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEDITABL', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo del traslado interno del paciente dentro de la institución (VARCHAR 2000, opcional). Documentación clínica de justificación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEDITABL', @level2type = N'COLUMN', @level2name = N'MOTTRAINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivo del traslado interno del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEDITABL', @level2type = N'COLUMN', @level2name = N'MOTTRAINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEDITABL', @level2type = N'COLUMN', @level2name = N'MOTTRAINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) de traslado interno: True=Trasladado a otra unidad funcional misma IPS (indicación 3-4), False=Remisión/traslado a otra IPS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEDITABL', @level2type = N'COLUMN', @level2name = N'TRAINTUNI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Define si se ha realizado un Traslado Interno a otra Unidad, Cuando en las Indicaciones Medicas la enumeracion es 3 o 4.  True: Traslado Interno  False: Traslado a otra IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEDITABL', @level2type = N'COLUMN', @level2name = N'TRAINTUNI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEDITABL', @level2type = N'COLUMN', @level2name = N'TRAINTUNI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicaciones médicas generales al paciente (VARCHAR MAX, ofuscado). Instrucciones clínicas, prescripciones, recomendaciones post-atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEDITABL', @level2type = N'COLUMN', @level2name = N'INDICAMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicaciones Medicas Generales al Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEDITABL', @level2type = N'COLUMN', @level2name = N'INDICAMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEDITABL', @level2type = N'COLUMN', @level2name = N'INDICAMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de destino/disposición final del paciente (CHAR 2): 1=Urgencias, 2=Observ urgencias, 3=Hospitalización, 4=UCI adulto, 5=UCI pediátrica, 6=UCI neonatal, 7=Consulta externa, 8=Cirugía, 9=Hospitalización domiciliaria, 10=Referencia, 11=Morgue, 12=Salida, 13=Continúa en unidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEDITABL', @level2type = N'COLUMN', @level2name = N'INDICAPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Define el destino del paciente  1. Trasladar a Urgencias: Solo consulta externa  2. Trasladar a Observacion Urgencias: solo Urgencias  3. Trasladar a Hospitalizacion: Dif Misma Unidad  4. Trasladar a  UCI Adulto: Dif Misma Unidad  5. Trasladar a UCI Pediatrica: Dif Misma Unidad  6. Trasladar a UCI Neonatal: Dif Misma Unidad  7. Trasladar a Consulta Externa: Dif Misma Unidad  8. Trasladar a  Cirugia: Dif Misma Unidad  9. Hospitalizacion en Casa  10. Referencia  11. Morgue  12. Salida  13. Continua en la Unidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEDITABL', @level2type = N'COLUMN', @level2name = N'INDICAPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEDITABL', @level2type = N'COLUMN', @level2name = N'INDICAPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Análisis clínico, evaluación físico/funcional del paciente (VARCHAR MAX, ofuscado). Texto de valoración objetiva en historia de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEDITABL', @level2type = N'COLUMN', @level2name = N'ANALISISP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Analisis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEDITABL', @level2type = N'COLUMN', @level2name = N'ANALISISP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEDITABL', @level2type = N'COLUMN', @level2name = N'ANALISISP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relato subjetivo del paciente, síntomas y quejas (VARCHAR MAX, no ofuscado). Antecedentes, historia del padecimiento actual.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEDITABL', @level2type = N'COLUMN', @level2name = N'SUBJETIVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Subjetivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEDITABL', @level2type = N'COLUMN', @level2name = N'SUBJETIVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEDITABL', @level2type = N'COLUMN', @level2name = N'SUBJETIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de inicio de la atención/consulta clínica (DATETIME). Marca temporal de apertura de folio/evolución.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEDITABL', @level2type = N'COLUMN', @level2name = N'FECINIATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Inicial de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEDITABL', @level2type = N'COLUMN', @level2name = N'FECINIATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEDITABL', @level2type = N'COLUMN', @level2name = N'FECINIATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional (CHAR 10, FK→INUNIFUNC). Área clínica: hospitalización, urgencias, consulta, UCI, quirófano.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEDITABL', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEDITABL', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEDITABL', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro/institución de atención (CHAR 10, FK→ADCENATEN). Identificación de sede o hospital.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEDITABL', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEDITABL', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEDITABL', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del ingreso/admisión del paciente (CHAR 10, FK→ADINGRESO). Correlativo único por admisión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEDITABL', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEDITABL', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEDITABL', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente (VARCHAR 25, PII ofuscado, FK→INPACIENT). Identificación única: cédula, documento, pasaporte.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEDITABL', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEDITABL', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEDITABL', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de la salud (CHAR 20, PII ofuscado). Médico, enfermero, especialista que atiende.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEDITABL', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEDITABL', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEDITABL', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo de folio/historia de ingreso (CHAR 10). Secuencia de evoluciones dentro de una admisión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEDITABL', @level2type = N'COLUMN', @level2name = N'CONSFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la Historia de Ingreso a la que corresponde cada evolucion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEDITABL', @level2type = N'COLUMN', @level2name = N'CONSFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEDITABL', @level2type = N'COLUMN', @level2name = N'CONSFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio (NCHAR 10, PK). Identificador único de evolución/nota clínica en historia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEDITABL', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEDITABL', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEDITABL', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre/tipo interno de historia clínica (CHAR 9). Clasificación o categoría de documento clínico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEDITABL', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEDITABL', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEDITABL', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registros editables de la historia clínica SOAP del paciente por atención. Contiene las notas clínicas subjetivas, análisis, indicaciones médicas e indicadores de traslado para cada folio de evolución.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEDITABL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEDITABL';
