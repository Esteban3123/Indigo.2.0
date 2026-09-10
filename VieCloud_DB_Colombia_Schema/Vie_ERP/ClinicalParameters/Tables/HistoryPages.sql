CREATE TABLE [ClinicalParameters].[HistoryPages] (
    [Id]               INT           IDENTITY (1, 1) NOT NULL,
    [Code]             VARCHAR (20)  NOT NULL,
    [Name]             VARCHAR (100) NOT NULL,
    [Status]           BIT           NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [CreationUser]     VARCHAR (20)  NOT NULL,
    [ModificationDate] DATETIME      NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    CONSTRAINT [PK_HistoryPages] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que modificó (VARCHAR 20) la página de historia clínica; registro de quién realizó el último cambio o ajuste.', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryPages', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de modificación de la página de la historia clínica', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryPages', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryPages', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación (DATETIME NULL) de la página de historia clínica; auditoría de cambios en configuración.', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryPages', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación de la página de la historia clínica', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryPages', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryPages', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó (VARCHAR 20) la página de historia clínica; registro de auditoría de quién registró la página inicial.', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryPages', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de creación de la página de la historia clínica', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryPages', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryPages', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación (DATETIME) de la página de historia clínica; registro de auditoría de cuándo se registró la página.', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryPages', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación de la página de la historia clínica', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryPages', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryPages', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado activo/inactivo (BIT) de la página de historia clínica; indica si la página está habilitada o deshabilitada en el sistema.', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryPages', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la página de la historia clínica', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryPages', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryPages', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo (VARCHAR 100) de la página de historia clínica; título de la sección, apartado o formulario clínico.', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryPages', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la página de la historia clínica', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryPages', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryPages', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código alfanumérico (VARCHAR 20) que identifica la página de historia clínica; código único del formulario o sección clínica.', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryPages', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la página de la historia clínica', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryPages', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryPages', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo único (INT IDENTITY), clave primaria de la página de historia clínica.', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryPages', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryPages', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryPages', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Páginas o secciones de la historia clínica. Registra cada página o módulo disponible dentro de la historia clínica del paciente, indicando su nombre, estado activo/inactivo y datos de auditoría de creación y modificación.', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryPages';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryPages';
