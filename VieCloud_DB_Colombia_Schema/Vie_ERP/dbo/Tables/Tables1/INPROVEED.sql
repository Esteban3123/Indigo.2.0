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
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[INPROVEED].[CODIGONIT]
    WITH (LABEL = 'Confidential - Financial', INFORMATION_TYPE = 'Financial');



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano de retención en la fuente por procedimiento/servicio del proveedor. BIT. Campo sin documentación clara en el sistema legado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROVEED', @level2type = N'COLUMN', @level2name = N'RETPERPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'(No se encontró documentación de este campo en la solución de crystal ni información por parte de los desarrolladores.)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROVEED', @level2type = N'COLUMN', @level2name = N'RETPERPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROVEED', @level2type = N'COLUMN', @level2name = N'RETPERPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano que especifica si el proveedor está obligado a facturar con IVA (Impuesto al Valor Agregado). BIT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROVEED', @level2type = N'COLUMN', @level2name = N'IVAPORVEE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si realiza IVA el proveedor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROVEED', @level2type = N'COLUMN', @level2name = N'IVAPORVEE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROVEED', @level2type = N'COLUMN', @level2name = N'IVAPORVEE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre, correo o identificación del contacto principal en el proveedor. CHAR(40). Referente de comunicación para gestión de facturas y pagos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROVEED', @level2type = N'COLUMN', @level2name = N'CONPROVEE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contacto en el Proveedor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROVEED', @level2type = N'COLUMN', @level2name = N'CONPROVEE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROVEED', @level2type = N'COLUMN', @level2name = N'CONPROVEE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cupo máximo aprobado en dinero para compras/servicios al proveedor. NUMERIC(15). Límite de crédito o valor autorizado de contratos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROVEED', @level2type = N'COLUMN', @level2name = N'CUPMAXPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cupo Maximo Aprobado por el Proveedor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROVEED', @level2type = N'COLUMN', @level2name = N'CUPMAXPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROVEED', @level2type = N'COLUMN', @level2name = N'CUPMAXPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Plazo máximo en días para el pago de facturas del proveedor. TINYINT. Términos de crédito acordados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROVEED', @level2type = N'COLUMN', @level2name = N'PLAPROVEE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Plazo Maximo para el pago de las Facturas del Proveedor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROVEED', @level2type = N'COLUMN', @level2name = N'PLAPROVEE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROVEED', @level2type = N'COLUMN', @level2name = N'PLAPROVEE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del municipio y departamento donde reside el proveedor. CHAR(5). FK a INMunicip. Ubicación geográfica de la entidad proveedora.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROVEED', @level2type = N'COLUMN', @level2name = N'DEPMUNCOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Municipio y Departamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROVEED', @level2type = N'COLUMN', @level2name = N'DEPMUNCOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROVEED', @level2type = N'COLUMN', @level2name = N'DEPMUNCOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de fax del proveedor o su contacto. CHAR(15). Medio de comunicación complementario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROVEED', @level2type = N'COLUMN', @level2name = N'FAXPROVEE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Fax del Proveedor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROVEED', @level2type = N'COLUMN', @level2name = N'FAXPROVEE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROVEED', @level2type = N'COLUMN', @level2name = N'FAXPROVEE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de celular o móvil del proveedor o contacto. CHAR(15). Canal directo para gestión de órdenes y pagos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROVEED', @level2type = N'COLUMN', @level2name = N'MOVPROVEE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero Movil del Proveedor o Contacto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROVEED', @level2type = N'COLUMN', @level2name = N'MOVPROVEE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROVEED', @level2type = N'COLUMN', @level2name = N'MOVPROVEE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número telefónico fijo del proveedor. CHAR(15). Contacto principal para comunicación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROVEED', @level2type = N'COLUMN', @level2name = N'TELPROVEE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero Telefonico del Proveedor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROVEED', @level2type = N'COLUMN', @level2name = N'TELPROVEE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROVEED', @level2type = N'COLUMN', @level2name = N'TELPROVEE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección física del proveedor. CHAR(60) NULL. Ubicación para correspondencia y auditoría.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROVEED', @level2type = N'COLUMN', @level2name = N'DIRPROVEE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Direccion del Proveedor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROVEED', @level2type = N'COLUMN', @level2name = N'DIRPROVEE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROVEED', @level2type = N'COLUMN', @level2name = N'DIRPROVEE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre legal o comercial del proveedor (farmacéutico, distribuidor, laboratorio, hospital, etc.). CHAR(60).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROVEED', @level2type = N'COLUMN', @level2name = N'NOMPROVEE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del Proveedor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROVEED', @level2type = N'COLUMN', @level2name = N'NOMPROVEE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROVEED', @level2type = N'COLUMN', @level2name = N'NOMPROVEE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NIT o número de identificación tributaria del proveedor. CHAR(15) MASKED Nit_Ofuscado PII. Identificación fiscal única.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROVEED', @level2type = N'COLUMN', @level2name = N'CODIGONIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Nit', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROVEED', @level2type = N'COLUMN', @level2name = N'CODIGONIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROVEED', @level2type = N'COLUMN', @level2name = N'CODIGONIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único identificador del proveedor en el sistema Indigo Vie. CHAR(15) PK. Equivalente a ID de la entidad proveedora.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROVEED', @level2type = N'COLUMN', @level2name = N'CODPROVEE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Proveedor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROVEED', @level2type = N'COLUMN', @level2name = N'CODPROVEE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROVEED', @level2type = N'COLUMN', @level2name = N'CODPROVEE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Maestro de proveedores del sistema. Registra la información de cada empresa o persona que provee bienes y servicios a la institución, incluyendo datos de contacto, ubicación geográfica, condiciones comerciales y configuración tributaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROVEED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROVEED';
