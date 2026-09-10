CREATE TABLE [dbo].[CALTECNOVIGILANCIA] (
    [ID]                   INT                                                                          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDCALREPORTE]         INT                                                                          NOT NULL,
    [NOMBREINSTI]          VARCHAR (100)                                                                NOT NULL,
    [AUUBICACI]            CHAR (20)                                                                    NOT NULL,
    [NIT]                  VARCHAR (15)                                                                 NOT NULL,
    [NIVELCOMPLEJI]        INT                                                                          NULL,
    [NATURALEZA]           INT                                                                          NOT NULL,
    [CODDIAGNO]            CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)') NOT NULL,
    [NOMBREGENERICO]       VARCHAR (100)                                                                NOT NULL,
    [NOMBRECOMERCAIL]      VARCHAR (100)                                                                NOT NULL,
    [REGISTROSANITARIO]    VARCHAR (100)                                                                NULL,
    [LOTE]                 VARCHAR (15)                                                                 NULL,
    [MODELO]               VARCHAR (15)                                                                 NULL,
    [REFERENCIA]           VARCHAR (15)                                                                 NULL,
    [SERIAL]               VARCHAR (15)                                                                 NULL,
    [NOMBREFABRICANTE]     VARCHAR (100)                                                                NOT NULL,
    [NOMBREIPORTADOR]      VARCHAR (100)                                                                NULL,
    [AREAUFUCODIGO]        CHAR (10)                                                                    NULL,
    [DISPOSITIVOUTILI]     BIT                                                                          NULL,
    [FECHAEVENTO]          DATE                                                                         NOT NULL,
    [FECHAELABORACION]     DATE                                                                         NOT NULL,
    [DETENCIONANTES]       BIT                                                                          NULL,
    [DETENCIONDURANTE]     BIT                                                                          NULL,
    [DETENCIONDESPUES]     BIT                                                                          NULL,
    [CLASIEVENADVESERIO]   BIT                                                                          NULL,
    [CLASIEVENADVNOESERIO] BIT                                                                          NULL,
    [CLASIINCIADVESERIO]   BIT                                                                          NULL,
    [CLASIINCIADVENOSERIO] BIT                                                                          NULL,
    [DESCRIPEVENTO]        VARCHAR (MAX)                                                                NOT NULL,
    [DESENLACEEVENTO]      INT                                                                          NOT NULL,
    [OTRODESENLACE]        VARCHAR (100)                                                                NULL,
    [CAUSA500]             BIT                                                                          NULL,
    [CAUSA510]             BIT                                                                          NULL,
    [CAUSA520]             BIT                                                                          NULL,
    [CAUSA530]             BIT                                                                          NULL,
    [CAUSA540]             BIT                                                                          NULL,
    [CAUSA550]             BIT                                                                          NULL,
    [CAUSA560]             BIT                                                                          NULL,
    [CAUSA570]             BIT                                                                          NULL,
    [CAUSA580]             BIT                                                                          NULL,
    [CAUSA590]             BIT                                                                          NULL,
    [CAUSA600]             BIT                                                                          NULL,
    [CAUSA610]             BIT                                                                          NULL,
    [CAUSA620]             BIT                                                                          NULL,
    [CAUSA630]             BIT                                                                          NULL,
    [CAUSA640]             BIT                                                                          NULL,
    [CAUSA650]             BIT                                                                          NULL,
    [CAUSA660]             BIT                                                                          NULL,
    [CAUSA670]             BIT                                                                          NULL,
    [CAUSA680]             BIT                                                                          NULL,
    [CAUSA690]             BIT                                                                          NULL,
    [CAUSA700]             BIT                                                                          NULL,
    [CAUSA710]             BIT                                                                          NULL,
    [CAUSA720]             BIT                                                                          NULL,
    [CAUSA730]             BIT                                                                          NULL,
    [CAUSA740]             BIT                                                                          NULL,
    [CAUSA750]             BIT                                                                          NULL,
    [CAUSA760]             BIT                                                                          NULL,
    [CAUSA770]             BIT                                                                          NULL,
    [CAUSA780]             BIT                                                                          NULL,
    [CAUSA790]             BIT                                                                          NULL,
    [CAUSA800]             BIT                                                                          NULL,
    [CAUSA810]             BIT                                                                          NULL,
    [CAUSA820]             BIT                                                                          NULL,
    [CAUSA830]             BIT                                                                          NULL,
    [CAUSA840]             BIT                                                                          NULL,
    [CAUSA850]             BIT                                                                          NULL,
    [CAUSA860]             BIT                                                                          NULL,
    [CAUSA870]             BIT                                                                          NULL,
    [CAUSA880]             BIT                                                                          NULL,
    [CAUSA890]             BIT                                                                          NULL,
    [CAUSA900]             BIT                                                                          NULL,
    [CAUSA910]             BIT                                                                          NULL,
    [CAUSA920]             BIT                                                                          NULL,
    [CAUSA930]             BIT                                                                          NULL,
    [CAUSA940]             BIT                                                                          NULL,
    [CAUSA950]             BIT                                                                          NULL,
    [CAUSA960]             BIT                                                                          NULL,
    [ACCICORRECTIVA]       VARCHAR (MAX)                                                                NULL,
    [REPORFABRICANTE]      BIT                                                                          NULL,
    [FECHAREPORTE]         DATE                                                                         NULL,
    [DISPOMEDICO]          BIT                                                                          NULL,
    [ENVIADISPOSITIVO]     BIT                                                                          NULL,
    [FECHAENVIO]           DATE                                                                         NULL,
    [NOMBRE]               VARCHAR (100)                                                                NULL,
    [PROFESION]            CHAR (4)                                                                     NULL,
    [ORGANIZACION]         CHAR (10)                                                                    NULL,
    [DIRECCION]            VARCHAR (100)                                                                NULL,
    [TELEFONO]             VARCHAR (15)                                                                 NULL,
    [UBICACION]            CHAR (20)                                                                    NULL,
    [CORREO]               VARCHAR (100)                                                                NULL,
    [FECHANOTIFICA]        DATE                                                                         NULL,
    [AUTORIZA]             BIT                                                                          NULL,
    CONSTRAINT [PK_CALTECNOVIGILANCIA] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CALTECNOVIGILANCIA_CALREPORTE] FOREIGN KEY ([IDCALREPORTE]) REFERENCES [dbo].[CALREPORTE] ([ID]),
    CONSTRAINT [FK_CALTECNOVIGILANCIA_CALTECNOVIGILANCIA] FOREIGN KEY ([UBICACION]) REFERENCES [dbo].[INUBICACI] ([AUUBICACI]),
    CONSTRAINT [FK_CALTECNOVIGILANCIA_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_CALTECNOVIGILANCIA_INUBICACI] FOREIGN KEY ([AUUBICACI]) REFERENCES [dbo].[INUBICACI] ([AUUBICACI]),
    CONSTRAINT [FK_CALTECNOVIGILANCIA_INUNIFUNC] FOREIGN KEY ([ORGANIZACION]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_CALTECNOVIGILANCIA_INUNIFUNC1] FOREIGN KEY ([AREAUFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[CALTECNOVIGILANCIA].[CODDIAGNO]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera booleana que autoriza la divulgación de información y origen del reporte al fabricante/importador. True=Sí, False=No. PII: consentimiento informado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'AUTORIZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autoriza la divulgación de la información y origen del reporte al fabricante  True = Si     False = NO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'AUTORIZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'AUTORIZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de notificación del evento adverso o incidente al ente regulador, fabricante o autoridad sanitaria (DATE).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHANOTIFICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de notificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHANOTIFICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHANOTIFICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Correo electrónico institucional del reportante, profesional de salud o contacto responsable (VARCHAR 100, PII).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CORREO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Correo electrónico institucional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CORREO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CORREO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ubicación geográfica, sede o campus donde ocurrió el evento/incidente (FK → INUBICACI, CHAR 20).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'UBICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ubicación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'UBICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'UBICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Teléfono de contacto del reportante o unidad funcional (VARCHAR 15, PII).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'TELEFONO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Teléfono', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'TELEFONO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'TELEFONO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección física, domicilio o ubicación postal del reportante u organización (VARCHAR 100, PII).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DIRECCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dirección', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DIRECCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DIRECCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de organización y unidad funcional a la que pertenece el reportante (FK → INUNIFUNC, CHAR 10).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ORGANIZACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Organización y área a la que pertenece', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ORGANIZACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ORGANIZACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de profesión u ocupación del reportante (médico, enfermero, tecnólogo, etc., CHAR 4).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PROFESION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Profesion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PROFESION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PROFESION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo del reportante, profesional de salud o persona responsable de reportar (VARCHAR 100, PII).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOMBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Informacion del reportante   Nombre ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOMBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOMBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que se envió el dispositivo médico al fabricante, importador o distribuidor (DATE).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHAENVIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de envió', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHAENVIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHAENVIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si el dispositivo médico fue enviado al fabricante/importador/distribuidor. True=Sí, False=No (BIT).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ENVIADISPOSITIVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿Se ha enviado el dispositivo médico al Fabricante/ Importador/Distribuidor?  True = Si    False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ENVIADISPOSITIVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ENVIADISPOSITIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si el dispositivo médico está disponible para evaluación técnica o pericial. True=Sí, False=No (BIT).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DISPOMEDICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿Dispositivo médico disponible para evaluación?  True = Si    False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DISPOMEDICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DISPOMEDICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que se elaboró y reportó el incidente adverso al ente regulatorio (DATE).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHAREPORTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de reporte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHAREPORTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHAREPORTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si se reportó el evento al fabricante o distribuidor del dispositivo. True=Sí, False=No (BIT).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'REPORFABRICANTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Reportó al Fabricante/Distribuidor  True = Si      False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'REPORFABRICANTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'REPORFABRICANTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de acciones correctivas y preventivas iniciadas por la institución (VARCHAR MAX).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ACCICORRECTIVA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Acciones correctivas y preventivas iniciadas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ACCICORRECTIVA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ACCICORRECTIVA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código 960: Desgaste, deterioro natural o envejecimiento del dispositivo médico (BIT flag).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA960';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'960 Desgaste', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA960';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA960';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código 950: Error de uso, manipulación incorrecta o inobservancia de instrucciones (BIT flag).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA950';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'950 Error de uso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA950';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA950';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código 940: Limitación o inadecuación de capacidad de uso del dispositivo (BIT flag).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA940';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'940 Capacidad de uso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA940';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA940';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código 930: Causa sin definir, indeterminada o no especificada (BIT flag).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA930';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'930 Sin definir', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA930';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA930';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código 920: Falla durante transporte y entrega del dispositivo (BIT flag).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA920';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'920 Trasporte y entrega', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA920';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA920';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código 910: Deficiencia en entrenamiento, capacitación o inducción de usuarios (BIT flag).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA910';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'910 Entrenamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA910';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA910';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código 900: Manipulación maliciosa, falsificación, sabotaje o acto delictivo (BIT flag).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA900';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'900 Manipulación, falsificación, sabotaje', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA900';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA900';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código 890: Incumplimiento de condiciones de almacenamiento, temperatura o humedad (BIT flag).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA890';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'890 Condiciones de almacenamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA890';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA890';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código 880: Falla en proceso de esterilización, desinfección o limpieza (BIT flag).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA880';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'880 Esterilización/desinfección/limpieza', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA880';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA880';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código 870: Defecto, fallo o corrupción de software, firmware o sistema operativo (BIT flag).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA870';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'870 Software', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA870';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA870';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código 860: Exposición a radiación ionizante o no ionizante (BIT flag).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA860';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'860 Radiación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA860';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA860';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código 850: Deficiencia en aseguramiento de calidad o control de procesos institucionales (BIT flag).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA850';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'850 Aseguramiento de la calidad en la institución para la atención de salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA850';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA850';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código 840: Medida de protección insuficiente, barrera defectuosa o dispositivo fallido (BIT flag).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA840';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'840 Medida de protección', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA840';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA840';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código 830: Falla de fuente de energía, batería, alimentación o cable de poder (BIT flag).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA830';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'830 Fuente de energía', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA830';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA830';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código 820: Condición clínica, patología preexistente o estado del paciente (BIT flag).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA820';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'820 Condiciones del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA820';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA820';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código 810: Variación anatómica o fisiológica del paciente incompatible con uso (BIT flag).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA810';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'810 Anatomía/Fisiología del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA810';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA810';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código 800: Defecto en empaque, envoltorio, integridad o sellado del producto (BIT flag).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA800';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'800 Empaque', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA800';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA800';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código 790: Otras causas no clasificadas en los códigos anteriores (BIT flag).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA790';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'790 Otros', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA790';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA790';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código 780: Evento no relacionado con dispositivo médico, causa externa (BIT flag).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA780';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'780 No relacionado con el dispositivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA780';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA780';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código 770: Condiciones no higiénicas, contaminación ambiental o falta de asepsia (BIT flag).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA770';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'770 Condiciones no higiénicas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA770';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA770';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código 760: Falla de componentes mecánicos, articulaciones, engranes o movimientos (BIT flag).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA760';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'760 Componentes Mecánicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA760';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA760';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código 750: Defecto de material, composición, aleación o sustancia tóxica (BIT flag).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA750';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'750 Material', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA750';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA750';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código 740: Defecto de fabricación, deficiencia en proceso productivo (BIT flag).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA740';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'740 Fabricación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA740';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA740';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código 730: Falla en mantenimiento preventivo, correctivo o calibración (BIT flag).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA730';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'730 Mantenimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA730';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA730';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código 720: Falla de escape, sellado, fugas o pérdida de estanqueidad (BIT flag).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA720';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'720 Escape/sellado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA720';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA720';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código 710: Deficiencia en instrucciones de uso, etiquetado, advertencias o rotulado (BIT flag).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA710';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'710 Instrucciones para uso y rotulado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA710';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA710';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código 700: Incompatibilidad entre dispositivos, sistemas o interfaces (BIT flag).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA700';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'700 Incompatibilidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA700';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA700';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código 690: Ambiente inapropiado, temperatura, humedad, altitud o contaminación (BIT flag).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA690';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'690 Ambiente inapropiado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA690';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA690';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código 680: Falla de dispositivo implantable, rechazo, migración o desplazamiento (BIT flag).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA680';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'680 Falla en el dispositivo implantable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA680';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA680';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código 670: Resultado falso de prueba diagnóstica, sensibilidad o especificidad reducida (BIT flag).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA670';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'670 Resultado falso de la prueba', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA670';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA670';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código 660: Falso positivo, resultado erróneo que indica presencia de condición inexistente (BIT flag).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA660';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'660 Falso positivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA660';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA660';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código 650: Falso negativo, resultado erróneo que oculta condición presente (BIT flag).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA650';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'650 Falso negativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA650';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA650';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código 640: Vencimiento de fecha de expiración, producto caducado o fuera de vigencia (BIT flag).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA640';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'640 Fecha de expiración', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA640';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA640';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código 630: Interferencia electromagnética EIM, radiofrecuencia o campos magnéticos (BIT flag).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA630';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'630 Interferencia Eletromagnética EIM', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA630';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA630';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código 620: Contacto eléctrico, descarga, electrocución o arco eléctrico (BIT flag).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA620';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'620 Contacto eléctrico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA620';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA620';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código 610: Falla de circuito eléctrico, cortocircuito o desconexión (BIT flag).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA610';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'610 Circuito eléctrico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA610';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA610';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código 600: Defecto de componente eléctrico, resistencia, capacitor, transistor (BIT flag).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA600';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'600 Componente eléctrico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA600';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA600';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código 590: Desconexión, desacoplamiento o separación no intencional de componentes (BIT flag).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA590';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'590 Desconexión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA590';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA590';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código 580: Defecto de diseño, error en ingeniería o especificación inadecuada (BIT flag).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA580';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'580 Diseño', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA580';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA580';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código 570: Contaminación posterior a producción, durante distribución o almacenamiento (BIT flag).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA570';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'570 Contaminación post-producción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA570';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA570';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código 560: Contaminación durante proceso de fabricación o manufactura (BIT flag).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA560';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'560 Contaminación durante la producción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA560';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA560';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código 550: Falla de hardware de computador, procesador, memoria RAM o disco duro (BIT flag).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA550';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'550 Hadware de computador', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA550';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA550';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código 540: Deficiencia en calibración, ajuste, precisión o desgaste de componentes (BIT flag).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA540';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'540 Calibración', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA540';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA540';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código 530: Uso de material biológico, virus, bacteria, prión o contaminante vivo (BIT flag).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA530';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'530 Uso de material biológico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA530';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA530';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código 520: Falla en sistema de alarma, alerta o mecanismo de notificación (BIT flag).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA520';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'520 Falla en la alarma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA520';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA520';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código 510: Respuesta fisiológica anormal, inesperada, paradójica o inexplicable (BIT flag).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA510';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'510 Respuesta fisiologica anormal o inexplicable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA510';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA510';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código 500: Uso anormal, fuera de indicación, protocolo impropio o desviación (BIT flag).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA500';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'500 Uso anoramal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA500';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA500';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especificación textual cuando se selecciona opción ''''Otro'''' en desenlace evento (VARCHAR 100).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'OTRODESENLACE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Si señaló la opción Otro especifique ¿Cuál?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'OTRODESENLACE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'OTRODESENLACE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación del desenlace: 1=Muerte, 2=Daño función/estructura corporal, 3=Enfermedad que amenaza vida, 4=Intervención médica/quirúrgica, 5=Incapacidad permanente parcial, 6=Hospitalización/prolongación, 7=Malformación congénita, 8=Sin daño, 9=Otro (INT).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DESENLACEEVENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Desenlace del evento /incidente adverso  1= Muerte  2=Daño de una función o estructura corporal  3=Enfermedad o daño que amenace la vida  4=Requiere intervención médica o quirúrgica  5=incapacidad permanente parcial  6=Hospitalización o prolongación de la misa  7=Malformación congénita  8=No hubo daño  9=Otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DESENLACEEVENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DESENLACEEVENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Narración detallada del evento adverso, incidente o situación que desencadenó el reporte (VARCHAR MAX).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DESCRIPEVENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción del evento o incidente adverso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DESCRIPEVENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DESCRIPEVENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera que indica clasificación como incidente adverso no serio. True=Seleccionado (BIT).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CLASIINCIADVENOSERIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clasificación    Incidente adverso no serio = True = Seleccionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CLASIINCIADVENOSERIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CLASIINCIADVENOSERIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera que indica clasificación como incidente adverso serio. True=Seleccionado (BIT).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CLASIINCIADVESERIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clasificación  Incidente adverso serio = True = Seleccionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CLASIINCIADVESERIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CLASIINCIADVESERIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera que indica clasificación como evento adverso no serio. True=Seleccionado (BIT).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CLASIEVENADVNOESERIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clasificación  Evento adverso no serio = True = Seleccionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CLASIEVENADVNOESERIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CLASIEVENADVNOESERIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera que indica clasificación como evento adverso serio. True=Seleccionado (BIT).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CLASIEVENADVESERIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clasificación  Evento adverso serio = True = Seleccionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CLASIEVENADVESERIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CLASIEVENADVESERIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de detección del evento/incidente después del uso del dispositivo médico. True=Seleccionado (BIT).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DETENCIONDESPUES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Detección del evento/incidente adverso  Despues del uso del DM = True = Seleccionado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DETENCIONDESPUES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DETENCIONDESPUES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de detección del evento/incidente durante el uso del dispositivo médico. True=Seleccionado (BIT).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DETENCIONDURANTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Detección del evento/incidente adverso  Durante del uso del DM = True = Seleccionado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DETENCIONDURANTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DETENCIONDURANTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de detección del evento/incidente antes del uso o durante inspección inicial. True=Seleccionado (BIT).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DETENCIONANTES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Detección del evento/incidente adverso  Antes del uso del DM = True = Seleccionado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DETENCIONANTES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DETENCIONANTES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de elaboración y registro del reporte de tecnovigilancia en el sistema (DATE).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHAELABORACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha elaboracion del reporte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHAELABORACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHAELABORACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de ocurrencia del evento adverso, incidente o problema asociado al dispositivo (DATE).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHAEVENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del evento /Incidente adverso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHAEVENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHAEVENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si el dispositivo médico fue utilizado más de una vez, reutilizable o de un solo uso. True=Sí/múltiple, False=No (BIT).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DISPOSITIVOUTILI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indique si el dispositivo médico ha sido utilizado más de una vez  True = si   False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DISPOSITIVOUTILI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DISPOSITIVOUTILI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de unidad funcional, área clínica o servicio donde se utilizaba el dispositivo (FK → INUNIFUNC, CHAR 10).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'AREAUFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Area de funcionamiento del dispositivo medico en el momento del evento/incidente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'AREAUFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'AREAUFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o razón social del importador, distribuidor o comerciante del dispositivo (VARCHAR 100).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOMBREIPORTADOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre o razon social del importador y/o distribuidor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOMBREIPORTADOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOMBREIPORTADOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o razón social del fabricante, productor u origen manufacturero del dispositivo (VARCHAR 100).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOMBREFABRICANTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre o Razon social del fabricante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOMBREFABRICANTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOMBREFABRICANTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de serie único del dispositivo médico para identificación y trazabilidad (VARCHAR 15).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'SERIAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Serial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'SERIAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'SERIAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de referencia, modelo comercial o código interno del fabricante (VARCHAR 15).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'REFERENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Referencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'REFERENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'REFERENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Designación del modelo o versión específica del dispositivo médico (VARCHAR 15).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'MODELO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Modelo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'MODELO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'MODELO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de lote de producción, identificador de fabricación o número de batch (VARCHAR 15).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'LOTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Lote', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'LOTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'LOTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de registro sanitario, permiso de comercialización o autorización regulatoria (VARCHAR 100).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'REGISTROSANITARIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Registro sanitario o permiso de comercializacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'REGISTROSANITARIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'REGISTROSANITARIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre comercial, denominación de marca o denominación de venta del dispositivo (VARCHAR 100).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOMBRECOMERCAIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre comercial del dispositivo Medico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOMBRECOMERCAIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOMBRECOMERCAIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre genérico, clasificación o descripción técnica según nomenclatura (VARCHAR 100).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOMBREGENERICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre generico del dispositivo Medico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOMBREGENERICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOMBREGENERICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CIE-10 de diagnóstico inicial del paciente (FK → INDIAGNOS, CHAR 4, Masked PII).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Diagnostico inicial del paciente ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación de naturaleza de la institución: 1=Pública, 2=Privada, 3=Mixta (INT).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NATURALEZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Maturaleza  1 = Publica  2 =  Privada  3 = Mexta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NATURALEZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NATURALEZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel de complejidad de la institución: 1=Baja, 2=Mediana, 3=Alta (INT).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NIVELCOMPLEJI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nivel de Complejidad   1 = Baja  2 = Mediana  3 = Alta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NIVELCOMPLEJI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NIVELCOMPLEJI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de identificación tributaria o NIT de la institución de salud (VARCHAR 15, PII).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nit del institucion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de ubicación, sede, municipio o región de la institución (FK → INUBICACI, CHAR 20).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'AUUBICACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ubicacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'AUUBICACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'AUUBICACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre legal o denominación de la institución de salud, centro de atención (VARCHAR 100).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOMBREINSTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la institucion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOMBREINSTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOMBREINSTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del reporte asociado, referencia a tabla CALREPORTE (INT, FK).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'IDCALREPORTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del reporte que guarda igual que id del tabla CALREPORTE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'IDCALREPORTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'IDCALREPORTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumerado de este registro de tecnovigilancia (INT IDENTITY, PK).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reportes de tecnovigilancia: eventos e incidentes adversos relacionados con dispositivos médicos o tecnología sanitaria. Registra datos del dispositivo involucrado, la institución, la clasificación del evento, sus causas posibles y las acciones correctivas tomadas, según la normativa colombiana de vigilancia sanitaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA';
