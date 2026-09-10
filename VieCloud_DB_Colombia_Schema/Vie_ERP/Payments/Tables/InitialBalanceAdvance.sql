CREATE TABLE [Payments].[InitialBalanceAdvance] (
    [Id]                          INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [InitialBalanceId]            INT           NOT NULL,
    [SupplierId]                  INT           NOT NULL,
    [ThirdPartyId]                INT           NOT NULL,
    [SupplierDistributionLinesId] INT           NOT NULL,
    [MainAccountId]               INT           NOT NULL,
    [CostCenterId]                INT           NULL,
    [AdvancePaymentsId]           INT           NULL,
    [AdvancePaymentsDate]         DATETIME      NOT NULL,
    [AdvancePaymentsDescription]  VARCHAR (200) NOT NULL,
    [Value]                       DECIMAL (18)  NOT NULL,
    CONSTRAINT [PK_InitialBalanceAdvance] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InitialBalanceAdvance_AdvancePayments] FOREIGN KEY ([AdvancePaymentsId]) REFERENCES [Payments].[AdvancePayments] ([Id]),
    CONSTRAINT [FK_InitialBalanceAdvance_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_InitialBalanceAdvance_InitialBalance] FOREIGN KEY ([InitialBalanceId]) REFERENCES [Payments].[InitialBalance] ([Id]),
    CONSTRAINT [FK_InitialBalanceAdvance_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_InitialBalanceAdvance_Supplier] FOREIGN KEY ([SupplierId]) REFERENCES [Common].[Supplier] ([Id]),
    CONSTRAINT [FK_InitialBalanceAdvance_SuppliersDistributionLines] FOREIGN KEY ([SupplierDistributionLinesId]) REFERENCES [Common].[SuppliersDistributionLines] ([Id]),
    CONSTRAINT [FK_InitialBalanceAdvance_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);




GO



GO



GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario del anticipo en decimal(18), monto del saldo inicial asignado al pago anticipado', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAdvance', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del saldo', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAdvance', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAdvance', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual del anticipo (máx 200 caracteres), concepto o motivo del pago adelantado', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAdvance', @level2type = N'COLUMN', @level2name = N'AdvancePaymentsDescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del anticipo', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAdvance', @level2type = N'COLUMN', @level2name = N'AdvancePaymentsDescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAdvance', @level2type = N'COLUMN', @level2name = N'AdvancePaymentsDescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del anticipo (DATETIME), cuándo se efectuó o registró el pago anticipado', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAdvance', @level2type = N'COLUMN', @level2name = N'AdvancePaymentsDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del anticipo', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAdvance', @level2type = N'COLUMN', @level2name = N'AdvancePaymentsDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAdvance', @level2type = N'COLUMN', @level2name = N'AdvancePaymentsDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del anticipo (FK a Payments.AdvancePayments), referencia al registro de pago adelantado', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAdvance', @level2type = N'COLUMN', @level2name = N'AdvancePaymentsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del anticipo', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAdvance', @level2type = N'COLUMN', @level2name = N'AdvancePaymentsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAdvance', @level2type = N'COLUMN', @level2name = N'AdvancePaymentsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del centro de costos (FK a Payroll.CostCenter), opcional, punto de imputación contable del anticipo', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAdvance', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID centro de costos', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAdvance', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAdvance', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta contable principal (FK a GeneralLedger.MainAccounts), para registro en libro mayor', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAdvance', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAdvance', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAdvance', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de línea de distribución del proveedor (FK a Common.SuppliersDistributionLines), asignación a ramo o modalidad', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAdvance', @level2type = N'COLUMN', @level2name = N'SupplierDistributionLinesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Linea de distribucion del proveedor', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAdvance', @level2type = N'COLUMN', @level2name = N'SupplierDistributionLinesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAdvance', @level2type = N'COLUMN', @level2name = N'SupplierDistributionLinesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tercero (FK a Common.ThirdParty), entidad relacionada como acreedor o beneficiario del anticipo', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAdvance', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAdvance', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAdvance', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del proveedor (FK a Common.Supplier), entidad proveedora del bien o servicio financiado', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAdvance', @level2type = N'COLUMN', @level2name = N'SupplierId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del proveedor', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAdvance', @level2type = N'COLUMN', @level2name = N'SupplierId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAdvance', @level2type = N'COLUMN', @level2name = N'SupplierId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del saldo inicial (FK a Payments.InitialBalance), vinculación al balance de apertura del período', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAdvance', @level2type = N'COLUMN', @level2name = N'InitialBalanceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del saldo inicial asociado a este anticipo', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAdvance', @level2type = N'COLUMN', @level2name = N'InitialBalanceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAdvance', @level2type = N'COLUMN', @level2name = N'InitialBalanceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK INT IDENTITY), clave primaria de la relación entre saldo inicial y anticipo', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAdvance', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la relacion entre saldo inicial y anticipo', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAdvance', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAdvance', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Anticipos registrados como parte del saldo inicial contable. Cada registro vincula un anticipo de pago a un proveedor, un tercero y una línea de distribución contable, guardando el valor, la fecha y la descripción del anticipo para el proceso de apertura o migración de saldos iniciales.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAdvance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAdvance';
