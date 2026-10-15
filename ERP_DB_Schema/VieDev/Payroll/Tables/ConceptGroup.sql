CREATE TABLE [Payroll].[ConceptGroup] (
    [Id]              INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ConceptId]       INT     NOT NULL,
    [GroupId]         INT     NOT NULL,
    [LiquidateBy]     TINYINT NOT NULL,
    [MaximunDiscount] BIT     NOT NULL,
    CONSTRAINT [PK_ConceptGroup] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ConceptGroup_Concept] FOREIGN KEY ([ConceptId]) REFERENCES [Payroll].[Concept] ([Id]),
    CONSTRAINT [FK_ConceptGroup_Group] FOREIGN KEY ([GroupId]) REFERENCES [Payroll].[Group] ([Id])
);


GO
ALTER TABLE [Payroll].[ConceptGroup] NOCHECK CONSTRAINT [FK_ConceptGroup_Concept];


GO
ALTER TABLE [Payroll].[ConceptGroup] NOCHECK CONSTRAINT [FK_ConceptGroup_Group];

