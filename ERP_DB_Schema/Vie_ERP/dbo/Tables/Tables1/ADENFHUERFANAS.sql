CREATE TABLE [dbo].[ADENFHUERFANAS] (
    [ID]              INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODIGO]          VARCHAR (6)   NOT NULL,
    [NOMBRE]          VARCHAR (150) NOT NULL,
    [ESTADO]          BIT           NOT NULL,
    [FECHACREACION]   DATETIME      NOT NULL,
    [USUARIOCREACION] CHAR (20)     NOT NULL,
    [FECHAMODIFICA]   DATETIME      NULL,
    [USUARIOMODIFICA] CHAR (20)     NULL,
    CONSTRAINT [PK_ADENFHUERFANAS] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de modificación  de las enfermedades huérfanas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADENFHUERFANAS', @level2type = N'COLUMN', @level2name = N'USUARIOMODIFICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de modificación  de las enfermedades huérfanas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADENFHUERFANAS', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de creación  de las enfermedades huérfanas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADENFHUERFANAS', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación  de las enfermedades huérfanas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADENFHUERFANAS', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado (True or false)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADENFHUERFANAS', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre  de las enfermedades huérfanas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADENFHUERFANAS', @level2type = N'COLUMN', @level2name = N'NOMBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el código de las enfermedades huérfanas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADENFHUERFANAS', @level2type = N'COLUMN', @level2name = N'CODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADENFHUERFANAS', @level2type = N'COLUMN', @level2name = N'ID';

