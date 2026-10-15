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
ALTER TABLE [MedicalFees].[MedicalFeesLiquidationDetail] NOCHECK CONSTRAINT [FK_MedicalFeesLiquidationDetail_MedicalFeesCausation];


GO
ALTER TABLE [MedicalFees].[MedicalFeesLiquidationDetail] NOCHECK CONSTRAINT [FK_MedicalFeesLiquidationDetail_MedicalFeesLiquidation];


GO
CREATE NONCLUSTERED INDEX [IX_MedicalFees_MedicalFeesLiquidationDetail__MedicalFeesCausationId]
    ON [MedicalFees].[MedicalFeesLiquidationDetail]([MedicalFeesCausationId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la causacion que se va a pagar dentro del documento de la liquidacion', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidationDetail', @level2type = N'COLUMN', @level2name = N'MedicalFeesCausationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera de la liquidacion', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidationDetail', @level2type = N'COLUMN', @level2name = N'MedicalFeesLiquidacionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de liquidacion  1 - Pago  2 - Descuento  3 - Glosa    Pago: Corresponde a los items que se van a pagar en la liquidacion    Descuento: Corresponde a los items que se van a descontar producto de una anulacion de factura ya liquidada.    Glosa: Corresponde a los items que se van a descontar producto de una glosa aceptada por la IPS a raiz de una falla exclusiva del profesional de salud. Estos items se calculan siempre y cuando el contrato asociado al profesional que presto el servicio tenga el parametro de Descuento Automatico por Aceptacion de Glosas en verdadero.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidationDetail', @level2type = N'COLUMN', @level2name = N'LiquidationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidationDetail', @level2type = N'COLUMN', @level2name = N'Id';

