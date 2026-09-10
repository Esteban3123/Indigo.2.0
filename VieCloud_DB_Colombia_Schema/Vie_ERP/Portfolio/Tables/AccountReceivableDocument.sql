CREATE TABLE [Portfolio].[AccountReceivableDocument] (
    [Id]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                VARCHAR (20)  NOT NULL,
    [OperatingUnitId]     INT           NOT NULL,
    [DocumentDate]        DATETIME      NOT NULL,
    [CustomerId]          INT           NOT NULL,
    [MainAccountId]       INT           NOT NULL,
    [CostCenterId]        INT           NULL,
    [InvoiceNumber]       VARCHAR (20)  NOT NULL,
    [Term]                INT           NOT NULL,
    [ExpiredDate]         DATETIME      NOT NULL,
    [Share]               INT           NOT NULL,
    [Value]               NUMERIC (18)  NOT NULL,
    [DebitValue]          NUMERIC (18)  NOT NULL,
    [CreditValue]         NUMERIC (18)  NOT NULL,
    [Observation]         VARCHAR (300) NULL,
    [AccountReceivableId] INT           NULL,
    [Status]              TINYINT       NOT NULL,
    [CreationUser]        VARCHAR (20)  NOT NULL,
    [CreationDate]        DATETIME      NOT NULL,
    [ModificationUser]    VARCHAR (20)  NULL,
    [ModificationDate]    DATETIME      NULL,
    [ConfirmationUser]    VARCHAR (20)  NULL,
    [ConfirmationDate]    DATETIME      NULL,
    [AnnulmentUser]       VARCHAR (20)  NULL,
    [AnnulmentDate]       DATETIME      NULL,
    [TimeStamp]           ROWVERSION    NOT NULL,
    [CurrencyId]          INT           NULL,
    CONSTRAINT [PK_AccountReceivableDocument__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccountReceivableDocument_AccountReceivable] FOREIGN KEY ([AccountReceivableId]) REFERENCES [Portfolio].[AccountReceivable] ([Id]),
    CONSTRAINT [FK_AccountReceivableDocument_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_AccountReceivableDocument_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_AccountReceivableDocument_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [Common].[Customer] ([Id]),
    CONSTRAINT [FK_AccountReceivableDocument_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_AccountReceivableDocument_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id])
);




GO



GO



GO



GO



GO



GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_AccountReceivableDocument__Code]
    ON [Portfolio].[AccountReceivableDocument]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la moneda del documento de cuentas por cobrar (FK a Common.Currency). Tipo: INT. Determina la divisa en que se registra el valor facturado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'CurrencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Moneda del documento de cuentas por cobrar', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'CurrencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'CurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal automática (TIMESTAMP) que captura el instante exacto de creación, modificación, confirmación o anulación del documento. Control de concurrencia.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que fue anulado el documento de cuentas por cobrar. Se registra solo cuando Status = 3 (Anulado).', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Anulación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del usuario (VARCHAR 20) que ejecutó la anulación del documento de cuentas por cobrar. Auditoría.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Anulación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que fue confirmado el documento de cuentas por cobrar. Marca transición a Status = 2 (Confirmado).', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Confirmación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del usuario (VARCHAR 20) que confirmó el documento de cuentas por cobrar y generó el registro contable.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Confirmación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) del último cambio realizado al documento de cuentas por cobrar antes de confirmación.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del usuario (VARCHAR 20) que realizó la última modificación del documento antes de confirmación.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de creación inicial del documento de cuentas por cobrar. Registro obligatorio.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del usuario (VARCHAR 20) que creó el documento de cuentas por cobrar. Auditoría obligatoria.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del documento (TINYINT): 1=Registrado (borrador), 2=Confirmado (contabilizado), 3=Anulado. Controla el flujo de aprobación.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la cuenta por cobrar (Registrado = 1, Confirmado = 2, Anulado = 3)', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK a Portfolio.AccountReceivable) generado solo cuando el documento es confirmado (Status=2). Vinculación contable.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id de la cuenta por cobrar que genero, este  solo se llena cuando el documento este confirmado', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo de texto (VARCHAR 300) para notas, comentarios o glosas del documento de cuentas por cobrar. Opcional.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'Observation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'Observation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'Observation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico (NUMERIC 18) que suma todos los detalles de tipo crédito del documento. Componente del saldo.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'CreditValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor del credito de la cuenta por cobrar, este se saca de la sumatoria de los detalles de tipo credito', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'CreditValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'CreditValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico (NUMERIC 18) que suma todos los detalles de tipo débito del documento. Componente del saldo.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'DebitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor del debito de la cuenta por cobrar, este se saca de la sumatoria de los detalles de tipo debito', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'DebitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'DebitValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor neto (NUMERIC 18) calculado como CreditValue - DebitValue. Representa el saldo a cobrar. Fórmula: Value + DebitValue = CreditValue.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la cuenta por cobrar, este se calcula de la operacion (ValueCredit - ValueDebit), es decir que Value + ValueDebit = ValueCredit', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de cuotas (INT) en que se divide el documento de cuentas por cobrar para el cobro fraccionado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'Share';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la cantidad de cuotas de la cuenta por cobrar', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'Share';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'Share';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de vencimiento (DATETIME) calculada automáticamente como DocumentDate + Term (en días). Define plazo de cobro.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'ExpiredDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de vencimiento de la cuenta por cobrar la cual es calculada, (se toma el plazo en dias mas la fecha del documento)', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'ExpiredDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'ExpiredDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Plazo de crédito (INT) expresado en días. Se suma a DocumentDate para obtener ExpiredDate.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'Term';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Plazo de la cuenta (En dias)', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'Term';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'Term';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de factura (VARCHAR 20) asociado al documento de cuentas por cobrar. Referencia a documento comercial original.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de la factura', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK a Payroll.CostCenter) del centro de costos que genera el documento. Opcional. Imputación contable.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del centro de costos.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK a GeneralLedger.MainAccounts) de la cuenta contable principal del documento. Contabilización obligatoria.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK a Common.Customer) del cliente deudor del documento de cuentas por cobrar. Referencia obligatoria.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'CustomerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Cliente', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'CustomerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'CustomerId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) del documento de cuentas por cobrar. Base para calcular ExpiredDate. Referencia de período.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del documento ', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK a Common.OperatingUnit) de la unidad operativa (centro de atención, sucursal, etc.) donde se originó el documento.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la Unidad operativa en donde se realizo el documento', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único (VARCHAR 20) que identifica el documento de cuentas por cobrar. Referencia externa.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, IDENTITY) de la fila del documento de cuentas por cobrar. Clave primaria. Generado automáticamente.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del documento de CxC', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Documentos de cuentas por cobrar (facturas, notas débito/crédito) generados en el módulo de cartera. Registra el valor, estado, vencimiento y trazabilidad de auditoría de cada documento financiero emitido a un cliente o pagador.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocument';
