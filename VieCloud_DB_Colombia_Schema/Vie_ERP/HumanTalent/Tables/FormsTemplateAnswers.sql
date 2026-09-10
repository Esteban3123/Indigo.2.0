CREATE TABLE [HumanTalent].[FormsTemplateAnswers] (
    [Id]                               INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CandidatesSelectionProcessId]     INT             NOT NULL,
    [FormsTemplateId]                  INT             NOT NULL,
    [ActivityResult]                   DECIMAL (18, 2) NULL,
    [Annexed]                          VARCHAR (100)   NULL,
    [CreationUser]                     VARCHAR (20)    NOT NULL,
    [CreationDate]                     DATETIME        NOT NULL,
    [ModificationUser]                 VARCHAR (20)    NULL,
    [ModificationDate]                 DATETIME        NULL,
    [PvTypeSelectionProcessesDetailId] INT             NULL,
    CONSTRAINT [PK_FormsTemplateAnswers] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FormsTemplateAnswers_FormsTemplate] FOREIGN KEY ([FormsTemplateId]) REFERENCES [HumanTalent].[FormsTemplate] ([Id]),
    CONSTRAINT [FK_FormsTemplateAnswers_FormsTemplateAnswers] FOREIGN KEY ([Id]) REFERENCES [HumanTalent].[FormsTemplateAnswers] ([Id]),
    CONSTRAINT [FK_FormsTemplateAnswers_PvTypeSelectionProcessesDetail] FOREIGN KEY ([PvTypeSelectionProcessesDetailId]) REFERENCES [HumanTalent].[PvTypeSelectionProcessesDetail] ([Id]),
    CONSTRAINT [FK_TemplateAnswers_CandidatesSelectionProcess] FOREIGN KEY ([CandidatesSelectionProcessId]) REFERENCES [HumanTalent].[CandidatesSelectionProcess] ([Id])
);




GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de detalle de tipo de proceso de selección PV (FK a PvTypeSelectionProcessesDetail). Referencia al tipo específico de etapa o prueba dentro del proceso de selección de personal.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplateAnswers', @level2type = N'COLUMN', @level2name = N'PvTypeSelectionProcessesDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Procesos de selección de tipo de PV Detalle Id', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplateAnswers', @level2type = N'COLUMN', @level2name = N'PvTypeSelectionProcessesDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplateAnswers', @level2type = N'COLUMN', @level2name = N'PvTypeSelectionProcessesDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de la última modificación del registro de respuesta del formulario. Auditoría de cambios en las respuestas y actividades.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplateAnswers', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplateAnswers', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplateAnswers', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de usuario (VARCHAR 20) que realizó la última modificación del registro. Trazabilidad de quién actualizó la respuesta o resultado.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplateAnswers', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del usuario que modifica el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplateAnswers', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplateAnswers', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de creación del registro de respuesta. Marca temporal de cuándo se registró la actividad o formulario completado.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplateAnswers', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplateAnswers', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplateAnswers', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de usuario (VARCHAR 20) que creó el registro de respuesta. Identifica quién diligencia o registra inicialmente la respuesta del candidato.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplateAnswers', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del usuario que realizo el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplateAnswers', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplateAnswers', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ruta, ubicación o nombre del archivo anexado (VARCHAR 100) asociado a la actividad, prueba o formulario. Almacena documentos, evidencias, certificados o archivos complementarios del proceso.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplateAnswers', @level2type = N'COLUMN', @level2name = N'Annexed';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para almacenar la ubicación del archivo o anexo de cada actividad realizada ', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplateAnswers', @level2type = N'COLUMN', @level2name = N'Annexed';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplateAnswers', @level2type = N'COLUMN', @level2name = N'Annexed';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje numérico (DECIMAL 18,2) del resultado total o calificación de la actividad, prueba o plantilla completada por el candidato. Puntuación o desempeño en la etapa de selección.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplateAnswers', @level2type = N'COLUMN', @level2name = N'ActivityResult';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para almacenar el porcentaje total del resultado de la actividad o plantilla realizada', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplateAnswers', @level2type = N'COLUMN', @level2name = N'ActivityResult';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplateAnswers', @level2type = N'COLUMN', @level2name = N'ActivityResult';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK a FormsTemplate) del formulario o plantilla de preguntas que el encargado del proceso diligencia al candidato durante la evaluación de selección.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplateAnswers', @level2type = N'COLUMN', @level2name = N'FormsTemplateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del formulario o plantilla que el encargado del proceso le diligencia al candidato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplateAnswers', @level2type = N'COLUMN', @level2name = N'FormsTemplateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplateAnswers', @level2type = N'COLUMN', @level2name = N'FormsTemplateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK a CandidatesSelectionProcess) que vincula el candidato, el proceso de selección y el empleado (si es candidato interno). Relación principal del registro de respuestas.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplateAnswers', @level2type = N'COLUMN', @level2name = N'CandidatesSelectionProcessId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla donde se almacena el Id de candidato, el id del proceso de selección y el id de empleado si es candidato interno.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplateAnswers', @level2type = N'COLUMN', @level2name = N'CandidatesSelectionProcessId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplateAnswers', @level2type = N'COLUMN', @level2name = N'CandidatesSelectionProcessId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, PK, IDENTITY) del registro de respuesta de formulario. Clave primaria de la tabla FormsTemplateAnswers.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplateAnswers', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplateAnswers', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplateAnswers', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Respuestas y resultados de formularios de evaluación aplicados a candidatos en los procesos de selección de personal. Registra qué plantilla de formulario se usó, el puntaje o resultado obtenido y los anexos asociados a cada etapa del proceso.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplateAnswers';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplateAnswers';
