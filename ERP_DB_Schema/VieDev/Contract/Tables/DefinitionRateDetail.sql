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
ALTER TABLE [Contract].[DefinitionRateDetail] NOCHECK CONSTRAINT [FK_DefinitionRateDetail_CupsGroup];


GO
ALTER TABLE [Contract].[DefinitionRateDetail] NOCHECK CONSTRAINT [FK_DefinitionRateDetail_CupsSubgroup];


GO
ALTER TABLE [Contract].[DefinitionRateDetail] NOCHECK CONSTRAINT [FK_DefinitionRateDetail_DefinitionRate];


GO
ALTER TABLE [Contract].[DefinitionRateDetail] NOCHECK CONSTRAINT [FK_DefinitionRateDetail_IPSService];


GO
ALTER TABLE [Contract].[DefinitionRateDetail] NOCHECK CONSTRAINT [FK_DefinitionRateDetail_RateManual];


GO
ALTER TABLE [Contract].[DefinitionRateDetail] NOCHECK CONSTRAINT [FK_DefinitionRateDetail_RateManualValidity];


GO
CREATE NONCLUSTERED INDEX [IX_DefinitionRateDetail_DefinitionRateId_CUPSEntityId_IPSServiceId]
    ON [Contract].[DefinitionRateDetail]([DefinitionRateId] ASC, [CUPSEntityId] ASC, [IPSServiceId] ASC)
    INCLUDE([AllowValueChange], [ConditionType], [ConditionType2], [CUPSGroupId], [CUPSSubgroupId], [LiquidationType], [LogicalOperator], [ManualType], [RateManualId], [RateManualValidityId], [RateVariation], [RuleType], [SalesValue], [SalesValueWithSurcharge], [Weight]);


GO
CREATE NONCLUSTERED INDEX [IX_DefinitionRateDetail_DefinitionRateId_IPSServiceId_CUPSEntityId]
    ON [Contract].[DefinitionRateDetail]([DefinitionRateId] ASC, [IPSServiceId] ASC, [CUPSEntityId] ASC)
    INCLUDE([ConditionType], [CUPSGroupId], [CUPSSubgroupId], [LiquidationType], [RateManualId], [RateManualValidityId], [RuleType]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la vigencia del manual tarifario    Nota: Este campo solo se llena si el tipo de liquidacion es Estandar', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'RateManualValidityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del manual tarifario    Nota: Este campo solo se llena si el tipo de liquidacion es Estandar', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'RateManualId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor con recargo que se va cobrar    Nota : este campo solo se llena si el tipo de liquidacion es Fija', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'SalesValueWithSurcharge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del servicio    Nota : este campo solo se llena si el tipo de liquidacion es Fija', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'SalesValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo del manual tarifario  1 - ISS 2001  2 - ISS 2004  3 - SOAT    solo se solicita si el tipo de liquidacion es fija', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'ManualType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de liquidacion  1 - Fija  2 - Estandar  3 - Vigencia', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'LiquidationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Peso de la condicion el cual debe ser de 1 a 10 siendo el numero 10 el mas pesado es decir el primero que se va a evaluar    Nota: Cuando la Condicion de liquidacion es Ninguna el pero va ser siempre de 0', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'Weight';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la segunda condicion de la liquidacion  1 - Horario  2 - Especialidad  3 - Unidad Funcional  4 - Tipo de Unidad   5 - N/A  6 - RIAS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'ConditionType2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Operador Logico entre las Condiciones  1 - Ninguna  2 - "Y"  3 - "O"    Cuando el operador logico es 1 quiere decir que solo maneja un condicion de lo contrario son dos condiciones', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'LogicalOperator';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la condicion de la liquidacion  1 - Horario  2 - Especialidad  3 - Unidad Funcional  4 - Tipo de Unidad   5 - Ninguna  6 - RIAS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'ConditionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de regla de la tarifa  1 - Servicio IPS  2 - CUPS  3 - SubGrupos CUPS  4 - Grupo CUPS  5 - General', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DefinitionRateDetail', @level2type = N'COLUMN', @level2name = N'RuleType';

