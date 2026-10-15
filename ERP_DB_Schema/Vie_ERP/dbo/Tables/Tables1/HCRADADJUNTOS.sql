CREATE TABLE [dbo].[HCRADADJUNTOS] (
    [ID]           INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCRADORDEN] INT           NOT NULL,
    [NOMADJUNTO]   VARCHAR (100) NOT NULL,
    [CODCENATE]    CHAR (10)     NOT NULL,
    [FECREGISTRO]  DATETIME      NOT NULL,
    [NUMINGRES]    CHAR (10)     NOT NULL,
    [EXTEARCHI]    CHAR (5)      NOT NULL,
    [CODUSUARI]    CHAR (20)     NOT NULL,
    [NOMANTARC]    VARCHAR (MAX) NOT NULL,
    [TIPO]         TINYINT       NOT NULL,
    CONSTRAINT [PK_HCRADADJUNTOS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCRADADJUNTOS_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCRADADJUNTOS_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCRADADJUNTOS_HCRADORDEN] FOREIGN KEY ([IDHCRADORDEN]) REFERENCES [dbo].[HCRADORDEN] ([ID])
);


GO
ALTER TABLE [dbo].[HCRADADJUNTOS] NOCHECK CONSTRAINT [FK_HCRADADJUNTOS_ADCENATEN];


GO
ALTER TABLE [dbo].[HCRADADJUNTOS] NOCHECK CONSTRAINT [FK_HCRADADJUNTOS_ADINGRESO];


GO
ALTER TABLE [dbo].[HCRADADJUNTOS] NOCHECK CONSTRAINT [FK_HCRADADJUNTOS_HCRADORDEN];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1  - Adjunto de radioterapia  2  - Adjunto de Braquiterapia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADADJUNTOS', @level2type = N'COLUMN', @level2name = N'TIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el nombre anterior del archivo ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADADJUNTOS', @level2type = N'COLUMN', @level2name = N'NOMANTARC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADADJUNTOS', @level2type = N'COLUMN', @level2name = N'CODUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Extensión del Archivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADADJUNTOS', @level2type = N'COLUMN', @level2name = N'EXTEARCHI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADADJUNTOS', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADADJUNTOS', @level2type = N'COLUMN', @level2name = N'FECREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Centro Atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADADJUNTOS', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del Adjunto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADADJUNTOS', @level2type = N'COLUMN', @level2name = N'NOMADJUNTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la orden de radioterapia o Braquiterapia (HCRADORDEN)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADADJUNTOS', @level2type = N'COLUMN', @level2name = N'IDHCRADORDEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADADJUNTOS', @level2type = N'COLUMN', @level2name = N'ID';

