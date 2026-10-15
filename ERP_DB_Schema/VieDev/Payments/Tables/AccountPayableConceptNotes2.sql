CREATE TABLE [Payments].[AccountPayableConceptNotes2] (
    [Code]         VARCHAR (20)  NOT NULL,
    [Name]         VARCHAR (100) NOT NULL,
    [ConceptType]  TINYINT       NOT NULL,
    [IdAccount]    VARCHAR (20)  NULL,
    [AffectBudget] BIT           NOT NULL,
    [Behavior]     TINYINT       NULL,
    [Status]       BIT           NOT NULL
);

