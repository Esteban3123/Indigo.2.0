CREATE TABLE [WHS].[WHSRole] (
    [Id]               INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (20) NOT NULL,
    [Name]             VARCHAR (80) NOT NULL,
    [State]            BIT          NOT NULL,
    [CreationUser]     VARCHAR (20) CONSTRAINT [DF_WHSRole_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]     DATETIME     CONSTRAINT [DF_WHSRole_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser] VARCHAR (20) NULL,
    [ModificationDate] DATETIME     NULL,
    CONSTRAINT [PK_WHSRole] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ__WHSRole__A25C5AA749CA2641] UNIQUE NONCLUSTERED ([Code] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación (DATETIME, auditoria), timestamp del cambio más reciente del rol', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'WHSRole', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'WHSRole', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'WHSRole', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de última modificación (VARCHAR 20, auditoria), quién cambió el rol más recientemente', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'WHSRole', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Modificación de Usuario', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'WHSRole', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'WHSRole', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación (DATETIME, auditoria), timestamp del momento en que se registró el rol', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'WHSRole', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'WHSRole', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'WHSRole', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario creador (VARCHAR 20, auditoria), identificación de quién creó el registro del rol', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'WHSRole', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Creación de Usuario', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'WHSRole', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'WHSRole', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado activo/inactivo (BIT, 1=activo 0=inactivo), indica si el rol está vigente o deshabilitado', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'WHSRole', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'WHSRole', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'WHSRole', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo del rol (VARCHAR 80), denominación legible del rol de usuario en almacén o warehouse', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'WHSRole', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'WHSRole', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'WHSRole', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del rol (VARCHAR 20, único), identificador alfanumérico del rol para búsqueda y referencia en sistemas', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'WHSRole', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'WHSRole', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'WHSRole', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, autonumérico), clave primaria del rol de almacén', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'WHSRole', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'WHSRole', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'WHSRole', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Roles o perfiles de acceso del módulo de bodegas/almacén (WHS). Define los distintos niveles de permisos o funciones que pueden asignarse a los usuarios dentro del sistema de gestión de inventario y suministros.', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'WHSRole';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'WHSRole';
