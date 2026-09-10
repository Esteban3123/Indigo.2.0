CREATE TABLE [Contract].[DefinitionRateDetail] (
    [Id]                      INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DefinitionRateId]        INT             NOT NULL,
    [RuleType]                TINYINT         NOT NULL,
    [IPSServiceId]            INT             NULL,
    [CUPSEntityId]            INT             NULL,
    [CUPSSubgroupId]          INT             NULL,
    [CUPSGroupId]             INT             NULL,
    [ConditionType]           TINYINT         NOT NULL,
    [LogicalOperator]         TINYINT         CONSTRAINT [DF_DefinitionRateDetail_LogicalOperator] DEFAULT ((1)) NOT NULL,
    [ConditionType2]          TINYINT         CONSTRAINT [DF_DefinitionRateDetail_ConditionType2] DEFAULT ((5)) NOT NULL,
    [Weight]                  TINYINT         NOT NULL,
    [AllowValueChange]        BIT             CONSTRAINT [DF_DefinitionRateDetail_AllowValueChange] DEFAULT ((0)) NOT NULL,
    [LiquidationType]         TINYINT         NULL,
    [ManualType]              TINYINT         NULL,
    [SalesValue]              NUMERIC (18, 2) NULL,
    [SalesValueWithSurcharge] NUMERIC (18, 2) CONSTRAINT [DF_DefinitionRateDetail_SalesValueWithSurcharge] DEFAULT ((0)) NULL,
    [RateManualId]            INT             NULL,
    [RateVariation]           NUMERIC (5, 2)  NULL,
    [RateManualValidityId]    INT             NULL,
    CONSTRAINT [PK_DefinitionRateDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DefinitionRateDetail_CUPSEntity] FOREIGN KEY ([CUPSEntityId]) REFERENCES [Contract].[CUPSEntity] ([Id]),
    CONSTRAINT [FK_DefinitionRateDetail_CupsGroup] FOREIGN KEY ([CUPSGroupId]) REFERENCES [Contract].[CupsGroup] ([Id]),
    CONSTRAINT [FK_DefinitionRateDetail_CupsSubgroup] FOREIGN KEY ([CUPSSubgroupId]) REFERENCES [Contract].[CupsSubgroup] ([Id]),
    CONSTRAINT [FK_DefinitionRateDetail_DefinitionRate] FOREIGN KEY ([DefinitionRateId]) REFERENCES [Contract].[DefinitionRate] ([Id]),
    CONSTRAINT [FK_DefinitionRateDetail_IPSService] FOREIGN KEY ([IPSServiceId]) REFERENCES [Contract].[IPSService] ([Id]),
    CONSTRAINT [FK_DefinitionRateDetail_RateManual] FOREIGN KEY ([RateManualId]) REFERENCES [Contract].[RateManual] ([Id]),
    CONSTRAINT [FK_DefinitionRateDetail_RateManualValidity] FOREIGN KEY ([RateManualValidityId]) REFERENCES [Contract].[RateManualValidity] ([Id])
);


GO
ALTER TABLE [Contract].[DefinitionRateDetail] NOCHECK CONSTRAINT [FK_DefinitionRateDetail_CUPSEntity];




GO
ALTER TABLE [Contract].[DefinitionRateDetail] NOCHECK CONSTRAINT [FK_DefinitionRateDetail_CUPSEntity];


GO



GO



GO



GO



GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_DefinitionRateDetail_DefinitionRateId_CUPSEntityId_IPSServiceId]
    ON [Contract].[DefinitionRateDetail]([DefinitionRateId] ASC, [CUPSEntityId] ASC, [IPSServiceId] ASC)
    INCLUDE([AllowValueChange], [ConditionType], [ConditionType2], [CUPSGroupId], [CUPSSubgroupId], [LiquidationType], [LogicalOperator], [ManualType], [RateManualId], [RateManualValidityId], [RateVariation], [RuleType], [SalesValue], [SalesValueWithSurcharge], [Weight]);


