CREATE TABLE [RegulatoryEngine].[DecisionTableColumn] (
    [Id]              BIGINT         IDENTITY (1, 1) NOT NULL,
    [DecisionTableId] BIGINT         NOT NULL,
    [ColumnName]      NVARCHAR (200) NOT NULL,
    [ColumnOrder]     INT            NOT NULL,
    [DataType]        NVARCHAR (50)  NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DecisionTableColumn_Table] FOREIGN KEY ([DecisionTableId]) REFERENCES [RegulatoryEngine].[DecisionTable] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define las columnas de una tabla de decisión. Cada columna representa un campo del contexto de validación que el motor debe leer del JSON de entrada (ej: UserType, EntityType). El conjunto de columnas determina cuántos campos se evalúan al buscar una fila coincidente en la tabla de decisión.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'DecisionTableColumn';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la columna.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'DecisionTableColumn', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK a la tabla de decisión a la que pertenece esta columna.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'DecisionTableColumn', @level2type = N'COLUMN', @level2name = N'DecisionTableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del campo en el contexto de validación. Debe coincidir exactamente con la clave del JSON de contexto enviado en la solicitud (ej: "UserType", "EntityType", "ObservationValue"). El motor usa este nombre para extraer el valor del contexto durante la evaluación.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'DecisionTableColumn', @level2type = N'COLUMN', @level2name = N'ColumnName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Orden de presentación de la columna dentro de la tabla de decisión. No afecta la lógica de evaluación; es de uso informativo para reportes y herramientas de gestión.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'DecisionTableColumn', @level2type = N'COLUMN', @level2name = N'ColumnOrder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de dato del campo del contexto. Valores posibles: STRING, INTEGER, DECIMAL, BOOLEAN. Es informativo para herramientas de gestión; la comparación en el motor siempre se realiza como texto insensible a mayúsculas.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'DecisionTableColumn', @level2type = N'COLUMN', @level2name = N'DataType';
