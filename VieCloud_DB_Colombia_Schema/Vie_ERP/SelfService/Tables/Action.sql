CREATE TABLE [SelfService].[Action] (
    [Id]               INT           IDENTITY (1, 1) NOT NULL,
    [Code]             VARCHAR (20)  NOT NULL,
    [Name]             VARCHAR (100) NOT NULL,
    [Status]           BIT           NOT NULL,
    [CreationUser]     VARCHAR (20)  NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    CONSTRAINT [PK_Action] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de acciones disponibles en el módulo de autoservicio (SelfService). Registra las operaciones o funciones que los usuarios pueden ejecutar desde el portal de autogestión.', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'Action';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'Action';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno de la acción (autogenerado).', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'Action', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'Action', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código corto que identifica la acción en el sistema, usado para referencias internas.', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'Action', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'Action', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo de la acción tal como se muestra al usuario en el autoservicio.', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'Action', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'Action', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado activo o inactivo de la acción (1 = activa, 0 = inactiva).', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'Action', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'Action', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro de la acción.', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'Action', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'Action', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se creó la acción.', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'Action', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'Action', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación al registro de la acción.', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'Action', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'Action', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación al registro de la acción.', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'Action', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'Action', @level2type = N'COLUMN', @level2name = N'ModificationDate';
