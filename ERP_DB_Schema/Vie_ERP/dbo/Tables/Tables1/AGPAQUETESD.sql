CREATE TABLE [dbo].[AGPAQUETESD] (
    [ID]               INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDAGPAQUETE]      INT       NOT NULL,
    [CODPRODUC]        CHAR (20) NOT NULL,
    [CANTIDAD]         INT       NOT NULL,
    [CANTIDADPROBABLE] INT       NOT NULL,
    [PROBABILIDADUSO]  INT       NOT NULL,
    [IdCostCenter]     INT       NULL,
    CONSTRAINT [PK_AGPAQUETESD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_AGPAQUETESD_AGPAQUETES] FOREIGN KEY ([IDAGPAQUETE]) REFERENCES [dbo].[AGPAQUETES] ([ID]),
    CONSTRAINT [FK_AGPAQUETESD_IHLISTPRO] FOREIGN KEY ([CODPRODUC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC]),
    CONSTRAINT [FK_AGPAQUETESD_Payroll.CostCenter] FOREIGN KEY ([IdCostCenter]) REFERENCES [Payroll].[CostCenter] ([Id])
);


GO
ALTER TABLE [dbo].[AGPAQUETESD] NOCHECK CONSTRAINT [FK_AGPAQUETESD_AGPAQUETES];


GO
ALTER TABLE [dbo].[AGPAQUETESD] NOCHECK CONSTRAINT [FK_AGPAQUETESD_IHLISTPRO];


GO
ALTER TABLE [dbo].[AGPAQUETESD] NOCHECK CONSTRAINT [FK_AGPAQUETESD_Payroll.CostCenter];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Se guarda el id asociado al centro de costo ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPAQUETESD', @level2type = N'COLUMN', @level2name = N'IdCostCenter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje Probabilidad de uso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPAQUETESD', @level2type = N'COLUMN', @level2name = N'PROBABILIDADUSO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad Probable Uso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPAQUETESD', @level2type = N'COLUMN', @level2name = N'CANTIDADPROBABLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad Solicitado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPAQUETESD', @level2type = N'COLUMN', @level2name = N'CANTIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de los Productos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPAQUETESD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de los Paquetes Quirurgicos que guardan en tabla AGPAQUETES', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPAQUETESD', @level2type = N'COLUMN', @level2name = N'IDAGPAQUETE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPAQUETESD', @level2type = N'COLUMN', @level2name = N'ID';

