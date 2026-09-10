CREATE TABLE [EHR].[SchemesPathologies2] (
    [SchemesId]     CHAR (20) NOT NULL,
    [PathologyCode] CHAR (4)  NOT NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de relación muchos-a-muchos entre esquemas de tratamiento o protocolos clínicos y patologías. Asocia un identificador de esquema con un código de patología de 4 caracteres, permitiendo que un mismo esquema cubra múltiples patologías y viceversa. No posee restricciones de clave primaria ni foráneas declaradas en el DDL provisto.', @level0type=N'SCHEMA', @level0name=N'EHR', @level1type=N'TABLE', @level1name=N'SchemesPathologies2';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'EHR', @level1type=N'TABLE', @level1name=N'SchemesPathologies2';
GO
