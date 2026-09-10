CREATE TABLE [InteropCost].[DistributionSecondaryBaseDetail] (
    [Id]                          INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DistributionSecondaryBaseId] INT             NOT NULL,
    [ProductionCenterId]          INT             NOT NULL,
    [Quantity]                    NUMERIC (18, 2) NOT NULL,
    CONSTRAINT [PK_DistributionSecondaryBaseDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DistributionSecondaryBaseDetail_DistributionSecondaryBase] FOREIGN KEY ([DistributionSecondaryBaseId]) REFERENCES [InteropCost].[DistributionSecondaryBase] ([Id]),
    CONSTRAINT [FK_DistributionSecondaryBaseDetail_ProductionCenter] FOREIGN KEY ([ProductionCenterId]) REFERENCES [InteropCost].[ProductionCenter] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad numérica (NUMERIC 18,2) a distribuir en el centro de producción. Se completa únicamente cuando el tipo de distribución es calculada o automática. Representa volumen, unidades o porcentaje según la base de distribución secundaria.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryBaseDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la cantidad que se va a distribuir, Solo se llena este campo si el tipo de distribucion es calculada', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryBaseDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryBaseDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) del Centro de Producción destino. Clave foránea a [InteropCost].[ProductionCenter]. Referencia la unidad funcional, área clínica o centro de costo donde se asignan los recursos.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryBaseDetail', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del centro de produccion', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryBaseDetail', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryBaseDetail', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de la Base de Distribución Secundaria padre. Clave foránea a [InteropCost].[DistributionSecondaryBase]. Agrupa detalles de distribución de costos a múltiples centros.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryBaseDetail', @level2type = N'COLUMN', @level2name = N'DistributionSecondaryBaseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la base de distribucion secundaria', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryBaseDetail', @level2type = N'COLUMN', @level2name = N'DistributionSecondaryBaseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryBaseDetail', @level2type = N'COLUMN', @level2name = N'DistributionSecondaryBaseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY 1,1) del registro de detalle de distribución. Clave primaria de [DistributionSecondaryBaseDetail]. Autonumérico.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryBaseDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryBaseDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryBaseDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle del reparto o distribución de bases secundarias de costos, registrando la cantidad asignada a cada centro de producción dentro de un proceso de distribución secundaria en el modelo de costeo.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryBaseDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryBaseDetail';
