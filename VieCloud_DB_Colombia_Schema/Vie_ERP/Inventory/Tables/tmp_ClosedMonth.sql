CREATE TABLE [Inventory].[tmp_ClosedMonth] (
    [Id]                           INT             IDENTITY (1, 1) NOT NULL,
    [Code]                         VARCHAR (20)    NOT NULL,
    [Year]                         INT             NOT NULL,
    [Month]                        INT             NOT NULL,
    [InventoryNoLegalizedBalance]  DECIMAL (18, 2) NOT NULL,
    [InventoryLegalizedBalance]    DECIMAL (18, 2) NOT NULL,
    [AccountingNoLegalizedBalance] DECIMAL (18, 2) NOT NULL,
    [AccountingLegalizedBalance]   DECIMAL (18, 2) NOT NULL,
    [CreationUser]                 VARCHAR (20)    NOT NULL,
    [CreationDate]                 DATETIME        NOT NULL,
    CONSTRAINT [PK_Tmp_Inventory_ClosedMonth] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla temporal del esquema de inventario que almacena saldos de cierre mensual por período (año/mes) y código de artículo o centro. Registra cuatro tipos de saldo: inventario y contabilidad, cada uno en versión legalizada y no legalizada, lo que sugiere un proceso de conciliación entre el módulo de inventario y la contabilidad. Incluye auditoría básica de creación (usuario y fecha).', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'TABLE', @level1name=N'tmp_ClosedMonth';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'TABLE', @level1name=N'tmp_ClosedMonth';
GO
