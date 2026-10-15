CREATE TABLE [MixingStation].[ExternalCareCenter] (
    [Id]                        INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                      VARCHAR (20)  NOT NULL,
    [Description]               VARCHAR (100) NOT NULL,
    [CustomerId]                INT           NOT NULL,
    [ContractExternalClientsId] INT           NOT NULL,
    [InterfaceType]             TINYINT       NOT NULL,
    [Status]                    BIT           NOT NULL,
    [CreationUser]              VARCHAR (20)  NOT NULL,
    [CreationDate]              DATETIME      NOT NULL,
    [ModificationUser]          VARCHAR (20)  NULL,
    [ModificationDate]          DATETIME      NULL,
    [TimeStamp]                 ROWVERSION    NOT NULL,
    CONSTRAINT [PK_ExternalCareCenter] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ExternalCareCenter_ContractExternalClients] FOREIGN KEY ([ContractExternalClientsId]) REFERENCES [MixingStation].[ContractExternalClients] ([Id]),
    CONSTRAINT [FK_ExternalCareCenter_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [Common].[Customer] ([Id])
);


GO
ALTER TABLE [MixingStation].[ExternalCareCenter] NOCHECK CONSTRAINT [FK_ExternalCareCenter_ContractExternalClients];


GO
ALTER TABLE [MixingStation].[ExternalCareCenter] NOCHECK CONSTRAINT [FK_ExternalCareCenter_Customer];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de interfaz:  1. His Indigo Externo  2. His Externo  3. Sin Interfaz', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalCareCenter', @level2type = N'COLUMN', @level2name = N'InterfaceType';

