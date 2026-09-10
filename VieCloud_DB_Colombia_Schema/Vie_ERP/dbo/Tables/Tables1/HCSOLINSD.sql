CREATE TABLE [dbo].[HCSOLINSD] (
    [CODCONCEC]        CHAR (10)                                                                        NOT NULL,
    [IPCODPACI]        VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]        CHAR (10)                                                                        NOT NULL,
    [CODCENATE]        CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]        CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]        CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [CODPRODUC]        CHAR (20)                                                                        NOT NULL,
    [CANPEDPRO]        INT                                                                              NOT NULL,
    [PREFECANT]        BIT                                                                              NOT NULL,
    [MOTPREANT]        CHAR (250)                                                                       NULL,
    [PREESTADO]        INT                                                                              NULL,
    [INDAUDFOR]        NUMERIC (18)                                                                     NOT NULL,
    [JUSTIINSU]        VARCHAR (MAX)                                                                    NULL,
    [MANEJOEXTRA]      BIT                                                                              NULL,
    [IDHCSOLINSC]      INT                                                                              NULL,
    [ID]               INT                                                                              IDENTITY (1, 1) NOT NULL,
    [TipoPrescripcion] INT                                                                              NULL,
    [IdNursingPackage]      INT                                                                              NULL,
    [AuthorizationEventId]  INT                                                                              NULL,
    CONSTRAINT [PK_HCSOLINSD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCSOLINSD_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCSOLINSD_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCSOLINSD_INPacient] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCSOLINSD_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCSOLINSD_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_HCSOLINSD_IPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_IDHCSOLINSC_ID] FOREIGN KEY ([IDHCSOLINSC]) REFERENCES [dbo].[HCSOLINSC] ([ID])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCSOLINSD].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCSOLINSD].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');




GO



GO



GO



GO



GO



GO



GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_HCSOLINSD_IPCODPACI_NUMINGRES]
    ON [dbo].[HCSOLINSD]([IPCODPACI] ASC, [NUMINGRES] ASC)
    INCLUDE([CANPEDPRO], [CODCONCEC], [CODPRODUC]);


