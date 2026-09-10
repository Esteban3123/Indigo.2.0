CREATE TABLE [dbo].[SEGmenusu] (
    [indidmenu] VARCHAR (5) NOT NULL,
    [indmodulo] CHAR (2)    NOT NULL,
    [indopcion] INT         NOT NULL,
    CONSTRAINT [PK_SEGmenusu] PRIMARY KEY CLUSTERED ([indidmenu] ASC, [indmodulo] ASC),
    CONSTRAINT [FK_SEGmenusu_SEGmodulu] FOREIGN KEY ([indmodulo]) REFERENCES [dbo].[SEGmodulu] ([indmodulo])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de opción de menú (INT): 0=Generales, 1=Archivo, 2=Procesos, 3=Utilidades, 4=Reportes. Categoriza la funcionalidad del elemento de menú en el sistema Indigo Vie Cloud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGmenusu', @level2type = N'COLUMN', @level2name = N'indopcion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Opcion 0->Generales  1-> Archivo 2->Procesos 3-> Utilidades 4->Reportes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGmenusu', @level2type = N'COLUMN', @level2name = N'indopcion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGmenusu', @level2type = N'COLUMN', @level2name = N'indopcion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del módulo en Indigo Vie Cloud (CHAR 2). Identificador del componente funcional (ej: facturación, historia clínica, laboratorio). FK → SEGmodulu.indmodulo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGmenusu', @level2type = N'COLUMN', @level2name = N'indmodulo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del modulo en INDIGO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGmenusu', @level2type = N'COLUMN', @level2name = N'indmodulo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGmenusu', @level2type = N'COLUMN', @level2name = N'indmodulo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del menú en Indigo Vie Cloud (VARCHAR 5). Identificador del elemento de navegación de usuario en la interfaz del ERP/EHR.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGmenusu', @level2type = N'COLUMN', @level2name = N'indidmenu';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Menu en INDIGO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGmenusu', @level2type = N'COLUMN', @level2name = N'indidmenu';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGmenusu', @level2type = N'COLUMN', @level2name = N'indidmenu';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Permisos y asignación de opciones de menú por usuario o rol en el sistema de seguridad. Controla a qué módulos y opciones del sistema tiene acceso cada usuario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGmenusu';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGmenusu';
