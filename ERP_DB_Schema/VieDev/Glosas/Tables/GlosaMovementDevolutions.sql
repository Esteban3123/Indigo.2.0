CREATE TABLE [Glosas].[GlosaMovementDevolutions] (
    [Id]                      INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdDevolutionsReceptionD] INT           NOT NULL,
    [InvoiceNumber]           VARCHAR (50)  NOT NULL,
    [DevolutionValue]         MONEY         NOT NULL,
    [TypeDevolution]          CHAR (1)      NOT NULL,
    [IdConceptGlosa]          INT           NOT NULL,
    [Comment]                 VARCHAR (MAX) NULL,
    [Answer]                  VARCHAR (MAX) NULL,
    [State]                   CHAR (1)      NOT NULL,
    [CreationUser]            VARCHAR (20)  NULL,
    [CreationDate]            DATETIME      NULL,
    [ModificationUser]        VARCHAR (20)  NULL,
    [ModificationDate]        DATETIME      NULL,
    [TimeStamp]               ROWVERSION    NOT NULL,
    CONSTRAINT [PK_GlosaMovementDevolutions__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GlosaMovementDevolutions_ConceptGlosas] FOREIGN KEY ([IdConceptGlosa]) REFERENCES [Common].[ConceptGlosas] ([Id]),
    CONSTRAINT [FK_GlosaMovementDevolutions_GlosaDevolutionsReceptionD] FOREIGN KEY ([IdDevolutionsReceptionD]) REFERENCES [Glosas].[GlosaDevolutionsReceptionD] ([Id])
);


GO
ALTER TABLE [Glosas].[GlosaMovementDevolutions] NOCHECK CONSTRAINT [FK_GlosaMovementDevolutions_ConceptGlosas];


GO
ALTER TABLE [Glosas].[GlosaMovementDevolutions] NOCHECK CONSTRAINT [FK_GlosaMovementDevolutions_GlosaDevolutionsReceptionD];

