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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del Tercero Responsable (Jefe de Activos Fijos). FK a tabla de terceros/proveedores. Responsable de la gestión y control de activos fijos en la unidad operativa.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'IdThirdPartyResponsible';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Jefe de Activos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'IdThirdPartyResponsible';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'IdThirdPartyResponsible';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT): incluir o excluir IVA en el costo de activos fijos. 1=Sí incluir IVA, 0=No incluir. Determina tratamiento tributario en valorización inicial.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'IvaCost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Incluir iva al costo (1 - SI, 0 - NO)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'IvaCost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'IvaCost';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico tope máximo de menor cuantía para activos fijos (NUMERIC 18). Umbral superior de reclasificación contable. Define límite de capitalización vs. gasto.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'TopMinorValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor tope de menor cuantía', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'TopMinorValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'TopMinorValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico mínimo de menor cuantía para activos fijos (NUMERIC 18). Umbral inferior de reclasificación. Determina si bien se registra como activo o gasto operativo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'LowBidAmount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor minimo de menor cuantia', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'LowBidAmount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'LowBidAmount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de Cuenta Contable de Recuperación (FK). Registra movimientos por recuperación, salvamento o residual de activos dados de baja o retirados.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'IdRecuperationAccountingAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Cuenta Contable de Recuperación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'IdRecuperationAccountingAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'IdRecuperationAccountingAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de Cuenta Contable de Ingresos por Otros Conceptos (FK). Registra ingresos diversos relacionados con activos fijos no clasificados en donación ni traspaso.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'IdOtherConceptsAccountingAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Cuenta Contable Ingreso Otros Conceptos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'IdOtherConceptsAccountingAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'IdOtherConceptsAccountingAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de Cuenta Contable de Ingresos por Traspaso de Bienes (FK). Registra movimientos por transferencia, cesión o cambio de propiedad de activos entre unidades.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'IdTransferPropertyAccountingAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Cuenta Contable Ingreso Traspaso de Bienes', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'IdTransferPropertyAccountingAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'IdTransferPropertyAccountingAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de Cuenta Contable de Ingresos por Donación (FK). Registra activos fijos recibidos como donación. Movimiento contable de ingreso sin costo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'IdDonationAccountingAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Cuenta Contable Ingresos x Donación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'IdDonationAccountingAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'IdDonationAccountingAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de Cuenta Contable de Otros Ingresos (FK). Registra ingresos alternativos asociados con activos fijos no clasificados en categorías estándar.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'IdOtherIngressAccountingAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Cuenta Contable Otros Ingresos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'IdOtherIngressAccountingAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'IdOtherIngressAccountingAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del Tipo de Comprobante Contable de Valorización y Desvalorización (FK). Comprobante para ajustes contables por revaluación o deterioro de activos fijos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'IdValorizationDevaluationAccountingVoucher';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Comprobante Contable de Valorización y Desvalorización', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'IdValorizationDevaluationAccountingVoucher';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'IdValorizationDevaluationAccountingVoucher';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del Tipo de Comprobante Contable de Salida/Baja (FK). Comprobante que registra retiro, venta, pérdida o disposición de activos fijos del patrimonio.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'IdOutputAccountingVoucher';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Tipo Comprobante Salida', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'IdOutputAccountingVoucher';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'IdOutputAccountingVoucher';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del Tipo de Comprobante Contable de Adición (FK). Comprobante que registra incrementos o mejoras en activos fijos existentes.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'IdAditionAccountingVoucher';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Tipo Comprobante Adición', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'IdAditionAccountingVoucher';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'IdAditionAccountingVoucher';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del Tipo de Comprobante Contable de Depreciación (FK). Comprobante periódico para registrar gasto de depreciación y acumular desgaste de activos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'IdDepreciationAccountingVoucher';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Tipo de Comprobante de Depreciación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'IdDepreciationAccountingVoucher';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'IdDepreciationAccountingVoucher';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del Tipo de Comprobante Contable de Ingreso (FK). Comprobante que registra entrada, adquisición o capitalización inicial de activos fijos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'IdIngressAccountingVoucher';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Tipo de Comprobante de Ingreso', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'IdIngressAccountingVoucher';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'IdIngressAccountingVoucher';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de procesamiento (DATE). Fecha en que se ejecutan cálculos de depreciación, valorización y movimientos contables de activos fijos en el período.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'ProcessDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Proceso', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'ProcessDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'ProcessDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la Unidad Operativa (FK). Centro de atención, departamento o sucursal donde se aplican los parámetros de activos fijos configurados.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Unidad Operativa', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY). Clave primaria del registro de parámetros de activos fijos. Configuración centralizada de cuentas contables y políticas por unidad operativa.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de los Parámetros de Activos Fijos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parámetros de configuración del módulo de Activos Fijos por unidad operativa. Define los comprobantes contables asociados a cada tipo de movimiento (ingreso, depreciación, adición, salida, valorización), las cuentas contables para donaciones, traslados, recuperaciones y otros conceptos, así como umbrales de valor mínimo y la fecha de proceso vigente.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetParameters';
