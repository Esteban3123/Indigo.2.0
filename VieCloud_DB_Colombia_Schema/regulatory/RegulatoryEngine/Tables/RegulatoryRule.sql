CREATE TABLE [RegulatoryEngine].[RegulatoryRule] (
    [Id]               BIGINT         IDENTITY (1, 1) NOT NULL,
    [RegulatoryPackId] BIGINT         NOT NULL,
    [RuleCode]         NVARCHAR (50)  NOT NULL,
    [RuleName]         NVARCHAR (300) NOT NULL,
    [EngineClass]      NVARCHAR (500) NOT NULL,
    [BlockingLevel]    NVARCHAR (20)  NOT NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    [CreatedAt]        DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedAt]        DATETIME2 (7)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RegulatoryRule_RegulatoryPack] FOREIGN KEY ([RegulatoryPackId]) REFERENCES [RegulatoryEngine].[RegulatoryPack] ([Id])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_RegulatoryRule_Code]
    ON [RegulatoryEngine].[RegulatoryRule]([RegulatoryPackId] ASC, [RuleCode] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Regla regulatoria individual dentro de un RegulatoryPack. RuleCode identifica la regla (ej: RVC005, RVC053). EngineClass es el nombre exacto de la clase C# que implementa la lógica (ej: CoverageClassificationCompatibilityRule). BlockingLevel determina el efecto cuando el resultado es negativo: BLOCK impide continuar, WARN muestra advertencia, AUDIT solo registra. No contiene lógica hardcodeada de país.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'RegulatoryRule';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la regla regulatoria.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'RegulatoryRule', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK al pack regulatorio al que pertenece esta regla. Determina la jurisdicción y vigencia heredada del pack.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'RegulatoryRule', @level2type = N'COLUMN', @level2name = N'RegulatoryPackId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único de la regla dentro del pack (ej: RVC005, RVC053, RVC084). Es la clave de negocio que los servicios usan para invocar la regla. Único por pack.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'RegulatoryRule', @level2type = N'COLUMN', @level2name = N'RuleCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo de la regla regulatoria (ej: Compatibilidad cobertura-clasificación, Ventana post-mortem).', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'RegulatoryRule', @level2type = N'COLUMN', @level2name = N'RuleName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre exacto de la clase C# que implementa la lógica de la regla (ej: CoverageClassificationCompatibilityRule, PostMortemServiceWindowRule, TerminologyMembershipRule). El motor resuelve la implementación por este nombre usando el patrón Strategy; debe coincidir exactamente con GetType().Name de la clase registrada en el contenedor de inyección de dependencias.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'RegulatoryRule', @level2type = N'COLUMN', @level2name = N'EngineClass';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Determina el efecto de la regla cuando el resultado es negativo. BLOCK: impide continuar el proceso; WARN: permite continuar pero muestra advertencia; AUDIT: solo registra en el log sin afectar el flujo.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'RegulatoryRule', @level2type = N'COLUMN', @level2name = N'BlockingLevel';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si la regla está activa. Las reglas inactivas son ignoradas por el motor de validación sin lanzar error.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'RegulatoryRule', @level2type = N'COLUMN', @level2name = N'IsActive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'RegulatoryRule', @level2type = N'COLUMN', @level2name = N'CreatedAt';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del registro. NULL si nunca ha sido modificado.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'RegulatoryRule', @level2type = N'COLUMN', @level2name = N'UpdatedAt';
