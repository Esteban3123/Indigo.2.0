CREATE TABLE [dbo].[HCNOTEVO1] (
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
    [INDICAMED] VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "instructions_Ofuscado", 0)')  NOT NULL,
    [TRAINTUNI] BIT                                                                              NOT NULL,
    [MOTTRAINT] VARCHAR (2000)                                                                   NULL,
    [INDAUDFOR] NUMERIC (18)                                                                     NOT NULL,
    [MOSEPICRI] BIT                                                                              NULL,
    [CODESPTRA] CHAR (3)                                                                         NULL,
    [PLAINDMED] VARCHAR (MAX)                                                                    NULL,
    [FECHINIHI] DATETIME                                                                         NULL,
    [ID]        INT                                                                              IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK_HCNOTEVO1] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCNOTEVO1_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCNOTEVO1_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCNOTEVO1_INESPECIA] FOREIGN KEY ([CODESPTRA]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_HCNOTEVO1_INPacient] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCNOTEVO1_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCNOTEVO1_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCNOTEVO1] NOCHECK CONSTRAINT [FK_HCNOTEVO1_INPROFSAL];


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCNOTEVO1].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCNOTEVO1].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCNOTEVO1].[ANALISISP]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCNOTEVO1].[INDICAMED]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');




GO
ALTER TABLE [dbo].[HCNOTEVO1] NOCHECK CONSTRAINT [FK_HCNOTEVO1_INPROFSAL];


GO
ALTER TABLE [dbo].[HCNOTEVO1] NOCHECK CONSTRAINT [FK_HCNOTEVO1_INPROFSAL];


GO



GO
ALTER TABLE [dbo].[HCNOTEVO1] NOCHECK CONSTRAINT [FK_HCNOTEVO1_INPROFSAL];


GO



GO
CREATE NONCLUSTERED INDEX [IX_HCNOTEVO1_IPCODPACI_ANALISISP_CODPROSAL_FECINIATE_NUMINGRES]
    ON [dbo].[HCNOTEVO1]([IPCODPACI] ASC)
    INCLUDE([ANALISISP], [CODPROSAL], [FECINIATE], [NUMINGRES]);


GO
ALTER INDEX [IX_HCNOTEVO1_IPCODPACI_ANALISISP_CODPROSAL_FECINIATE_NUMINGRES]
    ON [dbo].[HCNOTEVO1] DISABLE;




