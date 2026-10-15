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

