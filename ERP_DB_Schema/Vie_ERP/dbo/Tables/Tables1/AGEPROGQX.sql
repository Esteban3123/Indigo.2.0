CREATE TABLE [dbo].[AGEPROGQX] (
    [CODAUTONU]                INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCENATE]                CHAR (10)                                                                        NOT NULL,
    [IPCODPACI]                VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [CODSERIPS]                CHAR (20)                                                                        NOT NULL,
    [CODPROSAL]                CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [DURPROCQX]                INT                                                                              NOT NULL,
    [FECHORAIN]                DATETIME                                                                         NOT NULL,
    [FECHORAFI]                DATETIME                                                                         NOT NULL,
    [CODESTPQX]                CHAR (1)                                                                         NOT NULL,
    [CODUSUASI]                CHAR (20)                                                                        NOT NULL,
    [FECREGSIS]                DATETIME                                                                         NOT NULL,
    [CODESPECI]                CHAR (3)                                                                         NULL,
    [AUTOHCORDPROQ]            INT                                                                              NULL,
    [PRINCIPAL]                BIT                                                                              NULL,
    [IDPADRE]                  INT                                                                              NULL,
    [IDAGENDA]                 INT                                                                              NULL,
    [CODCAUCAN]                CHAR (3)                                                                         NULL,
    [CODUSUCAN]                CHAR (20)                                                                        NULL,
    [FECHACAN]                 DATETIME                                                                         NULL,
    [OBSERCAN]                 VARCHAR (500)                                                                    NULL,
    [IDRADICACIONQX]           INT                                                                              NULL,
    [ORIGENQX]                 INT                                                                              NULL,
    [ESTADOFARM]               INT                                                                              CONSTRAINT [DF_AGEPROGQX_ESTADOFARMACIA] DEFAULT ((1)) NOT NULL,
    [NUMINGRES]                CHAR (10)                                                                        NULL,
    [AGENSALAC]                INT                                                                              NOT NULL,
    [FECHAINA]                 DATETIME                                                                         NULL,
    [CODCAUINA]                CHAR (4)                                                                         NULL,
    [OBSCAUINA]                VARCHAR (200)                                                                    NULL,
    [CODUSUINA]                CHAR (20)                                                                        NULL,
    [DILIGENCIOHOJAGASTO]      BIT                                                                              NULL,
    [OBSERVACION]              VARCHAR (MAX)                                                                    NULL,
    [OTROSMATERIALES]          VARCHAR (MAX)                                                                    NULL,
    [OTROSAYUDANTES]           VARCHAR (MAX)                                                                    NULL,
    [USUARIOMOD]               CHAR (20)                                                                        NULL,
    [FECHAMOD]                 DATETIME                                                                         NULL,
    [MOTREPROGRAMO]            VARCHAR (MAX)                                                                    NULL,
    [TIPOANESTESIA]            TINYINT                                                                          NULL,
    [CONFASIST]                CHAR (1)                                                                         CONSTRAINT [DF_AGEPROGQX_CONFASIST] DEFAULT ((3)) NULL,
    [NUMAUTORI]                VARCHAR (30)                                                                     NULL,
    [TIPSERIPS]                INT                                                                              NULL,
    [DESMOTANU]                VARCHAR (500)                                                                    NULL,
    [IDDESCRIPCIONRELACIONADA] INT                                                                              NULL,
    [AUTOHCORDPRON]            INT                                                                              NULL,
    [TraceabilityPaperworkid]  INT                                                                              NULL,
    [DiagnosisCode]            CHAR (4)                                                                         NULL,
    CONSTRAINT [PK_AGEPROGQX] PRIMARY KEY CLUSTERED ([CODAUTONU] ASC),
    CONSTRAINT [FK_AGEPROGQX_ADCENATEN1] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_AGEPROGQX_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_AGEPROGQX_AGENSALAC1] FOREIGN KEY ([AGENSALAC]) REFERENCES [dbo].[AGENSALAC] ([CODCONCEC]),
    CONSTRAINT [FK_AGEPROGQX_HCORDPRON] FOREIGN KEY ([AUTOHCORDPRON]) REFERENCES [dbo].[HCORDPRON] ([AUTO]),
    CONSTRAINT [FK_AGEPROGQX_HCORDPROQ] FOREIGN KEY ([AUTOHCORDPROQ]) REFERENCES [dbo].[HCORDPROQ] ([AUTO]),
    CONSTRAINT [FK_AGEPROGQX_INCUPSIPS1] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_AGEPROGQX_INDIAGNOS] FOREIGN KEY ([DiagnosisCode]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_AGEPROGQX_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_AGEPROGQX_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_AGEPROGQX_INPROFSAL1] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_AGEPROGQX_Tickets] FOREIGN KEY ([AUTOHCORDPROQ]) REFERENCES [dbo].[HCORDPROQ] ([AUTO]),
    CONSTRAINT [FK_HCORDPRON_Tickets] FOREIGN KEY ([AUTOHCORDPRON]) REFERENCES [dbo].[HCORDPRON] ([AUTO])
);


GO
ALTER TABLE [dbo].[AGEPROGQX] NOCHECK CONSTRAINT [FK_AGEPROGQX_ADCENATEN1];


GO
ALTER TABLE [dbo].[AGEPROGQX] NOCHECK CONSTRAINT [FK_AGEPROGQX_ADINGRESO];


GO
ALTER TABLE [dbo].[AGEPROGQX] NOCHECK CONSTRAINT [FK_AGEPROGQX_AGENSALAC1];


GO
ALTER TABLE [dbo].[AGEPROGQX] NOCHECK CONSTRAINT [FK_AGEPROGQX_HCORDPRON];


