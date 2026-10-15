CREATE TABLE [dbo].[ADMONGO] (
    [ID]                INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [COLECCIONMONGO]    VARCHAR (100)  NOT NULL,
    [NOMBRE]            VARCHAR (100)  NOT NULL,
    [MESANO]            VARCHAR (50)   NOT NULL,
    [ESTADO]            BIT            NOT NULL,
    [USUARIO]           CHAR (20)      NOT NULL,
    [FECHACREACION]     DATETIME       NOT NULL,
    [USUARIOMOD]        CHAR (20)      NULL,
    [FECHAMODIFICACION] DATETIME       NULL,
    [OTRASCOLUMNAS]     VARCHAR (1000) NULL,
    CONSTRAINT [PK_ADMONGO] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado   1 = Activo   0 =  Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADMONGO', @level2type = N'COLUMN', @level2name = N'ESTADO';

