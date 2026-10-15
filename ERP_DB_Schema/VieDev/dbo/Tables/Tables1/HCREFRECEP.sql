CREATE TABLE [dbo].[HCREFRECEP] (
    [ID]                  INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [INENTADMID]          CHAR (9)                                                                         NULL,
    [ADCONTIPSID]         CHAR (100)                                                                       NULL,
    [ESTADO]              NCHAR (10)                                                                       NULL,
    [IPCODPACI]           VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [FECREGIS]            DATETIME                                                                         NOT NULL,
    [RCMEDIOSENVIOID]     INT                                                                              NULL,
    [RCCOBSALID]          INT                                                                              NULL,
    [RCMODSOLICID]        INT                                                                              NULL,
    [RCSERVICIOSID]       INT                                                                              NULL,
    [INUNIFUNCID]         CHAR (10)                                                                        NULL,
    [INESPECIAIDREMITE]   CHAR (3)                                                                         NULL,
    [FECMEDENV]           DATETIME                                                                         NOT NULL,
    [USERCREA]            CHAR (20)                                                                        NOT NULL,
    [RCMOTREFID]          INT                                                                              NULL,
    [EMBARAZO]            BIT                                                                              NULL,
    [ADCENATENID]         CHAR (10)                                                                        NOT NULL,
    [CONSEC]              VARCHAR (10)                                                                     NOT NULL,
    [INESPECIAIDAREMITIR] CHAR (3)                                                                         NOT NULL,
    [OBSERVACION]         VARCHAR (200)                                                                    NULL,
    [MEDREMITE]           VARCHAR (150)                                                                    NULL,
    [GENCONENTITY]        INT                                                                              NULL,
    [CODTIPPAC]           INT                                                                              NULL,
    [CODCOMPLEJ]          INT                                                                              NULL,
    [CODTIPOENTSOLI]      INT                                                                              NULL,
    [OTROTIPOENTSOLI]     VARCHAR (150)                                                                    NULL,
    [CODURGVITAL]         BIT                                                                              NULL,
    [NUMINGRES]           CHAR (10)                                                                        NULL,
    [ENVIOCORREO]         INT                                                                              NULL,
    CONSTRAINT [PK_HCREFRECEP] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCREFRECEP_ADCENATEN] FOREIGN KEY ([ADCENATENID]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCREFRECEP_ADCONTIPS] FOREIGN KEY ([ADCONTIPSID]) REFERENCES [dbo].[ADCONTIPS] ([CODIGOIPS]),
    CONSTRAINT [FK_HCREFRECEP_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCREFRECEP_INESPECIA] FOREIGN KEY ([INESPECIAIDREMITE]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_HCREFRECEP_INESPECIA1] FOREIGN KEY ([INESPECIAIDAREMITIR]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_HCREFRECEP_INUNIFUNC] FOREIGN KEY ([INUNIFUNCID]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_HCREFRECEP_RCCOBSAL] FOREIGN KEY ([RCCOBSALID]) REFERENCES [dbo].[RCCOBSAL] ([Id]),
    CONSTRAINT [FK_HCREFRECEP_RCMEDIOSENVIO] FOREIGN KEY ([RCMEDIOSENVIOID]) REFERENCES [dbo].[RCMEDIOSENVIO] ([Id]),
    CONSTRAINT [FK_HCREFRECEP_RCMODSOLIC] FOREIGN KEY ([RCMODSOLICID]) REFERENCES [dbo].[RCMODSOLIC] ([Id]),
    CONSTRAINT [FK_HCREFRECEP_RCSERVICIOS] FOREIGN KEY ([RCSERVICIOSID]) REFERENCES [dbo].[RCSERVICIOS] ([Id]),
    CONSTRAINT [IX_HCREFRECEP] UNIQUE NONCLUSTERED ([CONSEC] ASC)
);


GO
ALTER TABLE [dbo].[HCREFRECEP] NOCHECK CONSTRAINT [FK_HCREFRECEP_ADCENATEN];


GO
ALTER TABLE [dbo].[HCREFRECEP] NOCHECK CONSTRAINT [FK_HCREFRECEP_ADCONTIPS];


GO
ALTER TABLE [dbo].[HCREFRECEP] NOCHECK CONSTRAINT [FK_HCREFRECEP_ADINGRESO];


GO
ALTER TABLE [dbo].[HCREFRECEP] NOCHECK CONSTRAINT [FK_HCREFRECEP_INESPECIA];


GO
ALTER TABLE [dbo].[HCREFRECEP] NOCHECK CONSTRAINT [FK_HCREFRECEP_INESPECIA1];


GO
ALTER TABLE [dbo].[HCREFRECEP] NOCHECK CONSTRAINT [FK_HCREFRECEP_INUNIFUNC];


GO
ALTER TABLE [dbo].[HCREFRECEP] NOCHECK CONSTRAINT [FK_HCREFRECEP_RCCOBSAL];


GO
ALTER TABLE [dbo].[HCREFRECEP] NOCHECK CONSTRAINT [FK_HCREFRECEP_RCMEDIOSENVIO];


GO
ALTER TABLE [dbo].[HCREFRECEP] NOCHECK CONSTRAINT [FK_HCREFRECEP_RCMODSOLIC];


GO
ALTER TABLE [dbo].[HCREFRECEP] NOCHECK CONSTRAINT [FK_HCREFRECEP_RCSERVICIOS];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 -> Se debe Enviar Correo  2 -> Ya se envio el Correo   3 -> Reenvío de correo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'ENVIOCORREO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo entidad solicita:  1=IPS   2=EAPB  3=CRUE  4=OTRA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'CODTIPOENTSOLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMPLEJIDAD:  1=BAJA   2=MEDIA  3=ALTA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'CODCOMPLEJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de población:  1=Maternas   2=Menor de 5 años  3=Adulto mayor  4=Población general', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'CODTIPPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1- Registrado  2- Aceptado Sin Ingreso  3- Aceptado Con Ingreso  4- Rechazado  5- Cancelado   6- Egresado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'ESTADO';

