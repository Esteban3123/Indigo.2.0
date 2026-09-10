CREATE TABLE [HumanTalent].[CandidatesSelectionProcess] (
    [Id]                 INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [SelectionProcessId] INT          NOT NULL,
    [CandidateId]        INT          NULL,
    [CandidateEmployee]  VARCHAR (20) NULL,
    [Assessment]         INT          NULL,
    [CreationUser]       VARCHAR (20) NOT NULL,
    [CreationDate]       DATETIME     NOT NULL,
    [ModificationUser]   VARCHAR (20) NULL,
    [ModificationDate]   DATETIME     NULL,
    [PreselectedDate]    DATETIME     NULL,
    CONSTRAINT [PK__Candidat__3214EC0726934F30] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [fk_Candidate] FOREIGN KEY ([CandidateId]) REFERENCES [HumanTalent].[Candidate] ([Id]),
    CONSTRAINT [fk_SelectionProcess] FOREIGN KEY ([SelectionProcessId]) REFERENCES [HumanTalent].[SelectionProcess] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de preselección del candidato en el proceso de selección. Timestamp DATETIME que registra cuándo el candidato fue marcado como Preseleccionado, momento en que otros candidatos son descartados automáticamente.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CandidatesSelectionProcess', @level2type = N'COLUMN', @level2name = N'PreselectedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha preseleccionada', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CandidatesSelectionProcess', @level2type = N'COLUMN', @level2name = N'PreselectedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CandidatesSelectionProcess', @level2type = N'COLUMN', @level2name = N'PreselectedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación del registro de candidato en selección (DATETIME). Auditoría de cambios en evaluación, estado o datos asociados al proceso.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CandidatesSelectionProcess', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CandidatesSelectionProcess', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CandidatesSelectionProcess', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código/login del usuario que modificó por última vez el registro (VARCHAR 20). Referencia a usuario del sistema que editó Assessment, estado o comentarios del candidato.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CandidatesSelectionProcess', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del usuario que modifica el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CandidatesSelectionProcess', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CandidatesSelectionProcess', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de candidato en proceso de selección (DATETIME). Marca cuándo ingresó el candidato al flujo de talento humano.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CandidatesSelectionProcess', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CandidatesSelectionProcess', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CandidatesSelectionProcess', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código/login del usuario que creó el registro (VARCHAR 20). Identifica quién inició el candidato en el módulo de Talento Humano.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CandidatesSelectionProcess', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código usuario de Creación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CandidatesSelectionProcess', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CandidatesSelectionProcess', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valoración/estado del candidato en selección: 1=Descartado, 2=Desiste, 3=Preseleccionado, 4=Semillero, 5=Vinculado. INT. Determina si candidato continúa en proceso, se archiva en semilleros o se vincula como empleado. Descartado/Desiste = sale del proceso. Preseleccionado = descarta otros. Vinculado = pasó a empleado.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CandidatesSelectionProcess', @level2type = N'COLUMN', @level2name = N'Assessment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valoración: campo de selección única, opciones por defecto: 1-Descartado, 2- Desiste, 3- Preseleccionado , 4-Semillero y 5-Vinculado. Si se elige las opciones Descartado o Desiste, el candidato sale del proceso de selección. Si se escoge Semillero, el candidato sale del proceso de selección y se mostrará en el tablero de semilleros. Si se elige la opción Preseleccionado para un candidato, los demás pasan a ser Descartados. Si se tiene el estado 5 es porque el candiato ha sido creado como empleado', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CandidatesSelectionProcess', @level2type = N'COLUMN', @level2name = N'Assessment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CandidatesSelectionProcess', @level2type = N'COLUMN', @level2name = N'Assessment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de empleado asociado al candidato cuando es vinculado (VARCHAR 20). Se registra desde portal de autoservicios. Referencia al Legajo/ID del empleado una vez el candidato es contratado.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CandidatesSelectionProcess', @level2type = N'COLUMN', @level2name = N'CandidateEmployee';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del empleado asociado al proceso de selección, se registra desde el portal de autoservicios', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CandidatesSelectionProcess', @level2type = N'COLUMN', @level2name = N'CandidateEmployee';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CandidatesSelectionProcess', @level2type = N'COLUMN', @level2name = N'CandidateEmployee';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del candidato (INT, FK a HumanTalent.Candidate.Id). Vincula el registro de selección con datos base del candidato del módulo de Talento Humano.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CandidatesSelectionProcess', @level2type = N'COLUMN', @level2name = N'CandidateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del candidato asociado al proceso de selección se registra desde el módulo de talento humano', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CandidatesSelectionProcess', @level2type = N'COLUMN', @level2name = N'CandidateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CandidatesSelectionProcess', @level2type = N'COLUMN', @level2name = N'CandidateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera de proceso de selección (INT, FK a HumanTalent.SelectionProcess.Id). Agrupa todos los candidatos que participan en la misma convocatoria o proceso.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CandidatesSelectionProcess', @level2type = N'COLUMN', @level2name = N'SelectionProcessId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de cabecera', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CandidatesSelectionProcess', @level2type = N'COLUMN', @level2name = N'SelectionProcessId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CandidatesSelectionProcess', @level2type = N'COLUMN', @level2name = N'SelectionProcessId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY, PK) de cada registro candidato-selección. Clave primaria de la tabla CandidatesSelectionProcess, enumeración secuencial.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CandidatesSelectionProcess', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CandidatesSelectionProcess', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CandidatesSelectionProcess', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de candidatos vinculados a un proceso de selección de talento humano. Guarda qué personas (candidatos externos o empleados internos) participan en cada proceso de selección, su evaluación y la fecha en que fueron preseleccionados.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CandidatesSelectionProcess';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CandidatesSelectionProcess';
