CREATE TABLE [dbo].[FormRelationship] (
    [Id]        INT         NOT NULL,
    [IdErpForm] VARCHAR (5) NOT NULL,
    [IdHisForm] CHAR (3)    NOT NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de correspondencia entre formularios del sistema ERP y formularios del sistema HIS, vinculando sus identificadores respectivos. Permite mapear un formulario ERP (código hasta 5 caracteres) con su equivalente en HIS (código de 3 caracteres), estableciendo una relación de equivalencia entre ambos sistemas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'FormRelationship';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'FormRelationship';
GO
