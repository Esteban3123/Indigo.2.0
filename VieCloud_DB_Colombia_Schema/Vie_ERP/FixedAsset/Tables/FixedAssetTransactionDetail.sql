CREATE TABLE [FixedAsset].[FixedAssetTransactionDetail] (
    [Id]                      INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FixedAssetTransactionId] INT             NOT NULL,
    [TransactionClass]        TINYINT         NOT NULL,
    [PhysicalAssetId]         INT             NULL,
    [PhysicalAssetPartsId]    INT             NULL,
    [TransactionType]         TINYINT         NOT NULL,
    [ValorizationType]        TINYINT         CONSTRAINT [DF_FixedAssetTransactionDetail_ValorizationType] DEFAULT ((0)) NOT NULL,
    [AffectDepreciation]      BIT             NOT NULL,
    [Value]                   DECIMAL (18, 2) NOT NULL,
    [LifeTime]                INT             NOT NULL,
    [UnitLifeTime]            TINYINT         NOT NULL,
    [IvaPercentage]           NUMERIC (5, 2)  NOT NULL,
    [IvaValue]                DECIMAL (18, 2) NOT NULL,
    [DiscountPercentage]      NUMERIC (5, 2)  NOT NULL,
    [DiscountValue]           DECIMAL (18, 2) NOT NULL,
    [TotalValue]              DECIMAL (18, 2) NOT NULL,
    [RTFPercentage]           NUMERIC (5, 2)  CONSTRAINT [DF_FixedAssetTransactionDetail_RTFPercentage] DEFAULT ((0)) NOT NULL,
    [RTFValue]                DECIMAL (18, 2) NOT NULL,
    [AssetMainAccountId]      INT             NOT NULL,
    [Detail]                  VARCHAR (1000)  NULL,
    [IVAId]                   INT             NULL,
    CONSTRAINT [PK_FixedAssetTransactionDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetTransactionDetail_FixedAssetPhysicalAsset] FOREIGN KEY ([PhysicalAssetId]) REFERENCES [FixedAsset].[FixedAssetPhysicalAsset] ([Id]),
    CONSTRAINT [FK_FixedAssetTransactionDetail_FixedAssetPhysicalAssetParts] FOREIGN KEY ([PhysicalAssetPartsId]) REFERENCES [FixedAsset].[FixedAssetPhysicalAssetParts] ([Id]),
    CONSTRAINT [FK_FixedAssetTransactionDetail_FixedAssetTransaction] FOREIGN KEY ([FixedAssetTransactionId]) REFERENCES [FixedAsset].[FixedAssetTransaction] ([Id]),
    CONSTRAINT [FK_FixedAssetTransactionDetail_GeneralLedgerIVA] FOREIGN KEY ([IVAId]) REFERENCES [GeneralLedger].[GeneralLedgerIVA] ([Id]),
    CONSTRAINT [FK_FixedAssetTransactionDetail_MainAccounts] FOREIGN KEY ([AssetMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id])
);




GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del impuesto sobre el valor agregado (IVA) seleccionado para el item del activo fijo. FK → GeneralLedgerIVA. Null si no aplica IVA.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'IVAId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id del IVA seleccionado', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'IVAId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'IVAId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual del detalle, nota o comentario adicional del movimiento del activo fijo (hasta 1000 caracteres). Opcional.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'Detail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Detalle', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'Detail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'Detail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de cuenta contable principal (FK → MainAccounts) del catálogo de activos fijos asociado al bien o parte. Si es parte, se hereda del activo padre. Obligatorio para contabilización.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'AssetMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id del cuenta de activos del catalogo de articulos que tenga asociado el activo o la parte    Nota: si es una parte se debe sacar del catalogo que tenga el padre de la parte, es decir el activo principal', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'AssetMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'AssetMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario de retención en la fuente (retefuente) a cobrar o descontar del item, calculado sobre la base del activo. DECIMAL(18,2).', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'RTFValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la retefuente que se cobrara al item', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'RTFValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'RTFValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de retención en la fuente aplicado al item del activo fijo (0-100). NUMERIC(5,2). Defecto: 0.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'RTFPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el porcentaje de la retefuente que se le aplica al item', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'RTFPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'RTFPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total neto de la transacción: (Valor - Descuento + IVA + Retefuente). DECIMAL(18,2). Base para facturación y cuentas por cobrar.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'TotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Total del registro, Se obtiene tomando el (Value - DiscountValue + IvaValue)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'TotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'TotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor en pesos del descuento comercial aplicado al item. Calculado: Value × DiscountPercentage. Solo si genera cuentas por cobrar. DECIMAL(18,2).', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'DiscountValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del descuento, se obtiene multiplicando el lValue por el porcentaje del descuento, este campo solo se calcula si la transaccion genera cuenta por cobrar', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'DiscountValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'DiscountValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de descuento comercial del item (0-100). NUMERIC(5,2). Solo si la transacción genera cuentas por cobrar.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'DiscountPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje de descuento, este campo solo se calcula si la transaccion genera cuenta por cobrar', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'DiscountPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'DiscountPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor en pesos del impuesto IVA del activo fijo, calculado sobre la base imponible. DECIMAL(18,2). Solo si genera cuentas por cobrar.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'IvaValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del iva del Activo, este campo solo se calcula si la transaccion genera cuenta por cobrar', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'IvaValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'IvaValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tasa de IVA a aplicar al activo fijo (0-100). NUMERIC(5,2). Solo si la transacción genera cuentas por cobrar.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'IvaPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje del iva que se va a cobrar al Activo, este campo solo se calcula si la transaccion genera cuenta por cobrar', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'IvaPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'IvaPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de tiempo de vida útil: 1=Año, 2=Mes, 3=Día. TINYINT. Solo se completa si AffectDepreciation=1 (afecta depreciación).', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'UnitLifeTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Año,   2 - Mes,   3 - Dia  Solo se llena si AffectDepreciation esta en true', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'UnitLifeTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'UnitLifeTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vida útil del activo fijo en unidades (UnitLifeTime). INT. Obligatorio si AffectDepreciation=1. Requerido para cálculo de depreciación.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'LifeTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la Vida Util, solo se puede llenar si AffectDepreciation esta en true', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'LifeTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'LifeTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor base del activo fijo o parte en pesos, antes de descuentos e impuestos. DECIMAL(18,2). Base para cálculos posteriores.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si la transacción afecta depreciación/valor en libros: 1=Sí (valorización), 0=No (devaluación). BIT. Requiere LifeTime o vida útil. Afecta amortización contable.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'AffectDepreciation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si afecta depreciacion o el valor en libros, este campo solo se podriallenar en si si es una valorizacion ya que la devaluaciones nunca afecta el valor en libros  1 - Si  0 - No    Cuando se este afectando la depreciacion se debe tener en cuenta que es obligatorio que llenen alguno de los dos opciones que le damos ya sea el Valor o la Vida util, tambien pueden llenar las dos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'AffectDepreciation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'AffectDepreciation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de valorización o movimiento del activo: 0=No aplica, 1=Adición, 2=Mantenimiento, 3=Mejora, 4=Reparación. TINYINT. Defecto: 0.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'ValorizationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de valorizacion o transaccion  0 - No aplica  1 - Adicion  2 - Mantenimiento  3 - Mejora  4 - Reparacion', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'ValorizationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'ValorizationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de transacción del activo fijo: 1=Valorización (incremento), 2=Desvalorización (decremento). TINYINT. Afecta valor en libros.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'TransactionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de transaccion  1 - Valorizacion  2 - Desvalorizacion', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'TransactionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'TransactionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la parte o componente del activo fijo (FK → FixedAssetPhysicalAssetParts). Solo se completa si TransactionClass=2 (Parte). Null si es activo principal.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'PhysicalAssetPartsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id de la parte, este solo se llena si la clase del detalle de la transaccion es Parte', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'PhysicalAssetPartsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'PhysicalAssetPartsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del activo fijo principal (FK → FixedAssetPhysicalAsset). Solo se completa si TransactionClass=1 (Activo). Null si es parte.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'PhysicalAssetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id del Activo, este solo se llena si la clase del detalle de la transaccion es Activo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'PhysicalAssetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'PhysicalAssetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clase del detalle de transacción: 1=Activo fijo principal, 2=Parte o componente del activo. TINYINT. Define si usar PhysicalAssetId o PhysicalAssetPartsId.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'TransactionClass';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la clase de la transaccion que se va a realizar  1 - Activo  2 - Parte de Activo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'TransactionClass';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'TransactionClass';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la transacción padre (FK → FixedAssetTransaction). INT. Vínculo a encabezado de movimiento del activo fijo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetTransactionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de transacción de activo fijo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetTransactionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetTransactionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (autoincremental) del detalle de transacción de activo fijo. INT IDENTITY(1,1). Clave primaria.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de cada movimiento o transacción sobre activos fijos: registra los valores, porcentajes de IVA, descuentos, retenciones y vida útil asociados a cada ítem (activo o parte de activo) involucrado en una transacción de activos fijos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail';
