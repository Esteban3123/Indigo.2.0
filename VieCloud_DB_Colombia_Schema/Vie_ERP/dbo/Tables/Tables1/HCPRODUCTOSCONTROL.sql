CREATE TABLE [dbo].[HCPRODUCTOSCONTROL] (
    [ID]               INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [INCONSECUCONTROL] INT                                                                              NOT NULL,
    [FECHAREGISTRO]    DATETIME                                                                         NOT NULL,
    [CODCENATE]        CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]        CHAR (10)                                                                        NOT NULL,
    [IPCODPACI]        VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]        CHAR (10)                                                                        NOT NULL,
    [NUMEFOLIO]        NCHAR (10)                                                                       NULL,
    [CODPRODUC]        CHAR (20)                                                                        NOT NULL,
    [CODPROSAL]        CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [TIPOSOLICITUD]    INT                                                                              NOT NULL,
    [USUARIOENFERMRIA] CHAR (20)                                                                        NULL,
    [DESADMINI]        VARCHAR (MAX)                                                                    NULL,
    [CANTIDAD]         INT                                                                              NULL,
    [DURACIDOS]        CHAR (20)                                                                        NULL,
    [CODCONCECENF]     CHAR (10)                                                                        NULL,
    [CODCONCECMED]     NUMERIC (18)                                                                     NULL,
    [CODCONCECDEVO]    INT                                                                              NULL,
    [MANEXTPRO]        BIT                                                                              CONSTRAINT [DF_MANEXTPRO] DEFAULT ((0)) NOT NULL,
    [ORIGENPRODUCTO]   TINYINT                                                                          NULL,
    CONSTRAINT [PK_HCPRODUCTOSCONTROL] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCPRODUCTOSCONTROL_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCPRODUCTOSCONTROL_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCPRODUCTOSCONTROL_IHLISTPRO] FOREIGN KEY ([CODPRODUC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC]),
    CONSTRAINT [FK_HCPRODUCTOSCONTROL_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCPRODUCTOSCONTROL_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCPRODUCTOSCONTROL].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCPRODUCTOSCONTROL].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
CREATE NONCLUSTERED INDEX [IX_HCPRODUCTOSCONTROL_NUMEFOLIO_NUMINGRES_CODPRODUC]
    ON [dbo].[HCPRODUCTOSCONTROL]([NUMEFOLIO] ASC, [NUMINGRES] ASC, [CODPRODUC] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Origen del producto: 1=Prescripción de medicamentos, 2=Prescripción de mezclas. Tipo de origen que clasifica la fuente de la orden de medicamento o producto farmacéutico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'ORIGENPRODUCTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Columna para determinar el origen de un producto.  1. Preescripción de Medicamentos.  2. Preescripción de Mezclas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'ORIGENPRODUCTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'ORIGENPRODUCTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (SÍ/NO) que determina si el producto corresponde a un plan de manejo externo, tratamiento ambulatorio o seguimiento fuera del centro de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'MANEXTPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Este Producto Corresponde a un plan de Manejo Externo?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'MANEXTPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'MANEXTPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del consecutivo de devolución de medicamento/producto, vinculado a tabla HCDEVMEDC para rastrear devoluciones, reintegros o cancelaciones de suministro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'CODCONCECDEVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dato que se llena cuando se haga una devolución, tabla HCDEVMEDC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'CODCONCECDEVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'CODCONCECDEVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del consecutivo de la prescripción médica, vinculado a tabla HCPRESCRD. Registra cuándo el médico solicitó/prescribió el medicamento o producto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'CODCONCECMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Datos que se llena cuando el medico solicito tabla HCPRESCRD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'CODCONCECMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'CODCONCECMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del consecutivo de cabecera de suministro de enfermería, vinculado a tabla HCSOLINSC. Identifica el folio de despacho/entrega desde enfermería.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'CODCONCECENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del consecutivo de la cabecera de suministro desde enfermeria tabla HCSOLINSC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'CODCONCECENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'CODCONCECENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración del tratamiento: parámetro que especifica si es continuo, fijo o por días. Define la modalidad temporal de administración del producto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'DURACIDOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Duracion  Tratamiento continuo  fijo  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'DURACIDOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'DURACIDOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad solicitada, ordenada, entregada o devuelta del producto. Registra cantidad ordenada por médico, enfermería o cantidad devuelta en devoluciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'CANTIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad devuleve  cantidad ordena  cantidad ordena enfermera', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'CANTIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'CANTIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción detallada de la administración del medicamento/producto: vía, frecuencia, dosis, instrucciones especiales o notas de enfermería.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'DESADMINI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción de la administracion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'DESADMINI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'DESADMINI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (código/login) de la enfermera o profesional de enfermería que registró la solicitud, despacho o administración del medicamento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'USUARIOENFERMRIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que registro la solicitud enfermera', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'USUARIOENFERMRIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'USUARIOENFERMRIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de solicitud del medicamento: 1=Orden médica, 2=Solicitud de enfermería, 3=Devolutivos. Clasifica el origen de la petición de producto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'TIPOSOLICITUD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Orden medica  2 - Enfermeria   3 - Devolutivos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'TIPOSOLICITUD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'TIPOSOLICITUD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de la salud (médico, enfermera, profesional) que ordenó o prescribió el medicamento. PII: Identificación ofuscada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo del profesional que ordeno el medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del producto/medicamento de control, vinculado a catálogo de productos IHLISTPRO. Identifica el fármaco, insumo o producto a controlar.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigod el producto de control', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio del paciente, receta o orden de medicamentos. Referencia documental del paciente para trazar la prescripción.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Folio paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso/admisión del paciente en el centro de atención, vinculado a tabla ADINGRESO. Identifica la atención o evento clínico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente: cédula, documento de identidad o identificación única del paciente. PII: Identificación ofuscada con máscara.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de unidad funcional (servicio, departamento, piso), vinculado a tabla INUNIFUNC. Identifica la área de atención donde se suministra el producto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención/institución, vinculado a tabla ADCENATEN. Identifica la sede o establecimiento de salud responsable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Centro Atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de registro del control de medicamento/producto en el sistema. Marca temporal de creación del movimiento de suministro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo secuencial de medicamentos de control registrados en la tabla. Identifica el número de orden dentro del control de fármacos controlados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'INCONSECUCONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que me va a guardar el conscutivo de los medicamentos que son de control.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'INCONSECUCONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'INCONSECUCONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (clave primaria) del registro de control de productos/medicamentos. Consecutivo autoincrementable de la tabla HCPRODUCTOSCONTROL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de control y administración de productos (medicamentos e insumos) aplicados a un paciente durante su ingreso hospitalario. Incluye trazabilidad de quién solicitó el producto, quién lo administró, cantidades, dosis, conciliaciones y el origen del producto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODUCTOSCONTROL';
