CREATE TABLE [Common].[PersonProfession] (
    [Id]            INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdProfession]  TINYINT NOT NULL,
    [PersonId]      INT     NOT NULL,
    [IsSupported]   BIT     NOT NULL,
    [PersonStudyId] INT     NULL,
    [State]         BIT     NOT NULL,
    CONSTRAINT [PK_ProfessionDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PersonProfession_PersonStudy] FOREIGN KEY ([PersonStudyId]) REFERENCES [Common].[PersonStudy] ([Id]),
    CONSTRAINT [FK_ProfessionDetail_Employee] FOREIGN KEY ([PersonId]) REFERENCES [Common].[Person] ([Id]),
    CONSTRAINT [FK_ProfessionDetail_Profession] FOREIGN KEY ([IdProfession]) REFERENCES [Payroll].[Profession] ([Id])
);


GO
ALTER TABLE [Common].[PersonProfession] NOCHECK CONSTRAINT [FK_PersonProfession_PersonStudy];


GO
ALTER TABLE [Common].[PersonProfession] NOCHECK CONSTRAINT [FK_ProfessionDetail_Employee];


GO
ALTER TABLE [Common].[PersonProfession] NOCHECK CONSTRAINT [FK_ProfessionDetail_Profession];

