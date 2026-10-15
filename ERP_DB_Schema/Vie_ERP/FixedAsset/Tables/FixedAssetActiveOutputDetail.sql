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
ALTER TABLE [FixedAsset].[FixedAssetActiveOutputDetail] NOCHECK CONSTRAINT [FK_FixedAssetActiveOutputDetail_AccountReceivable];


GO
ALTER TABLE [FixedAsset].[FixedAssetActiveOutputDetail] NOCHECK CONSTRAINT [FK_FixedAssetActiveOutputDetail_FixedAssetActiveOutput];


GO
ALTER TABLE [FixedAsset].[FixedAssetActiveOutputDetail] NOCHECK CONSTRAINT [FK_FixedAssetActiveOutputDetail_FixedAssetPhysicalAsset];


GO
ALTER TABLE [FixedAsset].[FixedAssetActiveOutputDetail] NOCHECK CONSTRAINT [FK_FixedAssetActiveOutputDetail_FixedAssetPhysicalAssetParts];


GO
ALTER TABLE [FixedAsset].[FixedAssetActiveOutputDetail] NOCHECK CONSTRAINT [FK_FixedAssetActiveOutputDetail_MainAccounts];


GO
ALTER TABLE [FixedAsset].[FixedAssetActiveOutputDetail] NOCHECK CONSTRAINT [FK_FixedAssetActiveOutputDetail_ThirdParty];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id de la cuenta por cobrar', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetActiveOutputDetail', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el id del tercero al cual se le va a realizar la venta o consignacion    Este campo solo se habilita si el tipo de salida es Venta o Bienes Transferidos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetActiveOutputDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor de venta del activo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetActiveOutputDetail', @level2type = N'COLUMN', @level2name = N'SalesValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de baja  Null = No Aplica  El valor corresponde al Id del Tipo de baja de la tabla FixedAssetRetirementTypes    Este campo solo se solicita si el tipo de salida es Baja, de lo contrario se deja en Null', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetActiveOutputDetail', @level2type = N'COLUMN', @level2name = N'LowType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de salida  1 = Baja  2 = Venta', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetActiveOutputDetail', @level2type = N'COLUMN', @level2name = N'OutputType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el id de la cuenta contable del Activo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetActiveOutputDetail', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id de la Parte del Activo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetActiveOutputDetail', @level2type = N'COLUMN', @level2name = N'PhysicalAssetPartsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id del Activo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetActiveOutputDetail', @level2type = N'COLUMN', @level2name = N'PhysicalAssetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Tipo de Activo  1 - Activo  2 - Parte', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetActiveOutputDetail', @level2type = N'COLUMN', @level2name = N'ActiveType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de salida de activo fijo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetActiveOutputDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetActiveOutputId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetActiveOutputDetail', @level2type = N'COLUMN', @level2name = N'Id';

