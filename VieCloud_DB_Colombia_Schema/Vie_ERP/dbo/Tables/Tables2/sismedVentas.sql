CREATE TABLE [dbo].[sismedVentas] (
    [Cedula]                CHAR (15)     NOT NULL,
    [Nombre]                CHAR (250)    NOT NULL,
    [Ingreso]               CHAR (10)     NOT NULL,
    [CodigoServicio]        VARCHAR (20)  NOT NULL,
    [NombreServicio]        VARCHAR (200) NOT NULL,
    [FechaServicio]         DATETIME      NOT NULL,
    [Cantidad]              INT           NOT NULL,
    [ValorUnitario]         NUMERIC (18)  NOT NULL,
    [Descuento]             NUMERIC (18)  NOT NULL,
    [Total]                 NUMERIC (18)  NOT NULL,
    [Categoria]             VARCHAR (123) NOT NULL,
    [GrupoAtencion]         VARCHAR (123) NOT NULL,
    [EntidadAdministradora] VARCHAR (123) NULL,
    [TerceroEntidad]        VARCHAR (318) NULL,
    [UnidadesFuncionales]   VARCHAR (73)  NOT NULL,
    [CentroCosto]           VARCHAR (223) NOT NULL,
    [Factura]               VARCHAR (15)  NOT NULL,
    [UsuarioFacturacion]    VARCHAR (20)  NOT NULL,
    [UsuarioCargo]          VARCHAR (20)  NOT NULL,
    [FechaFactura]          DATETIME      NOT NULL,
    [CodeCUM]               VARCHAR (20)  NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CUM (Código Único de Medicamentos) del fármaco o medicamento dispensado. VARCHAR(20), nullable. Identifica medicamentos en el catálogo nacional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'CodeCUM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el codigo CUM', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'CodeCUM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'CodeCUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de generación de la factura de venta. DATETIME, requerida. Marca temporal del documento de facturación emitido.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'FechaFactura';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la fecha de la factura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'FechaFactura';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'FechaFactura';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cargo o rol del usuario que registró la transacción. VARCHAR(20), requerida. Posición funcional del operario de facturación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'UsuarioCargo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el cargo del usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'UsuarioCargo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'UsuarioCargo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario del sistema que ejecutó la facturación. VARCHAR(20), requerida. Login o identificador del operario que facturó el servicio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'UsuarioFacturacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la facturación del usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'UsuarioFacturacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'UsuarioFacturacion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de factura o comprobante de venta. VARCHAR(15), requerida. Identificador único del documento fiscal emitido.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'Factura';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la factura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'Factura';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'Factura';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Centro de costo asociado a la prestación. VARCHAR(223), requerida. Unidad contable que absorbe el gasto del servicio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'CentroCosto';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el centro de costo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'CentroCosto';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'CentroCosto';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad funcional donde se prestó el servicio (urgencia, consulta, hospitalización, laboratorio, etc). VARCHAR(73), requerida.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'UnidadesFuncionales';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la unidades funcionales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'UnidadesFuncionales';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'UnidadesFuncionales';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tercero o entidad contratante/responsable del pago. VARCHAR(318), nullable. Asegurador, empresa o institución asociada a la atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'TerceroEntidad';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la entidad como tercero', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'TerceroEntidad';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'TerceroEntidad';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Entidad administradora de salud (EAS, EPS, IPS). VARCHAR(123), nullable. Operador responsable de la cobertura y administración.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'EntidadAdministradora';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la entidad administradora', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'EntidadAdministradora';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'EntidadAdministradora';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Grupo de atención o tipología de servicio (consulta, urgencia, internación, procedimiento, etc). VARCHAR(123), requerida.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'GrupoAtencion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el grupo de atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'GrupoAtencion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'GrupoAtencion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Categoría o clasificación del servicio prestado. VARCHAR(123), requerida. Agrupa servicios por tipo (diagnóstico, terapia, farmacia, etc).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'Categoria';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la categoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'Categoria';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'Categoria';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total de la venta incluyendo descuentos. NUMERIC(18), requerida. Importe final facturado por el servicio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'Total';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el total', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'Total';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'Total';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de descuento aplicado. NUMERIC(18), requerida. Rebaja o deducción sobre el monto de la prestación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'Descuento';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el descuento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'Descuento';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'Descuento';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Precio unitario del servicio o medicamento. NUMERIC(18), requerida. Costo individual antes de aplicar cantidad y descuentos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'ValorUnitario';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el valor unitario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'ValorUnitario';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'ValorUnitario';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades del servicio o medicamento facturado. INT, requerida. Número de dosis, procedimientos o ítems.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'Cantidad';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la cantidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'Cantidad';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'Cantidad';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que se prestó el servicio de atención. DATETIME, requerida. Marca temporal de la consulta, procedimiento o dispensación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'FechaServicio';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la fecha del servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'FechaServicio';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'FechaServicio';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción o nombre del servicio prestado. VARCHAR(200), requerida. Denominación legible de la prestación (ej: consulta cardiólogo, laboratorio).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'NombreServicio';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el nombre del servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'NombreServicio';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'NombreServicio';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador del servicio en catálogo. VARCHAR(20), requerida. Referencia única del procedimiento, medicamento o prestación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'CodigoServicio';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el codigo del servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'CodigoServicio';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'CodigoServicio';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso o admisión del paciente. CHAR(10), requerida. Identificador de la atención o episodio asistencial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'Ingreso';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'Ingreso';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'Ingreso';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del paciente o responsable de la atención. CHAR(250), requerida. Identificación nominal de la persona atendida.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'Nombre';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el nombre del sistema de ventas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'Nombre';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'Nombre';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cédula o documento de identificación del paciente. CHAR(15), requerida. PII: número de identificación única del paciente (cédula, pasaporte, documento).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'Cedula';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la cédula', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'Cedula';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'Cedula';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de ventas y servicios facturados a pacientes: incluye los servicios, procedimientos o medicamentos cobrados por ingreso, con sus valores, descuentos, totales, entidad pagadora y datos de facturación. Útil para reportes de facturación, cartera, RIPS y análisis de ingresos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas';
