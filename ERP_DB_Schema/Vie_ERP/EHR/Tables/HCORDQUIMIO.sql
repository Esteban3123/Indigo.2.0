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
ALTER TABLE [EHR].[HCORDQUIMIO] NOCHECK CONSTRAINT [FK_HCORDQUI_ADCENATEN];


GO
ALTER TABLE [EHR].[HCORDQUIMIO] NOCHECK CONSTRAINT [FK_HCORDQUI_HCQUIORDENC];


GO
ALTER TABLE [EHR].[HCORDQUIMIO] NOCHECK CONSTRAINT [FK_HCORDQUI_INUNIFUNC];


GO
ALTER TABLE [EHR].[HCORDQUIMIO] NOCHECK CONSTRAINT [FK_HCORDQUI_Schemes];


GO
ALTER TABLE [EHR].[HCORDQUIMIO] NOCHECK CONSTRAINT [FK_HCORDQUIMIO_ADINGRESO];


GO
ALTER TABLE [EHR].[HCORDQUIMIO] NOCHECK CONSTRAINT [FK_HCORDQUIMIO_HCMOANULB];


GO
ALTER TABLE [EHR].[HCORDQUIMIO] NOCHECK CONSTRAINT [FK_HCORDQUIMIO_INDIAGNOS];


GO
ALTER TABLE [EHR].[HCORDQUIMIO] NOCHECK CONSTRAINT [FK_HCORDQUIMIO_INESPECIA];


GO
ALTER TABLE [EHR].[HCORDQUIMIO] NOCHECK CONSTRAINT [FK_HCORDQUIMIO_INPACIENT];


GO
ALTER TABLE [EHR].[HCORDQUIMIO] NOCHECK CONSTRAINT [FK_HCORDQUIMIO_INPROFSAL];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Folio en el que se suspende el esquema', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'NUMFOLSUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Id motivo Suspencion', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'IDHCMOANULB_SUSP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Usuario que anula', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'USUARIOANULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Obseravcion de la Anulacion', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'OBSERVACIONANULACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la Fecha de Anulacion', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'FECHAANULACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID del motivo anulacion', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'IDHCMOANULB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Me Identifica el Ultimo Ciclo Autorizado', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'ULTIMOCICLOAUTORIZADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario finaliza prematuro', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'USUFINALIZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Finalizar prematuro', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'FECHAFINALIZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observacion de finalizar prematuro', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'OBSERVACIONFINALIZAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Motivos del finalizar prematuro O SUSPENCION DEL ESQUEMA.    Motivos de la Suspensión, ese campo es quemado desde el formulario.    Esta Respuesta llena a la pregunta 60 de la Cuenta de Alto Costo.    1=Toxicidad de uno o más medicamentos    2=Otros motivos médicos    3=Muerte    4= Cambio de EAPB    5=Decisión del usuario    6=No hay disponibilidad de medicamentos    7=Otros motivos administrativos    8=Otras causas no contempladas  ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'MOTIVOFINALIZAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1=Neoadyuvancia (manejo  inicial prequirúrgico)  2= Tratamiento inicial curativo sin cirugía sugerida  3=Adyuvancia(manejo inicial postquirúrgico)  4=Manejo paliativo inicial  5=Manejo curativo de primera recaída  6=Manejo paliativo de primera recaída  7=Manejo curativo de segunda recaída  8=Manejo paliativo de segunda recaída  9=Manejo curativo de tercera recaída o posterior  10=Manejo paliativo de tercera recaída o posterior', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'48';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1-Prefase o citorreducción inicial  2-Inducción  3-Intensificación  4-Consolidación  5-Reinducción  6-Mantenimiento  7-Mantenimiento largo o final  8-Otra Fase', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'FASEQUIMIOTERAPIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '¿Cuántas fases de quimioterapia recibió el usuario en este periodo de reporte?', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'46';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la fecha de registro ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si tiene orden de la cita  1 = true  0 = false ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'ORDENCONCITA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Especialidad con la que se firma el folio', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el codigo del profesional', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el codigo unidad funcional', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el codigo centro atención', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ciclo Actual en el que va el paciente', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'CICLOACTUAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Total de Ciclos en el Esquema', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'CICLOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el codigo del diagnostico', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el numero de folio', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el numero ingreso', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el codigo del paciente', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Orden Solicitada  2 - Esquema iniciado  3 - Esquema finalizado completo  4 - Suspendido - Finalización prematura.  5 - Anulado - Cuando no se ha iniciado tratamiento', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'True - Si es manejo externo la orden.  False - No es de manejo externo la orden.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'MANEJOEXTERNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Id de esquemas', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'SchemesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDQUIMIO', @level2type = N'COLUMN', @level2name = N'ID';

