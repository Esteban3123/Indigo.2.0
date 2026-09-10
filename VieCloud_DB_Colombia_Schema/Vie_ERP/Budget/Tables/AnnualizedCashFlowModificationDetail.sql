CREATE TABLE [Budget].[AnnualizedCashFlowModificationDetail] (
    [Id]                   INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PACModificationId]    INT             NOT NULL,
    [AnnualizedCashFlowId] INT             NOT NULL,
    [Nature]               TINYINT         NOT NULL,
    [Value]                NUMERIC (18, 2) NOT NULL,
    CONSTRAINT [PK_PACModificationDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AnnualizedCashFlowModificationDetail_AnnualizedCashFlow] FOREIGN KEY ([AnnualizedCashFlowId]) REFERENCES [Budget].[AnnualizedCashFlow] ([Id]),
    CONSTRAINT [FK_PACModificationDetail_PACModification] FOREIGN KEY ([PACModificationId]) REFERENCES [Budget].[AnnualizedCashFlowModification] ([Id]),
    CONSTRAINT [UQ_AnnualizedCashFlowModificationDetail__PACModificationId__AnnualizedCashFlowId] UNIQUE NONCLUSTERED ([PACModificationId] ASC, [AnnualizedCashFlowId] ASC)
);




GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_AnnualizedCashFlowModificationDetail__PACModificationId]
    ON [Budget].[AnnualizedCashFlowModificationDetail]([PACModificationId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico de la modificación del flujo de caja; importe en pesos (NUMERIC 18,2); monto debitado o creditado en la línea de detalle del PAC', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlowModificationDetail', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la modificacion', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlowModificationDetail', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlowModificationDetail', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Naturaleza o tipo de aplicación contable: Débito (1) o Crédito (2); clasificación del movimiento financiero en la modificación presupuestal', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlowModificationDetail', @level2type = N'COLUMN', @level2name = N'Nature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Naturaleza de la aplicacion (Debito = 1, Credito = 2)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlowModificationDetail', @level2type = N'COLUMN', @level2name = N'Nature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlowModificationDetail', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del Flujo de Caja Anualizado (PAC); referencia a Budget.AnnualizedCashFlow; vincula el detalle al PAC padre', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlowModificationDetail', @level2type = N'COLUMN', @level2name = N'AnnualizedCashFlowId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id del PAC', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlowModificationDetail', @level2type = N'COLUMN', @level2name = N'AnnualizedCashFlowId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlowModificationDetail', @level2type = N'COLUMN', @level2name = N'AnnualizedCashFlowId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la cabecera de Modificación del PAC; referencia a Budget.AnnualizedCashFlowModification; enlaza el detalle a su documento de modificación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlowModificationDetail', @level2type = N'COLUMN', @level2name = N'PACModificationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Cabecera modificacion del pac', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlowModificationDetail', @level2type = N'COLUMN', @level2name = N'PACModificationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlowModificationDetail', @level2type = N'COLUMN', @level2name = N'PACModificationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK, IDENTITY) del detalle de modificación del PAC; clave primaria de la línea de modificación presupuestal', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlowModificationDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la modificacion del pac', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlowModificationDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlowModificationDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de modificaciones aplicadas a los flujos de caja anualizados dentro del presupuesto. Registra cada ajuste individual (modificación PAC) que afecta un flujo de caja anualizado, indicando la naturaleza del movimiento y el valor modificado.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlowModificationDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlowModificationDetail';
