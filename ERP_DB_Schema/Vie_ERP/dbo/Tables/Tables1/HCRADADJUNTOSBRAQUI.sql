CREATE TABLE [dbo].[HCRADADJUNTOSBRAQUI] (
    [ID]           INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCRADORDEN] INT           NOT NULL,
    [NOMADJUNTO]   VARCHAR (100) NOT NULL,
    [CODCENATE]    CHAR (10)     NOT NULL,
    [FECREGISTRO]  DATETIME      NOT NULL,
    [NUMINGRES]    CHAR (10)     NOT NULL,
    [EXTEARCHI]    VARCHAR (5)   NOT NULL,
    [CODUSUARI]    CHAR (20)     NOT NULL,
    [NOMANTARC]    VARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_HCRADADJUNTOSBRAQUI] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCRADADJUNTOSBRAQUI_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCRADADJUNTOSBRAQUI_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCRADADJUNTOSBRAQUI_HCRADORDEN] FOREIGN KEY ([IDHCRADORDEN]) REFERENCES [dbo].[HCRADORDEN] ([ID])
);


GO
ALTER TABLE [dbo].[HCRADADJUNTOSBRAQUI] NOCHECK CONSTRAINT [FK_HCRADADJUNTOSBRAQUI_ADCENATEN];


GO
ALTER TABLE [dbo].[HCRADADJUNTOSBRAQUI] NOCHECK CONSTRAINT [FK_HCRADADJUNTOSBRAQUI_ADINGRESO];


GO
ALTER TABLE [dbo].[HCRADADJUNTOSBRAQUI] NOCHECK CONSTRAINT [FK_HCRADADJUNTOSBRAQUI_HCRADORDEN];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del archivo abjunto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADADJUNTOSBRAQUI', @level2type = N'COLUMN', @level2name = N'NOMANTARC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADADJUNTOSBRAQUI', @level2type = N'COLUMN', @level2name = N'CODUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Extension del archivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADADJUNTOSBRAQUI', @level2type = N'COLUMN', @level2name = N'EXTEARCHI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADADJUNTOSBRAQUI', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADADJUNTOSBRAQUI', @level2type = N'COLUMN', @level2name = N'FECREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo centro de atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADADJUNTOSBRAQUI', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre documento abjunto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADADJUNTOSBRAQUI', @level2type = N'COLUMN', @level2name = N'NOMADJUNTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID relacion de la orden de braquiterapia (HCRADORDEN)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADADJUNTOSBRAQUI', @level2type = N'COLUMN', @level2name = N'IDHCRADORDEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADADJUNTOSBRAQUI', @level2type = N'COLUMN', @level2name = N'ID';

