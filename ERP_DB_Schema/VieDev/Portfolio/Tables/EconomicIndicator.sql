CREATE TABLE [Portfolio].[EconomicIndicator] (
    [Id]                  INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                VARCHAR (20)   NOT NULL,
    [Year]                VARCHAR (4)    NOT NULL,
    [Month]               VARCHAR (2)    NOT NULL,
    [FinancialInterests]  DECIMAL (5, 2) NOT NULL,
    [LatePaymentInterest] DECIMAL (5, 2) NOT NULL,
    [Status]              BIT            CONSTRAINT [DF_EconomicIndicator_Status] DEFAULT ((1)) NOT NULL,
    [CreationUser]        VARCHAR (20)   NOT NULL,
    [CreationDate]        DATETIME       NOT NULL,
    [ModificationUser]    VARCHAR (20)   NULL,
    [ModificationDate]    DATETIME       NULL,
    [TimeStamp]           ROWVERSION     NOT NULL,
    CONSTRAINT [PK_EconomicIndicator__Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador del indicador económico.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'EconomicIndicator', @level2type = N'COLUMN', @level2name = N'Code';

