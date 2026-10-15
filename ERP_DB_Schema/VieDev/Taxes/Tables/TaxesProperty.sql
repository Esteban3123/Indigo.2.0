CREATE TABLE [Taxes].[TaxesProperty] (
    [Id]               INT              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CodeDeparment]    VARCHAR (2)      NOT NULL,
    [CodeCity]         VARCHAR (3)      NOT NULL,
    [Code]             VARCHAR (50)     NOT NULL,
    [Type]             TINYINT          CONSTRAINT [DF_TaxesProperty_Type] DEFAULT ((0)) NOT NULL,
    [Taxed]            BIT              NOT NULL,
    [ThirdPartyId]     INT              NOT NULL,
    [Addres]           VARCHAR (200)    NOT NULL,
    [Commune]          VARCHAR (5)      NOT NULL,
    [EconomicDestiny]  VARCHAR (5)      NOT NULL,
    [LandArea]         DECIMAL (18)     NOT NULL,
    [BuiltArea]        DECIMAL (18)     NOT NULL,
    [Appraisal]        DECIMAL (18)     NOT NULL,
    [Latitude]         DECIMAL (20, 14) CONSTRAINT [DF_TaxesProperty_Latitude] DEFAULT ((0)) NOT NULL,
    [Longitude]        DECIMAL (20, 14) CONSTRAINT [DF_TaxesProperty_Longitude] DEFAULT ((0)) NOT NULL,
    [Status]           TINYINT          NOT NULL,
    [CreationUser]     VARCHAR (20)     NOT NULL,
    [CreationDate]     DATETIME         NOT NULL,
    [ModificationUser] VARCHAR (20)     NULL,
    [ModificationDate] DATETIME         NULL,
    [TimeStamp]        ROWVERSION       NOT NULL,
    CONSTRAINT [PK_TaxesProperty__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TaxesProperty_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Taxes].[TaxesProperty] NOCHECK CONSTRAINT [FK_TaxesProperty_ThirdParty];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el estado del activo  1 - Activo  0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo del predio  1 - Urbano  2 - Rural', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'Type';

