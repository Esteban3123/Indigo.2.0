CREATE TABLE [Budget].[BudgetaryValidity] (
    [Id]                           INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Year]                         INT             NOT NULL,
    [BudgetaryEntityId]            INT             NOT NULL,
    [CxPCreateSingleCategory]      BIT             CONSTRAINT [DF_BudgetaryValidity_CxPCreateSingleCategory] DEFAULT ((0)) NOT NULL,
    [CxPSingleCategoryId]          INT             NULL,
    [Status]                       TINYINT         NOT NULL,
    [IncomeMonth]                  INT             NOT NULL,
    [ExpenseMonth]                 INT             NOT NULL,
    [ResolutionNumber]             VARCHAR (20)    NOT NULL,
    [ResolutionValue]              NUMERIC (18, 2) NOT NULL,
    [LegalRepresentativeId]        INT             NULL,
    [ChiefBudgetOfficerId]         INT             NOT NULL,
    [ChiefFinancialOfficerId]      INT             NOT NULL,
    [InitialStateIncome]           INT             NULL,
    [InitialStateExpenses]         INT             NULL,
    [DateIncomeRecord]             DATETIME        NULL,
    [DateExpensesRecord]           DATETIME        NULL,
    [PACControl]                   BIT             NOT NULL,
    [ConsecutiveModPTOIncome]      INT             NULL,
    [ConsecutiveTrasPTOIncome]     INT             NULL,
    [ConsecutiveModPACIncome]      INT             NULL,
    [ConsecutiveTrasPACIncome]     INT             NULL,
    [ConsecutiveRecognition]       INT             NULL,
    [ConsecutiveModRecognition]    INT             NULL,
    [ConsecutiveCollection]        INT             NULL,
    [ConsecutiveModCollection]     INT             NULL,
    [ConsecutiveModPTOExpense]     INT             NULL,
    [ConsecutiveTrasPTOExpense]    INT             NULL,
    [ConsecutiveModPACExpense]     INT             NULL,
    [ConsecutiveTrasPACExpense]    INT             NULL,
    [ConsecutiveCDP]               INT             NULL,
    [ConsecutiveModCDP]            INT             NULL,
    [ConsecutiveRP]                INT             NULL,
    [ConsecutiveModRP]             INT             NULL,
    [ConsecutiveLiabilities]       INT             NULL,
    [ConsecutiveModLiabilities]    INT             NULL,
    [ConsecutiveODP]               INT             NULL,
    [ConsecutiveExtendedCDP]       INT             NULL,
    [ConsecutiveResourceRelease]   INT             NULL,
    [ConsecutiveReinstatement]     INT             NULL,
    [ConsecutiveReservation]       INT             NULL,
    [ConsecutiveCXP]               INT             NULL,
    [ConsecutiveCDPVFT]            INT             NULL,
    [ConsecutiveRPVFT]             INT             NULL,
    [ConsecutiveLiabilitiesVFT]    INT             NULL,
    [ConsecutiveODPVFT]            INT             NULL,
    [ConsecutiveSuspensionPTO]     INT             NULL,
    [ConsecutiveLiftingPTO]        INT             NULL,
    [ControlDateConsecutive]       BIT             NULL,
    [ConsecutiveCXC]               INT             NULL,
    [ConsecutiveDocumentExtension] INT             NULL,
    [AnnualClosureOfIncome]        BIT             NULL,
    [AnnualClosureOfExpenses]      BIT             NULL,
    [CreationUser]                 VARCHAR (20)    CONSTRAINT [DF_BudgetaryValidity_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]                 DATETIME        CONSTRAINT [DF_BudgetaryValidity_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]             VARCHAR (20)    NULL,
    [ModificationDate]             DATETIME        NULL,
    [ActivateUser]                 VARCHAR (20)    NULL,
    [ActivateDate]                 DATETIME        NULL,
    [ClosureUser]                  VARCHAR (20)    NULL,
    [ClosureDate]                  DATETIME        NULL,
    [TimeStamp]                    ROWVERSION      NOT NULL,
    CONSTRAINT [PK_BudgetaryValidity__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BudgetaryValidity_Category] FOREIGN KEY ([CxPSingleCategoryId]) REFERENCES [Budget].[Category] ([Id]),
    CONSTRAINT [FK_Validity_BudgetEntities] FOREIGN KEY ([BudgetaryEntityId]) REFERENCES [Budget].[BudgetaryEntity] ([Id]),
    CONSTRAINT [FK_Validity_Validity_BudgetBoss] FOREIGN KEY ([ChiefBudgetOfficerId]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_Validity_Validity_FinancialBoss] FOREIGN KEY ([ChiefFinancialOfficerId]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_Validity_Validity_LegalRepresentative] FOREIGN KEY ([LegalRepresentativeId]) REFERENCES [Common].[ThirdParty] ([Id])
);




