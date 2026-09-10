CREATE TABLE [dbo].[INAREASER] (
    [ARSCODIGO] CHAR (10)     NOT NULL,
    [ARSDESCRI] VARCHAR (100) NOT NULL,
    [CODCENCOS] CHAR (14)     NOT NULL,
    [CODCUEING] CHAR (15)     NOT NULL,
    [CODCUSUMP] CHAR (15)     NOT NULL,
    [MANAREURG] BIT           NOT NULL,
    [ARPRESATE] CHAR (1)      NOT NULL,
    [CODCUEDES] CHAR (15)     NULL,
    [CODCUINGP] CHAR (15)     NULL,
    [CODCUEHON] CHAR (15)     NULL,
    [TERMEDING] CHAR (1)      NULL,
    [CODBODEGA] VARCHAR (20)  NOT NULL,
    [CODCENATE] CHAR (10)     NOT NULL,
    [CENATEINV] CHAR (10)     NULL,
    [TERGASMED] BIT           NOT NULL,
    [ESTAREASE] BIT           NOT NULL,
    [CCINGTRA]  VARCHAR (16)  NULL,
    CONSTRAINT [PK_INAREASER] PRIMARY KEY CLUSTERED ([ARSCODIGO] ASC),
    CONSTRAINT [FK_INAREASER_IHBODEGAS] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_INAREASER_INCENTCOS] FOREIGN KEY ([CODCENCOS]) REFERENCES [dbo].[INCENTCOS] ([CODCENCOS])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código contable NIFF (Número de Identificación Fiscal Federal) por ingreso/trámite. VARCHAR(16), referencia contable para conciliación de movimientos de pacientes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'CCINGTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Información NIFF Cx Ingreso tramite', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'CCINGTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'CCINGTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del área de servicio: 1=Activo, 0=Inactivo. BIT, controla si el área prestadora atiende pacientes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'ESTAREASE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado Area de Servicio 1:Activo;0:Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'ESTAREASE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'ESTAREASE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si existe tercero (médico) con cuenta de ingreso a entidad: 1=Sí, 0=No. BIT, para comisiones o distribución de ingresos médicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'TERGASMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tercero del medico en cuenta de ingreso a entidad (0=NO;1=SI)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'TERGASMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'TERGASMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código alterno del centro de atención para inventarios. CHAR(10), equivalente a unidad funcional o sucursal donde se controla stock.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'CENATEINV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'CENATEINV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'CENATEINV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención (unidad funcional, clínica, urgencia, consulta). CHAR(10), identificador FK a INCENTCOS para ubicación física.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de bodega/almacén de inventarios. VARCHAR(20), ubicación de stock de medicamentos, suministros y dispositivos médicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'CODBODEGA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Bodegas de Inventarios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'CODBODEGA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'CODBODEGA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tercero (proveedor/médico) con cuenta de ingreso a entidad: 1=Sí, 0=No. CHAR(1), habilita facturación a terceros en esta área.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'TERMEDING';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tercero del Medico en Cuenta de Ingresos a Entidad 0:No;1:Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'TERMEDING';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'TERMEDING';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de cuenta contable para honorarios médicos. CHAR(15), registro de ingresos por servicios profesionales de médicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'CODCUEHON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Cuenta Honorarios Medicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'CODCUEHON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'CODCUEHON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de cuenta contable de ingreso para pacientes particulares. CHAR(15), captura de ingresos por atención privada/copago.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'CODCUINGP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Cuenta Contable Para Ingreso de Pacientes Particulares', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'CODCUINGP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'CODCUINGP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de cuenta contable para descuentos y ajustes. CHAR(15), nullable, registra rebajas, promociones y notas crédito.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'CODCUEDES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta Contable de Descuentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'CODCUEDES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'CODCUEDES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Área donde se presta la atención (código/tipo de servicio). CHAR(1), classifica urgencia, consulta externa, hospitalización, procedimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'ARPRESATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Area donde se Presta la Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'ARPRESATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'ARPRESATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Maneja área de urgencias: 1=Sí, 0=No. BIT, indica si el área atiende pacientes de emergencia/triaje.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'MANAREURG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Maneja Area de Urgencias 1:Si;2:No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'MANAREURG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'MANAREURG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de cuenta contable de suministros a pacientes. CHAR(15), gasto de medicamentos, dispositivos y materiales consumibles.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'CODCUSUMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta de Suministros a Pacientes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'CODCUSUMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'CODCUSUMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de cuenta contable de ingreso general. CHAR(15), captura el ingreso operacional del área de servicio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'CODCUEING';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Cuenta de Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'CODCUEING';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'CODCUEING';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de costo (unidad funcional, departamento). CHAR(14), FK a INCENTCOS, agrupa gastos y presupuestos del área.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'CODCENCOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de Centros de Costos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'CODCENCOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'CODCENCOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del área de servicio (urgencia, consulta, laboratorio, imagenología, quirófano). VARCHAR(100), nombre legible del servicio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'ARSDESCRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del Area de Servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'ARSDESCRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'ARSDESCRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del área de servicio. CHAR(10), PK, identificador de cada unidad funcional prestadora de servicios clínicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'ARSCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Area de Servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'ARSCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'ARSCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Áreas de servicio o unidades de atención dentro de la institución de salud. Define cada área (urgencias, hospitalización, consulta externa, etc.) con sus cuentas contables asociadas, centro de atención, bodega de insumos y parámetros operativos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER';
