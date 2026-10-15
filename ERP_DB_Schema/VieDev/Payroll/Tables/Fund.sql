CREATE TABLE [Payroll].[Fund] (
    [Id]                      INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                    VARCHAR (20)  NOT NULL,
    [ThirdPartyId]            INT           NOT NULL,
    [Name]                    VARCHAR (100) NOT NULL,
    [Unemployment]            BIT           NOT NULL,
    [Health]                  BIT           NOT NULL,
    [Pension]                 BIT           NOT NULL,
    [Risk]                    BIT           NOT NULL,
    [CompensationFund]        BIT           NOT NULL,
    [Type]                    TINYINT       NOT NULL,
    [MinistryCode]            VARCHAR (6)   NOT NULL,
    [State]                   BIT           NOT NULL,
    [CreationUser]            VARCHAR (20)  NOT NULL,
    [CreationDate]            DATETIME      NOT NULL,
    [ModificationUser]        VARCHAR (20)  NULL,
    [ModificationDate]        DATETIME      NULL,
    [TimeStamp]               ROWVERSION    NOT NULL,
    [Term]                    INT           NULL,
    [MainAccountReceivableId] INT           NULL,
    [PensionACCAI]            BIT           DEFAULT ((0)) NULL,
    CONSTRAINT [PK_Fund__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Fund_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [UQ_Fund__Code] UNIQUE NONCLUSTERED ([Code] ASC)
);


GO
ALTER TABLE [Payroll].[Fund] NOCHECK CONSTRAINT [FK_Fund_ThirdParty];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Pension ACCAI segun  Ley_2381_de_2024', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'PensionACCAI';

