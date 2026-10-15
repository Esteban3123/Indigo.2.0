CREATE TABLE [Common].[PersonLanguage] (
    [Id]            INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PersonId]      INT     NOT NULL,
    [LanguageId]    INT     NULL,
    [LanguageLevel] TINYINT NOT NULL,
    CONSTRAINT [PK_EmployeeLanguage] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK__PersonLan__Language] FOREIGN KEY ([LanguageId]) REFERENCES [Payroll].[Language] ([Id]),
    CONSTRAINT [FK_EmployeeLanguage_Person] FOREIGN KEY ([PersonId]) REFERENCES [Common].[Person] ([Id])
);


GO
ALTER TABLE [Common].[PersonLanguage] NOCHECK CONSTRAINT [FK__PersonLan__Language];


GO
ALTER TABLE [Common].[PersonLanguage] NOCHECK CONSTRAINT [FK_EmployeeLanguage_Person];

