CREATE TABLE [dbo].[Inventory.InventoyRequestATCDetail] (
    [Id]                 INT           IDENTITY (1, 1) NOT NULL,
    [InventoryRequestId] INT           NOT NULL,
    [ATCId]              INT           NOT NULL,
    [Quantity]           INT           NOT NULL,
    [OutstandinQuantity] INT           NOT NULL,
    [Descriptions]       VARCHAR (300) NULL,
    CONSTRAINT [PK_Inventory.InventoyRequestATCDetail] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripción de la solicitud de medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Inventory.InventoyRequestATCDetail', @level2type = N'COLUMN', @level2name = N'Descriptions';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad pendiente por entregar del medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Inventory.InventoyRequestATCDetail', @level2type = N'COLUMN', @level2name = N'OutstandinQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad solicitada del medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Inventory.InventoyRequestATCDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador único de la tabla del medicamento Inventory.ATC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Inventory.InventoyRequestATCDetail', @level2type = N'COLUMN', @level2name = N'ATCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador unico de la cabecera de la solicitud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Inventory.InventoyRequestATCDetail', @level2type = N'COLUMN', @level2name = N'InventoryRequestId';

