CREATE TABLE [MixingStation].[ContractExternalClients] (
    [Id]                   INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                 VARCHAR (20)  NOT NULL,
    [ContractType]         TINYINT       NOT NULL,
    [DocumentDate]         DATETIME      NOT NULL,
    [InitialDate]          DATETIME      NOT NULL,
    [EndDate]              DATETIME      NOT NULL,
    [ContractObject]       VARCHAR (MAX) NULL,
    [ContractNumber]       VARCHAR (20)  NOT NULL,
    [CustomerId]           INT           NOT NULL,
    [SaleMode]             TINYINT       NOT NULL,
    [TechnicalSupervision] BIT           CONSTRAINT [DF__ContractE__Techn__32C6252F] DEFAULT ((0)) NOT NULL,
    [ProductRateId]        INT           NOT NULL,
    [Clauses]              VARCHAR (MAX) NULL,
    [Annexes]              VARCHAR (MAX) NULL,
    [QuoteNumber]          VARCHAR (20)  NULL,
    [QuoteDate]            DATETIME      NULL,
    [ActNumber]            VARCHAR (20)  NULL,
    [ActDate]              DATETIME      NULL,
    [NegotiationType]      VARCHAR (200) NULL,
    [ApprovedBy]           VARCHAR (50)  NULL,
    [Status]               BIT           NOT NULL,
    [CreationUser]         VARCHAR (20)  NOT NULL,
    [CreationDate]         DATETIME      NOT NULL,
    [ModificationUser]     VARCHAR (20)  NULL,
    [ModificationDate]     DATETIME      NULL,
    [TimeStamp]            ROWVERSION    NOT NULL,
    [ManagesMaquila]       BIT           NULL,
    [WarehouseId]          INT           NULL,
    CONSTRAINT [PK_ContractExternalClients] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ContractExternalClients_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [Common].[Customer] ([Id]),
    CONSTRAINT [FK_ContractExternalClients_ProductRate] FOREIGN KEY ([ProductRateId]) REFERENCES [Inventory].[ProductRate] ([Id]),
    CONSTRAINT [FK_ContractExternalClients_Warehouse] FOREIGN KEY ([WarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id])
);


GO
ALTER TABLE [MixingStation].[ContractExternalClients] NOCHECK CONSTRAINT [FK_ContractExternalClients_Customer];


GO
ALTER TABLE [MixingStation].[ContractExternalClients] NOCHECK CONSTRAINT [FK_ContractExternalClients_ProductRate];


GO
ALTER TABLE [MixingStation].[ContractExternalClients] NOCHECK CONSTRAINT [FK_ContractExternalClients_Warehouse];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Modalidad de venta:  1. Crédito  2. Contado', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'SaleMode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de contrato:  1. Fijo  2. Variable', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'ContractType';

