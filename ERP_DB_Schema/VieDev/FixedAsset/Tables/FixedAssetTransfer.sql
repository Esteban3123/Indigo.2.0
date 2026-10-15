CREATE TABLE [FixedAsset].[FixedAssetTransfer] (
    [Id]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [OperatingUnitId]     INT           NOT NULL,
    [Code]                VARCHAR (20)  NOT NULL,
    [DocumentDate]        DATE          NOT NULL,
    [TransferType]        TINYINT       NOT NULL,
    [SourceLocationId]    INT           NULL,
    [SourceResponsibleId] INT           NULL,
    [TargetLocationId]    INT           NULL,
    [TargetResponsibleId] INT           NULL,
    [Observation]         VARCHAR (500) NULL,
    [Status]              TINYINT       NOT NULL,
    [CreationUser]        VARCHAR (20)  NOT NULL,
    [CreationDate]        DATETIME      NOT NULL,
    [ModificationUser]    VARCHAR (20)  NULL,
    [ModificationDate]    DATETIME      NULL,
    [ConfirmationUser]    VARCHAR (20)  NULL,
    [ConfirmationDate]    DATETIME      NULL,
    [AnnulmentUser]       VARCHAR (20)  NULL,
    [AnnulmentDate]       DATETIME      NULL,
    [TimeStamp]           ROWVERSION    NULL,
    CONSTRAINT [PK_FixedAssetTransfer] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetTransfer_FixedAssetResponsible] FOREIGN KEY ([SourceResponsibleId]) REFERENCES [FixedAsset].[FixedAssetResponsible] ([Id]),
    CONSTRAINT [FK_FixedAssetTransfer_FixedAssetResponsible1] FOREIGN KEY ([TargetResponsibleId]) REFERENCES [FixedAsset].[FixedAssetResponsible] ([Id]),
    CONSTRAINT [FK_FixedAssetTransfer_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetTransfer] NOCHECK CONSTRAINT [FK_FixedAssetTransfer_FixedAssetResponsible];


GO
ALTER TABLE [FixedAsset].[FixedAssetTransfer] NOCHECK CONSTRAINT [FK_FixedAssetTransfer_FixedAssetResponsible1];


GO
ALTER TABLE [FixedAsset].[FixedAssetTransfer] NOCHECK CONSTRAINT [FK_FixedAssetTransfer_OperatingUnit];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el estado del documento  1 - Registrado  2 - Confirmado  3 - Anulado', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransfer', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de traslado que se va a realizar  1 - Ubicacion  2 - Responsable   3 - Ubicacion y Responsable', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransfer', @level2type = N'COLUMN', @level2name = N'TransferType';

