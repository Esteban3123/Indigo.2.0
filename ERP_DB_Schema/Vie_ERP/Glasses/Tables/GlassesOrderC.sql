CREATE TABLE [Glasses].[GlassesOrderC] (
    [Id]                   INT           IDENTITY (1, 1) NOT NULL,
    [IDHCHISPACA]          INT           NOT NULL,
    [NUMEFOLIO]            CHAR (10)     NOT NULL,
    [IPCODPACI]            VARCHAR (25)  NOT NULL,
    [NUMINGRES]            CHAR (10)     NOT NULL,
    [CODCENATE]            CHAR (10)     NOT NULL,
    [UFUCODIGO]            CHAR (10)     NOT NULL,
    [CODPROSAL]            CHAR (20)     NOT NULL,
    [CODPRODUC]            CHAR (20)     NOT NULL,
    [CODDIAGNO]            CHAR (4)      NOT NULL,
    [Status]               INT           NOT NULL,
    [OrderDate]            DATETIME      NOT NULL,
    [WayOfUse]             INT           NULL,
    [PupillaryDistance]    VARCHAR (100) NULL,
    [Filters]              VARCHAR (100) NULL,
    [Color]                VARCHAR (100) NULL,
    [Photosensitive]       BIT           NOT NULL,
    [Antiglare]            BIT           NOT NULL,
    [HighIndex]            BIT           NOT NULL,
    [PeriodDuration]       VARCHAR (100) NULL,
    [RelevantClinicalData] VARCHAR (200) NULL,
    [Quantity]             INT           NOT NULL,
    [Extramural]           BIT           NOT NULL,
    [Material]             VARCHAR (100) NULL,
    [GlassesType]          INT           NULL,
    [PrintType]            INT           NULL,
    CONSTRAINT [PK_GlassesOrderC] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ADCENATEN_NCODCENATE] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_ADINGRESO_NUMINGRES] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCHISPACA_ID] FOREIGN KEY ([IDHCHISPACA]) REFERENCES [dbo].[HCHISPACA] ([ID]),
    CONSTRAINT [FK_IHLISTPRO_CODPRODUC] FOREIGN KEY ([CODPRODUC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC]),
    CONSTRAINT [FK_INDIAGNOS_CODDIAGNO] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_INPACIENT_IPCODPACI] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_INPROFSAL_CODPROSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_INUNIFUNC_UFUCODIGO] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [Glasses].[GlassesOrderC] NOCHECK CONSTRAINT [FK_ADCENATEN_NCODCENATE];


GO
ALTER TABLE [Glasses].[GlassesOrderC] NOCHECK CONSTRAINT [FK_ADINGRESO_NUMINGRES];


GO
ALTER TABLE [Glasses].[GlassesOrderC] NOCHECK CONSTRAINT [FK_HCHISPACA_ID];


GO
ALTER TABLE [Glasses].[GlassesOrderC] NOCHECK CONSTRAINT [FK_IHLISTPRO_CODPRODUC];


GO
ALTER TABLE [Glasses].[GlassesOrderC] NOCHECK CONSTRAINT [FK_INDIAGNOS_CODDIAGNO];


GO
ALTER TABLE [Glasses].[GlassesOrderC] NOCHECK CONSTRAINT [FK_INPACIENT_IPCODPACI];


GO
ALTER TABLE [Glasses].[GlassesOrderC] NOCHECK CONSTRAINT [FK_INPROFSAL_CODPROSAL];


GO
ALTER TABLE [Glasses].[GlassesOrderC] NOCHECK CONSTRAINT [FK_INUNIFUNC_UFUCODIGO];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de impresión:   0. No envio nada el medico (valor defecto)  1. Refracción objetiva con ciclopejia,   2. Refracción objetiva sin ciclopejia,   3. Refracción subjetiva con ciclopejia,   4. Refracción subjetiva sin ciclopejia', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'PrintType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de lentes: 1. Monofocal, 2. Bifocal, 3. Progresivo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'GlassesType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo Material del formulario', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'Material';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Corresponde a manejo extramural', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'Extramural';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Datos clínicos relevantes', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'RelevantClinicalData';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Periodo de duración', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'PeriodDuration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Alto índice', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'HighIndex';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Antirreflejo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'Antiglare';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fotosensible', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'Photosensitive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Color', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'Color';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Filtros', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'Filters';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Distancia pupilar', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'PupillaryDistance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Forma de uso: Lista desplegable de selección única con las siguientes opciones:     1 - Permanente    2 - Ocupacional   3 - Ocasional ', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'WayOfUse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del registro de la orden medica', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'OrderDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del registro  1 - Solicitado  ', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del diagnostico con el que se justifico la orden medica.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del producto', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Profesional de la salud de la orden medica', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad Funcional donde se hizo la orden', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Centro atencion donde se hizo la orden medica', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ingreso del paciente', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id paciente', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relacion con el tablero de folios HCHISPACA', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderC', @level2type = N'COLUMN', @level2name = N'IDHCHISPACA';

