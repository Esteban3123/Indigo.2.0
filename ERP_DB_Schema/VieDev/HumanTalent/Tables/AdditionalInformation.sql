CREATE TABLE [HumanTalent].[AdditionalInformation] (
    [Id]                     INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CandidateId]            INT          NULL,
    [EmployeeId]             INT          NULL,
    [AvailabilityTravelId]   INT          NULL,
    [AvailabilityTransferId] INT          NULL,
    [DriverLicense]          VARCHAR (50) NULL,
    [HowVacany]              VARCHAR (50) NULL,
    [DataAuthorizationId]    INT          NOT NULL,
    [CreationUser]           VARCHAR (20) NOT NULL,
    [CreationDate]           DATETIME     NOT NULL,
    [ModificationUser]       VARCHAR (20) NULL,
    [ModificationDate]       DATETIME     NULL,
    CONSTRAINT [PK__Addition__3214EC0772362190] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [fk_CandidateIdAI] FOREIGN KEY ([CandidateId]) REFERENCES [HumanTalent].[Candidate] ([Id])
);


GO
ALTER TABLE [HumanTalent].[AdditionalInformation] NOCHECK CONSTRAINT [fk_CandidateIdAI];

