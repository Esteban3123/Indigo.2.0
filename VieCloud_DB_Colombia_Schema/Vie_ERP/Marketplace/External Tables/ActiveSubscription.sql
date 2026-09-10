CREATE EXTERNAL TABLE [Marketplace].[ActiveSubscription] (
    [Id] INT NOT NULL,
    [OfferID] VARCHAR (75) NOT NULL,
    [AMPSubscriptionId] VARCHAR (75) NOT NULL,
    [SubscriptionStatus] VARCHAR (40) NOT NULL,
    [IsActive] BIT NOT NULL,
    [CreateDate] DATETIME NOT NULL,
    [PurchaserEmail] VARCHAR (70) NOT NULL,
    [PurchaserTenantId] VARCHAR (75) NOT NULL,
    [PlanId] VARCHAR (75) NOT NULL,
    [Container] VARCHAR (10) NOT NULL,
    [IsMetered] BIT NOT NULL,
    [LastDimension] INT NULL,
    [LastNotifyDate] DATETIME NULL,
    [IsRenewable] BIT NULL,
    [InitialDate] DATETIME NULL,
    [FinalDate] DATETIME NULL
)
    WITH (
    DATA_SOURCE = [INDIGOSEC],
    SCHEMA_NAME = N'Marketplace',
    OBJECT_NAME = N'ActiveSubscription'
    );

