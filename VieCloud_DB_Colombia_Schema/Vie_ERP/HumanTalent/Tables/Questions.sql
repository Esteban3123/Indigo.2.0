CREATE TABLE [HumanTalent].[Questions] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AnswerId]         INT           NOT NULL,
    [Code]             VARCHAR (20)  NOT NULL,
    [Name]             VARCHAR (80)  NOT NULL,
    [Description]      VARCHAR (110) NOT NULL,
    [State]            TINYINT       NOT NULL,
    [CreationUser]     VARCHAR (20)  NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    CONSTRAINT [PK_Questions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Questions_Answer] FOREIGN KEY ([AnswerId]) REFERENCES [HumanTalent].[Answer] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del registro (DATETIME, nullable). Marca temporal para control de cambios y auditoría.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Questions', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Questions', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Questions', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario que realizó la última modificación del registro (VARCHAR 20, nullable). Identificación PII auditada.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Questions', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del usuario que modifica el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Questions', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Questions', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de la pregunta (DATETIME). Marca temporal para auditoría.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Questions', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Questions', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Questions', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario que creó el registro de la pregunta (VARCHAR 20). Identificación PII auditada.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Questions', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de usuario que modifica el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Questions', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Questions', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de activación de la pregunta: 0=Inactivo, 1=Activo. TINYINT. Controla disponibilidad en encuestas y cuestionarios.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Questions', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la pregunta: 0- Inactivo, 1-Activo  ', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Questions', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Questions', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción detallada o contexto adicional de la pregunta (VARCHAR 110). Amplía el entendimiento del propósito o alcance de la pregunta.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Questions', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de la pregunta', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Questions', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Questions', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o enunciado principal de la pregunta (VARCHAR 80). Texto descriptivo que identifica el tema o aspecto a evaluar.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Questions', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la pregunta', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Questions', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Questions', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código alfanumérico único de la pregunta (VARCHAR 20). Usado para identificación legible en procesos de encuesta, evaluación o cuestionario.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Questions', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la pregunta', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Questions', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Questions', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del grupo de respuestas asociado a la pregunta. Referencia a [HumanTalent].[Answer]. Tipo INT.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Questions', @level2type = N'COLUMN', @level2name = N'AnswerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del grupo de respuestas ', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Questions', @level2type = N'COLUMN', @level2name = N'AnswerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Questions', @level2type = N'COLUMN', @level2name = N'AnswerId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK) de la pregunta en la tabla Questions. Autonumérico de tipo INT.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Questions', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Questions', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Questions', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Preguntas del módulo de Talento Humano, cada una asociada a una respuesta esperada. Permite configurar cuestionarios o evaluaciones para el personal, con su código, nombre, descripción y estado activo/inactivo.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Questions';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Questions';
