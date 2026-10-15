CREATE TABLE [Cost].[CostDistributionFixedAsset] (
    [Id]                        INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                      VARCHAR (20)    NOT NULL,
    [FixedAssetPhysicalAssetId] INT             NOT NULL,
    [FixedAssetCode]            VARCHAR (20)    CONSTRAINT [DF_CostDistributionFixedAsset_FixedAssetCode] DEFAULT ('') NOT NULL,
    [DepreciationValue]         DECIMAL (18, 2) NOT NULL,
    [Description]               VARCHAR (300)   NOT NULL,
    [Year]                      INT             NOT NULL,
    [Month]                     INT             NOT NULL,
    [Status]                    BIT             NOT NULL,
    [CreationUser]              VARCHAR (20)    NOT NULL,
    [CreationDate]              DATETIME        NOT NULL,
    [ModificationUser]          VARCHAR (20)    NULL,
    [ModificationDate]          DATETIME        NULL,
    [TimeStamp]                 ROWVERSION      NOT NULL,
    [HoursWorked]               INT             CONSTRAINT [DF_CostDistributionFixedAsset_HoursWorked] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_CostDistributionFixedAsset] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostDistributionFixedAsset_FixedAssetPhysicalAsset] FOREIGN KEY ([FixedAssetPhysicalAssetId]) REFERENCES [FixedAsset].[FixedAssetPhysicalAsset] ([Id])
);


GO
ALTER TABLE [Cost].[CostDistributionFixedAsset] NOCHECK CONSTRAINT [FK_CostDistributionFixedAsset_FixedAssetPhysicalAsset];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del de la entidad 1 - Activo  0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAsset', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor de la depreciacion del activo fijo en el mes y año correspondiente    Las tablas de depreciacion son AFNDEPRECI y AFNCALDEP', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAsset', @level2type = N'COLUMN', @level2name = N'DepreciationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del activo fijo del ERP con el que nos estemos interfazando  AFNACTIVO', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAsset', @level2type = N'COLUMN', @level2name = N'FixedAssetPhysicalAssetId';

