CREATE TABLE [Inventory].[ConsignmentCostListDetailRecord] (
    [Id]                          INT             IDENTITY (1, 1) NOT NULL,
    [ConsignmentCostListDetailId] INT             NOT NULL,
    [Cost]                        NUMERIC (12, 2) NULL,
    [CreationUser]                VARCHAR (20)    NOT NULL,
    [CreationDate]                DATETIME        NOT NULL,
    CONSTRAINT [PK_ConsignmentCostListDetailRecord] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ConsignmentCostListDetailRecord_ConsignmentCostListDetail] FOREIGN KEY ([ConsignmentCostListDetailId]) REFERENCES [Inventory].[ConsignmentCostListDetail] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de costo de consignación; timestamp de auditoría que rastrea cuándo se registró el valor de costo en el sistema', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentCostListDetailRecord', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creacion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentCostListDetailRecord', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentCostListDetailRecord', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro; identificación del operador o sistema que ingresó el costo de consignación (VARCHAR 20, auditoría)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentCostListDetailRecord', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de creacion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentCostListDetailRecord', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentCostListDetailRecord', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico del costo de consignación; monto económico asociado al detalle (NUMERIC 12,2 - hasta 10 dígitos enteros y 2 decimales); puede ser nulo si aún no se ha determinado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentCostListDetailRecord', @level2type = N'COLUMN', @level2name = N'Cost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del costo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentCostListDetailRecord', @level2type = N'COLUMN', @level2name = N'Cost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentCostListDetailRecord', @level2type = N'COLUMN', @level2name = N'Cost';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador foráneo de la tabla detalle (FK → Inventory.ConsignmentCostListDetail); referencia al detalle de la lista de costos de consignación relacionada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentCostListDetailRecord', @level2type = N'COLUMN', @level2name = N'ConsignmentCostListDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del la tabla detalle', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentCostListDetailRecord', @level2type = N'COLUMN', @level2name = N'ConsignmentCostListDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentCostListDetailRecord', @level2type = N'COLUMN', @level2name = N'ConsignmentCostListDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único primario del registro de costo de consignación; clave surrogate auto-incremental (IDENTITY 1,1)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentCostListDetailRecord', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentCostListDetailRecord', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentCostListDetailRecord', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro histórico de cambios o entradas de costo en el detalle de listas de precios de consignación de inventario. Guarda cada modificación o versión de costo asociada a un ítem de lista de consignación, con trazabilidad de quién y cuándo lo registró.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentCostListDetailRecord';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentCostListDetailRecord';
