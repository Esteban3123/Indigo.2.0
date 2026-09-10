CREATE TABLE [InteropCost].[ProductionCenterHomologation] (
    [Id]                 INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ProductionCenterId] INT           NOT NULL,
    [HomologationType]   TINYINT       NOT NULL,
    [Description]        VARCHAR (300) NOT NULL,
    [AccountOriginId]    INT           NOT NULL,
    [AccountOrigin]      VARCHAR (20)  NOT NULL,
    [AccountTargetId]    INT           NULL,
    [AccountTarget]      VARCHAR (20)  NULL,
    CONSTRAINT [PK_ProductionCenterHomologation] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProductionCenterHomologation_ProductionCenter] FOREIGN KEY ([ProductionCenterId]) REFERENCES [InteropCost].[ProductionCenter] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable de destino (Clase 6), código contable destino del ERP interfazado, cuenta receptora de asientos homologados', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenterHomologation', @level2type = N'COLUMN', @level2name = N'AccountTarget';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta de destino, siempre van hacer de clase 6', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenterHomologation', @level2type = N'COLUMN', @level2name = N'AccountTarget';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenterHomologation', @level2type = N'COLUMN', @level2name = N'AccountTarget';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta contable de destino en el ERP interfazado, clave foránea a catálogo de cuentas, destino de pólizas', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenterHomologation', @level2type = N'COLUMN', @level2name = N'AccountTargetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable de destino del erp con el que se haga interfaz', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenterHomologation', @level2type = N'COLUMN', @level2name = N'AccountTargetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenterHomologation', @level2type = N'COLUMN', @level2name = N'AccountTargetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable de origen, código contable fuente según tipo homologación (nómina, suministros, consumos, gastos, depreciación)', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenterHomologation', @level2type = N'COLUMN', @level2name = N'AccountOrigin';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta de origen', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenterHomologation', @level2type = N'COLUMN', @level2name = N'AccountOrigin';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenterHomologation', @level2type = N'COLUMN', @level2name = N'AccountOrigin';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta contable de origen en el ERP, clave foránea a catálogo de cuentas, fuente de asientos contables', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenterHomologation', @level2type = N'COLUMN', @level2name = N'AccountOriginId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable del ERP con el que se haga interfaz', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenterHomologation', @level2type = N'COLUMN', @level2name = N'AccountOriginId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenterHomologation', @level2type = N'COLUMN', @level2name = N'AccountOriginId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción o nombre del mapeo de homologación, texto explicativo del criterio de conversión entre centros de producción', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenterHomologation', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenterHomologation', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenterHomologation', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de homologación contable: 1=Mano de Obra (conceptos nómina), 2=Suministros (servicios), 3=Consumos pacientes, 4=Gastos Generales (clases 5-7), 5=Depreciación (activos fijos), 6=Ventas (clase 6)', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenterHomologation', @level2type = N'COLUMN', @level2name = N'HomologationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de homologacion  1 - Mano de obra  2 - Suministros  3 - Consumos  4 - Gastos Generales  5 - Depreciacion  6 - Ventas    1 - Mano de Obra  Cuando se selecciona mano de obra se deben leer todas la cuentas contables que esten parametrizadas en los conceptos de nomina, esas cuentas se mostraran como cuentas de origen    2 - Suministros  Cuendo se seleccione suministros en el campo cuenta origen se mostrar la cuenta de suministros que estan parametrizadas en las areas de servicios que ya seleccionaron    3 - Consumos  Cuando se seleccione consumos en el campo cuenta origen se debe mostrar la cuenta de suministro a pacientes que estan parametrizadas en las areas de servicios que ya seleccionaron    4 - Gastos Generales  Cuando se seleccione gastos se van a listar todas las cuentas auxiliares que pertenescan a las clases 5 y 7    5 - Depreciacion  Cuando se seleccione depreciacion se van a cargar todas las cuentas que tengan asignados los responsables de Activos fijos que tengan el mismo centro de costo del centro de produccion, Despues de que haya datos no se puede editar el centro de costo      Notas General: Todas las cuentas de destinos van a ser de Clase 6', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenterHomologation', @level2type = N'COLUMN', @level2name = N'HomologationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenterHomologation', @level2type = N'COLUMN', @level2name = N'HomologationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del centro de producción, unidad funcional o área de servicio origen de la homologación, FK a ProductionCenter', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenterHomologation', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del centro de produccion', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenterHomologation', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenterHomologation', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la homologación, clave primaria, registro de mapeo contable entre centros', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenterHomologation', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la homologacion', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenterHomologation', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenterHomologation', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Homologación de centros de producción para interoperabilidad de costos: relaciona cada centro de producción con sus cuentas contables de origen y destino según el tipo de homologación, permitiendo traducir o mapear cuentas entre sistemas.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenterHomologation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenterHomologation';
