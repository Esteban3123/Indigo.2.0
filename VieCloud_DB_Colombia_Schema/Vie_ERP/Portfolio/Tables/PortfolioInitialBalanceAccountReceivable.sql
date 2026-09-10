CREATE TABLE [Portfolio].[PortfolioInitialBalanceAccountReceivable] (
    [Id]                               INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PortfolioInitialBalanceId]        INT             NOT NULL,
    [AccountReceivableType]            TINYINT         NOT NULL,
    [ThirdPartyId]                     INT             NOT NULL,
    [CustomerId]                       INT             NULL,
    [InvoiceNumber]                    VARCHAR (20)    NOT NULL,
    [AccountReceivableDate]            DATETIME        NOT NULL,
    [Term]                             INT             NOT NULL,
    [ExpiredDate]                      DATETIME        NOT NULL,
    [Observations]                     VARCHAR (300)   NOT NULL,
    [PortfolioStatus]                  TINYINT         NOT NULL,
    [NumberShares]                     INT             NOT NULL,
    [Value]                            NUMERIC (18)    NOT NULL,
    [Balance]                          NUMERIC (18)    CONSTRAINT [DF_PortfolioInitialBalanceAccountReceivable_Balance] DEFAULT ((0)) NOT NULL,
    [CostCenterId]                     INT             NULL,
    [InvoiceCategoryId]                INT             NULL,
    [AccountWithoutRadicateId]         INT             CONSTRAINT [DF_PortfolioInitialBalanceAccountReceivable_AccountWithoutRadicateId] DEFAULT ((203)) NULL,
    [AccountRadicateId]                INT             CONSTRAINT [DF_PortfolioInitialBalanceAccountReceivable_AccountRadicateId] DEFAULT ((203)) NULL,
    [AccountObjectionRemediedId]       INT             CONSTRAINT [DF_PortfolioInitialBalanceAccountReceivable_AccountObjectionRemediedId] DEFAULT ((203)) NULL,
    [AccountConciliationId]            INT             NULL,
    [AccountLegalCollectionId]         INT             NULL,
    [AccountDebtorOrder]               INT             NULL,
    [AccountCreditorOrder]             INT             NULL,
    [AffectBudget]                     BIT             CONSTRAINT [DF_PortfolioInitialBalanceAccountReceivable_AffectBudget] DEFAULT ((0)) NOT NULL,
    [BudgetId]                         INT             NULL,
    [IsElectronicInvoice]              BIT             CONSTRAINT [DF__Portfolio__IsEle__088834A5] DEFAULT ((0)) NOT NULL,
    [CUFE]                             VARCHAR (250)   NULL,
    [AccountHardCollectionId]          INT             NULL,
    [DeteriorationBalance]             DECIMAL (18, 2) CONSTRAINT [DF_PortfolioInitialBalanceAccountReceivable_DeteriorationBalance] DEFAULT ((0)) NOT NULL,
    [DeteriorationBalanceCurrentYear]  DECIMAL (18, 2) CONSTRAINT [DF_PortfolioInitialBalanceAccountReceivable_DeteriorationBalanceCurrentYear] DEFAULT ((0)) NOT NULL,
    [DeteriorationBalancePreviousYear] DECIMAL (18, 2) CONSTRAINT [DF_PortfolioInitialBalanceAccountReceivable_DeteriorationBalancePreviousYear] DEFAULT ((0)) NOT NULL,
    [CurrentDeteriorationYear]         INT             NULL,
    [CUV]                              VARCHAR (200)   NULL,
    CONSTRAINT [PK_PortfolioInitialBalanceAccountReceivable] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CK_PortfolioInitialBalanceAccountReceivable_ValidateDeteriorationBalance] CHECK ([DeteriorationBalance]=([DeteriorationBalanceCurrentYear]+[DeteriorationBalancePreviousYear])),
    CONSTRAINT [CK_PortfolioInitialBalanceAccountReceivable_ValidateDeteriorationBalancesNotNegatives] CHECK ([DeteriorationBalance]>=(0) AND [DeteriorationBalanceCurrentYear]>=(0) AND [DeteriorationBalancePreviousYear]>=(0)),
    CONSTRAINT [FK_PortfolioAccountReceivable_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [Common].[Customer] ([Id]),
    CONSTRAINT [FK_PortfolioAccountReceivable_PortfolioInitialBalance] FOREIGN KEY ([PortfolioInitialBalanceId]) REFERENCES [Portfolio].[PortfolioInitialBalance] ([Id]),
    CONSTRAINT [FK_PortfolioAccountReceivable_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_PortfolioInitialBalanceAccountReceivable_Budget] FOREIGN KEY ([BudgetId]) REFERENCES [Budget].[Budget] ([Id]),
    CONSTRAINT [FK_PortfolioInitialBalanceAccountReceivable_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_PortfolioInitialBalanceAccountReceivable_InvoiceCategories] FOREIGN KEY ([InvoiceCategoryId]) REFERENCES [Billing].[InvoiceCategories] ([Id]),
    CONSTRAINT [FK_PortfolioInitialBalanceAccountReceivable_MainAccounts] FOREIGN KEY ([AccountWithoutRadicateId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_PortfolioInitialBalanceAccountReceivable_MainAccounts1] FOREIGN KEY ([AccountRadicateId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_PortfolioInitialBalanceAccountReceivable_MainAccounts2] FOREIGN KEY ([AccountObjectionRemediedId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_PortfolioInitialBalanceAccountReceivable_MainAccounts3] FOREIGN KEY ([AccountConciliationId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_PortfolioInitialBalanceAccountReceivable_MainAccounts4] FOREIGN KEY ([AccountLegalCollectionId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_PortfolioInitialBalanceAccountReceivable_MainAccounts5] FOREIGN KEY ([AccountDebtorOrder]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_PortfolioInitialBalanceAccountReceivable_MainAccounts6] FOREIGN KEY ([AccountCreditorOrder]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_PortfolioInitialBalanceAccountReceivable_MainAccounts7] FOREIGN KEY ([AccountHardCollectionId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id])
);


GO
ALTER TABLE [Portfolio].[PortfolioInitialBalanceAccountReceivable] NOCHECK CONSTRAINT [FK_PortfolioInitialBalanceAccountReceivable_MainAccounts7];






GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código Único de Factura Electrónica (CUFE) de la factura agregada en saldo inicial, identificador de validación fiscal', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'CUFE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'CUFE de la factura adicionada por saldo inicial', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'CUFE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'CUFE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano que valida si la factura es electrónica (1=Sí, 0=No), requiere CUFE asociado', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'IsElectronicInvoice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que valida si la factura es electrónica, lo que implica la adición del CUFE correspondiente.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'IsElectronicInvoice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'IsElectronicInvoice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del presupuesto de ingresos (FK Budget), vincula CxC a proyección presupuestaria', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'BudgetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id del presupuesto de ingresos', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'BudgetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'BudgetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si el grupo de atención afecta presupuesto (1=Sí, 0=No), genera reconocimiento automático al radicar factura', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'AffectBudget';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el grupo de atencion afecta presupuesto  1 - Si  0- No    Si esta como si, entonces cuando se vaya a radicar la factura este creara un reconocimiento automaticamente', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'AffectBudget';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'AffectBudget';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cuenta contable de orden de acreedores para glosas, exclusivo sector público (FK MainAccounts)', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'AccountCreditorOrder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta de orden de acreedores glosas, Solo para el sector publico', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'AccountCreditorOrder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'AccountCreditorOrder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cuenta contable de orden de deudores para glosas, exclusivo sector público (FK MainAccounts)', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'AccountDebtorOrder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Cuenta de orden de glosas, Solo para el sector publico', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'AccountDebtorOrder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'AccountDebtorOrder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cuenta contable para cobro jurídico o cuentas de difícil recaudo, sector privado (FK MainAccounts)', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'AccountLegalCollectionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable para cobro juridico o cuentas de dificil recaudo, Solo para empresas privadas', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'AccountLegalCollectionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'AccountLegalCollectionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cuenta contable de conciliación, exclusivo empresas privadas (FK MainAccounts)', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'AccountConciliationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta de conciliacion, solo para las empresas privadas', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'AccountConciliationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'AccountConciliationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cuenta contable para glosa subsanable/remedable, sector privado (FK MainAccounts)', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'AccountObjectionRemediedId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable de la Glosa Subsanable, Solo para las empresas privadas', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'AccountObjectionRemediedId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'AccountObjectionRemediedId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cuenta contable radicada, contabiliza factura registrada en sistema (FK MainAccounts)', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'AccountRadicateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable radicada', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'AccountRadicateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'AccountRadicateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cuenta contable sin radicación inicial, pendiente de registro en libro mayor (FK MainAccounts)', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'AccountWithoutRadicateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable sin Radicar', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'AccountWithoutRadicateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'AccountWithoutRadicateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de categoría de factura o CxC (básica, impuestos, inventarios, etc.) (FK InvoiceCategories)', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'InvoiceCategoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la categoria de la factura o CxC', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'InvoiceCategoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'InvoiceCategoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del centro de costos asociado a la CxC, usado para análisis presupuestario (FK CostCenter)', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id centro de costos', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo pendiente de la cuenta por cobrar (Numeric-18), diferencia entre Value y pagos acumulados', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'Balance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Saldo de la cuenta por cobrar', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'Balance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'Balance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor nominal/total de la cuenta por cobrar (Numeric-18), monto bruto sin aplicar pagos', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la cuenta por cobrar', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de cuotas o vencimientos de la cuenta por cobrar, para planes de pago fraccionado', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'NumberShares';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de cuotas', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'NumberShares';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'NumberShares';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de cartera: Sin Radicar(1), Radicada(2), Radicada Entidad(3), Objetada(4), Contestada Radicada(5), Aceptada(6), Certificada Parcial(7), Certificada Total(8), No Subsanable(9), Difícil Recaudo(10), Devuelta(11), Glosa Ratificada(12), Trámite Objeción(13), Devolución(14)', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'PortfolioStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de Cartera  Sin Radicar = 1  Radicada= 2  Radicada Entidad = 3,  Objetada =4  Contestada Radicada = 5  Aceptada =6  Certificada Parcial = 7  Certificada Total = 8  No Subsanable = 9  Difícil Recaudo =10  Factura Devuelta = 11  Glosa Ratificada =12  Radicación Tramite Objeción =13  Devolución Factura = 14  ', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'PortfolioStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'PortfolioStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo de notas o comentarios adicionales sobre la CxC, glosa, objeción o situación especial', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de vencimiento de la cuenta por cobrar (DATETIME), determina diferimiento y antigüedad de la deuda', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'ExpiredDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de vencimiento de la cuenta por cobrar', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'ExpiredDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'ExpiredDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Plazo de la CxC en días, período entre emisión y vencimiento original', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'Term';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Plazo de la cuenta (En dias)', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'Term';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'Term';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de origen o emisión de la cuenta por cobrar (DATETIME), punto de partida para cálculo de vencimiento', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'AccountReceivableDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la cuenta por cobrar', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'AccountReceivableDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'AccountReceivableDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número secuencial de la factura (VARCHAR-20), identificador único de referencia para la facturación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de la factura', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del cliente (FK Customer), puede ser nulo si tercero es diferente', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'CustomerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del cliente', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'CustomerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'CustomerId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tercero (proveedor, acreedor, deudor) asociado a la CxC (FK ThirdParty)', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero asociado', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de CxC: Básica(1), Facturación(2), Impuestos(3), Inventarios(4), Arrendamientos(5), Cuotaparte(6)', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'AccountReceivableType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de cuenta por cobrar (BÁSICA = 1,FACTURACIÓN = 2,IMPUESTOS = 3,INVENTARIOS = 4,ARRENDAMIENTOS = 5,CUOTAPARTE = 6)', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'AccountReceivableType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'AccountReceivableType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del saldo inicial o apertura de cartera asociado (FK PortfolioInitialBalance)', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'PortfolioInitialBalanceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del saldo inicial', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'PortfolioInitialBalanceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'PortfolioInitialBalanceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (IDENTITY) del registro de CxC en saldo inicial, clave primaria', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta por cobrar en saldo inicial', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuentas por cobrar del saldo inicial de cartera. Registra cada documento de cobro pendiente (facturas, títulos) con su valor, saldo, vencimiento y estado de gestión al momento de cargar el saldo inicial de cartera en el módulo de portfolio/cartera.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable';
