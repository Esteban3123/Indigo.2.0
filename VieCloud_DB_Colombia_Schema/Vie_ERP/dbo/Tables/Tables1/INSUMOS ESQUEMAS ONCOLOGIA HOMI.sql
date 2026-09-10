CREATE TABLE [dbo].[INSUMOS ESQUEMAS ONCOLOGIA HOMI] (
    [Codigo Esquema] VARCHAR (50) NULL,
    [Codigo Insumo]  VARCHAR (50) NULL,
    [Dia]            VARCHAR (50) NULL,
    [Cantidad]       VARCHAR (50) NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla que almacena los insumos asociados a esquemas de quimioterapia oncológica del hospital HOMI, relacionando cada esquema con sus insumos o medicamentos requeridos, el día de administración dentro del protocolo y la cantidad correspondiente a ese día.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'INSUMOS ESQUEMAS ONCOLOGIA HOMI';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'INSUMOS ESQUEMAS ONCOLOGIA HOMI';
GO
