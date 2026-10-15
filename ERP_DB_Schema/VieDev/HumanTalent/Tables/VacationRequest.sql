CREATE TABLE [HumanTalent].[VacationRequest] (
    [Id]                    INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [EmployeeId]            INT          NOT NULL,
    [InitialContractNumber] INT          NOT NULL,
    [IdContract]            INT          NOT NULL,
    [TypeVacation]          INT          NOT NULL,
    [InitialDateVacation]   DATE         NULL,
    [EndDateVacation]       DATE         NULL,
    [IncorporationDate]     DATE         NULL,
    [DaysRequest]           TINYINT      NOT NULL,
    [EnjoyDays]             TINYINT      NOT NULL,
    [LastStatus]            TINYINT      NOT NULL,
    [CreationDate]          DATETIME     NOT NULL,
    [CreationUser]          VARCHAR (20) NOT NULL,
    [ModificationUser]      VARCHAR (20) NULL,
    [ModificationDate]      DATETIME     NULL,
    [TimeStamp]             ROWVERSION   NOT NULL,
    CONSTRAINT [PK_VacationRequest_1] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_VacationRequest_Contract] FOREIGN KEY ([IdContract]) REFERENCES [Payroll].[Contract] ([Id]),
    CONSTRAINT [FK_VacationRequest_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id])
);


GO
ALTER TABLE [HumanTalent].[VacationRequest] NOCHECK CONSTRAINT [FK_VacationRequest_Contract];


GO
ALTER TABLE [HumanTalent].[VacationRequest] NOCHECK CONSTRAINT [FK_VacationRequest_Employee];

