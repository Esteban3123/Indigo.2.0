CREATE TABLE [Cost].[CostProductionCenterHomologation] (
    [Id]                         INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ProductionCenterId]         INT           NOT NULL,
    [HomologationType]           TINYINT       NOT NULL,
    [Description]                VARCHAR (300) NOT NULL,
    [AccountOriginId]            INT           NOT NULL,
    [AccountTargetId]            INT           NULL,
    [AllowSecondaryDistribution] BIT           CONSTRAINT [DF_CostProductionCenterHomologation_AllowSecondaryDistribution] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_CostProductionCenterHomologation] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostProductionCenterHomologation_CostProductionCenter] FOREIGN KEY ([ProductionCenterId]) REFERENCES [Cost].[CostProductionCenter] ([Id]),
    CONSTRAINT [FK_CostProductionCenterHomologation_MainAccounts] FOREIGN KEY ([AccountOriginId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_CostProductionCenterHomologation_MainAccounts1] FOREIGN KEY ([AccountTargetId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT, default=1) que habilita/deshabilita la distribución secundaria de costos desde este centro de producción hacia otras unidades funcionales o centros de costo. Valor 1=permitido, 0=no permitido.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostProductionCenterHomologation', @level2type = N'COLUMN', @level2name = N'AllowSecondaryDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite distribución secundaria', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostProductionCenterHomologation', @level2type = N'COLUMN', @level2name = N'AllowSecondaryDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostProductionCenterHomologation', @level2type = N'COLUMN', @level2name = N'AllowSecondaryDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, NULLABLE) de la cuenta contable destino del ERP interfazado. FK a [GeneralLedger].[MainAccounts]. Siempre pertenece a Clase 6. Puede ser nula si no hay mapeo destino.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostProductionCenterHomologation', @level2type = N'COLUMN', @level2name = N'AccountTargetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable de destino del erp con el que se haga interfaz', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostProductionCenterHomologation', @level2type = N'COLUMN', @level2name = N'AccountTargetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostProductionCenterHomologation', @level2type = N'COLUMN', @level2name = N'AccountTargetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de la cuenta contable origen/fuente del ERP interfazado. FK a [GeneralLedger].[MainAccounts]. La cuenta origen varía según HomologationType (nómina, suministros, consumos, gastos, depreciación).', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostProductionCenterHomologation', @level2type = N'COLUMN', @level2name = N'AccountOriginId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable del ERP con el que se haga interfaz', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostProductionCenterHomologation', @level2type = N'COLUMN', @level2name = N'AccountOriginId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostProductionCenterHomologation', @level2type = N'COLUMN', @level2name = N'AccountOriginId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Texto descriptivo (VARCHAR 300) de la homologación entre centros de producción y cuentas contables. Documenta la configuración del mapeo contable para costos de salud.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostProductionCenterHomologation', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción de la Homologación de Centros de Producción de Costos', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostProductionCenterHomologation', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostProductionCenterHomologation', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación (TINYINT 1-6) del tipo de homologación contable: 1=Mano de obra (lee cuentas de nómina), 2=Suministros (áreas de servicios), 3=Consumos (suministros a pacientes), 4=Gastos generales (clases 5-7), 5=Depreciación (activos fijos), 6=Ventas. Define qué cuentas origen se carguen automáticamente.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostProductionCenterHomologation', @level2type = N'COLUMN', @level2name = N'HomologationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de homologacion  1 - Mano de obra  2 - Suministros  3 - Consumos  4 - Gastos Generales  5 - Depreciacion  6 - Ventas    1 - Mano de Obra  Cuando se selecciona mano de obra se deben leer todas la cuentas contables que esten parametrizadas en los conceptos de nomina, esas cuentas se mostraran como cuentas de origen    2 - Suministros  Cuendo se seleccione suministros en el campo cuenta origen se mostrar la cuenta de suministros que estan parametrizadas en las areas de servicios que ya seleccionaron    3 - Consumos  Cuando se seleccione consumos en el campo cuenta origen se debe mostrar la cuenta de suministro a pacientes que estan parametrizadas en las areas de servicios que ya seleccionaron    4 - Gastos Generales  Cuando se seleccione gastos se van a listar todas las cuentas auxiliares que pertenescan a las clases 5 y 7    5 - Depreciacion  Cuando se seleccione depreciacion se van a cargar todas las cuentas que tengan asignados los responsables de Activos fijos que tengan el mismo centro de costo del centro de produccion, Despues de que haya datos no se puede editar el centro de costo      Notas General: Todas las cuentas de destinos van a ser de Clase 6', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostProductionCenterHomologation', @level2type = N'COLUMN', @level2name = N'HomologationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostProductionCenterHomologation', @level2type = N'COLUMN', @level2name = N'HomologationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) del centro de producción/unidad funcional. FK a [Cost].[CostProductionCenter]. Referencia a la estructura de costos del centro de atención.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostProductionCenterHomologation', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del centro de produccion', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostProductionCenterHomologation', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostProductionCenterHomologation', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT) de la homologación de centro de producción de costos. Clave primaria con identidad.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostProductionCenterHomologation', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la homologacion', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostProductionCenterHomologation', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostProductionCenterHomologation', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Homologación de centros de producción para costos: define cómo se mapean o traducen las cuentas contables de origen a cuentas destino según el tipo de homologación, permitiendo la distribución y reclasificación de costos entre centros de producción.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostProductionCenterHomologation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostProductionCenterHomologation';
