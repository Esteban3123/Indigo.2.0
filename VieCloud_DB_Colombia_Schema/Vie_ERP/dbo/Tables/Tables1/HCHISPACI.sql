CREATE TABLE [dbo].[HCHISPACI] (
    [IDETIPHIS]                            CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO]                            NCHAR (10)                                                                       NOT NULL,
    [CONSFOLIO]                            CHAR (10)                                                                        NULL,
    [IPCODPACI]                            VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]                            CHAR (10)                                                                        NOT NULL,
    [CODCENATE]                            CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]                            CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]                            CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECHISPAC]                            DATETIME                                                                         NOT NULL,
    [TIPHISPAC]                            CHAR (2)                                                                         NOT NULL,
    [INDICAPAC]                            CHAR (2)                                                                         NOT NULL,
    [INDICAMED]                            VARCHAR (MAX)                                                                    NULL,
    [TRAINTUNI]                            BIT                                                                              NOT NULL,
    [MOTTRAINT]                            VARCHAR (2000)                                                                   NULL,
    [ESTHISPAC]                            CHAR (1)                                                                         NOT NULL,
    [CODDIAGNO]                            CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')     NOT NULL,
    [DATSUBJET]                            VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "DataSubjetive_Ofuscado", 0)') NULL,
    [DATOBJETI]                            VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "DataObjetive_Ofuscado", 0)')  NULL,
    [DATPRONOS]                            VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "DataForecast_Ofuscado", 0)')  NULL,
    [DATTRATAM]                            VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "DataTreatment_Ofuscado", 0)') NULL,
    [INDAUDFOR]                            NUMERIC (18)                                                                     NOT NULL,
    [CODUSUARI]                            CHAR (20)                                                                        NULL,
    [FECVISREG]                            DATETIME                                                                         NULL,
    [MOSEPICRI]                            BIT                                                                              NULL,
    [ESTAFOLIO]                            BIT                                                                              NULL,
    [CODESPTRA]                            CHAR (3)                                                                         NULL,
    [GENCONEXT]                            BIT                                                                              NULL,
    [EVALIDADO]                            BIT                                                                              NULL,
    [PLAINDMED]                            VARCHAR (MAX)                                                                    NULL,
    [HCIRENAL]                             INT                                                                              NULL,
    [HCIRENALPERI]                         INT                                                                              NULL,
    [IDMODELOHC]                           INT                                                                              NULL,
    [JUNTAMEDICA]                          BIT                                                                              CONSTRAINT [DF__HCHISPACI__JUNTA__097AC230] DEFAULT ((0)) NOT NULL,
    [CONCILIACIONMED]                      BIT                                                                              NULL,
    [AssistedConsultationProfessional]     CHAR (20)                                                                        NULL,
    [Reformulation]                        BIT                                                                              NULL,
    [StoryType]                            INT                                                                              NULL,
    [OmitsElectronicSignature]             BIT                                                                              NULL,
    [ReasonOmitsElectronicSignature]       CHAR (4)                                                                         NULL,
    [ObservationsOmitsElectronicSignature] VARCHAR (200)                                                                    NULL,
    CONSTRAINT [PK_HCHISPACI] PRIMARY KEY CLUSTERED ([NUMEFOLIO] ASC, [IPCODPACI] ASC),
    CONSTRAINT [FK_HCHISPACI_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCHISPACI_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCHISPACI_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_HCHISPACI_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCHISPACI_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCHISPACI_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCHISPACI].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCHISPACI].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCHISPACI].[CODDIAGNO]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCHISPACI].[DATSUBJET]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCHISPACI].[DATOBJETI]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCHISPACI].[DATPRONOS]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCHISPACI].[DATTRATAM]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
