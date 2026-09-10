CREATE TABLE [HumanTalent].[FormsTemplate] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (20)  NOT NULL,
    [Title]            VARCHAR (50)  NOT NULL,
    [Name]             VARCHAR (80)  NOT NULL,
    [Description]      VARCHAR (110) NULL,
    [State]            TINYINT       NOT NULL,
    [CreationUser]     VARCHAR (20)  NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    CONSTRAINT [PK_FormsTemplate] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación del registro de plantilla de formulario (DATETIME, nullable). Auditoría de cambios', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplate', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplate', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplate', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o usuario que realizó la última modificación del registro (VARCHAR 20, nullable). Usuario que actualizó la plantilla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplate', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del usuario que modifica el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplate', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplate', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de plantilla (DATETIME). Marca de auditoría de origen', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplate', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplate', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplate', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o usuario que creó el registro (VARCHAR 20). Usuario responsable de la plantilla inicial', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplate', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de creación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplate', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplate', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la plantilla de formulario: 0=inactivo, 1=activo (TINYINT). Activo/inactivo, vigencia', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplate', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la plantilla: 0- inactivo, 1-activo', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplate', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplate', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción detallada de la plantilla de formulario (VARCHAR 110, nullable). Proposito, uso, observaciones', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplate', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción de la plantilla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplate', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplate', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo de la plantilla de formulario (VARCHAR 80). Nombre completo, identificación legible', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplate', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la plantilla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplate', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplate', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Título o encabezado de la plantilla para presentación (VARCHAR 50). Título, etiqueta de pantalla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplate', @level2type = N'COLUMN', @level2name = N'Title';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Titúlo para la plantilla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplate', @level2type = N'COLUMN', @level2name = N'Title';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplate', @level2type = N'COLUMN', @level2name = N'Title';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único identificador de la plantilla de formulario (VARCHAR 20). Clave, referencia, SKU de plantilla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplate', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la plantilla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplate', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplate', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único secuencial de la plantilla de formulario (INT IDENTITY). Clave primaria, PK', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplate', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplate', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplate', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Plantillas de formularios del módulo de Talento Humano. Registra las definiciones maestras de formularios (encuestas, evaluaciones, formularios de gestión de personal) con su código, título, nombre y estado, usadas para estructurar procesos de recursos humanos.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplate';
