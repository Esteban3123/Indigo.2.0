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
ALTER TABLE [Portfolio].[SettingPortfolio] NOCHECK CONSTRAINT [FK_SettingPortfolio_Dependency];


GO
ALTER TABLE [Portfolio].[SettingPortfolio] NOCHECK CONSTRAINT [FK_SettingPortfolio_JournalVoucherTypes];


GO
ALTER TABLE [Portfolio].[SettingPortfolio] NOCHECK CONSTRAINT [FK_SettingPortfolio_JournalVoucherTypes1];


GO
ALTER TABLE [Portfolio].[SettingPortfolio] NOCHECK CONSTRAINT [FK_SettingPortfolio_JournalVoucherTypes2];


GO
ALTER TABLE [Portfolio].[SettingPortfolio] NOCHECK CONSTRAINT [FK_SettingsPortfolio_JournalVoucherTypes];


GO
ALTER TABLE [Portfolio].[SettingPortfolio] NOCHECK CONSTRAINT [FK_SettingsPortfolio_JournalVoucherTypes1];


GO
ALTER TABLE [Portfolio].[SettingPortfolio] NOCHECK CONSTRAINT [FK_SettingsPortfolio_JournalVoucherTypes2];


GO
ALTER TABLE [Portfolio].[SettingPortfolio] NOCHECK CONSTRAINT [FK_SettingsPortfolio_JournalVoucherTypes3];


GO
ALTER TABLE [Portfolio].[SettingPortfolio] NOCHECK CONSTRAINT [FK_SettingsPortfolio_JournalVoucherTypes4];


GO
ALTER TABLE [Portfolio].[SettingPortfolio] NOCHECK CONSTRAINT [FK_SettingsPortfolio_OperatingUnit];

GO
CREATE NONCLUSTERED INDEX [IX_SettingPortfolio_OperatingUnitId]
    ON [Portfolio].[SettingPortfolio] ([OperatingUnitId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permite la visualización de facturas que tengan Nota Debito Credito', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'NotesDebitCreditPortfolio';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Configuracion que Permite la visualizacion de facturas con cruce de anticipos vs CxC', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'Transfers';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si o no permite agregar factura glosada sin conciliar en traslado a cobro jurídico', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'UnReconciledInvoice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dependencia con la cual se realizará el reconocimiento en presupuesto de la radicación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'DependencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo booleano que especifica si tiene colección legal, 1 - Si, 0 - No.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'LegalCollection';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Tipo de comprobante contable para el deterioro de cartera', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeDeteriorationAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Tipo de comprobante para cuentas de cobro de dificil Recaudo', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeHardCollectionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del registro', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del rango maximo de edades, es decir que todas las facturas que sean Mayores el valor de este campo perteneceran al valor maximo de pago', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'MaximunAgeRange';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el nombre del ultimo rango de las edades de pagos, es decir que aplican todas las facturas que sean Mayores a X ( > 186 )', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'NameMaximumAgeRange';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el nombre del rango de edad que comprende todos los que sean menores a un dia de vencimiento, es decir que son los que estan sin Vencer (<1)', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'NameMinimumAgeRange';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el id del tipo de comprobante contable para el documento de cuenta por cobrar', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeDocumentAccountReceivableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tipo de comprobante de Radicacion de cuentas', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeFilingAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tipo de comprobante de provision', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeProvisionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tipo de comprobante de traslados', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeTranslationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tipo de comprobante de notas de debito', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeDebitNotesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tipo de comprobante de notas de credito', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeCreditNotesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad operativa', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'Id';