CREATE NONCLUSTERED INDEX [IX_HCHISPACI_1]
    ON [dbo].[HCHISPACI]([IPCODPACI] ASC, [NUMINGRES] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones/notas explicativas del motivo por el cual se omite la firma electrónica del paciente y/o acudiente. VARCHAR(200), contiene PII sensible.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'ObservationsOmitsElectronicSignature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observacion la cual es el porque SE omite  la firma electrónica paciente y/o acudiente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'ObservationsOmitsElectronicSignature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'ObservationsOmitsElectronicSignature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de motivo (FK a HCMOANULB) que referencia por qué se omite la firma electrónica del paciente y/o acudiente. CHAR(4), identificador de catálogo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'ReasonOmitsElectronicSignature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con la tabla: HCMOANULB en la cual se guarda el id del motivo del porque SE omite  la firma electrónica paciente y/o acudiente.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'ReasonOmitsElectronicSignature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'ReasonOmitsElectronicSignature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano que registra si se omite la firma electrónica del paciente y/o acudiente en el folio de historia clínica. BIT, valores: 1=Sí, 0=No.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'OmitsElectronicSignature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Omitir firma electrónica paciente y/o acudiente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'OmitsElectronicSignature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'OmitsElectronicSignature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de historia clínica: clasificación numérica (0-22) que define el modelo de documento (Ingreso, Evolución, Junta Médica, Informe QX, Pre-anestesia, Parto, etc.). INT, determina plantilla de reporte.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'StoryType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'-- Sin_Definir = 0  -- Indicaciones_Atencion_Farmaceutica = 1   -- Evolucion_Valoracion_Salud_Visual = 2 ''''rptHCEvolucion   -- Evolucion = 3 ''''rptHCEvolucion   -- Evolucion_Valoracion_Seguimiento = 4 ''''rptHCEvolucion   -- Ingreso = 5 ''''rptHCIngreso  -- Nota_Evolucion_Rapida = 6 ''''rptHCNotas  -- Junta_Medica = 7 ''''rptHCNotas  -- Nota_Servicios_Apoyo = 8 ''''rptHCServiciosApoyo  -- Nota_Otros_Procedimientos = 9 ''''rptHCNotas  -- Ingreso_Indicaciones_Telefonicas = 10 ''''rptHCNotas  -- Nota_Indicaciones_Telefonicas = 11 ''''rptHCNotas  -- Ingreso_Informe_Quirurgico = 12 ''''rptHCIngreso  -- Nota_Informe_QX = 13 ''''rptHCNotas  -- Informe_QX = 14 ''''rptHCNotas  -- Ingreso_Record_Anestesia = 15 ''''rptHCIngreso  -- Nota_Record_Anestesia = 16 ''''rptHCNotas  -- Ingreso_Consulta_Pre_Anestesia = 17 ''''rptHCPreAnestesia  -- Nota_Consulta_Pre_Anestesia = 18 ''''rptHCPreAnestesia  -- Referencia = 19 ''''rptHCNotas  -- Atencion_Parto = 20 ''''rptHCAtencionParto  -- Recien_Nacido = 21 ''''rptHCRecienNacido  -- Extramural = 22 ''''rptHCExtramural', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'StoryType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'StoryType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano que registra si el médico seleccionó reformulación en el plan de manejo/tratamiento. BIT, 1=Sí reformula, 0=No.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'Reformulation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Me indica lo seleccionado por el medico en el plan manejo.    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'Reformulation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'Reformulation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de la salud que participa en consulta asistida o interconsulta (FK a INPROFSAL). CHAR(20), identificación PII del médico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'AssistedConsultationProfessional';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Profesional de consulta asistida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'AssistedConsultationProfessional';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'AssistedConsultationProfessional';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano de conciliación de medicamentos realizada durante la atención. BIT, 1=Conciliado, 0=No conciliado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'CONCILIACIONMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Conciliación medicamentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'CONCILIACIONMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'CONCILIACIONMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano que señala si la historia corresponde a sesión de junta médica/multidisciplinaria. BIT, 1=Sí es junta, 0=No.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'JUNTAMEDICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Junta médica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'JUNTAMEDICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'JUNTAMEDICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de relación con tabla PRMODELOHC; referencia el modelo/plantilla de historia clínica utilizado. INT, FK a modelo de historia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relación con la tabla PRMODELOHC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación de diálisis peritoneal en unidad renal: 1=APD (Diálisis Peritoneal Manual), 2=CAPD (Diálisis Peritoneal Automatizada). INT, válido solo cuando HCIRENAL=2.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'HCIRENALPERI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'HCI Renal - Unidad Renal  Campo que se registra cuando el HCI sea de tipo Peritoneal.  1 -APD Diálisis Peritoneal Manual  2 - CAPD Diálisis Peritoneal Automatizada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'HCIRENALPERI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'HCIRENALPERI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación de tipo de atención en unidad renal: 1=Predialisis, 2=Peritoneal, 3=Hemodiálisis. INT, especifica procedimiento renal registrado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'HCIRENAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'HCI Renal - Unidad Renal  1 - Predialisis  2 - Peritoneal  3 - Hemodiálisis ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'HCIRENAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'HCIRENAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Plantilla/contenido de indicaciones médicas generales predefinidas aplicadas al paciente. VARCHAR(MAX), texto estructurado de órdenes médicas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'PLAINDMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Plantilla de Indicaciones medicas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'PLAINDMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'PLAINDMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano que verifica si la historia clínica fue validada/revisada por el especialista tratante. BIT, 1=Validado, 0=Pendiente validación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'EVALIDADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Determina si la historia se valido por el especialista', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'EVALIDADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'EVALIDADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano de historia generada desde consulta externa; marca cabecera en dashboard médico. BIT, 1=Generada desde consulta externa, 0=No.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'GENCONEXT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Historia Generada desde consulta externa siendo control para que salga como cabecera en el dashboard medicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'GENCONEXT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'GENCONEXT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de especialidad tratante actual de la unidad funcional (válido para hospitalización). CHAR(3), se actualiza con interconsultas; muestra especialidad actual sin histórico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'CODESPTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Historias Clinicas: Codigo de la Especialidad Tratante  Opcion valida solo para unidades funcionales de tipo Hospitalizacion - Se actualiza con Interconsultas. Muestra la especialidad acutal tratante, no se tiene opcion para guardar historico de especialidades tratantes, opcionalmente se puede obtener de HCHISPACI', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'CODESPTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'CODESPTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del folio de historia clínica. BIT, 1=Activo, 0=Inactivo. Controla visibilidad y edición del registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'ESTAFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del Folio Activo=1;Inactivo=0', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'ESTAFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'ESTAFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano que determina si el folio se muestra en el documento de epicrisis (resumen de egreso). BIT, 1=Mostrar, 0=No mostrar.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'MOSEPICRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mostrar en  Epicrisis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'MOSEPICRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'MOSEPICRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que el usuario visualizó/consultó el registro de historia clínica. DATETIME, auditoría de acceso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'FECVISREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Visualizacion del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'FECVISREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'FECVISREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador del usuario (profesional) que visualizó/abrió el registro. CHAR(20), auditoría de acceso, FK a tabla usuarios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del usuario que visualizo el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'CODUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicación de auditoria/destino final del paciente: 1=Hospitalización, 2=Urgencias, 3=Observación, 4=Cirugía, 5=Remisión, 6=Morgue, 7=Consulta Externa, 8=Salida. NUMERIC(18).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicacion Paciente:  1: Orden de Hospitalizacion  2: Urgencias  3: Dejar en Observacion  4: Cirugia  5: Remitir  6: Morgue  7: Remitir a Consulta Externa  8: Salida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Datos de tratamiento/plan terapéutico capturados en la evaluación SOAP de historia clínica. VARCHAR(MAX), texto PII sensible (medicinas, dosis, instrucciones).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'DATTRATAM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que especifica los datos correspondientes al tratamiento capturada en la Historia Clinica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'DATTRATAM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'DATTRATAM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Datos de pronóstico/evolución esperada capturados en la evaluación SOAP de historia clínica. VARCHAR(MAX), datos clínicos sensibles.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'DATPRONOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que especifica los datos correspondientes al pronostico capturada en la Historia Clinica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'DATPRONOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'DATPRONOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Datos de exploración física/hallazgos objetivos capturados en la evaluación SOAP de historia clínica. VARCHAR(MAX), signos vitales y examen clínico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'DATOBJETI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que especifica los datos correspondientes a la evaluacion Objetiva capturada en la Historia Clinica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'DATOBJETI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'DATOBJETI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Datos de relato/síntomas subjetivos del paciente capturados en la evaluación SOAP de historia clínica. VARCHAR(MAX), narrativa clínica PII.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'DATSUBJET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que especifica los datos correspondientes a la evaluacion Subjetiva capturada en la Historia Clinica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'DATSUBJET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'DATSUBJET';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del diagnóstico principal/primario asignado al paciente en la historia (FK a INDIAGNOS). CHAR(4), diagnóstico PII ofuscado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la historia clínica de ingreso: A=Abierta (en evolución), C=Cerrada (completa). CHAR(1), válido solo para historias de tipo Ingreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'ESTHISPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valido solo para historias de Tipo Ingreso y define si la historia esta cerrada o abierta  A: Abierta  C: Cerrada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'ESTHISPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'ESTHISPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción/motivo del traslado interno del paciente entre unidades funcionales o a otra institución. VARCHAR(2000), narrativa clínica de justificación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'MOTTRAINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivo del traslado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'MOTTRAINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'MOTTRAINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano que marca si hubo traslado interno a otra unidad funcional. BIT, 1=Traslado interno, 0=Traslado a otra IPS (con indicaciones médicas 3 o 4).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'TRAINTUNI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Define si se ha realizado un Traslado Interno a otra Unidad, Cuando en las Indicaciones Medicas la enumeracion es 3 o 4.  True: Traslado Interno  False: Traslado a otra IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'TRAINTUNI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'TRAINTUNI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicaciones médicas generales de manejo capturadas para el paciente en la historia clínica. VARCHAR(MAX), órdenes médicas PII sensibles.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'INDICAMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicaciones Medicas Generales al Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'INDICAMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'INDICAMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de destino/disposición del paciente al cierre de atención: 1=Urgencias, 2=Observación, 3=Hospitalización, 4=UCI Adulto, 5=UCI Pediátrica, 6=UCI Neonatal, 7=Consulta Externa, 8=Cirugía, 9=Hospitalización en Casa, 10=Referencia, 11=Morgue, 12=Salida, 13=Continúa. CHAR(2).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'INDICAPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Define el destino del paciente  1. Trasladar a Urgencias: Solo consulta externa  2. Trasladar a Observacion Urgencias: solo Urgencias  3. Trasladar a Hospitalizacion: Dif Misma Unidad  4. Trasladar a  UCI Adulto: Dif Misma Unidad  5. Trasladar a UCI Pediatrica: Dif Misma Unidad  6. Trasladar a UCI Neonatal: Dif Misma Unidad  7. Trasladar a Consulta Externa: Dif Misma Unidad  8. Trasladar a  Cirugia: Dif Misma Unidad  9. Hospitalizacion en Casa  10. Referencia  11. Morgue  12. Salida  13. Continua en la Unidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'INDICAPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'INDICAPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de historia clínica: I=Ingreso (inicial), E=Evolución (seguimiento), O=Otros modelos de apoyo. CHAR(2), clasifica origen y propósito del folio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'TIPHISPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Define si la historia es de Ingreso o Evolucion  I: Ingreso  E: Evolucion  O: Otros modelos de apoyo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'TIPHISPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'TIPHISPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación/registro de la historia clínica del paciente. DATETIME, marca temporal de atención/evento clínico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'FECHISPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Historia Clinica Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'FECHISPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'FECHISPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador del profesional de la salud (médico, enfermero, etc.) que generó la historia clínica (FK a INPROFSAL). VARCHAR(25), PII ofuscado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional donde se generó la historia clínica (FK a INUNIFUNC). CHAR(10), departamento/área de atención (Hospitalización, UCI, Urgencias, etc.).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro/institución de atención donde se registra la historia clínica (FK a ADCENATEN). CHAR(10), identificador de sede/hospital.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso/admission del paciente al que pertenece esta historia clínica (FK a ADINGRESO). CHAR(10), clave de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador único del paciente (cédula/documento/identificación equivalente) (FK a INPACIENT). VARCHAR(25), PII principal ofuscado con máscara.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número consecutivo/correlativo de evoluciones dentro de un ingreso para rastrear historial de folios. CHAR(10), secuenciador de cambios en la historia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'CONSFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de Evoluciones de la Historia Clinica de Ingreso del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'CONSFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'CONSFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número único de folio/episodio de historia clínica (clave primaria). NCHAR(10), identificador universal del documento de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre/tipo interno de la historia clínica (categoría de clasificación). CHAR(9), etiqueta de clasificación del modelo de documento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Historia clínica de pacientes por ingreso (folio de atención). Registra cada consulta o evolución médica documentada: datos subjetivos, objetivos, diagnóstico, tratamiento, plan y pronóstico del paciente durante su atención en un centro de salud. Es el núcleo del registro clínico electrónico (HCE) del sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI';
