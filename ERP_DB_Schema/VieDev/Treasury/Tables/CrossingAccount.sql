CREATE TABLE [Treasury].[CrossingAccount] (
    [Id]               INT           IDENTITY (1, 1) NOT NULL,
    [Code]             VARCHAR (20)  NOT NULL,
    [Description]      VARCHAR (500) NOT NULL,
    [ThirdPartyId]     INT           NOT NULL,
    [DocumentDate]     DATETIME      NOT NULL,
    [OperatingUnitId]  INT           NOT NULL,
    [CrossingType]     TINYINT       NOT NULL,
    [Status]           TINYINT       NOT NULL,
    [CreationUser]     VARCHAR (20)  NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    [ConfirmationUser] VARCHAR (20)  NULL,
    [ConfirmationDate] DATETIME      NULL,
    [AnnulmentUser]    VARCHAR (20)  NULL,
    [AnnulmentDate]    DATETIME      NULL,
    [TimeStamp]        ROWVERSION    NOT NULL,
    [ReversedUser]     VARCHAR (20)  NULL,
    [ReversedDate]     DATETIME      NULL,
    [CurrencyId]       INT           NOT NULL,
    CONSTRAINT [PK_CrossingAccount] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CrossingAccount_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_CrossingAccount_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_CrossingAccount_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Treasury].[CrossingAccount] NOCHECK CONSTRAINT [FK_CrossingAccount_Currency];


GO
ALTER TABLE [Treasury].[CrossingAccount] NOCHECK CONSTRAINT [FK_CrossingAccount_OperatingUnit];


GO
ALTER TABLE [Treasury].[CrossingAccount] NOCHECK CONSTRAINT [FK_CrossingAccount_ThirdParty];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la moneda.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccount', @level2type = N'COLUMN', @level2name = N'CurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de cruce del documento  1 - Del Mismo Tercero  2 - Diferente Tercero', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccount', @level2type = N'COLUMN', @level2name = N'CrossingType';

