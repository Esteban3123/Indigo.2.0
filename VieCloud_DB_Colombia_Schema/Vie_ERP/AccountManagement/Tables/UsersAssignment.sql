CREATE TABLE [AccountManagement].[UsersAssignment] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [UserCode]  VARCHAR (20)   NOT NULL,
    [FullName]  NVARCHAR (100) NOT NULL,
    [Status]    BIT            DEFAULT ((1)) NOT NULL,
    [EntryType] TINYINT        DEFAULT ((0)) NOT NULL,
    [IsRemoved] BIT            CONSTRAINT [DF_UsersAssignment_IsRemoved] DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de ingreso/atención del usuario asignado: 1=Ambulatorio (consulta externa), 2=Hospitalario (internación), 3=Ambos tipos. Clasificación para control de acceso por modalidad de servicio.', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'UsersAssignment', @level2type = N'COLUMN', @level2name = N'EntryType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Ingreso, 1 = Ambulatorio, 2 = Hospitalario, 3  = Ambos', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'UsersAssignment', @level2type = N'COLUMN', @level2name = N'EntryType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'UsersAssignment', @level2type = N'COLUMN', @level2name = N'EntryType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado operacional del usuario: 1=Activo (habilitado para usar sistema), 0=Inactivo (deshabilitado). Por defecto es 1. Controla permiso de acceso.', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'UsersAssignment', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del usuario asignado, por defecto es 1 = Activo. 0 = Inactivo', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'UsersAssignment', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'UsersAssignment', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo del usuario asignado. Campo de texto (NVARCHAR 100) que identifica al profesional de salud o personal administrativo del sistema.', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'UsersAssignment', @level2type = N'COLUMN', @level2name = N'FullName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre completo del usuario', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'UsersAssignment', @level2type = N'COLUMN', @level2name = N'FullName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'UsersAssignment', @level2type = N'COLUMN', @level2name = N'FullName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del usuario dentro del Tenant/Organización. Identificador VARCHAR(20) para login y búsqueda en ERP/EHR Indigo Vie Cloud.', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'UsersAssignment', @level2type = N'COLUMN', @level2name = N'UserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del usuario del Tenant', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'UsersAssignment', @level2type = N'COLUMN', @level2name = N'UserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'UsersAssignment', @level2type = N'COLUMN', @level2name = N'UserCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico único (INT IDENTITY) de la tabla UsersAssignment. Clave primaria, incremento automático.', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'UsersAssignment', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'UsersAssignment', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'UsersAssignment', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Flag lógico (BIT) para soft-delete: 0=registro activo, 1=registro eliminado lógicamente. Preserva trazabilidad sin borrar datos físicos.', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'UsersAssignment', @level2type = N'COLUMN', @level2name = N'IsRemoved';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Flag para soft-deletes', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'UsersAssignment', @level2type = N'COLUMN', @level2name = N'IsRemoved';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'UsersAssignment', @level2type = N'COLUMN', @level2name = N'IsRemoved';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de usuarios asignados en el sistema de gestión de cuentas. Controla qué usuarios están activos, su tipo de acceso y si han sido eliminados lógicamente.', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'UsersAssignment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'UsersAssignment';
