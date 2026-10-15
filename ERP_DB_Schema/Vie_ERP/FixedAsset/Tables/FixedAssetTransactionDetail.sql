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
ALTER TABLE [FixedAsset].[FixedAssetTransactionDetail] NOCHECK CONSTRAINT [FK_FixedAssetTransactionDetail_FixedAssetPhysicalAsset];


GO
ALTER TABLE [FixedAsset].[FixedAssetTransactionDetail] NOCHECK CONSTRAINT [FK_FixedAssetTransactionDetail_FixedAssetPhysicalAssetParts];


GO
ALTER TABLE [FixedAsset].[FixedAssetTransactionDetail] NOCHECK CONSTRAINT [FK_FixedAssetTransactionDetail_FixedAssetTransaction];


GO
ALTER TABLE [FixedAsset].[FixedAssetTransactionDetail] NOCHECK CONSTRAINT [FK_FixedAssetTransactionDetail_GeneralLedgerIVA];


GO
ALTER TABLE [FixedAsset].[FixedAssetTransactionDetail] NOCHECK CONSTRAINT [FK_FixedAssetTransactionDetail_MainAccounts];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el id del IVA seleccionado', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'IVAId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Detalle', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'Detail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el id del cuenta de activos del catalogo de articulos que tenga asociado el activo o la parte    Nota: si es una parte se debe sacar del catalogo que tenga el padre de la parte, es decir el activo principal', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'AssetMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la retefuente que se cobrara al item', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'RTFValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el porcentaje de la retefuente que se le aplica al item', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'RTFPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Total del registro, Se obtiene tomando el (Value - DiscountValue + IvaValue)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'TotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del descuento, se obtiene multiplicando el lValue por el porcentaje del descuento, este campo solo se calcula si la transaccion genera cuenta por cobrar', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'DiscountValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje de descuento, este campo solo se calcula si la transaccion genera cuenta por cobrar', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'DiscountPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del iva del Activo, este campo solo se calcula si la transaccion genera cuenta por cobrar', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'IvaValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje del iva que se va a cobrar al Activo, este campo solo se calcula si la transaccion genera cuenta por cobrar', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'IvaPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Año,   2 - Mes,   3 - Dia  Solo se llena si AffectDepreciation esta en true', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'UnitLifeTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la Vida Util, solo se puede llenar si AffectDepreciation esta en true', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'LifeTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si afecta depreciacion o el valor en libros, este campo solo se podriallenar en si si es una valorizacion ya que la devaluaciones nunca afecta el valor en libros  1 - Si  0 - No    Cuando se este afectando la depreciacion se debe tener en cuenta que es obligatorio que llenen alguno de los dos opciones que le damos ya sea el Valor o la Vida util, tambien pueden llenar las dos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'AffectDepreciation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de valorizacion o transaccion  0 - No aplica  1 - Adicion  2 - Mantenimiento  3 - Mejora  4 - Reparacion', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'ValorizationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de transaccion  1 - Valorizacion  2 - Desvalorizacion', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'TransactionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el id de la parte, este solo se llena si la clase del detalle de la transaccion es Parte', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'PhysicalAssetPartsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el id del Activo, este solo se llena si la clase del detalle de la transaccion es Activo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'PhysicalAssetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la clase de la transaccion que se va a realizar  1 - Activo  2 - Parte de Activo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'TransactionClass';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de transacción de activo fijo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetTransactionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransactionDetail', @level2type = N'COLUMN', @level2name = N'Id';

