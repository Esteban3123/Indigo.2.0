CREATE TABLE [MedicalHistory].[ResultVariablesHistoryDynamicsIntra] (
    [Id]                     INT            IDENTITY (1, 1) NOT NULL,
    [IDHCHISPACA]            INT            NOT NULL,
    [IdHistoryPages]         INT            NOT NULL,
    [IdHistoryGroups]        INT            NOT NULL,
    [IdHistoryVariables]     INT            NOT NULL,
    [Value]                  VARCHAR (5000) NULL,
    [IdHistoryVariablesList] INT            NULL,
    CONSTRAINT [PK_ResultVariablesHistoryDynamicsIntra] PRIMARY KEY CLUSTERED ([Id] ASC)
);

