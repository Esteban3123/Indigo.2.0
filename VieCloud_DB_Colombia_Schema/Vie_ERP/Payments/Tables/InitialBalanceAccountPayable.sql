CREATE TABLE [Payments].[InitialBalanceAccountPayable] (
    [Id]                          INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [InitialBalanceId]            INT             NOT NULL,
    [SupplierId]                  INT             NOT NULL,
    [ThirdPartyId]                INT             NOT NULL,
    [SupplierDistributionLinesID] INT             NOT NULL,
    [MainAccountId]               INT             NOT NULL,
    [CostCenterId]                INT             NULL,
    [AccountPayableId]            INT             NULL,
    [BillNumber]                  VARCHAR (20)    NOT NULL,
    [BillDate]                    DATETIME        NOT NULL,
    [Term]                        INT             NOT NULL,
    [ExpiredDate]                 DATETIME        NOT NULL,
    [Value]                       DECIMAL (18, 2) NOT NULL,
    [Balance]                     DECIMAL (18, 2) NOT NULL,
    [ServicePeriodDate]           DATETIME        CONSTRAINT [DF_InitialBalanceAccountPayable_ServicePeriodDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [FilingUnitId]                INT             NOT NULL,
    [SupplierTypeId]              INT             NOT NULL,
    [RadicatedDate]               DATETIME        CONSTRAINT [DF__InitialBa__Radic__5D273EA6] DEFAULT ('2022-01-01') NOT NULL,
    [CurrencyId]                  INT             NULL,
    CONSTRAINT [PK_InitialBalanceAccountPayable] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InitialBalanceAccountPayable_AccountPayable] FOREIGN KEY ([AccountPayableId]) REFERENCES [Payments].[AccountPayable] ([Id]),
    CONSTRAINT [FK_InitialBalanceAccountPayable_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_InitialBalanceAccountPayable_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_InitialBalanceAccountPayable_FilingUnit] FOREIGN KEY ([FilingUnitId]) REFERENCES [Payments].[FilingUnit] ([Id]),
    CONSTRAINT [FK_InitialBalanceAccountPayable_InitialBalance] FOREIGN KEY ([InitialBalanceId]) REFERENCES [Payments].[InitialBalance] ([Id]),
    CONSTRAINT [FK_InitialBalanceAccountPayable_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_InitialBalanceAccountPayable_Supplier] FOREIGN KEY ([SupplierId]) REFERENCES [Common].[Supplier] ([Id]),
    CONSTRAINT [FK_InitialBalanceAccountPayable_SuppliersDistributionLines] FOREIGN KEY ([SupplierDistributionLinesID]) REFERENCES [Common].[SuppliersDistributionLines] ([Id]),
    CONSTRAINT [FK_InitialBalanceAccountPayable_SupplierType] FOREIGN KEY ([SupplierTypeId]) REFERENCES [Common].[SupplierType] ([Id]),
    CONSTRAINT [FK_InitialBalanceAccountPayable_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);




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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la moneda o divisa (FK → Common.Currency). Tipo: INT NULL. Permite filtrar saldos por USD, COP, EUR u otra moneda de transacción.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAccountPayable', @level2type = N'COLUMN', @level2name = N'CurrencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la moneda', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAccountPayable', @level2type = N'COLUMN', @level2name = N'CurrencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAccountPayable', @level2type = N'COLUMN', @level2name = N'CurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de radicación o registro oficial de la factura ante la autoridad competente (DATETIME, default 2022-01-01). Usado en auditoría y RIPS.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAccountPayable', @level2type = N'COLUMN', @level2name = N'RadicatedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'FECHA DE RADICACION', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAccountPayable', @level2type = N'COLUMN', @level2name = N'RadicatedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAccountPayable', @level2type = N'COLUMN', @level2name = N'RadicatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de proveedor clasificado en último nivel jerárquico (FK → Common.SupplierType). Solo selecciona nodos hoja de la estructura padre-hijo. Tipo: INT. Ej: farmacéutico, laboratorio clínico, servicios de salud.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAccountPayable', @level2type = N'COLUMN', @level2name = N'SupplierTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de proveedor, este se obtiene de la tabla SupplierDetailType pero en esta tabla solo estan los padres y en este campos solo se pueden seleccionar los hijos de ultimo nivel de esos registros, es decir que el tipo de proveedor que seleccionen debe estar en el ultimo nivel en la jerarquia', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAccountPayable', @level2type = N'COLUMN', @level2name = N'SupplierTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAccountPayable', @level2type = N'COLUMN', @level2name = N'SupplierTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad de radicación o centro de atención donde se radica la factura (FK → Payments.FilingUnit). Tipo: INT NOT NULL.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAccountPayable', @level2type = N'COLUMN', @level2name = N'FilingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad de radicacion', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAccountPayable', @level2type = N'COLUMN', @level2name = N'FilingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAccountPayable', @level2type = N'COLUMN', @level2name = N'FilingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del período de servicio prestado. Se utiliza para reportes, auditoría y conciliación (DATETIME, auto-generada). Tipo: DATETIME NOT NULL.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAccountPayable', @level2type = N'COLUMN', @level2name = N'ServicePeriodDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la fecha del periodo del servicio, este campos se utuliza para reportes', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAccountPayable', @level2type = N'COLUMN', @level2name = N'ServicePeriodDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAccountPayable', @level2type = N'COLUMN', @level2name = N'ServicePeriodDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del saldo pendiente de la cuenta por pagar en moneda original (DECIMAL 18,2). Diferencia entre Value y pagos realizados. Tipo: DECIMAL NOT NULL.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAccountPayable', @level2type = N'COLUMN', @level2name = N'Balance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del saldo', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAccountPayable', @level2type = N'COLUMN', @level2name = N'Balance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAccountPayable', @level2type = N'COLUMN', @level2name = N'Balance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total de la factura o documento de gasto inicial (DECIMAL 18,2). Monto bruto antes de descuentos. Tipo: DECIMAL NOT NULL.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAccountPayable', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del saldo', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAccountPayable', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAccountPayable', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de vencimiento o fecha límite de pago de la factura según plazo pactado (DATETIME). Tipo: DATETIME NOT NULL.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAccountPayable', @level2type = N'COLUMN', @level2name = N'ExpiredDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de vencimiento', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAccountPayable', @level2type = N'COLUMN', @level2name = N'ExpiredDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAccountPayable', @level2type = N'COLUMN', @level2name = N'ExpiredDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Plazo en días para el pago de la factura. Ej: 30, 60, 90 días (INT). Tipo: INT NOT NULL.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAccountPayable', @level2type = N'COLUMN', @level2name = N'Term';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Plazo', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAccountPayable', @level2type = N'COLUMN', @level2name = N'Term';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAccountPayable', @level2type = N'COLUMN', @level2name = N'Term';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de emisión de la factura, comprobante o documento de soporte (DATETIME). Tipo: DATETIME NOT NULL.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAccountPayable', @level2type = N'COLUMN', @level2name = N'BillDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la factura', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAccountPayable', @level2type = N'COLUMN', @level2name = N'BillDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAccountPayable', @level2type = N'COLUMN', @level2name = N'BillDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de factura, comprobante o documento único del proveedor (VARCHAR 20). Tipo: VARCHAR NOT NULL. Búsqueda: factura, documento, comprobante.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAccountPayable', @level2type = N'COLUMN', @level2name = N'BillNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de la factura', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAccountPayable', @level2type = N'COLUMN', @level2name = N'BillNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAccountPayable', @level2type = N'COLUMN', @level2name = N'BillNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta por pagar asociada (FK → Payments.AccountPayable, nullable). Tipo: INT NULL. Referencia a movimiento contable de pasivo.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAccountPayable', @level2type = N'COLUMN', @level2name = N'AccountPayableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAccountPayable', @level2type = N'COLUMN', @level2name = N'AccountPayableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAccountPayable', @level2type = N'COLUMN', @level2name = N'AccountPayableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del centro de costos o unidad funcional que consume el gasto (FK → Payroll.CostCenter, nullable). Tipo: INT NULL. Ej: urgencias, farmacia, laboratorio.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAccountPayable', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del centro del costos', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAccountPayable', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAccountPayable', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta contable principal en el plan de cuentas (FK → GeneralLedger.MainAccounts). Tipo: INT NOT NULL. Ej: 2105 Cuentas por pagar.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAccountPayable', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAccountPayable', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAccountPayable', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Línea de distribución del proveedor que especifica cómo se asigna el gasto (FK → Common.SuppliersDistributionLines). Tipo: INT NOT NULL.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAccountPayable', @level2type = N'COLUMN', @level2name = N'SupplierDistributionLinesID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Linea de distribucion del proveedor', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAccountPayable', @level2type = N'COLUMN', @level2name = N'SupplierDistributionLinesID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAccountPayable', @level2type = N'COLUMN', @level2name = N'SupplierDistributionLinesID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tercero (persona natural, empresa, entidad) que actúa como proveedor (FK → Common.ThirdParty). Tipo: INT NOT NULL. PII: número de identificación del proveedor.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAccountPayable', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAccountPayable', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAccountPayable', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del proveedor o acreedor registrado en el sistema (FK → Common.Supplier). Tipo: INT NOT NULL. Búsqueda: proveedor, farmacéutica, laboratorio, centro de salud.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAccountPayable', @level2type = N'COLUMN', @level2name = N'SupplierId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del proveedor', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAccountPayable', @level2type = N'COLUMN', @level2name = N'SupplierId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAccountPayable', @level2type = N'COLUMN', @level2name = N'SupplierId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del registro de saldo inicial o apertura de periodo (FK → Payments.InitialBalance). Tipo: INT NOT NULL.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAccountPayable', @level2type = N'COLUMN', @level2name = N'InitialBalanceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del saldo inicial ', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAccountPayable', @level2type = N'COLUMN', @level2name = N'InitialBalanceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAccountPayable', @level2type = N'COLUMN', @level2name = N'InitialBalanceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (clave primaria, IDENTITY 1,1) del registro de saldo inicial por pagar. Tipo: INT IDENTITY NOT NULL. Búsqueda: cuenta, pasivo, obligación.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAccountPayable', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAccountPayable', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAccountPayable', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldos iniciales de cuentas por pagar a proveedores. Registra las facturas pendientes de pago que existían al momento de apertura contable, incluyendo valores, vencimientos y distribución contable por proveedor y tercero.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAccountPayable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAccountPayable';
