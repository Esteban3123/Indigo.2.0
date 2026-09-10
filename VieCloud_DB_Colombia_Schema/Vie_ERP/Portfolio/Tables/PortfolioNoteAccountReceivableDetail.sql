CREATE TABLE [Portfolio].[PortfolioNoteAccountReceivableDetail] (
    [Id]                               INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PortfolioNoteAccountReceivableId] INT             NOT NULL,
    [EntityName]                       VARCHAR (250)   NOT NULL,
    [EntityId]                         INT             NOT NULL,
    [MainAccountId]                    INT             NOT NULL,
    [CostCenterId]                     INT             NULL,
    [Value]                            NUMERIC (18, 2) NOT NULL,
    [BaseValue]                        NUMERIC (20, 2) CONSTRAINT [DF__Portfolio__BaseV__46505CF4] DEFAULT ((0)) NOT NULL,
    [TaxValue]                         NUMERIC (20, 2) CONSTRAINT [DF__Portfolio__TaxVa__4744812D] DEFAULT ((0)) NOT NULL,
    [TaxPercentage]                    NUMERIC (5, 2)  NULL,
    [TaxId]                            INT             NULL,
    [ThirdPartyId]                     INT             NULL,
    CONSTRAINT [PK_PortfolioNoteAccountReceivableDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PortfolioNoteAccountReceivableDetail_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_PortfolioNoteAccountReceivableDetail_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_PortfolioNoteAccountReceivableDetail_PortfolioNoteAccountReceivableAdvance] FOREIGN KEY ([PortfolioNoteAccountReceivableId]) REFERENCES [Portfolio].[PortfolioNoteAccountReceivableAdvance] ([Id]),
    CONSTRAINT [FK_PortfolioNoteAccountReceivableDetail_TaxId] FOREIGN KEY ([TaxId]) REFERENCES [GeneralLedger].[GeneralLedgerIVA] ([Id]),
    CONSTRAINT [FK_PortfolioNoteAccountReceivableDetail_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del impuesto IVA asociado; FK a GeneralLedger.GeneralLedgerIVA; permite vincular la tarifa impositiva aplicada al detalle de la cuenta por cobrar.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableDetail', @level2type = N'COLUMN', @level2name = N'TaxId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del IVA Asociado', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableDetail', @level2type = N'COLUMN', @level2name = N'TaxId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableDetail', @level2type = N'COLUMN', @level2name = N'TaxId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de impuesto (IVA) aplicado al valor base del detalle; rango 0-100, típicamente 0, 5, 8, 16, 19%; NUMERIC(5,2).', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableDetail', @level2type = N'COLUMN', @level2name = N'TaxPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el porcentaje de impuestos.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableDetail', @level2type = N'COLUMN', @level2name = N'TaxPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableDetail', @level2type = N'COLUMN', @level2name = N'TaxPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto del impuesto IVA calculado sobre el valor base; NUMERIC(20,2); default 0; componente de tributación en facturación y glosas.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableDetail', @level2type = N'COLUMN', @level2name = N'TaxValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del IVA', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableDetail', @level2type = N'COLUMN', @level2name = N'TaxValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableDetail', @level2type = N'COLUMN', @level2name = N'TaxValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor base antes de aplicar impuestos; NUMERIC(20,2); default 0; monto principal sobre el cual se calcula el TaxValue en notas de ajuste a cuentas por cobrar.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableDetail', @level2type = N'COLUMN', @level2name = N'BaseValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor base, antes de IVA', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableDetail', @level2type = N'COLUMN', @level2name = N'BaseValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableDetail', @level2type = N'COLUMN', @level2name = N'BaseValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del ajuste contable del detalle de la nota de cuenta por cobrar; NUMERIC(18,2); monto total impactado en el registro de cartera.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableDetail', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del ajuste', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableDetail', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableDetail', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Centro de costo asignado al detalle; FK a Payroll.CostCenter; clasificación contable por unidad funcional, área de atención o departamento de salud.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Centro de costo del detalle', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable principal del detalle; FK a GeneralLedger.MainAccounts; vincula el movimiento a la estructura del plan de cuentas para registros de cartera.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableDetail', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta contable del detalle', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableDetail', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableDetail', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la entidad origen (paciente, proveedor, contrato) asociada al registro de ajuste; INT; clave para trazabilidad de la cartera.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableDetail', @level2type = N'COLUMN', @level2name = N'EntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador de la entidad origen asociada al registro', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableDetail', @level2type = N'COLUMN', @level2name = N'EntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableDetail', @level2type = N'COLUMN', @level2name = N'EntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la entidad origen (paciente, proveedor, contrato, unidad de negocio) vinculada al detalle; VARCHAR(250); facilita búsqueda e identificación del deudor.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableDetail', @level2type = N'COLUMN', @level2name = N'EntityName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la entidad origen relacionada al registro', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableDetail', @level2type = N'COLUMN', @level2name = N'EntityName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableDetail', @level2type = N'COLUMN', @level2name = N'EntityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK a Portfolio.PortfolioNoteAccountReceivableAdvance; identifica la nota de ajuste de cuenta por cobrar a la cual pertenece este detalle.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableDetail', @level2type = N'COLUMN', @level2name = N'PortfolioNoteAccountReceivableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la cuenta por cobrar asociada a la nota', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableDetail', @level2type = N'COLUMN', @level2name = N'PortfolioNoteAccountReceivableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableDetail', @level2type = N'COLUMN', @level2name = N'PortfolioNoteAccountReceivableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del detalle de la nota de cuenta por cobrar; PK; IDENTITY(1,1); clave para referenciar líneas individuales de ajuste en cartera.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del Registro', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tercero opcional del detalle; FK a Common.ThirdParty; nullable. Usado como override contable en NoteType=6 / EntityName=Glosas (Type 2 vigencias anteriores). Si es NULL, el SP usa AccountReceivable.ThirdPartyId.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de las líneas contables asociadas a una nota de cartera en cuentas por cobrar. Cada registro representa un movimiento desglosado por entidad, cuenta contable, centro de costo y valores con impuestos para la conciliación y registro contable de la cartera.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableDetail';
