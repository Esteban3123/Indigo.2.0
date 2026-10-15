CREATE TABLE [Payroll].[RetroactiveC] (
    [Id]                     INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Status]                 TINYINT         NOT NULL,
    [PaymentType]            CHAR (1)        NOT NULL,
    [NextPayrollDate]        DATE            NULL,
    [InitialDateRetroactive] DATE            NOT NULL,
    [UsedPercentage]         DECIMAL (5, 2)  NOT NULL,
    [IdGroup]                INT             NOT NULL,
    [IdEmployee]             INT             NOT NULL,
    [IdContract]             INT             NOT NULL,
    [InitialContractNumber]  INT             NOT NULL,
    [ExecuteProcessDate]     DATE            NOT NULL,
    [TotalRetroactiveValue]  NUMERIC (18, 2) NOT NULL,
    [CreationUserId]         INT             NOT NULL,
    [CreationDate]           DATETIME        NOT NULL,
    [ConfirmationUserId]     INT             NULL,
    [ConfirmationDate]       DATETIME        NULL,
    CONSTRAINT [PK_RetroactiveC] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RetroactiveC_Contract] FOREIGN KEY ([IdContract]) REFERENCES [Payroll].[Contract] ([Id]),
    CONSTRAINT [FK_RetroactiveC_Employee] FOREIGN KEY ([IdEmployee]) REFERENCES [Payroll].[Employee] ([Id]),
    CONSTRAINT [FK_RetroactiveC_Group] FOREIGN KEY ([IdGroup]) REFERENCES [Payroll].[Group] ([Id])
);


GO
ALTER TABLE [Payroll].[RetroactiveC] NOCHECK CONSTRAINT [FK_RetroactiveC_Contract];


GO
ALTER TABLE [Payroll].[RetroactiveC] NOCHECK CONSTRAINT [FK_RetroactiveC_Employee];


GO
ALTER TABLE [Payroll].[RetroactiveC] NOCHECK CONSTRAINT [FK_RetroactiveC_Group];

