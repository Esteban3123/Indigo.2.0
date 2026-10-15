CREATE TABLE [dbo].[PYPPARAHIST] (
    [ID]          INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODIGO]      VARCHAR (4)   NOT NULL,
    [NOMBRE]      VARCHAR (100) NOT NULL,
    [TIPUNIDAD]   INT           NOT NULL,
    [SEXO]        INT           NOT NULL,
    [EDADMINIMA]  INT           NOT NULL,
    [EDADMAXIMA]  INT           NOT NULL,
    [ESTADO]      BIT           NOT NULL,
    [FECHACREA]   DATETIME      NOT NULL,
    [USUARIOCREA] CHAR (20)     NOT NULL,
    [FECHAMOD]    DATETIME      NULL,
    [USUARIOMOD]  CHAR (20)     NULL,
    CONSTRAINT [PK_PYPPARAM] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el usuario quien modifico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPPARAHIST', @level2type = N'COLUMN', @level2name = N'USUARIOMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la fecha de modificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPPARAHIST', @level2type = N'COLUMN', @level2name = N'FECHAMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el usuario de creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPPARAHIST', @level2type = N'COLUMN', @level2name = N'USUARIOCREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la fecha de creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPPARAHIST', @level2type = N'COLUMN', @level2name = N'FECHACREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el estado   1 = Activo  0 = Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPPARAHIST', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la edad maxima', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPPARAHIST', @level2type = N'COLUMN', @level2name = N'EDADMAXIMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la edad minima', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPPARAHIST', @level2type = N'COLUMN', @level2name = N'EDADMINIMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Sexo  1 =  masculino  2 = femenino   3 = HC aplica para los dos Sexo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPPARAHIST', @level2type = N'COLUMN', @level2name = N'SEXO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Tipo Unidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPPARAHIST', @level2type = N'COLUMN', @level2name = N'TIPUNIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Nombre de la HC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPPARAHIST', @level2type = N'COLUMN', @level2name = N'NOMBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Codigo de la HC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPPARAHIST', @level2type = N'COLUMN', @level2name = N'CODIGO';

