CREATE TABLE [Glosas].[GlosasPublicParametersInterface] (
    [Id]                              INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ParametersInterfaceId]           INT          NULL,
    [YearAccount]                     INT          NOT NULL,
    [DebitAccount]                    VARCHAR (15) NOT NULL,
    [AcceptanceObjectionsConcept]     VARCHAR (3)  NULL,
    [AcceptanceObjectionsConceptPast] VARCHAR (3)  NULL,
    [AcceptancReiterationsConcept]    VARCHAR (3)  NULL,
    [TimeStamp]                       ROWVERSION   NOT NULL,
    CONSTRAINT [PK_GlosasPublicParametersInterface] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GlosasPublicParametersInterface_GlosasParametersInterface] FOREIGN KEY ([ParametersInterfaceId]) REFERENCES [Glosas].[GlosasParametersInterface] ([Id])
);


GO
ALTER TABLE [Glosas].[GlosasPublicParametersInterface] NOCHECK CONSTRAINT [FK_GlosasPublicParametersInterface_GlosasParametersInterface];

