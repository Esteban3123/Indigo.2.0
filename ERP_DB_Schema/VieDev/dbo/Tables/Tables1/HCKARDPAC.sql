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
ALTER TABLE [dbo].[HCKARDPAC] NOCHECK CONSTRAINT [FK__HCKARDPAC__IdDet__3FC1C448];


GO
ALTER TABLE [dbo].[HCKARDPAC] NOCHECK CONSTRAINT [FK_HCKARDPAC_HCKARDPAC];


GO
CREATE NONCLUSTERED INDEX [IX_HCKARDPAC_CODPRODUC_CANPRODUCT_HCCTRAPLN_OBSERVACI]
    ON [dbo].[HCKARDPAC]([CODPRODUC] ASC)
    INCLUDE([CANPRODUCT], [HCCTRAPLN], [OBSERVACI]);


GO
CREATE NONCLUSTERED INDEX [IDX_HCKARDPAC_NUMINGRES]
    ON [dbo].[HCKARDPAC]([NUMINGRES] ASC)
    INCLUDE([IPCODPACI], [UFUCODIGO], [CODPRODUC], [CANPRODUCT], [TIPREGIST], [FECREGKAR], [TIPORIREG], [DESMOVPRO], [CONSECFAR]);


GO
CREATE NONCLUSTERED INDEX [IX_HCKARDPAC_NUMINGRES_CODCENATE_IPCODPACI_UFUCODIGO_TIPORIREG_CANPRODUCT_CODPRODUC_DESMOVPRO_FECREGKAR_JUSANULAC_TIPREGIST]
    ON [dbo].[HCKARDPAC]([NUMINGRES] ASC, [CODCENATE] ASC, [IPCODPACI] ASC, [UFUCODIGO] ASC, [TIPORIREG] ASC)
    INCLUDE([CANPRODUCT], [CODPRODUC], [DESMOVPRO], [FECREGKAR], [JUSANULAC], [TIPREGIST]);


GO
CREATE NONCLUSTERED INDEX [IDX_HCKARDPAC_UFUCODIGO_NUMINGRES_TIPORIREG]
    ON [dbo].[HCKARDPAC]([UFUCODIGO] ASC, [NUMINGRES] ASC, [TIPORIREG] ASC)
    INCLUDE([IPCODPACI], [CODPRODUC], [CANPRODUCT], [TIPREGIST], [FECREGKAR], [DESMOVPRO], [CONSECFAR]);


GO
CREATE NONCLUSTERED INDEX [IX_HCKARDPAC_IPCODPACI_NUMINGRES_FECREGKAR_TIPORIREG]
    ON [dbo].[HCKARDPAC]([IPCODPACI] ASC, [NUMINGRES] ASC, [FECREGKAR] ASC, [TIPORIREG] ASC)
    INCLUDE([CANPRODUCT], [CODPRODUC], [CODPROSAL], [DESMOVPRO], [UFUCODIGO]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda aqui el ID de la Tabla Origen:    Ejemplos:    1ero: Si el medicamento ES guardado desde una HC el id origen seria el  de la tabla HCPRASCRA, si el medicamento viene de orden de  una mezcla el id origen que queda aqui seria el id origen de la tabla de la mezcla.    2. Cuando se haga una reposicion desde solicitud de medicamento e insumos y el medicamento que va a guardar NO cuenta con una prescripcion medica va el el ID de la tabla de la solicitud de medicamentos e insumos, PERO SI el medicamento SI cuenta con una prescrpcion medica va el ID de la tabla Origen es decir HCPRESCRA ó mezclas etc.        ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'IdSourceTable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda aqui el Nombre de la Tabla Origen:    Ejemplos:    1ero: Si el medicamento ES guardado desde una HC la tabla Origen seria HCPRASCRA si el medicamento viene de orden de  una mezcla seria el nombre que queda aqui seria el nombre de la tabla de la mezcla.    2. Cuando se haga una reposicion desde solicitud de medicamento e insumos y el medicamento que va a guardar NO cuenta con una prescripcion medica va el Nombre de la tabla de la solicitud de medicamentos e insumos, PERO SI el medicamento SI cuenta con una prescrpcion medica va el Nombre de la tabla Origen es decir HCPRESCRA.            ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'SourceTable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Guardar la columna para central mezclas   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'IdDetailPhysicalCUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Origen del Registro:  0. No Aplica  ''Entradas  1. Despacho de Farmacia - Solicitud del Medico  2. Despacho de Farmacia - Solicitud de Enfermeria  3. Despacho de Farmacia - Solicitud de Emergencia  4. Prestamo de Medicamentos  5. Traslado de Medicamentos - Modulo de Gestion Hospitalaria  ''Solicitudes  6. Ordenes Medicas  7. Emergencia  8. Solicitud de Insumos (Enfermeria)  ''Anulaciones  9. Anulacion Farmacia  10. Descartar Aplicacion de Medicamento  23. Anulación solicitud pendiente al egresar  ''Salidas  11. Aplicacion de Medicamento  12. Registro de Mezcla  13. Registro de Insumos (Gasto)  14. Procedimientos de Terapia  15. Venopuncion  16. Devolutivos Prestamos  17. Devolutivos Farmacia  18. Traslado de Medicamentos - Modulo Gestion Hospitalaria  19-Acepacticion de devolutivo Farmacia  20- Ajuste de inventario   21 - Ajuste de inventario : Entrada  22 - Ajuste de inventario : Salida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'TIPORIREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Tipo de Consecutivo  4: HCLIQAPLC - Salida Registro y Control de Aplicacion Enfermeras de mezclas y liquidos- Consecutivo de la Hoja de Mezclas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'HCCTRAPLM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Tipo de Consecutivo  3: HCCTRAPLM - Salida Registro y Control de Aplicacion Enfermeras de medicamentos - Consecutivo de la Hoja de Medicamentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'HCCTRAPLN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Tipo de Consecutivo  2: HCSOLINSC - Ingreso Solicitud Insumos Enfermeras - Consecutivo de Insumos de Enfermeria  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'HCSOLINSN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Tipo de Consecutivo  1: HCPRESCRC - Ingreso Preescripcion Medica - Consecutivo de Prescripcion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'HCPRESCRN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Registro   1: Entrada  2: Salida  3: Movimiento de salida por costo de no calidad  4-Anulacion  5-Devolutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'TIPREGIST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero consecutivo automatico de la tabla:  Id del Consecutivo 00000011', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCKARDPAC', @level2type = N'COLUMN', @level2name = N'NUMCONSEC';

