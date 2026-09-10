CREATE TABLE [Treasury].[SettingsTreasury] (
    [Id]                                           INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdOperatingUnit]                              INT          NOT NULL,
    [BudgetConfirm]                                BIT          NOT NULL,
    [SettingBudgetforConcept]                      BIT          NOT NULL,
    [CheckBookControl]                             BIT          NOT NULL,
    [BuildPaymentOrderAutomatic]                   BIT          NOT NULL,
    [AllowModifyDocumentsDateBank]                 BIT          NOT NULL,
    [NumberDayBank]                                INT          CONSTRAINT [DF_SettingsTreasury_NumberDayBank] DEFAULT ((0)) NOT NULL,
    [JournalVoucherTypeCashReceipts]               INT          NOT NULL,
    [JournalVoucherTypeVoucherTransaction]         INT          NOT NULL,
    [JournalVoucherTypeBankAppropriations]         INT          NOT NULL,
    [JournalVoucherTypeTreasuryNotes]              INT          NOT NULL,
    [JournalVoucherTypeVoucherTransactionCrossing] INT          NOT NULL,
    [SaturdaySkillful]                             BIT          NOT NULL,
    [SundaySkillful]                               BIT          NOT NULL,
    [GetThirdPartyCashRegister]                    TINYINT      CONSTRAINT [DF_SettingsTreasury_GetThirdPartyCashRegister] DEFAULT ((1)) NOT NULL,
    [GetThirdPartyBank]                            TINYINT      CONSTRAINT [DF_SettingsTreasury_GetThirdPartyBank] DEFAULT ((1)) NOT NULL,
    [CreationUser]                                 VARCHAR (20) NOT NULL,
    [CreationDate]                                 DATETIME     NOT NULL,
    [ModificationUser]                             VARCHAR (20) NULL,
    [ModificationDate]                             DATETIME     NULL,
    [TimeStamp]                                    ROWVERSION   NOT NULL,
    [JournalVoucherTypeConstitutionCashId]         INT          NULL,
    CONSTRAINT [PK_SettingsTreasury] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SettingsTreasury_JournalVoucherTypes] FOREIGN KEY ([JournalVoucherTypeBankAppropriations]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingsTreasury_JournalVoucherTypes1] FOREIGN KEY ([JournalVoucherTypeCashReceipts]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingsTreasury_JournalVoucherTypes2] FOREIGN KEY ([JournalVoucherTypeTreasuryNotes]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingsTreasury_JournalVoucherTypes3] FOREIGN KEY ([JournalVoucherTypeVoucherTransaction]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingsTreasury_JournalVoucherTypes4] FOREIGN KEY ([JournalVoucherTypeVoucherTransactionCrossing]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingsTreasury_JournalVoucherTypes5] FOREIGN KEY ([JournalVoucherTypeConstitutionCashId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id])
);
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tipo de comprobante contable para constitución de caja, referencia FK a GeneralLedger.JournalVoucherTypes (INT, nullable). Define el comprobante usado en movimientos de constitución de fondos de caja.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeConstitutionCashId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeConstitutionCashId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeConstitutionCashId';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca de tiempo automática (TIMESTAMP) generada en creación, registro o modificación del registro de configuración tesorería. Control de versión y auditoria.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sello de tiempo. Guarda el instante tiempo de la creación, registro o modificación de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'TimeStamp';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) del último cambio realizado en la configuración de tesorería. Auditoría y trazabilidad de modificaciones.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'ModificationDate';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 20) que realizó la última modificación de la configuración tesorería. Auditoría, responsabilidad y trazabilidad.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'ModificationUser';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que se creó el registro de configuración tesorería. Auditoría y trazabilidad de origen.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'CreationDate';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 20) que creó el registro de configuración tesorería. Auditoría, responsabilidad y control de cambios.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'CreationUser';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (TINYINT: 1=Tercero Cuenta Bancaria, 2=Tercero Movimiento) que especifica la fuente del tercero en movimientos bancarios. Configuración de origen de datos contraparte.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'GetThirdPartyBank';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica de donde se obtiene el Tercero cuando se realicen movimientos de la cuenta bancaria  1 - Tercero Cuenta Bancaria  2 - Tercero Movimiento', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'GetThirdPartyBank';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'GetThirdPartyBank';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (TINYINT: 1=Tercero Caja, 2=Tercero Movimiento) que define la procedencia del tercero en movimientos de caja. Configuración de origen de contraparte en operaciones.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'GetThirdPartyCashRegister';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica de donde se obtiene el Tercero cuando se realicen movimientos de la Caja  1 - Tercero Caja  2 - Tercero Movimiento', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'GetThirdPartyCashRegister';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'GetThirdPartyCashRegister';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT: 1=Sí, 0=No) que indica si domingo es día hábil para tesorería. Calendario laboral y cálculo de fechas valor.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'SundaySkillful';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Domingo Hábil', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'SundaySkillful';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'SundaySkillful';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT: 1=Sí, 0=No) que indica si sábado es día hábil para tesorería. Calendario laboral y procesamiento de operaciones.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'SaturdaySkillful';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sábado Hábil', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'SaturdaySkillful';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'SaturdaySkillful';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del tipo de comprobante contable para egresos con cruce (FK a JournalVoucherTypes). Comprobantes de egreso cruzados entre cuentas.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeVoucherTransactionCrossing';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de comprobante Comprobantes de Egreso - Cruce', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeVoucherTransactionCrossing';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeVoucherTransactionCrossing';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del tipo de comprobante contable para notas de tesorería (FK a JournalVoucherTypes). Ajustes y notas administrativas de tesorería.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeTreasuryNotes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de comprobante Notas de Tesoreria', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeTreasuryNotes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeTreasuryNotes';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del tipo de comprobante contable para consignaciones/depósitos bancarios (FK a JournalVoucherTypes). Movimientos de ingresos a cuentas bancarias.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeBankAppropriations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de comprobante Consignaciones Bancarias', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeBankAppropriations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeBankAppropriations';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del tipo de comprobante contable para comprobantes de egreso (FK a JournalVoucherTypes). Salidas de fondos y pagos bancarios.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeVoucherTransaction';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de comprobante Comprobantes de Egreso', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeVoucherTransaction';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeVoucherTransaction';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del tipo de comprobante contable para recibos de caja (FK a JournalVoucherTypes). Ingresos de efectivo y movimientos de caja.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeCashReceipts';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de comprobante Recibos de Caja', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeCashReceipts';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeCashReceipts';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de días hacia atrás (INT, ≥0) permitidos para retrodatar documentos bancarios. Solo activo si AllowModifyDocumentsDateBank=1. Control de antigüedad en modificaciones.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'NumberDayBank';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la cantidad de dias hacia atras que se puede modificar la fecha del documento    Nota: este campo solo se llena con un valor diferente a cero si el campo AllowModifyDocumentDateBank esta en true', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'NumberDayBank';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'NumberDayBank';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT: 1=Sí, 0=No) que habilita modificación retroactiva de fechas en documentos bancarios. Flexibilidad de correcciones contables.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'AllowModifyDocumentsDateBank';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permitir modificar las fechas de documentos de Bancos', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'AllowModifyDocumentsDateBank';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'AllowModifyDocumentsDateBank';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT: 1=Sí, 0=No) que activa generación automática de órdenes de pago. Automatización de procesos de egreso.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'BuildPaymentOrderAutomatic';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Generar Orden de Pago Automaticamente', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'BuildPaymentOrderAutomatic';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'BuildPaymentOrderAutomatic';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT: 1=Sí, 0=No) que parametriza presupuesto desglosado por concepto. Control presupuestario granular.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'SettingBudgetforConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Parametriza Presupuesto por Concepto', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'SettingBudgetforConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'SettingBudgetforConcept';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT: 1=Sí, 0=No) que requiere confirmación/aprobación de presupuestos. Validación presupuestaria obligatoria.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'BudgetConfirm';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Confirma Presupuesto', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'BudgetConfirm';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'BudgetConfirm';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de la unidad operativa a la que aplican estos parámetros tesorería. Centro de atención, sede, filial.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad operativa', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del registro de configuración tesorería. Clave primaria.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'Id';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuración del módulo de Tesorería por unidad operativa. Define parámetros operativos como confirmación de presupuesto, control de chequeras, generación automática de órdenes de pago, tipos de comprobantes contables para cada movimiento de caja y banco, y días hábiles permitidos.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury';
