CREATE TABLE [Report].[Tableordenciclo] (
    [fecharegistro] DATETIME NOT NULL,
    [idhcordquimio] INT      NOT NULL,
    [schemesid]     INT      NOT NULL,
    [ciclo]         INT      NOT NULL,
    [dia]           INT      NOT NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de reporte que almacena registros de órdenes de quimioterapia identificadas por un ID de orden (`idhcordquimio`) y un esquema de tratamiento (`schemesid`). Registra la progresión temporal del ciclo y día de cada orden, junto con la fecha de registro, permitiendo rastrear el avance del paciente dentro de un protocolo de quimioterapia por ciclo y día.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'TABLE', @level1name=N'Tableordenciclo';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'TABLE', @level1name=N'Tableordenciclo';
GO
