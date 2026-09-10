CREATE TABLE [WHS].[Activities] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (20)  NOT NULL,
    [Name]             VARCHAR (350) NOT NULL,
    [State]            BIT           NOT NULL,
    [CreationUser]     VARCHAR (20)  NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    CONSTRAINT [PK_Activities] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ__Activiti__A25C5AA7369B44A7] UNIQUE NONCLUSTERED ([Code] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del registro de actividad SST (DATETIME). Auditoría de cambios.', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'Activities', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'Activities', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'Activities', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación del registro de actividad SST (VARCHAR 20). Trazabilidad de cambios.', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'Activities', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'Activities', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'Activities', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de actividad SST (DATETIME). Marca temporal de origen.', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'Activities', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Creación', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'Activities', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'Activities', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro de actividad SST (VARCHAR 20). Trazabilidad de auditoría.', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'Activities', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'Activities', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'Activities', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del registro: 1 = Activo, 0 = Inactivo. Controla visibilidad y disponibilidad de la actividad SST (BIT).', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'Activities', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado: 1 - Activo, 0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'Activities', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'Activities', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo de la actividad de Salud y Seguridad en el Trabajo (SST), procedimiento o tarea ocupacional (VARCHAR 350).', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'Activities', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de Actividades SST', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'Activities', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'Activities', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único identificador de la actividad SST. Clave única (UNIQUE, VARCHAR 20). Referencia normalizada.', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'Activities', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de Actividades SST', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'Activities', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'Activities', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único secuencial de la actividad SST (INT IDENTITY). Clave primaria de la tabla.', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'Activities', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de Actividades SST', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'Activities', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'Activities', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de actividades del módulo de bodega/almacén (WHS). Registra las actividades o tareas disponibles, con su estado activo/inactivo y trazabilidad de creación y modificación.', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'Activities';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'Activities';
