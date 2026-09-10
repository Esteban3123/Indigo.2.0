CREATE TABLE [dbo].[ActualizarConcentracion] (
    [Codigo_Medicamento]    VARCHAR (20)  NULL,
    [Nombre_Medicamento]    VARCHAR (200) NULL,
    [CONCENTRACION]         VARCHAR (50)  NULL,
    [UNIDADES]              VARCHAR (50)  NULL,
    [PreviousConcentration] VARCHAR (50)  NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de trabajo utilizada para actualizar la concentración de medicamentos. Almacena el código y nombre del medicamento junto con la nueva concentración, sus unidades y el valor de concentración previo, lo que sugiere un proceso de migración o corrección masiva de datos de concentración en el catálogo de medicamentos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ActualizarConcentracion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ActualizarConcentracion';
GO
