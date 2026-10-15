CREATE TABLE [dbo].[AGMOTINDPS] (
    [ID]        INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODIMOT]   VARCHAR (3)   NOT NULL,
    [NOMMOT]    VARCHAR (50)  NOT NULL,
    [OBSERMOT]  VARCHAR (500) NULL,
    [ESTADO]    BIT           NOT NULL,
    [FECHREGIS] DATETIME      NOT NULL,
    [CODUSUARI] CHAR (20)     NOT NULL,
    CONSTRAINT [PK_AGINDISPSALA] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Usuario de creacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGMOTINDPS', @level2type = N'COLUMN', @level2name = N'CODUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creacion del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGMOTINDPS', @level2type = N'COLUMN', @level2name = N'FECHREGIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado 1 - Activo     2 - Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGMOTINDPS', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observacion o comenntario del Motivo de Indisponibilidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGMOTINDPS', @level2type = N'COLUMN', @level2name = N'OBSERMOT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del Motivo de Indisponibilidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGMOTINDPS', @level2type = N'COLUMN', @level2name = N'NOMMOT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Motivo de Indisponibilidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGMOTINDPS', @level2type = N'COLUMN', @level2name = N'CODIMOT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico del  Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGMOTINDPS', @level2type = N'COLUMN', @level2name = N'ID';

