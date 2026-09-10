CREATE TABLE [dbo].[HCKARDPAC] (
    [NUMCONSEC]                    VARCHAR (50)                                                                     NOT NULL,
    [IPCODPACI]                    VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]                    CHAR (10)                                                                        NOT NULL,
    [CODCENATE]                    CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]                    CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]                    CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NULL,
    [CODPRODUC]                    CHAR (20)                                                                        NOT NULL,
    [CANPRODUCT]                   INT                                                                              NOT NULL,
    [TIPREGIST]                    CHAR (1)                                                                         NOT NULL,
    [HCPRESCRN]                    NUMERIC (18)                                                                     NULL,
    [HCSOLINSN]                    CHAR (10)                                                                        NULL,
    [HCCTRAPLN]                    CHAR (10)                                                                        NULL,
    [HCCTRAPLM]                    NUMERIC (18)                                                                     NULL,
    [CODDOCUME]                    CHAR (10)                                                                        NULL,
    [FECREGKAR]                    DATETIME                                                                         NOT NULL,
    [TIPORIREG]                    INT                                                                              NOT NULL,
    [DESMOVPRO]                    VARCHAR (500)                                                                    NOT NULL,
    [JUSANULAC]                    VARCHAR (2000)                                                                   NULL,
    [CONSECFAR]                    INT                                                                              NULL,
    [FECHAUTIL]                    DATETIME                                                                         NULL,
    [OBSERVACI]                    VARCHAR (8000)                                                                   NULL,
    [IdDetailPhysicalCUM]          INT                                                                              NULL,
    [SourceTable]                  VARCHAR (100)                                                                    NULL,
    [IdSourceTable]                INT                                                                              NULL,
    [CodeSusceptibleMixingStation] UNIQUEIDENTIFIER                                                                 NULL,
    CONSTRAINT [PK_HCKARDPAC] PRIMARY KEY CLUSTERED ([NUMCONSEC] ASC),
    CONSTRAINT [FK__HCKARDPAC__IdDet__3FC1C448] FOREIGN KEY ([IdDetailPhysicalCUM]) REFERENCES [MedicalHistory].[DetailPhysicalCUM] ([Id]),
    CONSTRAINT [FK_HCKARDPAC_HCKARDPAC] FOREIGN KEY ([NUMCONSEC]) REFERENCES [dbo].[HCKARDPAC] ([NUMCONSEC])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCKARDPAC].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCKARDPAC].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');




GO
CREATE NONCLUSTERED INDEX [IX_HCKARDPAC_CODPRODUC_CANPRODUCT_HCCTRAPLN_OBSERVACI]
    ON [dbo].[HCKARDPAC]([CODPRODUC] ASC)
    INCLUDE([CANPRODUCT], [HCCTRAPLN], [OBSERVACI]);


GO
CREATE NONCLUSTERED INDEX [IDX_HCKARDPAC_NUMINGRES]
    ON [dbo].[HCKARDPAC]([NUMINGRES] ASC)
    INCLUDE([IPCODPACI], [UFUCODIGO], [CODPRODUC], [CANPRODUCT], [TIPREGIST], [FECREGKAR], [TIPORIREG], [DESMOVPRO], [CONSECFAR]);


GO
CREATE NONCLUSTERED INDEX [IX_HCKARDPAC_CODPRODUC_TIPORIREG]
    ON [dbo].[HCKARDPAC]([CODPRODUC] ASC, [TIPORIREG] ASC)
    INCLUDE([CANPRODUCT], [NUMINGRES], [UFUCODIGO]);


GO
ALTER INDEX [IX_HCKARDPAC_CODPRODUC_TIPORIREG]
    ON [dbo].[HCKARDPAC] DISABLE;




GO
CREATE NONCLUSTERED INDEX [IX_HCKARDPAC_NUMINGRES_CODCENATE_IPCODPACI_UFUCODIGO_TIPORIREG_CANPRODUCT_CODPRODUC_DESMOVPRO_FECREGKAR_JUSANULAC_TIPREGIST]
    ON [dbo].[HCKARDPAC]([NUMINGRES] ASC, [CODCENATE] ASC, [IPCODPACI] ASC, [UFUCODIGO] ASC, [TIPORIREG] ASC)
    INCLUDE([CANPRODUCT], [CODPRODUC], [DESMOVPRO], [FECREGKAR], [JUSANULAC], [TIPREGIST]);


GO
CREATE NONCLUSTERED INDEX [IDX_HCKARDPAC_UFUCODIGO_NUMINGRES_TIPORIREG]
    ON [dbo].[HCKARDPAC]([UFUCODIGO] ASC, [NUMINGRES] ASC, [TIPORIREG] ASC)
    INCLUDE([IPCODPACI], [CODPRODUC], [CANPRODUCT], [TIPREGIST], [FECREGKAR], [DESMOVPRO], [CONSECFAR]);


