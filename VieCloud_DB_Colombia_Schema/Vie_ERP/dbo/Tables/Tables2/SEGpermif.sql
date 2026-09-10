CREATE TABLE [dbo].[SEGpermif] (
    [indidmenu]     VARCHAR (5) NOT NULL,
    [indmendes]     CHAR (80)   NOT NULL,
    [ioptguard]     BIT         NOT NULL,
    [ioptconsu]     BIT         NOT NULL,
    [ioptdisen]     BIT         NOT NULL,
    [ioptactua]     BIT         NOT NULL,
    [ioptelimi]     BIT         NOT NULL,
    [ioptnaveg]     BIT         NOT NULL,
    [ioptconfi]     BIT         NOT NULL,
    [ioptanula]     BIT         NOT NULL,
    [ioptimpri]     BIT         NOT NULL,
    [igricrear]     BIT         NOT NULL,
    [igrimodif]     BIT         NOT NULL,
    [igrielimi]     BIT         NOT NULL,
    [integracion]   TINYINT     CONSTRAINT [DF_SEGpermif_integracion] DEFAULT ((1)) NOT NULL,
    [esfundacional] BIT         NULL,
    CONSTRAINT [PK_SEGpermif] PRIMARY KEY CLUSTERED ([indidmenu] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT, NULL) que indica si el formulario es fundacional (1=replica en BD fundacional) o transaccional (0=solo BD transaccional). Determina estrategia de persistencia y sincronización de datos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'esfundacional';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si el formulario es fundacional o no.  0 - No fundacional (guarda en la BD transaccional)  1 - Fundacional (guarda en una BD fundacional y replica)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'esfundacional';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'esfundacional';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de integración del menú/formulario: 1=DGH+VIE, 2=solo VIE, 3=solo DGH. Define sistemas externos conectados (TINYINT, default 1).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'integracion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Integracion con DGH y con VIE   -  2 - Integracion solo con VIE    -  3 - Integracion solo con DGH   -', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'integracion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'integracion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Permiso de grilla: eliminar registro. BIT 1=Habilitado, 0=Deshabilitado. Controla si el usuario puede borrar filas en vista tabular.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'igrielimi';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Grilla Eliminar Registro  1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'igrielimi';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'igrielimi';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Permiso de grilla: modificar/editar registro. BIT 1=Habilitado, 0=Deshabilitado. Controla edición en línea de filas en vista tabular.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'igrimodif';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Grilla Modificar Registro 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'igrimodif';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'igrimodif';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Permiso de grilla: crear nuevo registro. BIT 1=Habilitado, 0=Deshabilitado. Controla inserción de filas en vista tabular.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'igricrear';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Grilla Crear Registro 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'igricrear';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'igricrear';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Permiso de formulario: imprimir. BIT 1=Habilitado, 0=Deshabilitado. Autoriza generar salida impresa/reporte del formulario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'ioptimpri';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite Imprimir 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'ioptimpri';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'ioptimpri';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Permiso de formulario: anular registro. BIT 1=Habilitado, 0=Deshabilitado. Autoriza marcar como inválido/cancelado sin eliminar.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'ioptanula';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite Anular 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'ioptanula';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'ioptanula';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Permiso de formulario: confirmar. BIT 1=Habilitado, 0=Deshabilitado. Autoriza sellar/validar datos del formulario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'ioptconfi';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite Confirmar 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'ioptconfi';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'ioptconfi';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Permiso de formulario: navegar. BIT 1=Habilitado, 0=Deshabilitado. Autoriza movimiento entre registros y secciones del formulario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'ioptnaveg';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Formulario Navegar  1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'ioptnaveg';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'ioptnaveg';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Permiso de formulario: eliminar. BIT 1=Habilitado, 0=Deshabilitado. Autoriza borrar registro completo desde formulario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'ioptelimi';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Formulario eliminar  1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'ioptelimi';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'ioptelimi';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Permiso de formulario: actualizar/editar. BIT 1=Habilitado, 0=Deshabilitado. Autoriza modificar datos de un registro existente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'ioptactua';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Formulario Actualizar 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'ioptactua';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'ioptactua';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Permiso de formulario: guardar como diseño. BIT 1=Habilitado, 0=Deshabilitado. Autoriza guardar plantilla/configuración personalizada del formulario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'ioptdisen';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Formulario Guardar Como Diseñar (Guardar Como) 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'ioptdisen';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'ioptdisen';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Permiso de formulario: consultar/leer. BIT 1=Habilitado, 0=Deshabilitado. Autoriza ver datos del formulario sin modificar.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'ioptconsu';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Formulario Consultar 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'ioptconsu';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'ioptconsu';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Permiso de formulario: guardar. BIT 1=Habilitado, 0=Deshabilitado. Autoriza registrar/persistir datos nuevos o cambios en formulario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'ioptguard';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Formulario Guardar 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'ioptguard';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'ioptguard';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción legible del menú o formulario (CHAR 80). Etiqueta que visualiza el usuario final en interfaz.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'indmendes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del Menu', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'indmendes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'indmendes';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador único del menú/formulario en INDIGO (VARCHAR 5, PK). Clave primaria para referenciar permisos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'indidmenu';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Menu en INDIGO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'indidmenu';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif', @level2type = N'COLUMN', @level2name = N'indidmenu';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Permisos y opciones habilitadas por menú o funcionalidad del sistema de seguridad. Define qué acciones (guardar, consultar, actualizar, eliminar, imprimir, etc.) están permitidas para cada opción de menú, controlando el acceso y las capacidades de los usuarios en cada módulo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermif';
