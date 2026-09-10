CREATE TABLE [dbo].[CALREACTIVOVIGILANCIA] (
    [ID]                           INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDCALREPORTE]                 INT           NULL,
    [NOMBREINSTITUCION]            VARCHAR (200) NOT NULL,
    [NIT]                          VARCHAR (15)  NOT NULL,
    [NATURALEZA]                   INT           NOT NULL,
    [NIVELCOMPLEJIDAD]             INT           NOT NULL,
    [UBICACION]                    CHAR (20)     NOT NULL,
    [DIRECCION]                    VARCHAR (200) NULL,
    [TELEFONO]                     VARCHAR (20)  NULL,
    [CORREO]                       VARCHAR (100) NULL,
    [FECHAREPORTE]                 DATE          NOT NULL,
    [NOMBRECOMERCIAL]              VARCHAR (200) NOT NULL,
    [REGISTROSANITARIO]            VARCHAR (200) NOT NULL,
    [LOTE]                         VARCHAR (50)  NOT NULL,
    [FECHAVENCIMIENTO]             DATE          NOT NULL,
    [PROCEDENCIA]                  INT           NOT NULL,
    [REQUIEREFRIO]                 BIT           NOT NULL,
    [TEMPERATURA]                  INT           NOT NULL,
    [NOMBRERAZONSOCIALIMPORTA]     VARCHAR (200) NOT NULL,
    [SERVICIOREACTIVO]             INT           NOT NULL,
    [CUALOTROSERVICIO]             VARCHAR (200) NULL,
    [CUMPLIERONCONDICIONES]        BIT           NOT NULL,
    [PRODUCTOCERTIFICADO]          BIT           NOT NULL,
    [FECHAOCURRENCIA]              DATE          NOT NULL,
    [FECHAELABORACIONREPORTE]      DATE          NOT NULL,
    [DETENCCIONEFECTOINDESEADO]    INT           NOT NULL,
    [PROBOEMAREACTIVO]             INT           NOT NULL,
    [CLASIFICACIONEFECTOINDESEADO] INT           NOT NULL,
    [DESCRIPCIONEFECTOINDESEADO]   VARCHAR (500) NOT NULL,
    [DANOCORPORAL]                 BIT           NULL,
    [MUERTE]                       BIT           NULL,
    [RETRASO]                      BIT           NULL,
    [HOSPITALIZACION]              BIT           NULL,
    [PRESCRIPCION]                 BIT           NULL,
    [TRATAMIENTOINAPROP]           BIT           NULL,
    [TRANSFUSIONBIOLO]             BIT           NULL,
    [INTERVENMEDICA]               BIT           NULL,
    [INTERVENQX]                   BIT           NULL,
    [INTERVENPSICOLO]              BIT           NULL,
    [DIAGNOSINCORREC]              BIT           NULL,
    [OTRA]                         BIT           NULL,
    [CUALOTRODESENLACE]            VARCHAR (200) NULL,
    [DETECTOCAUSA]                 BIT           NULL,
    [CAUSAPROBABLE]                VARCHAR (200) NULL,
    [NOTIFICOIMPORTADOR]           BIT           NULL,
    [NOTIFICOFABRICANTE]           BIT           NULL,
    [NOTIFICOCOMERCIALIZA]         BIT           NULL,
    [NOTIFICODISTRIBUIDOR]         BIT           NULL,
    [FECHANOTIFICACION]            DATE          NULL,
    [ENVIADOREACTIVO]              BIT           NULL,
    [INSTIPROGRAMARIESGOS]         BIT           NULL,
    [REALIZOTIPOANALISIS]          BIT           NULL,
    [HERRAMIENTAUTILIZO]           INT           NULL,
    [CUALOTRAHERRAMIENTA]          VARCHAR (200) NULL,
    [DESCRIPCIONCAUSA]             VARCHAR (MAX) NULL,
    [INICIOACCIONESMEJORA]         BIT           NULL,
    [ACCIONESMEJORAM]              VARCHAR (MAX) NULL,
    [NOMBREREPORTANTE]             VARCHAR (200) NULL,
    [IDENTIREPORTANTE]             VARCHAR (15)  NULL,
    [PROFESIONREPORTA]             CHAR (4)      NULL,
    [CARGOREPORTA]                 VARCHAR (200) NULL,
    [AREAREPORTA]                  VARCHAR (200) NULL,
    [DIRECCIONREPORTA]             VARCHAR (200) NULL,
    [UBICACIONREPORTA]             CHAR (20)     NULL,
    [TELEFONOREPORTA]              VARCHAR (20)  NULL,
    [CELULARREPORTA]               VARCHAR (12)  NULL,
    [FECHANOTIREPORTAN]            DATE          NULL,
    [CORREOREPORTA]                VARCHAR (100) NULL,
    [AUTORIZADIVULGACION]          BIT           NULL,
    CONSTRAINT [PK_CALREACTIVOVIGILANCIA] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CALREACTIVOVIGILANCIA_CALREPORTE] FOREIGN KEY ([IDCALREPORTE]) REFERENCES [dbo].[CALREPORTE] ([ID]),
    CONSTRAINT [FK_CALREACTIVOVIGILANCIA_INUBICACI] FOREIGN KEY ([UBICACION]) REFERENCES [dbo].[INUBICACI] ([AUUBICACI]),
    CONSTRAINT [FK_CALREACTIVOVIGILANCIA_INUBICACI1] FOREIGN KEY ([UBICACIONREPORTA]) REFERENCES [dbo].[INUBICACI] ([AUUBICACI])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: autorización para divulgar información y origen del reporte al fabricante e/o importador del reactivo. Valor: 1=Sí/True, 0=No/False. PII - Consentimiento informado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'AUTORIZADIVULGACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿Autoriza la divulgación de la información y origen del reporte al fabricante y/o importador?  True = SiFalse = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'AUTORIZADIVULGACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'AUTORIZADIVULGACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Correo electrónico personal de contacto del reportante, para notificaciones sobre vigilancia y farmacovigilancia del reactivo. VARCHAR(100), PII.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CORREOREPORTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Correo electrónico personal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CORREOREPORTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CORREOREPORTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de notificación al reportante sobre el evento adverso o incidente detectado. Tipo DATE, auditoría de comunicación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHANOTIREPORTAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de notificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHANOTIREPORTAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHANOTIREPORTAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de teléfono celular del reportante para contacto inmediato. VARCHAR(12), PII - Identification_Ofuscado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CELULARREPORTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Celular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CELULARREPORTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CELULARREPORTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Teléfono de domicilio del reportante, línea de correspondencia. VARCHAR(20), PII - contacto alterno.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'TELEFONOREPORTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Teléfono de domicilio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'TELEFONOREPORTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'TELEFONOREPORTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ubicación geográfica/departamental del reportante (FK a INUBICACI). CHAR(20), referencia territorial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'UBICACIONREPORTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ubicación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'UBICACIONREPORTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'UBICACIONREPORTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección de correspondencia postal del reportante para envío de documentación. VARCHAR(200), PII.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DIRECCIONREPORTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dirección de correspondencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DIRECCIONREPORTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DIRECCIONREPORTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Área, departamento o unidad funcional de la organización a la que pertenece el reportante. VARCHAR(200), contexto laboral.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'AREAREPORTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Área de la organización a la que pertenece', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'AREAREPORTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'AREAREPORTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cargo, puesto o rol laboral del reportante en la institución. VARCHAR(200), perfil profesional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CARGOREPORTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cargo del reportante ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CARGOREPORTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CARGOREPORTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Profesión del reportante: médico, tecnólogo, farmacéutico, bioquímico, etc. CHAR(4), credencial de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PROFESIONREPORTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Profesion del reportante ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PROFESIONREPORTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PROFESIONREPORTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de identificación del reportante: cédula, pasaporte o documento de identidad. VARCHAR(15), PII - Identification_Ofuscado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'IDENTIREPORTANTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de identificacion del reportante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'IDENTIREPORTANTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'IDENTIREPORTANTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo del reportante del evento adverso/incidente del reactivo. VARCHAR(200), PII - Identification_Ofuscado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOMBREREPORTANTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación del reportante  Nombre del Reportante ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOMBREREPORTANTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOMBREREPORTANTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de acciones de mejoramiento preventivas/correctivas implementadas tras el efecto indeseado. VARCHAR(MAX), texto de auditoría.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ACCIONESMEJORAM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'cuales Acciones mejoramiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ACCIONESMEJORAM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ACCIONESMEJORAM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: ¿se iniciaron acciones de mejoramiento (preventivas/correctivas)? 1=Sí, 0=No. BIT, gestión de riesgos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'INICIOACCIONESMEJORA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿Inició acciones de mejoramiento (preventivas/correctivas)?  True = SiFalse = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'INICIOACCIONESMEJORA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'INICIOACCIONESMEJORA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción detallada de la causa raíz o factores que favorecieron el efecto indeseado reportado. VARCHAR(MAX), análisis de causalidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DESCRIPCIONCAUSA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Describa la causa o factores que favorecieron que se presentara el efecto indeseado reportado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DESCRIPCIONCAUSA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DESCRIPCIONCAUSA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especificación de otra herramienta de análisis utilizada (diferente a Londres, AMFE, Espina de pescado). VARCHAR(200), customización.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CUALOTRAHERRAMIENTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿Cuál? otra herramienta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CUALOTRAHERRAMIENTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CUALOTRAHERRAMIENTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Herramienta metodológica empleada para análisis del efecto indeseado: 1=Protocolo de Londres, 2=AMFE, 3=Espina de pescado, 4=N/A, 5=Otra. INT, farmacovigilancia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'HERRAMIENTAUTILIZO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿Qué herram utilizó para análisis del efecto indeseado?  1 = Protocolo de Londres  2 = AMFE  3 = Espina de pescado  4 = N/A  5 = Otra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'HERRAMIENTAUTILIZO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'HERRAMIENTAUTILIZO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: ¿se realizó análisis de causa raíz del efecto indeseado? 1=Sí, 0=No. BIT, investigación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'REALIZOTIPOANALISIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿Se realizó algún tipo de análisis del efecto indeseado?   True = SiFalse = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'REALIZOTIPOANALISIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'REALIZOTIPOANALISIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: ¿la institución mantiene programa activo de gestión de riesgos? 1=Sí en funcionamiento, 0=No. BIT, cumplimiento normativo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'INSTIPROGRAMARIESGOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿La institución tiene en funcionamiento un programa de gestión de riesgos?   True = SiFalse = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'INSTIPROGRAMARIESGOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'INSTIPROGRAMARIESGOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: ¿se envió el reactivo al importador/distribuidor para análisis/investigación? 1=Sí, 0=No. BIT, trazabilidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ENVIADOREACTIVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se ha enviado el reactivo al Importador y/o Distribuidor  True = SiFalse = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ENVIADOREACTIVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ENVIADOREACTIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que se notificó al fabricante, importador, distribuidor o comercializador sobre el evento. DATE, auditoría regulatoria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de notificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: ¿se notificó al distribuidor del reactivo? 1=Sí, 0=No. BIT, cadena de suministro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOTIFICODISTRIBUIDOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Notificó al Distribuidor  True = SiFalse = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOTIFICODISTRIBUIDOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOTIFICODISTRIBUIDOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: ¿se notificó al comercializador del reactivo? 1=Sí, 0=No. BIT, responsabilidad comercial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOTIFICOCOMERCIALIZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Notificó al Comercializador  True = SiFalse = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOTIFICOCOMERCIALIZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOTIFICOCOMERCIALIZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: ¿se notificó al fabricante del reactivo? 1=Sí, 0=No. BIT, farmacovigilancia regulatoria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOTIFICOFABRICANTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Notificó al Fabricante  True = SiFalse = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOTIFICOFABRICANTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOTIFICOFABRICANTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: ¿se notificó al importador del reactivo? 1=Sí, 0=No. BIT, responsabilidad de importación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOTIFICOIMPORTADOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Notificó al Importador  True = SiFalse = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOTIFICOIMPORTADOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOTIFICOIMPORTADOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Causa probable identificada del efecto indeseado: defecto de envase, empaque, inserto, registro sanitario o error del reactivo. VARCHAR(200), análisis.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSAPROBABLE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Causa probable del efecto indeseado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSAPROBABLE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSAPROBABLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: ¿se detectó/identificó la causa raíz que generó el efecto indeseado? 1=Sí, 0=No. BIT, resolución investigativa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DETECTOCAUSA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿Se detectó la causa que generó el efecto indeseado?  True = Si  False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DETECTOCAUSA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DETECTOCAUSA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especificación de otro desenlace o consecuencia no listada del efecto indeseado. VARCHAR(200), eventos adicionales.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CUALOTRODESENLACE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cual otro efecto indeseado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CUALOTRODESENLACE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CUALOTRODESENLACE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: desenlace ''''Otra'''' consecuencia registrada del efecto indeseado. 1=Seleccionado, 0=No. BIT, clasificación de evento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'OTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Desenlace del efecto indeseado  True = Seleccionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'OTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'OTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: desenlace ''''Diagnóstico incorrecto'''' resultante del efecto indeseado. 1=Seleccionado, 0=No. BIT, consecuencia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DIAGNOSINCORREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Desenlace del efecto indeseado  True = Seleccionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DIAGNOSINCORREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DIAGNOSINCORREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: desenlace ''''Intervención psicológica'''' requerida por efecto indeseado. 1=Seleccionado, 0=No. BIT, daño emocional/psiquiátrico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'INTERVENPSICOLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Desenlace del efecto indeseado  True = Seleccionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'INTERVENPSICOLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'INTERVENPSICOLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: desenlace ''''Intervención quirúrgica'''' necesaria por efecto indeseado. 1=Seleccionado, 0=No. BIT, procedimiento quirúrgico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'INTERVENQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Desenlace del efecto indeseado  True = Seleccionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'INTERVENQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'INTERVENQX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: desenlace ''''Intervención médica'''' requerida por efecto indeseado. 1=Seleccionado, 0=No. BIT, atención clínica adicional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'INTERVENMEDICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Desenlace del efecto indeseado  True = Seleccionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'INTERVENMEDICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'INTERVENMEDICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: desenlace ''''Transfusión de producto biológico'''' necesaria por efecto indeseado. 1=Seleccionado, 0=No. BIT, producto sanguíneo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'TRANSFUSIONBIOLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Desenlace del efecto indeseado  True = Seleccionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'TRANSFUSIONBIOLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'TRANSFUSIONBIOLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: desenlace ''''Tratamiento inapropiado'''' resultante del efecto indeseado. 1=Seleccionado, 0=No. BIT, daño iatrogénico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'TRATAMIENTOINAPROP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Desenlace del efecto indeseado  True = Seleccionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'TRATAMIENTOINAPROP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'TRATAMIENTOINAPROP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: desenlace ''''Prescripción'''' (pérdida de prescripción médica) resultante del efecto. 1=Seleccionado, 0=No. BIT, error terapéutico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PRESCRIPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Desenlace del efecto indeseado  True = Seleccionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PRESCRIPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PRESCRIPCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: desenlace ''''Hospitalización'''' requerida por efecto indeseado. 1=Seleccionado, 0=No. BIT, ingreso hospitalario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'HOSPITALIZACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Desenlace del efecto indeseado  True = Seleccionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'HOSPITALIZACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'HOSPITALIZACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: desenlace ''''Retraso'''' en diagnóstico/tratamiento por efecto indeseado. 1=Seleccionado, 0=No. BIT, impacto temporal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'RETRASO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Desenlace del efecto indeseado  True = Seleccionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'RETRASO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'RETRASO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: desenlace ''''Muerte'''' del paciente atribuible al efecto indeseado. 1=Seleccionado, 0=No. BIT, evento centinela, máxima severidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'MUERTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Desenlace del efecto indeseado  True = Seleccionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'MUERTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'MUERTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: desenlace ''''Daño corporal'''' (lesión física) resultante del efecto indeseado. 1=Seleccionado, 0=No. BIT, daño anatómico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DANOCORPORAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Desenlace del efecto indeseado  True = Seleccionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DANOCORPORAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DANOCORPORAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción detallada del evento adverso o incidente detectado con el reactivo. VARCHAR(500), narrativa clínica de farmacovigilancia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DESCRIPCIONEFECTOINDESEADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción del efecto indeseado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DESCRIPCIONEFECTOINDESEADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DESCRIPCIONEFECTOINDESEADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación del efecto: 1=Evento Adverso (daño inesperado), 2=Incidente (no causó daño). INT, tipología regulatoria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CLASIFICACIONEFECTOINDESEADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clasificación del efecto indeseado  1= Evento Adverso  2=Incidente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CLASIFICACIONEFECTOINDESEADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CLASIFICACIONEFECTOINDESEADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Problema detectado con el reactivo: 1=Envase, 2=Empaque, 3=Inserto, 4=Número de Registro Sanitario inválido, 5=Error del reactivo en prueba. INT, defectología.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PROBOEMAREACTIVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿Cuál fue el problema con el reactivo o la prueba?  1=Envase  2=Empaque  3=Inserto  4=No de Registro Sanitario  5=Errores imputables al reactivo en el desarrollo de un aprueba', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PROBOEMAREACTIVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PROBOEMAREACTIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Momento de detección del efecto indeseado: 1=Antes del uso del RDIV, 2=Durante el uso, 3=Después del uso. INT, línea temporal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DETENCCIONEFECTOINDESEADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Detección del efecto indeseado  1=Antes del uso del RDIV  2=Durante el uso del RDIV  3=Después del uso del RDIV ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DETENCCIONEFECTOINDESEADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DETENCCIONEFECTOINDESEADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que se elaboró/documentó el reporte de vigilancia del reactivo. DATE, trazabilidad documental.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHAELABORACIONREPORTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de elaboración del reporte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHAELABORACIONREPORTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHAELABORACIONREPORTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha exacta en que ocurrió el efecto indeseado/evento adverso del reactivo. DATE, evento de vigilancia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHAOCURRENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de ocurrencia del efecto indeseado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHAOCURRENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHAOCURRENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: ¿el reactivo cuenta con certificado de análisis/conformidad? 1=Sí, 0=No. BIT, aseguramiento de calidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PRODUCTOCERTIFICADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' ¿El producto cuenta con certificado de análisis?  True = So False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PRODUCTOCERTIFICADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PRODUCTOCERTIFICADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: ¿se cumplieron las condiciones de almacenamiento requeridas del reactivo? 1=Sí, 0=No. BIT, cadena de frío/custodia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CUMPLIERONCONDICIONES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿Se cumplieron con las condiciones de almacenamiento?  True = So   False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CUMPLIERONCONDICIONES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CUMPLIERONCONDICIONES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especificación de otro servicio o área de funcionamiento del reactivo (diferente a laboratorio clínico, salud pública, transfusión, banco de sangre). VARCHAR(200).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CUALOTROSERVICIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'cual otro servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CUALOTROSERVICIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CUALOTROSERVICIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Servicio o unidad funcional donde se utilizó el reactivo al momento del efecto: 1=Laboratorio clínico, 2=Laboratorio de Salud Pública, 3=Servicio Transfusional, 4=Banco de Sangre, 5=Otro. INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'SERVICIOREACTIVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Servicio o Área de funcionamiento del reactivo en el momento del efecto indeseado:   1 = Laboratorio clínico  2 = Laboratorio de Salud Pública   3 = Servicio Transfusional  4 = Banco de Sangre  5 = Otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'SERVICIOREACTIVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'SERVICIOREACTIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre comercial, razón social o entidad del importador y/o distribuidor del reactivo. VARCHAR(200), responsable de suministro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOMBRERAZONSOCIALIMPORTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre o razón social del Importador y/o Distribuidor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOMBRERAZONSOCIALIMPORTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOMBRERAZONSOCIALIMPORTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Temperatura de almacenamiento requerida para el reactivo en grados Celsius (°C). INT, especificación de cadena de frío.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'TEMPERATURA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Temperatura almacenamiento requerida (°C)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'TEMPERATURA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'TEMPERATURA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: ¿el reactivo requiere cadena de frío para conservación? 1=Sí, 0=No. BIT, logística especializada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'REQUIEREFRIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Requiere cadena de frio  True = Si   false = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'REQUIEREFRIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'REQUIEREFRIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Origen del reactivo: 1=Nacional (fabricado en país), 2=Importado (extranjero). INT, clasificación de procedencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PROCEDENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Procedencia:  1 = Nacional  2 = Importado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PROCEDENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PROCEDENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de expiración o vencimiento del lote del reactivo. DATE, control de vigencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHAVENCIMIENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de vencimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHAVENCIMIENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHAVENCIMIENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de lote/partida/batch del reactivo afectado. VARCHAR(50), trazabilidad de producto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'LOTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Lote', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'LOTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'LOTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de Registro Sanitario (INVIMA u ente regulador) del reactivo. VARCHAR(200), cumplimiento normativo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'REGISTROSANITARIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Registro Sanitario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'REGISTROSANITARIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'REGISTROSANITARIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre comercial del reactivo/producto afectado por el evento adverso. VARCHAR(200), identificación de producto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOMBRECOMERCIAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre comercial del reactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOMBRECOMERCIAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOMBRECOMERCIAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que se generó/presentó el reporte de vigilancia ante la autoridad. DATE, auditoría regulatoria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHAREPORTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del reporte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHAREPORTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHAREPORTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Correo electrónico de contacto institucional (de la institución reportante). VARCHAR(100), PII de institución.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CORREO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Correo electrónico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CORREO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CORREO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Teléfono de contacto de la institución reportante. VARCHAR(20), datos de localizacion institucional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'TELEFONO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Teléfono', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'TELEFONO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'TELEFONO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección postal/física de la institución reportante del evento. VARCHAR(200), ubicación de origen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DIRECCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dirección', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DIRECCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DIRECCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ubicación geográfica/departamental de la institución (FK a INUBICACI). CHAR(20), referencia territorial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'UBICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ubicación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'UBICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'UBICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel de complejidad de la institución: 1=Nivel 1, 2=Nivel 2, 3=Nivel 3, 4=N/A. INT, clasificación de institución de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NIVELCOMPLEJIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nivel de complejidad de la institución  1=1  2=2  3=3  4=n/a', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NIVELCOMPLEJIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NIVELCOMPLEJIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Naturaleza jurídica de la institución reportante: 1=Pública, 2=Privada, 3=Mixta. INT, tipo de prestador.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NATURALEZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Naturaleza de la institución reportante  1= Publica  2=Privada  3= Mixta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NATURALEZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NATURALEZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de Identificación Tributaria (NIT) de la institución reportante. VARCHAR(15), identificación legal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'NIT de la Institucion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo/razón social de la institución de salud que reporta el evento. VARCHAR(200), centro de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOMBREINSTITUCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la institucion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOMBREINSTITUCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOMBREINSTITUCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de referencia al reporte maestro en tabla CALREPORTE (FK). INT, relación de auditoría.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'IDCALREPORTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del reporte que guarda igual que id del tabla CALREPORTE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'IDCALREPORTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'IDCALREPORTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementado de cada registro de vigilancia de reactivo en la tabla. INT IDENTITY(1,1), clave primaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reportes de vigilancia de reactivos de diagnóstico in vitro (reactivos de laboratorio). Registra los eventos o efectos indeseados ocurridos con reactivos, incluyendo datos del producto, la institución, el incidente, sus consecuencias clínicas, las causas probables y las acciones de mejora tomadas, para cumplimiento de la vigilancia sanitaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA';
