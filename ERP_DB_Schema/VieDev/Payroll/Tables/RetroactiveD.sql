CREATE TABLE [Payroll].[RetroactiveD] (
    [Id]                          INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdRetroactiveC]              INT          NOT NULL,
    [IdConcept]                   INT          NOT NULL,
    [ValueConcept]                NUMERIC (18) NOT NULL,
    [ValueConceptWithRetroactive] NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_RetroactiveD] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RetroactiveD_Concept] FOREIGN KEY ([IdConcept]) REFERENCES [Payroll].[Concept] ([Id]),
    CONSTRAINT [FK_RetroactiveD_RetroactiveD] FOREIGN KEY ([IdRetroactiveC]) REFERENCES [Payroll].[RetroactiveC] ([Id])
);


GO
ALTER TABLE [Payroll].[RetroactiveD] NOCHECK CONSTRAINT [FK_RetroactiveD_Concept];


GO
ALTER TABLE [Payroll].[RetroactiveD] NOCHECK CONSTRAINT [FK_RetroactiveD_RetroactiveD];

