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
ALTER TABLE [HumanTalent].[FormsTemplateAnswers] NOCHECK CONSTRAINT [FK_FormsTemplateAnswers_FormsTemplate];


GO
ALTER TABLE [HumanTalent].[FormsTemplateAnswers] NOCHECK CONSTRAINT [FK_FormsTemplateAnswers_FormsTemplateAnswers];


GO
ALTER TABLE [HumanTalent].[FormsTemplateAnswers] NOCHECK CONSTRAINT [FK_FormsTemplateAnswers_PvTypeSelectionProcessesDetail];


GO
ALTER TABLE [HumanTalent].[FormsTemplateAnswers] NOCHECK CONSTRAINT [FK_TemplateAnswers_CandidatesSelectionProcess];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procesos de selección de tipo de PV Detalle Id', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplateAnswers', @level2type = N'COLUMN', @level2name = N'PvTypeSelectionProcessesDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de modificación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplateAnswers', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del usuario que modifica el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplateAnswers', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplateAnswers', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del usuario que realizo el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplateAnswers', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para almacenar la ubicación del archivo o anexo de cada actividad realizada ', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplateAnswers', @level2type = N'COLUMN', @level2name = N'Annexed';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para almacenar el porcentaje total del resultado de la actividad o plantilla realizada', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplateAnswers', @level2type = N'COLUMN', @level2name = N'ActivityResult';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del formulario o plantilla que el encargado del proceso le diligencia al candidato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplateAnswers', @level2type = N'COLUMN', @level2name = N'FormsTemplateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla donde se almacena el Id de candidato, el id del proceso de selección y el id de empleado si es candidato interno.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplateAnswers', @level2type = N'COLUMN', @level2name = N'CandidatesSelectionProcessId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FormsTemplateAnswers', @level2type = N'COLUMN', @level2name = N'Id';

