CREATE TABLE [InteropCost].[DistributionManpower] (
    [Id]                        INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                      VARCHAR (20)  NOT NULL,
    [EmployeeId]                INT           NOT NULL,
    [Description]               VARCHAR (300) NOT NULL,
    [Year]                      INT           NOT NULL,
    [Month]                     INT           NOT NULL,
    [HoursWorked]               INT           NOT NULL,
    [TotalAccrued]              NUMERIC (18)  CONSTRAINT [DF_DistributionManpower_TotalAccrued] DEFAULT ((0)) NOT NULL,
    [TotalProvision]            NUMERIC (18)  CONSTRAINT [DF_DistributionManpower_TotalProvision] DEFAULT ((0)) NOT NULL,
    [TotalEmployerContribution] NUMERIC (18)  CONSTRAINT [DF_DistributionManpower_TotalEmployerContribution] DEFAULT ((0)) NOT NULL,
    [TotalParafiscal]           NUMERIC (18)  CONSTRAINT [DF_DistributionManpower_TotalParafiscal] DEFAULT ((0)) NOT NULL,
    [Status]                    BIT           NOT NULL,
    [CreationUser]              VARCHAR (20)  NOT NULL,
    [CreationDate]              DATETIME      NOT NULL,
    [ModificationUser]          VARCHAR (20)  NULL,
    [ModificationDate]          DATETIME      NULL,
    [TimeStamp]                 ROWVERSION    NOT NULL,
    CONSTRAINT [PK_DistributionManpower__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DistributionManpower_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id]),
    CONSTRAINT [UQ_DistributionManpower__Code] UNIQUE NONCLUSTERED ([Code] ASC)
);


GO
ALTER TABLE [InteropCost].[DistributionManpower] NOCHECK CONSTRAINT [FK_DistributionManpower_Employee];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del de la entidad 1 - Activo  0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionManpower', @level2type = N'COLUMN', @level2name = N'Status';

