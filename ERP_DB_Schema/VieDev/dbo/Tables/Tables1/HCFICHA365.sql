CREATE TABLE [dbo].[HCFICHA365] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [GRUPOSUSTANCIAS]     INT           NULL,
    [CODIPRODUCTO]        VARCHAR (50)  NULL,
    [TIPOEXPO]            INT           NULL,
    [LUGARINTOX]          INT           NULL,
    [FECHAEXPO]           DATE          NULL,
    [VIAEXPO]             INT           NULL,
    [ESCOLARIDAD]         INT           NULL,
    [AFILIADOARL]         BIT           NULL,
    [CODIGOARL]           VARCHAR (50)  NULL,
    [ESTADOCIVIL]         INT           NULL,
    [CASOBROTE]           BIT           NULL,
    [NUMCASOS]            VARCHAR (50)  NULL,
    [FECHAINVEST]         DATE          NULL,
    [SITUAALERTA]         BIT           NULL,
    [MUESTRASTOXICO]      BIT           NULL,
    [SANGRETOTAL]         BIT           NULL,
    [ORINA]               BIT           NULL,
    [TEJIDO]              BIT           NULL,
    [SUERO]               BIT           NULL,
    [AGUA]                BIT           NULL,
    [CABELLO]             BIT           NULL,
    [EMPAQUE]             BIT           NULL,
    [OTROS]               BIT           NULL,
    [UNAS]                BIT           NULL,
    [NOMBREPRUEBA]        VARCHAR (50)  NULL,
    [VALORRESUL]          VARCHAR (50)  NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA365] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA365_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA365_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA365] NOCHECK CONSTRAINT [CK_HCFICHA365_JSON];


GO
ALTER TABLE [dbo].[HCFICHA365] NOCHECK CONSTRAINT [FK_HCFICHA365_HCFICHANOTIFICACION];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nuevas columnas en formato JSON     Desde versión "V01_2020-03-06" -->  - HORAEXPOSICION : datetime     ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA365', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el  Valor resultado / unidades', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA365', @level2type = N'COLUMN', @level2name = N'VALORRESUL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el Nombre de la prueba toxicológica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA365', @level2type = N'COLUMN', @level2name = N'NOMBREPRUEBA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si el tipo de muestra solicitada es UNAS    true = si     false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA365', @level2type = N'COLUMN', @level2name = N'UNAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si el tipo de muestra solicitada  es Otros    true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA365', @level2type = N'COLUMN', @level2name = N'OTROS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si el tipo de muestra solicitada es Empaque    true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA365', @level2type = N'COLUMN', @level2name = N'EMPAQUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si el tipo de muestra solicitada es Cabello   true = si      false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA365', @level2type = N'COLUMN', @level2name = N'CABELLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si el tipo de muestra solicitada es Agua   true = si     false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA365', @level2type = N'COLUMN', @level2name = N'AGUA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si el tipo de muestra solicitada es Suero   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA365', @level2type = N'COLUMN', @level2name = N'SUERO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si el tipo de muestra solicitada es Tejido true = si     false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA365', @level2type = N'COLUMN', @level2name = N'TEJIDO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si el tipo de muestra solicitada  es orina   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA365', @level2type = N'COLUMN', @level2name = N'ORINA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el  Tipo de muestras solicitadas es sangre total    true = Si     false = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA365', @level2type = N'COLUMN', @level2name = N'SANGRETOTAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda Si se tomaron muestras de toxicología el lugar de intoxicacion 1 = Si  0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA365', @level2type = N'COLUMN', @level2name = N'MUESTRASTOXICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la Situación de alerta  1 = Si  0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA365', @level2type = N'COLUMN', @level2name = N'SITUAALERTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la Fecha de investigación epidemiológica brote ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA365', @level2type = N'COLUMN', @level2name = N'FECHAINVEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el  Número de casos en este brote', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA365', @level2type = N'COLUMN', @level2name = N'NUMCASOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda ¿El caso hace parte de un brote?   1= Si  0= No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA365', @level2type = N'COLUMN', @level2name = N'CASOBROTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el estado civil 1 = soltero  2 = casado 3= union libre 4 = viudo 5 = divorciado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA365', @level2type = N'COLUMN', @level2name = N'ESTADOCIVIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda Código y nombre de la A.R.L', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA365', @level2type = N'COLUMN', @level2name = N'CODIGOARL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si esta afliado A.R.L  1= SI   0 = NO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA365', @level2type = N'COLUMN', @level2name = N'AFILIADOARL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la escolaridad 1 = preescolar  2= basica primaria  3= basica secundaria 4 =  Media académica o clásica 5 = media tecnica  6= normalista 7 = Técnica profesional 8= tecnologia  9 = profesional          10 =Especialización  11 = maestria 12 = doctorado  13 = ninguno 14 = sin informacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA365', @level2type = N'COLUMN', @level2name = N'ESCOLARIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Via de Exposición:   1 - Respiratoria   2 - Oral   3 - Dérmica / Mucosa   4 - Ocular   5 - Desconocida   6 - Parenteral (intramuscular, intravenosa, subcutánea, intraperitoneal)   7 - Transplacentaria   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA365', @level2type = N'COLUMN', @level2name = N'VIAEXPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la Fecha de exposición', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA365', @level2type = N'COLUMN', @level2name = N'FECHAEXPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el lugar de intoxicacion  1 = Hogar  2 = Establecimiento educativo  3 = Establecimiento militar    4 = Establecimiento comercial 5 = Establecimiento penitenciario  6= Lugar de trabajo 7 = Vía pública/parque 8 = Bares/Tabernas/Discotecas ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA365', @level2type = N'COLUMN', @level2name = N'LUGARINTOX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el tipo exposicion 1 = Ocupacional  2 = Accidental  3 = Suicidio consumado  4 = Posible acto Homicidio   5 = posible acto delictivo 6 = desconocida 7  Intensional psicoactiva  adiccion   8 = Automedicacion  autoprescripcion ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA365', @level2type = N'COLUMN', @level2name = N'TIPOEXPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda Código y nombre del producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA365', @level2type = N'COLUMN', @level2name = N'CODIPRODUCTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda Grupo de sustancias  1 = medicamentos 2 = plaguicidads 3 = matanol 4 =  metales  5= solventes  6 = otras sustancias quimicas 7 = gases  8 = sustancias psicoactivas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA365', @level2type = N'COLUMN', @level2name = N'GRUPOSUSTANCIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guardad el codigo del diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA365', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la identidad de la fIcha', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA365', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  el cosecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA365', @level2type = N'COLUMN', @level2name = N'ID';

