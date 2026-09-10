CREATE TABLE [Inventory].[EntranceVoucherCommitment] (
    [Id]                 INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [EntranceVoucherId]  INT             NOT NULL,
    [CommitmentDetailId] INT             NOT NULL,
    [Value]              DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_EntranceVoucherCommitment] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EntranceVoucherCommitment_CommitmentDetail] FOREIGN KEY ([CommitmentDetailId]) REFERENCES [Budget].[CommitmentDetail] ([Id]),
    CONSTRAINT [FK_EntranceVoucherCommitment_EntranceVoucher] FOREIGN KEY ([EntranceVoucherId]) REFERENCES [Inventory].[EntranceVoucher] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario (DECIMAL 18,2) con el cual se genera la obligación presupuestaria en el detalle de compromiso seleccionado; monto que afecta el compromiso de gasto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherCommitment', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor con el cual se generará la obligación en el detalle seleccionado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherCommitment', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherCommitment', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del detalle del compromiso presupuestario; referencia a Budget.CommitmentDetail para vincular la obligación de pago', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherCommitment', @level2type = N'COLUMN', @level2name = N'CommitmentDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Detalle del compromiso', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherCommitment', @level2type = N'COLUMN', @level2name = N'CommitmentDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherCommitment', @level2type = N'COLUMN', @level2name = N'CommitmentDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la cabecera del comprobante de entrada de inventario; referencia a Inventory.EntranceVoucher que registra la recepción de bienes o suministros', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherCommitment', @level2type = N'COLUMN', @level2name = N'EntranceVoucherId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera del comprobante de entrada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherCommitment', @level2type = N'COLUMN', @level2name = N'EntranceVoucherId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherCommitment', @level2type = N'COLUMN', @level2name = N'EntranceVoucherId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del registro de vinculación entre comprobante de entrada y detalle de compromiso presupuestario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherCommitment', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherCommitment', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherCommitment', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona los comprobantes de entrada de inventario con los compromisos presupuestales o de compra, registrando el valor comprometido por cada ítem recibido en bodega. Permite rastrear qué compromiso (orden de compra o contrato) respalda cada entrada de mercancía.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherCommitment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherCommitment';
