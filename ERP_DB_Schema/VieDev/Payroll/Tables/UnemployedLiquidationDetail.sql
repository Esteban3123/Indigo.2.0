CREATE TABLE [Payroll].[UnemployedLiquidationDetail] (
    [Id]                        INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [UnemployedLiquidationId]   INT             NOT NULL,
    [Year]                      INT             NOT NULL,
    [Month]                     TINYINT         NOT NULL,
    [Salary]                    NUMERIC (18, 2) NOT NULL,
    [WorkedDays]                TINYINT         NOT NULL,
    [ValIBC]                    NUMERIC (18, 2) NOT NULL,
    [UnemployedAverage]         NUMERIC (18, 2) NOT NULL,
    [UnemployedInterestAverage] NUMERIC (18, 2) NOT NULL,
    [ValIBCNotSanction]         NUMERIC (18, 2) NOT NULL,
    [SanctionsDays]             INT             NOT NULL,
    CONSTRAINT [PK_UnemployedLiquidationDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UnemployedLiquidationDetail_UnemployedLiquidation] FOREIGN KEY ([UnemployedLiquidationId]) REFERENCES [Payroll].[UnemployedLiquidation] ([Id])
);


GO
ALTER TABLE [Payroll].[UnemployedLiquidationDetail] NOCHECK CONSTRAINT [FK_UnemployedLiquidationDetail_UnemployedLiquidation];

