CREATE TABLE [Common].[Log] (
    [Id]          INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TableSchema] VARCHAR (250) NOT NULL,
    [TableName]   VARCHAR (250) NOT NULL,
    [Key]         INT           NOT NULL,
    [ColumnName]  VARCHAR (250) NOT NULL,
    [OldValue]    VARCHAR (MAX) NOT NULL,
    [NewValue]    VARCHAR (MAX) NOT NULL,
    [UpdatedBy]   VARCHAR (250) NULL,
    [UpdatedDate] DATETIME      NOT NULL,
    CONSTRAINT [PK_Common_Log] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) exacta del cambio; permite ordenar cronológicamente la evolución de datos y auditorías regulatorias RIPS/HCU.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Log', @level2type = N'COLUMN', @level2name = N'UpdatedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la Actualización ', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Log', @level2type = N'COLUMN', @level2name = N'UpdatedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Log', @level2type = N'COLUMN', @level2name = N'UpdatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario o proceso (VARCHAR 250) que ejecutó la modificación; identifica quién cambió datos de paciente, factura, procedimiento o diagnóstico.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Log', @level2type = N'COLUMN', @level2name = N'UpdatedBy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Actualizado por', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Log', @level2type = N'COLUMN', @level2name = N'UpdatedBy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Log', @level2type = N'COLUMN', @level2name = N'UpdatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor nuevo (VARCHAR MAX) después de la actualización; registra el cambio aplicado para validación de integridad y compliance.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Log', @level2type = N'COLUMN', @level2name = N'NewValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nuevo valor ', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Log', @level2type = N'COLUMN', @level2name = N'NewValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Log', @level2type = N'COLUMN', @level2name = N'NewValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor anterior (VARCHAR MAX) antes de la actualización; conserva dato histórico para comparación y trazabilidad de cambios en atributos críticos.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Log', @level2type = N'COLUMN', @level2name = N'OldValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Anterior ', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Log', @level2type = N'COLUMN', @level2name = N'OldValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Log', @level2type = N'COLUMN', @level2name = N'OldValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la columna (VARCHAR 250) donde ocurrió la modificación; permite auditoría granular de cambios en diagnóstico, estado, valores de atención.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Log', @level2type = N'COLUMN', @level2name = N'ColumnName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la columna ', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Log', @level2type = N'COLUMN', @level2name = N'ColumnName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Log', @level2type = N'COLUMN', @level2name = N'ColumnName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador entero (INT) de la fila modificada en la tabla auditada; vincula directamente al registro original afectado.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Log', @level2type = N'COLUMN', @level2name = N'Key';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'LLave ', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Log', @level2type = N'COLUMN', @level2name = N'Key';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Log', @level2type = N'COLUMN', @level2name = N'Key';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la tabla SQL Server (VARCHAR 250) en la cual se registró el cambio; facilita búsqueda de modificaciones por entidad (Paciente, Factura, RIPS, etc.).', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Log', @level2type = N'COLUMN', @level2name = N'TableName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la tabla ', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Log', @level2type = N'COLUMN', @level2name = N'TableName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Log', @level2type = N'COLUMN', @level2name = N'TableName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Esquema SQL Server (VARCHAR 250) que contiene la tabla auditada; permite trazabilidad en estructuras multitenancy.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Log', @level2type = N'COLUMN', @level2name = N'TableSchema';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Esquema de tabla', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Log', @level2type = N'COLUMN', @level2name = N'TableSchema';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Log', @level2type = N'COLUMN', @level2name = N'TableSchema';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental (INT IDENTITY) de cada registro de auditoría en la tabla de bitácora.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Log', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Log', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Log', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de auditoría de cambios en el sistema: guarda el historial de modificaciones realizadas sobre cualquier tabla, indicando qué campo fue cambiado, el valor anterior y el nuevo valor, quién hizo el cambio y cuándo.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Log';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Log';
