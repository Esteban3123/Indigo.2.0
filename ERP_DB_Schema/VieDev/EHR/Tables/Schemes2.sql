CREATE TABLE [EHR].[Schemes2] (
    [Code]                   VARCHAR (20)  NOT NULL,
    [Description]            VARCHAR (250) NOT NULL,
    [Cycles]                 INT           NOT NULL,
    [State]                  BIT           NOT NULL,
    [ServiceIPS]             CHAR (20)     NOT NULL,
    [DurationAdministration] INT           NOT NULL,
    [TypeScheme]             INT           NOT NULL,
    [RestDay]                INT           NULL,
    [Observation]            VARCHAR (MAX) NULL,
    [CreationUser]           VARCHAR (20)  NOT NULL,
    [CreationDate]           DATETIME      NOT NULL,
    [ModificationUser]       VARCHAR (20)  NULL,
    [ModificationDate]       DATETIME      NULL,
    [TimeStamp]              ROWVERSION    NOT NULL
);

