CREATE TABLE [Inventory].[ClosedMonthModulesConciliation] (
    [Id]                    INT             IDENTITY (1, 1) NOT NULL,
    [Year]                  INT             NOT NULL,
    [Month]                 INT             NOT NULL,
    [EntityName]            VARCHAR (250)   NOT NULL,
    [MainAccountId]         INT             NOT NULL,
    [TotalDebitAccounting]  DECIMAL (20, 2) NOT NULL,
    [TotalCreditAccounting] DECIMAL (20, 2) NOT NULL,
    [TotalDebitInventory]   DECIMAL (20, 2) NOT NULL,
    [TotalCreditInventory]  DECIMAL (20, 2) NOT NULL,
    CONSTRAINT [PK_ClosedMonthModulesConciliation] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90),
    CONSTRAINT [FK_ClosedMonthModulesConciliation_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total de créditos registrados en módulo de Inventario (DECIMAL 20,2). Suma de movimientos de entrada/incremento de stock para el período, para conciliación con Contabilidad.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthModulesConciliation', @level2type = N'COLUMN', @level2name = N'TotalCreditInventory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Total Creditos de  inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthModulesConciliation', @level2type = N'COLUMN', @level2name = N'TotalCreditInventory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthModulesConciliation', @level2type = N'COLUMN', @level2name = N'TotalCreditInventory';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total de débitos registrados en módulo de Inventario (DECIMAL 20,2). Suma de movimientos de salida/decremento de stock para el período, para conciliación con Contabilidad.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthModulesConciliation', @level2type = N'COLUMN', @level2name = N'TotalDebitInventory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Total Debitos de inventario ', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthModulesConciliation', @level2type = N'COLUMN', @level2name = N'TotalDebitInventory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthModulesConciliation', @level2type = N'COLUMN', @level2name = N'TotalDebitInventory';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total de créditos registrados en módulo de Contabilidad/Libro Mayor (DECIMAL 20,2). Suma de movimientos contables de entrada para validar coincidencia con Inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthModulesConciliation', @level2type = N'COLUMN', @level2name = N'TotalCreditAccounting';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Total Creditos de  contabilidad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthModulesConciliation', @level2type = N'COLUMN', @level2name = N'TotalCreditAccounting';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthModulesConciliation', @level2type = N'COLUMN', @level2name = N'TotalCreditAccounting';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total de débitos registrados en módulo de Contabilidad/Libro Mayor (DECIMAL 20,2). Suma de movimientos contables de salida para validar coincidencia con Inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthModulesConciliation', @level2type = N'COLUMN', @level2name = N'TotalDebitAccounting';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Total Debitos de contabilidad ', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthModulesConciliation', @level2type = N'COLUMN', @level2name = N'TotalDebitAccounting';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthModulesConciliation', @level2type = N'COLUMN', @level2name = N'TotalDebitAccounting';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la Cuenta Contable asociada (clave foránea FK a GeneralLedger.MainAccounts). Agrupa movimientos por cuenta del plan de cuentas, usualmente derivada del grupo/familia de productos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthModulesConciliation', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta contable de los movimientos , se obtiene del grupo de productos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthModulesConciliation', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthModulesConciliation', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o tipo de documento transaccional (VARCHAR 250). Identifica la fuente de movimiento: compra, venta, ajuste, devolución, traslado, etc.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthModulesConciliation', @level2type = N'COLUMN', @level2name = N'EntityName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del documento de la transaccion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthModulesConciliation', @level2type = N'COLUMN', @level2name = N'EntityName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthModulesConciliation', @level2type = N'COLUMN', @level2name = N'EntityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mes del período de conciliación (INT, 1-12). Define el mes contable y de inventario para cierre mensual.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthModulesConciliation', @level2type = N'COLUMN', @level2name = N'Month';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mes  de la conciliacion de los movimientos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthModulesConciliation', @level2type = N'COLUMN', @level2name = N'Month';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthModulesConciliation', @level2type = N'COLUMN', @level2name = N'Month';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Año del período de conciliación (INT). Define el ejercicio fiscal para cierre mensual.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthModulesConciliation', @level2type = N'COLUMN', @level2name = N'Year';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Año de la conciliacion de los movimientos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthModulesConciliation', @level2type = N'COLUMN', @level2name = N'Year';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthModulesConciliation', @level2type = N'COLUMN', @level2name = N'Year';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de registro (INT IDENTITY 1,1). Clave primaria de la tabla ClosedMonthModulesConciliation.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthModulesConciliation', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthModulesConciliation', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthModulesConciliation', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de conciliación de cierre mensual entre módulos Contabilidad e Inventario. Acumula totales de movimientos por tipo de documento (compras, ventas, ajustes, devoluciones) para validar coincidencia de débitos y créditos entre ambos módulos en período contable cerrado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthModulesConciliation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tabla que guarda los totales de movimientos por tipo de documento por mes para el cierre de mes de inventario, es la conciliacion de movimientos entre contabilidad e inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthModulesConciliation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthModulesConciliation';

