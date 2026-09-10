CREATE TABLE [dbo].[MedicamentoFinal] (
    [DCIId]                  NVARCHAR (255) NULL,
    [Code]                   NVARCHAR (255) NULL,
    [Name]                   NVARCHAR (255) NULL,
    [AbbreviationName]       NVARCHAR (255) NULL,
    [AdministrationRouteId]  NVARCHAR (255) NULL,
    [PharmacologicalGroupId] NVARCHAR (255) NULL,
    [Concentration]          NVARCHAR (255) NULL,
    [InventoryRiskLevelId]   NVARCHAR (255) NULL,
    [StabilityMinimumHours]  FLOAT (53)     NULL,
    [StabilityMaximumHours]  FLOAT (53)     NULL,
    [FormulationType]        NVARCHAR (255) NULL,
    [Weight]                 NVARCHAR (255) NULL,
    [UnitMedida]             NVARCHAR (255) NULL,
    [Volume]                 NVARCHAR (255) NULL,
    [VolumeMeasureUnit]      NVARCHAR (255) NULL,
    [AdministrationUnitId]   NVARCHAR (255) NULL,
    [ATCEntityId]            NVARCHAR (255) NULL,
    [Presentacion]           NVARCHAR (255) NULL,
    [Pos]                    FLOAT (53)     NULL,
    [AllPOSPathologies]      NVARCHAR (255) NULL,
    [BillingGroupNoPosId]    NVARCHAR (255) NULL,
    [ProductNPT]             NVARCHAR (255) NULL,
    [Osmolarity]             NVARCHAR (255) NULL,
    [Density]                NVARCHAR (255) NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de catálogo consolidado de medicamentos que almacena atributos farmacológicos y logísticos: identificador DCI, código, nombre, vía de administración, grupo farmacológico, concentración, nivel de riesgo en inventario, rangos de estabilidad en horas, tipo de formulación, peso, volumen con unidades, clasificación ATC, presentación comercial, indicador POS/No-POS con grupo de facturación, y parámetros de nutrición parenteral (osmolaridad, densidad).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MedicamentoFinal';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MedicamentoFinal';
GO
