CREATE TABLE [dbo].[TableJournalVoucher] (
    [Id]            INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdVoucherType] INT           NOT NULL,
    [VoucherDate]   DATETIME      NOT NULL,
    [Imported]      BIT           NOT NULL,
    [Status]        TINYINT       NOT NULL,
    [Detail]        VARCHAR (500) NULL,
    [EntityCode]    VARCHAR (20)  NULL,
    [EntityId]      INT           NULL,
    [EntityName]    VARCHAR (250) NULL,
    [IsClosedYear]  BIT           NOT NULL,
    CONSTRAINT [PK_TableJournalVoucher__Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Comprobantes contables del libro diario (journal vouchers). Registra cada asiento o voucher contable con su fecha, tipo, estado, entidad relacionada y si pertenece a un año cerrado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TableJournalVoucher';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TableJournalVoucher';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del comprobante contable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TableJournalVoucher', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TableJournalVoucher', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de comprobante contable (por ejemplo: ingreso, egreso, ajuste, traslado). Referencia al catálogo de tipos de voucher.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TableJournalVoucher', @level2type = N'COLUMN', @level2name = N'IdVoucherType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TableJournalVoucher', @level2type = N'COLUMN', @level2name = N'IdVoucherType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del comprobante o asiento contable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TableJournalVoucher', @level2type = N'COLUMN', @level2name = N'VoucherDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TableJournalVoucher', @level2type = N'COLUMN', @level2name = N'VoucherDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el comprobante fue importado desde un sistema externo (1 = importado, 0 = creado en el sistema).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TableJournalVoucher', @level2type = N'COLUMN', @level2name = N'Imported';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TableJournalVoucher', @level2type = N'COLUMN', @level2name = N'Imported';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado actual del comprobante contable (por ejemplo: borrador, aprobado, anulado, contabilizado).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TableJournalVoucher', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TableJournalVoucher', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción o detalle textual del comprobante contable, concepto o glosa del asiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TableJournalVoucher', @level2type = N'COLUMN', @level2name = N'Detail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TableJournalVoucher', @level2type = N'COLUMN', @level2name = N'Detail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la entidad relacionada con el comprobante (puede ser un centro de costo, tercero, contrato u otra entidad de negocio).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TableJournalVoucher', @level2type = N'COLUMN', @level2name = N'EntityCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TableJournalVoucher', @level2type = N'COLUMN', @level2name = N'EntityCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador interno de la entidad relacionada con el comprobante.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TableJournalVoucher', @level2type = N'COLUMN', @level2name = N'EntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TableJournalVoucher', @level2type = N'COLUMN', @level2name = N'EntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la entidad asociada al comprobante contable (tercero, empresa, centro de atención, etc.).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TableJournalVoucher', @level2type = N'COLUMN', @level2name = N'EntityName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TableJournalVoucher', @level2type = N'COLUMN', @level2name = N'EntityName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el comprobante pertenece a un año contable cerrado (1 = año cerrado, 0 = año activo o abierto).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TableJournalVoucher', @level2type = N'COLUMN', @level2name = N'IsClosedYear';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TableJournalVoucher', @level2type = N'COLUMN', @level2name = N'IsClosedYear';
