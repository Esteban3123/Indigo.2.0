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
ALTER TABLE [dbo].[HCHISPACI] NOCHECK CONSTRAINT [FK_HCHISPACI_ADcenaten];


GO
ALTER TABLE [dbo].[HCHISPACI] NOCHECK CONSTRAINT [FK_HCHISPACI_ADINGRESO];


GO
ALTER TABLE [dbo].[HCHISPACI] NOCHECK CONSTRAINT [FK_HCHISPACI_INDIAGNOS];


GO
ALTER TABLE [dbo].[HCHISPACI] NOCHECK CONSTRAINT [FK_HCHISPACI_INPACIENT];


GO
ALTER TABLE [dbo].[HCHISPACI] NOCHECK CONSTRAINT [FK_HCHISPACI_INPROFSAL];


GO
ALTER TABLE [dbo].[HCHISPACI] NOCHECK CONSTRAINT [FK_HCHISPACI_INUNIFUNC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observacion la cual es el porque SE omite  la firma electrónica paciente y/o acudiente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'ObservationsOmitsElectronicSignature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relacion con la tabla: HCMOANULB en la cual se guarda el id del motivo del porque SE omite  la firma electrónica paciente y/o acudiente.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'ReasonOmitsElectronicSignature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Omitir firma electrónica paciente y/o acudiente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'OmitsElectronicSignature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'-- Sin_Definir = 0  -- Indicaciones_Atencion_Farmaceutica = 1   -- Evolucion_Valoracion_Salud_Visual = 2 ''rptHCEvolucion   -- Evolucion = 3 ''rptHCEvolucion   -- Evolucion_Valoracion_Seguimiento = 4 ''rptHCEvolucion   -- Ingreso = 5 ''rptHCIngreso  -- Nota_Evolucion_Rapida = 6 ''rptHCNotas  -- Junta_Medica = 7 ''rptHCNotas  -- Nota_Servicios_Apoyo = 8 ''rptHCServiciosApoyo  -- Nota_Otros_Procedimientos = 9 ''rptHCNotas  -- Ingreso_Indicaciones_Telefonicas = 10 ''rptHCNotas  -- Nota_Indicaciones_Telefonicas = 11 ''rptHCNotas  -- Ingreso_Informe_Quirurgico = 12 ''rptHCIngreso  -- Nota_Informe_QX = 13 ''rptHCNotas  -- Informe_QX = 14 ''rptHCNotas  -- Ingreso_Record_Anestesia = 15 ''rptHCIngreso  -- Nota_Record_Anestesia = 16 ''rptHCNotas  -- Ingreso_Consulta_Pre_Anestesia = 17 ''rptHCPreAnestesia  -- Nota_Consulta_Pre_Anestesia = 18 ''rptHCPreAnestesia  -- Referencia = 19 ''rptHCNotas  -- Atencion_Parto = 20 ''rptHCAtencionParto  -- Recien_Nacido = 21 ''rptHCRecienNacido  -- Extramural = 22 ''rptHCExtramural', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'StoryType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Me indica lo seleccionado por el medico en el plan manejo.    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'Reformulation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Profesional de consulta asistida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'AssistedConsultationProfessional';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Conciliación medicamentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'CONCILIACIONMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Junta médica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'JUNTAMEDICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con la tabla PRMODELOHC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'HCI Renal - Unidad Renal  Campo que se registra cuando el HCI sea de tipo Peritoneal.  1 -APD Diálisis Peritoneal Manual  2 - CAPD Diálisis Peritoneal Automatizada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'HCIRENALPERI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'HCI Renal - Unidad Renal  1 - Predialisis  2 - Peritoneal  3 - Hemodiálisis ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'HCIRENAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Historias Clinicas: Codigo de la Especialidad Tratante  Opcion valida solo para unidades funcionales de tipo Hospitalizacion - Se actualiza con Interconsultas. Muestra la especialidad acutal tratante, no se tiene opcion para guardar historico de especialidades tratantes, opcionalmente se puede obtener de HCHISPACI', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'CODESPTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indicacion Paciente:  1: Orden de Hospitalizacion  2: Urgencias  3: Dejar en Observacion  4: Cirugia  5: Remitir  6: Morgue  7: Remitir a Consulta Externa  8: Salida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valido solo para historias de Tipo Ingreso y define si la historia esta cerrada o abierta  A: Abierta  C: Cerrada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'ESTHISPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Define si se ha realizado un Traslado Interno a otra Unidad, Cuando en las Indicaciones Medicas la enumeracion es 3 o 4.  True: Traslado Interno  False: Traslado a otra IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'TRAINTUNI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Define el destino del paciente  1. Trasladar a Urgencias: Solo consulta externa  2. Trasladar a Observacion Urgencias: solo Urgencias  3. Trasladar a Hospitalizacion: Dif Misma Unidad  4. Trasladar a  UCI Adulto: Dif Misma Unidad  5. Trasladar a UCI Pediatrica: Dif Misma Unidad  6. Trasladar a UCI Neonatal: Dif Misma Unidad  7. Trasladar a Consulta Externa: Dif Misma Unidad  8. Trasladar a  Cirugia: Dif Misma Unidad  9. Hospitalizacion en Casa  10. Referencia  11. Morgue  12. Salida  13. Continua en la Unidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'INDICAPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Define si la historia es de Ingreso o Evolucion  I: Ingreso  E: Evolucion  O: Otros modelos de apoyo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACI', @level2type = N'COLUMN', @level2name = N'TIPHISPAC';

