CREATE TABLE [FixedAsset].[FixedAssetActiveOutputDetail] (
    [Id]                       INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FixedAssetActiveOutputId] INT             NOT NULL,
    [ActiveType]               TINYINT         NOT NULL,
    [PhysicalAssetId]          INT             NULL,
    [PhysicalAssetPartsId]     INT             NULL,
    [MainAccountId]            INT             NOT NULL,
    [OutputType]               TINYINT         NOT NULL,
    [LowType]                  TINYINT         NULL,
    [SalesValue]               DECIMAL (18, 2) NOT NULL,
    [ThirdPartyId]             INT             NULL,
    [AccountReceivableId]      INT             NULL,
    CONSTRAINT [PK_FixedAssetActiveOutputDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetActiveOutputDetail_AccountReceivable] FOREIGN KEY ([AccountReceivableId]) REFERENCES [Portfolio].[AccountReceivable] ([Id]),
    CONSTRAINT [FK_FixedAssetActiveOutputDetail_FixedAssetActiveOutput] FOREIGN KEY ([FixedAssetActiveOutputId]) REFERENCES [FixedAsset].[FixedAssetActiveOutput] ([Id]),
    CONSTRAINT [FK_FixedAssetActiveOutputDetail_FixedAssetPhysicalAsset] FOREIGN KEY ([PhysicalAssetId]) REFERENCES [FixedAsset].[FixedAssetPhysicalAsset] ([Id]),
    CONSTRAINT [FK_FixedAssetActiveOutputDetail_FixedAssetPhysicalAssetParts] FOREIGN KEY ([PhysicalAssetPartsId]) REFERENCES [FixedAsset].[FixedAssetPhysicalAssetParts] ([Id]),
    CONSTRAINT [FK_FixedAssetActiveOutputDetail_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_FixedAssetActiveOutputDetail_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);




GO



GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cuenta por cobrar asociada al activo; vinculación con cartera de Portfolio.AccountReceivable para seguimiento de ingresos pendientes', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetActiveOutputDetail', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id de la cuenta por cobrar', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetActiveOutputDetail', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetActiveOutputDetail', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tercero (cliente, proveedor, entidad) receptor en operaciones de venta o consignación de activos; solo se completa si OutputType=Venta(2) o Bienes Transferidos; referencia a Common.ThirdParty', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetActiveOutputDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id del tercero al cual se le va a realizar la venta o consignacion    Este campo solo se habilita si el tipo de salida es Venta o Bienes Transferidos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetActiveOutputDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetActiveOutputDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario de salida del activo fijo en venta, consignación o baja; DECIMAL(18,2); representa precio de enajenación para cálculo contable y fiscal', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetActiveOutputDetail', @level2type = N'COLUMN', @level2name = N'SalesValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor de venta del activo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetActiveOutputDetail', @level2type = N'COLUMN', @level2name = N'SalesValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetActiveOutputDetail', @level2type = N'COLUMN', @level2name = N'SalesValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación de retiro/baja del activo (ID de FixedAssetRetirementTypes); solo obligatorio si OutputType=Baja(1); valores nulos indican no aplica; tipifica motivo de baja (daño, obsolescencia, etc.)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetActiveOutputDetail', @level2type = N'COLUMN', @level2name = N'LowType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de baja  Null = No Aplica  El valor corresponde al Id del Tipo de baja de la tabla FixedAssetRetirementTypes    Este campo solo se solicita si el tipo de salida es Baja, de lo contrario se deja en Null', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetActiveOutputDetail', @level2type = N'COLUMN', @level2name = N'LowType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetActiveOutputDetail', @level2type = N'COLUMN', @level2name = N'LowType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación de salida del activo: 1=Baja/Retiro, 2=Venta/Enajenación; determina flujo contable y campos requeridos (ThirdPartyId, LowType); TINYINT', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetActiveOutputDetail', @level2type = N'COLUMN', @level2name = N'OutputType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de salida  1 = Baja  2 = Venta', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetActiveOutputDetail', @level2type = N'COLUMN', @level2name = N'OutputType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetActiveOutputDetail', @level2type = N'COLUMN', @level2name = N'OutputType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cuenta contable principal del activo fijo en mayores (GeneralLedger.MainAccounts); registra movimiento en patrimonio/activos al momento de salida', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetActiveOutputDetail', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id de la cuenta contable del Activo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetActiveOutputDetail', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetActiveOutputDetail', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de componente o parte constitutiva del activo cuando ActiveType=2; referencia a FixedAssetPhysicalAssetParts; NULL si se retira activo completo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetActiveOutputDetail', @level2type = N'COLUMN', @level2name = N'PhysicalAssetPartsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id de la Parte del Activo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetActiveOutputDetail', @level2type = N'COLUMN', @level2name = N'PhysicalAssetPartsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetActiveOutputDetail', @level2type = N'COLUMN', @level2name = N'PhysicalAssetPartsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del activo fijo físico completo objeto de salida; referencia a FixedAssetPhysicalAsset; NULL si se retira solo componente (PhysicalAssetPartsId)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetActiveOutputDetail', @level2type = N'COLUMN', @level2name = N'PhysicalAssetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id del Activo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetActiveOutputDetail', @level2type = N'COLUMN', @level2name = N'PhysicalAssetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetActiveOutputDetail', @level2type = N'COLUMN', @level2name = N'PhysicalAssetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de bien en salida: 1=Activo completo, 2=Componente/Parte; TINYINT; determina si se usa PhysicalAssetId o PhysicalAssetPartsId', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetActiveOutputDetail', @level2type = N'COLUMN', @level2name = N'ActiveType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Tipo de Activo  1 - Activo  2 - Parte', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetActiveOutputDetail', @level2type = N'COLUMN', @level2name = N'ActiveType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetActiveOutputDetail', @level2type = N'COLUMN', @level2name = N'ActiveType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador foráneo del encabezado de salida de activo (FixedAsset.FixedAssetActiveOutput); agrupa detalles múltiples bajo una misma operación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetActiveOutputDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetActiveOutputId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de salida de activo fijo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetActiveOutputDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetActiveOutputId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetActiveOutputDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetActiveOutputId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave primaria autonumérica IDENTITY(1,1); identifica unívocamente cada línea de detalle en operación de salida de activo fijo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetActiveOutputDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetActiveOutputDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetActiveOutputDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de las salidas o bajas de activos fijos activos. Registra cada línea de una salida de activo, indicando el tipo de activo, el motivo de la baja, el valor de venta, la cuenta contable principal y el tercero involucrado en la transacción.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetActiveOutputDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetActiveOutputDetail';
