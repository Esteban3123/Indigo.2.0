CREATE TABLE [Payments].[AgesPayments] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [SettingPaymentId] INT           CONSTRAINT [DF_AgesPayments_SettingPaymentId] DEFAULT ((1010)) NOT NULL,
    [Name]             VARCHAR (100) NOT NULL,
    [InitialRange]     INT           NOT NULL,
    [EndRange]         INT           NOT NULL,
    [Color]            INT           NOT NULL,
    [CreationUser]     VARCHAR (20)  NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    CONSTRAINT [PK_AgesPayments] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AgesPayments_SettingPayments] FOREIGN KEY ([SettingPaymentId]) REFERENCES [Payments].[SettingPayments] ([Id])
);


GO
ALTER TABLE [Payments].[AgesPayments] NOCHECK CONSTRAINT [FK_AgesPayments_SettingPayments];

