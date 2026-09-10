CREATE TABLE [RegulatoryEngine].[DecisionTableRow] (
    [Id]              BIGINT IDENTITY (1, 1) NOT NULL,
    [DecisionTableId] BIGINT NOT NULL,
    [IsAllowed]       BIT    NOT NULL,
    [EffectiveFrom]   DATE   NOT NULL,
    [EffectiveTo]     DATE   NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DecisionTableRow_Table] FOREIGN KEY ([DecisionTableId]) REFERENCES [RegulatoryEngine].[DecisionTable] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Representa una fila de la tabla de decisión. Cada fila define una combinación específica de valores de contexto y el resultado que debe producir cuando esa combinación se detecta (permitida o denegada). Las filas tienen vigencia temporal: el motor solo evalúa filas cuya ventana EffectiveFrom-EffectiveTo incluye la fecha actual UTC.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'DecisionTableRow';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la fila.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'DecisionTableRow', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK a la tabla de decisión a la que pertenece esta fila.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'DecisionTableRow', @level2type = N'COLUMN', @level2name = N'DecisionTableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Determina el resultado cuando esta fila coincide con el contexto de validación. 1 (true) = el motor retorna PASS; 0 (false) = el motor retorna BLOCK.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'DecisionTableRow', @level2type = N'COLUMN', @level2name = N'IsAllowed';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha desde la cual esta fila es válida (inclusive). El motor excluye filas cuya fecha de inicio sea posterior a la fecha actual UTC. Permite programar cambios de reglas con anticipación.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'DecisionTableRow', @level2type = N'COLUMN', @level2name = N'EffectiveFrom';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha hasta la cual esta fila es válida (inclusive). NULL indica que la fila no tiene fecha de vencimiento. El motor excluye filas cuya fecha de fin sea anterior a la fecha actual UTC.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'DecisionTableRow', @level2type = N'COLUMN', @level2name = N'EffectiveTo';
