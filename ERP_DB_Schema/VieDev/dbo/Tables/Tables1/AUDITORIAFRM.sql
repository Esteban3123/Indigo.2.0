CREATE TABLE [dbo].[AUDITORIAFRM] (
    [ID]                  INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NOMBRETABLA]         VARCHAR (30) NOT NULL,
    [IDREGISTRO]          VARCHAR (30) NOT NULL,
    [USUARIOCREACION]     CHAR (20)    NOT NULL,
    [FECHACREACION]       DATETIME     NOT NULL,
    [USUARIOMODIFICACION] CHAR (20)    NULL,
    [FECHAMODIFICACION]   DATETIME     NULL,
    CONSTRAINT [PK_AUDITORIAFRM] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_AUDITORIAFRM_SEGusuaru_1] FOREIGN KEY ([USUARIOCREACION]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI]),
    CONSTRAINT [FK_AUDITORIAFRM_SEGusuaru_2] FOREIGN KEY ([USUARIOMODIFICACION]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI])
);


GO
ALTER TABLE [dbo].[AUDITORIAFRM] NOCHECK CONSTRAINT [FK_AUDITORIAFRM_SEGusuaru_1];


GO
ALTER TABLE [dbo].[AUDITORIAFRM] NOCHECK CONSTRAINT [FK_AUDITORIAFRM_SEGusuaru_2];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de modificación del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AUDITORIAFRM', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de modificación del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AUDITORIAFRM', @level2type = N'COLUMN', @level2name = N'USUARIOMODIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AUDITORIAFRM', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de creación del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AUDITORIAFRM', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID registro de la tabla que relaciona', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AUDITORIAFRM', @level2type = N'COLUMN', @level2name = N'IDREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la Tabla que relaciona', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AUDITORIAFRM', @level2type = N'COLUMN', @level2name = N'NOMBRETABLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumérico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AUDITORIAFRM', @level2type = N'COLUMN', @level2name = N'ID';

