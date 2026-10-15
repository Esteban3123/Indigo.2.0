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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Almacen Maquila', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'WarehouseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Maneja Almacen Maquila', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'ManagesMaquila';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del registro 1 - Activo 0- Inactivo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Aprobado por', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'ApprovedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de negociación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'NegotiationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del acta', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'ActDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de acta', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'ActNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha cotización', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'QuoteDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de cotización', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'QuoteNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Anexos', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'Annexes';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clausulas', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'Clauses';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la plantilla de productos', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'ProductRateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Modalidad de venta:  1. Crédito  2. Contado', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'SaleMode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del cliente', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'CustomerId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número o código del contrato', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'ContractNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Objeto del contrato', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'ContractObject';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha final del contrato', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'EndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha inicial del contrato', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'InitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha documento', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de contrato:  1. Fijo  2. Variable', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'ContractType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del contrato', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'Id';

