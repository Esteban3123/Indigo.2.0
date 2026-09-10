CREATE TABLE [dbo].[HCORHEMCO] (
    [ID]           INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODDIAGNO]    CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')     NULL,
    [HB]           VARCHAR (10)                                                                     NULL,
    [HTO]          VARCHAR (10)                                                                     NULL,
    [RCTOPLAQ]     VARCHAR (10)                                                                     NULL,
    [FIBRINOG]     VARCHAR (10)                                                                     NULL,
    [OTROSLAB]     VARCHAR (200)                                                                    NULL,
    [ITRESTAU]     BIT                                                                              NULL,
    [ITCAPTRA]     BIT                                                                              NULL,
    [ITVOLCIR]     BIT                                                                              NULL,
    [ITEXATRA]     BIT                                                                              NULL,
    [ITRESCIR]     BIT                                                                              NULL,
    [ITHEMOST]     BIT                                                                              NULL,
    [AUTOENVP]     BIT                                                                              NOT NULL,
    [PRIOTRAN]     TINYINT                                                                          NOT NULL,
    [RESERCIR]     BIT                                                                              NOT NULL,
    [TIPOCIRU]     VARCHAR (50)                                                                     NULL,
    [FECHACIR]     DATETIME                                                                         NULL,
    [TRANPREV]     BIT                                                                              NOT NULL,
    [REACTRAN]     BIT                                                                              NOT NULL,
    [RTHEMOLI]     BIT                                                                              NULL,
    [RTFEBRIL]     BIT                                                                              NULL,
    [RTURTICA]     BIT                                                                              NULL,
    [OTRASRET]     VARCHAR (200)                                                                    NULL,
    [AOEMBPRE]     BIT                                                                              NOT NULL,
    [AOEMBAC]      BIT                                                                              NULL,
    [AOERIFE]      BIT                                                                              NOT NULL,
    [IDETIPHIS]    CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO]    NCHAR (10)                                                                       NOT NULL,
    [IPCODPACI]    VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]    CHAR (10)                                                                        NOT NULL,
    [CODCENATE]    CHAR (10)                                                                        NOT NULL,
    [FECORDMED]    DATETIME                                                                         NULL,
    [OBSERVACI]    VARCHAR (500)                                                                    NULL,
    [MANEXTPRO]    BIT                                                                              NOT NULL,
    [ESEMERGENCIA] BIT                                                                              NULL,
    [COOMBSDTO]    BIT                                                                              NULL,
    [RAI]          BIT                                                                              NULL,
    [AUTOCTRL]     BIT                                                                              NULL,
    [REARASANT]    BIT                                                                              NULL,
    [SOLEXTRAMU]   BIT                                                                              CONSTRAINT [DF_HCORHEMCO_SOLEXTRAMU] DEFAULT ((0)) NOT NULL,
    [HCCOMSANID]   INT                                                                              NULL,
    [INTERFAZ]     BIT                                                                              CONSTRAINT [DF_HCORHEMCO_INTERFAZ] DEFAULT ((0)) NOT NULL,
    [idAGASICITA]  INT                                                                              NULL,
    [SYNCMIRTH]             BIT                                                                              NULL,
    [AuthorizationEventId]  INT                                                                              NULL,
    CONSTRAINT [PK_HCORHEMCO] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCORHEMCO_ADCENATE] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCORHEMCO_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCORHEMCO_HCORHEMCO] FOREIGN KEY ([ID]) REFERENCES [dbo].[HCORHEMCO] ([ID]),
    CONSTRAINT [FK_HCORHEMCO_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_HCORHEMCO_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCORHEMCO].[CODDIAGNO]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCORHEMCO].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');




GO
CREATE NONCLUSTERED INDEX [IX_HCORHEMCO_IPCODPACI_NUMEFOLIO_NUMINGRES_IDETIPHIS]
    ON [dbo].[HCORHEMCO]([IPCODPACI] ASC, [NUMEFOLIO] ASC, [NUMINGRES] ASC, [IDETIPHIS] ASC)
    INCLUDE([MANEXTPRO]);


