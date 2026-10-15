CREATE TABLE [dbo].[HCREFCONP] (
    [AUTO]            INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]       VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]       CHAR (10)                                                                        NOT NULL,
    [CODCENATE]       CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]       CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]       CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [MUNCODIGO]       CHAR (5)                                                                         NULL,
    [SERVIDOREM]      CHAR (50)                                                                        NOT NULL,
    [CODIGOIPS]       CHAR (100)                                                                       NULL,
    [NIVELREMIT]      CHAR (10)                                                                        NULL,
    [FECSOLICIT]      DATETIME                                                                         NULL,
    [FECCONFIR]       DATETIME                                                                         NULL,
    [CODESPECI]       CHAR (3)                                                                         NULL,
    [CONFIRMAP]       CHAR (100)                                                                       NULL,
    [FECLLEGADA]      DATETIME                                                                         NULL,
    [QUIENSOLRE]      CHAR (100)                                                                       NULL,
    [MOTREMISI]       CHAR (30)                                                                        NULL,
    [OBSERVACIO]      VARCHAR (MAX)                                                                    NULL,
    [CODUSUMOD]       CHAR (20)                                                                        NULL,
    [FECMODREG]       DATETIME                                                                         NULL,
    [FECREGSIS]       DATETIME                                                                         NULL,
    [CODCONCECG]      INT                                                                              NULL,
    [ESTADO]          INT                                                                              CONSTRAINT [DF__HCREFCONP__ESTAD__6BDAAE47] DEFAULT ((1)) NOT NULL,
    [RCCOBSALID]      INT                                                                              NULL,
    [CODEPECIREM]     CHAR (3)                                                                         NULL,
    [RCMOTREFID]      INT                                                                              NULL,
    [CODDIAGNO]       CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')     NULL,
    [EMBARAZO]        INT                                                                              NULL,
    [RCMODREFID]      INT                                                                              NULL,
    [RCMOTNOREFID]    INT                                                                              NULL,
    [JUSTSUSPEN]      VARCHAR (MAX)                                                                    NULL,
    [FECHASUSPEN]     DATETIME                                                                         NULL,
    [USUARSUSPEN]     CHAR (20)                                                                        NULL,
    [NOTIFEMAIL]      BIT                                                                              CONSTRAINT [DF_HCREFCONP_NOTIFEMAIL] DEFAULT ((1)) NOT NULL,
    [TRANSPASIST]     TINYINT                                                                          NULL,
    [OTROTRANSP]      NCHAR (30)                                                                       NULL,
    [MEDIOTRANS]      TINYINT                                                                          NULL,
    [EMPRETRANS]      NCHAR (30)                                                                       NULL,
    [OTRAEMPTRA]      NCHAR (30)                                                                       NULL,
    [NOMRECIBE]       NCHAR (30)                                                                       NULL,
    [CARGO]           TINYINT                                                                          NULL,
    [OTROCARGO]       NCHAR (30)                                                                       NULL,
    [FECHATRANS]      DATETIME                                                                         NULL,
    [VISADO]          INT                                                                              NULL,
    [USUREGVISADO]    CHAR (20)                                                                        NULL,
    [FECHAVISADO]     DATETIME                                                                         NULL,
    [VISADOSUSPE]     INT                                                                              NULL,
    [USUVISASUSP]     CHAR (20)                                                                        NULL,
    [FECHAVISADOSUS]  DATETIME                                                                         NULL,
    [JUSTSIPERTINEN]  VARCHAR (MAX)                                                                    NULL,
    [TIPOSIPERTINEN]  INT                                                                              NULL,
    [FECHSIPERTINEN]  DATETIME                                                                         NULL,
    [USUSIPERTINEN]   CHAR (20)                                                                        NULL,
    [NOVEDAD]         VARCHAR (MAX)                                                                    NULL,
    [EXTRAMURAL]      BIT                                                                              CONSTRAINT [DF_HCREFCONP_EXTRAMURAL] DEFAULT ((0)) NULL,
    [RequestType]     INT                                                                              NULL,
    [RequestPriority] INT                                                                              NULL,
    [CODSERIPS]       CHAR (10)                                                                        NULL,
    [QuantityService] INT                                                                              NULL,
    CONSTRAINT [PK_HCREFCONP] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_HCREFCONP_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCREFCONP_HCREFCONP] FOREIGN KEY ([RCCOBSALID]) REFERENCES [dbo].[RCCOBSAL] ([Id]),
    CONSTRAINT [FK_HCREFCONP_HCREFCONP1] FOREIGN KEY ([AUTO]) REFERENCES [dbo].[HCREFCONP] ([AUTO]),
    CONSTRAINT [FK_HCREFCONP_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_HCREFCONP_INESPECIA] FOREIGN KEY ([CODEPECIREM]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_HCREFCONP_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCREFCONP_RCMODSOLIC] FOREIGN KEY ([RCMODREFID]) REFERENCES [dbo].[RCMODSOLIC] ([Id]),
    CONSTRAINT [FK_HCREFCONP_RCMOTNOREF] FOREIGN KEY ([RCMOTNOREFID]) REFERENCES [dbo].[RCMOTNOREF] ([Id]),
    CONSTRAINT [FK_HCREFCONP_RCMOTREF] FOREIGN KEY ([RCMOTREFID]) REFERENCES [dbo].[RCMOTREF] ([Id])
);


GO
ALTER TABLE [dbo].[HCREFCONP] NOCHECK CONSTRAINT [FK_HCREFCONP_ADINGRESO];


GO
ALTER TABLE [dbo].[HCREFCONP] NOCHECK CONSTRAINT [FK_HCREFCONP_HCREFCONP];


