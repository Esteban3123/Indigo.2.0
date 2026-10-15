CREATE TABLE [FixedAsset].[FixedAssetParameters] (
    [Id]                                         INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [OperatingUnitId]                            INT          NOT NULL,
    [ProcessDate]                                DATE         NOT NULL,
    [IdIngressAccountingVoucher]                 INT          NOT NULL,
    [IdDepreciationAccountingVoucher]            INT          NOT NULL,
    [IdAditionAccountingVoucher]                 INT          NOT NULL,
    [IdOutputAccountingVoucher]                  INT          NOT NULL,
    [IdValorizationDevaluationAccountingVoucher] INT          NOT NULL,
    [IdOtherIngressAccountingAccount]            INT          NOT NULL,
    [IdDonationAccountingAccount]                INT          NOT NULL,
    [IdTransferPropertyAccountingAccount]        INT          NOT NULL,
    [IdOtherConceptsAccountingAccount]           INT          NOT NULL,
    [IdRecuperationAccountingAccount]            INT          NOT NULL,
    [LowBidAmount]                               NUMERIC (18) NOT NULL,
    [TopMinorValue]                              NUMERIC (18) NOT NULL,
    [IvaCost]                                    BIT          NOT NULL,
    [IdThirdPartyResponsible]                    INT          NOT NULL,
    CONSTRAINT [PK_FixedAssetParameters_1] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Jefe de Activos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'IdThirdPartyResponsible';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Incluir iva al costo (1 - SI, 0 - NO)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'IvaCost';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor tope de menor cuantía', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'TopMinorValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor minimo de menor cuantia', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'LowBidAmount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Cuenta Contable de Recuperación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'IdRecuperationAccountingAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Cuenta Contable Ingreso Otros Conceptos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'IdOtherConceptsAccountingAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Cuenta Contable Ingreso Traspaso de Bienes', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'IdTransferPropertyAccountingAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Cuenta Contable Ingresos x Donación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'IdDonationAccountingAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Cuenta Contable Otros Ingresos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'IdOtherIngressAccountingAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Comprobante Contable de Valorización y Desvalorización', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'IdValorizationDevaluationAccountingVoucher';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Tipo Comprobante Salida', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'IdOutputAccountingVoucher';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Tipo Comprobante Adición', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'IdAditionAccountingVoucher';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Tipo de Comprobante de Depreciación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'IdDepreciationAccountingVoucher';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Tipo de Comprobante de Ingreso', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'IdIngressAccountingVoucher';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Proceso', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'ProcessDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la Unidad Operativa', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de los Parámetros de Activos Fijos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'Id';

