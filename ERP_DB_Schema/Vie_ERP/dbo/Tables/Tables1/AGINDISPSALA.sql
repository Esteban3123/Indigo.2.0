CREATE TABLE [dbo].[AGINDISPSALA] (
    [ID]           INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDSALA]       INT       NOT NULL,
    [IDMOTIINDIS]  INT       NOT NULL,
    [FECININDISPO] DATETIME  NOT NULL,
    [FECFINDISPO]  DATETIME  NOT NULL,
    [FECHREGIS]    DATETIME  NOT NULL,
    [CODUSUARI]    CHAR (20) NOT NULL,
    CONSTRAINT [PK_AGINDISPSALA_1] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_AGINDISPSALA_AGENSALAC] FOREIGN KEY ([IDSALA]) REFERENCES [dbo].[AGENSALAC] ([CODCONCEC]),
    CONSTRAINT [FK_AGINDISPSALA_AGMOTINDPS] FOREIGN KEY ([IDMOTIINDIS]) REFERENCES [dbo].[AGMOTINDPS] ([ID])
);


GO
ALTER TABLE [dbo].[AGINDISPSALA] NOCHECK CONSTRAINT [FK_AGINDISPSALA_AGENSALAC];


GO
ALTER TABLE [dbo].[AGINDISPSALA] NOCHECK CONSTRAINT [FK_AGINDISPSALA_AGMOTINDPS];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Usuario de Creacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGINDISPSALA', @level2type = N'COLUMN', @level2name = N'CODUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creacion de registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGINDISPSALA', @level2type = N'COLUMN', @level2name = N'FECHREGIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Final de Indisponibilidad de sala', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGINDISPSALA', @level2type = N'COLUMN', @level2name = N'FECFINDISPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Inicial de Indisponibilidad de sala', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGINDISPSALA', @level2type = N'COLUMN', @level2name = N'FECININDISPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID relacion con tabla de Motivos de Indisponibilidad de salas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGINDISPSALA', @level2type = N'COLUMN', @level2name = N'IDMOTIINDIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID relacion con salas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGINDISPSALA', @level2type = N'COLUMN', @level2name = N'IDSALA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGINDISPSALA', @level2type = N'COLUMN', @level2name = N'ID';

