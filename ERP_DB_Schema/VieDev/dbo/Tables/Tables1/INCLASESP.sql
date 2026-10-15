CREATE TABLE [dbo].[INCLASESP] (
    [CODCLAPROV] CHAR (2)  NOT NULL,
    [DESCLAPROV] CHAR (40) NOT NULL,
    CONSTRAINT [PK_INCLAPROVEE] PRIMARY KEY CLUSTERED ([CODCLAPROV] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de la Clase de Proveedor    Nota: Ejemplo - Proveedor Inventarios, Proveedor Activos Fijos, Proveedor Dietas, Proveedor Medicamentos, etc.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCLASESP', @level2type = N'COLUMN', @level2name = N'DESCLAPROV';