GO
ALTER INDEX [IDX_HCKARDPAC_UFUCODIGO_NUMINGRES_TIPORIREG]
    ON [dbo].[HCKARDPAC] DISABLE;




GO
CREATE NONCLUSTERED INDEX [IX_HCKARDPAC_IPCODPACI_NUMINGRES_FECREGKAR_TIPORIREG]
    ON [dbo].[HCKARDPAC]([IPCODPACI] ASC, [NUMINGRES] ASC, [FECREGKAR] ASC, [TIPORIREG] ASC)
    INCLUDE([CANPRODUCT], [CODPRODUC], [CODPROSAL], [DESMOVPRO], [UFUCODIGO]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de la tabla origen del movimiento de kardex: si viene de prescripción médica (HCPRESCRA), mezcla, solicitud de insumos de enfermería, o reposición sin prescripción. INT, auditoría de trazabilidad farmacéutica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'IdSourceTable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda aqui el ID de la Tabla Origen:    Ejemplos:    1ero: Si el medicamento ES guardado desde una HC el id origen seria el  de la tabla HCPRASCRA, si el medicamento viene de orden de  una mezcla el id origen que queda aqui seria el id origen de la tabla de la mezcla.    2. Cuando se haga una reposicion desde solicitud de medicamento e insumos y el medicamento que va a guardar NO cuenta con una prescripcion medica va el el ID de la tabla de la solicitud de medicamentos e insumos, PERO SI el medicamento SI cuenta con una prescrpcion medica va el ID de la tabla Origen es decir HCPRESCRA ó mezclas etc.        ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'IdSourceTable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'IdSourceTable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la tabla origen del registro: HCPRESCRA (prescripción médica), mezcla, solicitud insumos enfermería, u otra fuente del movimiento de kardex. VARCHAR(100), trazabilidad de documento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'SourceTable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda aqui el Nombre de la Tabla Origen:    Ejemplos:    1ero: Si el medicamento ES guardado desde una HC la tabla Origen seria HCPRASCRA si el medicamento viene de orden de  una mezcla seria el nombre que queda aqui seria el nombre de la tabla de la mezcla.    2. Cuando se haga una reposicion desde solicitud de medicamento e insumos y el medicamento que va a guardar NO cuenta con una prescripcion medica va el Nombre de la tabla de la solicitud de medicamentos e insumos, PERO SI el medicamento SI cuenta con una prescrpcion medica va el Nombre de la tabla Origen es decir HCPRESCRA.            ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'SourceTable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'SourceTable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia a central de mezclas (DetailPhysicalCUM): almacena el ID cuando el movimiento corresponde a preparación o control de mezclas y líquidos. INT, FK a MedicalHistory.DetailPhysicalCUM.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'IdDetailPhysicalCUM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guardar la columna para central mezclas   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'IdDetailPhysicalCUM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'IdDetailPhysicalCUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observación adicional, nota o comentario del movimiento de kardex, medicamento, insumo o procedimiento realizado. VARCHAR(8000), campo libre de texto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'OBSERVACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observacion ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'OBSERVACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'OBSERVACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de utilización, consumo o aplicación efectiva del medicamento, insumo o producto al paciente. DATETIME, auditoría de dispensación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'FECHAUTIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Utilizacion del Insumo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'FECHAUTIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'FECHAUTIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo o número secuencial del registro en log de farmacia para trazabilidad y control de movimientos. INT, auditoría interna.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'CONSECFAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo Log farmacia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'CONSECFAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'CONSECFAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación, motivo o razón de la anulación del movimiento de kardex por farmacia. VARCHAR(2000), auditoría de cancelaciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'JUSANULAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena la Jutificacion de Anulación de Farmacia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'JUSANULAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'JUSANULAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual del movimiento de kardex: entrada, salida, mezcla, traslado, devolutivo, ajuste o anulación de medicamentos e insumos. VARCHAR(500), legibilidad del registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'DESMOVPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del Movimiento de Kardex', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'DESMOVPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'DESMOVPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Origen del registro (código numérico 0-23): entrada (despacho farmacia, préstamo, traslado), solicitud (orden médica, emergencia, insumos enfermería), anulación (farmacia, aplicación, pendiente), salida (aplicación, mezcla, gasto, procedimiento, devolución, ajuste). INT, clasificación de movimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'TIPORIREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Origen del Registro:  0. No Aplica  ''''Entradas  1. Despacho de Farmacia - Solicitud del Medico  2. Despacho de Farmacia - Solicitud de Enfermeria  3. Despacho de Farmacia - Solicitud de Emergencia  4. Prestamo de Medicamentos  5. Traslado de Medicamentos - Modulo de Gestion Hospitalaria  ''''Solicitudes  6. Ordenes Medicas  7. Emergencia  8. Solicitud de Insumos (Enfermeria)  ''''Anulaciones  9. Anulacion Farmacia  10. Descartar Aplicacion de Medicamento  23. Anulación solicitud pendiente al egresar  ''''Salidas  11. Aplicacion de Medicamento  12. Registro de Mezcla  13. Registro de Insumos (Gasto)  14. Procedimientos de Terapia  15. Venopuncion  16. Devolutivos Prestamos  17. Devolutivos Farmacia  18. Traslado de Medicamentos - Modulo Gestion Hospitalaria  19-Acepacticion de devolutivo Farmacia  20- Ajuste de inventario   21 - Ajuste de inventario : Entrada  22 - Ajuste de inventario : Salida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'TIPORIREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'TIPORIREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora exacta del registro, entrada o creación del movimiento en kardex de farmacia. DATETIME, NOT NULL, auditoría de timestamp.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'FECREGKAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Hora del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'FECREGKAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'FECREGKAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número o código del documento (solicitud de producto - SP) generado en la entrada o ingreso del medicamento/insumo. CHAR(10), referencia documental.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'CODDOCUME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Documento (SP) Generado en la Entrada del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'CODDOCUME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'CODDOCUME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del tipo de consecutivo 4 (HCLIQAPLC): referencia a hoja de control de aplicación de mezclas y líquidos por enfermería. NUMERIC(18), FK lógica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'HCCTRAPLM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Tipo de Consecutivo  4: HCLIQAPLC - Salida Registro y Control de Aplicacion Enfermeras de mezclas y liquidos- Consecutivo de la Hoja de Mezclas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'HCCTRAPLM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'HCCTRAPLM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del tipo de consecutivo 3 (HCCTRAPLN): referencia a hoja de control de aplicación de medicamentos por enfermería. CHAR(10), FK lógica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'HCCTRAPLN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Tipo de Consecutivo  3: HCCTRAPLM - Salida Registro y Control de Aplicacion Enfermeras de medicamentos - Consecutivo de la Hoja de Medicamentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'HCCTRAPLN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'HCCTRAPLN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del tipo de consecutivo 2 (HCSOLINSC): referencia a solicitud de insumos de enfermería. CHAR(10), FK lógica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'HCSOLINSN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Tipo de Consecutivo  2: HCSOLINSC - Ingreso Solicitud Insumos Enfermeras - Consecutivo de Insumos de Enfermeria  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'HCSOLINSN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'HCSOLINSN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del tipo de consecutivo 1 (HCPRESCRC): referencia a prescripción médica, consecutivo de receta del profesional. NUMERIC(18), FK lógica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'HCPRESCRN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Tipo de Consecutivo  1: HCPRESCRC - Ingreso Preescripcion Medica - Consecutivo de Prescripcion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'HCPRESCRN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'HCPRESCRN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de registro (1=Entrada, 2=Salida, 3=Movimiento salida no calidad, 4=Anulación, 5=Devolutivo). CHAR(1), clasificación de operación de kardex.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'TIPREGIST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Registro   1: Entrada  2: Salida  3: Movimiento de salida por costo de no calidad  4-Anulacion  5-Devolutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'TIPREGIST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'TIPREGIST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad o número de unidades del medicamento, insumo o producto movimentado en el kardex. INT, NOT NULL, cantidad dispensada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'CANPRODUCT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'CANPRODUCT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'CANPRODUCT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del medicamento, insumo o producto farmacéutico movimentado. CHAR(20), NOT NULL, identificador de bien.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de la salud (médico, enfermero, terapeuta) que prescribe, solicita o autoriza el movimiento. CHAR(20), Identification_Ofuscado (PII).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional (farmacia, piso, UCI, urgencia, quirófano) donde ocurre el movimiento o destino del medicamento. CHAR(10), ubicación del bien.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención, institución, sucursal o seccional hospitalaria donde se registra el movimiento. CHAR(10), ubicación organizacional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del ingreso, admisión o atención del paciente en la institución. CHAR(10), NOT NULL, referencia a episodio de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente, identificación, cédula o documento de identidad del paciente. VARCHAR(25), Identification_Ofuscado (PII), NOT NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número consecutivo automático único, generador de ID (00000011) del registro en tabla HCKARDPAC. VARCHAR(50), NOT NULL, PK clustered.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'NUMCONSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero consecutivo automatico de la tabla:  Id del Consecutivo 00000011', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'NUMCONSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'NUMCONSEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Kárdex de medicamentos y productos farmacéuticos por paciente. Registra cada movimiento de entrega, devolución o ajuste de productos dispensados durante un ingreso hospitalario, incluyendo cantidades, prescripciones asociadas, profesional responsable y justificaciones de anulación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único (GUID) que identifica la estación o punto de mezcla susceptible asociado al movimiento del producto, usado en procesos de preparación de mezclas farmacéuticas o nutrición parenteral.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'CodeSusceptibleMixingStation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'CodeSusceptibleMixingStation';
