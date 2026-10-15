CREATE TABLE [dbo].[ADAUTSERC] (
    [CODCONCEC] NUMERIC (18)                                                                     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NUMINFORM] CHAR (10)                                                                        NOT NULL,
    [FECINFORM] DATETIME                                                                         NOT NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [CODENTIDA] CHAR (9)                                                                         NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [CODORIATE] CHAR (2)                                                                         NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [NUMEFOLIO] CHAR (10)                                                                        NULL,
    [JUSCLISER] VARCHAR (MAX)                                                                    NOT NULL,
    [CODPRIATE] CHAR (1)                                                                         NOT NULL,
    [TIPSERSOL] CHAR (1)                                                                         NOT NULL,
    [UBICAPACI] CHAR (1)                                                                         NOT NULL,
    [UFUCODIGO] CHAR (10)                                                                        NOT NULL,
    [UFUDESCRI] CHAR (60)                                                                        NOT NULL,
    [NUMCAMHOS] CHAR (10)                                                                        NULL,
    [NOMGUIATE] CHAR (30)                                                                        NOT NULL,
    [CODUSUARI] CHAR (20)                                                                        NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [ESTTRANSA] CHAR (1)                                                                         NOT NULL,
    [DESFINENV] CHAR (1)                                                                         NOT NULL,
    [INDAUDFOR] NUMERIC (18)                                                                     NOT NULL,
    CONSTRAINT [PK_ADAUTSERC] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC),
    CONSTRAINT [FK_ADAUTSERC_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_ADAUTSERC_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_ADAUTSERC_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_ADAUTSERC_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_ADAUTSERC_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_ADAUTSERC_SEGusuaru] FOREIGN KEY ([CODUSUARI]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI])
);


GO
ALTER TABLE [dbo].[ADAUTSERC] NOCHECK CONSTRAINT [FK_ADAUTSERC_ADCENATEN];


GO
ALTER TABLE [dbo].[ADAUTSERC] NOCHECK CONSTRAINT [FK_ADAUTSERC_ADINGRESO];


GO
ALTER TABLE [dbo].[ADAUTSERC] NOCHECK CONSTRAINT [FK_ADAUTSERC_INPACIENT];


GO
ALTER TABLE [dbo].[ADAUTSERC] NOCHECK CONSTRAINT [FK_ADAUTSERC_INPROFSAL];


GO
ALTER TABLE [dbo].[ADAUTSERC] NOCHECK CONSTRAINT [FK_ADAUTSERC_INUNIFUNC];


GO
ALTER TABLE [dbo].[ADAUTSERC] NOCHECK CONSTRAINT [FK_ADAUTSERC_SEGusuaru];


GO
CREATE NONCLUSTERED INDEX [IX_ADAUTSERC_NUMINFORM]
    ON [dbo].[ADAUTSERC]([NUMINFORM] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ADAUTSERC_IPCODPACI_NUMINGRES]
    ON [dbo].[ADAUTSERC]([IPCODPACI] ASC, [NUMINGRES] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ADAUTSERC_FECINFORM]
    ON [dbo].[ADAUTSERC]([FECINFORM] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el destino de envio final:  1: a la EAPB  2: a la DTL  3: a la DTD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'DESFINENV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la Transaccion:  1: En Proceso de Envio  2: Envio 1 Fallido  3: Envio 2 Fallido  4: Envio 3 Fallido  5: Envio DTL Fallido  6: Envio DTD Fallido  7: Envio Satisfactorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'ESTTRANSA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Usuario que reporta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'CODUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Guia de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'NOMGUIATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Numero de la Cama', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'NUMCAMHOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de la Unidad Funcional - Extrae los 30 Caracteres Iniciales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'UFUDESCRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ubicacion del Paciente - Homologado con Tipo de Unidad Funcional en donde se realiza la solicitud  1: Consulta externa   2: Urgencias  3: Hospitalizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'UBICAPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Servicio de Solicitud  1: Posterior a la Atención Inicial de Urgencias  2: Servicios Electivos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'TIPSERSOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Prioridad de la Atencion  1: Prioritaria  2: No Prioritaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'CODPRIATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Justificacion Clinica para la solicitud del Servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'JUSCLISER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Folio de Historia Clinica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Ingreso    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Origen de la Atencion - Homologado al momento de crear el Ingreso.  01: Accidente de trabajo              02: Accidente de tránsito             06: Evento catastrófico           13: Enfermedad general                14: Enfermedad profesional             16: Accidente de trabajo y Accidente de Transito        17 = Evento catastrófico y Accidente de trabajo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'CODORIATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'CODENTIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la Solicitud de Servicios Adicionales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'FECINFORM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de la Autorizacion para Servicios Adicionales a la Atencion Inicial de Urgencias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'NUMINFORM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo Interno de Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSERC', @level2type = N'COLUMN', @level2name = N'CODCONCEC';

