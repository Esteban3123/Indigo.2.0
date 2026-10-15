CREATE TABLE [FixedAsset].[FixedAssetDepreciationDetail] (
    [Id]                                       INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FixedAssetDepreciationId]                 INT             NOT NULL,
    [LegalBookId]                              INT             NOT NULL,
    [ActiveClass]                              TINYINT         NOT NULL,
    [FixedAssetPhysicalAssetId]                INT             NULL,
    [FixedAssetPhysicalAssetDetailBookId]      INT             NULL,
    [FixedAssetPhysicalAssetPartsId]           INT             NULL,
    [FixedAssetPhysicalAssetPartsDetailBookId] INT             NULL,
    [DepreciationValue]                        DECIMAL (18, 2) NOT NULL,
    [LifeTime]                                 INT             NOT NULL,
    [RemainingLifeTime]                        INT             NOT NULL,
    [DepreciatedDays]                          INT             NOT NULL,
    [ValorizationValue]                        DECIMAL (18, 2) NOT NULL,
    [DevaluationValue]                         DECIMAL (18, 2) NOT NULL,
    [AdjustedValue]                            DECIMAL (18, 2) NOT NULL,
    [TransactionValue]                         DECIMAL (18, 2) CONSTRAINT [DF_FixedAssetDepreciationDetail_TransactionValue] DEFAULT ((0)) NOT NULL,
    [AccumulatedDepreciation]                  DECIMAL (18, 2) NOT NULL,
    [ResidualValue]                            DECIMAL (18, 2) NOT NULL,
    [FinantialDiscount]                        DECIMAL (18, 2) CONSTRAINT [DF__FixedAsse__Finan__6BB6EBCD] DEFAULT ((0)) NOT NULL,
    [FinantialDiscountAdjusment]               DECIMAL (18, 2) CONSTRAINT [DF__FixedAsse__Finan__6CAB1006] DEFAULT ((0)) NOT NULL,
    [FinantialDiscountDepreciation]            DECIMAL (18, 2) CONSTRAINT [DF__FixedAsse__Finan__7263E95C] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_FixedAssetDepreciationDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetDepreciationDetail_FixedAssetDepreciation] FOREIGN KEY ([FixedAssetDepreciationId]) REFERENCES [FixedAsset].[FixedAssetDepreciation] ([Id]),
    CONSTRAINT [FK_FixedAssetDepreciationDetail_LegalBook] FOREIGN KEY ([LegalBookId]) REFERENCES [GeneralLedger].[LegalBook] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetDepreciationDetail] NOCHECK CONSTRAINT [FK_FixedAssetDepreciationDetail_FixedAssetDepreciation];


GO
ALTER TABLE [FixedAsset].[FixedAssetDepreciationDetail] NOCHECK CONSTRAINT [FK_FixedAssetDepreciationDetail_LegalBook];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor que falta por depreciar o el valor en libros que tenia antes de efectuar el cierre  ', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'ResidualValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el id del detalle contable de la parte    Este campo solo se llena si la Clase del Activo es 2', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetPhysicalAssetPartsDetailBookId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id de la parte del activo    Este campo solo se llena si la Clase del Activo es 2', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetPhysicalAssetPartsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle contable del activo    Este campo solo se llena si la clase del activo es 1', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetPhysicalAssetDetailBookId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id del activo dijo    Este campo solo se llena si la Clase del Activo es 1', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetPhysicalAssetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo del activo que se esta depreciando  1 - Activo Principal  2 - Parte del Activo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'ActiveClass';

