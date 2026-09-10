CREATE TABLE [dbo].[HCNOTEVI1] (
    [IDETIPHIS] CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO] NCHAR (10)                                                                       NOT NULL,
    [CONSFOLIO] CHAR (10)                                                                        NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO] CHAR (10)                                                                        NOT NULL,
    [FECINIATE] DATETIME                                                                         NOT NULL,
    [ANALISISP] VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "Analysis_Ofuscado", 0)')      NULL,
    [INDICAPAC] CHAR (2)                                                                         NOT NULL,
    [INDICAMED] VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "Instructions_Ofuscado", 0)')  NOT NULL,
    [TRAINTUNI] BIT                                                                              NOT NULL,
    [MOTTRAINT] VARCHAR (2000)                                                                   NULL,
    [INDAUDFOR] NUMERIC (18)                                                                     NOT NULL,
    [MOSEPICRI] BIT                                                                              NULL,
    [CODESPTRA] CHAR (3)                                                                         NULL,
    [PLAINDMED] VARCHAR (MAX)                                                                    NULL,
    [FECHINIHI] DATETIME                                                                         NULL,
    CONSTRAINT [PK_HCNOTEVI1] PRIMARY KEY CLUSTERED ([NUMEFOLIO] ASC, [IPCODPACI] ASC),
    CONSTRAINT [FK_HCNOTEVI1_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCNOTEVI1_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCNOTEVI1_INESPECIA] FOREIGN KEY ([CODESPTRA]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_HCNOTEVI1_INPacient] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCNOTEVI1_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCNOTEVI1].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCNOTEVI1].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCNOTEVI1].[ANALISISP]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCNOTEVI1].[INDICAMED]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de inicio de la historia clínica o proceso de atención del paciente. Tipo: DATETIME. Marca el comienzo formal del registro evolutivo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVI1', @level2type = N'COLUMN', @level2name = N'FECHINIHI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la fecha inicio historia o proceso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVI1', @level2type = N'COLUMN', @level2name = N'FECHINIHI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVI1', @level2type = N'COLUMN', @level2name = N'FECHINIHI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Plantilla de indicaciones médicas generales; texto con instrucciones estándar o personalizadas para el manejo del paciente. Tipo: VARCHAR(MAX). Almacena directivas clínicas reutilizables.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVI1', @level2type = N'COLUMN', @level2name = N'PLAINDMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Plantilla de Indicaciones medicas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVI1', @level2type = N'COLUMN', @level2name = N'PLAINDMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVI1', @level2type = N'COLUMN', @level2name = N'PLAINDMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la especialidad tratante actual (FK: INESPECIA.CODESPECI). Válido solo en hospitalizaciones; se actualiza con interconsultas. No registra histórico de cambios. Tipo: CHAR(3). Búsqueda: especialidad, médico especialista.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVI1', @level2type = N'COLUMN', @level2name = N'CODESPTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Historias Clinicas: Codigo de la Especialidad Tratante  Opcion valida solo para unidades funcionales de tipo Hospitalizacion - Se actualiza con Interconsultas. Muestra la especialidad acutal tratante, no se tiene opcion para guardar historico de especialidades tratantes, opcionalmente se puede obtener de HCHISPACA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVI1', @level2type = N'COLUMN', @level2name = N'CODESPTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVI1', @level2type = N'COLUMN', @level2name = N'CODESPTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: si mostrar (1) u ocultar (0) en epicrisis. Tipo: BIT. Controla visibilidad de datos en resumen clínico final.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVI1', @level2type = N'COLUMN', @level2name = N'MOSEPICRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mostrar en  Epicrisis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVI1', @level2type = N'COLUMN', @level2name = N'MOSEPICRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVI1', @level2type = N'COLUMN', @level2name = N'MOSEPICRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicación de auditoría/destino formal del paciente: 1=Orden Hospitalización, 2=Urgencias, 3=Observación, 4=Cirugía, 5=Remitir, 6=Morgue, 7=Consulta Externa, 8=Salida. Tipo: NUMERIC(18). Búsqueda: destino, salida, egreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVI1', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicacion Paciente:  1: Orden de Hospitalizacion  2: Urgencias  3: Dejar en Observacion  4: Cirugia  5: Remitir  6: Morgue  7: Remitir a Consulta Externa  8: Salida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVI1', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVI1', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo o razón del traslado interno entre unidades funcionales o hacia otra institución. Tipo: VARCHAR(2000). Texto libre para documentar justificación clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVI1', @level2type = N'COLUMN', @level2name = N'MOTTRAINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivo Traslado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVI1', @level2type = N'COLUMN', @level2name = N'MOTTRAINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVI1', @level2type = N'COLUMN', @level2name = N'MOTTRAINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: True=Traslado interno a otra unidad funcional (cuando indicaciones 3 o 4); False=Traslado a otra IPS/institución. Tipo: BIT. Búsqueda: traslado, transferencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVI1', @level2type = N'COLUMN', @level2name = N'TRAINTUNI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Define si se ha realizado un Traslado Interno a otra Unidad, Cuando en las Indicaciones Medicas la enumeracion es 3 o 4.  True: Traslado Interno  False: Traslado a otra IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVI1', @level2type = N'COLUMN', @level2name = N'TRAINTUNI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVI1', @level2type = N'COLUMN', @level2name = N'TRAINTUNI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicaciones médicas generales dirigidas al paciente: instrucciones clínicas, cuidados, restricciones. Tipo: VARCHAR(MAX), MASKED. Búsqueda: instrucciones, prescripciones, recomendaciones médicas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVI1', @level2type = N'COLUMN', @level2name = N'INDICAMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicaciones Medicas Generales al Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVI1', @level2type = N'COLUMN', @level2name = N'INDICAMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVI1', @level2type = N'COLUMN', @level2name = N'INDICAMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Destino/condición del paciente al egreso: 1=Urgencias consulta externa, 2=Observación urgencias, 3=Hospitalización, 4=UCI Adulto, 5=UCI Pediátrica, 6=UCI Neonatal, 7=Consulta Externa, 8=Cirugía, 9=Hospitalización domiciliaria, 10=Referencia, 11=Morgue, 12=Salida, 13=Continúa en unidad. Tipo: CHAR(2). Búsqueda: destino, condición egreso, traslado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVI1', @level2type = N'COLUMN', @level2name = N'INDICAPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Define el destino del paciente  1. Trasladar a Urgencias: Solo consulta externa  2. Trasladar a Observacion Urgencias: solo Urgencias  3. Trasladar a Hospitalizacion: Dif Misma Unidad  4. Trasladar a  UCI Adulto: Dif Misma Unidad  5. Trasladar a UCI Pediatrica: Dif Misma Unidad  6. Trasladar a UCI Neonatal: Dif Misma Unidad  7. Trasladar a Consulta Externa: Dif Misma Unidad  8. Trasladar a  Cirugia: Dif Misma Unidad  9. Hospitalizacion en Casa  10. Referencia  11. Morgue  12. Salida  13. Continua en la Unidad  7: Morgue  8: Salida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVI1', @level2type = N'COLUMN', @level2name = N'INDICAPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVI1', @level2type = N'COLUMN', @level2name = N'INDICAPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Análisis clínico, impresión o valoración del paciente; texto libre con interpretación de síntomas, signos y estado general. Tipo: VARCHAR(MAX), MASKED. Búsqueda: análisis clínico, evaluación, impresión diagnóstica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVI1', @level2type = N'COLUMN', @level2name = N'ANALISISP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Analisis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVI1', @level2type = N'COLUMN', @level2name = N'ANALISISP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVI1', @level2type = N'COLUMN', @level2name = N'ANALISISP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora inicial de atención del paciente en la unidad funcional. Tipo: DATETIME. Marca el registro de ingreso/inicio de proceso asistencial. Búsqueda: fecha atención, ingreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVI1', @level2type = N'COLUMN', @level2name = N'FECINIATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Inicial de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVI1', @level2type = N'COLUMN', @level2name = N'FECINIATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVI1', @level2type = N'COLUMN', @level2name = N'FECINIATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional (FK: INUNIFUNC.UFUCODIGO). Identifica la unidad/servicio donde se registra la evolución. Tipo: CHAR(10). Búsqueda: unidad funcional, servicio, área clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVI1', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVI1', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVI1', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro/institución de atención (FK: ADCENATEN.CODCENATE). Identifica la sede o hospital donde se atiende. Tipo: CHAR(10). Búsqueda: centro atención, institución, hospital, clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVI1', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVI1', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVI1', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso del paciente (FK: ADINGRESO.NUMINGRES). Identifica único el episodio de atención/hospitalización. Tipo: CHAR(10). Búsqueda: número ingreso, episodio, admisión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVI1', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVI1', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVI1', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente (PK parcial, FK: INPACIENT.IPCODPACI). Identificador único: cédula, documento, equivalente a identificación del paciente. Tipo: VARCHAR(25), MASKED. Búsqueda: código paciente, cédula, documento, identificación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVI1', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVI1', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVI1', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud tratante (médico, cirujano, especialista). Tipo: CHAR(20), MASKED. Búsqueda: profesional, médico, especialista, código profesional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVI1', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVI1', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVI1', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo de la historia de ingreso; vincula cada evolución al folio base del episodio. Tipo: CHAR(10). Búsqueda: folio, historia clínica, evolución.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVI1', @level2type = N'COLUMN', @level2name = N'CONSFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la Historia de Ingreso a la que corresponde cada evolucion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVI1', @level2type = N'COLUMN', @level2name = N'CONSFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVI1', @level2type = N'COLUMN', @level2name = N'CONSFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio de la evolución clínica (PK parcial). Identificador único de cada nota/registro evolutivo dentro del ingreso. Tipo: NCHAR(10). Búsqueda: folio, evolución, nota clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVI1', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVI1', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVI1', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre/identificador interno del tipo de historia clínica (ej: evolución médica, nota de enfermería, interconsulta). Tipo: CHAR(9). Búsqueda: tipo historia, categoría nota, evolución.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVI1', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVI1', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVI1', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Notas de evolución clínica (folio de historia clínica) registradas por el profesional de salud durante un ingreso del paciente. Contiene el análisis clínico, indicaciones médicas, plan de manejo y datos del episodio de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVI1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVI1';
