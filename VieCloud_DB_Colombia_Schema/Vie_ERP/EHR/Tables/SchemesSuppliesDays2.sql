CREATE TABLE [EHR].[SchemesSuppliesDays2] (
    [SchemesId]        CHAR (20) NOT NULL,
    [SuppliesDrugCode] CHAR (20) NOT NULL,
    [Day]              INT       NOT NULL,
    [Quantity]         INT       NOT NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de detalle que registra la distribución diaria de insumos o medicamentos asociados a esquemas de tratamiento. Para cada combinación de esquema, código de insumo/medicamento y día, almacena la cantidad a suministrar, permitiendo modelar cronogramas de administración por día dentro de un esquema clínico.', @level0type=N'SCHEMA', @level0name=N'EHR', @level1type=N'TABLE', @level1name=N'SchemesSuppliesDays2';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'EHR', @level1type=N'TABLE', @level1name=N'SchemesSuppliesDays2';
GO
