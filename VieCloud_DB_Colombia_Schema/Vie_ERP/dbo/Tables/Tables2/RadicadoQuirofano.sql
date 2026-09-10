CREATE TABLE [dbo].[RadicadoQuirofano] (
    [Cedula_Usuario] NVARCHAR (255) NULL,
    [Fecha_Radicado] NVARCHAR (255) NULL,
    [Hora_Radicado]  NVARCHAR (255) NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla que registra el radicado de usuarios en quirófano, almacenando la cédula del usuario junto con la fecha y hora en que fue radicado. Su estructura mínima sugiere que actúa como log o tabla auxiliar de control de ingreso/registro al área quirúrgica, sin claves foráneas ni restricciones definidas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'RadicadoQuirofano';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'RadicadoQuirofano';
GO
