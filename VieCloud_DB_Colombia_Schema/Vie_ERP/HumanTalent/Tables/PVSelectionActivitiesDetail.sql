CREATE TABLE [HumanTalent].[PVSelectionActivitiesDetail] (
    [Id]                    INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [SelectionActivitiesId] INT          NOT NULL,
    [FormsTemplateId]       INT          NOT NULL,
    [CreationUser]          VARCHAR (20) NOT NULL,
    [CreationDate]          DATETIME     NOT NULL,
    [ModificationUser]      VARCHAR (20) NULL,
    [ModificationDate]      DATETIME     NULL,
    CONSTRAINT [PK_PVSelectionActivitiesDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PVSelectionActivitiesDetail_FormsTemplate] FOREIGN KEY ([FormsTemplateId]) REFERENCES [HumanTalent].[FormsTemplate] ([Id]),
    CONSTRAINT [FK_PVSelectionActivitiesDetail_SelectionActivities] FOREIGN KEY ([SelectionActivitiesId]) REFERENCES [HumanTalent].[SelectionActivities] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación del registro de detalle de actividad de selección; DATETIME; auditoria de cambios.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVSelectionActivitiesDetail', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modifiación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVSelectionActivitiesDetail', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVSelectionActivitiesDetail', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o identificador del usuario que realizó la última modificación del registro; VARCHAR(20); auditoría de cambios en gestión de talento humano.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVSelectionActivitiesDetail', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de usuario que modifica el registro ', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVSelectionActivitiesDetail', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVSelectionActivitiesDetail', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de detalle de actividad de selección; DATETIME; trazabilidad de origen.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVSelectionActivitiesDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVSelectionActivitiesDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVSelectionActivitiesDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o identificador del usuario que creó el registro; VARCHAR(20); auditoría de origen en proceso de selección de personal.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVSelectionActivitiesDetail', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del usuario que crea el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVSelectionActivitiesDetail', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVSelectionActivitiesDetail', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de plantilla de formulario o cuestionario; INT; FK→FormsTemplate; contiene preguntas predefinidas que inician el flujo de preguntas y respuestas en la evaluación.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVSelectionActivitiesDetail', @level2type = N'COLUMN', @level2name = N'FormsTemplateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de formulario o plantillas (Contienen preguntas y desde ahí inicia el proceso de preguntas y respuestas)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVSelectionActivitiesDetail', @level2type = N'COLUMN', @level2name = N'FormsTemplateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVSelectionActivitiesDetail', @level2type = N'COLUMN', @level2name = N'FormsTemplateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la actividad de selección padre (cabecera); INT; FK→SelectionActivities; agrupa detalles de pruebas, entrevistas o evaluaciones.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVSelectionActivitiesDetail', @level2type = N'COLUMN', @level2name = N'SelectionActivitiesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de cabecera', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVSelectionActivitiesDetail', @level2type = N'COLUMN', @level2name = N'SelectionActivitiesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVSelectionActivitiesDetail', @level2type = N'COLUMN', @level2name = N'SelectionActivitiesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único primario del detalle de actividad de selección; INT IDENTITY; clave de auditoría y referencia en proceso de reclutamiento y talento humano.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVSelectionActivitiesDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVSelectionActivitiesDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVSelectionActivitiesDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de actividades de selección de personal: registra los formularios o plantillas asociados a cada actividad del proceso de selección de talento humano (entrevistas, pruebas, evaluaciones), junto con la trazabilidad de creación y modificación del registro.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVSelectionActivitiesDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVSelectionActivitiesDetail';
