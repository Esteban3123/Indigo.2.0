CREATE TABLE [InteropCost].[ProductionCenterServiceArea] (
    [Id]                 INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ProductionCenterId] INT          NOT NULL,
    [ServiceAreaId]      INT          NOT NULL,
    [ServiceAreaCode]    VARCHAR (20) NOT NULL,
    CONSTRAINT [PK_ProductionCenterServiceArea] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProductionCenterServiceArea_ProductionCenter] FOREIGN KEY ([ProductionCenterId]) REFERENCES [InteropCost].[ProductionCenter] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del área de servicio (unidad funcional, departamento, centro de atención) utilizado en interfaz/integración con ERP. Varchar(20), identificador único del área de servicios.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenterServiceArea', @level2type = N'COLUMN', @level2name = N'ServiceAreaCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del area de servicio con el que se haga interfaz', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenterServiceArea', @level2type = N'COLUMN', @level2name = N'ServiceAreaCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenterServiceArea', @level2type = N'COLUMN', @level2name = N'ServiceAreaCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico (INT) del área de servicio en el ERP con el que se establece interfaz/integración. Referencia a unidad funcional, departamento o línea de atención.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenterServiceArea', @level2type = N'COLUMN', @level2name = N'ServiceAreaId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la area del servicio del erp con el que se haga interfaz', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenterServiceArea', @level2type = N'COLUMN', @level2name = N'ServiceAreaId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenterServiceArea', @level2type = N'COLUMN', @level2name = N'ServiceAreaId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico (INT) del centro de producción/centro de atención. Clave foránea a tabla ProductionCenter. Vincula el centro con sus áreas de servicio.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenterServiceArea', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del centro de produccion', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenterServiceArea', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenterServiceArea', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, IDENTITY) de la asociación entre centro de producción y área de servicio. Clave primaria de la tabla de relación.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenterServiceArea', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del centro de produccion del area de servicio', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenterServiceArea', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenterServiceArea', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación entre centros de producción y áreas de servicio. Asocia cada centro de atención o producción con las áreas de servicio (servicios habilitados) que le corresponden, incluyendo su código de área.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenterServiceArea';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenterServiceArea';
