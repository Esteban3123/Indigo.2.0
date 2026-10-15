CREATE TABLE [Budget].[SettingsBudget] (
    [Id]                       INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [OperatingUnitId]          INT          NOT NULL,
    [EnabledInterface]         BIT          CONSTRAINT [DF_SettingBudget_EnabledInterface] DEFAULT ((1)) NOT NULL,
    [CreateReserves]           BIT          CONSTRAINT [DF_SettingBudget_CreateReserves] DEFAULT ((1)) NOT NULL,
    [CreatePayableAccounts]    BIT          CONSTRAINT [DF_SettingBudget_CreatePayableAccounts] DEFAULT ((1)) NOT NULL,
    [CreateReceivableAccounts] BIT          CONSTRAINT [DF_SettingBudget_CreateReceivableAccounts] DEFAULT ((1)) NOT NULL,
    [DocumentsGroupping]       BIT          CONSTRAINT [DF_SettingBudget_DocumentsGroupping] DEFAULT ((1)) NOT NULL,
    [CreationUser]             VARCHAR (20) CONSTRAINT [DF_BudgetParameter_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]             DATETIME     CONSTRAINT [DF_BudgetParameter_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]         VARCHAR (20) NULL,
    [ModificationDate]         DATETIME     NULL,
    [TimeStamp]                ROWVERSION   NOT NULL,
    CONSTRAINT [PK_SettingBudget] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BudgetParameter_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id])
);


GO
ALTER TABLE [Budget].[SettingsBudget] NOCHECK CONSTRAINT [FK_BudgetParameter_OperatingUnit];

