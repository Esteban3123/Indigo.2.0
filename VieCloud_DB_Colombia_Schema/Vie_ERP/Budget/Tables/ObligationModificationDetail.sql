CREATE TABLE [Budget].[ObligationModificationDetail] (
    [Id]                       INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ObligationModificationId] INT          NOT NULL,
    [ObligationDetailId]       INT          NOT NULL,
    [ExpiredDate]              DATETIME     NOT NULL,
    [Nature]                   TINYINT      NOT NULL,
    [Value]                    NUMERIC (18) NOT NULL,
    [IsLogBase]                BIT          NOT NULL,
    CONSTRAINT [PK_ObligationModificationDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ObligationModificationDetail_ObligationDetail] FOREIGN KEY ([ObligationDetailId]) REFERENCES [Budget].[ObligationDetail] ([Id]),
    CONSTRAINT [FK_ObligationModificationDetail_ObligationModification] FOREIGN KEY ([ObligationModificationId]) REFERENCES [Budget].[ObligationModification] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) que determina si el registro es registro base o auditado de la obligación; marca origen/historial del cambio.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ObligationModificationDetail', @level2type = N'COLUMN', @level2name = N'IsLogBase';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Determina si el registro es base de la obligacion', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ObligationModificationDetail', @level2type = N'COLUMN', @level2name = N'IsLogBase';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ObligationModificationDetail', @level2type = N'COLUMN', @level2name = N'IsLogBase';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico (NUMERIC 18) del movimiento de obligación; monto en pesos del débito o crédito aplicado en la modificación.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ObligationModificationDetail', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ObligationModificationDetail', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ObligationModificationDetail', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Naturaleza del movimiento (TINYINT): 0=NINGUNA, 1=DEBITO (disminuye obligación), 2=CREDITO (aumenta obligación); tipo de ajuste contable.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ObligationModificationDetail', @level2type = N'COLUMN', @level2name = N'Nature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Naturaleza (NINGUNA = 0,DEBITO = 1,CREDITO = 2)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ObligationModificationDetail', @level2type = N'COLUMN', @level2name = N'Nature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ObligationModificationDetail', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de vencimiento (DATETIME) del detalle modificado; indica cuándo vence la obligación, contrato, factura o compromiso presupuestal.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ObligationModificationDetail', @level2type = N'COLUMN', @level2name = N'ExpiredDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de vencimieno', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ObligationModificationDetail', @level2type = N'COLUMN', @level2name = N'ExpiredDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ObligationModificationDetail', @level2type = N'COLUMN', @level2name = N'ExpiredDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle de obligación original (FK a ObligationDetail); referencia el rubro, concepto o línea presupuestal modificado.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ObligationModificationDetail', @level2type = N'COLUMN', @level2name = N'ObligationDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id detalle de la oblicacion asociada', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ObligationModificationDetail', @level2type = N'COLUMN', @level2name = N'ObligationDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ObligationModificationDetail', @level2type = N'COLUMN', @level2name = N'ObligationDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera de modificación de obligación asociada (FK a ObligationModification); vincula el detalle al evento de cambio presupuestal.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ObligationModificationDetail', @level2type = N'COLUMN', @level2name = N'ObligationModificationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Cabecera de la modificacion de la obligacion', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ObligationModificationDetail', @level2type = N'COLUMN', @level2name = N'ObligationModificationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ObligationModificationDetail', @level2type = N'COLUMN', @level2name = N'ObligationModificationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (IDENTITY) del registro de detalle de modificación de obligación; clave primaria de la tabla.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ObligationModificationDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ObligationModificationDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ObligationModificationDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de las modificaciones realizadas a las obligaciones presupuestales. Registra cada cambio individual (adición, reducción o ajuste) sobre una obligación, incluyendo el valor modificado, la fecha de vencimiento y la naturaleza del movimiento.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ObligationModificationDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ObligationModificationDetail';
