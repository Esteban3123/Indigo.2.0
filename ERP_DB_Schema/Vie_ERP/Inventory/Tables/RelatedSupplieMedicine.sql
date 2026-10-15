CREATE TABLE [Inventory].[RelatedSupplieMedicine] (
    [Id]       INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ATCId]    INT     NOT NULL,
    [ItemType] TINYINT NOT NULL,
    [SourceId] INT     NOT NULL,
    CONSTRAINT [PK_RelatedSupplieMedicine] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RelatedSupplieMedicine_ATC] FOREIGN KEY ([ATCId]) REFERENCES [Inventory].[ATC] ([Id])
);


GO
ALTER TABLE [Inventory].[RelatedSupplieMedicine] NOCHECK CONSTRAINT [FK_RelatedSupplieMedicine_ATC];


GO
CREATE NONCLUSTERED INDEX [IX_RelatedSupplieMedicine_ATCId]
    ON [Inventory].[RelatedSupplieMedicine]([ATCId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de insumo o id de medicamento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RelatedSupplieMedicine', @level2type = N'COLUMN', @level2name = N'SourceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de item: 1|Insumo, 2|Medicamento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RelatedSupplieMedicine', @level2type = N'COLUMN', @level2name = N'ItemType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de medicamento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RelatedSupplieMedicine', @level2type = N'COLUMN', @level2name = N'ATCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RelatedSupplieMedicine', @level2type = N'COLUMN', @level2name = N'Id';

