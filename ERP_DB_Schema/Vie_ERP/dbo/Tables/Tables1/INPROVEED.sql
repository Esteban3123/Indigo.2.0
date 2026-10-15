CREATE TABLE [dbo].[INPROVEED] (
    [CODPROVEE] CHAR (15)                                                          NOT NULL,
    [CODIGONIT] CHAR (15) MASKED WITH (FUNCTION = 'partial(0, "Nit_Ofuscado", 0)') NOT NULL,
    [NOMPROVEE] CHAR (60)                                                          NOT NULL,
    [DIRPROVEE] CHAR (60)                                                          NULL,
    [TELPROVEE] CHAR (15)                                                          NULL,
    [MOVPROVEE] CHAR (15)                                                          NULL,
    [FAXPROVEE] CHAR (15)                                                          NULL,
    [DEPMUNCOD] CHAR (5)                                                           NOT NULL,
    [PLAPROVEE] TINYINT                                                            NOT NULL,
    [CUPMAXPRO] NUMERIC (15)                                                       NOT NULL,
    [CONPROVEE] CHAR (40)                                                          NOT NULL,
    [IVAPORVEE] BIT                                                                NOT NULL,
    [RETPERPRO] BIT                                                                NOT NULL,
    CONSTRAINT [PK_INPROVEED] PRIMARY KEY CLUSTERED ([CODPROVEE] ASC),
    CONSTRAINT [FK_INPROVEED_INMunicip] FOREIGN KEY ([DEPMUNCOD]) REFERENCES [dbo].[INMUNICIP] ([DEPMUNCOD]) ON UPDATE CASCADE
);


GO
ALTER TABLE [dbo].[INPROVEED] NOCHECK CONSTRAINT [FK_INPROVEED_INMunicip];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(No se encontró documentación de este campo en la solución de crystal ni información por parte de los desarrolladores.)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROVEED', @level2type = N'COLUMN', @level2name = N'RETPERPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si realiza IVA el proveedor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROVEED', @level2type = N'COLUMN', @level2name = N'IVAPORVEE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contacto en el Proveedor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROVEED', @level2type = N'COLUMN', @level2name = N'CONPROVEE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cupo Maximo Aprobado por el Proveedor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROVEED', @level2type = N'COLUMN', @level2name = N'CUPMAXPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Plazo Maximo para el pago de las Facturas del Proveedor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROVEED', @level2type = N'COLUMN', @level2name = N'PLAPROVEE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Municipio y Departamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROVEED', @level2type = N'COLUMN', @level2name = N'DEPMUNCOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Fax del Proveedor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROVEED', @level2type = N'COLUMN', @level2name = N'FAXPROVEE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero Movil del Proveedor o Contacto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROVEED', @level2type = N'COLUMN', @level2name = N'MOVPROVEE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero Telefonico del Proveedor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROVEED', @level2type = N'COLUMN', @level2name = N'TELPROVEE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Direccion del Proveedor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROVEED', @level2type = N'COLUMN', @level2name = N'DIRPROVEE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del Proveedor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROVEED', @level2type = N'COLUMN', @level2name = N'NOMPROVEE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Nit', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROVEED', @level2type = N'COLUMN', @level2name = N'CODIGONIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Proveedor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROVEED', @level2type = N'COLUMN', @level2name = N'CODPROVEE';

