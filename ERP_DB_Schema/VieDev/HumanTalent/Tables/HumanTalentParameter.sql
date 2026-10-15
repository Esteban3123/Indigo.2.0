CREATE TABLE [HumanTalent].[HumanTalentParameter] (
    [Id]                             INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [VacationNumberDaysRequest]      TINYINT NOT NULL,
    [VacationNumberDaysCancellation] TINYINT NOT NULL,
    [TypeCalculationPreviousDays]    TINYINT NOT NULL,
    [MinimunVacationRequestDays]     TINYINT NOT NULL,
    CONSTRAINT [PK_HumanTalentParameter] PRIMARY KEY CLUSTERED ([Id] ASC)
);

