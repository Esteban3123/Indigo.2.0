CREATE TABLE [dbo].[HCRADIMGBRAQUI] (
    [ID]           INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCRADORDEN] INT          NOT NULL,
    [IDHCIMARADIO] INT          NOT NULL,
    [CODCENATE]    CHAR (10)    NOT NULL,
    [FECREGISTRO]  DATETIME     NOT NULL,
    [EXTARCHIVO]   VARCHAR (10) NOT NULL,
    CONSTRAINT [PK_HCRADIMGBRAQUI] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCRADIMGBRAQUI_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCRADIMGBRAQUI_HCIMARADIO] FOREIGN KEY ([IDHCIMARADIO]) REFERENCES [dbo].[HCIMARADIO] ([ID]),
    CONSTRAINT [FK_HCRADIMGBRAQUI_HCRADORDEN] FOREIGN KEY ([IDHCRADORDEN]) REFERENCES [dbo].[HCRADORDEN] ([ID])
);


GO
ALTER TABLE [dbo].[HCRADIMGBRAQUI] NOCHECK CONSTRAINT [FK_HCRADIMGBRAQUI_ADCENATEN];


GO
ALTER TABLE [dbo].[HCRADIMGBRAQUI] NOCHECK CONSTRAINT [FK_HCRADIMGBRAQUI_HCIMARADIO];


GO
ALTER TABLE [dbo].[HCRADIMGBRAQUI] NOCHECK CONSTRAINT [FK_HCRADIMGBRAQUI_HCRADORDEN];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Extension del archivo (JPG,BIT,PNG)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADIMGBRAQUI', @level2type = N'COLUMN', @level2name = N'EXTARCHIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADIMGBRAQUI', @level2type = N'COLUMN', @level2name = N'FECREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'codigo centro atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADIMGBRAQUI', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relacion con la tabla de imagenes (HCIMARADIO)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADIMGBRAQUI', @level2type = N'COLUMN', @level2name = N'IDHCIMARADIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de relacion con la orden de braquiterapia (HCRADORDEN)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADIMGBRAQUI', @level2type = N'COLUMN', @level2name = N'IDHCRADORDEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADIMGBRAQUI', @level2type = N'COLUMN', @level2name = N'ID';

