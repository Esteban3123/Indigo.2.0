CREATE TABLE [dbo].[SEGpermir] (
    [codigorol] CHAR (4)    NOT NULL,
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
    CONSTRAINT [PK_SEGpermir] PRIMARY KEY CLUSTERED ([codigorol] ASC, [indidmenu] ASC),
    CONSTRAINT [FK_SEGpermir_SEGrolesu] FOREIGN KEY ([codigorol]) REFERENCES [dbo].[SEGrolesu] ([codigorol])
);


GO
ALTER TABLE [dbo].[SEGpermir] NOCHECK CONSTRAINT [FK_SEGpermir_SEGrolesu];


GO


CREATE TRIGGER trg_BlockOperations
ON SEGpermir 
INSTEAD OF INSERT, UPDATE
AS
BEGIN
    RAISERROR('Operación no permitida, contacta al equipo desarollo EHR Cra 5 A No. 22 - 31, Neiva, Huila (Segundo piso)', 16, 1);
    ROLLBACK TRANSACTION;
END;
GO
DISABLE TRIGGER [dbo].[trg_BlockOperations]
    ON [dbo].[SEGpermir];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Permiso para ver/visualizar formulario (BIT: 1=Sí, 0=No). Controla visibilidad de pantalla en menú.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir', @level2type = N'COLUMN', @level2name = N'ioptvisib';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ver Formulario 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir', @level2type = N'COLUMN', @level2name = N'ioptvisib';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir', @level2type = N'COLUMN', @level2name = N'ioptvisib';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Permiso para eliminar registros en grilla/tabla (BIT: 1=Sí, 0=No). Operación de borrado en vista de datos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir', @level2type = N'COLUMN', @level2name = N'igrielimi';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Grilla Eliminar Registro  1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir', @level2type = N'COLUMN', @level2name = N'igrielimi';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir', @level2type = N'COLUMN', @level2name = N'igrielimi';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Permiso para modificar/editar registros en grilla (BIT: 1=Sí, 0=No). Actualización directa en tabla de datos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir', @level2type = N'COLUMN', @level2name = N'igrimodif';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Grilla Modificar Registro 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir', @level2type = N'COLUMN', @level2name = N'igrimodif';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir', @level2type = N'COLUMN', @level2name = N'igrimodif';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Permiso para crear nuevos registros en grilla (BIT: 1=Sí, 0=No). Inserción de filas en vista de datos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir', @level2type = N'COLUMN', @level2name = N'igricrear';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Grilla Crear Registro 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir', @level2type = N'COLUMN', @level2name = N'igricrear';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir', @level2type = N'COLUMN', @level2name = N'igricrear';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Permiso para imprimir/exportar formulario (BIT: 1=Sí, 0=No). Genera reporte o documento imprimible.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir', @level2type = N'COLUMN', @level2name = N'ioptimpri';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite Imprimir 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir', @level2type = N'COLUMN', @level2name = N'ioptimpri';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir', @level2type = N'COLUMN', @level2name = N'ioptimpri';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Permiso para anular/invalidar registro (BIT: 1=Sí, 0=No). Cancela transacción sin borrar dato.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir', @level2type = N'COLUMN', @level2name = N'ioptanula';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite Anular 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir', @level2type = N'COLUMN', @level2name = N'ioptanula';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir', @level2type = N'COLUMN', @level2name = N'ioptanula';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Permiso para confirmar/validar operación (BIT: 1=Sí, 0=No). Autoriza cambio en estado de registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir', @level2type = N'COLUMN', @level2name = N'ioptconfi';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite Confirmar 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir', @level2type = N'COLUMN', @level2name = N'ioptconfi';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir', @level2type = N'COLUMN', @level2name = N'ioptconfi';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Permiso para navegar entre registros en formulario (BIT: 1=Sí, 0=No). Movimiento entre filas/siguiente-anterior.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir', @level2type = N'COLUMN', @level2name = N'ioptnaveg';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Formulario Navegar  1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir', @level2type = N'COLUMN', @level2name = N'ioptnaveg';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir', @level2type = N'COLUMN', @level2name = N'ioptnaveg';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Permiso para eliminar registro en formulario (BIT: 1=Sí, 0=No). Borrado desde entrada de datos individual.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir', @level2type = N'COLUMN', @level2name = N'ioptelimi';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Formulario eliminar  1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir', @level2type = N'COLUMN', @level2name = N'ioptelimi';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir', @level2type = N'COLUMN', @level2name = N'ioptelimi';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Permiso para actualizar/guardar cambios en formulario (BIT: 1=Sí, 0=No). Modifica datos existentes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir', @level2type = N'COLUMN', @level2name = N'ioptactua';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Formulario Actualizar 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir', @level2type = N'COLUMN', @level2name = N'ioptactua';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir', @level2type = N'COLUMN', @level2name = N'ioptactua';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Permiso para guardar como diseño/plantilla en formulario (BIT: 1=Sí, 0=No). Crea copia o variante de registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir', @level2type = N'COLUMN', @level2name = N'ioptdisen';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Formulario Guardar Como Diseñar (Guardar Como) 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir', @level2type = N'COLUMN', @level2name = N'ioptdisen';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir', @level2type = N'COLUMN', @level2name = N'ioptdisen';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Permiso para consultar/leer datos en formulario (BIT: 1=Sí, 0=No). Acceso de lectura a registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir', @level2type = N'COLUMN', @level2name = N'ioptconsu';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Formulario Consultar 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir', @level2type = N'COLUMN', @level2name = N'ioptconsu';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir', @level2type = N'COLUMN', @level2name = N'ioptconsu';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Permiso para guardar/crear nuevo registro en formulario (BIT: 1=Sí, 0=No). Inserta dato nuevo en base.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir', @level2type = N'COLUMN', @level2name = N'ioptguard';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Formulario Guardar 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir', @level2type = N'COLUMN', @level2name = N'ioptguard';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir', @level2type = N'COLUMN', @level2name = N'ioptguard';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del menú en Indigo Vie Cloud (VARCHAR 5). Referencia a opción de navegación del sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir', @level2type = N'COLUMN', @level2name = N'indidmenu';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Menu en INDIGO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir', @level2type = N'COLUMN', @level2name = N'indidmenu';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir', @level2type = N'COLUMN', @level2name = N'indidmenu';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del rol de usuario asignado (CHAR 4, FK a SEGrolesu). Identifica perfil: administrador, médico, enfermera, recepción, etc.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir', @level2type = N'COLUMN', @level2name = N'codigorol';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Rol', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir', @level2type = N'COLUMN', @level2name = N'codigorol';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir', @level2type = N'COLUMN', @level2name = N'codigorol';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Permisos de seguridad por rol y menú: define qué acciones puede realizar cada rol del sistema sobre cada opción de menú, incluyendo guardar, consultar, actualizar, eliminar, imprimir, anular, navegar, configurar y visibilidad, así como permisos sobre grillas (crear, modificar, eliminar).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir';
