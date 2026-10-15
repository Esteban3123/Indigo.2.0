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
ALTER TABLE [HumanTalent].[CandidatesSelectionProcess] NOCHECK CONSTRAINT [fk_Candidate];


GO
ALTER TABLE [HumanTalent].[CandidatesSelectionProcess] NOCHECK CONSTRAINT [fk_SelectionProcess];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha preseleccionada', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CandidatesSelectionProcess', @level2type = N'COLUMN', @level2name = N'PreselectedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de modificación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CandidatesSelectionProcess', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del usuario que modifica el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CandidatesSelectionProcess', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CandidatesSelectionProcess', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código usuario de Creación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CandidatesSelectionProcess', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valoración: campo de selección única, opciones por defecto: 1-Descartado, 2- Desiste, 3- Preseleccionado , 4-Semillero y 5-Vinculado. Si se elige las opciones Descartado o Desiste, el candidato sale del proceso de selección. Si se escoge Semillero, el candidato sale del proceso de selección y se mostrará en el tablero de semilleros. Si se elige la opción Preseleccionado para un candidato, los demás pasan a ser Descartados. Si se tiene el estado 5 es porque el candiato ha sido creado como empleado', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CandidatesSelectionProcess', @level2type = N'COLUMN', @level2name = N'Assessment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del empleado asociado al proceso de selección, se registra desde el portal de autoservicios', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CandidatesSelectionProcess', @level2type = N'COLUMN', @level2name = N'CandidateEmployee';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del candidato asociado al proceso de selección se registra desde el módulo de talento humano', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CandidatesSelectionProcess', @level2type = N'COLUMN', @level2name = N'CandidateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de cabecera', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CandidatesSelectionProcess', @level2type = N'COLUMN', @level2name = N'SelectionProcessId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CandidatesSelectionProcess', @level2type = N'COLUMN', @level2name = N'Id';

