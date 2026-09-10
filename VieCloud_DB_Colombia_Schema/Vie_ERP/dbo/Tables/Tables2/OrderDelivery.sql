CREATE TABLE [dbo].[OrderDelivery] (
    [Id]                INT  IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [OrderDetailId]     INT  NOT NULL,
    [EntryDate]         DATE NOT NULL,
    [AmountEntering]    INT  NOT NULL,
    [OutstandingAmount] INT  NOT NULL,
    [DaysDifference]    INT  NOT NULL,
    CONSTRAINT [PK_OrderDelivery__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OrderDelivery_OrderDetail] FOREIGN KEY ([OrderDetailId]) REFERENCES [dbo].[OrderDetail] ([Id])
);




GO


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de entregas parciales o totales de ítems de una orden de compra o pedido. Permite hacer seguimiento de cuánto se ha recibido, cuánto queda pendiente y en cuántos días se realizó la entrega.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OrderDelivery';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OrderDelivery';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno de cada registro de entrega.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OrderDelivery', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OrderDelivery', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia al ítem o detalle de la orden al que corresponde esta entrega.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OrderDelivery', @level2type = N'COLUMN', @level2name = N'OrderDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OrderDelivery', @level2type = N'COLUMN', @level2name = N'OrderDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que se realizó la entrega o ingreso del ítem al sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OrderDelivery', @level2type = N'COLUMN', @level2name = N'EntryDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OrderDelivery', @level2type = N'COLUMN', @level2name = N'EntryDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad recibida o entregada en esta transacción.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OrderDelivery', @level2type = N'COLUMN', @level2name = N'AmountEntering';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OrderDelivery', @level2type = N'COLUMN', @level2name = N'AmountEntering';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad pendiente por entregar o recibir después de este ingreso, saldo pendiente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OrderDelivery', @level2type = N'COLUMN', @level2name = N'OutstandingAmount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OrderDelivery', @level2type = N'COLUMN', @level2name = N'OutstandingAmount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de días transcurridos entre la fecha esperada y la fecha real de entrega, diferencia en días.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OrderDelivery', @level2type = N'COLUMN', @level2name = N'DaysDifference';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OrderDelivery', @level2type = N'COLUMN', @level2name = N'DaysDifference';
