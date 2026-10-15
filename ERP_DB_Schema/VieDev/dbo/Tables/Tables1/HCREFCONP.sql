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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo:  1->Medica  2->Administrativa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'TIPOSIPERTINEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cargo (Datos de translado)    Auxiliar 1  Enfermero(a) 2  Médico General 3  Médico Especialista 4  Otro 5', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'CARGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Medio de transporte (Datos de translado)    Terrestre 1  Maritimo y/o fluial 2  Aéreo 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'MEDIOTRANS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Transporte de Asisitencia (Datos de translado)    Basico 1  Medicalizado 2  Otro 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'TRANSPASIST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado para saber como esta la gestion de la referencia   1->Solicitado.  2->Pendinte o Gestionando.  3->Aceptado con pediente de salida.  4->Suspendido.  5->Ya salio.  6->Solicitud con Pertinencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONP', @level2type = N'COLUMN', @level2name = N'ESTADO';

