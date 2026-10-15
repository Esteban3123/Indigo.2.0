CREATE TABLE [dbo].[HCPARALERNOT] (
    [CODCONSEC]           TINYINT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCENATE]           CHAR (10)   NOT NULL,
    [TIPSECALER]          CHAR (1)    NOT NULL,
    [ENVMAILUS]           BIT         NOT NULL,
    [ENVSMSUSU]           BIT         NOT NULL,
    [NOMCOMUSU]           CHAR (100)  NOT NULL,
    [NOMCORSMS]           NCHAR (10)  NOT NULL,
    [NUMTELMOV]           CHAR (10)   NOT NULL,
    [CORELEUSU]           NCHAR (50)  NOT NULL,
    [MENADICOR]           NCHAR (250) NOT NULL,
    [USUARIOCREACION]     CHAR (20)   NULL,
    [FECHACREACION]       DATETIME    NULL,
    [USUARIOMODIFICACION] CHAR (20)   NULL,
    [FECHAMODIFICACION]   DATETIME    NULL,
    CONSTRAINT [PK_HCPARALERNOT_1] PRIMARY KEY CLUSTERED ([CODCONSEC] ASC),
    CONSTRAINT [FK_HCPARALERNOT_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCPARALERNOT_SEGusuaru_1] FOREIGN KEY ([USUARIOCREACION]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI]),
    CONSTRAINT [FK_HCPARALERNOT_SEGusuaru_2] FOREIGN KEY ([USUARIOMODIFICACION]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI])
);


GO
ALTER TABLE [dbo].[HCPARALERNOT] NOCHECK CONSTRAINT [FK_HCPARALERNOT_ADCENATEN];


GO
ALTER TABLE [dbo].[HCPARALERNOT] NOCHECK CONSTRAINT [FK_HCPARALERNOT_SEGusuaru_1];


GO
ALTER TABLE [dbo].[HCPARALERNOT] NOCHECK CONSTRAINT [FK_HCPARALERNOT_SEGusuaru_2];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALERNOT', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALERNOT', @level2type = N'COLUMN', @level2name = N'USUARIOMODIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Creación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALERNOT', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALERNOT', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mensaje Adicional para el envio de Correo Electronico en Cada Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALERNOT', @level2type = N'COLUMN', @level2name = N'MENADICOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Correo Electronico Usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALERNOT', @level2type = N'COLUMN', @level2name = N'CORELEUSU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero Telefonico Movil para el envio de SMS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALERNOT', @level2type = N'COLUMN', @level2name = N'NUMTELMOV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre Corto para el Envio de SMS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALERNOT', @level2type = N'COLUMN', @level2name = N'NOMCORSMS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre Completo del Usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALERNOT', @level2type = N'COLUMN', @level2name = N'NOMCOMUSU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si se realiza el envio de SMS como Medio de Comunicacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALERNOT', @level2type = N'COLUMN', @level2name = N'ENVSMSUSU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si se realiza el envio de Correos Electronicos como Medio de Comunicacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALERNOT', @level2type = N'COLUMN', @level2name = N'ENVMAILUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo Seccion de Alertas  1: Laboratorio  2: Imagenologia  3: Interconsultas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALERNOT', @level2type = N'COLUMN', @level2name = N'TIPSECALER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALERNOT', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALERNOT', @level2type = N'COLUMN', @level2name = N'CODCONSEC';

