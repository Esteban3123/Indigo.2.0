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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la Actualización ', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Log', @level2type = N'COLUMN', @level2name = N'UpdatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Actualizado por', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Log', @level2type = N'COLUMN', @level2name = N'UpdatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nuevo valor ', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Log', @level2type = N'COLUMN', @level2name = N'NewValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Anterior ', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Log', @level2type = N'COLUMN', @level2name = N'OldValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la columna ', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Log', @level2type = N'COLUMN', @level2name = N'ColumnName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'LLave ', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Log', @level2type = N'COLUMN', @level2name = N'Key';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la tabla ', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Log', @level2type = N'COLUMN', @level2name = N'TableName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esquema de tabla', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Log', @level2type = N'COLUMN', @level2name = N'TableSchema';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Log', @level2type = N'COLUMN', @level2name = N'Id';

