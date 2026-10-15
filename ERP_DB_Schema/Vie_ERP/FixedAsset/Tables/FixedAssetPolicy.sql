CREATE TABLE [FixedAsset].[FixedAssetPolicy] (
    [Id]               INT                                                                          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (20) MASKED WITH (FUNCTION = 'partial(0, "CodePolicy_Ofuscado", 0)') NOT NULL,
    [PolicyTypeId]     INT                                                                          NOT NULL,
    [InsuranceId]      INT                                                                          NOT NULL,
    [Name]             VARCHAR (50)                                                                 NOT NULL,
    [InitialDate]      DATE                                                                         NOT NULL,
    [EndDate]          DATE                                                                         NOT NULL,
    [Object]           VARCHAR (200)                                                                NOT NULL,
    [Status]           BIT                                                                          NOT NULL,
    [CreationUser]     VARCHAR (20)                                                                 NOT NULL,
    [CreationDate]     DATETIME                                                                     NOT NULL,
    [ModificationUser] VARCHAR (20)                                                                 NULL,
    [ModificationDate] DATETIME                                                                     NULL,
    [TimeStamp]        ROWVERSION                                                                   NOT NULL,
    CONSTRAINT [PK_Poliza_1] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Poliza_Insurance] FOREIGN KEY ([InsuranceId]) REFERENCES [FixedAsset].[FixedAssetInsurance] ([Id]),
    CONSTRAINT [FK_Poliza_PolizaType] FOREIGN KEY ([PolicyTypeId]) REFERENCES [FixedAsset].[FixedAssetPolicyType] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetPolicy] NOCHECK CONSTRAINT [FK_Poliza_Insurance];


GO
ALTER TABLE [FixedAsset].[FixedAssetPolicy] NOCHECK CONSTRAINT [FK_Poliza_PolizaType];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPolicy', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificacion', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPolicy', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificacion', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPolicy', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Creacion', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPolicy', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creacion', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPolicy', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la poliza 1 - Activo 0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPolicy', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Objeto de la poliza', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPolicy', @level2type = N'COLUMN', @level2name = N'Object';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Final de la poliza', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPolicy', @level2type = N'COLUMN', @level2name = N'EndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Inicial de la poliza', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPolicy', @level2type = N'COLUMN', @level2name = N'InitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la poliza', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPolicy', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la aseguradora', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPolicy', @level2type = N'COLUMN', @level2name = N'InsuranceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tipo de poliza', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPolicy', @level2type = N'COLUMN', @level2name = N'PolicyTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la poliza', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPolicy', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPolicy', @level2type = N'COLUMN', @level2name = N'Id';

