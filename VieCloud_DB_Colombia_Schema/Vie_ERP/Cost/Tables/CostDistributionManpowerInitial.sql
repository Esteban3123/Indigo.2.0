CREATE TABLE [Cost].[CostDistributionManpowerInitial] (
    [Id]                        INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Year]                      INT             NOT NULL,
    [Month]                     INT             NOT NULL,
    [ManpowerType]              TINYINT         NOT NULL,
    [EntityId]                  INT             NOT NULL,
    [GroupId]                   INT             NULL,
    [EmployeeId]                INT             NULL,
    [ThirdPartyId]              INT             NOT NULL,
    [PositionId]                INT             NULL,
    [ProductionCenterId]        INT             NOT NULL,
    [HoursQuantity]             INT             CONSTRAINT [DF__CostDistr__Hours__5F6A1C34] DEFAULT ((0)) NOT NULL,
    [TotalAccrued]              DECIMAL (18, 2) CONSTRAINT [DF__CostDistr__Total__605E406D] DEFAULT ((0)) NOT NULL,
    [TotalProvision]            DECIMAL (18, 2) CONSTRAINT [DF__CostDistr__Total__615264A6] DEFAULT ((0)) NOT NULL,
    [TotalEmployerContribution] DECIMAL (18, 2) CONSTRAINT [DF__CostDistr__Total__624688DF] DEFAULT ((0)) NOT NULL,
    [TotalParafiscal]           DECIMAL (18, 2) CONSTRAINT [DF__CostDistr__Total__633AAD18] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_CostDistributionManpowerInitial] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total parafiscal (SENA, ICBF, cajas de compensación). Decimal(18,2), default 0. Distribución inicial de costos de mano de obra.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerInitial', @level2type = N'COLUMN', @level2name = N'TotalParafiscal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Total Parafiscal', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerInitial', @level2type = N'COLUMN', @level2name = N'TotalParafiscal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerInitial', @level2type = N'COLUMN', @level2name = N'TotalParafiscal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contribución total del empleador (aporte patronal, seguridad social, pensión). Decimal(18,2), default 0. Costo inicial de nómina.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerInitial', @level2type = N'COLUMN', @level2name = N'TotalEmployerContribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contribución total del empleador', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerInitial', @level2type = N'COLUMN', @level2name = N'TotalEmployerContribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerInitial', @level2type = N'COLUMN', @level2name = N'TotalEmployerContribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total provisión (reserva para prestaciones, indemnizaciones, cesantías). Decimal(18,2), default 0. Distribución de costos de mano de obra.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerInitial', @level2type = N'COLUMN', @level2name = N'TotalProvision';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Total Provisión', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerInitial', @level2type = N'COLUMN', @level2name = N'TotalProvision';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerInitial', @level2type = N'COLUMN', @level2name = N'TotalProvision';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total acumulado (devengado, causado de mano de obra). Decimal(18,2), default 0. Suma inicial de horas y costos por período.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerInitial', @level2type = N'COLUMN', @level2name = N'TotalAccrued';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Total acumulado', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerInitial', @level2type = N'COLUMN', @level2name = N'TotalAccrued';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerInitial', @level2type = N'COLUMN', @level2name = N'TotalAccrued';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de horas laboradas o asignadas. INT, default 0. Horas de mano de obra por tipo, centro de producción.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerInitial', @level2type = N'COLUMN', @level2name = N'HoursQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Quantity Hours', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerInitial', @level2type = N'COLUMN', @level2name = N'HoursQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerInitial', @level2type = N'COLUMN', @level2name = N'HoursQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del centro de producción (unidad funcional, área, departamento). INT, FK. Donde se distribuye el costo.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerInitial', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del centro de producción', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerInitial', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerInitial', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la posición, cargo o rol. INT nullable, FK. Profesional de salud, administrativo, servicios generales.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerInitial', @level2type = N'COLUMN', @level2name = N'PositionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de posición', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerInitial', @level2type = N'COLUMN', @level2name = N'PositionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerInitial', @level2type = N'COLUMN', @level2name = N'PositionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tercero, prestador, contratista. INT, FK. Entidad que provee o recibe el servicio de mano de obra.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerInitial', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID ThirdPartyId', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerInitial', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerInitial', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del empleado, profesional de salud, trabajador. INT nullable, FK. Recurso humano asignado a la distribución.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerInitial', @level2type = N'COLUMN', @level2name = N'EmployeeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de empleado', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerInitial', @level2type = N'COLUMN', @level2name = N'EmployeeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerInitial', @level2type = N'COLUMN', @level2name = N'EmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del grupo, equipo, categoría. INT nullable, FK. Agrupación de empleados o centros de costo.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerInitial', @level2type = N'COLUMN', @level2name = N'GroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del grupo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerInitial', @level2type = N'COLUMN', @level2name = N'GroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerInitial', @level2type = N'COLUMN', @level2name = N'GroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la entidad, institución, centro de atención. INT, FK. Organización propietaria del registro.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerInitial', @level2type = N'COLUMN', @level2name = N'EntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de entidad', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerInitial', @level2type = N'COLUMN', @level2name = N'EntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerInitial', @level2type = N'COLUMN', @level2name = N'EntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de mano de obra (empleado directo, temporal, contratista, profesional independiente). TINYINT. Clasificación inicial para distribución.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerInitial', @level2type = N'COLUMN', @level2name = N'ManpowerType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'tipo de mano de obra', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerInitial', @level2type = N'COLUMN', @level2name = N'ManpowerType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerInitial', @level2type = N'COLUMN', @level2name = N'ManpowerType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mes del período de distribución de costos (1-12). INT. Período mensual de acumulación de horas y costos.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerInitial', @level2type = N'COLUMN', @level2name = N'Month';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mes', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerInitial', @level2type = N'COLUMN', @level2name = N'Month';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerInitial', @level2type = N'COLUMN', @level2name = N'Month';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Año del período de distribución de costos. INT. Período anual para análisis de costos de nómina.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerInitial', @level2type = N'COLUMN', @level2name = N'Year';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Año', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerInitial', @level2type = N'COLUMN', @level2name = N'Year';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerInitial', @level2type = N'COLUMN', @level2name = N'Year';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (identity). INT. Clave primaria de la tabla de distribución inicial de mano de obra.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerInitial', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerInitial', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerInitial', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro inicial de distribución de costos de mano de obra por período (año y mes), empleado o tercero, centro de producción y tipo de personal. Consolida horas trabajadas, devengos, provisiones, aportes patronales y parafiscales para el cálculo y distribución inicial de costos de nómina.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerInitial';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerInitial';
