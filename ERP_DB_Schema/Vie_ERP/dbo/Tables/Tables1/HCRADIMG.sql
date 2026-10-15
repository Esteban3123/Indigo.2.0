CREATE TABLE [dbo].[HCRADIMG] (
    [ID]           INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCRADORDEN] INT       NOT NULL,
    [IDHCIMARADIO] INT       NOT NULL,
    [CODCENATE]    CHAR (10) NOT NULL,
    [FECREGISTRO]  DATETIME  NOT NULL,
    [EXTARCHIVO]   CHAR (10) NOT NULL,
    [TIPO]         TINYINT   NOT NULL,
    CONSTRAINT [PK_HCRADIMG] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCRADIMG_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCRADIMG_HCIMARADIO] FOREIGN KEY ([IDHCIMARADIO]) REFERENCES [dbo].[HCIMARADIO] ([ID]),
    CONSTRAINT [FK_HCRADIMG_HCRADORDEN] FOREIGN KEY ([IDHCRADORDEN]) REFERENCES [dbo].[HCRADORDEN] ([ID])
);


GO
ALTER TABLE [dbo].[HCRADIMG] NOCHECK CONSTRAINT [FK_HCRADIMG_ADCENATEN];


GO
ALTER TABLE [dbo].[HCRADIMG] NOCHECK CONSTRAINT [FK_HCRADIMG_HCIMARADIO];


GO
ALTER TABLE [dbo].[HCRADIMG] NOCHECK CONSTRAINT [FK_HCRADIMG_HCRADORDEN];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1  - Imagen de radioterapia  2  - Imagen de Braquiterapia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADIMG', @level2type = N'COLUMN', @level2name = N'TIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Extensión del archivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADIMG', @level2type = N'COLUMN', @level2name = N'EXTARCHIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADIMG', @level2type = N'COLUMN', @level2name = N'FECREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código centro atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADIMG', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la Imagen, relación con la Tabla   HCIMARADIO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADIMG', @level2type = N'COLUMN', @level2name = N'IDHCIMARADIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la Información de la radioterapia relación con la tabla HCRADINFOC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADIMG', @level2type = N'COLUMN', @level2name = N'IDHCRADORDEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADIMG', @level2type = N'COLUMN', @level2name = N'ID';

