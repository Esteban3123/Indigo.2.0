CREATE TABLE [Payroll].[LiquidationDetail] (
    [Id]                      INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PayrollId]               INT            NOT NULL,
    [RegisterStatus]          INT            NOT NULL,
    [PayrollDate]             DATE           NOT NULL,
    [LiquidationPeriod]       CHAR (1)       NOT NULL,
    [ConceptClass]            CHAR (3)       NOT NULL,
    [ConceptId]               INT            NOT NULL,
    [ConceptCode]             VARCHAR (4)    NULL,
    [ConceptDetail]           VARCHAR (300)  NULL,
    [ConceptTotalValue]       NUMERIC (18)   NOT NULL,
    [AccruedValue]            NUMERIC (18)   NULL,
    [DeductedValue]           NUMERIC (18)   NULL,
    [RetentionBase]           DECIMAL (18)   NULL,
    [RetentionPercentage]     DECIMAL (6, 3) NULL,
    [InitialBalance]          NUMERIC (18)   NULL,
    [ConceptType]             CHAR (1)       NOT NULL,
    [AgreementsId]            INT            NULL,
    [AgreementsDId]           INT            NULL,
    [ConceptFormulate]        VARCHAR (MAX)  NULL,
    [ReplaceConceptFormulate] VARCHAR (MAX)  NULL,
    [DistribuirGasto]         BIT            NULL,
    [InabilityCollect]        NUMERIC (18)   NULL,
    [SpendingInability]       NUMERIC (18)   NULL,
    [TotalNumberHours]        INT            CONSTRAINT [DF_LiquidationDetail_TotalNumberHours] DEFAULT ((0)) NULL,
    [IdThirdParty]            INT            NULL,
    [TypeArticleRTF]          TINYINT        NULL,
    [RetentionId]             INT            NULL,
    [ErpDays]                 INT            NULL,
    [EmployeerDays]           INT            NULL,
    [LutoDays]                INT            NULL,
    [Quantity]                DECIMAL (5, 2) DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_LiquidationDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Liquidation_LiquidationDetail] FOREIGN KEY ([PayrollId]) REFERENCES [Payroll].[Liquidation] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_LiquidationDetail_AgreementsC] FOREIGN KEY ([AgreementsId]) REFERENCES [Payroll].[AgreementsC] ([Id]),
    CONSTRAINT [FK_LiquidationDetail_AgreementsD] FOREIGN KEY ([AgreementsDId]) REFERENCES [Payroll].[AgreementsD] ([Id]),
    CONSTRAINT [FK_LiquidationDetail_RetentionConcept] FOREIGN KEY ([RetentionId]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id]),
    CONSTRAINT [FK_LiquidationDetail_ThirdParty] FOREIGN KEY ([IdThirdParty]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_PayrollDetail_PayrollConcept] FOREIGN KEY ([ConceptId]) REFERENCES [Payroll].[Concept] ([Id])
);


GO
ALTER TABLE [Payroll].[LiquidationDetail] NOCHECK CONSTRAINT [FK_Liquidation_LiquidationDetail];


GO
ALTER TABLE [Payroll].[LiquidationDetail] NOCHECK CONSTRAINT [FK_LiquidationDetail_AgreementsD];




GO
ALTER TABLE [Payroll].[LiquidationDetail] NOCHECK CONSTRAINT [FK_Liquidation_LiquidationDetail];


GO



GO
ALTER TABLE [Payroll].[LiquidationDetail] NOCHECK CONSTRAINT [FK_LiquidationDetail_AgreementsD];


GO



GO



GO




GO



GO
ALTER TABLE [Payroll].[LiquidationDetail] NOCHECK CONSTRAINT [FK_Liquidation_LiquidationDetail];


GO



GO
ALTER TABLE [Payroll].[LiquidationDetail] NOCHECK CONSTRAINT [FK_LiquidationDetail_AgreementsD];


GO



GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_LiquidationDetail__ConceptId]
    ON [Payroll].[LiquidationDetail]([ConceptId] ASC);


GO
ALTER INDEX [IX_LiquidationDetail__ConceptId]
    ON [Payroll].[LiquidationDetail] DISABLE;




