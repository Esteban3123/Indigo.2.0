CREATE TABLE [StaffPick].[ExternEntities] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (20)  NOT NULL,
    [Name]             VARCHAR (200) NULL,
    [Type]             TINYINT       NOT NULL,
    [State]            BIT           NOT NULL,
    [CreationUser]     VARCHAR (20)  CONSTRAINT [DF_ExternEntities_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]     DATETIME      CONSTRAINT [DF_ExternEntities_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    CONSTRAINT [PK_ExternEntities] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ__ExternEn__A25C5AA754EEDBEB] UNIQUE NONCLUSTERED ([Code] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del registro de la entidad externa (DATETIME, auditoría)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'ExternEntities', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'ExternEntities', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'ExternEntities', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación de la entidad externa (VARCHAR 20, auditoría)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'ExternEntities', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'ExternEntities', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'ExternEntities', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de la entidad externa (DATETIME, auditoría)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'ExternEntities', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Creación', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'ExternEntities', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'ExternEntities', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro de la entidad externa (VARCHAR 20, auditoría, por defecto 999)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'ExternEntities', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'ExternEntities', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'ExternEntities', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la entidad externa: 1=Activo, 0=Inactivo (BIT, booleano)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'ExternEntities', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado Entidades Externas: 1 - Activo, 0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'ExternEntities', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'ExternEntities', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de entidad externa: 1=Centro Educativo, 2=Centro Médico, 3=Fuente de Selección, 4=Proveedor de RR.HH, 5=Centro de Formación (TINYINT)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'ExternEntities', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipos de Entidades Externas: 1 - Centros Educativos, 2 - Centros Médicos, 3 - Fuentes de Selección, 4 - Proveedores de RR.HH, 5 - Centros de Formación', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'ExternEntities', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'ExternEntities', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o razón social de la entidad externa (Centro médico, educativo, proveedor, etc.) (VARCHAR 200)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'ExternEntities', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Entidades Externas', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'ExternEntities', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'ExternEntities', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador único de la entidad externa (VARCHAR 20, clave única, UNIQUE)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'ExternEntities', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código Entidades Externas', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'ExternEntities', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'ExternEntities', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico único de la entidad externa (INT IDENTITY, clave primaria)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'ExternEntities', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Entidades Externas', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'ExternEntities', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'ExternEntities', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de entidades externas (aseguradoras, EPS, IPS, empresas u otras organizaciones) que interactúan con el sistema. Permite clasificarlas por tipo y controlar su estado activo/inactivo para uso en contratos, facturación y autorizaciones.', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'ExternEntities';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'ExternEntities';
