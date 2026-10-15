CREATE TABLE [dbo].[SEGusuaru] (
    [CODUSUARI]  CHAR (20)     NOT NULL,
    [NOMUSUARI]  CHAR (60)     NOT NULL,
    [PASSUSUAR]  CHAR (50)     NOT NULL,
    [CODUSUDGH]  CHAR (50)     NULL,
    [USUACTIVO]  BIT           NOT NULL,
    [CODIGOROL]  CHAR (4)      NOT NULL,
    [CODGRUPOU]  CHAR (3)      NOT NULL,
    [USUEMAILC]  CHAR (60)     NULL,
    [USUEMAILE]  CHAR (60)     NULL,
    [DESCARUSU]  VARCHAR (150) NULL,
    [TIPPERUSU]  CHAR (1)      NOT NULL,
    [PERASISTE]  CHAR (1)      NULL,
    [CODCENATE]  CHAR (10)     NULL,
    [UFUCODIGO]  CHAR (10)     NULL,
    [SOLCAMCON]  BIT           NOT NULL,
    [DIACAMCON]  INT           NOT NULL,
    [FECULTCAM]  DATETIME      NOT NULL,
    [FECADCUE]   DATETIME      NULL,
    [USUADMINI]  BIT           NULL,
    [TOKEN]      CHAR (100)    NULL,
    [EXPIRATION] DATETIME      NULL,
    CONSTRAINT [PK_SEGusuaru] PRIMARY KEY CLUSTERED ([CODUSUARI] ASC),
    CONSTRAINT [FK_SEGusuaru_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_SEGusuaru_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_SEGusuaru_SEGgruusu] FOREIGN KEY ([CODGRUPOU]) REFERENCES [dbo].[SEGgruusu] ([codgrupou]),
    CONSTRAINT [FK_SEGusuaru_SEGrolesu] FOREIGN KEY ([CODIGOROL]) REFERENCES [dbo].[SEGrolesu] ([codigorol])
);


GO
ALTER TABLE [dbo].[SEGusuaru] NOCHECK CONSTRAINT [FK_SEGusuaru_ADCENATEN];


GO
ALTER TABLE [dbo].[SEGusuaru] NOCHECK CONSTRAINT [FK_SEGusuaru_INUNIFUNC];


GO
ALTER TABLE [dbo].[SEGusuaru] NOCHECK CONSTRAINT [FK_SEGusuaru_SEGgruusu];


GO
ALTER TABLE [dbo].[SEGusuaru] NOCHECK CONSTRAINT [FK_SEGusuaru_SEGrolesu];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la fecha de expiración', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'EXPIRATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el token', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'TOKEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario administrador', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'USUADMINI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Caducidad de la Cuenta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'FECADCUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del Ultimo Cambio de Contraseña', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'FECULTCAM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ciclo en dias para el cambio de contraseña', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'DIACAMCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Solicitar cambio de contraseña en el proximo inicio de sesion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'SOLCAMCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Perfil Asistencial:  1= DashBoard Medico  2= DashBoard Enfermeria  3= DashBoard Interconsultas  4= DashBoard Terapias  5= DashBoard Laboratorio  6= DashBoard Imagenologia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'PERASISTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Define el Tipo de Perfil:  1= Administrativo  2= Asistencial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'TIPPERUSU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cargo del usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'DESCARUSU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Correo Electronico del Usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'USUEMAILE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'SIP Usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'USUEMAILC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Grupo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'CODGRUPOU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Rol', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'CODIGOROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Activo 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'USUACTIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del usuario en DGH', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'CODUSUDGH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contraseña del usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'PASSUSUAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del Usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'NOMUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGusuaru', @level2type = N'COLUMN', @level2name = N'CODUSUARI';

