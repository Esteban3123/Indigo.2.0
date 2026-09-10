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



GO



GO



GO



GO



GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_DefinitionRateDetailCondition__DefinitionRateDetailId]
    ON [Contract].[DefinitionRateDetailCondition]([DefinitionRateDetailId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de vigencia del manual tarifario (INT, FK a Contract.RateManualValidity); solo si liquidación es Estándar', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'RateManualValidityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la vigencia del manual tarifario    Nota: Este campo solo se llena si el tipo de liquidacion es Estandar', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'RateManualValidityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'RateManualValidityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de descripción de contrato (INT, FK) para la segunda condición; se completa solo si la segunda condición es por descripción contractual', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'ContractDescriptionId2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la descripción, este campo solo se llena si la segunda condicion es por Descripción', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'ContractDescriptionId2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'ContractDescriptionId2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de descripción de contrato (INT, FK a Contract.ContractDescriptions); referencia a condiciones contractuales específicas', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'ContractDescriptionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la descripción de contratos', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'ContractDescriptionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'ContractDescriptionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del RIAS (INT) para la segunda condición; se completa solo si la segunda condición es por RIAS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'RIASId2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del RIAS que viene desde Crystal, este campo solo se llena si la segunda condicion es por RIAS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'RIASId2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'RIASId2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del Registro de Información de Atenciones y Servicios (INT) proveniente de Crystal; para primera condición por RIAS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'RIASId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del RIAS que viene desde Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'RIASId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'RIASId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de variación/ajuste de tarifa (NUMERIC 5,2); factor multiplicador o descuento en la tarifa base', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'RateVariation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje de la variacion de la tarifa', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'RateVariation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'RateVariation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del manual tarifario (INT, FK a Contract.RateManual); solo si liquidación es Estándar', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'RateManualId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del manual tarifario    Nota: Este campo solo se llena si el tipo de liquidacion es Estandar', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'RateManualId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'RateManualId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor con recargo/incremento (NUMERIC 18,2); importe final a facturar con surcharge aplicado, solo si liquidación es Fija', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'SalesValueWithSurcharge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor con recargo que se va cobrar    Nota : este campo solo se llena si el tipo de liquidacion es Fija', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'SalesValueWithSurcharge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'SalesValueWithSurcharge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario del servicio/procedimiento (NUMERIC 18,2); importe fijo de cobro, solo si liquidación es Fija', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'SalesValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del servicio    Nota : este campo solo se llena si el tipo de liquidacion es Fija', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'SalesValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'SalesValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación de manual tarifario (TINYINT): 1=ISS 2001, 2=ISS 2004, 3=SOAT; solo si liquidación es Fija', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'ManualType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo del manual tarifario  1 - ISS 2001  2 - ISS 2004  3 - SOAT    solo se solicita si el tipo de liquidacion es fija', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'ManualType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'ManualType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de liquidación (TINYINT): 1=Fija, 2=Estándar (manual tarifario), 3=Vigencia; determina campos obligatorios del registro', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'LiquidationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de liquidacion  1 - Fija  2 - Estandar  3 - Vigencia', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'LiquidationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'LiquidationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación de tipo de unidad funcional (TINYINT 1-25) para la segunda condición; se completa solo si la segunda condición es por tipo de unidad', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'UnitTypeId2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Unidad Funcional:  1: Urgencias  2: Hospitalizacion  3: Apoyo Dx  4: Apoyo Terapeutico  5: Unidades de Cuidado Intensivo Adulto  6: Unidades de Cuidado Intermedio Adulto  7: Unidades de Cuidado Intensivo Pediatrica  8: Unidades de Cuidado Intermedio Pediatrica  9: Unidades de Cuidado Intensivo Neonatal  10: Unidades de Cuidado Intermedio Neonatal  11: Unidades de Cuidado Basico Neonatal  12: Unidad Renal  13 Unidad Oncologica  14: Unidad Medicina Nuclear  15: Consulta Externa  16: Unidad Mental  17: Unidad de Quemados  18: Unidad de Cuidado Paliativo  19: Cirugia  20: Laboratorio  21: Cardiologia No Invasiva  22: Cardiologia Invasiva  23: Gineco-Obstetricia  24: Consulta Externa - Gineco-Obstetricia  25: Otras    Este campo solo se llena si la segunda condicion es por Tipo de Unidad', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'UnitTypeId2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'UnitTypeId2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de unidad funcional (INT, FK) para la segunda condición; se completa solo si la segunda condición es por unidad funcional', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad funcional    Este campo solo se llena si la segunda condicion es por Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de especialidad (CHAR 3) para la segunda condición; se completa solo si la segunda condición es por especialidad', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'SpecialtyId2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la especialidad    Este campo solo se llena si la segunda condicion es por Especialidad', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'SpecialtyId2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'SpecialtyId2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora final de la segunda condición temporal (TIME); se completa solo si la segunda condición es por horario', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'EndTime2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la Hora Final  Este campo solo se llena si la segunda condicion es por Horario', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'EndTime2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'EndTime2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora inicial de la segunda condición temporal (TIME); se completa solo si la segunda condición es por horario', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'StartTime2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la Hora inicial  Este campo solo se llena si la segunda condicion es por Horario', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'StartTime2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'StartTime2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Operador lógico de la segunda condición (TINYINT): 1=Igual, 2=Distinto; solo se completa si hay dos condiciones combinadas', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'Operator2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Operador de la segunda condicion  1 - "="  2 - "<>"    Este campo solo se llena si hay dos condiciones  ', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'Operator2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'Operator2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación de tipo de unidad funcional (TINYINT 1-25): urgencias, hospitalización, apoyo diagnóstico, UCI, pediatría, quemados, medicina nuclear, cirugía, laboratorio, consulta externa, etc.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'UnitTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Unidad Funcional:  1: Urgencias  2: Hospitalizacion  3: Apoyo Dx  4: Apoyo Terapeutico  5: Unidades de Cuidado Intensivo Adulto  6: Unidades de Cuidado Intermedio Adulto  7: Unidades de Cuidado Intensivo Pediatrica  8: Unidades de Cuidado Intermedio Pediatrica  9: Unidades de Cuidado Intensivo Neonatal  10: Unidades de Cuidado Intermedio Neonatal  11: Unidades de Cuidado Basico Neonatal  12: Unidad Renal  13 Unidad Oncologica  14: Unidad Medicina Nuclear  15: Consulta Externa  16: Unidad Mental  17: Unidad de Quemados  18: Unidad de Cuidado Paliativo  19: Cirugia  20: Laboratorio  21: Cardiologia No Invasiva  22: Cardiologia Invasiva  23: Gineco-Obstetricia  24: Consulta Externa - Gineco-Obstetricia  25: Otras', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'UnitTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'UnitTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de unidad funcional (INT, FK a Payroll.FunctionalUnit) para la primera condición; área/centro de atención como urgencia, hospitalización, laboratorio', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de especialidad (CHAR 3) vinculado a la primera condición; referencia a especialidad médica del profesional de salud', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'SpecialtyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la especialidad', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'SpecialtyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'SpecialtyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de finalización de vigencia (TIME) para condiciones basadas en horario; cierre del rango horario de aplicabilidad', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'EndTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora de finalización', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'EndTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'EndTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de inicio de vigencia (TIME) para condiciones basadas en horario; aplica en primeras condiciones temporales', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'StartTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora de inicio', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'StartTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'StartTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Operador lógico de la primera condición (TINYINT): 1=Igual, 2=Distinto; usado en evaluación de reglas de liquidación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'Operator';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Operador de la primera condicion  1 - "="  2 - "<>"  ', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'Operator';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'Operator';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle de tarifa relacionado (FK a DefinitionRateDetail); aplica para servicios, procedimientos y atenciones', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'DefinitionRateDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id del detalle de la tarifa Solo aplica para Servicios', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'DefinitionRateDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'DefinitionRateDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) de la condición de tarifa en el detalle de definición de tarifas del contrato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Condiciones específicas que aplican a cada detalle de tarifa de un contrato, definiendo franjas horarias, especialidades, unidades funcionales y tipos de liquidación que determinan cómo se calcula el valor a cobrar o pagar por un servicio contratado.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetailCondition';
