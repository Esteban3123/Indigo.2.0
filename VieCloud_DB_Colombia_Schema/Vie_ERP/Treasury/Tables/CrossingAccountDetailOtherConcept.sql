CREATE TABLE [Treasury].[CrossingAccountDetailOtherConcept] (
    [Id]                    INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CrossingAccountId]     INT             NOT NULL,
    [TreasuryNoteConceptId] INT             NOT NULL,
    [MainAccountId]         INT             NOT NULL,
    [ThirdPartyId]          INT             NULL,
    [CostCenterId]          INT             NULL,
    [Nature]                TINYINT         NOT NULL,
    [Value]                 DECIMAL (18, 2) NOT NULL,
    [Detail]                VARCHAR (MAX)   CONSTRAINT [DF_CrossingAccountDetailOtherConcept_Detail] DEFAULT ('') NOT NULL,
    [IdCashFlowConcept]     INT             NULL,
    CONSTRAINT [PK_CrossingAccountDetailOtherConcept] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CrossingAccountDetailOtherConcept_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_CrossingAccountDetailOtherConcept_CrossingAccount] FOREIGN KEY ([CrossingAccountId]) REFERENCES [Treasury].[CrossingAccount] ([Id]),
    CONSTRAINT [FK_CrossingAccountDetailOtherConcept_IdCashFlowConcept] FOREIGN KEY ([IdCashFlowConcept]) REFERENCES [Treasury].[CashFlowConcept] ([Id]),
    CONSTRAINT [FK_CrossingAccountDetailOtherConcept_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_CrossingAccountDetailOtherConcept_NoteConcepts] FOREIGN KEY ([TreasuryNoteConceptId]) REFERENCES [Treasury].[NoteConcepts] ([Id]),
    CONSTRAINT [FK_CrossingAccountDetailOtherConcept_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);




GO



GO



GO



GO



GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_CrossingAccountDetailOtherConcept_IdCashFlowConcept]
    ON [Treasury].[CrossingAccountDetailOtherConcept]([IdCashFlowConcept] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del concepto de flujo de efectivo (cash flow), vinculado a Treasury.CashFlowConcept para clasificar movimientos de tesorería', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailOtherConcept', @level2type = N'COLUMN', @level2name = N'IdCashFlowConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de concepto de flujo de efectivo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailOtherConcept', @level2type = N'COLUMN', @level2name = N'IdCashFlowConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailOtherConcept', @level2type = N'COLUMN', @level2name = N'IdCashFlowConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual detallada del concepto adicional de cruzamiento contable, ampliación de información sobre el movimiento de tesorería', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailOtherConcept', @level2type = N'COLUMN', @level2name = N'Detail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Detalle', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailOtherConcept', @level2type = N'COLUMN', @level2name = N'Detail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailOtherConcept', @level2type = N'COLUMN', @level2name = N'Detail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Importe decimal (18,2) del movimiento: monto en pesos, depende de Nature (débito/crédito) para determinar signo contable', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailOtherConcept', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailOtherConcept', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailOtherConcept', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Naturaleza del movimiento: 1=Débito, 2=Crédito; determina si incrementa o disminuye la cuenta principal en la nota de tesorería', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailOtherConcept', @level2type = N'COLUMN', @level2name = N'Nature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Naturaleza de la Nota 1. Debito 2. Credito', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailOtherConcept', @level2type = N'COLUMN', @level2name = N'Nature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailOtherConcept', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del centro de costos (unidad funcional de costo) asociado, vinculado a Payroll.CostCenter para imputación presupuestal', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailOtherConcept', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id centro de costos', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailOtherConcept', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailOtherConcept', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tercero (proveedor, acreedor, deudor) involucrado en el movimiento, referencia a Common.ThirdParty', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailOtherConcept', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id terceros', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailOtherConcept', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailOtherConcept', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta contable principal (activo, pasivo, patrimonio) según plan de cuentas del mayor general', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailOtherConcept', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de cuenta principal', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailOtherConcept', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailOtherConcept', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del concepto de nota de tesorería, vinculado a Treasury.NoteConcepts para clasificación del movimiento', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailOtherConcept', @level2type = N'COLUMN', @level2name = N'TreasuryNoteConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del concepto de nota del Tesoro', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailOtherConcept', @level2type = N'COLUMN', @level2name = N'TreasuryNoteConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailOtherConcept', @level2type = N'COLUMN', @level2name = N'TreasuryNoteConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del cruzamiento contable padre, referencia a Treasury.CrossingAccount para agrupar detalles del asiento', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailOtherConcept', @level2type = N'COLUMN', @level2name = N'CrossingAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de cuenta cruzada', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailOtherConcept', @level2type = N'COLUMN', @level2name = N'CrossingAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailOtherConcept', @level2type = N'COLUMN', @level2name = N'CrossingAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave primaria única (INT IDENTITY) del detalle de cruzamiento contable con concepto adicional en tesorería', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailOtherConcept', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador único del registro.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailOtherConcept', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailOtherConcept', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de conceptos adicionales o varios en el cruce de cuentas de tesorería. Registra los movimientos contables complementarios asociados a un cruce de cuenta, incluyendo la cuenta principal, tercero, centro de costo, naturaleza del movimiento (débito/crédito), valor y concepto de flujo de caja.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailOtherConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CrossingAccountDetailOtherConcept';
