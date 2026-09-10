CREATE TABLE [Report].[Table_DIM_GRUPO_ETAREO] (
    [ID_GRUPO_ETAREO]          INT            IDENTITY (0, 1) NOT NULL,
    [GRUPO_ETAREO_EDAD]        FLOAT (53)     NOT NULL,
    [GRUPO_ETAREO_RES_5268]    NVARCHAR (100) NOT NULL,
    [GRUPO_ETAREO_UPC]         NVARCHAR (100) NULL,
    [GRUPO_ETAREO_CICLO_VITAL] NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK_GRUPO_ETAREO] PRIMARY KEY CLUSTERED ([ID_GRUPO_ETAREO] ASC)
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de dimensión para reporting que clasifica grupos etarios según distintas categorizaciones: una basada en la Resolución 5268 (normativa colombiana de salud), otra bajo el esquema UPC (Unidad de Pago por Capitación) y una tercera por ciclo vital. Cada registro asocia un valor de edad (en formato decimal) con sus respectivas clasificaciones, sirviendo como tabla de referencia para segmentar indicadores de salud por rangos de edad.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'TABLE', @level1name=N'Table_DIM_GRUPO_ETAREO';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'TABLE', @level1name=N'Table_DIM_GRUPO_ETAREO';
GO
