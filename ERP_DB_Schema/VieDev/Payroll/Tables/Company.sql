CREATE TABLE [Payroll].[Company] (
    [Id]                  INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Nit]                 VARCHAR (50) NULL,
    [ThirdPartyId]        INT          NULL,
    [Name]                VARCHAR (50) NOT NULL,
    [LegalRepresentative] VARCHAR (50) NOT NULL,
    [CityId]              INT          NOT NULL,
    [PayrollType]         BIT          NULL,
    [AgreementType]       BIT          NULL,
    [State]               BIT          NOT NULL,
    [CreationUser]        VARCHAR (20) NOT NULL,
    [CreationDate]        DATETIME     NOT NULL,
    [ModificationUser]    VARCHAR (20) NULL,
    [ModificationDate]    DATETIME     NULL,
    [TimeStamp]           ROWVERSION   NOT NULL,
    [ARLFundId]           INT          NULL,
    [ForeclousureType]    BIT          NULL,
    [CodeJudgmentAccount] VARCHAR (50) NULL,
    CONSTRAINT [PK_Company__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Company_City] FOREIGN KEY ([CityId]) REFERENCES [Common].[City] ([Id]),
    CONSTRAINT [FK_Company_Fund] FOREIGN KEY ([ARLFundId]) REFERENCES [Payroll].[Fund] ([Id]),
    CONSTRAINT [FK_Company_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Payroll].[Company] NOCHECK CONSTRAINT [FK_Company_City];


GO
ALTER TABLE [Payroll].[Company] NOCHECK CONSTRAINT [FK_Company_Fund];


GO
ALTER TABLE [Payroll].[Company] NOCHECK CONSTRAINT [FK_Company_ThirdParty];

