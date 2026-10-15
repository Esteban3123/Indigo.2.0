CREATE TABLE [dbo].[IHDCIMEDI] (
    [CODDCIMED] VARCHAR (20) NOT NULL,
    [DESDCIMED] CHAR (255)   NOT NULL,
    CONSTRAINT [PK_IHDCIMEDI] PRIMARY KEY CLUSTERED ([CODDCIMED] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de la Denominacion Comun Internacion del Medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHDCIMEDI', @level2type = N'COLUMN', @level2name = N'DESDCIMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Denominacion Comun Internacion del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHDCIMEDI', @level2type = N'COLUMN', @level2name = N'CODDCIMED';

