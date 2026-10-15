CREATE TABLE [Admissions].[TriageParametersEntity] (
    [id]          INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [EntityCode]  CHAR (9)      NOT NULL,
    [State]       BIT           NOT NULL,
    [Observation] VARCHAR (300) NOT NULL,
    [AppliedIV]   BIT           NOT NULL,
    [AppliedV]    BIT           NOT NULL,
    CONSTRAINT [PK_TriageParametersEntity] PRIMARY KEY CLUSTERED ([id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Parametrizacion aplica a Triage V', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TriageParametersEntity', @level2type = N'COLUMN', @level2name = N'AppliedV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Parametrizacion aplica a Triage IV', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TriageParametersEntity', @level2type = N'COLUMN', @level2name = N'AppliedIV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TriageParametersEntity', @level2type = N'COLUMN', @level2name = N'Observation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TriageParametersEntity', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la entidad parametrizada', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TriageParametersEntity', @level2type = N'COLUMN', @level2name = N'EntityCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TriageParametersEntity', @level2type = N'COLUMN', @level2name = N'id';

