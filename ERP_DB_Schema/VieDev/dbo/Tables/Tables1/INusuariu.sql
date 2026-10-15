CREATE TABLE [dbo].[INusuariu] (
    [codusuari] CHAR (3)  NOT NULL,
    [nomusuari] CHAR (40) NOT NULL,
    [passusuar] CHAR (21) NOT NULL,
    [igrunivel] CHAR (3)  NOT NULL,
    [codusudgh] CHAR (3)  NOT NULL,
    [usuactivo] INT       NOT NULL,
    CONSTRAINT [PK_INusuariu__codusuari] PRIMARY KEY CLUSTERED ([codusuari] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario Activo 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INusuariu', @level2type = N'COLUMN', @level2name = N'usuactivo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario en DGH', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INusuariu', @level2type = N'COLUMN', @level2name = N'codusudgh';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el grupo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INusuariu', @level2type = N'COLUMN', @level2name = N'igrunivel';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave del usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INusuariu', @level2type = N'COLUMN', @level2name = N'passusuar';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INusuariu', @level2type = N'COLUMN', @level2name = N'nomusuari';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INusuariu', @level2type = N'COLUMN', @level2name = N'codusuari';

