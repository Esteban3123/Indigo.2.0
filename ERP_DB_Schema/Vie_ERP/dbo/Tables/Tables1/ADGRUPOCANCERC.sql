CREATE TABLE [dbo].[ADGRUPOCANCERC] (
    [ID]                INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODIGOGRUPO]       INT       NOT NULL,
    [FECHACREACION]     DATETIME  NOT NULL,
    [USUARIOCREACION]   CHAR (20) NOT NULL,
    [FECHAMODIFICACION] DATETIME  NULL,
    [USUARIOMODIFICA]   CHAR (20) NULL,
    CONSTRAINT [PK_ADGRUPOCANCERC] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'usuario que modifica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUPOCANCERC', @level2type = N'COLUMN', @level2name = N'USUARIOMODIFICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fecha modificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUPOCANCERC', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'usuario de creacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUPOCANCERC', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUPOCANCERC', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del grupo, este codigo esta quemado en codigo  1 - Mama  2 - Pulmon  3 - Gástrico  4 - Colorrectal  5 - Cáncer anal  6 - Próstata  7 - Cérvix  8 - Melanoma  9 - Leucemias  10 - Linfomas (Hodgkin - No Hodgkin)  11 - Otros', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUPOCANCERC', @level2type = N'COLUMN', @level2name = N'CODIGOGRUPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUPOCANCERC', @level2type = N'COLUMN', @level2name = N'ID';

