CREATE TABLE [MixingStation].[MedicinesProduction] (
    [Id]                INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CMConfigurationId] INT       NOT NULL,
    [ATCId]             INT       NOT NULL,
    [UnitDoseTypeId]    INT       NOT NULL,
    [Status]            BIT       NOT NULL,
    [CenterAttentionId] CHAR (10) NULL,
    [AllowsRemnant]     BIT       CONSTRAINT [DF__Medicines__Allow__62F5F1EC] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_MedicinesProduction] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MedicinesProduction_ATC] FOREIGN KEY ([ATCId]) REFERENCES [Inventory].[ATC] ([Id]),
    CONSTRAINT [FK_MedicinesProduction_CMConfiguration] FOREIGN KEY ([CMConfigurationId]) REFERENCES [MixingStation].[CMConfiguration] ([Id]),
    CONSTRAINT [FK_MedicinesProduction_UnitDoseType] FOREIGN KEY ([UnitDoseTypeId]) REFERENCES [MixingStation].[UnitDoseType] ([Id])
);


GO
ALTER TABLE [MixingStation].[MedicinesProduction] NOCHECK CONSTRAINT [FK_MedicinesProduction_ATC];


GO
ALTER TABLE [MixingStation].[MedicinesProduction] NOCHECK CONSTRAINT [FK_MedicinesProduction_CMConfiguration];


GO
ALTER TABLE [MixingStation].[MedicinesProduction] NOCHECK CONSTRAINT [FK_MedicinesProduction_UnitDoseType];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'permite remitente 1 = si  0 = No', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MedicinesProduction', @level2type = N'COLUMN', @level2name = N'AllowsRemnant';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MedicinesProduction', @level2type = N'COLUMN', @level2name = N'CenterAttentionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MedicinesProduction', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tipo de dosis unitaria', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MedicinesProduction', @level2type = N'COLUMN', @level2name = N'UnitDoseTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del medicamento', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MedicinesProduction', @level2type = N'COLUMN', @level2name = N'ATCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la central de mezclas', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MedicinesProduction', @level2type = N'COLUMN', @level2name = N'CMConfigurationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MedicinesProduction', @level2type = N'COLUMN', @level2name = N'Id';