GO
ALTER TABLE [dbo].[HCREFCONP] NOCHECK CONSTRAINT [FK_HCREFCONP_HCREFCONP1];


GO
ALTER TABLE [dbo].[HCREFCONP] NOCHECK CONSTRAINT [FK_HCREFCONP_INDIAGNOS];


GO
ALTER TABLE [dbo].[HCREFCONP] NOCHECK CONSTRAINT [FK_HCREFCONP_INESPECIA];


GO
ALTER TABLE [dbo].[HCREFCONP] NOCHECK CONSTRAINT [FK_HCREFCONP_INPACIENT];


GO
ALTER TABLE [dbo].[HCREFCONP] NOCHECK CONSTRAINT [FK_HCREFCONP_RCMODSOLIC];


GO
ALTER TABLE [dbo].[HCREFCONP] NOCHECK CONSTRAINT [FK_HCREFCONP_RCMOTNOREF];


GO
ALTER TABLE [dbo].[HCREFCONP] NOCHECK CONSTRAINT [FK_HCREFCONP_RCMOTREF];


GO
CREATE NONCLUSTERED INDEX [IX_HCREFCONP__CODCENATE__ESTADO__INC__IPCODPACI__NUMINGRES__UFUCODIGO__FECSOLICIT]
    ON [dbo].[HCREFCONP]([CODCENATE] ASC, [ESTADO] ASC)
    INCLUDE([IPCODPACI], [NUMINGRES], [UFUCODIGO], [FECSOLICIT]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'16-09-2024 campo nuevo:
Cantidad del servicio relacionado, codigo CUPS que el medico selecciona al ordenar una referencia o contrareferencia.
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'QuantityService';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'16-09-2024 campo nuevo:
Codigo del servicio relacionado, codigo CUPS que el medico selecciona al ordenar una referencia o contrareferencia.
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prioritaria = 1 
No prioritaria = 2 
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'RequestPriority';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia = 1
Contrarreferencia = 2 

', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'RequestType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si la Solicitud de referencia por parte del medico fue marcada como extramural.   True    False ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'EXTRAMURAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Novedad general de la Referencia, este campo se llena desde la primera pestaña del Dashboard de Solicitud de Referencia campo Novedad de la rejilla principal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'NOVEDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'usuario Si pertinencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'USUSIPERTINEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Si pertinencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'FECHSIPERTINEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo:  1->Medica  2->Administrativa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'TIPOSIPERTINEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Justificación que se registra cuando Si hay pertinencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'JUSTSIPERTINEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Visado, esta fecha de visado se realiza desde el Dashboard de Referencia desde la pestaña de Solicitudes Canceladas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'FECHAVISADOSUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que viso el registro ,este usuario de visado se realiza desde el Dashboard de Referencia desde la pestaña de Solicitudes Canceladas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'USUVISASUSP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1=Visado de lo contrario es que no esta visado, este visado se realiza desde el Dashboard de Referencia desde la pestaña de Solicitudes Canceladas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'VISADOSUSPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha visado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'FECHAVISADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que viso el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'USUREGVISADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1=Visado de lo contrario es que no esta visado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'VISADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de translado (Datos de translado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'FECHATRANS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Otro cargo (Datos de translado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'OTROCARGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cargo (Datos de translado)    Auxiliar 1  Enfermero(a) 2  Médico General 3  Médico Especialista 4  Otro 5', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'CARGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de quien recibe (Datos de translado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'NOMRECIBE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Otra empresa (Datos de translado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'OTRAEMPTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Empresa de transporte (Datos de translado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'EMPRETRANS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Medio de transporte (Datos de translado)    Terrestre 1  Maritimo y/o fluial 2  Aéreo 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'MEDIOTRANS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Otro transporte de asistencia (Datos de translado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'OTROTRANSP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Transporte de Asisitencia (Datos de translado)    Basico 1  Medicalizado 2  Otro 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'TRANSPASIST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si la referencia ya fue notificada automáticamento por correo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'NOTIFEMAIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que suspende', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'USUARSUSPEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de suspension', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'FECHASUSPEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Justificacion suspension', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'JUSTSUSPEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Motivo de no referencia se relaciona con el id de la tabla RCMOTNOREF. este campo cuando se vaya a suspender la solicitud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'RCMOTNOREFID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Modalidad De Referencia Que Se relaciona con el Id de la tabla  RCMODSOLIC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'RCMODREFID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de embarazo 1->Si  2->No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'EMBARAZO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Motivo de Referencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'RCMOTREFID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la especialidad a la que se remite', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'CODEPECIREM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la cobertura de salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'RCCOBSALID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado para saber como esta la gestion de la referencia   1->Solicitado.  2->Pendinte o Gestionando.  3->Aceptado con pediente de salida.  4->Suspendido.  5->Ya salio.  6->Solicitud con Pertinencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo para la gestion de la referencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'CODCONCECG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del registro en el sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'FECREGSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificacion del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'FECMODREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de usuario que modifica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'CODUSUMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observacion (resumen de la historia clinica)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'OBSERVACIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Motivos de remision', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'MOTREMISI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'quien solicita la remision', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'QUIENSOLRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de salida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'FECLLEGADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Persona que confirma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'CONFIRMAP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de confirmacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'FECCONFIR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha solicitud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'FECSOLICIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nivel a que se remite', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'NIVELREMIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la institucion prestadora de servicios de salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'CODIGOIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Servicio a donde se remite', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'SERVIDOREM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Municipio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'MUNCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'AUTO';

