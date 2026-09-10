CREATE TABLE [Budget].[ReimbursementResourceDetaill] (
    [Id]                         INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ReimbursementResourceId]    INT          NOT NULL,
    [PaymentOrderDetailId]       INT          NOT NULL,
    [ObligationModificationId]   INT          NULL,
    [CommitmentModificationId]   INT          NULL,
    [AvailabilityModificationId] INT          NULL,
    [BudgetModificationId]       INT          NULL,
    [Value]                      NUMERIC (18) CONSTRAINT [DF__Reimburse__Value__7AC7528F] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_ReimbursementResourceDetaill] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ReimbursementResourceDetaill_AvailabilityModification] FOREIGN KEY ([AvailabilityModificationId]) REFERENCES [Budget].[AvailabilityModification] ([Id]),
    CONSTRAINT [FK_ReimbursementResourceDetaill_BudgetModification] FOREIGN KEY ([BudgetModificationId]) REFERENCES [Budget].[BudgetModification] ([Id]),
    CONSTRAINT [FK_ReimbursementResourceDetaill_CommitmentModification] FOREIGN KEY ([CommitmentModificationId]) REFERENCES [Budget].[CommitmentModification] ([Id]),
    CONSTRAINT [FK_ReimbursementResourceDetaill_ObligationModification] FOREIGN KEY ([ObligationModificationId]) REFERENCES [Budget].[ObligationModification] ([Id]),
    CONSTRAINT [FK_ReimbursementResourceDetaill_PaymentOrderDetail] FOREIGN KEY ([PaymentOrderDetailId]) REFERENCES [Budget].[PaymentOrderDetail] ([Id]),
    CONSTRAINT [FK_ReimbursementResourceDetaill_ReimbursementResource] FOREIGN KEY ([ReimbursementResourceId]) REFERENCES [Budget].[ReimbursementResource] ([Id])
);




GO



GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto o valor numérico del detalle de reintegro/reembolso (NUMERIC 18, default 0). Representa el importe económico asociado a la línea de reintegro presupuestal.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ReimbursementResourceDetaill', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Documento', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ReimbursementResourceDetaill', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ReimbursementResourceDetaill', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la modificación presupuestal vinculada (FK → Budget.BudgetModification). Permite rastrear cambios al presupuesto original del reintegro.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ReimbursementResourceDetaill', @level2type = N'COLUMN', @level2name = N'BudgetModificationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de modificación de presupuesto', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ReimbursementResourceDetaill', @level2type = N'COLUMN', @level2name = N'BudgetModificationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ReimbursementResourceDetaill', @level2type = N'COLUMN', @level2name = N'BudgetModificationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la modificación a la disponibilidad presupuestal (FK → Budget.AvailabilityModification). Registra ajustes en la disponibilidad de recursos para el reintegro.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ReimbursementResourceDetaill', @level2type = N'COLUMN', @level2name = N'AvailabilityModificationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la modificaicon a la disponibilidad', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ReimbursementResourceDetaill', @level2type = N'COLUMN', @level2name = N'AvailabilityModificationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ReimbursementResourceDetaill', @level2type = N'COLUMN', @level2name = N'AvailabilityModificationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la modificación al compromiso presupuestal (FK → Budget.CommitmentModification). Documenta cambios en el compromiso de gasto asociado al reintegro.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ReimbursementResourceDetaill', @level2type = N'COLUMN', @level2name = N'CommitmentModificationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la modificacion al compromiso', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ReimbursementResourceDetaill', @level2type = N'COLUMN', @level2name = N'CommitmentModificationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ReimbursementResourceDetaill', @level2type = N'COLUMN', @level2name = N'CommitmentModificationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la modificación a la obligación presupuestal (FK → Budget.ObligationModification). Vincula cambios en la obligación de pago del reintegro.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ReimbursementResourceDetaill', @level2type = N'COLUMN', @level2name = N'ObligationModificationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la modificacion de la obligacion', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ReimbursementResourceDetaill', @level2type = N'COLUMN', @level2name = N'ObligationModificationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ReimbursementResourceDetaill', @level2type = N'COLUMN', @level2name = N'ObligationModificationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle de orden de pago (FK → Budget.PaymentOrderDetail). Enlaza cada línea de reintegro con su correspondiente detalle de orden de pago.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ReimbursementResourceDetaill', @level2type = N'COLUMN', @level2name = N'PaymentOrderDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de detalle de orden de pago', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ReimbursementResourceDetaill', @level2type = N'COLUMN', @level2name = N'PaymentOrderDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ReimbursementResourceDetaill', @level2type = N'COLUMN', @level2name = N'PaymentOrderDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera/encabezado del reintegro (FK → Budget.ReimbursementResource). Clave foránea que agrupa este detalle a su reintegro principal.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ReimbursementResourceDetaill', @level2type = N'COLUMN', @level2name = N'ReimbursementResourceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera del reintegro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ReimbursementResourceDetaill', @level2type = N'COLUMN', @level2name = N'ReimbursementResourceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ReimbursementResourceDetaill', @level2type = N'COLUMN', @level2name = N'ReimbursementResourceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del detalle de reintegro (PK, INT IDENTITY). Clave primaria que identifica cada línea individual del registro de reintegro presupuestal.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ReimbursementResourceDetaill', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle del reintegro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ReimbursementResourceDetaill', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ReimbursementResourceDetaill', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de recursos de reembolso presupuestal: registra el desglose de cada reembolso vinculando la orden de pago, las modificaciones presupuestales (obligación, compromiso, disponibilidad y modificación general) y el valor monetario asociado a cada movimiento de reintegro de fondos.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ReimbursementResourceDetaill';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ReimbursementResourceDetaill';
