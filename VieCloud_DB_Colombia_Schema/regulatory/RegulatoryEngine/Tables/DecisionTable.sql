CREATE TABLE [RegulatoryEngine].[DecisionTable] (
    [Id]               BIGINT          IDENTITY (1, 1) NOT NULL,
    [RegulatoryRuleId] BIGINT          NOT NULL,
    [Name]             NVARCHAR (200)  NOT NULL,
    [Description]      NVARCHAR (1000) NULL,
    [IsActive]         BIT             DEFAULT ((1)) NOT NULL,
    [CreatedAt]        DATETIME2 (7)   DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DecisionTable_RegulatoryRule] FOREIGN KEY ([RegulatoryRuleId]) REFERENCES [RegulatoryEngine].[RegulatoryRule] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define una tabla de decisión asociada a una regla regulatoria. Es una estructura matricial que determina qué combinaciones de valores del contexto de validación están permitidas o denegadas. Cada regla puede tener máximo una tabla de decisión activa. Las filas representan combinaciones de valores y las columnas representan los campos del contexto JSON.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'DecisionTable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la tabla de decisión.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'DecisionTable', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK a la regla regulatoria que esta tabla de decisión evalúa. Una regla solo puede tener una tabla de decisión activa en un momento dado.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'DecisionTable', @level2type = N'COLUMN', @level2name = N'RegulatoryRuleId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo de la tabla de decisión.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'DecisionTable', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción opcional del propósito y criterios de la tabla de decisión.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'DecisionTable', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si la tabla de decisión está activa. Solo la tabla activa se usa durante la validación. Permite versionar tablas de decisión sin eliminar las anteriores.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'DecisionTable', @level2type = N'COLUMN', @level2name = N'IsActive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'DecisionTable', @level2type = N'COLUMN', @level2name = N'CreatedAt';
