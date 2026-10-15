CREATE TABLE [dbo].[HCFICHA300] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [TIPOAGRE]            INT           NULL,
    [AREAMORD]            INT           NULL,
    [AGREPROV]            BIT           NULL,
    [TIPOLESI]            BIT           NULL,
    [PROFUNDI]            BIT           NULL,
    [CABEZA]              BIT           NULL,
    [MANOS]               BIT           NULL,
    [TRONCO]              BIT           NULL,
    [MIEMSUP]             BIT           NULL,
    [MIEMINF]             BIT           NULL,
    [PIES]                BIT           NULL,
    [GENITA]              BIT           NULL,
    [FECHAAGRE]           DATE          NULL,
    [ESPAGRE]             INT           NULL,
    [ANIVAC]              INT           NULL,
    [PRESECARNE]          BIT           NULL,
    [FECHAVACU]           DATE          NULL,
    [NOMPROP]             VARCHAR (50)  NULL,
    [DIREPROP]            VARCHAR (50)  NULL,
    [ESTAANAGRE]          INT           NULL,
    [ESTAANCON]           INT           NULL,
    [UBIANIM]             INT           NULL,
    [TIPOEXPO]            INT           NULL,
    [SUEROANTI]           INT           NULL,
    [FECHAAPLI]           DATE          NULL,
    [VACUANTI]            INT           NULL,
    [NUMDOSIS]            VARCHAR (50)  NULL,
    [FECHAULT]            DATE          NULL,
    [LAVHERI]             BIT           NULL,
    [SUTUHER]             BIT           NULL,
    [ORDENSUE]            BIT           NULL,
    [ORDENAPLI]           BIT           NULL,
    [TELEFONO]            VARCHAR (50)  NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA300] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA300_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA300_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA300] NOCHECK CONSTRAINT [CK_HCFICHA300_JSON];


GO
ALTER TABLE [dbo].[HCFICHA300] NOCHECK CONSTRAINT [FK_HCFICHA300_HCFICHANOTIFICACION];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ordenó Aplicación Vacuna  True = si false = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'ORDENAPLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ordenó Suero Antirrábico  True = si false = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'ORDENSUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sutura de la Herida  True = si false = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'SUTUHER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Lavado de Herida con Agua y Jabón  True = si   false = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'LAVHERI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Vacuna Antirrábica  1=si   2=no  3=no sabe', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'VACUANTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Suero Antirrábico  1=si  2=no  3=No sabe', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'SUEROANTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Exposición  1= No exposicion  2=Exposicion leve  3=Exposicion Grave', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'TIPOEXPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ubicación del Animal Agreso  1=Observable  2=Perdido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'UBIANIM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Animal al Momento de la Consulta  1=vivo  2=Muerto  3=Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'ESTAANCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Animal al Momento de la Agresión o Contacto  1=Con Signos de Rabia  2=Sin Signos de Rabia  3=Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'ESTAANAGRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Presentó Carné de Vacunación Antirrábica  True = si   False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'PRESECARNE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Animal Vacunado  1= si   2= no  3=Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'ANIVAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especie Agresora  1=perro  2=gato  3=bovino -bufalino  4=equidos  5=porcino(Cerdo)  6=murcielago  7=zorro  8=mico  9=humano  10=otros silvestres  11=ovino - caprino  12= grandes roedores', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'ESPAGRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Localización Anatómica de la Lesión  7=Genitales Externos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'GENITA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Localización Anatómica de la Lesión  6=Pies, Dedos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'PIES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Localización Anatómica de la Lesión  5=Miembros Inferiores', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'MIEMINF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Localización Anatómica de la Lesión  4=Miembros Superiores', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'MIEMSUP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Localización Anatómica de la Lesión  3=Tronco', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'TRONCO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Localización Anatómica de la Lesión  2=Manos, Dedos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'MANOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Localización Anatómica de la Lesión  1=Cabeza, Cara, Cuello', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'CABEZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Profundidad  True = SuperficialFalse = Profunda', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'PROFUNDI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Lesión  True = Unica  False = Multiple', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'TIPOLESI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Agresión Provocada  True = si   false = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'AGREPROV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Si marcó mordedura, Seleccione Área  1=En Área Cubierta del Cuerpo  2=En Área Descubierta del Cuerpo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'AREAMORD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Agresión o Contacto  1=Mordedura  2=Arañazo o Rasguño  3=Contacto de Mucosa o Piel Lesionada con Saliva o Baba Infectada con Virus Rábico  4=Contacto de Mucosa o Piel Lesionada con Tejido Nervioso, Material Biológico o Secreciones Infectadas con Virus Rábico  5=Inhalación en Ambientes Cargados o Virus Rábico (Aerosoles)  6=Transplante de Órganos o Tejidos Infectados con Virus Rábico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA300', @level2type = N'COLUMN', @level2name = N'TIPOAGRE';

