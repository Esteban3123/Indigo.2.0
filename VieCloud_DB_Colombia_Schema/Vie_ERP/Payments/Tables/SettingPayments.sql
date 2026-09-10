CREATE TABLE [Payments].[SettingPayments] (
    [Id]                             INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdJournalVoucherAccountPayable] INT           NOT NULL,
    [IdJournalVoucherTranslation]    INT           NOT NULL,
    [IdJournalVoucherCreditNotes]    INT           NOT NULL,
    [IdJournalVoucherDebitNotes]     INT           NOT NULL,
    [IdJournalVocuherAmortization]   INT           CONSTRAINT [DF_SettingPayments_IdJournalVocuherAmortization] DEFAULT ((7)) NOT NULL,
    [ObligationDebitValue]           BIT           NOT NULL,
    [BudgetInterface]                BIT           CONSTRAINT [DF_SettingPayments_BudgetInterface] DEFAULT ((0)) NOT NULL,
    [OnlyObligation]                 BIT           NOT NULL,
    [ObligationBudgetInterface]      BIT           NOT NULL,
    [IdOperatingUnit]                INT           NOT NULL,
    [NameMinimumAgeRange]            VARCHAR (100) NOT NULL,
    [NameMaximumAgeRange]            VARCHAR (100) NOT NULL,
    [MaximunAgeRange]                INT           NOT NULL,
    [State]                          BIT           NOT NULL,
    [CreationUser]                   VARCHAR (20)  NOT NULL,
    [CreationDate]                   DATETIME      NOT NULL,
    [ModificationUser]               VARCHAR (20)  NULL,
    [ModificationDate]               DATETIME      NULL,
    [TimeStamp]                      ROWVERSION    NOT NULL,
    [PostulateBudgetInterfaceBy]     TINYINT       CONSTRAINT [DF_SettingPayments_PostulateBudgetInterfaceBy] DEFAULT ((1)) NOT NULL,
    [AutomaticConfirmation]          BIT           CONSTRAINT [DF_SettingPayments_AutomaticConfirmation] DEFAULT ((0)) NOT NULL,
    [IdAccountingFactoring]          INT           NULL,
    [IdCashFlowConcept]              INT           NULL,
    [IdCashFlowConceptExpense]       INT           NULL,
    CONSTRAINT [PK_ParametersPayment] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ParametersPayment_JournalVoucher] FOREIGN KEY ([IdJournalVoucherAccountPayable]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_ParametersPayment_JournalVoucher1] FOREIGN KEY ([IdJournalVoucherTranslation]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_ParametersPayment_JournalVoucher2] FOREIGN KEY ([IdJournalVoucherCreditNotes]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_ParametersPayment_JournalVoucher3] FOREIGN KEY ([IdJournalVoucherDebitNotes]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingPayments_CashFlowConcept] FOREIGN KEY ([IdCashFlowConcept]) REFERENCES [Treasury].[CashFlowConcept] ([Id]),
    CONSTRAINT [FK_SettingPayments_CashFlowConceptExpense] FOREIGN KEY ([IdCashFlowConceptExpense]) REFERENCES [Treasury].[CashFlowConcept] ([Id]),
    CONSTRAINT [FK_SettingPayments_JournalVoucherTypes] FOREIGN KEY ([IdAccountingFactoring]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingPayments_JournalVoucherTypes_Amortization] FOREIGN KEY ([IdJournalVocuherAmortization]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingPayments_OperatingUnit] FOREIGN KEY ([IdOperatingUnit]) REFERENCES [Common].[OperatingUnit] ([Id])
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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT, FK a Treasury.CashFlowConcept. Identificador del concepto de gastos/egresos en el flujo de efectivo; vinculado a movimientos de tesorería y presupuesto de gastos.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'IdCashFlowConceptExpense';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de gatos del flujo de efectivo', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'IdCashFlowConceptExpense';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'IdCashFlowConceptExpense';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT, FK a Treasury.CashFlowConcept. Identificador del concepto de flujo de efectivo; referencia para ingresos, egresos y movimientos de tesorería en pagos.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'IdCashFlowConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Concepto de flujo de Efectivo', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'IdCashFlowConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'IdCashFlowConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT, FK a GeneralLedger.JournalVoucherTypes. Identificador del tipo de comprobante contable para factoring; refiere a traslado de derechos de facturación y confirmación automática.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'IdAccountingFactoring';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Comprobante Contable para Factoring', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'IdAccountingFactoring';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'IdAccountingFactoring';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT (0=No, 1=Sí). Indicador de confirmación automática para factoring; controla si los comprobantes de factoring se confirman sin intervención manual.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'AutomaticConfirmation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valida si el factoring debe tener Confirmación automatica.  0 => No  1=> Si', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'AutomaticConfirmation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'AutomaticConfirmation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT (1=Factura, 2=Cuenta por Pagar). Especifica nivel de postulación presupuestal en interfaz; en desuso—reemplazado por configuración factura a factura debido a múltiples rubros por cuenta.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'PostulateBudgetInterfaceBy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si la interfaz presupuestal se postula en:  1 - La Factura  2 - La Cuenta por Pagar    En desuso por actualización. Esto debido a que, como una cuenta por pagar puede tener muchos rubros presupuestales asociados, estos deben ser definidos factura a factura.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'PostulateBudgetInterfaceBy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'PostulateBudgetInterfaceBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TIMESTAMP. Marca de tiempo de versión del registro; captura instante exacto de creación, modificación o cambio de estado del parámetro de pago.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME NULL. Fecha y hora de última modificación del parámetro; registra cuándo se actualizó la configuración de pagos.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(20) NULL. Usuario que realizó la última modificación; auditoría de cambios en parámetros de pago y factoring.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME. Fecha y hora de creación del parámetro; marca origen del registro de configuración de pagos.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(20). Usuario que creó el parámetro; auditoría inicial de la configuración de pagos y reglas presupuestales.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Estado del registro (0=Inactivo, 1=Activo); controla si la configuración de pagos está vigente o deshabilitada.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del registro', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Valor numérico del rango máximo de antigüedad de facturas en días; facturas con antigüedad superior pertenecen a la categoría de pago máxima.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'MaximunAgeRange';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del rango maximo de edades, es decir que todas las facturas que sean Mayores el valor de este campo perteneceran al valor maximo de pago', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'MaximunAgeRange';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'MaximunAgeRange';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(100). Etiqueta del rango máximo de antigüedad (ej: ''''>186 días''''); describe la categoría de mayor vencimiento para estratificación de pagos.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'NameMaximumAgeRange';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el nombre del ultimo rango de las edades de pagos, es decir que aplican todas las facturas que sean Mayores a X ( > 186 )', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'NameMaximumAgeRange';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'NameMaximumAgeRange';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(100). Etiqueta del rango mínimo de antigüedad (ej: ''''<1 día''''); describe la categoría de facturas sin vencer para priorización de pagos.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'NameMinimumAgeRange';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el nombre del rango de edad que comprende todos los que sean menores a un dia de vencimiento, es decir que son los que estan sin Vencer (<1)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'NameMinimumAgeRange';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'NameMinimumAgeRange';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT, FK a Common.OperatingUnit. Identificador de la unidad operativa (centro de atención, sede, división); alcance de aplicación de la configuración de pagos.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad operativa', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Indicador de obligatoriedad en asociar compromisos/obligaciones presupuestales a cuentas por pagar; fuerza enlace con presupuesto.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'ObligationBudgetInterface';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si es obligatoria la asociación del compromiso / obligación en la cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'ObligationBudgetInterface';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'ObligationBudgetInterface';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Indicador de creación de una sola obligación por grupo de facturas; en desuso—reemplazado por control factura a factura con múltiples rubros presupuestales.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'OnlyObligation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si se crea una sola obligacion por el grupo de facturas asociada a la cuenta por pagar.    En desuso por actualización. Esto debido a que, como una cuenta por pagar puede tener muchos rubros presupuestales asociados, estos deben ser definidos factura a factura. Y el control se debe realizar una a una.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'OnlyObligation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'OnlyObligation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Indicador de habilitación de interfaz con módulo de presupuesto; controla si los pagos se sincronizan con compromisos y obligaciones presupuestales.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'BudgetInterface';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si se encuentra habilitada la interfaz con presupuesto', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'BudgetInterface';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'BudgetInterface';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Indicador de creación de obligación usando solo valores de débito (cargas) en cuenta por pagar; excluye créditos y notas.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'ObligationDebitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si la obligacion se crea usando solo los valores debitos de la cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'ObligationDebitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'ObligationDebitValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT, FK a GeneralLedger.JournalVoucherTypes (default=7). Identificador del tipo de comprobante para amortizaciones/abonos de cuentas por pagar.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'IdJournalVocuherAmortization';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tipo de comprobante de amortizacion', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'IdJournalVocuherAmortization';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'IdJournalVocuherAmortization';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT, FK a GeneralLedger.JournalVoucherTypes. Identificador del tipo de comprobante para notas de débito; incrementos de valor en cuentas por pagar.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'IdJournalVoucherDebitNotes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del comprobante de notas de debito', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'IdJournalVoucherDebitNotes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'IdJournalVoucherDebitNotes';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT, FK a GeneralLedger.JournalVoucherTypes. Identificador del tipo de comprobante para notas de crédito; reducciones de valor en cuentas por pagar.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'IdJournalVoucherCreditNotes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del comprobante de notas de credito', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'IdJournalVoucherCreditNotes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'IdJournalVoucherCreditNotes';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT, FK a GeneralLedger.JournalVoucherTypes. Identificador del tipo de comprobante para traslados entre cuentas/terceros; movimientos de reclasificación.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'IdJournalVoucherTranslation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del comprobante de traslados', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'IdJournalVoucherTranslation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'IdJournalVoucherTranslation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT, FK a GeneralLedger.JournalVoucherTypes. Identificador del tipo de comprobante para cuentas por pagar (obligaciones de pago); facturación de proveedores y acreedores.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'IdJournalVoucherAccountPayable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de tipo de comprobante de cuentas por pagar', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'IdJournalVoucherAccountPayable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'IdJournalVoucherAccountPayable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT IDENTITY. Identificador único y autonumérico del parámetro de pago; clave primaria de la tabla de configuración.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuración general del módulo de pagos y cuentas por pagar: define los comprobantes contables asociados a cada tipo de movimiento (cuentas por pagar, notas crédito/débito, amortizaciones, factoring), los parámetros de interfaz presupuestal, rangos de edad permitidos para las obligaciones y opciones de automatización de confirmaciones de pago por unidad operativa.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments';