GO



GO



GO



GO



GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_BudgetaryValidity__Year__BudgetaryEntityId]
    ON [Budget].[BudgetaryValidity]([Year] ASC, [BudgetaryEntityId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_BudgetaryValidity__BudgetaryEntityId]
    ON [Budget].[BudgetaryValidity]([BudgetaryEntityId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal TIMESTAMP (SQL Server) que registra el instante exacto de creación, modificación, activación o cierre de la vigencia presupuestaria; utilizada para auditoría y control de cambios.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha DATETIME del cierre definitivo de la vigencia presupuestaria; marca cuándo se finalizó la gestión presupuestal del año.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ClosureDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de cierre', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ClosureDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ClosureDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario VARCHAR(20) que ejecutó el cierre de la vigencia presupuestaria; identificador de quien clausuró los ejercicios de ingresos y gastos.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ClosureUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de cierre', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ClosureUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ClosureUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha DATETIME de activación o puesta en vigencia del presupuesto; momento en que entra en operación la validación presupuestaria del año.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ActivateDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de activacion de vigencia', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ActivateDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ActivateDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario VARCHAR(20) que activó la vigencia presupuestaria; identificador de quien autorizó el inicio de la ejecución presupuestal.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ActivateUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Que activo la vigencia', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ActivateUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ActivateUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha DATETIME de la última modificación realizada a la vigencia presupuestaria; registra cuándo se ajustaron los parámetros o consecutivos.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario VARCHAR(20) que realizó la última modificación; identificador de quien cambió la configuración de la vigencia.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha DATETIME de creación de la vigencia presupuestaria; momento en que se registró por primera vez en el sistema.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario VARCHAR(20) que creó la vigencia presupuestaria; identificador del autor del registro inicial.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT (0=No, 1=Sí) que marca si se realizó el cierre anual de gastos; controla si los compromisos y obligaciones se cerraron al final del período.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'AnnualClosureOfExpenses';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca de cierre anual de gastos', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'AnnualClosureOfExpenses';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'AnnualClosureOfExpenses';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT (0=No, 1=Sí) que marca si se realizó el cierre anual de ingresos; controla si los recaudos y reconocimientos se cerraron al final del período.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'AnnualClosureOfIncome';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca de cierre anual de ingresos', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'AnnualClosureOfIncome';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'AnnualClosureOfIncome';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contador INT para las prórrogas o extensiones de documentos presupuestarios; número secuencial de ampliaciones de plazo otorgadas en la vigencia.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveDocumentExtension';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo usado para las prorrogas de los documentos', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveDocumentExtension';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveDocumentExtension';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contador INT secuencial para Cuentas por Cobrar (CXC); rastrea el número de deudores y facturas pendientes de cobro en la vigencia.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveCXC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo usado para cuentas por cobrar', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveCXC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveCXC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT (0=No, 1=Sí) que habilita o deshabilita el control de fechas en documentos presupuestarios; determina si se validan restricciones temporales.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ControlDateConsecutive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Determina si para la vigencia actual se habilita el control de fechas en documentos', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ControlDateConsecutive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ControlDateConsecutive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contador INT para levantamientos de suspensiones del Presupuesto de Trasferencias de Operación (PTO); registra reactivaciones del presupuesto de gastos.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveLiftingPTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo usado para los levantamientos a suspensiones del presupuesto de gastos', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveLiftingPTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveLiftingPTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contador INT para suspensiones del Presupuesto de Trasferencias de Operación (PTO); número de congelaciones del presupuesto de gastos autorizadas.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveSuspensionPTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo usado para las suspensiones del presupuesto de gastos', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveSuspensionPTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveSuspensionPTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contador INT para Órdenes de Pago (ODP) de Vigencia Futura; rastrea obligaciones que se ejecutarán en vigencias posteriores.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveODPVFT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo usado para las obligaciones de vigencia futura', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveODPVFT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveODPVFT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contador INT para Obligaciones de Vigencia Futura (VFT); número de compromisos adquiridos que se pagarán en años siguientes.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveLiabilitiesVFT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo usado para las obligaciones de vigencia futura', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveLiabilitiesVFT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveLiabilitiesVFT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contador INT para Reservas Presupuestales (RP) de Vigencia Futura; rastrea compromisos y reservas que trascienden el año fiscal.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveRPVFT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo usado para los compromisos de vigencia futura', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveRPVFT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveRPVFT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contador INT para Certificados de Disponibilidad Presupuestaria (CDP) de Vigencia Futura; registra disponibilidades comprometidas para ejercicios siguientes.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveCDPVFT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo usado para las disponibilidades de vigencia futura', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveCDPVFT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveCDPVFT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contador INT secuencial para Cuentas por Pagar (CXP); número de obligaciones y acreedores registrados en la vigencia presupuestaria.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveCXP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo usado para las cuentas por pagar', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveCXP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveCXP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contador INT para Reservas Presupuestales; rastrea compromisos o reservas de recursos presupuestarios autorizados en la vigencia.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveReservation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo usado para las reservas presupuestales', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveReservation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveReservation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contador INT para Reintegros o Devoluciones de Recursos; número de recursos devueltos o reincorporados al presupuesto.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveReinstatement';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo usado para los reintegros', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveReinstatement';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveReinstatement';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contador INT para Liberación de Recursos Presupuestarios; rastrea fondos descongelados o liberados para ejecución.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveResourceRelease';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo para la liberacion de recursos', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveResourceRelease';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveResourceRelease';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contador INT para Prórrogas de Certificados de Disponibilidad (CDP); número de ampliaciones de plazo otorgadas a disponibilidades.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveExtendedCDP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo para las prorrogas de las disponibilidades', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveExtendedCDP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveExtendedCDP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contador INT para Órdenes de Pago (ODP); número secuencial de órdenes de desembolso expedidas en la vigencia.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveODP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo Usado para las ordenes de pago', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveODP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveODP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contador INT para Modificaciones de Obligaciones; rastrea ajustes, ampliaciones o reducciones de compromisos contraídos.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveModLiabilities';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo usado para las modificaciones de obligaciones', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveModLiabilities';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveModLiabilities';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contador INT para Obligaciones; número secuencial de compromisos presupuestarios registrados en la vigencia.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveLiabilities';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo usado para las obligaciones', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveLiabilities';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveLiabilities';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contador INT para Modificaciones de Reservas Presupuestales (RP); rastrea ajustes a compromisos y reservas autorizadas.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveModRP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo usado para las modificaciones de los Compromisos / Reservas', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveModRP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveModRP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contador INT para Reservas Presupuestales (RP); número de compromisos y reservas de recursos presupuestarios.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveRP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo usado para los compromisos / Reservas', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveRP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveRP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contador INT para Modificaciones de Certificados de Disponibilidad (CDP); rastrea ajustes a disponibilidades presupuestarias.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveModCDP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo para las modificaciones a las disponibilidades', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveModCDP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveModCDP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contador INT para Certificados de Disponibilidad Presupuestaria (CDP); número de disponibilidades expedidas para obligaciones.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveCDP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo para las disponibilidades', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveCDP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveCDP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contador INT para Traslados de Presupuesto de Ajustes al Código (PAC) de Gastos; rastrea movimientos de presupuesto de gastos entre rubros.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveTrasPACExpense';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo usado para el traslado de P.A.C de gastos', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveTrasPACExpense';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveTrasPACExpense';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contador INT para Modificaciones de PAC de Gastos; registra ajustes al presupuesto de gasto mediante modificaciones presupuestarias.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveModPACExpense';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo usado para la modificacion del P.A.C de gastos', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveModPACExpense';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveModPACExpense';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contador INT para Traslados de Presupuesto de Trasferencias de Operación (PTO) de Gastos; rastrea reasignaciones de presupuesto de gasto.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveTrasPTOExpense';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo usado para los traslados de presupuesto de gastos', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveTrasPTOExpense';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveTrasPTOExpense';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contador INT para Modificaciones de PTO de Gastos; registra ampliaciones, reducciones o ajustes del presupuesto de gastos.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveModPTOExpense';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo usado para las modificaciones al presupuesto de gastos', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveModPTOExpense';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveModPTOExpense';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contador INT para Modificaciones de Recaudos o Ingresos; rastrea ajustes a los ingresos efectivamente cobrados.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveModCollection';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo usado para la modificacion de los recaudos', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveModCollection';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveModCollection';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contador INT para Recaudos o Ingresos Efectivamente Cobrados; número de registros de ingreso realizado en la vigencia.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveCollection';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo usado para los recaudos', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveCollection';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveCollection';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contador INT para Modificaciones de Reconocimientos; rastrea ajustes a obligaciones reconocidas por la entidad.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveModRecognition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo usado para las modificaciones de los reconocimientos', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveModRecognition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveModRecognition';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contador INT para Reconocimientos de Obligaciones; número de pasivos u obligaciones reconocidas en la vigencia.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveRecognition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo usado para los reconocimientos', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveRecognition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveRecognition';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contador INT para Traslados de PAC de Ingresos; rastrea movimientos de presupuesto de ingresos entre rubros.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveTrasPACIncome';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo usado para los translados de PAC de Ingresos', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveTrasPACIncome';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveTrasPACIncome';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contador INT para Modificaciones de PAC de Ingresos; registra ajustes al presupuesto de ingreso mediante modificaciones presupuestarias.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveModPACIncome';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo usado para las modificaciones de PAC de Ingresos', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveModPACIncome';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveModPACIncome';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contador INT para Traslados de PTO de Ingresos; rastrea reasignaciones de presupuesto de ingreso entre conceptos.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveTrasPTOIncome';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo usado para los translados de presupuestos de ingresos', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveTrasPTOIncome';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveTrasPTOIncome';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contador INT para Modificaciones de PTO de Ingresos; registra ampliaciones, reducciones o ajustes del presupuesto de ingresos.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveModPTOIncome';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo usado para las modificaciones al presupuestos de ingresos', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveModPTOIncome';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveModPTOIncome';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT (0=No, 1=Sí) que habilita el manejo y control de Presupuesto de Ajustes al Código (PAC); determina si se permite modificar presupuesto por PAC.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'PACControl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Manejo o Control de P.A.C', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'PACControl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'PACControl';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha DATETIME de registro de los gastos iniciales; momento en que se registra el estado inicial de los gastos presupuestarios.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'DateExpensesRecord';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de registro de gastos', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'DateExpensesRecord';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'DateExpensesRecord';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha DATETIME de registro de los ingresos iniciales; momento en que se registra el estado inicial de los ingresos presupuestarios.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'DateIncomeRecord';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de registro de ingreso', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'DateIncomeRecord';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'DateIncomeRecord';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código TINYINT del estado inicial de gastos: 1=Registrado, 2=Confirmado, 3=Anulado; situación de la disponibilidad presupuestal al inicio.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'InitialStateExpenses';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado Inicial Gastos 1 - Registrado 2 - Confirmado 3 - Anulado', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'InitialStateExpenses';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'InitialStateExpenses';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código TINYINT del estado inicial de ingresos: 1=Registrado, 2=Confirmado, 3=Anulado; situación del ingreso presupuestado al inicio.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'InitialStateIncome';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado Inicial de Ingresos 1 - Registrado 2 - Confirmado 3 - Anulado', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'InitialStateIncome';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'InitialStateIncome';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador INT del Jefe Financiero u oficial de hacienda (FK→Common.ThirdParty); responsable de la gestión financiera de la vigencia.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ChiefFinancialOfficerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Jefe financiero(tercero)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ChiefFinancialOfficerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ChiefFinancialOfficerId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador INT del Jefe de Presupuestos (FK→Common.ThirdParty); responsable de la ejecución y control presupuestario.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ChiefBudgetOfficerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Jefe de presupuestos(tercero)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ChiefBudgetOfficerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ChiefBudgetOfficerId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador INT del Representante Legal (FK→Common.ThirdParty); autoriza resoluciones y actos administrativos presupuestarios.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'LegalRepresentativeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Representante legal(tercero)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'LegalRepresentativeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'LegalRepresentativeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto NUMERIC(18,2) aprobado en la resolución presupuestaria; valor total del presupuesto autorizado para la vigencia.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ResolutionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la resolucion', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ResolutionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ResolutionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número VARCHAR(20) de la Resolución o Acuerdo presupuestario; referencia legal que autoriza la vigencia presupuestaria.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ResolutionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Resolución/Acuerdo', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ResolutionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ResolutionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mes INT de cierre de gastos (1-12 de enero a diciembre); período hasta el cual se ejecutan gastos en la vigencia.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ExpenseMonth';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mes de Gastos de enero a diciembre con el consecutivo de 1 a 12', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ExpenseMonth';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ExpenseMonth';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mes INT de cierre de ingresos (1-12 de enero a diciembre); período hasta el cual se registran ingresos en la vigencia.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'IncomeMonth';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mes de Ingresos de enero a diciembre con los consecutivos del 1 al 12', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'IncomeMonth';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'IncomeMonth';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado TINYINT de la vigencia presupuestaria: 1=Registrada, 2=Activa, 3=Cerrada; fase del ciclo presupuestario en que se encuentra.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la validación: 1-Registrada, 2-Activa, 3-Cerrada', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador INT del Rubro Único (FK→Budget.Category) para Cuentas por Pagar de vigencia anterior; agrupa CXP al cierre anual.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'CxPSingleCategoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del rubro para las cuentas por pagar de la vigencia anterior', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'CxPSingleCategoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'CxPSingleCategoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT (0=No, 1=Sí) que especifica si las obligaciones cerrarse consolidarán en un solo rubro de CXP para la vigencia siguiente.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'CxPCreateSingleCategory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si cuando se va a realizar el cierre anual las obligacion pasan como Cxp a la nueva vigencia a un solo rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'CxPCreateSingleCategory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'CxPCreateSingleCategory';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador INT de la Entidad Presupuestaria (FK→Budget.BudgetaryEntity); entidad u organismo cuya vigencia presupuestaria se registra.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'BudgetaryEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Entidad Presupuestaria', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'BudgetaryEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'BudgetaryEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Año INT de la vigencia presupuestaria; período fiscal anual (ej: 2024) para el cual se configura el presupuesto.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'Year';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Año de la vigencia', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'Year';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'Year';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador INT IDENTITY autonumérico de la vigencia presupuestaria; clave primaria única de cada registro de validación presupuestal.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vigencia presupuestal de una entidad: agrupa la configuración anual del presupuesto público (ingresos y gastos) por entidad presupuestal, incluyendo la resolución que la legaliza, los funcionarios responsables, el control del PAC (Plan Anual de Caja) y los consecutivos de todos los documentos presupuestales (CDP, RP, ODP, CXP, CXC, traslados, modificaciones, reservas, reconocimientos, etc.).', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity';