GO
CREATE NONCLUSTERED INDEX [IX_DefinitionRateDetail__DefinitionRateId]
    ON [Contract].[DefinitionRateDetail]([DefinitionRateId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_DefinitionRateDetail_DefinitionRateId_IPSServiceId_CUPSEntityId]
    ON [Contract].[DefinitionRateDetail]([DefinitionRateId] ASC, [IPSServiceId] ASC, [CUPSEntityId] ASC)
    INCLUDE([ConditionType], [CUPSGroupId], [CUPSSubgroupId], [LiquidationType], [RateManualId], [RateManualValidityId], [RuleType]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK, nullable) de la vigencia/período de validez del manual tarifario. Vincula a [Contract].[RateManualValidity]. Solo se completa si LiquidationType=Estándar.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'RateManualValidityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la vigencia del manual tarifario    Nota: Este campo solo se llena si el tipo de liquidacion es Estandar', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'RateManualValidityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'RateManualValidityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Variación/ajuste porcentual de la tarifa (NUMERIC 5,2, nullable). Porcentaje de incremento o decremento aplicado.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'RateVariation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje de la variacion de la tarifa', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'RateVariation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'RateVariation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK, nullable) del manual tarifario de referencia. Vincula a [Contract].[RateManual]. Solo se completa si LiquidationType=Estándar.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'RateManualId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del manual tarifario    Nota: Este campo solo se llena si el tipo de liquidacion es Estandar', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'RateManualId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'RateManualId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor con recargo/incremento a cobrar (NUMERIC 18,2, default=0, nullable). Solo se completa si LiquidationType=Fija. Incluye sobrecosto aplicable.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'SalesValueWithSurcharge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor con recargo que se va cobrar    Nota : este campo solo se llena si el tipo de liquidacion es Fija', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'SalesValueWithSurcharge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'SalesValueWithSurcharge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor/precio del servicio en moneda local (NUMERIC 18,2, nullable). Solo se completa si LiquidationType=Fija.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'SalesValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del servicio    Nota : este campo solo se llena si el tipo de liquidacion es Fija', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'SalesValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'SalesValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de manual tarifario (TINYINT, nullable): 1=ISS 2001, 2=ISS 2004, 3=SOAT. Solo aplica si LiquidationType=Fija.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'ManualType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo del manual tarifario  1 - ISS 2001  2 - ISS 2004  3 - SOAT    solo se solicita si el tipo de liquidacion es fija', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'ManualType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'ManualType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de liquidación (TINYINT, nullable): 1=Fija (valor manual), 2=Estándar (manual tarifario), 3=Vigencia. Determina qué campos se completan.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'LiquidationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de liquidacion  1 - Fija  2 - Estandar  3 - Vigencia', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'LiquidationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'LiquidationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT, default=0) que indica si permite modificar valores durante facturación. 0=No permite, 1=Permite cambio.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'AllowValueChange';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si permite cambiar los valores cuando se esta facturando', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'AllowValueChange';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'AllowValueChange';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peso/prioridad de evaluación de la condición (TINYINT 1-10, donde 10=máxima prioridad). Cuando ConditionType=Ninguna, peso=0.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'Weight';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Peso de la condicion el cual debe ser de 1 a 10 siendo el numero 10 el mas pesado es decir el primero que se va a evaluar    Nota: Cuando la Condicion de liquidacion es Ninguna el pero va ser siempre de 0', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'Weight';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'Weight';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segunda condición de liquidación (TINYINT, default=5): 1=Horario, 2=Especialidad, 3=Unidad Funcional, 4=Tipo de Unidad, 5=N/A (no aplica), 6=RIAS.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'ConditionType2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la segunda condicion de la liquidacion  1 - Horario  2 - Especialidad  3 - Unidad Funcional  4 - Tipo de Unidad   5 - N/A  6 - RIAS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'ConditionType2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'ConditionType2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Operador lógico entre condiciones (TINYINT, default=1): 1=Ninguna (una sola condición), 2=Y (AND), 3=O (OR).', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'LogicalOperator';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Operador Logico entre las Condiciones  1 - Ninguna  2 - "Y"  3 - "O"    Cuando el operador logico es 1 quiere decir que solo maneja un condicion de lo contrario son dos condiciones', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'LogicalOperator';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'LogicalOperator';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primera condición de liquidación (TINYINT): 1=Horario, 2=Especialidad, 3=Unidad Funcional, 4=Tipo de Unidad, 5=Ninguna, 6=RIAS.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'ConditionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la condicion de la liquidacion  1 - Horario  2 - Especialidad  3 - Unidad Funcional  4 - Tipo de Unidad   5 - Ninguna  6 - RIAS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'ConditionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'ConditionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK, nullable) del grupo CUPS (clasificación superior). Vincula a [Contract].[CupsGroup]. Usado cuando RuleType=4.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'CUPSGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del grupo del CUPS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'CUPSGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'CUPSGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK, nullable) del subgrupo CUPS (clasificación intermedia). Vincula a [Contract].[CupsSubgroup]. Usado cuando RuleType=3.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'CUPSSubgroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del subgrupo del cups', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'CUPSSubgroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'CUPSSubgroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK, nullable) de la entidad CUPS (procedimiento/servicio codificado). Vincula a [Contract].[CUPSEntity]. Usado cuando RuleType=2.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del CUPS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK, nullable) del servicio/prestación IPS asociado. Vincula a [Contract].[IPSService]. Usado cuando RuleType=1.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'IPSServiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id del servicio IPS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'IPSServiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'IPSServiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de regla de aplicación de tarifa (TINYINT): 1=Servicio IPS, 2=CUPS, 3=Subgrupos CUPS, 4=Grupo CUPS, 5=General. Define el nivel de granularidad.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'RuleType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de regla de la tarifa  1 - Servicio IPS  2 - CUPS  3 - SubGrupos CUPS  4 - Grupo CUPS  5 - General', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'RuleType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'RuleType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) de la cabecera/definición de tarifa padre. Vincula a [Contract].[DefinitionRate].', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'DefinitionRateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera de la definicion de la tarifa', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'DefinitionRateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'DefinitionRateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT) de la fila en tabla de detalle de definición de tarifa. Clave primaria con identity.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de las reglas y condiciones que componen una tarifa de contrato. Cada registro define cómo se aplica una tarifa específica a un servicio, grupo o subgrupo de CUPS, incluyendo el tipo de liquidación, el valor de venta, posibles recargos y si se permite modificar el valor manualmente.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail';
