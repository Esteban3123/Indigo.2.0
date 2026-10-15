CREATE TABLE [EHR].[SchemesSuppliesDays] (
    [Id]               INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [SchemesId]        INT       NOT NULL,
    [SuppliesDrugCode] CHAR (20) NOT NULL,
    [Day]              INT       NOT NULL,
    [Quantity]         INT       NOT NULL,
    CONSTRAINT [PK_DaysAdministration] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SchemesSuppliesDays_IHLISTPRO] FOREIGN KEY ([SuppliesDrugCode]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC]),
    CONSTRAINT [FK_SchemesSuppliesDays_Schemes] FOREIGN KEY ([SchemesId]) REFERENCES [EHR].[Schemes] ([Id])
);


GO
ALTER TABLE [EHR].[SchemesSuppliesDays] NOCHECK CONSTRAINT [FK_SchemesSuppliesDays_IHLISTPRO];


GO
ALTER TABLE [EHR].[SchemesSuppliesDays] NOCHECK CONSTRAINT [FK_SchemesSuppliesDays_Schemes];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad de Insumo', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesSuppliesDays', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dia en que se requiere el insumo (1-8-15-22)', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesSuppliesDays', @level2type = N'COLUMN', @level2name = N'Day';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del insumo - relacion con (IHLISTPRO)', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesSuppliesDays', @level2type = N'COLUMN', @level2name = N'SuppliesDrugCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del esquema (ERH.Schemes)', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesSuppliesDays', @level2type = N'COLUMN', @level2name = N'SchemesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesSuppliesDays', @level2type = N'COLUMN', @level2name = N'Id';

