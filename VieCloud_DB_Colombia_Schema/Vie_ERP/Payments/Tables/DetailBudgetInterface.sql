CREATE TABLE [Payments].[DetailBudgetInterface] (
    [Id]               INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdAccountPayable] INT             NOT NULL,
    [CommitmentDetail] INT             NOT NULL,
    [Value]            DECIMAL (18, 2) NOT NULL,
    [ExpirationDate]   DATETIME        NOT NULL,
    [Contract]         VARCHAR (30)    NULL,
    [TimeStamp]        ROWVERSION      NOT NULL,
    CONSTRAINT [PK_DetailBudgetInterface] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DetailBudgetInterface_AccountPayable] FOREIGN KEY ([IdAccountPayable]) REFERENCES [Payments].[AccountPayable] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal (TIMESTAMP) del evento: instante exacto de creación, registro o modificación del detalle presupuestal. Auditoría de cambios.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DetailBudgetInterface', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DetailBudgetInterface', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DetailBudgetInterface', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número o identificador del contrato asociado (VARCHAR 30). Referencia al documento contractual que respalda el compromiso presupuestal.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DetailBudgetInterface', @level2type = N'COLUMN', @level2name = N'Contract';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contrato', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DetailBudgetInterface', @level2type = N'COLUMN', @level2name = N'Contract';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DetailBudgetInterface', @level2type = N'COLUMN', @level2name = N'Contract';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de vencimiento del compromiso presupuestal. Límite temporal para la ejecución o vigencia del gasto comprometido.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DetailBudgetInterface', @level2type = N'COLUMN', @level2name = N'ExpirationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de vencimiento', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DetailBudgetInterface', @level2type = N'COLUMN', @level2name = N'ExpirationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DetailBudgetInterface', @level2type = N'COLUMN', @level2name = N'ExpirationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario (DECIMAL 18,2) del detalle presupuestal. Monto del compromiso, gasto o asignación presupuestal en la interfaz de pagos.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DetailBudgetInterface', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DetailBudgetInterface', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DetailBudgetInterface', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle del compromiso presupuestal. Referencia a la línea específica de compromiso u obligación financiera.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DetailBudgetInterface', @level2type = N'COLUMN', @level2name = N'CommitmentDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Detalle del compromiso', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DetailBudgetInterface', @level2type = N'COLUMN', @level2name = N'CommitmentDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DetailBudgetInterface', @level2type = N'COLUMN', @level2name = N'CommitmentDetail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) de la Cuenta por Pagar. Vínculo a Payments.AccountPayable; agrupa detalles presupuestales de una misma obligación financiera.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DetailBudgetInterface', @level2type = N'COLUMN', @level2name = N'IdAccountPayable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DetailBudgetInterface', @level2type = N'COLUMN', @level2name = N'IdAccountPayable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DetailBudgetInterface', @level2type = N'COLUMN', @level2name = N'IdAccountPayable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del detalle de la interfaz presupuestal. Clave primaria; cada registro es un detalle de compromiso presupuestal.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DetailBudgetInterface', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id detalle interfaz presupuestal', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DetailBudgetInterface', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DetailBudgetInterface', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de la interfaz entre cuentas por pagar y el presupuesto: registra cada compromiso presupuestal asociado a una obligación de pago, con su valor, fecha de vencimiento y contrato relacionado.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DetailBudgetInterface';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DetailBudgetInterface';
