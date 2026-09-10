CREATE TABLE [EHR].[HCORDQUIMIO] (
    [ID]                    INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [SchemesId]             INT                                                                              NOT NULL,
    [MANEJOEXTERNO]         BIT                                                                              NOT NULL,
    [ESTADO]                INT                                                                              NOT NULL,
    [IPCODPACI]             VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]             CHAR (10)                                                                        NOT NULL,
    [NUMEFOLIO]             CHAR (10)                                                                        NOT NULL,
    [CODDIAGNO]             CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')     NOT NULL,
    [CICLOS]                INT                                                                              NOT NULL,
    [CICLOACTUAL]           INT                                                                              NOT NULL,
    [CODCENATE]             CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]             CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]             CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [CODESPECI]             CHAR (3)                                                                         NOT NULL,
    [ORDENCONCITA]          BIT                                                                              NOT NULL,
    [FECHAREGISTRO]         DATETIME                                                                         NOT NULL,
    [46]                    INT                                                                              NULL,
    [FASEQUIMIOTERAPIA]     INT                                                                              NULL,
    [48]                    INT                                                                              NULL,
    [MOTIVOFINALIZAR]       INT                                                                              NULL,
    [OBSERVACIONFINALIZAR]  VARCHAR (MAX)                                                                    NULL,
    [FECHAFINALIZA]         DATETIME                                                                         NULL,
    [USUFINALIZA]           CHAR (20)                                                                        NULL,
    [ULTIMOCICLOAUTORIZADO] INT                                                                              CONSTRAINT [DF_HCORDQUIMIO_ULTIMOCICLOAUTORIZADO] DEFAULT ((0)) NOT NULL,
    [IDHCMOANULB]           CHAR (4)                                                                         NULL,
    [FECHAANULACION]        DATETIME                                                                         NULL,
    [OBSERVACIONANULACION]  VARCHAR (MAX)                                                                    NULL,
    [USUARIOANULA]          VARCHAR (20)                                                                     NULL,
    [IDHCMOANULB_SUSP]      CHAR (4)                                                                         NULL,
    [NUMFOLSUS]             CHAR (10)                                                                        NULL,
    CONSTRAINT [PK_HCORDQUI] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCORDQUI_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCORDQUI_HCQUIORDENC] FOREIGN KEY ([ID]) REFERENCES [EHR].[HCORDQUIMIO] ([ID]),
    CONSTRAINT [FK_HCORDQUI_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_HCORDQUI_Schemes] FOREIGN KEY ([SchemesId]) REFERENCES [EHR].[Schemes] ([Id]),
    CONSTRAINT [FK_HCORDQUIMIO_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCORDQUIMIO_HCMOANULB] FOREIGN KEY ([IDHCMOANULB_SUSP]) REFERENCES [dbo].[HCMOANULB] ([CODMOTANU]),
    CONSTRAINT [FK_HCORDQUIMIO_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_HCORDQUIMIO_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_HCORDQUIMIO_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCORDQUIMIO_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [EHR].[HCORDQUIMIO].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [EHR].[HCORDQUIMIO].[CODDIAGNO]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [EHR].[HCORDQUIMIO].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');




GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio donde se suspende el esquema de quimioterapia. Referencia al folio de suspensión del tratamiento oncológico.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'NUMFOLSUS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Folio en el que se suspende el esquema', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'NUMFOLSUS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'NUMFOLSUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del motivo de suspensión del esquema quimioterápico. FK a tabla de motivos de anulación/suspensión (HCMOANULB).', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'IDHCMOANULB_SUSP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Id motivo Suspencion', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'IDHCMOANULB_SUSP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'IDHCMOANULB_SUSP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (profesional de salud) que registra la anulación de la orden de quimioterapia. Varchar(20), PII sensible.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'USUARIOANULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Usuario que anula', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'USUARIOANULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'USUARIOANULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones o notas descriptivas sobre las causas y detalles de la anulación de la orden quimioterápica.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'OBSERVACIONANULACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Obseravcion de la Anulacion', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'OBSERVACIONANULACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'OBSERVACIONANULACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se registra la anulación de la orden de quimioterapia en el sistema.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'FECHAANULACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la Fecha de Anulacion', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'FECHAANULACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'FECHAANULACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del motivo por el cual se anula la orden quimioterápica. FK a HCMOANULB.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'IDHCMOANULB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del motivo anulacion', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'IDHCMOANULB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'IDHCMOANULB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del último ciclo de quimioterapia que ha sido autorizado/aprobado para el paciente. INT, default=0.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'ULTIMOCICLOAUTORIZADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Me Identifica el Ultimo Ciclo Autorizado', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'ULTIMOCICLOAUTORIZADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'ULTIMOCICLOAUTORIZADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (profesional de salud) que registra la finalización prematura del esquema quimioterápico.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'USUFINALIZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario finaliza prematuro', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'USUFINALIZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'USUFINALIZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de registro de la finalización prematura del esquema de quimioterapia.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'FECHAFINALIZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Finalizar prematuro', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'FECHAFINALIZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'FECHAFINALIZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Notas y observaciones que justifican la finalización anticipada del tratamiento quimioterápico.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'OBSERVACIONFINALIZAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observacion de finalizar prematuro', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'OBSERVACIONFINALIZAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'OBSERVACIONFINALIZAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del motivo de suspensión o finalización prematura del esquema (1=Toxicidad, 2=Médicos, 3=Muerte, 4=Cambio EAPB, 5=Decisión usuario, 6=No disponibilidad medicamentos, 7=Administrativos, 8=Otros). Requerido para Cuenta de Alto Costo (CAC).', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'MOTIVOFINALIZAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivos del finalizar prematuro O SUSPENCION DEL ESQUEMA.    Motivos de la Suspensión, ese campo es quemado desde el formulario.    Esta Respuesta llena a la pregunta 60 de la Cuenta de Alto Costo.    1=Toxicidad de uno o más medicamentos    2=Otros motivos médicos    3=Muerte    4= Cambio de EAPB    5=Decisión del usuario    6=No hay disponibilidad de medicamentos    7=Otros motivos administrativos    8=Otras causas no contempladas  ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'MOTIVOFINALIZAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'MOTIVOFINALIZAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo/intención del tratamiento quimioterápico: neoadyuvancia, curativo sin cirugía, adyuvancia, paliativo, recaída (1ª/2ª/3ª+). Clasificación clínica oncológica.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'48';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1=Neoadyuvancia (manejo  inicial prequirúrgico)  2= Tratamiento inicial curativo sin cirugía sugerida  3=Adyuvancia(manejo inicial postquirúrgico)  4=Manejo paliativo inicial  5=Manejo curativo de primera recaída  6=Manejo paliativo de primera recaída  7=Manejo curativo de segunda recaída  8=Manejo paliativo de segunda recaída  9=Manejo curativo de tercera recaída o posterior  10=Manejo paliativo de tercera recaída o posterior', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'48';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'48';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fase del esquema quimioterápico: prefase/citorreducción, inducción, intensificación, consolidación, reinducción, mantenimiento, mantenimiento largo o final, otra fase.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'FASEQUIMIOTERAPIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1-Prefase o citorreducción inicial  2-Inducción  3-Intensificación  4-Consolidación  5-Reinducción  6-Mantenimiento  7-Mantenimiento largo o final  8-Otra Fase', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'FASEQUIMIOTERAPIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'FASEQUIMIOTERAPIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad/número total de fases de quimioterapia recibidas por el paciente en el período de reporte.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'46';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿Cuántas fases de quimioterapia recibió el usuario en este periodo de reporte?', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'46';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'46';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de registro/creación de la orden de quimioterapia en el sistema.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la fecha de registro ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: 1=Existe orden de cita asociada a la orden quimioterápica, 0=No existe orden de cita.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'ORDENCONCITA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si tiene orden de la cita  1 = true  0 = false ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'ORDENCONCITA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'ORDENCONCITA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la especialidad médica (oncología) con la que se firma/autoriza el folio de quimioterapia. FK a INESPECIA.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Especialidad con la que se firma el folio', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud (oncólogo, médico tratante) que ordena/autoriza el esquema quimioterápico. FK a INPROFSAL. PII Ofuscado.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el codigo del profesional', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional (oncología, hematología, etc.) donde se administra la quimioterapia. FK a INUNIFUNC.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el codigo unidad funcional', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro/institución de atención donde se realiza el tratamiento quimioterápico. FK a ADCENATEN.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el codigo centro atención', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del ciclo quimioterápico actual en el que se encuentra el paciente dentro del esquema.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'CICLOACTUAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ciclo Actual en el que va el paciente', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'CICLOACTUAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'CICLOACTUAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número total de ciclos programados/planificados en el esquema quimioterápico del paciente.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'CICLOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Total de Ciclos en el Esquema', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'CICLOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'CICLOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código diagnóstico (CIE-10 u otra codificación) del tipo de cáncer/neoplasia maligna. FK a INDIAGNOS. PII Ofuscado.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el codigo del diagnostico', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número único del folio/documento de la orden de quimioterapia. Char(10).', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el numero de folio', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso/admisión del paciente en el sistema de urgencias/hospitalización. FK a ADINGRESO.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el numero ingreso', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código/identificación del paciente (cédula, documento de identidad). PII Ofuscado con máscara Identification_Ofuscado. FK a INPACIENT.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el codigo del paciente', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la orden quimioterápica: 1=Solicitada, 2=Iniciada, 3=Finalizada completa, 4=Suspendida/Finalización prematura, 5=Anulada sin iniciación.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Orden Solicitada  2 - Esquema iniciado  3 - Esquema finalizado completo  4 - Suspendido - Finalización prematura.  5 - Anulado - Cuando no se ha iniciado tratamiento', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: 1=La orden de quimioterapia es de manejo externo (extrainstitucional), 0=Manejo interno institucional.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'MANEJOEXTERNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'True - Si es manejo externo la orden.  False - No es de manejo externo la orden.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'MANEJOEXTERNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'MANEJOEXTERNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del esquema quimioterápico (plan de tratamiento) al que pertenece la orden. FK a EHR.Schemes.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'SchemesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Id de esquemas', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'SchemesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'SchemesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK) de la orden de quimioterapia. Identity INT autoincremental.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'ID';


GO
CREATE NONCLUSTERED INDEX [IX_HCORDQUIMIO_ConsultaOnco]
    ON [EHR].[HCORDQUIMIO]([FECHAREGISTRO] DESC, [ESTADO] ASC, [CODCENATE] ASC)
    INCLUDE([ID], [CICLOACTUAL], [CICLOS], [IPCODPACI], [NUMINGRES], [ULTIMOCICLOAUTORIZADO], [SchemesId], [CODDIAGNO], [UFUCODIGO], [CODPROSAL], [CODESPECI], [NUMEFOLIO], [ORDENCONCITA]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Órdenes de quimioterapia de pacientes hospitalizados o ambulatorios. Registra los ciclos de tratamiento oncológico prescritos, su estado, el esquema aplicado, el profesional tratante y el seguimiento de anulaciones o finalizaciones del tratamiento.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO';
