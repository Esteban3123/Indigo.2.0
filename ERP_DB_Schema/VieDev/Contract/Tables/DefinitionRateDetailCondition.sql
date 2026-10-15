CREATE TABLE [Contract].[DefinitionRateDetailCondition] (
    [Id]                      INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DefinitionRateDetailId]  INT             NOT NULL,
    [Operator]                TINYINT         NOT NULL,
    [StartTime]               TIME (7)        NULL,
    [EndTime]                 TIME (7)        NULL,
    [SpecialtyId]             CHAR (3)        NULL,
    [FunctionalUnitId]        INT             NULL,
    [UnitTypeId]              TINYINT         NULL,
    [Operator2]               TINYINT         NULL,
    [StartTime2]              TIME (7)        NULL,
    [EndTime2]                TIME (7)        NULL,
    [SpecialtyId2]            CHAR (3)        NULL,
    [FunctionalUnitId2]       INT             NULL,
    [UnitTypeId2]             TINYINT         NULL,
    [LiquidationType]         TINYINT         NOT NULL,
    [ManualType]              TINYINT         NULL,
    [SalesValue]              NUMERIC (18, 2) NULL,
    [SalesValueWithSurcharge] NUMERIC (18, 2) CONSTRAINT [DF_DefinitionRateDetailSchedule_SalesValueWithSurcharge] DEFAULT ((0)) NULL,
    [RateManualId]            INT             NULL,
    [RateVariation]           NUMERIC (5, 2)  NULL,
    [RIASId]                  INT             NULL,
    [RIASId2]                 INT             NULL,
    [ContractDescriptionId]   INT             NULL,
    [ContractDescriptionId2]  INT             NULL,
    [RateManualValidityId]    INT             NULL,
    CONSTRAINT [PK_DefinitionRateDetailCondition__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DefinitionRateDetailCondition_ContractDescriptions] FOREIGN KEY ([ContractDescriptionId]) REFERENCES [Contract].[ContractDescriptions] ([Id]),
    CONSTRAINT [FK_DefinitionRateDetailCondition_ContractDescriptions1] FOREIGN KEY ([ContractDescriptionId2]) REFERENCES [Contract].[ContractDescriptions] ([Id]),
    CONSTRAINT [FK_DefinitionRateDetailCondition_RateManualValidity] FOREIGN KEY ([RateManualValidityId]) REFERENCES [Contract].[RateManualValidity] ([Id]),
    CONSTRAINT [FK_DefinitionRateDetailSchedule_DefinitionRateDetail] FOREIGN KEY ([DefinitionRateDetailId]) REFERENCES [Contract].[DefinitionRateDetail] ([Id]),
    CONSTRAINT [FK_DefinitionRateDetailSchedule_FunctionalUnit] FOREIGN KEY ([FunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [FK_DefinitionRateDetailSchedule_FunctionalUnit1] FOREIGN KEY ([FunctionalUnitId2]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [FK_DefinitionRateDetailSchedule_RateManual] FOREIGN KEY ([RateManualId]) REFERENCES [Contract].[RateManual] ([Id])
);


GO
ALTER TABLE [Contract].[DefinitionRateDetailCondition] NOCHECK CONSTRAINT [FK_DefinitionRateDetailCondition_ContractDescriptions];


GO
ALTER TABLE [Contract].[DefinitionRateDetailCondition] NOCHECK CONSTRAINT [FK_DefinitionRateDetailCondition_ContractDescriptions1];


GO
ALTER TABLE [Contract].[DefinitionRateDetailCondition] NOCHECK CONSTRAINT [FK_DefinitionRateDetailCondition_RateManualValidity];


GO
ALTER TABLE [Contract].[DefinitionRateDetailCondition] NOCHECK CONSTRAINT [FK_DefinitionRateDetailSchedule_DefinitionRateDetail];


GO
ALTER TABLE [Contract].[DefinitionRateDetailCondition] NOCHECK CONSTRAINT [FK_DefinitionRateDetailSchedule_FunctionalUnit];


GO
ALTER TABLE [Contract].[DefinitionRateDetailCondition] NOCHECK CONSTRAINT [FK_DefinitionRateDetailSchedule_FunctionalUnit1];


GO
ALTER TABLE [Contract].[DefinitionRateDetailCondition] NOCHECK CONSTRAINT [FK_DefinitionRateDetailSchedule_RateManual];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la vigencia del manual tarifario    Nota: Este campo solo se llena si el tipo de liquidacion es Estandar', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'RateManualValidityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del manual tarifario    Nota: Este campo solo se llena si el tipo de liquidacion es Estandar', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'RateManualId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor con recargo que se va cobrar    Nota : este campo solo se llena si el tipo de liquidacion es Fija', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'SalesValueWithSurcharge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del servicio    Nota : este campo solo se llena si el tipo de liquidacion es Fija', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'SalesValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo del manual tarifario  1 - ISS 2001  2 - ISS 2004  3 - SOAT    solo se solicita si el tipo de liquidacion es fija', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'ManualType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de liquidacion  1 - Fija  2 - Estandar  3 - Vigencia', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'LiquidationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Unidad Funcional:  1: Urgencias  2: Hospitalizacion  3: Apoyo Dx  4: Apoyo Terapeutico  5: Unidades de Cuidado Intensivo Adulto  6: Unidades de Cuidado Intermedio Adulto  7: Unidades de Cuidado Intensivo Pediatrica  8: Unidades de Cuidado Intermedio Pediatrica  9: Unidades de Cuidado Intensivo Neonatal  10: Unidades de Cuidado Intermedio Neonatal  11: Unidades de Cuidado Basico Neonatal  12: Unidad Renal  13 Unidad Oncologica  14: Unidad Medicina Nuclear  15: Consulta Externa  16: Unidad Mental  17: Unidad de Quemados  18: Unidad de Cuidado Paliativo  19: Cirugia  20: Laboratorio  21: Cardiologia No Invasiva  22: Cardiologia Invasiva  23: Gineco-Obstetricia  24: Consulta Externa - Gineco-Obstetricia  25: Otras    Este campo solo se llena si la segunda condicion es por Tipo de Unidad', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'UnitTypeId2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad funcional    Este campo solo se llena si la segunda condicion es por Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la especialidad    Este campo solo se llena si la segunda condicion es por Especialidad', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'SpecialtyId2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la Hora Final  Este campo solo se llena si la segunda condicion es por Horario', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'EndTime2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la Hora inicial  Este campo solo se llena si la segunda condicion es por Horario', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'StartTime2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Operador de la segunda condicion  1 - "="  2 - "<>"    Este campo solo se llena si hay dos condiciones  ', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'Operator2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Unidad Funcional:  1: Urgencias  2: Hospitalizacion  3: Apoyo Dx  4: Apoyo Terapeutico  5: Unidades de Cuidado Intensivo Adulto  6: Unidades de Cuidado Intermedio Adulto  7: Unidades de Cuidado Intensivo Pediatrica  8: Unidades de Cuidado Intermedio Pediatrica  9: Unidades de Cuidado Intensivo Neonatal  10: Unidades de Cuidado Intermedio Neonatal  11: Unidades de Cuidado Basico Neonatal  12: Unidad Renal  13 Unidad Oncologica  14: Unidad Medicina Nuclear  15: Consulta Externa  16: Unidad Mental  17: Unidad de Quemados  18: Unidad de Cuidado Paliativo  19: Cirugia  20: Laboratorio  21: Cardiologia No Invasiva  22: Cardiologia Invasiva  23: Gineco-Obstetricia  24: Consulta Externa - Gineco-Obstetricia  25: Otras', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'UnitTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Operador de la primera condicion  1 - "="  2 - "<>"  ', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'Operator';

