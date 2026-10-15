CREATE TABLE [Inventory].[UpdateExpirationDate] (
    [Id]                INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [BatchSerialId]     INT           NOT NULL,
    [OldExpirationDate] DATE          NOT NULL,
    [NewExpirationDate] DATE          NOT NULL,
    [Observation]       VARCHAR (MAX) NOT NULL,
    [CreationUser]      VARCHAR (20)  CONSTRAINT [DF_UpdateExpirationDate_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]      DATETIME      CONSTRAINT [DF_UpdateExpirationDate_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    CONSTRAINT [PK_UpdateExpirationDate] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UpdateExpirationDate_BatchSerial] FOREIGN KEY ([BatchSerialId]) REFERENCES [Inventory].[BatchSerial] ([Id])
);


GO
ALTER TABLE [Inventory].[UpdateExpirationDate] NOCHECK CONSTRAINT [FK_UpdateExpirationDate_BatchSerial];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'UpdateExpirationDate', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'UpdateExpirationDate', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observacion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'UpdateExpirationDate', @level2type = N'COLUMN', @level2name = N'Observation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la nueva fecha de expiracion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'UpdateExpirationDate', @level2type = N'COLUMN', @level2name = N'NewExpirationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la antigua fecha de expiracion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'UpdateExpirationDate', @level2type = N'COLUMN', @level2name = N'OldExpirationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del lote o serial', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'UpdateExpirationDate', @level2type = N'COLUMN', @level2name = N'BatchSerialId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'UpdateExpirationDate', @level2type = N'COLUMN', @level2name = N'Id';

