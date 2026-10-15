CREATE TABLE [Contract].[BillingItemsRestrictionDetailCondition] (
    [Id]                              INT         IDENTITY (1, 1) NOT NULL,
    [BillingItemsRestrictionDetailId] INT         NOT NULL,
    [Operator]                        TINYINT     NOT NULL,
    [FunctionalUnitId]                INT         NULL,
    [UnitTypeId]                      TINYINT     NULL,
    [StayType]                        VARCHAR (3) NULL,
    [ManualType]                      TINYINT     NULL,
    [SurgicalGroupId]                 INT         NULL,
    [UVRRangeId]                      INT         NULL,
    [Operator2]                       TINYINT     NULL,
    [FunctionalUnitId2]               INT         NULL,
    [UnitTypeId2]                     TINYINT     NULL,
    [StayType2]                       VARCHAR (3) NULL,
    [ManualType2]                     TINYINT     NULL,
    [SurgicalGroupId2]                INT         NULL,
    [UVRRangeId2]                     INT         NULL,
    CONSTRAINT [PK_BillingItemsRestrictionDetailCondition] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BillingItemsRestrictionDetailCondition_BillingItemsRestrictionDetail] FOREIGN KEY ([BillingItemsRestrictionDetailId]) REFERENCES [Contract].[BillingItemsRestrictionDetail] ([Id]),
    CONSTRAINT [FK_BillingItemsRestrictionDetailCondition_FunctionalUnit] FOREIGN KEY ([FunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [FK_BillingItemsRestrictionDetailCondition_FunctionalUnit2] FOREIGN KEY ([FunctionalUnitId2]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [FK_BillingItemsRestrictionDetailCondition_SurgicalGroup] FOREIGN KEY ([SurgicalGroupId]) REFERENCES [Contract].[SurgicalGroup] ([Id]),
    CONSTRAINT [FK_BillingItemsRestrictionDetailCondition_SurgicalGroup2] FOREIGN KEY ([SurgicalGroupId2]) REFERENCES [Contract].[SurgicalGroup] ([Id]),
    CONSTRAINT [FK_BillingItemsRestrictionDetailCondition_UVRRange] FOREIGN KEY ([UVRRangeId]) REFERENCES [Contract].[UVRRange] ([Id]),
    CONSTRAINT [FK_BillingItemsRestrictionDetailCondition_UVRRange2] FOREIGN KEY ([UVRRangeId2]) REFERENCES [Contract].[UVRRange] ([Id])
);


GO
ALTER TABLE [Contract].[BillingItemsRestrictionDetailCondition] NOCHECK CONSTRAINT [FK_BillingItemsRestrictionDetailCondition_BillingItemsRestrictionDetail];


GO
ALTER TABLE [Contract].[BillingItemsRestrictionDetailCondition] NOCHECK CONSTRAINT [FK_BillingItemsRestrictionDetailCondition_FunctionalUnit];


GO
ALTER TABLE [Contract].[BillingItemsRestrictionDetailCondition] NOCHECK CONSTRAINT [FK_BillingItemsRestrictionDetailCondition_FunctionalUnit2];


GO
ALTER TABLE [Contract].[BillingItemsRestrictionDetailCondition] NOCHECK CONSTRAINT [FK_BillingItemsRestrictionDetailCondition_SurgicalGroup];


GO
ALTER TABLE [Contract].[BillingItemsRestrictionDetailCondition] NOCHECK CONSTRAINT [FK_BillingItemsRestrictionDetailCondition_SurgicalGroup2];


GO
ALTER TABLE [Contract].[BillingItemsRestrictionDetailCondition] NOCHECK CONSTRAINT [FK_BillingItemsRestrictionDetailCondition_UVRRange];


GO
ALTER TABLE [Contract].[BillingItemsRestrictionDetailCondition] NOCHECK CONSTRAINT [FK_BillingItemsRestrictionDetailCondition_UVRRange2];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del rango de unidad de valor relativo adicional.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'UVRRangeId2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del grupo quirúrgico adicional.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'SurgicalGroupId2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo del manual tarifario  1 - ISS 2001  2 - ISS 2004  3 - SOAT', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'ManualType2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena un tipo adicional de estancia.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'StayType2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Unidad Funcional:  1: Urgencias  2: Hospitalizacion  3: Apoyo Dx  4: Apoyo Terapeutico  5: Unidades de Cuidado Intensivo Adulto  6: Unidades de Cuidado Intermedio Adulto  7: Unidades de Cuidado Intensivo Pediatrica  8: Unidades de Cuidado Intermedio Pediatrica  9: Unidades de Cuidado Intensivo Neonatal  10: Unidades de Cuidado Intermedio Neonatal  11: Unidades de Cuidado Basico Neonatal  12: Unidad Renal  13 Unidad Oncologica  14: Unidad Medicina Nuclear  15: Consulta Externa  16: Unidad Mental  17: Unidad de Quemados  18: Unidad de Cuidado Paliativo  19: Cirugia  20: Laboratorio  21: Cardiologia No Invasiva  22: Cardiologia Invasiva  23: Gineco-Obstetricia  24: Consulta Externa - Gineco-Obstetricia  25: Otras', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'UnitTypeId2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Operador de la segunda condicion  1 - "="  2 - "<>"    Este campo solo se llena si hay dos condiciones  ', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'Operator2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del rango de unidad de valor relativo.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'UVRRangeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del grupo quirúrgico.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'SurgicalGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo del manual tarifario  1 - ISS 2001  2 - ISS 2004  3 - SOAT', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'ManualType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena el tipo de estancia.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'StayType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Unidad Funcional:  1: Urgencias  2: Hospitalizacion  3: Apoyo Dx  4: Apoyo Terapeutico  5: Unidades de Cuidado Intensivo Adulto  6: Unidades de Cuidado Intermedio Adulto  7: Unidades de Cuidado Intensivo Pediatrica  8: Unidades de Cuidado Intermedio Pediatrica  9: Unidades de Cuidado Intensivo Neonatal  10: Unidades de Cuidado Intermedio Neonatal  11: Unidades de Cuidado Basico Neonatal  12: Unidad Renal  13 Unidad Oncologica  14: Unidad Medicina Nuclear  15: Consulta Externa  16: Unidad Mental  17: Unidad de Quemados  18: Unidad de Cuidado Paliativo  19: Cirugia  20: Laboratorio  21: Cardiologia No Invasiva  22: Cardiologia Invasiva  23: Gineco-Obstetricia  24: Consulta Externa - Gineco-Obstetricia  25: Otras', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'UnitTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Operador de la primera condicion  1 - "="  2 - "<>"  ', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'Operator';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del detalle de restricción de elementos de facturación.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'BillingItemsRestrictionDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la condición.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'Id';

