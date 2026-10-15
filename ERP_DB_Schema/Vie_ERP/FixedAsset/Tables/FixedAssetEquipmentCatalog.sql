CREATE TABLE [FixedAsset].[FixedAssetEquipmentCatalog] (
    [Id]                                INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                              VARCHAR (20) NOT NULL,
    [Description]                       VARCHAR (50) NOT NULL,
    [Active]                            BIT          NOT NULL,
    [IdIngressAccountingAccount]        INT          NOT NULL,
    [IdIngressLeasingAccountingAccount] INT          NOT NULL,
    [IdDebitLoanAccountingAccount]      INT          NOT NULL,
    [IdCreaditLoanAccountingAccount]    INT          NOT NULL,
    [IdDepreciationAccountingAccount]   INT          NOT NULL,
    [Status]                            BIT          NOT NULL,
    [CreationUser]                      VARCHAR (20) NOT NULL,
    [CreationDate]                      DATETIME     NOT NULL,
    [ModificationUser]                  VARCHAR (20) NULL,
    [ModificationDate]                  DATETIME     NULL,
    [TimeStamp]                         ROWVERSION   NOT NULL,
    CONSTRAINT [PK_FixedAssetEquipmentCatalog] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEquipmentCatalog', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Modificación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEquipmentCatalog', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de Modificación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEquipmentCatalog', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEquipmentCatalog', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de Creación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEquipmentCatalog', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado (1 - Activo, 0 - Inactivo)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEquipmentCatalog', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de Cuenta Crédito de Depreciación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEquipmentCatalog', @level2type = N'COLUMN', @level2name = N'IdDepreciationAccountingAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de Cuenta Crédito de Comodato', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEquipmentCatalog', @level2type = N'COLUMN', @level2name = N'IdCreaditLoanAccountingAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de Cuenta Débito de Comodato', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEquipmentCatalog', @level2type = N'COLUMN', @level2name = N'IdDebitLoanAccountingAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de cuenta de Ingreso de Leasing', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEquipmentCatalog', @level2type = N'COLUMN', @level2name = N'IdIngressLeasingAccountingAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Cuenta de Ingreso', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEquipmentCatalog', @level2type = N'COLUMN', @level2name = N'IdIngressAccountingAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Activo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEquipmentCatalog', @level2type = N'COLUMN', @level2name = N'Active';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripción', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEquipmentCatalog', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del Catálogo de Equipos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEquipmentCatalog', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Catálogo de Equipos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEquipmentCatalog', @level2type = N'COLUMN', @level2name = N'Id';

