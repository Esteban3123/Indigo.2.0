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
ALTER TABLE [dbo].[CALREACTIVOVIGILANCIA] NOCHECK CONSTRAINT [FK_CALREACTIVOVIGILANCIA_CALREPORTE];


GO
ALTER TABLE [dbo].[CALREACTIVOVIGILANCIA] NOCHECK CONSTRAINT [FK_CALREACTIVOVIGILANCIA_INUBICACI];


GO
ALTER TABLE [dbo].[CALREACTIVOVIGILANCIA] NOCHECK CONSTRAINT [FK_CALREACTIVOVIGILANCIA_INUBICACI1];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '¿Autoriza la divulgación de la información y origen del reporte al fabricante y/o importador?  True = SiFalse = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'AUTORIZADIVULGACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Correo electrónico personal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CORREOREPORTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de notificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHANOTIREPORTAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Celular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CELULARREPORTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Teléfono de domicilio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'TELEFONOREPORTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ubicación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'UBICACIONREPORTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dirección de correspondencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DIRECCIONREPORTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Área de la organización a la que pertenece', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'AREAREPORTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cargo del reportante ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CARGOREPORTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Profesion del reportante ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PROFESIONREPORTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de identificacion del reportante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'IDENTIREPORTANTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificación del reportante  Nombre del Reportante ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOMBREREPORTANTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'cuales Acciones mejoramiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ACCIONESMEJORAM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '¿Inició acciones de mejoramiento (preventivas/correctivas)?  True = SiFalse = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'INICIOACCIONESMEJORA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Describa la causa o factores que favorecieron que se presentara el efecto indeseado reportado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DESCRIPCIONCAUSA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '¿Cuál? otra herramienta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CUALOTRAHERRAMIENTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '¿Qué herram utilizó para análisis del efecto indeseado?  1 = Protocolo de Londres  2 = AMFE  3 = Espina de pescado  4 = N/A  5 = Otra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'HERRAMIENTAUTILIZO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '¿Se realizó algún tipo de análisis del efecto indeseado?   True = SiFalse = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'REALIZOTIPOANALISIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '¿La institución tiene en funcionamiento un programa de gestión de riesgos?   True = SiFalse = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'INSTIPROGRAMARIESGOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Se ha enviado el reactivo al Importador y/o Distribuidor  True = SiFalse = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ENVIADOREACTIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de notificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Notificó al Distribuidor  True = SiFalse = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOTIFICODISTRIBUIDOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Notificó al Comercializador  True = SiFalse = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOTIFICOCOMERCIALIZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Notificó al Fabricante  True = SiFalse = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOTIFICOFABRICANTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Notificó al Importador  True = SiFalse = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOTIFICOIMPORTADOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Causa probable del efecto indeseado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSAPROBABLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '¿Se detectó la causa que generó el efecto indeseado?  True = Si  False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DETECTOCAUSA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cual otro efecto indeseado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CUALOTRODESENLACE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Desenlace del efecto indeseado  True = Seleccionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'OTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Desenlace del efecto indeseado  True = Seleccionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DIAGNOSINCORREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Desenlace del efecto indeseado  True = Seleccionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'INTERVENPSICOLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Desenlace del efecto indeseado  True = Seleccionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'INTERVENQX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Desenlace del efecto indeseado  True = Seleccionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'INTERVENMEDICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Desenlace del efecto indeseado  True = Seleccionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'TRANSFUSIONBIOLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Desenlace del efecto indeseado  True = Seleccionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'TRATAMIENTOINAPROP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Desenlace del efecto indeseado  True = Seleccionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PRESCRIPCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Desenlace del efecto indeseado  True = Seleccionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'HOSPITALIZACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Desenlace del efecto indeseado  True = Seleccionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'RETRASO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Desenlace del efecto indeseado  True = Seleccionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'MUERTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Desenlace del efecto indeseado  True = Seleccionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DANOCORPORAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripción del efecto indeseado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DESCRIPCIONEFECTOINDESEADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clasificación del efecto indeseado  1= Evento Adverso  2=Incidente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CLASIFICACIONEFECTOINDESEADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '¿Cuál fue el problema con el reactivo o la prueba?  1=Envase  2=Empaque  3=Inserto  4=No de Registro Sanitario  5=Errores imputables al reactivo en el desarrollo de un aprueba', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PROBOEMAREACTIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Detección del efecto indeseado  1=Antes del uso del RDIV  2=Durante el uso del RDIV  3=Después del uso del RDIV ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DETENCCIONEFECTOINDESEADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de elaboración del reporte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHAELABORACIONREPORTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de ocurrencia del efecto indeseado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHAOCURRENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' ¿El producto cuenta con certificado de análisis?  True = So False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PRODUCTOCERTIFICADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '¿Se cumplieron con las condiciones de almacenamiento?  True = So   False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CUMPLIERONCONDICIONES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'cual otro servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CUALOTROSERVICIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Servicio o Área de funcionamiento del reactivo en el momento del efecto indeseado:   1 = Laboratorio clínico  2 = Laboratorio de Salud Pública   3 = Servicio Transfusional  4 = Banco de Sangre  5 = Otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'SERVICIOREACTIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre o razón social del Importador y/o Distribuidor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOMBRERAZONSOCIALIMPORTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Temperatura almacenamiento requerida (°C)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'TEMPERATURA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Requiere cadena de frio  True = Si   false = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'REQUIEREFRIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Procedencia:  1 = Nacional  2 = Importado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PROCEDENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de vencimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHAVENCIMIENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Lote', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'LOTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Registro Sanitario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'REGISTROSANITARIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre comercial del reactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOMBRECOMERCIAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del reporte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHAREPORTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Correo electrónico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CORREO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Teléfono', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'TELEFONO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dirección', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DIRECCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ubicación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'UBICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nivel de complejidad de la institución  1=1  2=2  3=3  4=n/a', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NIVELCOMPLEJIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Naturaleza de la institución reportante  1= Publica  2=Privada  3= Mixta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NATURALEZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'NIT de la Institucion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la institucion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOMBREINSTITUCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del reporte que guarda igual que id del tabla CALREPORTE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'IDCALREPORTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ID';