GO
ALTER TABLE [dbo].[AGEPROGQX] NOCHECK CONSTRAINT [FK_AGEPROGQX_HCORDPROQ];


GO
ALTER TABLE [dbo].[AGEPROGQX] NOCHECK CONSTRAINT [FK_AGEPROGQX_INCUPSIPS1];


GO
ALTER TABLE [dbo].[AGEPROGQX] NOCHECK CONSTRAINT [FK_AGEPROGQX_INDIAGNOS];


GO
ALTER TABLE [dbo].[AGEPROGQX] NOCHECK CONSTRAINT [FK_AGEPROGQX_INESPECIA];


GO
ALTER TABLE [dbo].[AGEPROGQX] NOCHECK CONSTRAINT [FK_AGEPROGQX_INPACIENT];


GO
ALTER TABLE [dbo].[AGEPROGQX] NOCHECK CONSTRAINT [FK_AGEPROGQX_INPROFSAL1];


GO
ALTER TABLE [dbo].[AGEPROGQX] NOCHECK CONSTRAINT [FK_AGEPROGQX_Tickets];


GO
ALTER TABLE [dbo].[AGEPROGQX] NOCHECK CONSTRAINT [FK_HCORDPRON_Tickets];


GO
CREATE NONCLUSTERED INDEX [IDX_AGEPROGQX_CODCENATE_IPCODPACI_ORIGENQX_ESTADOFARM_FECHORAIN]
    ON [dbo].[AGEPROGQX]([CODCENATE] ASC, [IPCODPACI] ASC, [ORIGENQX] ASC, [ESTADOFARM] ASC, [FECHORAIN] ASC)
    INCLUDE([CODSERIPS], [CODPROSAL], [AGENSALAC], [CODESTPQX], [CODESPECI], [PRINCIPAL], [NUMINGRES]);


GO
CREATE NONCLUSTERED INDEX [IX_AGEPROGQX_IDPADRE]
    ON [dbo].[AGEPROGQX]([IDPADRE] ASC);


GO


CREATE TRIGGER [dbo].[IdAgenda_NO_NULL] 
   ON  [dbo].[AGEPROGQX]
   AFTER  INSERT,UPDATE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   if exists(select 1 from inserted where IDAGENDA is null )
   begin
	RAISERROR (15600, -1, -1, 'Error desde trigger en la tabla [AGEPROGQX], el id agenda NO puede ser null, contactar al administrador del sistema');
	ROLLBACK TRANSACTION; 
	return 
   end

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Diagnostico que se Postula en el dasboard de procedmientos Invasivos al momento de programar una cirugia. ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'DiagnosisCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla cabecera de las autorizaciones de vie erp    Tabla  [Authorization].TraceabilityPaperwork      Este campo se diligencia siempre y cuando se seleccione una Autorizaciones de VIE ERP al asignar la cita medica y por medio de este campo actualizamos la cabecera de VIE cuando se cancele la cita.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico del registro de la tabla de HCORDPRON', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'AUTOHCORDPRON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Justificacion ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'DESMOTANU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '13: Permitir Registrar mismo Nùmero de Autorizaciòn ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'TIPSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Guarda Numero de Autorizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'NUMAUTORI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de Confirmacion de Asistencia --> 1= Confirmado , 2 =Cancelado , 3= Sin Definir', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'CONFASIST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Local  2 - Regional  3 - general  4 - Combinada  5 - No aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'TIPOANESTESIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Motivo de reprogramación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'MOTREPROGRAMO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Modificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'FECHAMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de modificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'USUARIOMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Otros ayudante de cirugia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'OTROSAYUDANTES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Otros Materiales de cirugia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'OTROSMATERIALES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observacion de la cirugia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'OBSERVACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si ya se empezo a diligenciar hoja de gasto QX', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'DILIGENCIOHOJAGASTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'codigo de usuario quien registro casusas inatencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'CODUSUINA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'observaciones del causas inatencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'OBSCAUINA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de Causa Inatencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'CODCAUINA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha que registro Causas Inatención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'FECHAINA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'codigo consecutivo de la sala', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'AGENSALAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ingreso con el que se creo la solicitud del paquete QX - para cirugias de origen Ambulatorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado Solicitud de confirmacion del paquete quirurgico desde GX :   1. Pendiente 2. Solicitado a Farmacia  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'ESTADOFARM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Cirugía de origen Ambulatorias  2 - Cirugía de origen Hospitalaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'ORIGENQX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la radicacion desde la tabla ADRADICACIONQX', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'IDRADICACIONQX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Comentario de cancelacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'OBSERCAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha cancelacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'FECHACAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario cancelacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'CODUSUCAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo cancelacion, proviene de tabla AGCACANQX]', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'CODCAUCAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la agenda medica (AGAGEMEDC)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'IDAGENDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del procedimiento principal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'IDPADRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Si es la cirugía principal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'PRINCIPAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico del registro de la tabla de HCORDPROQ.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'AUTOHCORDPROQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'codigo de la Especialidad con la que fue creada la  agenda del medico se saca de la tabla AGAGEMEDC (Tabla que Contiene la Agenda Medica)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Guarda la Fecha y hora en que se registró la CIRUGIA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'FECREGSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Usuario que Asigno la CIRUGIA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'CODUSUASI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo estado del procedimiento  0. Cirugia Programada  1. Paciente admitido (Cirugia Origen Ambulatoria)  2. Paciente en sala de espera  3. Paciente en sala quirurgica  4. Paciente en recuperación  5.  Paciente con alta  6. Anulado - Cancelada  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'CODESTPQX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha y hora final de la C?irugia programada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'FECHORAFI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha y hora inicial de la cirugia programada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'FECHORAIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Duracion del procedimiento qx', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'DURPROCQX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Unico de Procedimientos y Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'CODAUTONU';

