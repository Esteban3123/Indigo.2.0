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
ALTER TABLE [Cost].[StandarCostDetails] NOCHECK CONSTRAINT [FK_StandarCostDetails_StandarCost];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la actividad', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'CostActivityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del costo adicional', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'AdditionalCost';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de Inventarios', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'InventoryValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de nómina', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'PayrollValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del activo fijo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'FixedAssetValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de modificación del costo estandar', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de modificacón del costo estandar', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de creación del costo estandar', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación del costo estandar', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones del costo estandar', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'Observation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del costo estandar', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'StandarCostValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha final', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'FinalDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha inicial ', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'InitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del CUPS (Campo no utilizado actualmente)', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'CupsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la cabecera del costo estandar, relacion con la tabla - StandarCost', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'StandarCostId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de los detalles del costo estandar', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandarCostDetails', @level2type = N'COLUMN', @level2name = N'Id';

