CREATE TABLE [FixedAsset].[FixedAssetResponsible] (
    [Id]                INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]              VARCHAR (20) NOT NULL,
    [ThirdPartyId]      INT          NOT NULL,
    [VinculationTypeId] INT          NOT NULL,
    [ReponsibleTypeId]  INT          NOT NULL,
    [Status]            BIT          NOT NULL,
    [CreationUser]      VARCHAR (20) NOT NULL,
    [CreationDate]      DATETIME     NOT NULL,
    [ModificationUser]  VARCHAR (20) NULL,
    [ModificationDate]  DATETIME     NULL,
    [TimeStamp]         ROWVERSION   NOT NULL,
    [Capacity]          TINYINT      NULL,
    CONSTRAINT [PK_FixedAssetResponsible__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Responsible_ResponsibleType] FOREIGN KEY ([ReponsibleTypeId]) REFERENCES [FixedAsset].[ResponsibleType] ([Id]),
    CONSTRAINT [FK_Responsible_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_Responsible_VinculationType] FOREIGN KEY ([VinculationTypeId]) REFERENCES [FixedAsset].[FixedAssetVinculationType] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetResponsible] NOCHECK CONSTRAINT [FK_Responsible_ResponsibleType];


GO
ALTER TABLE [FixedAsset].[FixedAssetResponsible] NOCHECK CONSTRAINT [FK_Responsible_ThirdParty];


GO
ALTER TABLE [FixedAsset].[FixedAssetResponsible] NOCHECK CONSTRAINT [FK_Responsible_VinculationType];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_FixedAssetResponsible__Code]
    ON [FixedAsset].[FixedAssetResponsible]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetResponsible', @level2type = N'COLUMN', @level2name = N'TimeStamp';

