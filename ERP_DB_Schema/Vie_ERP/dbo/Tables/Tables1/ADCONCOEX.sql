CREATE TABLE [dbo].[ADCONCOEX] (
    [CODCONCEC]                NUMERIC (18)                                                                     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]                VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [IPFECHACO]                DATETIME                                                                         NOT NULL,
    [IPFECHCIT]                DATETIME                                                                         NOT NULL,
    [CONESTADO]                INT                                                                              NOT NULL,
    [IPNOMCOMP]                CHAR (250) MASKED WITH (FUNCTION = 'partial(0, "Name_Ofuscado", 0)')             NOT NULL,
    [CODENTIDA]                CHAR (9)                                                                         NULL,
    [CODCENATE]                CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]                CHAR (10)                                                                        NOT NULL,
    [CODTIPCON]                INT                                                                              NULL,
    [CODPROSAL]                CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NULL,
    [NUMINGRES]                CHAR (10)                                                                        NOT NULL,
    [NUMCONCIT]                CHAR (20)                                                                        NULL,
    [PRIMERLLA]                BIT                                                                              NULL,
    [SEGUNDLLA]                BIT                                                                              NULL,
    [TERCERLLA]                BIT                                                                              NULL,
    [FECPRILLA]                DATETIME                                                                         NULL,
    [FECSEGLLA]                DATETIME                                                                         NULL,
    [FECTERLLA]                DATETIME                                                                         NULL,
    [OBVAUSENT]                VARCHAR (300)                                                                    NULL,
    [PROAUSENT]                CHAR (20)                                                                        NULL,
    [PROPRILLA]                CHAR (20)                                                                        NULL,
    [PROSEGLLA]                CHAR (20)                                                                        NULL,
    [PROTERLLA]                CHAR (20)                                                                        NULL,
    [FECAUSENT]                DATETIME                                                                         NULL,
    [INDAUDFOR]                NUMERIC (18)                                                                     NOT NULL,
    [AUTESTADO]                INT                                                                              NULL,
    [CODESPECI]                CHAR (3)                                                                         NULL,
    [LIQUIDAR]                 BIT                                                                              NOT NULL,
    [CODSERIPS]                CHAR (20)                                                                        NULL,
    [GENCAREGROUP]             INT                                                                              NULL,
    [GENCONENTITY]             INT                                                                              NULL,
    [GENINVOICE]               VARCHAR (20)                                                                     NULL,
    [GENINVOICEID]             INT                                                                              NULL,
    [IDDESCRIPCIONRELACIONADA] INT                                                                              NULL,
    [FECREGSIS]                DATETIME                                                                         NULL,
    [IDHCHISPACA]              INT                                                                              NULL,
    [IDHCCTRNOTE]              INT                                                                              NULL,
    [IDHCCTRNOTT]              INT                                                                              NULL,
    [FECREGISTROSALA]          DATETIME                                                                         NULL,
    [FECENCONSULTORIO]         DATETIME                                                                         NULL,
    [FECCITACUMPLIDA]          DATETIME                                                                         NULL,
    [IdCitaSync]               VARCHAR (25)                                                                     NULL,
    [CodigoConsultorio]        VARCHAR (100)                                                                    NULL,
    CONSTRAINT [PK_HCCONCOEX] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC),
    CONSTRAINT [FK_ADCONCOEX_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_ADCONCOEX_HCCTRNOTE] FOREIGN KEY ([IDHCCTRNOTE]) REFERENCES [dbo].[HCCTRNOTE] ([ID]),
    CONSTRAINT [FK_ADCONCOEX_HCCTRNOTT] FOREIGN KEY ([IDHCCTRNOTT]) REFERENCES [dbo].[HCCTRNOTT] ([ID]),
    CONSTRAINT [FK_ADCONCOEX_HCHISPACA] FOREIGN KEY ([IDHCHISPACA]) REFERENCES [dbo].[HCHISPACA] ([ID]),
    CONSTRAINT [FK_ADCONCOEX_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[ADCONCOEX] NOCHECK CONSTRAINT [FK_ADCONCOEX_ADINGRESO];


GO
ALTER TABLE [dbo].[ADCONCOEX] NOCHECK CONSTRAINT [FK_ADCONCOEX_HCCTRNOTE];


GO
ALTER TABLE [dbo].[ADCONCOEX] NOCHECK CONSTRAINT [FK_ADCONCOEX_HCCTRNOTT];


GO
ALTER TABLE [dbo].[ADCONCOEX] NOCHECK CONSTRAINT [FK_ADCONCOEX_HCHISPACA];


GO
ALTER TABLE [dbo].[ADCONCOEX] NOCHECK CONSTRAINT [FK_ADCONCOEX_INPACIENT];


GO
CREATE NONCLUSTERED INDEX [NUMINGRES]
    ON [dbo].[ADCONCOEX]([NUMINGRES] ASC);


GO
CREATE NONCLUSTERED INDEX [IDX_ControlCE]
    ON [dbo].[ADCONCOEX]([CONESTADO] ASC, [CODCENATE] ASC, [CODPROSAL] ASC, [IPFECHCIT] ASC)
    INCLUDE([CODCONCEC], [CODENTIDA], [CODESPECI], [CODTIPCON], [IPCODPACI], [IPFECHACO], [NUMCONCIT], [NUMINGRES]);


GO
CREATE NONCLUSTERED INDEX [IX_ADCONCOEX]
    ON [dbo].[ADCONCOEX]([IPCODPACI] ASC, [NUMINGRES] ASC, [CONESTADO] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ADCONCOEX_CODCENATE_CODPROSAL_CONESTADO_CODENTIDA_IPCODPACI_IPFECHACO_NUMCONCIT_NUMINGRES_PRIMERLLA_SEGUNDLLA_TERCERLLA]
    ON [dbo].[ADCONCOEX]([CODCENATE] ASC, [CODPROSAL] ASC, [CONESTADO] ASC)
    INCLUDE([CODENTIDA], [IPCODPACI], [IPFECHACO], [NUMCONCIT], [NUMINGRES], [PRIMERLLA], [SEGUNDLLA], [TERCERLLA]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el código del consultorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONCOEX', @level2type = N'COLUMN', @level2name = N'CodigoConsultorio';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el id de la sincronización de la cita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONCOEX', @level2type = N'COLUMN', @level2name = N'IdCitaSync';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'llega a éste estado en el momento que el profesional finaliza la historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONCOEX', @level2type = N'COLUMN', @level2name = N'FECCITACUMPLIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Llega a éste estado en el momento que el profesional desde el dashboard médico/especialistas/académicos/servicios de apoyo. Inicia a atención del paciente (ingresa a alguna HC del botón registrar dashboard paciente)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONCOEX', @level2type = N'COLUMN', @level2name = N'FECENCONSULTORIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Se guarda la fecha y hora en la que el paciente registra asistencia en sala', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONCOEX', @level2type = N'COLUMN', @level2name = N'FECREGISTROSALA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la tabla HCCTRNOTT (Nota de Terapia)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONCOEX', @level2type = N'COLUMN', @level2name = N'IDHCCTRNOTT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la tabla HCCTRNOTE (Nota de Enfermería)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONCOEX', @level2type = N'COLUMN', @level2name = N'IDHCCTRNOTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la tabla HCHISPACA  (Historia Clínica)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONCOEX', @level2type = N'COLUMN', @level2name = N'IDHCHISPACA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha y Hora en la que se guardó el Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONCOEX', @level2type = N'COLUMN', @level2name = N'FECREGSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONCOEX', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la factura (Tabla vie)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONCOEX', @level2type = N'COLUMN', @level2name = N'GENINVOICEID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Factura (Tabla vie)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONCOEX', @level2type = N'COLUMN', @level2name = N'GENINVOICE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Entidad (Tabla vie)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONCOEX', @level2type = N'COLUMN', @level2name = N'GENCONENTITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Grupo de atencion (Tabla vie)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONCOEX', @level2type = N'COLUMN', @level2name = N'GENCAREGROUP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de Servicio IPS de la Cita Medica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONCOEX', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Establece si realiza interfaz con dgh para generar orden de servicio, o si no relaciona a un ingreso facturado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONCOEX', @level2type = N'COLUMN', @level2name = N'LIQUIDAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'código especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONCOEX', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auto estado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONCOEX', @level2type = N'COLUMN', @level2name = N'AUTESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Control Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONCOEX', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha que se ausenta el paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONCOEX', @level2type = N'COLUMN', @level2name = N'FECAUSENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Profesional que realiza el tercer llamado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONCOEX', @level2type = N'COLUMN', @level2name = N'PROTERLLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Profesional que realiza el segundo llamado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONCOEX', @level2type = N'COLUMN', @level2name = N'PROSEGLLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Profesional que realiza el primer llamado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONCOEX', @level2type = N'COLUMN', @level2name = N'PROPRILLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Profesional que marca al paciente como ausente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONCOEX', @level2type = N'COLUMN', @level2name = N'PROAUSENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observacion cuando el paciente se ausenta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONCOEX', @level2type = N'COLUMN', @level2name = N'OBVAUSENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha y hora del tercer llamado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONCOEX', @level2type = N'COLUMN', @level2name = N'FECTERLLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha y hora del segundo llamado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONCOEX', @level2type = N'COLUMN', @level2name = N'FECSEGLLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha y hora del primer llamado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONCOEX', @level2type = N'COLUMN', @level2name = N'FECPRILLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tercer Llamado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONCOEX', @level2type = N'COLUMN', @level2name = N'TERCERLLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Segundo Llamado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONCOEX', @level2type = N'COLUMN', @level2name = N'SEGUNDLLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Primer Llamado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONCOEX', @level2type = N'COLUMN', @level2name = N'PRIMERLLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero Consecutivo Cita en Interfaz', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONCOEX', @level2type = N'COLUMN', @level2name = N'NUMCONCIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Ingreso Generado al Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONCOEX', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud que prescribe', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONCOEX', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1->primera vez  2->control  3->pos operatorio  4->no aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONCOEX', @level2type = N'COLUMN', @level2name = N'CODTIPCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONCOEX', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONCOEX', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Entidad a la que pertenece el paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONCOEX', @level2type = N'COLUMN', @level2name = N'CODENTIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre Completo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONCOEX', @level2type = N'COLUMN', @level2name = N'IPNOMCOMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del Control de Consultas: 
1: Sin Atender  
2: Ausente/Anulada   
3: Atendido   
4: En Sala    
5: En Consultorio 
6: Atendido pero paciente continua en la unidad (nuevo estado 19-05-2023) se creo mas que todo para salud visual, cambio que pidio andres cabrera.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONCOEX', @level2type = N'COLUMN', @level2name = N'CONESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha en cual fue agendada la cita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONCOEX', @level2type = N'COLUMN', @level2name = N'IPFECHCIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la Consulta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONCOEX', @level2type = N'COLUMN', @level2name = N'IPFECHACO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONCOEX', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Consecutivo Interno (Autonumerico)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONCOEX', @level2type = N'COLUMN', @level2name = N'CODCONCEC';

