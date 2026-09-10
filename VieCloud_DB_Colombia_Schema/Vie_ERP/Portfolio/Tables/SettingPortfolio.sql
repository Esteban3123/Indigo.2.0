CREATE TABLE [Portfolio].[SettingPortfolio] (
    [Id]                                            INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [OperatingUnitId]                               INT           NOT NULL,
    [JournalVoucherTypeCreditNotesId]               INT           NOT NULL,
    [JournalVoucherTypeDebitNotesId]                INT           NOT NULL,
    [JournalVoucherTypeTranslationId]               INT           NOT NULL,
    [JournalVoucherTypeProvisionId]                 INT           NOT NULL,
    [JournalVoucherTypeFilingAccountId]             INT           NOT NULL,
    [JournalVoucherTypeDocumentAccountReceivableId] INT           NOT NULL,
    [NameMinimumAgeRange]                           VARCHAR (100) NOT NULL,
    [NameMaximumAgeRange]                           VARCHAR (100) NOT NULL,
    [MaximunAgeRange]                               INT           NOT NULL,
    [State]                                         BIT           NOT NULL,
    [CreationUser]                                  VARCHAR (20)  NOT NULL,
    [CreationDate]                                  DATETIME      NOT NULL,
    [ModificationUser]                              VARCHAR (20)  NULL,
    [ModificationDate]                              DATETIME      NULL,
    [TimeStamp]                                     ROWVERSION    NOT NULL,
    [JournalVoucherTypeHardCollectionId]            INT           NOT NULL,
    [JournalVoucherTypeDeteriorationAccountId]      INT           NULL,
    [LegalCollection]                               BIT           CONSTRAINT [DF_SettingPortfolio_LegalCollection] DEFAULT ((1)) NOT NULL,
    [DependencyId]                                  INT           NULL,
    [UnReconciledInvoice]                           BIT           NULL,
    [Transfers]                                     BIT           NOT NULL,
    [NotesDebitCreditPortfolio]                     BIT           NOT NULL,
    [ApplyDeteriorationByClassification]            BIT           DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_SettingPortfolio] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SettingPortfolio_Dependency] FOREIGN KEY ([DependencyId]) REFERENCES [Budget].[Dependency] ([Id]),
    CONSTRAINT [FK_SettingPortfolio_JournalVoucherTypes] FOREIGN KEY ([JournalVoucherTypeDocumentAccountReceivableId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingPortfolio_JournalVoucherTypes1] FOREIGN KEY ([JournalVoucherTypeHardCollectionId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingPortfolio_JournalVoucherTypes2] FOREIGN KEY ([JournalVoucherTypeDeteriorationAccountId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingsPortfolio_JournalVoucherTypes] FOREIGN KEY ([JournalVoucherTypeCreditNotesId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingsPortfolio_JournalVoucherTypes1] FOREIGN KEY ([JournalVoucherTypeDebitNotesId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingsPortfolio_JournalVoucherTypes2] FOREIGN KEY ([JournalVoucherTypeFilingAccountId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingsPortfolio_JournalVoucherTypes3] FOREIGN KEY ([JournalVoucherTypeProvisionId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingsPortfolio_JournalVoucherTypes4] FOREIGN KEY ([JournalVoucherTypeTranslationId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingsPortfolio_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id])
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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) que habilita visualización de facturas/cuentas por cobrar con Notas Débito y Crédito en cartera; sinónimos: notas de ajuste, comprobantes de corrección contable.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'NotesDebitCreditPortfolio';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite la visualización de facturas que tengan Nota Debito Credito', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'NotesDebitCreditPortfolio';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'NotesDebitCreditPortfolio';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuración booleana (BIT) que permite visualizar facturas con cruce de anticipos vs Cuentas por Cobrar (CxC); sinónimos: traslados, compensación de saldos, anticipos aplicados.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'Transfers';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Configuracion que Permite la visualizacion de facturas con cruce de anticipos vs CxC', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'Transfers';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'Transfers';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) que define si permite agregar facturas glosadas sin conciliar en traslado a cobro jurídico/legal; sinónimos: factura no reconciliada, glosa pendiente, cobro judicial.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'UnReconciledInvoice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si o no permite agregar factura glosada sin conciliar en traslado a cobro jurídico', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'UnReconciledInvoice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'UnReconciledInvoice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia (FK) a Dependencia/Unidad Funcional donde se realiza el reconocimiento presupuestal de la radicación de cuentas; sinónimos: centro de atención, unidad de negocio, área operativa.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'DependencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dependencia con la cual se realizará el reconocimiento en presupuesto de la radicación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'DependencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'DependencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo booleano (BIT, 1=Sí, 0=No) que especifica si aplica gestión de cobro legal/jurídico; sinónimos: cobranza judicial, gestión de deuda en procedimiento legal.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'LegalCollection';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo booleano que especifica si tiene colección legal, 1 - Si, 0 - No.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'LegalCollection';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'LegalCollection';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia (FK) al Tipo de Comprobante Contable para registrar deterioro de cartera/CxC; sinónimos: provisión por deterioro, estimación incobrable, ajuste por obsolescencia.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeDeteriorationAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Tipo de comprobante contable para el deterioro de cartera', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeDeteriorationAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeDeteriorationAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia (FK) al Tipo de Comprobante Contable para cuentas de cobro de difícil recaudo/cartera vencida; sinónimos: cobranza judicial, cuentas irrecuperables, gestión de deuda morosa.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeHardCollectionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Tipo de comprobante para cuentas de cobro de dificil Recaudo', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeHardCollectionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeHardCollectionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Timestamp SQL Server (TIMESTAMP) que registra marca temporal automática de creación, modificación o cambio de estado del registro; sinónimos: sello temporal, control de versión de fila.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo DATETIME que registra fecha y hora de última modificación del registro de configuración de cartera; sinónimos: fecha de actualización, última edición.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo VARCHAR(20) que identifica usuario que realizó última modificación del registro; sinónimos: usuario de edición, responsable de cambio.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo DATETIME que registra fecha y hora de creación inicial del registro de configuración; sinónimos: fecha de inserción, fecha de registro.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo VARCHAR(20) que identifica usuario que creó el registro de configuración de cartera; sinónimos: usuario de inserción, responsable de registro.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo booleano (BIT) que indica estado activo/inactivo del registro de configuración de cartera; sinónimos: estatus, vigencia, habilitado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del registro', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor entero (INT) que define límite máximo de rango de antigüedad en días; facturas mayores a este valor se clasifican en rango máximo de cartera; sinónimos: edad máxima de deuda, días límite de antigüedad.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'MaximunAgeRange';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del rango maximo de edades, es decir que todas las facturas que sean Mayores el valor de este campo perteneceran al valor maximo de pago', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'MaximunAgeRange';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'MaximunAgeRange';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Etiqueta descriptiva (VARCHAR 100) del rango de edad máxima de cartera (ej: >186 días); usado para reportes y glosario de cobranza; sinónimos: categoría de cartera vencida, clasificación de antigüedad extrema.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'NameMaximumAgeRange';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el nombre del ultimo rango de las edades de pagos, es decir que aplican todas las facturas que sean Mayores a X ( > 186 )', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'NameMaximumAgeRange';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'NameMaximumAgeRange';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Etiqueta descriptiva (VARCHAR 100) del rango de edad mínima (<1 día); clasifica facturas sin vencer o recién emitidas; sinónimos: cartera vigente, facturas a vencer, deuda fresca.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'NameMinimumAgeRange';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el nombre del rango de edad que comprende todos los que sean menores a un dia de vencimiento, es decir que son los que estan sin Vencer (<1)', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'NameMinimumAgeRange';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'NameMinimumAgeRange';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia (FK) al Tipo de Comprobante Contable para documentos de Cuenta por Cobrar/CxC; sinónimos: comprobante de factura, asiento de acreedor, documento de ingreso.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeDocumentAccountReceivableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id del tipo de comprobante contable para el documento de cuenta por cobrar', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeDocumentAccountReceivableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeDocumentAccountReceivableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia (FK) al Tipo de Comprobante Contable para radicación/traslado de cuentas a cobro; sinónimos: comprobante de radicación, asiento de transferencia de cartera.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeFilingAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tipo de comprobante de Radicacion de cuentas', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeFilingAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeFilingAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia (FK) al Tipo de Comprobante Contable para registro de provisiones/estimaciones de cartera; sinónimos: comprobante de provisión, ajuste de reserva contable.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeProvisionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tipo de comprobante de provision', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeProvisionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeProvisionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia (FK) al Tipo de Comprobante Contable para traslados de cartera entre áreas; sinónimos: comprobante de traslado, transferencia contable, movimiento de deuda.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeTranslationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tipo de comprobante de traslados', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeTranslationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeTranslationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia (FK) al Tipo de Comprobante Contable para Notas Débito; sinónimos: comprobante de ajuste al alza, documento de incremento de factura.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeDebitNotesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tipo de comprobante de notas de debito', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeDebitNotesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeDebitNotesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia (FK) al Tipo de Comprobante Contable para Notas Crédito/devoluciones; sinónimos: comprobante de ajuste a la baja, documento de rebaja de factura.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeCreditNotesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tipo de comprobante de notas de credito', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeCreditNotesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeCreditNotesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia (FK) a Unidad Operativa/Centro de Atención donde aplica la configuración de cartera; sinónimos: sede, sucursal, punto de atención, unidad funcional.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad operativa', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (INT IDENTITY) del registro de configuración de cartera; clave primaria de tabla SettingPortfolio.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT, default=0) que activa aplicación de reglas de deterioro de cartera por clasificación de antigüedad; sinónimos: deterioro por rango de antigüedad, provisión por clasificación.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'ApplyDeteriorationByClassification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si en los parámetros aplica Reglas de Deterioro por Clasificación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'ApplyDeteriorationByClassification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'ApplyDeteriorationByClassification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuración general del módulo de cartera (cuentas por cobrar). Define los tipos de comprobantes contables asociados a notas crédito, notas débito, traslados, provisiones, deterioro y cobro jurídico, así como los rangos de edad de cartera y los parámetros de comportamiento por unidad operativa.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio';
