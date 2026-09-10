CREATE TABLE [dbo].[insumosVSesquemas] (
    [esquema]  VARCHAR (10) NULL,
    [insumo]   VARCHAR (20) NULL,
    [dia]      INT          NULL,
    [cantidad] INT          NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de relación que asocia insumos con esquemas de tratamiento, registrando la cantidad de cada insumo requerida por día dentro de un esquema. Permite definir la dotación diaria de insumos según el esquema clínico o farmacológico al que pertenecen.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'insumosVSesquemas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'insumosVSesquemas';
GO
