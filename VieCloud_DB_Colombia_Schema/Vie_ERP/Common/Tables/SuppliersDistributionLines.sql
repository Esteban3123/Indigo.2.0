CREATE TABLE [Common].[SuppliersDistributionLines] (
    [Id]                 INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdSupplier]         INT NOT NULL,
    [IdDistributionLine] INT NOT NULL,
    [Status]             BIT CONSTRAINT [DF_SuppliersDistributionLines_Status] DEFAULT ((1)) NOT NULL,
    [PositionId]         INT NULL,
    [Factoring]          BIT CONSTRAINT [DF__Suppliers__Facto__1788EA65] DEFAULT ((0)) NOT NULL,
    [NotFactoring]       BIT CONSTRAINT [DF__Suppliers__NotFa__5921A398] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_SuppliersDistributionLines] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SuppliersDistributionLines_DistributionLines] FOREIGN KEY ([IdDistributionLine]) REFERENCES [Common].[DistributionLines] ([Id]),
    CONSTRAINT [FK_SuppliersDistributionLines_Position] FOREIGN KEY ([PositionId]) REFERENCES [Payroll].[Position] ([Id]),
    CONSTRAINT [FK_SuppliersDistributionLines_Supplier] FOREIGN KEY ([IdSupplier]) REFERENCES [Common].[Supplier] ([Id])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera booleana (BIT, default=0) que marca la línea de distribución para ser expuesta o reportada como NO facturable (exluida de operaciones de factoring o financiamiento).', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'SuppliersDistributionLines', @level2type = N'COLUMN', @level2name = N'NotFactoring';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marcar linea de distribucion a exponer', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'SuppliersDistributionLines', @level2type = N'COLUMN', @level2name = N'NotFactoring';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'SuppliersDistributionLines', @level2type = N'COLUMN', @level2name = N'NotFactoring';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera booleana (BIT, default=0) que indica si el registro está asignado para factoring (gestión de cuentas por cobrar, descuento de cartera o cesión de derechos del proveedor).', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'SuppliersDistributionLines', @level2type = N'COLUMN', @level2name = N'Factoring';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Asignar registro de factoring', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'SuppliersDistributionLines', @level2type = N'COLUMN', @level2name = N'Factoring';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'SuppliersDistributionLines', @level2type = N'COLUMN', @level2name = N'Factoring';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del cargo/puesto (INT, FK → Payroll.Position, nullable). Si el proveedor es independiente, permite asociar el cargo que ocupa para cálculos de costeo por actividades y distribución de costos laborales.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'SuppliersDistributionLines', @level2type = N'COLUMN', @level2name = N'PositionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Si el proveedor es un independiente se permite agregar a la linea de distribución el cargo que ocupa el proveedor.   De esta manera poder realizar los calculos para el costeo por actividades', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'SuppliersDistributionLines', @level2type = N'COLUMN', @level2name = N'PositionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'SuppliersDistributionLines', @level2type = N'COLUMN', @level2name = N'PositionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera booleana (BIT, default=1) que indica si la línea de distribución está habilitada o activa para el proveedor. Controla la vigencia operativa del vínculo.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'SuppliersDistributionLines', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite saber si está habilitada la linea de distribución al proveedor', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'SuppliersDistributionLines', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'SuppliersDistributionLines', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la línea de distribución (INT, FK → Common.DistributionLines). Referencia el catálogo o estructura de distribución de costos/gastos.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'SuppliersDistributionLines', @level2type = N'COLUMN', @level2name = N'IdDistributionLine';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del la linea de distribución', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'SuppliersDistributionLines', @level2type = N'COLUMN', @level2name = N'IdDistributionLine';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'SuppliersDistributionLines', @level2type = N'COLUMN', @level2name = N'IdDistributionLine';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del proveedor (INT, FK → Common.Supplier). Referencia la entidad proveedora que se vincula a la línea de distribución.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'SuppliersDistributionLines', @level2type = N'COLUMN', @level2name = N'IdSupplier';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del proveedor', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'SuppliersDistributionLines', @level2type = N'COLUMN', @level2name = N'IdSupplier';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'SuppliersDistributionLines', @level2type = N'COLUMN', @level2name = N'IdSupplier';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, PK) del registro de línea de distribución de proveedor. Generado automáticamente por IDENTITY.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'SuppliersDistributionLines', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'SuppliersDistributionLines', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'SuppliersDistributionLines', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona proveedores con las líneas de distribución que tienen asignadas, indicando si la línea aplica con factoring, sin factoring o ambas modalidades de pago. Permite controlar qué líneas de distribución están activas para cada proveedor y la posición que ocupan.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'SuppliersDistributionLines';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'SuppliersDistributionLines';