GO
CREATE NONCLUSTERED INDEX [IX_HCSOLINSD_CODCONCEC_MANEJOEXTRA]
    ON [dbo].[HCSOLINSD]([CODCONCEC] ASC, [MANEJOEXTRA] ASC)
    INCLUDE([CANPEDPRO], [CODPRODUC], [JUSTIINSU]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del paquete de enfermería (INT, FK→AGPAQUETES.ID); almacena el ID del paquete si el producto fue prescrito como parte de un paquete de cuidados de enfermería.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLINSD', @level2type = N'COLUMN', @level2name = N'IdNursingPackage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el Id del paquete de enfermería al que pertenece el producto si este fue guardado como paquete.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLINSD', @level2type = N'COLUMN', @level2name = N'IdNursingPackage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLINSD', @level2type = N'COLUMN', @level2name = N'IdNursingPackage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de prescripción (INT); categoría que clasifica la naturaleza de la prescripción (ej: medicamento, insumo, procedimiento de enfermería).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLINSD', @level2type = N'COLUMN', @level2name = N'TipoPrescripcion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el tipo de Prescripción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLINSD', @level2type = N'COLUMN', @level2name = N'TipoPrescripcion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLINSD', @level2type = N'COLUMN', @level2name = N'TipoPrescripcion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único/consecutivo de la línea de insumo (INT IDENTITY, PK); clave primaria que genera automáticamente el número de registro en la tabla de detalle de prescripciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLINSD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLINSD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLINSD', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con tabla cabecera HCSOLINSC (INT, FK); vincula la línea de insumo con su prescripción/atención madre en la tabla de encabezado de insumos y servicios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLINSD', @level2type = N'COLUMN', @level2name = N'IDHCSOLINSC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con la tabla cabecera de insumos: HCSOLINSC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLINSD', @level2type = N'COLUMN', @level2name = N'IDHCSOLINSC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLINSD', @level2type = N'COLUMN', @level2name = N'IDHCSOLINSC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Manejo extramural/domiciliario (BIT); Verdadero=Sí, Falso=No; indica si el insumo o medicamento requiere administración o seguimiento fuera de la institución.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLINSD', @level2type = N'COLUMN', @level2name = N'MANEJOEXTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Manejo extramural:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLINSD', @level2type = N'COLUMN', @level2name = N'MANEJOEXTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLINSD', @level2type = N'COLUMN', @level2name = N'MANEJOEXTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación clínica del insumo (VARCHAR MAX, nullable); texto libre donde se documentan razones médicas o administrativas para la prescripción de insumo/medicamento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLINSD', @level2type = N'COLUMN', @level2name = N'JUSTIINSU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justificación del insumo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLINSD', @level2type = N'COLUMN', @level2name = N'JUSTIINSU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLINSD', @level2type = N'COLUMN', @level2name = N'JUSTIINSU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador/control de auditoría y facturación (NUMERIC 18); registro numérico para trazabilidad de auditoría interna, validación contable y control de glosas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLINSD', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Control Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLINSD', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLINSD', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de medicamento/insumo (INT, nullable); 1=Realiza interfase con Inventarios/Kardex, 2=No realiza interfase (producto ya existe en Kardex del paciente, centro y unidad funcional actual).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLINSD', @level2type = N'COLUMN', @level2name = N'PREESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del Medicamento  1: Realiza Interfase con Inventarios  2: No Realiza Interfase con Inventarios x existencia del producto en el Kardex del Paciente correspondiente a la Centro de Atencion y Unidad Funcional Actual.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLINSD', @level2type = N'COLUMN', @level2name = N'PREESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLINSD', @level2type = N'COLUMN', @level2name = N'PREESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo de prescripción anterior (CHAR 250, nullable); documentación de la razón clínica o administrativa de cambios previos en la prescripción.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLINSD', @level2type = N'COLUMN', @level2name = N'MOTPREANT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivo Prescripcion Anterior', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLINSD', @level2type = N'COLUMN', @level2name = N'MOTPREANT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLINSD', @level2type = N'COLUMN', @level2name = N'MOTPREANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prescripción con fecha anterior (BIT); indicador booleano si la prescripción corresponde a orden histórica o reactivación de prescripción previa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLINSD', @level2type = N'COLUMN', @level2name = N'PREFECANT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Prescripcion Fecha Anterior', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLINSD', @level2type = N'COLUMN', @level2name = N'PREFECANT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLINSD', @level2type = N'COLUMN', @level2name = N'PREFECANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad pedida/prescrita del producto (INT); número de unidades de insumo, medicamento o dosis solicitadas en la línea de prescripción.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLINSD', @level2type = N'COLUMN', @level2name = N'CANPEDPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Pedida del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLINSD', @level2type = N'COLUMN', @level2name = N'CANPEDPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLINSD', @level2type = N'COLUMN', @level2name = N'CANPEDPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del producto/insumo/medicamento (CHAR 20); identificador único del artículo farmacéutico, insumo médico o producto de salud prescrito.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLINSD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLINSD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLINSD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de la salud (CHAR 20, MASKED, FK→INPROFSAL.CODPROSAL); identificación del médico, enfermero o profesional que realiza la prescripción.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLINSD', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLINSD', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLINSD', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional (CHAR 10, FK→INUNIFUNC); identifica el servicio/departamento (urgencias, hospitalización, consulta externa, UCI) donde se prescribe.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLINSD', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLINSD', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLINSD', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención/institución (CHAR 10, FK→ADCENATEN); identifica la sede, hospital o clínica donde se realiza la prescripción.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLINSD', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLINSD', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLINSD', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del ingreso/atención del paciente (CHAR 10, FK→ADINGRESO); vincula la prescripción con el episodio de atención o hospitalización específico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLINSD', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLINSD', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLINSD', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente/identificación (VARCHAR 25, MASKED, PII, FK→INPACIENT); cédula, documento o ID único del paciente (ofuscado para cumplimiento de privacidad).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLINSD', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLINSD', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLINSD', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del consecutivo (CHAR 10); número secuencial o identificador de referencia para rastreo administrativo de la prescripción en el flujo de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLINSD', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLINSD', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLINSD', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Solicitudes de insumos y productos (medicamentos, dispositivos médicos, material quirúrgico) generadas durante un ingreso hospitalario. Registra qué producto solicitó cada profesional de salud para un paciente, en qué cantidad, con qué estado de aprobación y la justificación clínica correspondiente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLINSD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLINSD';
