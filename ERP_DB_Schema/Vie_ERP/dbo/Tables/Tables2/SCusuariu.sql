CREATE TABLE [dbo].[SCusuariu] (
    [codusuari] CHAR (3)  NOT NULL,
    [nomusuari] CHAR (40) NOT NULL,
    [perseguim] CHAR (1)  NOT NULL,
    [percuenta] CHAR (1)  NOT NULL,
    [perarmado] CHAR (1)  NOT NULL,
    [peraudito] CHAR (1)  NOT NULL,
    [perradica] CHAR (1)  NOT NULL,
    [perjefefa] CHAR (1)  NOT NULL,
    [peradmini] CHAR (1)  NOT NULL,
    CONSTRAINT [PK_Seusuarios] PRIMARY KEY CLUSTERED ([codusuari] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Perfil Administrador del sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SCusuariu', @level2type = N'COLUMN', @level2name = N'peradmini';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Perfil Jefe de facturacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SCusuariu', @level2type = N'COLUMN', @level2name = N'perjefefa';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Perfil Radicacion de Cuentas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SCusuariu', @level2type = N'COLUMN', @level2name = N'perradica';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Perfil Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SCusuariu', @level2type = N'COLUMN', @level2name = N'peraudito';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Perfil Armado de Cuentas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SCusuariu', @level2type = N'COLUMN', @level2name = N'perarmado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Perfil Analista de Cuentas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SCusuariu', @level2type = N'COLUMN', @level2name = N'percuenta';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Perfil Salida y Seguimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SCusuariu', @level2type = N'COLUMN', @level2name = N'perseguim';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del Usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SCusuariu', @level2type = N'COLUMN', @level2name = N'nomusuari';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SCusuariu', @level2type = N'COLUMN', @level2name = N'codusuari';

