CREATE TABLE [Report].[Table_HOMO_GRUPO_ETARIO] (
    [ID_GRUPO_ETAREO]   INT        NULL,
    [GRUPO_ETAREO_EDAD] FLOAT (53) NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla auxiliar del esquema de reportería que almacena grupos etarios identificados por un código numérico y asociados a un valor de edad en formato decimal. Probablemente sirve como tabla de referencia o lookup para clasificar pacientes por rango de edad en informes homologados.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'TABLE', @level1name=N'Table_HOMO_GRUPO_ETARIO';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'TABLE', @level1name=N'Table_HOMO_GRUPO_ETARIO';
GO
