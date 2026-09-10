CREATE TABLE [MedicalFees].[MedicalFeesLiquidationDetail] (
    [Id]                       INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [LiquidationType]          TINYINT NULL,
    [MedicalFeesLiquidacionId] INT     NOT NULL,
    [MedicalFeesCausationId]   INT     NOT NULL,
    CONSTRAINT [PK_MedicalFeesLiquidationDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MedicalFeesLiquidationDetail_MedicalFeesCausation] FOREIGN KEY ([MedicalFeesCausationId]) REFERENCES [MedicalFees].[MedicalFeesCausation] ([Id]),
    CONSTRAINT [FK_MedicalFeesLiquidationDetail_MedicalFeesLiquidation] FOREIGN KEY ([MedicalFeesLiquidacionId]) REFERENCES [MedicalFees].[MedicalFeesLiquidation] ([Id])
);




GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_MedicalFees_MedicalFeesLiquidationDetail__MedicalFeesCausationId]
    ON [MedicalFees].[MedicalFeesLiquidationDetail]([MedicalFeesCausationId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la causación de honorarios médicos que se liquida, paga, descuenta o glosa (FK a MedicalFeesCausation). Referencia el concepto a procesar en el documento de liquidación', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidationDetail', @level2type = N'COLUMN', @level2name = N'MedicalFeesCausationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la causacion que se va a pagar dentro del documento de la liquidacion', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidationDetail', @level2type = N'COLUMN', @level2name = N'MedicalFeesCausationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidationDetail', @level2type = N'COLUMN', @level2name = N'MedicalFeesCausationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera/documento principal de liquidación de honorarios (FK a MedicalFeesLiquidation). Vincula el detalle con su liquidación padre', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidationDetail', @level2type = N'COLUMN', @level2name = N'MedicalFeesLiquidacionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera de la liquidacion', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidationDetail', @level2type = N'COLUMN', @level2name = N'MedicalFeesLiquidacionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidationDetail', @level2type = N'COLUMN', @level2name = N'MedicalFeesLiquidacionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de liquidación (TINYINT): 1=Pago (items a cancelar), 2=Descuento (anulación de factura ya liquidada), 3=Glosa (descuento por glosa aceptada con falla del profesional de salud, según parámetro de descuento automático en contrato)', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidationDetail', @level2type = N'COLUMN', @level2name = N'LiquidationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de liquidacion  1 - Pago  2 - Descuento  3 - Glosa    Pago: Corresponde a los items que se van a pagar en la liquidacion    Descuento: Corresponde a los items que se van a descontar producto de una anulacion de factura ya liquidada.    Glosa: Corresponde a los items que se van a descontar producto de una glosa aceptada por la IPS a raiz de una falla exclusiva del profesional de salud. Estos items se calculan siempre y cuando el contrato asociado al profesional que presto el servicio tenga el parametro de Descuento Automatico por Aceptacion de Glosas en verdadero.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidationDetail', @level2type = N'COLUMN', @level2name = N'LiquidationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidationDetail', @level2type = N'COLUMN', @level2name = N'LiquidationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del registro de detalle en la liquidación de honorarios médicos', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidationDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidationDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidationDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de las liquidaciones de honorarios médicos: registra cada ítem o línea asociada a una liquidación, vinculando el tipo de liquidación con la causación de honorarios correspondiente.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidationDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidationDetail';
