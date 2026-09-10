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



GO



GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del rango UVR secundario (FK a Contract.UVRRange). Banda UVR adicional para límites de cobertura escalonada o diferenciada por monto.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'UVRRangeId2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del rango de unidad de valor relativo adicional.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'UVRRangeId2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'UVRRangeId2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del grupo quirúrgico secundario (FK a Contract.SurgicalGroup). Segunda especialidad quirúrgica para restricciones complejas de procedimientos.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'SurgicalGroupId2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del grupo quirúrgico adicional.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'SurgicalGroupId2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'SurgicalGroupId2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Manual tarifario secundario: 1=ISS 2001, 2=ISS 2004, 3=SOAT. Tarifa alternativa en condición compuesta.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'ManualType2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo del manual tarifario  1 - ISS 2001  2 - ISS 2004  3 - SOAT', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'ManualType2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'ManualType2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de estancia secundario (VARCHAR 3): internación, ambulatoria, urgencia. Segunda modalidad de atención para restricciones múltiples.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'StayType2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena un tipo adicional de estancia.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'StayType2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'StayType2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de unidad funcional secundario (TINYINT): urgencias, hospitalización, apoyo diagnóstico, cuidado intensivo/intermedio, laboratorio, cardiología, gineco-obstetricia, psiquiatría, quemados, cuidado paliativo, etc.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'UnitTypeId2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Unidad Funcional:  1: Urgencias  2: Hospitalizacion  3: Apoyo Dx  4: Apoyo Terapeutico  5: Unidades de Cuidado Intensivo Adulto  6: Unidades de Cuidado Intermedio Adulto  7: Unidades de Cuidado Intensivo Pediatrica  8: Unidades de Cuidado Intermedio Pediatrica  9: Unidades de Cuidado Intensivo Neonatal  10: Unidades de Cuidado Intermedio Neonatal  11: Unidades de Cuidado Basico Neonatal  12: Unidad Renal  13 Unidad Oncologica  14: Unidad Medicina Nuclear  15: Consulta Externa  16: Unidad Mental  17: Unidad de Quemados  18: Unidad de Cuidado Paliativo  19: Cirugia  20: Laboratorio  21: Cardiologia No Invasiva  22: Cardiologia Invasiva  23: Gineco-Obstetricia  24: Consulta Externa - Gineco-Obstetricia  25: Otras', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'UnitTypeId2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'UnitTypeId2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la unidad funcional secundaria (FK a Payroll.FunctionalUnit). Segunda unidad para restricciones complejas que incluyen múltiples centros.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Operador lógico de la segunda condición: 1=Igualdad (=), 2=Desigualdad (<>). Solo se completa si hay condición adicional (AND/OR lógico).', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'Operator2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Operador de la segunda condicion  1 - "="  2 - "<>"    Este campo solo se llena si hay dos condiciones  ', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'Operator2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'Operator2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del rango de Unidad de Valor Relativo primario (FK a Contract.UVRRange). Banda de UVR que limita cobertura, autorización o glosa de servicios según valor económico.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'UVRRangeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del rango de unidad de valor relativo.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'UVRRangeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'UVRRangeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del grupo quirúrgico primario (FK a Contract.SurgicalGroup). Agrupa procedimientos quirúrgicos por especialidad (general, cardiovascular, neurología, ortopedia, etc.).', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'SurgicalGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del grupo quirúrgico.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'SurgicalGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'SurgicalGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Manual tarifario aplicable: 1=ISS 2001, 2=ISS 2004, 3=SOAT. Define tarifa de servicios, procedimientos y medicamentos según normativa vigente.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'ManualType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo del manual tarifario  1 - ISS 2001  2 - ISS 2004  3 - SOAT', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'ManualType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'ManualType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de estancia (VARCHAR 3): internación, ambulatoria, urgencia. Clasifica modalidad de atención y permanencia del paciente en la unidad.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'StayType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el tipo de estancia.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'StayType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'StayType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de unidad funcional (TINYINT): urgencias, hospitalización, apoyo diagnóstico, cuidado intensivo/intermedio adulto/pediátrico, renal, oncología, medicina nuclear, cirugía, cardiología, gineco-obstetricia, psiquiatría, quemados, cuidado paliativo.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'UnitTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Unidad Funcional:  1: Urgencias  2: Hospitalizacion  3: Apoyo Dx  4: Apoyo Terapeutico  5: Unidades de Cuidado Intensivo Adulto  6: Unidades de Cuidado Intermedio Adulto  7: Unidades de Cuidado Intensivo Pediatrica  8: Unidades de Cuidado Intermedio Pediatrica  9: Unidades de Cuidado Intensivo Neonatal  10: Unidades de Cuidado Intermedio Neonatal  11: Unidades de Cuidado Basico Neonatal  12: Unidad Renal  13 Unidad Oncologica  14: Unidad Medicina Nuclear  15: Consulta Externa  16: Unidad Mental  17: Unidad de Quemados  18: Unidad de Cuidado Paliativo  19: Cirugia  20: Laboratorio  21: Cardiologia No Invasiva  22: Cardiologia Invasiva  23: Gineco-Obstetricia  24: Consulta Externa - Gineco-Obstetricia  25: Otras', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'UnitTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'UnitTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la unidad funcional primaria (FK a Payroll.FunctionalUnit). Centro de atención, área clínica: urgencias, hospitalización, cuidado intensivo, consulta externa, quirófano, laboratorio, etc.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Operador lógico de la primera condición: 1=Igualdad (=), 2=Desigualdad (<>). Define comparación para unidad funcional, tipo de estancia, grupo quirúrgico o rango UVR.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'Operator';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Operador de la primera condicion  1 - "="  2 - "<>"  ', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'Operator';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'Operator';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del detalle de restricción de elementos de facturación (FK). Vincula a la restricción padre que define límites de cobertura, glosas o autorización de procedimientos, exámenes o servicios.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'BillingItemsRestrictionDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de restricción de elementos de facturación.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'BillingItemsRestrictionDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'BillingItemsRestrictionDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK) de la condición de restricción de facturación. INT IDENTITY.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador único de la condición.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Condiciones detalladas de las restricciones de ítems de facturación en contratos. Cada registro define los criterios (unidad funcional, tipo de unidad, tipo de estancia, tipo manual, grupo quirúrgico, rango UVR) y operadores lógicos que determinan cuándo aplica una restricción de cobro o facturación, permitiendo combinar hasta dos conjuntos de condiciones.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BillingItemsRestrictionDetailCondition';
