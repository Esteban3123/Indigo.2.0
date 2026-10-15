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
ALTER TABLE [InteropCost].[ProductionCenterHomologation] NOCHECK CONSTRAINT [FK_ProductionCenterHomologation_ProductionCenter];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenterHomologation', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de homologacion  1 - Mano de obra  2 - Suministros  3 - Consumos  4 - Gastos Generales  5 - Depreciacion  6 - Ventas    1 - Mano de Obra  Cuando se selecciona mano de obra se deben leer todas la cuentas contables que esten parametrizadas en los conceptos de nomina, esas cuentas se mostraran como cuentas de origen    2 - Suministros  Cuendo se seleccione suministros en el campo cuenta origen se mostrar la cuenta de suministros que estan parametrizadas en las areas de servicios que ya seleccionaron    3 - Consumos  Cuando se seleccione consumos en el campo cuenta origen se debe mostrar la cuenta de suministro a pacientes que estan parametrizadas en las areas de servicios que ya seleccionaron    4 - Gastos Generales  Cuando se seleccione gastos se van a listar todas las cuentas auxiliares que pertenescan a las clases 5 y 7    5 - Depreciacion  Cuando se seleccione depreciacion se van a cargar todas las cuentas que tengan asignados los responsables de Activos fijos que tengan el mismo centro de costo del centro de produccion, Despues de que haya datos no se puede editar el centro de costo      Notas General: Todas las cuentas de destinos van a ser de Clase 6', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenterHomologation', @level2type = N'COLUMN', @level2name = N'HomologationType';

