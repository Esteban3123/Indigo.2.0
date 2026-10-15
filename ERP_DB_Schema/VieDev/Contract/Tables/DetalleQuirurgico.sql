CREATE TABLE [Contract].[DetalleQuirurgico] (
    [Code]           NVARCHAR (255) NULL,
    [hijo]           NVARCHAR (255) NULL,
    [ServiceAmount]  NVARCHAR (255) NULL,
    [DefaultService] NVARCHAR (255) NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena el servicio predeterminado.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DetalleQuirurgico', @level2type = N'COLUMN', @level2name = N'DefaultService';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica el monto del servicio.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DetalleQuirurgico', @level2type = N'COLUMN', @level2name = N'ServiceAmount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena hijo del detalle quirurgico.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DetalleQuirurgico', @level2type = N'COLUMN', @level2name = N'hijo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Detalle Quirurgico.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DetalleQuirurgico', @level2type = N'COLUMN', @level2name = N'Code';

