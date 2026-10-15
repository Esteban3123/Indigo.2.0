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
ALTER TABLE [dbo].[HCDEVMEDC] NOCHECK CONSTRAINT [FK_HCDEVMEDC_ADCENATEN];


GO
ALTER TABLE [dbo].[HCDEVMEDC] NOCHECK CONSTRAINT [FK_HCDEVMEDC_ADINGRESO];


GO
ALTER TABLE [dbo].[HCDEVMEDC] NOCHECK CONSTRAINT [FK_HCDEVMEDC_INPACIENT];


GO
ALTER TABLE [dbo].[HCDEVMEDC] NOCHECK CONSTRAINT [FK_HCDEVMEDC_INPROFSAL];


GO
ALTER TABLE [dbo].[HCDEVMEDC] NOCHECK CONSTRAINT [FK_HCDEVMEDC_INUNIFUNC];


GO
CREATE NONCLUSTERED INDEX [IDX_DevolutivosMedicamentos]
    ON [dbo].[HCDEVMEDC]([DEVESTADO] ASC, [CODCENATE] ASC)
    INCLUDE([CODBODEGA], [CODCENCOS], [CODCONCEC], [CODPROSAL], [CODUSUARI], [FECHDEVOL], [FECREGISTR], [IPCODPACI], [NUMINGRES], [ORIDEVMED], [UFUCODIGO]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la Hoja Gasto que genera el devolutivo (HCHOJAGASTOQX)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDC', @level2type = N'COLUMN', @level2name = N'IDHCHOJAGASTOQX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDC', @level2type = N'COLUMN', @level2name = N'CODUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDC', @level2type = N'COLUMN', @level2name = N'FECREGISTR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Origen Devolucion Medicammentos  
1-Traslado   
2-Egreso  
3-Suspender Aplicaciones Enfermeria  
4- Opciones Enfermeria.  
5. Hoja Qx(Paquete Qx). 
6. Hoja Qx(Productos Adicionales - Solicitud de medicamentos e insumos) 
7. Hoja QX (Paquetes de enfermeria)
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDC', @level2type = N'COLUMN', @level2name = N'ORIDEVMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ceontro de Costos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDC', @level2type = N'COLUMN', @level2name = N'CODCENCOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Bodega', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDC', @level2type = N'COLUMN', @level2name = N'CODBODEGA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la Devolucion:  1. Pendiente  2. Entregada  3. Anulada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDC', @level2type = N'COLUMN', @level2name = N'DEVESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDC', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDC', @level2type = N'COLUMN', @level2name = N'FECHDEVOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDC', @level2type = N'COLUMN', @level2name = N'CODCONCEC';

