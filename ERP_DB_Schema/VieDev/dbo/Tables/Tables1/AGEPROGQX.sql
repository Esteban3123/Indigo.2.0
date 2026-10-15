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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla cabecera de las autorizaciones de vie erp    Tabla  [Authorization].TraceabilityPaperwork      Este campo se diligencia siempre y cuando se seleccione una Autorizaciones de VIE ERP al asignar la cita medica y por medio de este campo actualizamos la cabecera de VIE cuando se cancele la cita.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Local  2 - Regional  3 - general  4 - Combinada  5 - No aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'TIPOANESTESIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado Solicitud de confirmacion del paquete quirurgico desde GX :   1. Pendiente 2. Solicitado a Farmacia  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'ESTADOFARM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Cirugía de origen Ambulatorias  2 - Cirugía de origen Hospitalaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'ORIGENQX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo estado del procedimiento  0. Cirugia Programada  1. Paciente admitido (Cirugia Origen Ambulatoria)  2. Paciente en sala de espera  3. Paciente en sala quirurgica  4. Paciente en recuperación  5.  Paciente con alta  6. Anulado - Cancelada  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'CODESTPQX';

