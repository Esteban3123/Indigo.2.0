CREATE TABLE [Contract].[SettingsContract] (
    [Id]                                INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [OperatingUnitId]                   INT          NOT NULL,
    [CUPSWithRelatedDescription]        BIT          CONSTRAINT [DF_SettingsContract_CUPSWithRelatedDescription] DEFAULT ((0)) NOT NULL,
    [CreationUser]                      VARCHAR (20) CONSTRAINT [DF_SettingsContract_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]                      DATETIME     CONSTRAINT [DF_SettingsContract_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]                  VARCHAR (20) NULL,
    [ModificationDate]                  DATETIME     NULL,
    [TimeStamp]                         ROWVERSION   NOT NULL,
    [RequestQuoteOutpatientServices]    BIT          CONSTRAINT [DF_SettingsContract_RequestQuoteOutpatientServices] DEFAULT ((0)) NOT NULL,
    [RequestQuoteIntrahospitalServices] BIT          CONSTRAINT [DF_SettingsContract_RequestQuoteIntrahospitalServices] DEFAULT ((0)) NOT NULL,
    [RequestQuoteOutpatientProducts]    BIT          CONSTRAINT [DF_SettingsContract_RequestQuoteOutpatientProducts] DEFAULT ((0)) NOT NULL,
    [RequestQuoteIntrahospitalProducts] BIT          CONSTRAINT [DF_SettingsContract_RequestQuoteIntrahospitalProducts] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_SettingsContract] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SettingsContract_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id])
);


GO
ALTER TABLE [Contract].[SettingsContract] NOCHECK CONSTRAINT [FK_SettingsContract_OperatingUnit];

