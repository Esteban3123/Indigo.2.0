CREATE TABLE [Billing].[LiquidationDataDetail] (
    [Id]                   INT             IDENTITY (1, 1) NOT NULL,
    [LiquidationDataId]    INT             NOT NULL,
    [ConditionType]        TINYINT         NOT NULL,
    [ConditionsItemsIds]   VARCHAR (1000)  NOT NULL,
    [LimitType]            TINYINT         NOT NULL,
    [Quantity]             INT             NOT NULL,
    [TaxInclude]           BIT             NOT NULL,
    [LimitValue]           NUMERIC (20, 2) NOT NULL,
    [InsurerCoveredValue]  NUMERIC (20, 2) CONSTRAINT [DF__Liquidati__Insur__6E2878CD] DEFAULT ((0)) NOT NULL,
    [InsuranceCoinsurance] NUMERIC (5, 2)  CONSTRAINT [DF_LiquidationDataDetail_InsuranceCoinsurance] DEFAULT ((0)) NOT NULL,
    [PatientCoinsurance]   NUMERIC (5, 2)  CONSTRAINT [DF_LiquidationDataDetail_PatientCoinsurance] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_LiquidationDataDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_LiquidationDataDetail_LiquidationData] FOREIGN KEY ([LiquidationDataId]) REFERENCES [Billing].[LiquidationData] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje (%) de coaseguro a cargo del paciente, responsabilidad compartida. Solo se completa cuando LimitType es 3 (Coaseguro). Tipo: NUMERIC(5,2), rango 0-100%.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataDetail', @level2type = N'COLUMN', @level2name = N'PatientCoinsurance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'% Coaseguro paciente,  Este campo solo se llena si el tipo de limite es Coaseguro (LimitType 3)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataDetail', @level2type = N'COLUMN', @level2name = N'PatientCoinsurance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataDetail', @level2type = N'COLUMN', @level2name = N'PatientCoinsurance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje (%) de coaseguro a cargo de la aseguradora, responsabilidad compartida. Solo se completa cuando LimitType es 3 (Coaseguro). Tipo: NUMERIC(5,2), rango 0-100%.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataDetail', @level2type = N'COLUMN', @level2name = N'InsuranceCoinsurance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'% Coaseguro aseguradora,  Este campo solo se llena si el tipo de limite es Coaseguro (LimitType 3)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataDetail', @level2type = N'COLUMN', @level2name = N'InsuranceCoinsurance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataDetail', @level2type = N'COLUMN', @level2name = N'InsuranceCoinsurance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor máximo en pesos que cubre la aseguradora antes de aplicar coaseguro. Tope de cobertura previo a responsabilidad compartida paciente-aseguradora. Tipo: NUMERIC(20,2).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataDetail', @level2type = N'COLUMN', @level2name = N'InsurerCoveredValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Limite max a cubrir por la aseguradora antes de coaseguro', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataDetail', @level2type = N'COLUMN', @level2name = N'InsurerCoveredValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataDetail', @level2type = N'COLUMN', @level2name = N'InsurerCoveredValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor máximo o tope de cobertura que la aseguradora reconoce según el tipo de límite (unitario, total o coaseguro). Base para cálculo de glosa y facturación. Tipo: NUMERIC(20,2).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataDetail', @level2type = N'COLUMN', @level2name = N'LimitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Limite max a cubrir por la aseguradora', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataDetail', @level2type = N'COLUMN', @level2name = N'LimitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataDetail', @level2type = N'COLUMN', @level2name = N'LimitValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano si el valor límite incluye IVA. 1=Sí (incluye impuesto), 0=No (valor sin IVA). Tipo: BIT. Afecta cálculo de liquidación y factura.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataDetail', @level2type = N'COLUMN', @level2name = N'TaxInclude';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Incluye IVA : 1 (TRUE -SI); 0 (False-No)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataDetail', @level2type = N'COLUMN', @level2name = N'TaxInclude';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataDetail', @level2type = N'COLUMN', @level2name = N'TaxInclude';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad máxima de servicios/productos a cubrir por la aseguradora bajo esta condición. Valor 0 indica sin límite de cantidad (no aplica restricción). Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad max de items a cubir por la aseguradora, 0 es No aplica.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación del tipo de límite de cobertura: 1=Valor unitario (por servicio), 2=Valor total (acumulado), 3=Coaseguro (compartido paciente-aseguradora). Tipo: TINYINT.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataDetail', @level2type = N'COLUMN', @level2name = N'LimitType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de limite:   1- Valor Unitario,  2- Valor Total,  3 - Coaseguro', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataDetail', @level2type = N'COLUMN', @level2name = N'LimitType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataDetail', @level2type = N'COLUMN', @level2name = N'LimitType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista de identificadores numéricos (separados por coma) de elementos del catálogo que cumplen la condición especificada (grupo, subgrupo, servicio o producto). Ej: ''''5,12,45''''. Tipo: VARCHAR(1000).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataDetail', @level2type = N'COLUMN', @level2name = N'ConditionsItemsIds';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda los Ids, separados por coma, en base a la condicion anterior', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataDetail', @level2type = N'COLUMN', @level2name = N'ConditionsItemsIds';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataDetail', @level2type = N'COLUMN', @level2name = N'ConditionsItemsIds';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de elemento catalogado al cual aplica el límite: 1=Grupo de servicios, 2=Subgrupo de servicios, 3=Servicio individual, 4=Productos farmacéuticos/insumos. Tipo: TINYINT.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataDetail', @level2type = N'COLUMN', @level2name = N'ConditionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de condicion :                   1. Grupo Cátalogo de Servicios                   2. SubGrupo Cátalogo de servicio                   3. Cátalogo de Servicios    4. Productos', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataDetail', @level2type = N'COLUMN', @level2name = N'ConditionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataDetail', @level2type = N'COLUMN', @level2name = N'ConditionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera/maestro de liquidación (Billing.LiquidationData) al cual pertenece este detalle. FK a tabla LiquidationData. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataDetail', @level2type = N'COLUMN', @level2name = N'LiquidationDataId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera datos de liquidacion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataDetail', @level2type = N'COLUMN', @level2name = N'LiquidationDataId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataDetail', @level2type = N'COLUMN', @level2name = N'LiquidationDataId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único secuencial de cada fila de detalle de liquidación. Clave primaria. Tipo: INT IDENTITY(1,1).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de las condiciones y límites de liquidación definidos en una regla de facturación. Registra, para cada configuración de liquidación, los tipos de condición aplicables, los valores límite, la inclusión de impuestos, y los porcentajes o montos que cubre el asegurador y el paciente (coaseguro), usada en el cálculo de glosas, copagos y cobertura de contratos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'LiquidationDataDetail';
