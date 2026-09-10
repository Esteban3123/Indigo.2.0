CREATE TABLE [Inventory].[tmp_ClosedMonthModulesConciliation] (
    [Id]                    INT             IDENTITY (1, 1) NOT NULL,
    [Year]                  INT             NOT NULL,
    [Month]                 INT             NOT NULL,
    [EntityName]            VARCHAR (250)   NOT NULL,
    [MainAccountId]         INT             NOT NULL,
    [TotalDebitAccounting]  DECIMAL (20, 2) NOT NULL,
    [TotalCreditAccounting] DECIMAL (20, 2) NOT NULL,
    [TotalDebitInventory]   DECIMAL (20, 2) NOT NULL,
    [TotalCreditInventory]  DECIMAL (20, 2) NOT NULL,
    CONSTRAINT [PK_Tmp_ClosedMonthModulesConciliation] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla temporal que almacena resultados del proceso de conciliación entre el módulo de inventario y el módulo contable para un período mensual cerrado. Registra por entidad y cuenta contable principal los totales de débitos y créditos de ambos módulos, permitiendo comparar si los movimientos de inventario coinciden con los registros contables del mes y año indicados.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'TABLE', @level1name=N'tmp_ClosedMonthModulesConciliation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'TABLE', @level1name=N'tmp_ClosedMonthModulesConciliation';
GO
