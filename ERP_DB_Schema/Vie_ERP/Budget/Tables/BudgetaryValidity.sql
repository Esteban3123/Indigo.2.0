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
ALTER TABLE [Budget].[BudgetaryValidity] NOCHECK CONSTRAINT [FK_BudgetaryValidity_Category];


GO
ALTER TABLE [Budget].[BudgetaryValidity] NOCHECK CONSTRAINT [FK_Validity_BudgetEntities];


GO
ALTER TABLE [Budget].[BudgetaryValidity] NOCHECK CONSTRAINT [FK_Validity_Validity_BudgetBoss];


GO
ALTER TABLE [Budget].[BudgetaryValidity] NOCHECK CONSTRAINT [FK_Validity_Validity_FinancialBoss];


GO
ALTER TABLE [Budget].[BudgetaryValidity] NOCHECK CONSTRAINT [FK_Validity_Validity_LegalRepresentative];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_BudgetaryValidity__Year__BudgetaryEntityId]
    ON [Budget].[BudgetaryValidity]([Year] ASC, [BudgetaryEntityId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_BudgetaryValidity__BudgetaryEntityId]
    ON [Budget].[BudgetaryValidity]([BudgetaryEntityId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de cierre', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ClosureDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de cierre', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ClosureUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de activacion de vigencia', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ActivateDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Que activo la vigencia', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ActivateUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca de cierre anual de gastos', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'AnnualClosureOfExpenses';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca de cierre anual de ingresos', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'AnnualClosureOfIncome';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo usado para las prorrogas de los documentos', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveDocumentExtension';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo usado para cuentas por cobrar', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveCXC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina si para la vigencia actual se habilita el control de fechas en documentos', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ControlDateConsecutive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo usado para los levantamientos a suspensiones del presupuesto de gastos', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveLiftingPTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo usado para las suspensiones del presupuesto de gastos', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveSuspensionPTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo usado para las obligaciones de vigencia futura', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveODPVFT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo usado para las obligaciones de vigencia futura', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveLiabilitiesVFT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo usado para los compromisos de vigencia futura', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveRPVFT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo usado para las disponibilidades de vigencia futura', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveCDPVFT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo usado para las cuentas por pagar', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveCXP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo usado para las reservas presupuestales', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveReservation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo usado para los reintegros', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveReinstatement';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo para la liberacion de recursos', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveResourceRelease';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo para las prorrogas de las disponibilidades', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveExtendedCDP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo Usado para las ordenes de pago', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveODP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo usado para las modificaciones de obligaciones', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveModLiabilities';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo usado para las obligaciones', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveLiabilities';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo usado para las modificaciones de los Compromisos / Reservas', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveModRP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo usado para los compromisos / Reservas', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveRP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo para las modificaciones a las disponibilidades', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveModCDP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo para las disponibilidades', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveCDP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo usado para el traslado de P.A.C de gastos', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveTrasPACExpense';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo usado para la modificacion del P.A.C de gastos', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveModPACExpense';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo usado para los traslados de presupuesto de gastos', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveTrasPTOExpense';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo usado para las modificaciones al presupuesto de gastos', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveModPTOExpense';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo usado para la modificacion de los recaudos', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveModCollection';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo usado para los recaudos', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveCollection';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo usado para las modificaciones de los reconocimientos', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveModRecognition';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo usado para los reconocimientos', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveRecognition';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo usado para los translados de PAC de Ingresos', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveTrasPACIncome';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo usado para las modificaciones de PAC de Ingresos', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveModPACIncome';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo usado para los translados de presupuestos de ingresos', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveTrasPTOIncome';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo usado para las modificaciones al presupuestos de ingresos', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ConsecutiveModPTOIncome';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Manejo o Control de P.A.C', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'PACControl';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de registro de gastos', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'DateExpensesRecord';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de registro de ingreso', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'DateIncomeRecord';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado Inicial Gastos 1 - Registrado 2 - Confirmado 3 - Anulado', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'InitialStateExpenses';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado Inicial de Ingresos 1 - Registrado 2 - Confirmado 3 - Anulado', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'InitialStateIncome';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Jefe financiero(tercero)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ChiefFinancialOfficerId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Jefe de presupuestos(tercero)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ChiefBudgetOfficerId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Representante legal(tercero)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'LegalRepresentativeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la resolucion', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ResolutionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Resolución/Acuerdo', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ResolutionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mes de Gastos de enero a diciembre con el consecutivo de 1 a 12', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'ExpenseMonth';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mes de Ingresos de enero a diciembre con los consecutivos del 1 al 12', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'IncomeMonth';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la validación: 1-Registrada, 2-Activa, 3-Cerrada', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del rubro para las cuentas por pagar de la vigencia anterior', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'CxPSingleCategoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si cuando se va a realizar el cierre anual las obligacion pasan como Cxp a la nueva vigencia a un solo rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'CxPCreateSingleCategory';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Entidad Presupuestaria', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'BudgetaryEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Año de la vigencia', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'Year';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetaryValidity', @level2type = N'COLUMN', @level2name = N'Id';

