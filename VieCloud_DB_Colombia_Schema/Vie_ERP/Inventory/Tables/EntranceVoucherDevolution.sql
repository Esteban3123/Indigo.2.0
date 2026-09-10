CREATE TABLE [Inventory].[EntranceVoucherDevolution] (
    [Id]                   INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                 VARCHAR (20)    NOT NULL,
    [OperatingUnitId]      INT             NOT NULL,
    [DocumentDate]         DATETIME        NOT NULL,
    [WarehouseId]          INT             NOT NULL,
    [EntranceVoucherId]    INT             NOT NULL,
    [Description]          VARCHAR (MAX)   NULL,
    [FreightValue]         DECIMAL (18, 2) NOT NULL,
    [FreightIVAPercentage] NUMERIC (5, 2)  NOT NULL,
    [FreightIVAValue]      DECIMAL (18, 2) NOT NULL,
    [Value]                DECIMAL (18, 2) NOT NULL,
    [ValueDiscount]        DECIMAL (18, 2) NOT NULL,
    [ValueTax]             DECIMAL (18, 2) NOT NULL,
    [WithholdingTax]       DECIMAL (18, 2) NOT NULL,
    [WithholdingICA]       DECIMAL (18, 2) NOT NULL,
    [RetentionSource]      DECIMAL (18, 2) NOT NULL,
    [RetentionOther]       DECIMAL (18, 2) NOT NULL,
    [DeductionOther]       DECIMAL (18, 2) NOT NULL,
    [DistrictTax]          DECIMAL (18, 2) NOT NULL,
    [TotalValue]           DECIMAL (18, 2) NOT NULL,
    [PaymentNoteId]        INT             NULL,
    [Status]               TINYINT         NOT NULL,
    [CreationUser]         VARCHAR (20)    NOT NULL,
    [CreationDate]         DATETIME        NOT NULL,
    [ModificationUser]     VARCHAR (20)    NULL,
    [ModificationDate]     DATETIME        NULL,
    [ConfirmationUser]     VARCHAR (20)    NULL,
    [ConfirmationDate]     DATETIME        NULL,
    [AnnulmentUser]        VARCHAR (20)    NULL,
    [AnnulmentDate]        DATETIME        NULL,
    [TimeStamp]            ROWVERSION      NOT NULL,
    CONSTRAINT [PK_EntranceVoucherDevolution__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EntranceVoucherDevolution_EntranceVoucher] FOREIGN KEY ([EntranceVoucherId]) REFERENCES [Inventory].[EntranceVoucher] ([Id]),
    CONSTRAINT [FK_EntranceVoucherDevolution_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_EntranceVoucherDevolution_PaymentNotes] FOREIGN KEY ([PaymentNoteId]) REFERENCES [Payments].[PaymentNotes] ([Id]),
    CONSTRAINT [FK_EntranceVoucherDevolution_Warehouse] FOREIGN KEY ([WarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id])
);




GO



GO



GO



GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_EntranceVoucherDevolution__Code]
    ON [Inventory].[EntranceVoucherDevolution]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal (TIMESTAMP) del evento de creación, modificación, confirmación o anulación del registro de devolución; auditoría automática de cambios en inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que se anuló la devolución del comprobante de entrada; estado final = anulado (3).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Anulación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 20, PII) que ejecutó la anulación del registro de devolución en inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Anulación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que se confirmó la devolución y se generó la nota de pago asociada; estado = confirmado (2).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Confirmación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 20, PII) que confirmó la devolución del comprobante de entrada en almacén.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Confirmación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) del último cambio realizado al registro de devolución antes de confirmación o anulación.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 20, PII) que realizó la última modificación del registro de devolución.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de creación del registro de devolución; auditoría inicial del proceso.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 20, PII) que creó originalmente el registro de devolución del comprobante de entrada.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado actual (TINYINT): 1=registrado (borrador), 2=confirmado (nota de pago generada), 3=anulado; controla flujo de devolución.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado (registrado = 1,confirmado = 2,anulado = 3)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) de la nota de pago generada al confirmar la devolución; referencia a Payments.PaymentNotes.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'PaymentNoteId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id de la nota de pagos que se genera al confirmar la devolucion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'PaymentNoteId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'PaymentNoteId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total (DECIMAL 18,2) de la devolución incluidos impuestos, retenciones, descuentos y flete; monto final a pagar/acreditar.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'TotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor total', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'TotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'TotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor (DECIMAL 18,2) de descuentos o impuestos distritales aplicables a la devolución según localización.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'DistrictTax';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el valor de los descuentos distritales del registro', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'DistrictTax';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'DistrictTax';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Otras deducciones (DECIMAL 18,2) no clasificadas como retención, descuento o impuesto; ajustes varios.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'DeductionOther';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'otras deducciones ', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'DeductionOther';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'DeductionOther';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Retención sobre otros conceptos (DECIMAL 18,2) no incluidos en fuente, ICA o impuesto de renta.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'RetentionOther';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Retención Otros', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'RetentionOther';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'RetentionOther';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Retención en la fuente (DECIMAL 18,2) aplicada sobre la devolución; cumplimiento tributario DIAN.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'RetentionSource';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fuente de retención', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'RetentionSource';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'RetentionSource';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Retención ICA (DECIMAL 18,2) sobre el valor de la devolución; impuesto municipal de industria y comercio.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'WithholdingICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Retención de ICA', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'WithholdingICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'WithholdingICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Impuesto de retención (DECIMAL 18,2) aplicado al comprobante de devolución; cumplimiento fiscal.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'WithholdingTax';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'impuesto de Retención', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'WithholdingTax';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'WithholdingTax';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del impuesto (DECIMAL 18,2) incluido en la devolución; generalmente IVA sobre base imponible.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'ValueTax';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del impuesto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'ValueTax';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'ValueTax';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total del descuento (DECIMAL 18,2) aplicado al comprobante de devolución; reduce base imponible.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'ValueDiscount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del descuento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'ValueDiscount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'ValueDiscount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor neto (DECIMAL 18,2) de la devolución sin impuestos ni retenciones; base para cálculos tributarios.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del IVA (DECIMAL 18,2) calculado sobre el flete de la devolución; incluido en TotalValue.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'FreightIVAValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del iva que se va a obtener del valor del flete', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'FreightIVAValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'FreightIVAValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de IVA (NUMERIC 5,2) a aplicar al flete según parámetros de inventario configurados.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'FreightIVAPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje del iva que se va aplicar al flete, El porcentaje del flete se obtiene de los parametros de inventarios', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'FreightIVAPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'FreightIVAPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del flete (DECIMAL 18,2) por transporte y manejo de la devolución desde almacén.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'FreightValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del flete', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'FreightValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'FreightValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción (VARCHAR MAX, nullable) de la razón, motivo o justificación de la devolución del comprobante.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la descripcion del por que de la devolucion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) del comprobante de entrada original a devolver; referencia a Inventory.EntranceVoucher.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'EntranceVoucherId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del comprobante de entrada que se desea devolver', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'EntranceVoucherId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'EntranceVoucherId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) del almacén desde el cual se origina la devolución; referencia a Inventory.Warehouse.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'WarehouseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del almacen donde van a salir los productos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'WarehouseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'WarehouseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del documento (DATETIME) de devolución; fecha operativa del comprobante devolutivo.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la fecha del documento de devolucion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) de la unidad operativa, centro de costo o sucursal que genera la devolución; referencia a Common.OperatingUnit.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad operativa', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único (VARCHAR 20) asignado al comprobante de devolución; identificación normalizada en inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la devolucion del comprobante de entrada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, PK IDENTITY) del registro de devolución del comprobante de entrada en la tabla.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la devolucion del comprobante de entrada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Devoluciones de compras (entradas de almacén). Registra las devoluciones a proveedor originadas a partir de un comprobante de entrada de inventario, incluyendo valores de flete, descuentos, impuestos, retenciones y deducciones asociadas a cada devolución.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution';
