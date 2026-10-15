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
    [Quantity]                DECIMAL(5,2)   NOT NULL
    CONSTRAINT [PK_LiquidationDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Liquidation_LiquidationDetail] FOREIGN KEY ([PayrollId]) REFERENCES [Payroll].[Liquidation] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_LiquidationDetail_AgreementsC] FOREIGN KEY ([AgreementsId]) REFERENCES [Payroll].[AgreementsC] ([Id]),
    CONSTRAINT [FK_LiquidationDetail_AgreementsD] FOREIGN KEY ([AgreementsDId]) REFERENCES [Payroll].[AgreementsD] ([Id]),
    CONSTRAINT [FK_LiquidationDetail_RetentionConcept] FOREIGN KEY ([RetentionId]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id]),
    CONSTRAINT [FK_LiquidationDetail_ThirdParty] FOREIGN KEY ([IdThirdParty]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_PayrollDetail_PayrollConcept] FOREIGN KEY ([ConceptId]) REFERENCES [Payroll].[Concept] ([Id])
);

GO
ALTER TABLE [Payroll].[LiquidationDetail] ADD  DEFAULT ((0)) FOR [Quantity]


GO
ALTER TABLE [Payroll].[LiquidationDetail] NOCHECK CONSTRAINT [FK_Liquidation_LiquidationDetail];


GO
ALTER TABLE [Payroll].[LiquidationDetail] NOCHECK CONSTRAINT [FK_LiquidationDetail_AgreementsC];


GO
ALTER TABLE [Payroll].[LiquidationDetail] NOCHECK CONSTRAINT [FK_LiquidationDetail_AgreementsD];


GO
ALTER TABLE [Payroll].[LiquidationDetail] NOCHECK CONSTRAINT [FK_LiquidationDetail_RetentionConcept];


GO
ALTER TABLE [Payroll].[LiquidationDetail] NOCHECK CONSTRAINT [FK_LiquidationDetail_ThirdParty];


GO
ALTER TABLE [Payroll].[LiquidationDetail] NOCHECK CONSTRAINT [FK_PayrollDetail_PayrollConcept];


GO
CREATE NONCLUSTERED INDEX [IX_LiquidationDetail__ConceptId]
    ON [Payroll].[LiquidationDetail]([ConceptId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_LiquidationDetail__PayrollId]
    ON [Payroll].[LiquidationDetail]([PayrollId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad registrada', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Días de luto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'LutoDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Concepto de retencion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'RetentionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Artículo Usado en Retefuente: 0 - Ninguno, 1 - Artículo 383, 2 - Artículo 384', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'TypeArticleRTF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Tercero', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'IdThirdParty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número Total de Horas (Aplica para conceptos que se paguen con HORAS)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'TotalNumberHours';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Gasto de Incapacidad ', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'SpendingInability';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Incapacidad x Cobrar', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'InabilityCollect';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Distribuir Gastos', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'DistribuirGasto';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fórmula del Concepto Remplazada', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'ReplaceConceptFormulate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fórmula del Concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'ConceptFormulate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Detalle del Convenio (Se llena únicamente cuando la Nómina se Confirma)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'AgreementsDId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador para controlar los convenios con el mismo concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'AgreementsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Concepto: 1 Devengado - 2- Deducido  3 Patrono', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'ConceptType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Saldo Inicial', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'InitialBalance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje de Retención', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'RetentionPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Base de Retención', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'RetentionBase';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Total Deducido', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'DeductedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Total Devengado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'AccruedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Total del Concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'ConceptTotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Detalle del Concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'ConceptDetail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifcia el codigo del concepto ', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'ConceptCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'ConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clase del Concepto "Codigo dek concepto"', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'ConceptClass';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Periodo de Liquidacion: "1" Primera Quincena - "2" Segunda Quincena', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'LiquidationPeriod';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la Nómina', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'PayrollDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Registro (Campo Oculto)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'RegisterStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la Tabla Payroll FK', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'PayrollId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'Id';

