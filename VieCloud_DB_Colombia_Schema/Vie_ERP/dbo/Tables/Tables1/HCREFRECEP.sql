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
ALTER TABLE [dbo].[HCREFRECEP] NOCHECK CONSTRAINT [FK_HCREFRECEP_RCMODSOLIC];


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCREFRECEP].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado envío de correo de referencia: 1=Debe enviar, 2=Ya enviado, 3=Reenvío. INT, control de notificación electrónica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'ENVIOCORREO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 -> Se debe Enviar Correo  2 -> Ya se envio el Correo   3 -> Reenvío de correo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'ENVIOCORREO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'ENVIOCORREO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso del paciente, referencia a ADINGRESO. Vincula referencia con atención/hospitalización. CHAR(10), FK.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de urgencia vital en referencia: 1=Sí urgencia, 0=No. BIT, prioridad de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'CODURGVITAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'URGENCIA VITAL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'CODURGVITAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'CODURGVITAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de otra entidad solicitante cuando tipo no es IPS, EAPB, CRUE. VARCHAR(150).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'OTROTIPOENTSOLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Otro tipo de entidad solicita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'OTROTIPOENTSOLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'OTROTIPOENTSOLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación entidad que solicita referencia: 1=IPS, 2=EAPB, 3=CRUE, 4=OTRA. INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'CODTIPOENTSOLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo entidad solicita:  1=IPS   2=EAPB  3=CRUE  4=OTRA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'CODTIPOENTSOLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'CODTIPOENTSOLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel complejidad clínica de referencia: 1=Baja, 2=Media, 3=Alta. INT, determina recursos necesarios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'CODCOMPLEJ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'COMPLEJIDAD:  1=BAJA   2=MEDIA  3=ALTA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'CODCOMPLEJ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'CODCOMPLEJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo población/grupo poblacional: 1=Maternas/embarazo, 2=Menor 5 años, 3=Adulto mayor, 4=Población general. INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'CODTIPPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de población:  1=Maternas   2=Menor de 5 años  3=Adulto mayor  4=Población general', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'CODTIPPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'CODTIPPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador EAPB nativa Vie en sistema. INT, FK entidad aseguradora.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'GENCONENTITY';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id eapb nativa Vie', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'GENCONENTITY';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'GENCONENTITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre/datos del profesional médico que genera referencia, remitente. VARCHAR(150).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'MEDREMITE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Medico que remite', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'MEDREMITE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'MEDREMITE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo observaciones/notas clínicas adicionales en referencia. VARCHAR(200).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'OBSERVACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'OBSERVACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'OBSERVACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código especialidad destino a la cual se remite paciente. CHAR(3), FK INESPECIA.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'INESPECIAIDAREMITIR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especialidad a remitir', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'INESPECIAIDAREMITIR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'INESPECIAIDAREMITIR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número consecutivo único de referencia, identificador de negocio. VARCHAR(10), UNIQUE.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'CONSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'CONSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'CONSEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador centro de atención donde se genera referencia. CHAR(10), FK ADCENATEN, obligatorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'ADCENATENID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id centro de atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'ADCENATENID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'ADCENATENID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador estado de embarazo: 1=Sí embarazada, 0=No. BIT, clasificador población materno.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'EMBARAZO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Si está en embarazo 1:si 0:no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'EMBARAZO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'EMBARAZO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador motivo/causa de referencia (clínico, administrativo). INT, FK RCMOTREF.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'RCMOTREFID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del motivo de refencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'RCMOTREFID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'RCMOTREFID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación usuario que crea/registra referencia en sistema. CHAR(20), auditoria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'USERCREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de creacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'USERCREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'USERCREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de envío efectivo de referencia a destino. DATETIME, obligatorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'FECMEDENV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de envío', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'FECMEDENV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'FECMEDENV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código especialidad de origen que remite, profesional remitente. CHAR(3), FK INESPECIA.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'INESPECIAIDREMITE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id especialidad que remite y a remitir', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'INESPECIAIDREMITE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'INESPECIAIDREMITE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador unidad funcional/servicio destino para remisión. CHAR(10), FK INUNIFUNC.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'INUNIFUNCID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id Servicios a remitir', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'INUNIFUNCID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'INUNIFUNCID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador servicio clínico que origina/remite referencia. INT, FK RCSERVICIOS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'RCSERVICIOSID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Servicio que remite', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'RCSERVICIOSID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'RCSERVICIOSID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador modalidad/tipo solicitud de referencia (urgente, programada). INT, FK RCMODSOLIC.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'RCMODSOLICID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id modalidad solicitud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'RCMODSOLICID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'RCMODSOLICID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador tipo cobertura/beneficio de salud del paciente en referencia. INT, FK RCCOBSAL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'RCCOBSALID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id Cobertura salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'RCCOBSALID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'RCCOBSALID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador medio transmisión referencia: correo, fax, sistema, físico. INT, FK RCMEDIOSENVIO.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'RCMEDIOSENVIOID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id Medio de envio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'RCMEDIOSENVIOID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'RCMEDIOSENVIOID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora registro inicial de referencia en sistema. DATETIME, obligatorio, timestamp creación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'FECREGIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha que se registra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'FECREGIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'FECREGIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificación paciente: cédula/documento/carné. VARCHAR(25), PII ofuscado, obligatorio, FK.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado referencia: 1=Registrada, 2=Aceptada sin ingreso, 3=Aceptada con ingreso, 4=Rechazada, 5=Cancelada, 6=Egresada. NCHAR(10).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1- Registrado  2- Aceptado Sin Ingreso  3- Aceptado Con Ingreso  4- Rechazado  5- Cancelado   6- Egresado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador IPS que genera/remite referencia, entidad origen. CHAR(100), FK ADCONTIPS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'ADCONTIPSID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id IPS que remite', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'ADCONTIPSID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'ADCONTIPSID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador EAPB/entidad administradora afiliación paciente. CHAR(9), FK INENTIDAD.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'INENTADMID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id EAPB', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'INENTADMID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'INENTADMID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único tabla HCREFRECEP, clave primaria. INT IDENTITY, autonumérico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registros de referencias y remisiones médicas entre especialidades o centros de atención. Guarda las solicitudes de referencia de un paciente hacia otra especialidad o institución, incluyendo el médico que remite, el servicio destino, el motivo y la urgencia de la remisión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECEP';
