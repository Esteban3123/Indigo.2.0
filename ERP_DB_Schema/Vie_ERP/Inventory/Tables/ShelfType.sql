CREATE TABLE [Inventory].[ShelfType] (
    [Id]                 INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]               VARCHAR (20)  NOT NULL,
    [Description]        VARCHAR (MAX) NULL,
    [Large]              DECIMAL (6)   CONSTRAINT [DF_ShelfType_Large] DEFAULT ((0)) NULL,
    [Wide]               DECIMAL (6)   CONSTRAINT [DF_ShelfType_Wide] DEFAULT ((0)) NULL,
    [Deep]               DECIMAL (6)   CONSTRAINT [DF_ShelfType_Deep] DEFAULT ((0)) NULL,
    [PartitionXDeep]     DECIMAL (6)   CONSTRAINT [DF_ShelfType_PartitionXDeep] DEFAULT ((0)) NULL,
    [Partitions]         DECIMAL (6)   CONSTRAINT [DF_ShelfType_Partitions] DEFAULT ((0)) NULL,
    [LocationXPartition] DECIMAL (6)   CONSTRAINT [DF_ShelfType_LocationXPartition] DEFAULT ((0)) NULL,
    [State]              BIT           NOT NULL,
    [CreationUser]       VARCHAR (20)  CONSTRAINT [DF_ShelfType_CreationUser] DEFAULT ('999') NOT NULL,
    [CreationDate]       DATETIME      CONSTRAINT [DF_ShelfType_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]   VARCHAR (20)  NULL,
    [ModificationDate]   DATETIME      NULL,
    [TimeStamp]          ROWVERSION    NOT NULL,
    CONSTRAINT [PK__ShelfType] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ShelfType', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ShelfType', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de modificacion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ShelfType', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fecha de creacion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ShelfType', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de creacion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ShelfType', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ShelfType', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Particiones x Locación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ShelfType', @level2type = N'COLUMN', @level2name = N'LocationXPartition';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Particiones', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ShelfType', @level2type = N'COLUMN', @level2name = N'Partitions';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Particiones x produndida', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ShelfType', @level2type = N'COLUMN', @level2name = N'PartitionXDeep';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Profundo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ShelfType', @level2type = N'COLUMN', @level2name = N'Deep';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ancho', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ShelfType', @level2type = N'COLUMN', @level2name = N'Wide';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Largo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ShelfType', @level2type = N'COLUMN', @level2name = N'Large';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripción', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ShelfType', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ShelfType', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ShelfType', @level2type = N'COLUMN', @level2name = N'Id';

