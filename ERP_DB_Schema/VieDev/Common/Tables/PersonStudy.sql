CREATE TABLE [Common].[PersonStudy] (
    [Id]                             INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PersonId]                       INT            NOT NULL,
    [StudyTypeId]                    INT            NOT NULL,
    [IsFormal]                       BIT            NOT NULL,
    [Name]                           VARCHAR (100)  NOT NULL,
    [StudyCenterId]                  INT            NOT NULL,
    [ProfessionalCardNumber]         VARCHAR (20)   NULL,
    [ProfessionalCardExpeditionDate] DATE           NULL,
    [ProfessionalCardOnProcess]      BIT            NULL,
    [StartDate]                      DATE           NOT NULL,
    [EndingDate]                     DATE           NULL,
    [GraduationDate]                 DATE           NULL,
    [Status]                         TINYINT        NULL,
    [IsInternal]                     BIT            NULL,
    [AverageGrade]                   DECIMAL (2, 1) NULL,
    [StudyValue]                     INT            NULL,
    [StudyValueCompanyPercentage]    INT            NULL,
    [Length]                         INT            NOT NULL,
    [TimeUnitId]                     INT            NOT NULL,
    [CityId]                         INT            NOT NULL,
    [State]                          BIT            NOT NULL,
    CONSTRAINT [PK_EmployeeStudy] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EmployeeStudy_Person] FOREIGN KEY ([PersonId]) REFERENCES [Common].[Person] ([Id]),
    CONSTRAINT [FK_EmployeeStudy_StudyCenter] FOREIGN KEY ([StudyCenterId]) REFERENCES [Payroll].[StudyCenter] ([Id]),
    CONSTRAINT [FK_EmployeeStudy_StudyType] FOREIGN KEY ([StudyTypeId]) REFERENCES [Payroll].[StudyType] ([Id]),
    CONSTRAINT [FK_PersonStudy_City] FOREIGN KEY ([CityId]) REFERENCES [Common].[City] ([Id]),
    CONSTRAINT [FK_PersonStudy_TimeUnit] FOREIGN KEY ([TimeUnitId]) REFERENCES [Common].[TimeUnit] ([Id])
);


GO
ALTER TABLE [Common].[PersonStudy] NOCHECK CONSTRAINT [FK_EmployeeStudy_Person];


GO
ALTER TABLE [Common].[PersonStudy] NOCHECK CONSTRAINT [FK_EmployeeStudy_StudyCenter];


GO
ALTER TABLE [Common].[PersonStudy] NOCHECK CONSTRAINT [FK_EmployeeStudy_StudyType];


GO
ALTER TABLE [Common].[PersonStudy] NOCHECK CONSTRAINT [FK_PersonStudy_City];


GO
ALTER TABLE [Common].[PersonStudy] NOCHECK CONSTRAINT [FK_PersonStudy_TimeUnit];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del estudio   1 - Estudia actualmente   2 - Estudio Interrumpido  3 - Estudio Terminado  4 - Graduado', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'Status';

