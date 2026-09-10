CREATE TABLE [RegulatoryEngine].[DecisionTableCell] (
    [Id]                    BIGINT         IDENTITY (1, 1) NOT NULL,
    [DecisionTableRowId]    BIGINT         NOT NULL,
    [DecisionTableColumnId] BIGINT         NOT NULL,
    [Value]                 NVARCHAR (500) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DecisionTableCell_Column] FOREIGN KEY ([DecisionTableColumnId]) REFERENCES [RegulatoryEngine].[DecisionTableColumn] ([Id]),
    CONSTRAINT [FK_DecisionTableCell_Row] FOREIGN KEY ([DecisionTableRowId]) REFERENCES [RegulatoryEngine].[DecisionTableRow] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena el valor esperado en la intersección de una fila y una columna de la tabla de decisión. Cada celda representa la condición que debe cumplir un campo específico del contexto de validación para que la fila sea considerada coincidente. El motor compara el valor de la celda con el valor real del contexto usando igualdad insensible a mayúsculas.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'DecisionTableCell';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la celda.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'DecisionTableCell', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK a la fila de la tabla de decisión a la que pertenece esta celda.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'DecisionTableCell', @level2type = N'COLUMN', @level2name = N'DecisionTableRowId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK a la columna que define qué campo del contexto de validación evalúa esta celda.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'DecisionTableCell', @level2type = N'COLUMN', @level2name = N'DecisionTableColumnId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor esperado del campo de contexto. Se almacena siempre como texto y se compara con el valor real del contexto de validación usando igualdad insensible a mayúsculas (ej: "3", "ADMIN", "true").', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'DecisionTableCell', @level2type = N'COLUMN', @level2name = N'Value';