GO
CREATE NONCLUSTERED INDEX [IX_HCORHEMCO__IPCODPACI]
    ON [dbo].[HCORHEMCO]([IPCODPACI] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_HCORHEMCO_DashboardMedicalOrders_CareCenter_RequestDate]
    ON [dbo].[HCORHEMCO]([CODCENATE] ASC, [FECORDMED] DESC)
    INCLUDE([ID], [NUMINGRES], [NUMEFOLIO], [IPCODPACI])
    WHERE [MANEXTPRO] = 1 AND [FECORDMED] >= '20260102';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera BIT que identifica si la solicitud de hemocomponentes se sincronizó o guardó en la conexión de miRConnect (integración interfaz)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'SYNCMIRTH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identifica si guarda la conexión de miRConnect', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'SYNCMIRTH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'SYNCMIRTH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador INT de cita/agendamiento en tabla AGASICITA (FK), relaciona solicitud hemo con cita programada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'idAGASICITA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relación con la tabla AGASICITA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'idAGASICITA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'idAGASICITA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera BIT (1=sí/0=no) que determina si la solicitud se envió a interfaz externa de hemocomponentes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'INTERFAZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Determina si se envio o no a la interfaz hemocomponentes.  1 - si envio  0 - no envio  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'INTERFAZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'INTERFAZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador INT que relaciona registro con tabla HCCOMSAN, vincula comunicación/sanidad del hemocomponente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'HCCOMSANID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiene relación con la tabla HCCOMSAN', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'HCCOMSANID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'HCCOMSANID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera BIT (1=sí/0=no) que identifica si la solicitud de hemocomponente es extramural (fuera de sede)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'SOLEXTRAMU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identifica cuando la solicitud es extramural', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'SOLEXTRAMU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'SOLEXTRAMU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera BIT (1=positivo/0=negativo) que especifica si se realizó prueba de rastreo de anticuerpos en transfusión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'REARASANT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si se realizó rastreo de anticuerpos, 1-> si, 0-> no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'REARASANT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'REARASANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera BIT (1=positivo/0=negativo) de Auto Control; resultado de prueba de control de transfusión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'AUTOCTRL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Auto Control 1-> positivo, 0->negativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'AUTOCTRL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'AUTOCTRL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera BIT (1=positivo/0=negativo) prueba RAI (Reacción de Aglutinación Indirecta) en hemocomponente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'RAI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'RAI 1->Positivo, 0-> Negativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'RAI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'RAI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera BIT (1=positivo/0=negativo) prueba de Coombs directo para detectar anticuerpos en glóbulos rojos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'COOMBSDTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'COOMBS DTO 1-> Positivo, 0-> Negativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'COOMBSDTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'COOMBSDTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera BIT que especifica si la solicitud es de emergencia/urgencia al momento de hacer reserva de hemocomponentes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'ESEMERGENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si es de emergencia al momento de hacer reserva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'ESEMERGENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'ESEMERGENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera BIT que aplica para manejo externo, extramural o por procedimiento de la solicitud de transfusión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'MANEXTPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Aplica para manejo externo o extramural', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'MANEXTPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'MANEXTPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo VARCHAR(500) para notas, comentarios u observaciones clínicas sobre la solicitud de hemocomponentes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'OBSERVACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'OBSERVACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'OBSERVACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora DATETIME de registro/orden médica de la solicitud de hemocomponente por paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'FECORDMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Registro de la solicitud de hemocomponente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'FECORDMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'FECORDMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CHAR(10) del centro de atención (FK ADCENATEN) donde se solicita la transfusión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número CHAR(10) de ingreso/admisión del paciente (FK ADINGRESO) para la transfusión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código VARCHAR(25) del paciente, documento/cédula/identificación (FK INPACIENT, PII ofuscado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número NCHAR(10) de folio único de la solicitud de hemocomponentes para trazabilidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo CHAR(9) de historia clínica; identificador de historia física del paciente en atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedente obstétrico BIT (1=sí/0=no): eritroblastosis fetal en embarazos previos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'AOERIFE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Obstetricos: Eritroblasis Fetal  1 = si  0 = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'AOERIFE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'AOERIFE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedente obstétrico BIT (1=sí/0=no): embarazo actual; flag para pacientes embarazadas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'AOEMBAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Obstetricos: Embarazo Actual  1 = si  0 = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'AOEMBAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'AOEMBAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedente obstétrico BIT (1=sí/0=no): embarazos previos; historial obstétrico de la paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'AOEMBPRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Obstetricos: Embarazos Previos  1 = si  0 = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'AOEMBPRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'AOEMBPRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo VARCHAR(200) para describir otras reacciones transfusionales no clasificadas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'OTRASRET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Otras Reacciones Transfusionales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'OTRASRET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'OTRASRET';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reacción transfusional BIT (1=sí/0=no): urticaria/rash alérgico post-transfusión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'RTURTICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Urticariantes  1 = si  0 = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'RTURTICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'RTURTICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reacción transfusional BIT (1=sí/0=no): fiebre (no hemolítica) posterior a transfusión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'RTFEBRIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Febriles   1 = si  0 = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'RTFEBRIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'RTFEBRIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reacción transfusional BIT (1=sí/0=no): hemolítica por incompatibilidad de hemocomponentes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'RTHEMOLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hemoliticas  1 = si  0 = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'RTHEMOLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'RTHEMOLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera BIT (1=sí/0=no) que indica presencia de reacciones transfusionales en procedimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'REACTRAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Reacciones Transfusionales:  1 = si  0 = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'REACTRAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'REACTRAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera BIT (1=sí/0=no) que indica antecedente de transfusiones previas en el paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'TRANPREV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Transfusiones Previas:  1 = si  0 = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'TRANPREV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'TRANPREV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora DATETIME programada de cirugía para la cual se reserva hemocomponente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'FECHACIR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Cirugía', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'FECHACIR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'FECHACIR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo VARCHAR(50) que describe el tipo de procedimiento quirúrgico programado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'TIPOCIRU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Cirugía', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'TIPOCIRU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'TIPOCIRU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera BIT (1=sí/0=no) que indica si la solicitud es reserva de hemocomponentes para cirugía', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'RESERCIR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Rerseva para cirugía  1 = si  0 = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'RESERCIR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'RESERCIR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel TINYINT (1-4) de prioridad: 1=Emergencia(≤15min), 2=Urgencia(≤1h), 3=Urgencia diferida(≤3h), 4=Normal(≤24h)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'PRIOTRAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Prioridad para la transfusion:  1. Emergencia (hasta 15 min)  2.  Urgencia (hasta 1 hora)  3.  Urgencia Diferida (hasta 3 horas)  4. Normal (hasta 24 horas)  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'PRIOTRAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'PRIOTRAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera BIT (1=sí/0=no) autorización de enviar hemocomponente sin pruebas cruzadas por urgencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'AUTOENVP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Por la urgencia de la solicitud autoriza enviar el (los) producto(s) solicitado(s) sin pruebas cruzadas  1 = si  0 = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'AUTOENVP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'AUTOENVP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicación transfusional BIT (1=sí/0=no): Hemostasis/coagulación, control de sangrado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'ITHEMOST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Opcion Indicaciones para la Transfucion:   Hemostasis 1-> si, 0-> no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'ITHEMOST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'ITHEMOST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicación transfusional BIT (1=sí/0=no): Reserva para cirugía programada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'ITRESCIR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Opcion Indicaciones para la Transfucion:   Reserva para Cirugía 1-> si, 0-> no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'ITRESCIR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'ITRESCIR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicación transfusional BIT (1=sí/0=no): Exanguinotransfusión; recambio de sangre completo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'ITEXATRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Opciones Indicaciones para la Transfucion:   Exaguineo Transfusion 1-> si, 0-> no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'ITEXATRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'ITEXATRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicación transfusional BIT (1=sí/0=no): Volumen circulatorio; reposición de pérdida sanguínea', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'ITVOLCIR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Opciones Indicaciones para la Transfucion:   Volumen Circulatorio 1-> si, 0-> no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'ITVOLCIR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'ITVOLCIR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicación transfusional BIT (1=sí/0=no): Capacidad transportadora de oxígeno en anemia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'ITCAPTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicaciones para la Transfucion:   Capacidad Transportadora 1-> si, 0-> no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'ITCAPTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'ITCAPTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicación transfusional BIT (1=sí/0=no): Restaurar o mantener volumen/capacidad transfusional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'ITRESTAU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Opcion Indicaciones para la Transfucion:   Restaurar o Mantener. 1-> si, 0-> no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'ITRESTAU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'ITRESTAU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo VARCHAR(200) para otros estudios de laboratorio adicionales en solicitud hemocomponente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'OTROSLAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Otros Laboratorios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'OTROSLAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'OTROSLAB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor VARCHAR(10) del nivel de Fibrinógeno (factor coagulación VII) en laboratorio transfusional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'FIBRINOG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fibrinogeno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'FIBRINOG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'FIBRINOG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor VARCHAR(10) de Recuento Plaquetario; nivel de plaquetas para transfusión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'RCTOPLAQ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'RCTO Plaquetas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'RCTOPLAQ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'RCTOPLAQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor VARCHAR(10) de Hematocrito; porcentaje de volumen de glóbulos rojos en sangre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'HTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'HTO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'HTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'HTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor VARCHAR(10) de Hemoglobina; concentración de hemoglobina en laboratorio preanálisis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'HB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'HB', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'HB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'HB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código diagnóstico CHAR(4) (FK INDIAGNOS, PII ofuscado) que justifica la solicitud de hemocomponentes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador INT (PK) único auto-incremental del registro de solicitud de hemocomponentes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la tabla de hemocomponentes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de órdenes de hemoterapia (transfusión de sangre y hemoderivados) por paciente e ingreso. Guarda los valores de laboratorio previos a la transfusión, indicadores de antecedentes transfusionales, reacciones adversas previas, tipo y fecha de cirugía, parámetros de seguridad y configuración del proceso transfusional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO';
