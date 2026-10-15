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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificación del reportante  Nombre del Reportante ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOMBREREPORTANTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '¿Inició acciones de mejoramiento (preventivas/correctivas)?  True = SiFalse = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'INICIOACCIONESMEJORA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '¿Qué herram utilizó para análisis del efecto indeseado?  1 = Protocolo de Londres  2 = AMFE  3 = Espina de pescado  4 = N/A  5 = Otra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'HERRAMIENTAUTILIZO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '¿Se realizó algún tipo de análisis del efecto indeseado?   True = SiFalse = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'REALIZOTIPOANALISIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '¿La institución tiene en funcionamiento un programa de gestión de riesgos?   True = SiFalse = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'INSTIPROGRAMARIESGOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Se ha enviado el reactivo al Importador y/o Distribuidor  True = SiFalse = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ENVIADOREACTIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Notificó al Distribuidor  True = SiFalse = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOTIFICODISTRIBUIDOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Notificó al Comercializador  True = SiFalse = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOTIFICOCOMERCIALIZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Notificó al Fabricante  True = SiFalse = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOTIFICOFABRICANTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Notificó al Importador  True = SiFalse = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOTIFICOIMPORTADOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '¿Se detectó la causa que generó el efecto indeseado?  True = Si  False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DETECTOCAUSA';


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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clasificación del efecto indeseado  1= Evento Adverso  2=Incidente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CLASIFICACIONEFECTOINDESEADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '¿Cuál fue el problema con el reactivo o la prueba?  1=Envase  2=Empaque  3=Inserto  4=No de Registro Sanitario  5=Errores imputables al reactivo en el desarrollo de un aprueba', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PROBOEMAREACTIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Detección del efecto indeseado  1=Antes del uso del RDIV  2=Durante el uso del RDIV  3=Después del uso del RDIV ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DETENCCIONEFECTOINDESEADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' ¿El producto cuenta con certificado de análisis?  True = So False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PRODUCTOCERTIFICADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '¿Se cumplieron con las condiciones de almacenamiento?  True = So   False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CUMPLIERONCONDICIONES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Servicio o Área de funcionamiento del reactivo en el momento del efecto indeseado:   1 = Laboratorio clínico  2 = Laboratorio de Salud Pública   3 = Servicio Transfusional  4 = Banco de Sangre  5 = Otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'SERVICIOREACTIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Requiere cadena de frio  True = Si   false = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'REQUIEREFRIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Procedencia:  1 = Nacional  2 = Importado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PROCEDENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nivel de complejidad de la institución  1=1  2=2  3=3  4=n/a', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NIVELCOMPLEJIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Naturaleza de la institución reportante  1= Publica  2=Privada  3= Mixta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREACTIVOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NATURALEZA';

