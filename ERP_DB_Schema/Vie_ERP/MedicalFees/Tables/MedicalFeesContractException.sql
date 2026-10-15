CREATE TABLE [MedicalFees].[MedicalFeesContractException] (
    [Id]                     INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [MedicalFeesConctractId] INT            NOT NULL,
    [ExceptionType]          TINYINT        NOT NULL,
    [IPSServiceId]           INT            NULL,
    [CUPSEntityId]           INT            NULL,
    [CUPSSubgroupId]         INT            NULL,
    [CUPSGroupId]            INT            NULL,
    [CareGroupId]            INT            NULL,
    [ContractEntityId]       INT            NULL,
    [RateManualType]         TINYINT        NULL,
    [RateType]               TINYINT        NOT NULL,
    [PercentageRate]         NUMERIC (5, 2) NULL,
    [RateManualId]           INT            NULL,
    [RateVariation]          NUMERIC (5, 2) NULL,
    [AmountPayable]          NUMERIC (18)   NULL,
    CONSTRAINT [PK_MedicalFeesContractGeneralException] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MedicalFeesContractException_CupsGroup] FOREIGN KEY ([CUPSGroupId]) REFERENCES [Contract].[CupsGroup] ([Id]),
    CONSTRAINT [FK_MedicalFeesContractException_CupsSubgroup] FOREIGN KEY ([CUPSSubgroupId]) REFERENCES [Contract].[CupsSubgroup] ([Id]),
    CONSTRAINT [FK_MedicalFeesContractException_IPSService] FOREIGN KEY ([IPSServiceId]) REFERENCES [Contract].[IPSService] ([Id]),
    CONSTRAINT [FK_MedicalFeesContractException_MedicalFeesContract] FOREIGN KEY ([MedicalFeesConctractId]) REFERENCES [MedicalFees].[MedicalFeesContract] ([Id]),
    CONSTRAINT [FK_MedicalFeesContractGeneralException_CareGroup] FOREIGN KEY ([CareGroupId]) REFERENCES [Contract].[CareGroup] ([Id]),
    CONSTRAINT [FK_MedicalFeesContractGeneralException_ContractEntity] FOREIGN KEY ([ContractEntityId]) REFERENCES [Contract].[ContractEntity] ([Id]),
    CONSTRAINT [FK_MedicalFeesContractGeneralException_CUPSEntity] FOREIGN KEY ([CUPSEntityId]) REFERENCES [Contract].[CUPSEntity] ([Id])
);


GO
ALTER TABLE [MedicalFees].[MedicalFeesContractException] NOCHECK CONSTRAINT [FK_MedicalFeesContractException_CupsGroup];


GO
ALTER TABLE [MedicalFees].[MedicalFeesContractException] NOCHECK CONSTRAINT [FK_MedicalFeesContractException_CupsSubgroup];


GO
ALTER TABLE [MedicalFees].[MedicalFeesContractException] NOCHECK CONSTRAINT [FK_MedicalFeesContractException_IPSService];


GO
ALTER TABLE [MedicalFees].[MedicalFeesContractException] NOCHECK CONSTRAINT [FK_MedicalFeesContractException_MedicalFeesContract];


GO
ALTER TABLE [MedicalFees].[MedicalFeesContractException] NOCHECK CONSTRAINT [FK_MedicalFeesContractGeneralException_CareGroup];


GO
ALTER TABLE [MedicalFees].[MedicalFeesContractException] NOCHECK CONSTRAINT [FK_MedicalFeesContractGeneralException_ContractEntity];


GO
ALTER TABLE [MedicalFees].[MedicalFeesContractException] NOCHECK CONSTRAINT [FK_MedicalFeesContractGeneralException_CUPSEntity];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica el valor fijo a liquidar si el tipo es 3', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException', @level2type = N'COLUMN', @level2name = N'AmountPayable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje de la variacion de la tarifa', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException', @level2type = N'COLUMN', @level2name = N'RateVariation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica el manual de tarifas con el que se va a realizar el calculo de la causacion si el tipo es 2', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException', @level2type = N'COLUMN', @level2name = N'RateManualId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica el porcentaje a liquidar si el tipo es 1', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException', @level2type = N'COLUMN', @level2name = N'PercentageRate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Tarifa  1 - Por % del valor cobrado  2 - Por Manual Tarifario  3 - Por Valor Fijo  ', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException', @level2type = N'COLUMN', @level2name = N'RateType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo del manual tarifario  1 - ISS 2001  2 - ISS 2004  3 - SOAT', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException', @level2type = N'COLUMN', @level2name = N'RateManualType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la Entidad', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException', @level2type = N'COLUMN', @level2name = N'ContractEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Grupo de Atencion', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException', @level2type = N'COLUMN', @level2name = N'CareGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del grupo de CUPS', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException', @level2type = N'COLUMN', @level2name = N'CUPSGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del subgrupo del CUPS', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException', @level2type = N'COLUMN', @level2name = N'CUPSSubgroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del CUPS', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id del servicio IPS', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException', @level2type = N'COLUMN', @level2name = N'IPSServiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Regla  1 - Servicio IPS  2 - CUPS  3 - SubGrupo CUPS  4 - Grupo CUPS  5 - Grupo de Atencion  6 - Entidad  7 - Tipo de Manual Tarifario  8 - General', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException', @level2type = N'COLUMN', @level2name = N'ExceptionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla de tarifas para honorarios generales', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException', @level2type = N'COLUMN', @level2name = N'MedicalFeesConctractId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException', @level2type = N'COLUMN', @level2name = N'Id';

