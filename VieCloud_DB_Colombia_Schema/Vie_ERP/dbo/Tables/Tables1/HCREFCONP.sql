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
ALTER TABLE [dbo].[HCREFCONP] NOCHECK CONSTRAINT [FK_HCREFCONP_INPACIENT];


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCREFCONP].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCREFCONP].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCREFCONP].[CODDIAGNO]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');




GO
ALTER TABLE [dbo].[HCREFCONP] NOCHECK CONSTRAINT [FK_HCREFCONP_ADINGRESO];


GO
ALTER TABLE [dbo].[HCREFCONP] NOCHECK CONSTRAINT [FK_HCREFCONP_INPACIENT];


GO

CREATE NONCLUSTERED INDEX [IX_HCREFCONP__CODCENATE__ESTADO__INC__IPCODPACI__NUMINGRES__UFUCODIGO__FECSOLICIT]
    ON [dbo].[HCREFCONP]([CODCENATE] ASC, [ESTADO] ASC)
    INCLUDE([IPCODPACI], [NUMINGRES], [UFUCODIGO], [FECSOLICIT]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad del servicio CUPS (código de procedimiento) seleccionado por el médico al ordenar referencia o contrareferencia. Tipo: INT. Sinónimos: cantidad de servicios, volumen de procedimientos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'QuantityService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'16-09-2024 campo nuevo:
Cantidad del servicio relacionado, codigo CUPS que el medico selecciona al ordenar una referencia o contrareferencia.
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'QuantityService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'QuantityService';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del servicio CUPS relacionado, seleccionado por el médico en la orden de referencia o contrareferencia. Tipo: CHAR(10). Sinónimos: código de procedimiento, código CUPS, servicio ordenado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'16-09-2024 campo nuevo:
Codigo del servicio relacionado, codigo CUPS que el medico selecciona al ordenar una referencia o contrareferencia.
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prioridad de la solicitud de referencia: 1=Prioritaria, 2=No prioritaria. Tipo: INT. Sinónimos: urgencia, nivel de prioridad, atención prioritaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'RequestPriority';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Prioritaria = 1 
No prioritaria = 2 
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'RequestPriority';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'RequestPriority';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de solicitud: 1=Referencia, 2=Contrareferencia. Tipo: INT. Sinónimos: modalidad de remisión, tipo de remisión, solicitud de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'RequestType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Referencia = 1
Contrarreferencia = 2 

', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'RequestType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'RequestType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) que marca si la referencia fue ordenada como atención extramural (fuera de la institución). Valores: 1=Sí, 0=No. Sinónimos: atención externa, fuera de sede.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'EXTRAMURAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si la Solicitud de referencia por parte del medico fue marcada como extramural.   True    False ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'EXTRAMURAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'EXTRAMURAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Novedad general de la referencia registrada en el Dashboard desde la pestaña principal. Tipo: VARCHAR(MAX). Sinónimos: observación general, actualización, evento relevante de la referencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'NOVEDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Novedad general de la Referencia, este campo se llena desde la primera pestaña del Dashboard de Solicitud de Referencia campo Novedad de la rejilla principal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'NOVEDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'NOVEDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de usuario que registra la evaluación de pertinencia de la referencia. Tipo: CHAR(20). Sinónimos: usuario de revisión, auditor de pertinencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'USUSIPERTINEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'usuario Si pertinencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'USUSIPERTINEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'USUSIPERTINEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se evalúa la pertinencia de la referencia. Tipo: DATETIME. Sinónimos: fecha de validación, fecha de revisión clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'FECHSIPERTINEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Si pertinencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'FECHSIPERTINEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'FECHSIPERTINEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de pertinencia evaluada: 1=Médica (clínica), 2=Administrativa. Tipo: INT. Sinónimos: modalidad de pertinencia, clase de revisión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'TIPOSIPERTINEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo:  1->Medica  2->Administrativa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'TIPOSIPERTINEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'TIPOSIPERTINEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación documentada cuando la pertinencia es confirmada como sí. Tipo: VARCHAR(MAX). Sinónimos: argumentación clínica, motivo de aceptación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'JUSTSIPERTINEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justificación que se registra cuando Si hay pertinencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'JUSTSIPERTINEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'JUSTSIPERTINEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de visado de la referencia suspendida, registrada en Dashboard pestaña Solicitudes Canceladas. Tipo: DATETIME. Sinónimos: fecha de aprobación suspensión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'FECHAVISADOSUS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Visado, esta fecha de visado se realiza desde el Dashboard de Referencia desde la pestaña de Solicitudes Canceladas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'FECHAVISADOSUS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'FECHAVISADOSUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que visó/aprobó el registro de referencia suspendida en Dashboard. Tipo: CHAR(20). Sinónimos: auditor de suspensión, responsable de validación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'USUVISASUSP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que viso el registro ,este usuario de visado se realiza desde el Dashboard de Referencia desde la pestaña de Solicitudes Canceladas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'USUVISASUSP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'USUVISASUSP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de visado de referencia suspendida: 1=Visado, 0 o NULL=No visado. Tipo: INT. Sinónimos: aprobación de suspensión, validación de cancelación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'VISADOSUSPE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1=Visado de lo contrario es que no esta visado, este visado se realiza desde el Dashboard de Referencia desde la pestaña de Solicitudes Canceladas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'VISADOSUSPE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'VISADOSUSPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de visado/aprobación de la referencia. Tipo: DATETIME. Sinónimos: fecha de validación, fecha de autorización.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'FECHAVISADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha visado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'FECHAVISADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'FECHAVISADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario responsable de visar/aprobar el registro de referencia. Tipo: CHAR(20). Sinónimos: auditor, validador clínico, profesional autorizador.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'USUREGVISADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que viso el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'USUREGVISADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'USUREGVISADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de visado/aprobación de referencia: 1=Visado, 0 o NULL=No visado. Tipo: INT. Sinónimos: aprobado, autorizado, validado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'VISADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1=Visado de lo contrario es que no esta visado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'VISADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'VISADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de traslado del paciente entre centros de atención. Tipo: DATETIME. Sinónimos: fecha de transporte, fecha de movilización.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'FECHATRANS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de translado (Datos de translado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'FECHATRANS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'FECHATRANS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especificación de otro cargo del personal que recibe/traslada cuando no está en lista estándar. Tipo: NCHAR(30). Sinónimos: profesional adicional, rol especial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'OTROCARGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Otro cargo (Datos de translado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'OTROCARGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'OTROCARGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cargo del personal que realiza/recibe el traslado: 1=Auxiliar, 2=Enfermero(a), 3=Médico General, 4=Médico Especialista, 5=Otro. Tipo: TINYINT. Sinónimos: profesión transportista, rol de traslado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'CARGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cargo (Datos de translado)    Auxiliar 1  Enfermero(a) 2  Médico General 3  Médico Especialista 4  Otro 5', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'CARGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'CARGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo de la persona que recibe al paciente en el destino. Tipo: NCHAR(30). Sinónimos: receptor del paciente, personal que acepta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'NOMRECIBE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de quien recibe (Datos de translado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'NOMRECIBE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'NOMRECIBE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de otra empresa de transporte cuando no está en la lista predefinida. Tipo: NCHAR(30). Sinónimos: proveedor alternativo, transportista adicional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'OTRAEMPTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Otra empresa (Datos de translado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'OTRAEMPTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'OTRAEMPTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Empresa prestadora de servicio de transporte médico/asistencial. Tipo: NCHAR(30). Sinónimos: proveedor de traslado, servicio de ambulancia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'EMPRETRANS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Empresa de transporte (Datos de translado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'EMPRETRANS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'EMPRETRANS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Medio de transporte utilizado: 1=Terrestre, 2=Marítimo y/o fluvial, 3=Aéreo. Tipo: TINYINT. Sinónimos: vía de traslado, modalidad de transporte.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'MEDIOTRANS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Medio de transporte (Datos de translado)    Terrestre 1  Maritimo y/o fluial 2  Aéreo 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'MEDIOTRANS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'MEDIOTRANS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especificación de otro tipo de transporte de asistencia no listado. Tipo: NCHAR(30). Sinónimos: transporte alternativo, vehículo especial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'OTROTRANSP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Otro transporte de asistencia (Datos de translado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'OTROTRANSP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'OTROTRANSP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de transporte de asistencia: 1=Básico, 2=Medicalizado, 3=Otro. Tipo: TINYINT. Sinónimos: nivel de transporte, tipo de ambulancia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'TRANSPASIST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Transporte de Asisitencia (Datos de translado)    Basico 1  Medicalizado 2  Otro 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'TRANSPASIST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'TRANSPASIST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de notificación automática: 1=Notificado por correo, 0=No notificado. Tipo: BIT. Sinónimos: envío de email, comunicación automática.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'NOTIFEMAIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si la referencia ya fue notificada automáticamento por correo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'NOTIFEMAIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'NOTIFEMAIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de usuario que suspende la solicitud de referencia. Tipo: CHAR(20). Sinónimos: usuario cancelador, responsable de suspensión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'USUARSUSPEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que suspende', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'USUARSUSPEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'USUARSUSPEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se suspende la solicitud de referencia. Tipo: DATETIME. Sinónimos: fecha de cancelación, fecha de suspensión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'FECHASUSPEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de suspension', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'FECHASUSPEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'FECHASUSPEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación documentada para la suspensión de la referencia. Tipo: VARCHAR(MAX). Sinónimos: motivo de cancelación, razón de suspensión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'JUSTSUSPEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justificacion suspension', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'JUSTSUSPEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'JUSTSUSPEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del motivo de NO referencia (FK a RCMOTNOREF), usado cuando se suspende la solicitud. Tipo: INT. Sinónimos: causa de rechazo, motivo de no remisión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'RCMOTNOREFID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivo de no referencia se relaciona con el id de la tabla RCMOTNOREF. este campo cuando se vaya a suspender la solicitud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'RCMOTNOREFID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'RCMOTNOREFID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la modalidad de referencia (FK a RCMODSOLIC). Tipo: INT. Sinónimos: tipo de modalidad, clase de referencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'RCMODREFID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Modalidad De Referencia Que Se relaciona con el Id de la tabla  RCMODSOLIC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'RCMODREFID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'RCMODREFID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de embarazo de la paciente: 1=Sí, 2=No. Tipo: INT. Sinónimos: gestación, condición obstétrica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'EMBARAZO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de embarazo 1->Si  2->No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'EMBARAZO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'EMBARAZO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de diagnóstico CIE-10 del paciente (FK a INDIAGNOS), PII ofuscado. Tipo: CHAR(4). Sinónimos: código ICD, diagnóstico principal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del motivo de referencia (FK a RCMOTREF). Tipo: INT. Sinónimos: causa de remisión, razón de derivación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'RCMOTREFID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivo de Referencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'RCMOTREFID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'RCMOTREFID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la especialidad médica a la que se remite (FK a INESPECIA). Tipo: CHAR(3). Sinónimos: especialidad destino, área médica solicitada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'CODEPECIREM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la especialidad a la que se remite', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'CODEPECIREM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'CODEPECIREM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cobertura/régimen de salud del paciente (FK a RCCOBSAL). Tipo: INT. Sinónimos: plan de salud, afiliación sanitaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'RCCOBSALID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la cobertura de salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'RCCOBSALID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'RCCOBSALID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de gestión de la referencia: 1=Solicitado, 2=Pendiente/Gestionando, 3=Aceptado con pendiente de salida, 4=Suspendido, 5=Ya salió, 6=Con pertinencia. Tipo: INT. Sinónimos: estatus referencia, fase de tramite.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado para saber como esta la gestion de la referencia   1->Solicitado.  2->Pendinte o Gestionando.  3->Aceptado con pediente de salida.  4->Suspendido.  5->Ya salio.  6->Solicitud con Pertinencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo/número secuencial para la gestión y seguimiento de la referencia. Tipo: INT. Sinónimos: correlativo de gestión, número de seguimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'CODCONCECG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo para la gestion de la referencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'CODCONCECG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'CODCONCECG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de registro inicial del documento en el sistema. Tipo: DATETIME. Sinónimos: fecha de creación, fecha de ingreso al EHR.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'FECREGSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del registro en el sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'FECREGSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'FECREGSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del registro. Tipo: DATETIME. Sinónimos: fecha de actualización, última edición.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'FECMODREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificacion del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'FECMODREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'FECMODREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de usuario que realiza la modificación del registro. Tipo: CHAR(20). Sinónimos: usuario editor, responsable de actualización.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'CODUSUMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de usuario que modifica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'CODUSUMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'CODUSUMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones clínicas/resumen de la historia clínica del paciente. Tipo: VARCHAR(MAX). Sinónimos: notas clínicas, hallazgos relevantes, antecedentes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'OBSERVACIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observacion (resumen de la historia clinica)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'OBSERVACIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'OBSERVACIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivos clínicos/administrativos de la remisión de referencia. Tipo: CHAR(30). Sinónimos: causa de derivación, razón de remisión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'MOTREMISI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivos de remision', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'MOTREMISI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'MOTREMISI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre/identificación del profesional de salud que solicita la remisión. Tipo: CHAR(100). Sinónimos: solicitante, médico ordenante.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'QUIENSOLRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'quien solicita la remision', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'QUIENSOLRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'QUIENSOLRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de salida/llegada del paciente al destino. Tipo: DATETIME. Sinónimos: fecha de traslado, fecha de movilización.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'FECLLEGADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de salida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'FECLLEGADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'FECLLEGADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la persona que confirma la aceptación de la referencia. Tipo: CHAR(100). Sinónimos: receptor confirmante, validador de ingreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'CONFIRMAP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Persona que confirma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'CONFIRMAP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'CONFIRMAP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la especialidad médica solicitada o actual. Tipo: CHAR(3). Sinónimos: código de área médica, especialidad clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de confirmación de aceptación de la referencia. Tipo: DATETIME. Sinónimos: fecha de aceptación, fecha de validación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'FECCONFIR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de confirmacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'FECCONFIR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'FECCONFIR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de solicitud/creación de la referencia por el médico. Tipo: DATETIME. Sinónimos: fecha de orden, fecha de generación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'FECSOLICIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha solicitud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'FECSOLICIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'FECSOLICIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel asistencial destino: Primario, Secundario, Terciario. Tipo: CHAR(10). Sinónimos: complejidad de centro, nivel de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'NIVELREMIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nivel a que se remite', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'NIVELREMIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'NIVELREMIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador de la Institución Prestadora de Servicios de Salud destino. Tipo: CHAR(100). Sinónimos: código de centro, código de hospital.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'CODIGOIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la institucion prestadora de servicios de salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'CODIGOIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'CODIGOIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del servicio/unidad clínica de destino donde se remite. Tipo: CHAR(50). Sinónimos: servicio hospitalario, área de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'SERVIDOREM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Servicio a donde se remite', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'SERVIDOREM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'SERVIDOREM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del municipio de la institución destino. Tipo: CHAR(5). Sinónimos: código territorial, ubicación geográfica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'MUNCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Municipio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'MUNCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'MUNCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud ordenante, identificación PII ofuscada. Tipo: CHAR(20). Sinónimos: código médico, cédula profesional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la Unidad Funcional (área/servicio) origen de la referencia. Tipo: CHAR(10). Sinónimos: código de servicio, área clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Centro de Atención donde se genera la referencia. Tipo: CHAR(10). Sinónimos: código de institución, código de sede.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número único del ingreso/admisión del paciente (FK a ADINGRESO). Tipo: CHAR(10). Sinónimos: número de atención, expediente de ingreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente/identificación PII ofuscada (FK a INPACIENT). Tipo: VARCHAR(25). Sinónimos: cédula, documento, ID paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementado (clave primaria) del registro de referencia. Tipo: INT IDENTITY. Sinónimos: ID referencia, clave única.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
CREATE NONCLUSTERED INDEX [IX_HCREFCONP_BUSQUEDA_REF]
    ON [dbo].[HCREFCONP]([IPCODPACI] ASC, [NUMINGRES] ASC, [CODCENATE] ASC, [UFUCODIGO] ASC)
    INCLUDE([AUTO], [CODPROSAL], [MUNCODIGO], [SERVIDOREM], [CODIGOIPS], [NIVELREMIT], [FECSOLICIT], [FECCONFIR], [CODESPECI], [CONFIRMAP], [FECLLEGADA], [QUIENSOLRE], [MOTREMISI], [CODUSUMOD], [FECMODREG], [FECREGSIS], [TRANSPASIST], [OTROTRANSP], [MEDIOTRANS], [EMPRETRANS], [OTRAEMPTRA], [NOMRECIBE], [CARGO], [OTROCARGO], [FECHATRANS]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Remisiones y contrarreferencias de pacientes: registra las solicitudes de remisión a otras IPS o especialidades, el seguimiento de confirmación, transporte asistencial, visado y novedades del proceso de referencia y contrarreferencia en la historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP';
