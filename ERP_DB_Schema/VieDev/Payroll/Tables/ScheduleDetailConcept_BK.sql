CREATE TABLE [Payroll].[ScheduleDetailConcept_BK] (
    [Id]                   INT     IDENTITY (1, 1) NOT NULL,
    [ScheduleDetailHourId] INT     NULL,
    [ConceptType]          TINYINT NOT NULL,
    [ConceptId]            INT     NOT NULL
);

