CREATE TABLE [GeneralLedger].[GeneralLedgerIVA] (
    [Id]                           INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                         VARCHAR (20)   NOT NULL,
    [Name]                         VARCHAR (200)  NOT NULL,
    [Percentage]                   NUMERIC (5, 2) NOT NULL,
    [Status]                       BIT            NOT NULL,
    [CreationUser]                 VARCHAR (20)   CONSTRAINT [DF_GeneralLedgerIVA_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]                 DATETIME       CONSTRAINT [DF_GeneralLedgerIVA_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]             VARCHAR (20)   NULL,
    [ModificationDate]             DATETIME       NULL,
    [TimeStamp]                    ROWVERSION     NOT NULL,
    [IdAccountPurchaseService]     INT            NULL,
    [IdAccountSale]                INT            NULL,
    [IdAccountDebitControlFiscal]  INT            NULL,
    [IdAccountCreditControlFiscal] INT            NULL,
    [ApplyTaxDevolution]           BIT            CONSTRAINT [DF__GeneralLe__Apply__7F9D1D92] DEFAULT ((0)) NOT NULL,
    [PaymentMethodTypes]           VARCHAR (500)  NULL,
    [TaxClassificationType]  TINYINT NOT NULL DEFAULT (1),
    CONSTRAINT [PK_GeneralLedgerIVA] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GeneralLedgerIVA_AccountCreditControlFiscal] FOREIGN KEY ([IdAccountCreditControlFiscal]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_GeneralLedgerIVA_AccountDebitControlFiscal] FOREIGN KEY ([IdAccountDebitControlFiscal]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_GeneralLedgerIVA_AccountPurchaseService] FOREIGN KEY ([IdAccountPurchaseService]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_GeneralLedgerIVA_AccountSale] FOREIGN KEY ([IdAccountSale]) REFERENCES [GeneralLedger].[MainAccounts] ([Id])
);




GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipos de métodos de pago concatenados por comas (VARCHAR 500), ej: 1,2,3,4. Define qué formas de pago aplican a este régimen de IVA: efectivo, tarjeta, transferencia, cheque, etc.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerIVA', @level2type = N'COLUMN', @level2name = N'PaymentMethodTypes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Tipos de metodos de pago,                   en este campo van concatenado los tipo por comas Ej:                   1,2,3,4..', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerIVA', @level2type = N'COLUMN', @level2name = N'PaymentMethodTypes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerIVA', @level2type = N'COLUMN', @level2name = N'PaymentMethodTypes';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT): 1=Aplica devolución/reintegro de IVA, 0=No aplica. Determina si el régimen habilita devolución fiscal de impuestos pagados.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerIVA', @level2type = N'COLUMN', @level2name = N'ApplyTaxDevolution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Aplica devolucion del IVA true(1) or false (0)', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerIVA', @level2type = N'COLUMN', @level2name = N'ApplyTaxDevolution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerIVA', @level2type = N'COLUMN', @level2name = N'ApplyTaxDevolution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT FK) de la cuenta contable principal para control fiscal en crédito. Referencia a [GeneralLedger].[MainAccounts], registra movimientos de IVA por cobrar o crédito fiscal.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerIVA', @level2type = N'COLUMN', @level2name = N'IdAccountCreditControlFiscal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta control fiscal Credito', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerIVA', @level2type = N'COLUMN', @level2name = N'IdAccountCreditControlFiscal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerIVA', @level2type = N'COLUMN', @level2name = N'IdAccountCreditControlFiscal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT FK) de la cuenta contable principal para control fiscal en débito. Referencia a [GeneralLedger].[MainAccounts], registra movimientos de IVA por pagar o impuesto causado.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerIVA', @level2type = N'COLUMN', @level2name = N'IdAccountDebitControlFiscal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta control fiscal Debito', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerIVA', @level2type = N'COLUMN', @level2name = N'IdAccountDebitControlFiscal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerIVA', @level2type = N'COLUMN', @level2name = N'IdAccountDebitControlFiscal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT FK) de la cuenta contable principal para IVA en ventas/ingresos. Referencia a [GeneralLedger].[MainAccounts], acumula el impuesto cobrado en facturas de venta.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerIVA', @level2type = N'COLUMN', @level2name = N'IdAccountSale';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta IVA Ventas', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerIVA', @level2type = N'COLUMN', @level2name = N'IdAccountSale';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerIVA', @level2type = N'COLUMN', @level2name = N'IdAccountSale';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT FK) de la cuenta contable principal para IVA en compras/servicios. Referencia a [GeneralLedger].[MainAccounts], acumula el impuesto pagado en facturas de adquisición.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerIVA', @level2type = N'COLUMN', @level2name = N'IdAccountPurchaseService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta IVA Compra/Servicio', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerIVA', @level2type = N'COLUMN', @level2name = N'IdAccountPurchaseService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerIVA', @level2type = N'COLUMN', @level2name = N'IdAccountPurchaseService';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal automática (TIMESTAMP) que registra el instante exacto de creación, modificación o evento en la fila. Controla versiones y cambios en BD.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerIVA', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerIVA', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerIVA', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación del registro IVA (DATETIME, nullable). Auditoría de cambios posteriores a la creación.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerIVA', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerIVA', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerIVA', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación del régimen IVA (VARCHAR 20, nullable). Trazabilidad de cambios en la configuración fiscal.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerIVA', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerIVA', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerIVA', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro (DATETIME, default función getdate()). Auditoría de cuándo se estableció este régimen.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerIVA', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerIVA', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerIVA', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el régimen IVA (VARCHAR 20, default 999). Trazabilidad de origen del registro en el sistema.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerIVA', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerIVA', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerIVA', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del régimen IVA: 1=Activo (vigente), 0=Inactivo (descontinuado). Controla si se aplica en nuevas operaciones.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerIVA', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del Documento 1 - Activo 0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerIVA', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerIVA', @level2type = N'COLUMN', @level2name = N'Status';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación tributaria del IVA: 1 - Gravado, 2 - Excluido, 3 - Exento', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerIVA', @level2type = N'COLUMN', @level2name = N'TaxClassificationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clasificación tributaria del IVA: 1 - Gravado, 2 - Excluido, 3 - Exento', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerIVA', @level2type = N'COLUMN', @level2name = N'TaxClassificationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de IVA a aplicar (NUMERIC 5,2), ej: 19.00, 5.00, 0.00. Tasa impositiva sobre base gravable en ventas y compras.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerIVA', @level2type = N'COLUMN', @level2name = N'Percentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje de iva que se va aplicar', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerIVA', @level2type = N'COLUMN', @level2name = N'Percentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerIVA', @level2type = N'COLUMN', @level2name = N'Percentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo del régimen IVA (VARCHAR 200), ej: ''''IVA General 19%'''', ''''IVA Reducido 5%'''', ''''Exento 0%''''. Identificación legible para usuarios.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerIVA', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'nombre del iva', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerIVA', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerIVA', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del régimen IVA (VARCHAR 20, PK parcial), ej: ''''IVA19'''', ''''IVA5'''', ''''IVA0''''. Referencia corta en RIPS, facturas y glosas.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerIVA', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del iva', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerIVA', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerIVA', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del régimen IVA (INT IDENTITY, PK). Clave primaria para asociar transacciones, facturas y configuraciones fiscales.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerIVA', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del IVA', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerIVA', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerIVA', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de tipos de IVA (impuesto al valor agregado) utilizados en la contabilidad general. Define los códigos, porcentajes y cuentas contables asociadas a cada tarifa de IVA para compras, ventas y control fiscal.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerIVA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerIVA';
