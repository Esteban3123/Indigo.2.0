CREATE TABLE [StaffPick].[ResponsibleTypeHierarchy] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (20)  NOT NULL,
    [Description]      VARCHAR (200) NOT NULL,
    [Status]           BIT           NOT NULL,
    [CreationUser]     VARCHAR (20)  NOT NULL,
    [CreationDate]     DATETIME      CONSTRAINT [DF_ResponsibleTypeHierarchy_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    CONSTRAINT [PK_ResponsibleTypeHierarchy] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ__Responsi__A25C5AA74F5D61E6] UNIQUE NONCLUSTERED ([Code] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del registro de tipo de responsable (DATETIME, nullable)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'ResponsibleTypeHierarchy', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Modificación', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'ResponsibleTypeHierarchy', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'ResponsibleTypeHierarchy', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación del tipo de responsable (VARCHAR 20, nullable)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'ResponsibleTypeHierarchy', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'ResponsibleTypeHierarchy', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'ResponsibleTypeHierarchy', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de tipo de responsable (DATETIME, valor por defecto: fecha actual)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'ResponsibleTypeHierarchy', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'ResponsibleTypeHierarchy', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'ResponsibleTypeHierarchy', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro del tipo de responsable (VARCHAR 20, requerido)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'ResponsibleTypeHierarchy', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'ResponsibleTypeHierarchy', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'ResponsibleTypeHierarchy', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del tipo de responsable: 1=Activo, 0=Inactivo (BIT)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'ResponsibleTypeHierarchy', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado (1 - Activo, 0 - Inactivo)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'ResponsibleTypeHierarchy', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'ResponsibleTypeHierarchy', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del tipo de responsable en la jerarquía organizativa; texto identificador del rol o cargo del profesional de salud, unidad funcional o centro de atención (VARCHAR 200)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'ResponsibleTypeHierarchy', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción del Tipo de Reponsable de la Jerarquía Organizativa', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'ResponsibleTypeHierarchy', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'ResponsibleTypeHierarchy', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del tipo de responsable en la jerarquía organizativa; identificador alfanumérico para clasificar roles, profesionales o áreas funcionales (VARCHAR 20, clave única)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'ResponsibleTypeHierarchy', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Tipo de Responsable de la Jerarquia Organizativa', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'ResponsibleTypeHierarchy', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'ResponsibleTypeHierarchy', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK) del tipo de responsable en la jerarquía organizativa (INT IDENTITY)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'ResponsibleTypeHierarchy', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Tipo de Responsable', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'ResponsibleTypeHierarchy', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'ResponsibleTypeHierarchy', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipos de jerarquía de responsables o responsabilidades en la atención, usados para clasificar roles como titular, beneficiario, acompañante u otras categorías de responsabilidad dentro del proceso asistencial o administrativo.', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'ResponsibleTypeHierarchy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'ResponsibleTypeHierarchy';
