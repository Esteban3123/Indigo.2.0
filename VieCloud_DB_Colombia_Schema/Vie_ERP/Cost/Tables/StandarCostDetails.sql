CREATE TABLE [Cost].[StandarCostDetails] (
    [Id]               INT             IDENTITY (1, 1) NOT NULL,
    [StandarCostId]    INT             NOT NULL,
    [CupsId]           INT             NULL,
    [InitialDate]      DATETIME        NULL,
    [FinalDate]        DATETIME        NULL,
    [StandarCostValue] DECIMAL (20, 2) NOT NULL,
    [Observation]      VARCHAR (200)   NOT NULL,
    [CreationDate]     DATETIME        NOT NULL,
    [CreationUser]     VARCHAR (50)    NOT NULL,
    [ModificationDate] DATETIME        NULL,
    [ModificationUser] VARCHAR (50)    NULL,
    [FixedAssetValue]  DECIMAL (20, 2) NULL,
    [PayrollValue]     DECIMAL (20, 2) NULL,
    [InventoryValue]   DECIMAL (20, 2) NULL,
    [AdditionalCost]   DECIMAL (20, 2) NULL,
    [CostActivityId]   INT             NULL,
    CONSTRAINT [PK_StandarCostDetails] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_StandarCostDetails_CostActivity] FOREIGN KEY ([CostActivityId]) REFERENCES [Cost].[CostActivity] ([Id]),
    CONSTRAINT [FK_StandarCostDetails_CUPSEntity] FOREIGN KEY ([CupsId]) REFERENCES [Contract].[CUPSEntity] ([Id]),
    CONSTRAINT [FK_StandarCostDetails_StandarCost] FOREIGN KEY ([StandarCostId]) REFERENCES [Cost].[StandarCost] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT NULL) de la actividad de costo; FK a Cost.CostActivity para vincular a centro de costo, unidad funcional o línea de negocio', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'CostActivityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la actividad', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'CostActivityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'CostActivityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del costo adicional o indirecto (DECIMAL 20,2 NULL); gastos de operación, servicios generales, utilidades no clasificadas', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'AdditionalCost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del costo adicional', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'AdditionalCost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'AdditionalCost';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de inventarios o consumibles (DECIMAL 20,2 NULL); materiales, insumos, medicamentos utilizados en el servicio', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'InventoryValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de Inventarios', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'InventoryValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'InventoryValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de nómina o costo laboral (DECIMAL 20,2 NULL); salarios, honorarios y beneficios del personal dedicado', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'PayrollValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de nómina', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'PayrollValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'PayrollValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de activos fijos (DECIMAL 20,2 NULL); depreciación de equipos, infraestructura e inmuebles asignados al costo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'FixedAssetValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del activo fijo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'FixedAssetValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'FixedAssetValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 50 NULL) que realizó la última modificación del costo estándar; trazabilidad de ajustes', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de modificación del costo estandar', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de última modificación (DATETIME NULL) del costo estándar; auditoria de cambios posteriores a la creación', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificacón del costo estandar', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 50) que creó el registro de costo estándar; identificación del analista o administrador responsable', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de creación del costo estandar', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación (DATETIME) del registro de detalle de costo estándar; auditoria de origen del dato', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación del costo estandar', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones o notas (VARCHAR 200) del costo estándar; comentarios adicionales, justificación, ajustes o cambios realizados', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'Observation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones del costo estandar', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'Observation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'Observation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total del costo estándar (DECIMAL 20,2); suma de componentes (nómina, activo fijo, inventario, costo adicional)', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'StandarCostValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del costo estandar', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'StandarCostValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'StandarCostValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha final (DATETIME) de vigencia o aplicación del costo estándar; cierre del rango de validez', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'FinalDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha final', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'FinalDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'FinalDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha inicial (DATETIME) de vigencia o aplicación del costo estándar; inicio del rango de validez', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'InitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha inicial ', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'InitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'InitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) del código CUPS (procedimiento, servicio o insumo); FK a Contract.CUPSEntity; campo actualmente no utilizado en operaciones', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'CupsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del CUPS (Campo no utilizado actualmente)', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'CupsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'CupsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de la cabecera/maestro de costo estándar; FK a tabla StandarCost para agrupar detalles por contrato, servicio o procedimiento', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'StandarCostId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera del costo estandar, relacion con la tabla - StandarCost', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'StandarCostId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'StandarCostId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT) del detalle de costo estándar; clave primaria de la tabla StandarCostDetails', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de los detalles del costo estandar', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle del costo estándar por servicio o procedimiento (CUPS): registra los valores de referencia de costo para un período vigente, desglosados en activos fijos, nómina/personal, inventario y costos adicionales. Se usa para comparar el costo teórico esperado contra el costo real de prestación de servicios.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails';
