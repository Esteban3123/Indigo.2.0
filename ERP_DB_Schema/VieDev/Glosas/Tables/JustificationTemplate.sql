CREATE TABLE [Glosas].[JustificationTemplate] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (10)  NOT NULL,
    [Name]             VARCHAR (100) NOT NULL,
    [IdConcept]        INT           NOT NULL,
    [IsDefault]        BIT           NOT NULL,
    [Justification]    VARCHAR (MAX) NOT NULL,
    [CreationUser]     VARCHAR (20)  CONSTRAINT [DF_JustificationTemplate_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]     DATETIME      CONSTRAINT [DF_JustificationTemplate_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    [TimeStamp]        ROWVERSION    NOT NULL,
    CONSTRAINT [PK_JustificationTemplate__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_JustificationTemplate_SpecificConcept] FOREIGN KEY ([IdConcept]) REFERENCES [Common].[ConceptGlosas] ([Id])
);


GO
ALTER TABLE [Glosas].[JustificationTemplate] NOCHECK CONSTRAINT [FK_JustificationTemplate_SpecificConcept];

