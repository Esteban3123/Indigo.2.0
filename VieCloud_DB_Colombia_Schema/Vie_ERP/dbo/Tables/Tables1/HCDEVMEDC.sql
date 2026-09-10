CREATE TABLE [dbo].[HCDEVMEDC] (
    [CODCONCEC]       INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FECHDEVOL]       DATETIME                                                                         NOT NULL,
    [CODPROSAL]       CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [IPCODPACI]       VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]       CHAR (10)                                                                        NOT NULL,
    [CODCENATE]       CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]       CHAR (10)                                                                        NOT NULL,
    [DEVESTADO]       CHAR (1)                                                                         NOT NULL,
    [CODBODEGA]       VARCHAR (20)                                                                     NOT NULL,
    [CODCENCOS]       CHAR (14)                                                                        NOT NULL,
    [ORIDEVMED]       CHAR (1)                                                                         NULL,
    [FECREGISTR]      DATETIME                                                                         NOT NULL,
    [CODUSUARI]       CHAR (20)                                                                        NOT NULL,
    [IDHCHOJAGASTOQX] INT                                                                              NULL,
    CONSTRAINT [PK_HCDEVMEDC] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC),
    CONSTRAINT [FK_HCDEVMEDC_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCDEVMEDC_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCDEVMEDC_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCDEVMEDC_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCDEVMEDC_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCDEVMEDC].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCDEVMEDC].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
CREATE NONCLUSTERED INDEX [IDX_DevolutivosMedicamentos]
    ON [dbo].[HCDEVMEDC]([DEVESTADO] ASC, [CODCENATE] ASC)
    INCLUDE([CODBODEGA], [CODCENCOS], [CODCONCEC], [CODPROSAL], [CODUSUARI], [FECHDEVOL], [FECREGISTR], [IPCODPACI], [NUMINGRES], [ORIDEVMED], [UFUCODIGO]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la Hoja de Gasto Quirúrgico (HCHOJAGASTOQX) que origina el devolutivo de medicamentos e insumos. INT, nullable, referencia a documento de egreso qx.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDC', @level2type = N'COLUMN', @level2name = N'IDHCHOJAGASTOQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Hoja Gasto que genera el devolutivo (HCHOJAGASTOQX)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDC', @level2type = N'COLUMN', @level2name = N'IDHCHOJAGASTOQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDC', @level2type = N'COLUMN', @level2name = N'IDHCHOJAGASTOQX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Usuario que registra la devolución. CHAR(20), auditoría de quién realiza la transacción. Identificación PII.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDC', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDC', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDC', @level2type = N'COLUMN', @level2name = N'CODUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de registro del devolutivo en el sistema. DATETIME, marca temporal de creación del documento de devolución.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDC', @level2type = N'COLUMN', @level2name = N'FECREGISTR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDC', @level2type = N'COLUMN', @level2name = N'FECREGISTR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDC', @level2type = N'COLUMN', @level2name = N'FECREGISTR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Origen de la devolución de medicamentos e insumos: 1=Traslado entre servicios, 2=Egreso/Alta paciente, 3=Suspensión aplicaciones enfermería, 4=Opciones enfermería, 5=Paquete quirúrgico, 6=Productos adicionales solicitud, 7=Paquete enfermería. CHAR(1).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDC', @level2type = N'COLUMN', @level2name = N'ORIDEVMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Origen Devolucion Medicammentos  
1-Traslado   
2-Egreso  
3-Suspender Aplicaciones Enfermeria  
4- Opciones Enfermeria.  
5. Hoja Qx(Paquete Qx). 
6. Hoja Qx(Productos Adicionales - Solicitud de medicamentos e insumos) 
7. Hoja QX (Paquetes de enfermeria)
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDC', @level2type = N'COLUMN', @level2name = N'ORIDEVMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDC', @level2type = N'COLUMN', @level2name = N'ORIDEVMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Centro de Costos que absorbe el devolutivo. CHAR(14), para contabilización y análisis de gastos por unidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDC', @level2type = N'COLUMN', @level2name = N'CODCENCOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ceontro de Costos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDC', @level2type = N'COLUMN', @level2name = N'CODCENCOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDC', @level2type = N'COLUMN', @level2name = N'CODCENCOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la Bodega o almacén destino del medicamento/insumo devuelto. VARCHAR(20), ubicación física de recepción.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDC', @level2type = N'COLUMN', @level2name = N'CODBODEGA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Bodega', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDC', @level2type = N'COLUMN', @level2name = N'CODBODEGA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDC', @level2type = N'COLUMN', @level2name = N'CODBODEGA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del devolutivo: 1=Pendiente, 2=Entregada, 3=Anulada. CHAR(1), control de flujo de devoluciones en bodega.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDC', @level2type = N'COLUMN', @level2name = N'DEVESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la Devolucion:  1. Pendiente  2. Entregada  3. Anulada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDC', @level2type = N'COLUMN', @level2name = N'DEVESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDC', @level2type = N'COLUMN', @level2name = N'DEVESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la Unidad Funcional (servicio, piso, área) que devuelve el medicamento. CHAR(10), FK a INUNIFUNC, trazabilidad clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Centro de Atención donde se origina el devolutivo. CHAR(10), FK a ADCENATEN, ubicación de la atención sanitaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDC', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDC', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDC', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del Ingreso o atención hospitalaria asociada. CHAR(10), FK a ADINGRESO, vinculación con historia clínica del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Paciente (cédula, identificación, documento). VARCHAR(25) enmascarado PII, FK a INPACIENT, identificación única.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Profesional de la Salud responsable. CHAR(20) enmascarado PII, FK a INPROFSAL, trazabilidad del acto médico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de la devolución de medicamentos/insumos. DATETIME, diferente a fecha registro; puede ser posterior al devolutivo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDC', @level2type = N'COLUMN', @level2name = N'FECHDEVOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDC', @level2type = N'COLUMN', @level2name = N'FECHDEVOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDC', @level2type = N'COLUMN', @level2name = N'FECHDEVOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Consecutivo (PK). INT IDENTITY(1,1), número único secuencial del devolutivo en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDC', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDC', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDC', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Devoluciones de medicamentos realizadas durante una atención o ingreso hospitalario. Registra qué medicamentos fueron devueltos a bodega, por qué paciente, en qué ingreso, y el estado de cada devolución.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDC';
