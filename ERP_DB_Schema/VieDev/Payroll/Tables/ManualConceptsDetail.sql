CREATE TABLE [Payroll].[ManualConceptsDetail] (
    [Id]                    INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ManualConceptId]       INT             NOT NULL,
    [PayrollDateLiquidated] DATE            NOT NULL,
    [Value]                 DECIMAL (18, 2) NULL,
    [State]                 TINYINT         NOT NULL,
    CONSTRAINT [PK_ManualConceptsDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ManualConceptsDetail_ManualConcepts] FOREIGN KEY ([ManualConceptId]) REFERENCES [Payroll].[ManualConcepts] ([Id])
);


GO
ALTER TABLE [Payroll].[ManualConceptsDetail] NOCHECK CONSTRAINT [FK_ManualConceptsDetail_ManualConcepts];

