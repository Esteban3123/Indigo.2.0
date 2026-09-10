CREATE TABLE [dbo].[SEGpermiu] (
    [codusuari] CHAR (20)   NOT NULL,
    [indidmenu] VARCHAR (5) NOT NULL,
    [ioptguard] BIT         NOT NULL,
    [ioptconsu] BIT         NOT NULL,
    [ioptdisen] BIT         NOT NULL,
    [ioptactua] BIT         NOT NULL,
    [ioptelimi] BIT         NOT NULL,
    [ioptnaveg] BIT         NOT NULL,
    [ioptconfi] BIT         NOT NULL,
    [ioptanula] BIT         NOT NULL,
    [ioptimpri] BIT         NOT NULL,
    [igricrear] BIT         NOT NULL,
    [igrimodif] BIT         NOT NULL,
    [igrielimi] BIT         NOT NULL,
    [ioptvisib] BIT         NOT NULL,
    CONSTRAINT [PK_SEGpermiu] PRIMARY KEY CLUSTERED ([codusuari] ASC, [indidmenu] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Permiso para ver/visualizar formulario (BIT: 1=Sí, 0=No). Controla visibilidad de pantalla en aplicación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermiu', @level2type = N'COLUMN', @level2name = N'ioptvisib';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ver Formulario 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermiu', @level2type = N'COLUMN', @level2name = N'ioptvisib';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermiu', @level2type = N'COLUMN', @level2name = N'ioptvisib';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Permiso para eliminar registro en grilla/tabla (BIT: 1=Sí, 0=No). Permite borrar filas de datos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermiu', @level2type = N'COLUMN', @level2name = N'igrielimi';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Grilla Eliminar Registro  1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermiu', @level2type = N'COLUMN', @level2name = N'igrielimi';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermiu', @level2type = N'COLUMN', @level2name = N'igrielimi';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Permiso para modificar/editar registro en grilla/tabla (BIT: 1=Sí, 0=No). Permite cambiar datos en filas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermiu', @level2type = N'COLUMN', @level2name = N'igrimodif';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Grilla Modificar Registro 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermiu', @level2type = N'COLUMN', @level2name = N'igrimodif';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermiu', @level2type = N'COLUMN', @level2name = N'igrimodif';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Permiso para crear nuevo registro en grilla/tabla (BIT: 1=Sí, 0=No). Permite insertar filas de datos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermiu', @level2type = N'COLUMN', @level2name = N'igricrear';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Grilla Crear Registro 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermiu', @level2type = N'COLUMN', @level2name = N'igricrear';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermiu', @level2type = N'COLUMN', @level2name = N'igricrear';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Permiso para imprimir formulario/reporte (BIT: 1=Sí, 0=No). Exporta o genera documentos imprimibles.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermiu', @level2type = N'COLUMN', @level2name = N'ioptimpri';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite Imprimir 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermiu', @level2type = N'COLUMN', @level2name = N'ioptimpri';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermiu', @level2type = N'COLUMN', @level2name = N'ioptimpri';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Permiso para anular/deshabilitar registro (BIT: 1=Sí, 0=No). Marca como inactivo sin eliminar.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermiu', @level2type = N'COLUMN', @level2name = N'ioptanula';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite Anular 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermiu', @level2type = N'COLUMN', @level2name = N'ioptanula';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermiu', @level2type = N'COLUMN', @level2name = N'ioptanula';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Permiso para confirmar/validar operación (BIT: 1=Sí, 0=No). Autoriza cierre o aprobación de transacción.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermiu', @level2type = N'COLUMN', @level2name = N'ioptconfi';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite Confirmar 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermiu', @level2type = N'COLUMN', @level2name = N'ioptconfi';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermiu', @level2type = N'COLUMN', @level2name = N'ioptconfi';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Permiso para navegar en formulario (BIT: 1=Sí, 0=No). Permite moverse entre campos y secciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermiu', @level2type = N'COLUMN', @level2name = N'ioptnaveg';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Formulario Navegar  1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermiu', @level2type = N'COLUMN', @level2name = N'ioptnaveg';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermiu', @level2type = N'COLUMN', @level2name = N'ioptnaveg';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Permiso para eliminar en formulario (BIT: 1=Sí, 0=No). Borra registro completo desde pantalla.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermiu', @level2type = N'COLUMN', @level2name = N'ioptelimi';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Formulario eliminar  1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermiu', @level2type = N'COLUMN', @level2name = N'ioptelimi';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermiu', @level2type = N'COLUMN', @level2name = N'ioptelimi';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Permiso para actualizar/guardar cambios en formulario (BIT: 1=Sí, 0=No). Modifica datos existentes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermiu', @level2type = N'COLUMN', @level2name = N'ioptactua';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Formulario Actualizar 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermiu', @level2type = N'COLUMN', @level2name = N'ioptactua';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermiu', @level2type = N'COLUMN', @level2name = N'ioptactua';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Permiso para guardar como diseño/plantilla (BIT: 1=Sí, 0=No). Crea variante o copia de formulario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermiu', @level2type = N'COLUMN', @level2name = N'ioptdisen';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Formulario Guardar Como Diseñar (Guardar Como) 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermiu', @level2type = N'COLUMN', @level2name = N'ioptdisen';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermiu', @level2type = N'COLUMN', @level2name = N'ioptdisen';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Permiso para consultar/ver datos en formulario (BIT: 1=Sí, 0=No). Acceso de lectura a información.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermiu', @level2type = N'COLUMN', @level2name = N'ioptconsu';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Formulario Consultar 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermiu', @level2type = N'COLUMN', @level2name = N'ioptconsu';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermiu', @level2type = N'COLUMN', @level2name = N'ioptconsu';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Permiso para guardar/crear nuevo registro en formulario (BIT: 1=Sí, 0=No). Inserta datos nuevos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermiu', @level2type = N'COLUMN', @level2name = N'ioptguard';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Formulario Guardar 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermiu', @level2type = N'COLUMN', @level2name = N'ioptguard';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermiu', @level2type = N'COLUMN', @level2name = N'ioptguard';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de menú/módulo en INDIGO (VARCHAR 5). Referencia a opción de interfaz, sinonimia: código de pantalla.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermiu', @level2type = N'COLUMN', @level2name = N'indidmenu';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Menu en INDIGO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermiu', @level2type = N'COLUMN', @level2name = N'indidmenu';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermiu', @level2type = N'COLUMN', @level2name = N'indidmenu';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del usuario/operador (CHAR 20). Cédula, documento o login del usuario en sistema. Identificación PII.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermiu', @level2type = N'COLUMN', @level2name = N'codusuari';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermiu', @level2type = N'COLUMN', @level2name = N'codusuari';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermiu', @level2type = N'COLUMN', @level2name = N'codusuari';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Permisos de acceso por usuario a cada opción de menú del sistema. Define qué acciones puede realizar cada usuario en cada pantalla o módulo: guardar, consultar, actualizar, eliminar, imprimir, anular, navegar, configurar y visibilidad, así como permisos de grilla (crear, modificar, eliminar filas).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermiu';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermiu';