GO
CREATE NONCLUSTERED INDEX [IX_LiquidationDetail__PayrollId]
    ON [Payroll].[LiquidationDetail]([PayrollId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad registrada (DECIMAL 5,2); unidades, horas, porcentaje u otra métrica cuantificable del concepto (default=0)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad registrada', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Días de luto (INT); cantidad de días pagados por fallecimiento familiar; descuento de afiliación según ley', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'LutoDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Días de luto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'LutoDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'LutoDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea al concepto de retención (FK GeneralLedger.RetentionConcepts); vincula tipo de retención legal/fiscal', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'RetentionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concepto de retencion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'RetentionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'RetentionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de artículo Retefuente (TINYINT): 0=Ninguno, 1=Artículo 383, 2=Artículo 384; discrimina norma fiscal aplicable', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'TypeArticleRTF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Artículo Usado en Retefuente: 0 - Ninguno, 1 - Artículo 383, 2 - Artículo 384', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'TypeArticleRTF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'TypeArticleRTF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea al tercero (FK Common.ThirdParty); identifica aportante externo (EPS, pensión, ARL) asociado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'IdThirdParty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Tercero', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'IdThirdParty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'IdThirdParty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número total de horas (INT); cantidad de horas trabajadas o liquidadas; aplica para conceptos pagados por hora', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'TotalNumberHours';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número Total de Horas (Aplica para conceptos que se paguen con HORAS)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'TotalNumberHours';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'TotalNumberHours';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Gasto de incapacidad (NUMERIC 18); valor gastado por concepto de incapacidad (EPS, ARL) en el período', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'SpendingInability';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Gasto de Incapacidad ', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'SpendingInability';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'SpendingInability';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Incapacidad por cobrar (NUMERIC 18); valor de incapacidad generado pero aún no pagado al empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'InabilityCollect';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Incapacidad x Cobrar', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'InabilityCollect';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'InabilityCollect';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Distribuir gastos (BIT); indica si el valor debe repartirse entre centros de costo o unidades funcionales', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'DistribuirGasto';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Distribuir Gastos', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'DistribuirGasto';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'DistribuirGasto';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fórmula del concepto reemplazada (VARCHAR MAX); versión alternativa o ajustada de la fórmula original', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'ReplaceConceptFormulate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fórmula del Concepto Remplazada', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'ReplaceConceptFormulate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'ReplaceConceptFormulate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fórmula del concepto (VARCHAR MAX); expresión matemática que define cálculo dinámico del concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'ConceptFormulate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fórmula del Concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'ConceptFormulate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'ConceptFormulate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea al detalle del convenio (FK Payroll.AgreementsD); se completa solo cuando la nómina es confirmada', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'AgreementsDId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Detalle del Convenio (Se llena únicamente cuando la Nómina se Confirma)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'AgreementsDId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'AgreementsDId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de acuerdo o convenio (FK Payroll.AgreementsC); controla conceptos vinculados a pactos colectivos', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'AgreementsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador para controlar los convenios con el mismo concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'AgreementsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'AgreementsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de concepto: ''''1''''=Devengado (ingreso), ''''2''''=Deducido (descuento), ''''3''''=Patronal (aporte empleador); clasifica naturaleza', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'ConceptType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Concepto: 1 Devengado - 2- Deducido  3 Patrono', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'ConceptType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'ConceptType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo inicial (NUMERIC 18); valor llevado del período anterior para conceptos de acumulación o provisión', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'InitialBalance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Saldo Inicial', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'InitialBalance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'InitialBalance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de retención (DECIMAL 6,3); tasa aplicada a la base para calcular retención fiscal o legal', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'RetentionPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje de Retención', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'RetentionPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'RetentionPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Base de retención (DECIMAL 18); valor sobre el cual se calcula la retención (Reteiva, Reteica, Retefuente, etc.)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'RetentionBase';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Base de Retención', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'RetentionBase';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'RetentionBase';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total deducido (NUMERIC 18); valor restado por conceptos de descuento, afiliaciones o deducciones autorizadas', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'DeductedValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Total Deducido', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'DeductedValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'DeductedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total devengado (NUMERIC 18); valor acumulado del concepto que genera derecho económico al empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'AccruedValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Total Devengado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'AccruedValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'AccruedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario total del concepto (NUMERIC 18); base antes de distribuciones o retenciones aplicables', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'ConceptTotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Total del Concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'ConceptTotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'ConceptTotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción ampliada del concepto (VARCHAR 300); detalle narrativo del rubro devengado, deducido o retención', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'ConceptDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Detalle del Concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'ConceptDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'ConceptDetail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código alfanumérico del concepto (VARCHAR 4); identificador secundario para búsqueda y reportes', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'ConceptCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifcia el codigo del concepto ', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'ConceptCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'ConceptCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea que referencia Payroll.Concept; vincula al catálogo maestro de conceptos salariales', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'ConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'ConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'ConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación de código del concepto (CHAR 3); agrupa conceptos por naturaleza contable y fiscal', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'ConceptClass';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clase del Concepto "Codigo dek concepto"', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'ConceptClass';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'ConceptClass';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Período de liquidación: ''''1''''=Primera Quincena, ''''2''''=Segunda Quincena; define rango de días acumulados', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'LiquidationPeriod';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Periodo de Liquidacion: "1" Primera Quincena - "2" Segunda Quincena', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'LiquidationPeriod';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'LiquidationPeriod';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de la nómina (DATE); base para cálculo de períodos, retenciones y validaciones de vigencia', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'PayrollDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la Nómina', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'PayrollDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'PayrollDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado interno del registro (campo oculto en UI); controla validación y procesamiento del detalle', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'RegisterStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del Registro (Campo Oculto)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'RegisterStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'RegisterStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) que referencia el identificador de la nómina padre en tabla Payroll.Liquidation; relación 1:N', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'PayrollId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Tabla Payroll FK', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'PayrollId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'PayrollId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (INT IDENTITY) de cada línea de detalle en la liquidación de nómina', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de conceptos liquidados en cada nómina: devengados, deducciones, retenciones y valores por concepto de pago para cada empleado en un período de liquidación.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Días calculados por el sistema ERP para la liquidación del concepto (días computados automáticamente según reglas del sistema).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'ErpDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'ErpDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Días reconocidos o reportados por el empleador para la liquidación del concepto de nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'EmployeerDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'EmployeerDays';
