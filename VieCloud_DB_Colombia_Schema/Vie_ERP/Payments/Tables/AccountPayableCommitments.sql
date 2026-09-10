CREATE TABLE [Payments].[AccountPayableCommitments] (
    [Id]                 INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AccountPayableId]   INT             NOT NULL,
    [CommitmentDetailId] INT             NOT NULL,
    [Value]              DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_AccountPayableCommitments__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccountPayableCommitments_AccountPayable] FOREIGN KEY ([AccountPayableId]) REFERENCES [Payments].[AccountPayable] ([Id]),
    CONSTRAINT [FK_AccountPayableCommitments_CommitmentDetail] FOREIGN KEY ([CommitmentDetailId]) REFERENCES [Budget].[CommitmentDetail] ([Id])
);




GO



GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_AccountPayableCommitments]
    ON [Payments].[AccountPayableCommitments]([AccountPayableId] ASC, [CommitmentDetailId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto en pesos (decimal 18,2) que se asignará como obligación al detalle de compromiso presupuestario seleccionado; vinculado a la cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableCommitments', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor con el cual se generará la obligación en el detalle seleccionado', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableCommitments', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableCommitments', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle del compromiso presupuestario (FK a Budget.CommitmentDetail); línea específica de gasto o compromiso a vincular con la obligación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableCommitments', @level2type = N'COLUMN', @level2name = N'CommitmentDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Detalle del compromiso', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableCommitments', @level2type = N'COLUMN', @level2name = N'CommitmentDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableCommitments', @level2type = N'COLUMN', @level2name = N'CommitmentDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta por pagar (FK a Payments.AccountPayable); obligación financiera, factura o deuda a contabilizar', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableCommitments', @level2type = N'COLUMN', @level2name = N'AccountPayableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableCommitments', @level2type = N'COLUMN', @level2name = N'AccountPayableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableCommitments', @level2type = N'COLUMN', @level2name = N'AccountPayableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de vinculación entre cuenta por pagar y detalle de compromiso (clave primaria, secuencial)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableCommitments', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableCommitments', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableCommitments', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra los compromisos o afectaciones presupuestales asociados a cuentas por pagar, vinculando cada obligación de pago con el detalle del compromiso y el valor comprometido.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableCommitments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableCommitments';
