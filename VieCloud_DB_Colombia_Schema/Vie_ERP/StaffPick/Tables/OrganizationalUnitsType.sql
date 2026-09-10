CREATE TABLE [StaffPick].[OrganizationalUnitsType] (
    [Id]               INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (20) NOT NULL,
    [Name]             VARCHAR (80) NOT NULL,
    [Status]           BIT          NOT NULL,
    [CreationUser]     VARCHAR (20) NOT NULL,
    [CreationDate]     DATETIME     CONSTRAINT [DF_OrganizationalUnitsType_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser] VARCHAR (20) NULL,
    [ModificationDate] DATETIME     NULL,
    CONSTRAINT [PK_OrganizationalUnitsType] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ__Organiza__A25C5AA79AA1123B] UNIQUE NONCLUSTERED ([Code] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del registro de tipo de unidad organizativa (DATETIME, auditoría)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'OrganizationalUnitsType', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'OrganizationalUnitsType', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'OrganizationalUnitsType', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación del tipo de unidad organizativa (VARCHAR 20, auditoría)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'OrganizationalUnitsType', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modifación', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'OrganizationalUnitsType', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'OrganizationalUnitsType', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de tipo de unidad organizativa (DATETIME, auditoría, default sistema)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'OrganizationalUnitsType', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Creación', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'OrganizationalUnitsType', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'OrganizationalUnitsType', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro del tipo de unidad organizativa (VARCHAR 20, auditoría)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'OrganizationalUnitsType', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'OrganizationalUnitsType', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'OrganizationalUnitsType', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del tipo de unidad organizativa: 1 = Activo, 0 = Inactivo (BIT, booleano)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'OrganizationalUnitsType', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado (1 - Activo, 0 - Inactivo)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'OrganizationalUnitsType', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'OrganizationalUnitsType', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo del tipo de unidad organizativa o centro de atención (VARCHAR 80, ej: Urgencias, Consulta Externa, Laboratorio)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'OrganizationalUnitsType', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la Unidad Organizativa', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'OrganizationalUnitsType', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'OrganizationalUnitsType', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del tipo de unidad organizativa, identificador corto para referencias (VARCHAR 20, UNIQUE, clave de negocio)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'OrganizationalUnitsType', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del Tupo de Unidad Organizativa', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'OrganizationalUnitsType', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'OrganizationalUnitsType', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único y autonumérico del tipo de unidad organizativa (INT IDENTITY, clave primaria)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'OrganizationalUnitsType', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Autonumérico', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'OrganizationalUnitsType', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'OrganizationalUnitsType', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de tipos de unidades organizacionales (por ejemplo: departamento, área, sede, unidad funcional). Permite clasificar y gestionar las distintas categorías bajo las cuales se organizan las unidades dentro de la institución.', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'OrganizationalUnitsType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'OrganizationalUnitsType';