GO
CREATE NONCLUSTERED INDEX [IX_HCNOTEVO1_UNIQUE]
    ON [dbo].[HCNOTEVO1]([IPCODPACI] ASC, [NUMEFOLIO] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (IDENTITY, INT). Consecutivo automático de la tabla HCNOTEVO1 para evoluciones clínicas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de inicialización de la historia clínica (DATETIME). Marca el inicio del registro de evolución del paciente en la atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1', @level2type = N'COLUMN', @level2name = N'FECHINIHI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la Inicalizacion de la Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1', @level2type = N'COLUMN', @level2name = N'FECHINIHI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1', @level2type = N'COLUMN', @level2name = N'FECHINIHI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Plantilla de indicaciones médicas (VARCHAR MAX). Contiene el formato predefinido o texto de órdenes/instrucciones médicas generales al paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1', @level2type = N'COLUMN', @level2name = N'PLAINDMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Plantilla de Indicaciones medicas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1', @level2type = N'COLUMN', @level2name = N'PLAINDMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1', @level2type = N'COLUMN', @level2name = N'PLAINDMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de especialidad tratante (CHAR 3, FK→INESPECIA). Especialidad médica actual que atiende al paciente. Válido solo en hospitalización; se actualiza con interconsultas. PII ofuscado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1', @level2type = N'COLUMN', @level2name = N'CODESPTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Historias Clinicas: Codigo de la Especialidad Tratante  Opcion valida solo para unidades funcionales de tipo Hospitalizacion - Se actualiza con Interconsultas. Muestra la especialidad acutal tratante, no se tiene opcion para guardar historico de especialidades tratantes, opcionalmente se puede obtener de HCHISPACA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1', @level2type = N'COLUMN', @level2name = N'CODESPTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1', @level2type = N'COLUMN', @level2name = N'CODESPTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador para mostrar en epicrisis (BIT). Bandera que determina si la evolución debe incluirse en el resumen final de la atención (epicrisis).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1', @level2type = N'COLUMN', @level2name = N'MOSEPICRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mostrar en  Epicrisis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1', @level2type = N'COLUMN', @level2name = N'MOSEPICRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1', @level2type = N'COLUMN', @level2name = N'MOSEPICRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicación de auditoría o disposición final (NUMERIC 18). Códigos: 1=Hospitalización, 2=Urgencias, 3=Observación, 4=Cirugía, 5=Remisión, 6=Morgue, 7=Consulta Externa, 8=Salida.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicacion Paciente:  1: Orden de Hospitalizacion  2: Urgencias  3: Dejar en Observacion  4: Cirugia  5: Remitir  6: Morgue  7: Remitir a Consulta Externa  8: Salida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo del traslado interno (VARCHAR 2000). Descripción textual de la razón del movimiento del paciente entre unidades funcionales o centros de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1', @level2type = N'COLUMN', @level2name = N'MOTTRAINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivo Traslado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1', @level2type = N'COLUMN', @level2name = N'MOTTRAINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1', @level2type = N'COLUMN', @level2name = N'MOTTRAINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de traslado interno (BIT). True=Traslado interno a otra unidad funcional (códigos 3 o 4 en indicaciones). False=Traslado a otra IPS/Centro externo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1', @level2type = N'COLUMN', @level2name = N'TRAINTUNI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Define si se ha realizado un Traslado Interno a otra Unidad, Cuando en las Indicaciones Medicas la enumeracion es 3 o 4.  True: Traslado Interno  False: Traslado a otra IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1', @level2type = N'COLUMN', @level2name = N'TRAINTUNI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1', @level2type = N'COLUMN', @level2name = N'TRAINTUNI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicaciones médicas generales (VARCHAR MAX, ofuscado). Órdenes, instrucciones y prescripciones médicas dirigidas al paciente. PII/datos clínicos protegidos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1', @level2type = N'COLUMN', @level2name = N'INDICAMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicaciones Medicas Generales al Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1', @level2type = N'COLUMN', @level2name = N'INDICAMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1', @level2type = N'COLUMN', @level2name = N'INDICAMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de destino o disposición del paciente (CHAR 2). Códigos 1-16: Urgencias, Observación, Hospitalización, UCI (Adulto/Pediátrica/Neonatal), Cirugía, Casa, Referencia, Morgue, Salida, Permanencia, Retiro Voluntario, Fuga.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1', @level2type = N'COLUMN', @level2name = N'INDICAPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Define el destino del paciente  1. Trasladar a Urgencias: Solo consulta externa  2. Trasladar a Observacion Urgencias: solo Urgencias  3. Trasladar a Hospitalizacion: Dif Misma Unidad  4. Trasladar a  UCI Adulto: Dif Misma Unidad  5. Trasladar a UCI Pediatrica: Dif Misma Unidad  6. Trasladar a UCI Neonatal: Dif Misma Unidad  7. Trasladar a Consulta Externa: Dif Misma Unidad  8. Trasladar a  Cirugia: Dif Misma Unidad  9. Hospitalizacion en Casa  10. Referencia  11. Morgue  12. Salida  13. Continua en la Unidad  15. Retiro Voluntario  16. Fuga', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1', @level2type = N'COLUMN', @level2name = N'INDICAPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1', @level2type = N'COLUMN', @level2name = N'INDICAPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Análisis o evaluación clínica (VARCHAR MAX, ofuscado). Texto con análisis, interpretación clínica o notas de evaluación del paciente. Datos clínicos PII.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1', @level2type = N'COLUMN', @level2name = N'ANALISISP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Analisis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1', @level2type = N'COLUMN', @level2name = N'ANALISISP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1', @level2type = N'COLUMN', @level2name = N'ANALISISP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora inicial de atención (DATETIME). Marca el inicio de la evolución clínica en la unidad funcional durante el ingreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1', @level2type = N'COLUMN', @level2name = N'FECINIATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Inicial de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1', @level2type = N'COLUMN', @level2name = N'FECINIATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1', @level2type = N'COLUMN', @level2name = N'FECINIATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de unidad funcional (CHAR 10, FK→INUNIFUNC). Identifica la unidad funcional (hospitalización, urgencias, UCI, etc.) donde se realiza la atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención (CHAR 10, FK→ADCENATEN). Identifica la institución o centro de salud (IPS) donde se registra la evolución.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del ingreso (CHAR 10, FK→ADINGRESO). Identificador único del episodio o admisión del paciente en la institución.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente (VARCHAR 25, FK→INPACIENT). Identificador único del paciente en el sistema, equivalente a cédula/documento de identidad. PII ofuscado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud (CHAR 20, FK→INPROFSAL). Identifica al médico, enfermero o profesional que registra la evolución. PII ofuscado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo de historia de ingreso (CHAR 10). Número secuencial que vincula cada evolución clínica a su historia de ingreso padre.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1', @level2type = N'COLUMN', @level2name = N'CONSFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la Historia de Ingreso a la que corresponde cada evolucion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1', @level2type = N'COLUMN', @level2name = N'CONSFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1', @level2type = N'COLUMN', @level2name = N'CONSFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio (NCHAR 10). Identificador de la hoja o folio individual de la historia clínica dentro del ingreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre interno de tipo de historia (CHAR 9). Clasificación interna del tipo o categoría de historia clínica (evolución, nota inicial, etc.).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Notas de evolución clínica (folio de evolución) registradas por profesionales de la salud durante el ingreso del paciente. Contiene el análisis clínico, indicaciones médicas, plan de manejo e información de auditoría de cada nota evolutiva en historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOTEVO1';
