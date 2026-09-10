CREATE TABLE [RegulatoryEngine].[RuleParameter] (
    [Id]               BIGINT         IDENTITY (1, 1) NOT NULL,
    [RegulatoryRuleId] BIGINT         NOT NULL,
    [ParameterName]    NVARCHAR (200) NOT NULL,
    [ParameterValue]   NVARCHAR (MAX) NULL,
    [DataType]         NVARCHAR (50)  NOT NULL,
    [CreatedAt]        DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RuleParameter_RegulatoryRule] FOREIGN KEY ([RegulatoryRuleId]) REFERENCES [RegulatoryEngine].[RegulatoryRule] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_RuleParameter_Rule]
    ON [RegulatoryEngine].[RuleParameter]([RegulatoryRuleId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parámetros configurables por regla regulatoria. Permite ajustar el comportamiento de una regla sin modificar código (ej: MinimumValue=24, AllowedPostDeathServiceWindowHours=72, ValueSet=ValidProcedureCodes). DataType=VALUESET indica que ParameterValue es el nombre de un TerminologyValueSet al que la regla debe consultar.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'RuleParameter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del parámetro.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'RuleParameter', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK a la regla regulatoria a la que pertenece este parámetro.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'RuleParameter', @level2type = N'COLUMN', @level2name = N'RegulatoryRuleId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del parámetro que la clase C# lee por coincidencia exacta de texto (ej: MinimumValue, MaximumValue, AllowedPostDeathServiceWindowHours, ValueSet). Debe coincidir con la constante definida en la implementación de la regla.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'RuleParameter', @level2type = N'COLUMN', @level2name = N'ParameterName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del parámetro almacenado como texto. La clase C# lo convierte al tipo requerido según DataType (ej: "72" → int para horas, "24.5" → decimal para rangos, "ValidProcedureCodes" → nombre de ValueSet). NULL indica que el parámetro existe pero sin valor configurado; la regla debe aplicar su valor por defecto.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'RuleParameter', @level2type = N'COLUMN', @level2name = N'ParameterValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de dato del parámetro. Valores posibles: STRING, INTEGER, DECIMAL, BOOLEAN, VALUESET. VALUESET indica que ParameterValue contiene el nombre de un TerminologyValueSet. Usado por herramientas de gestión para validar el formato del valor antes de guardarlo.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'RuleParameter', @level2type = N'COLUMN', @level2name = N'DataType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'RuleParameter', @level2type = N'COLUMN', @level2name = N'CreatedAt';
