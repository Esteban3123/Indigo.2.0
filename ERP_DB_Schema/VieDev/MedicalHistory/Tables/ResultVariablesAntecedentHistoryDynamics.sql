CREATE TABLE [MedicalHistory].[ResultVariablesAntecedentHistoryDynamics] (
    [Id]                     INT            IDENTITY (1, 1) NOT NULL,
    [IDHCHISPACA]            INT            NOT NULL,
    [CodeAntecedent]         INT            NOT NULL,
    [IdHistoryVariables]     INT            NOT NULL,
    [Value]                  VARCHAR (5000) NOT NULL,
    [IdHistoryVariablesList] INT            NULL,
    CONSTRAINT [PK_ResultVariablesAntecedentHistoryDynamics] PRIMARY KEY CLUSTERED ([Id] ASC)
);

