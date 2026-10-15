CREATE TABLE [Payroll].[RetirementReason] (
    [Id]                TINYINT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]              VARCHAR (20)  NOT NULL,
    [Name]              VARCHAR (100) NULL,
    [Compensation]      BIT           NOT NULL,
    [Severance]         BIT           CONSTRAINT [DF_RetirementReason_Serevance] DEFAULT ((0)) NOT NULL,
    [SeveranceInterest] BIT           CONSTRAINT [DF_RetirementReason_SeveranceInterest] DEFAULT ((0)) NOT NULL,
    [Prenotice]         BIT           CONSTRAINT [DF_RetirementReason_Prenotice] DEFAULT ((0)) NOT NULL,
    [Bonus]             BIT           CONSTRAINT [DF_RetirementReason_Bonus] DEFAULT ((0)) NOT NULL,
    [Vacations]         BIT           CONSTRAINT [DF_RetirementReason_Vacations] DEFAULT ((0)) NOT NULL,
    [State]             BIT           NOT NULL,
    [CreationUser]      VARCHAR (20)  NOT NULL,
    [CreationDate]      DATETIME      NOT NULL,
    [ModificationUser]  VARCHAR (20)  NULL,
    [ModificationDate]  DATETIME      NULL,
    [TimeStamp]         ROWVERSION    NOT NULL,
    CONSTRAINT [PK_RetirementReason__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ_RetirementReason__Code] UNIQUE NONCLUSTERED ([Code] ASC)
);


GO
CREATE NONCLUSTERED INDEX [PK_RetirementReason_CodeState]
    ON [Payroll].[RetirementReason]([Code] ASC, [State] ASC);

