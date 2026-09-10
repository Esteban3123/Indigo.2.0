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
ALTER TABLE [dbo].[AGEPROGQX] NOCHECK CONSTRAINT [FK_AGEPROGQX_HCORDPRON];


GO
ALTER TABLE [dbo].[AGEPROGQX] NOCHECK CONSTRAINT [FK_AGEPROGQX_HCORDPROQ];


GO
ALTER TABLE [dbo].[AGEPROGQX] NOCHECK CONSTRAINT [FK_AGEPROGQX_INPROFSAL1];


GO
ALTER TABLE [dbo].[AGEPROGQX] NOCHECK CONSTRAINT [FK_AGEPROGQX_Tickets];


GO
ALTER TABLE [dbo].[AGEPROGQX] NOCHECK CONSTRAINT [FK_HCORDPRON_Tickets];


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[AGEPROGQX].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[AGEPROGQX].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');




GO
ALTER TABLE [dbo].[AGEPROGQX] NOCHECK CONSTRAINT [FK_AGEPROGQX_HCORDPRON];


GO
ALTER TABLE [dbo].[AGEPROGQX] NOCHECK CONSTRAINT [FK_AGEPROGQX_HCORDPROQ];


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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del diagnóstico postulado en el dashboard de procedimientos invasivos al momento de programar una cirugía. Referencia a tabla INDIAGNOS para clasificación clínica (CHAR 4, FK).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'DiagnosisCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Diagnostico que se Postula en el dasboard de procedmientos Invasivos al momento de programar una cirugia. ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'DiagnosisCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'DiagnosisCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de rastreabilidad de la cabecera de autorizaciones VIE ERP (Authorization.TraceabilityPaperwork). Se diligencia al seleccionar una autorización al asignar cita médica y actualiza la cabecera cuando se cancela la cita (INT, FK).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkid';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla cabecera de las autorizaciones de vie erp    Tabla  [Authorization].TraceabilityPaperwork      Este campo se diligencia siempre y cuando se seleccione una Autorizaciones de VIE ERP al asignar la cita medica y por medio de este campo actualizamos la cabecera de VIE cuando se cancele la cita.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkid';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Autonúmerico del registro de observaciones post-operatorias de la tabla HCORDPRON. Vinculación con historia clínica de enfermería quirúrgica (INT, FK).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'AUTOHCORDPRON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico del registro de la tabla de HCORDPRON', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'AUTOHCORDPRON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'AUTOHCORDPRON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de descripción de relación con VIE ERP (contract.CUPSEntityContractDescriptions). Vinculación de CUPS con contrato (INT, FK).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación o motivo de la anulación de la cirugía programada (VARCHAR 500).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'DESMOTANU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justificacion ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'DESMOTANU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'DESMOTANU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de servicio RIPS. Valor 13: permite registrar mismo número de autorización para múltiples servicios (INT).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'TIPSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'13: Permitir Registrar mismo Nùmero de Autorizaciòn ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'TIPSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'TIPSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de autorización de la cirugía. Referencia para facturación y trámite de RIPS (VARCHAR 30).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'NUMAUTORI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Numero de Autorizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'NUMAUTORI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'NUMAUTORI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de confirmación de asistencia del paciente: 1=Confirmado, 2=Cancelado, 3=Sin definir (CHAR 1, default=3).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'CONFASIST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de Confirmacion de Asistencia --> 1= Confirmado , 2 =Cancelado , 3= Sin Definir', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'CONFASIST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'CONFASIST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de anestesia utilizada: 1=Local, 2=Regional, 3=General, 4=Combinada, 5=No aplica (TINYINT).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'TIPOANESTESIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Local  2 - Regional  3 - general  4 - Combinada  5 - No aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'TIPOANESTESIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'TIPOANESTESIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo de reprogramación de la cirugía. Razón por la cual se cambió la fecha u hora programada (VARCHAR MAX).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'MOTREPROGRAMO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivo de reprogramación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'MOTREPROGRAMO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'MOTREPROGRAMO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación del registro de programación quirúrgica (DATETIME).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'FECHAMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Modificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'FECHAMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'FECHAMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario que realizó la última modificación del registro (CHAR 20).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'USUARIOMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de modificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'USUARIOMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'USUARIOMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de otros ayudantes de cirugía adicionales al equipo base quirúrgico (VARCHAR MAX).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'OTROSAYUDANTES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Otros ayudante de cirugia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'OTROSAYUDANTES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'OTROSAYUDANTES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de otros materiales quirúrgicos utilizados en la cirugía adicionales a los estándar (VARCHAR MAX).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'OTROSMATERIALES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Otros Materiales de cirugia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'OTROSMATERIALES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'OTROSMATERIALES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones generales de la cirugía: hallazgos, eventos intraoperatorios, incidentes (VARCHAR MAX).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'OBSERVACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observacion de la cirugia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'OBSERVACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'OBSERVACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (bit) de si se comenzó a diligenciar la hoja de gasto/consumo de la cirugía (BIT).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'DILIGENCIOHOJAGASTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si ya se empezo a diligenciar hoja de gasto QX', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'DILIGENCIOHOJAGASTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'DILIGENCIOHOJAGASTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario que registró las causas de inasistencia del paciente (CHAR 20).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'CODUSUINA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo de usuario quien registro casusas inatencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'CODUSUINA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'CODUSUINA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones detalladas sobre las causas de inasistencia del paciente a la cirugía (VARCHAR 200).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'OBSCAUINA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'observaciones del causas inatencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'OBSCAUINA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'OBSCAUINA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de causa de inasistencia del paciente. Clasificación de motivos de no presentación (CHAR 4).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'CODCAUINA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de Causa Inatencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'CODCAUINA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'CODCAUINA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se registraron las causas de inasistencia del paciente (DATETIME).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'FECHAINA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha que registro Causas Inatención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'FECHAINA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'FECHAINA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código/consecutivo de la sala quirúrgica asignada para la cirugía. Referencia a tabla AGENSALAC (INT, FK).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'AGENSALAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo consecutivo de la sala', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'AGENSALAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'AGENSALAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso hospitalario con el que se creó la solicitud del paquete quirúrgico. Aplica para cirugías de origen ambulatorio (CHAR 10, FK).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ingreso con el que se creo la solicitud del paquete QX - para cirugias de origen Ambulatorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de solicitud de confirmación del paquete quirúrgico desde farmacia: 1=Pendiente, 2=Solicitado a farmacia (INT, default=1).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'ESTADOFARM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado Solicitud de confirmacion del paquete quirurgico desde GX :   1. Pendiente 2. Solicitado a Farmacia  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'ESTADOFARM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'ESTADOFARM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Origen de la cirugía: 1=Ambulatoria (sin hospitalización previa), 2=Hospitalaria (paciente ya ingresado) (INT).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'ORIGENQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Cirugía de origen Ambulatorias  2 - Cirugía de origen Hospitalaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'ORIGENQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'ORIGENQX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la radicación/inscripción de la cirugía en la tabla ADRADICACIONQX. Vinculación con solicitud oficial (INT, FK).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'IDRADICACIONQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la radicacion desde la tabla ADRADICACIONQX', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'IDRADICACIONQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'IDRADICACIONQX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Comentario o justificación de la cancelación de la cirugía programada (VARCHAR 500).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'OBSERCAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Comentario de cancelacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'OBSERCAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'OBSERCAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se canceló la cirugía programada (DATETIME).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'FECHACAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha cancelacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'FECHACAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'FECHACAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario que registró la cancelación de la cirugía (CHAR 20).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'CODUSUCAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario cancelacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'CODUSUCAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'CODUSUCAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de causa/motivo de cancelación. Referencia a tabla AGCACANQX para clasificación (CHAR 3).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'CODCAUCAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo cancelacion, proviene de tabla AGCACANQX]', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'CODCAUCAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'CODCAUCAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la agenda médica del profesional (tabla AGAGEMEDC). Vinculación con disponibilidad del cirujano (INT, FK).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'IDAGENDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la agenda medica (AGAGEMEDC)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'IDAGENDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'IDAGENDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del procedimiento principal si este es un procedimiento secundario o complementario (INT, FK).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'IDPADRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del procedimiento principal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'IDPADRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'IDPADRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (bit) de si es la cirugía principal dentro del paquete quirúrgico (BIT).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'PRINCIPAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Si es la cirugía principal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'PRINCIPAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'PRINCIPAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Autonúmerico del registro de orden de compra/insumos quirúrgicos de la tabla HCORDPROQ (INT, FK).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'AUTOHCORDPROQ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico del registro de la tabla de HCORDPROQ.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'AUTOHCORDPROQ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'AUTOHCORDPROQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la especialidad médica con la que fue creada la agenda del cirujano. Referencia a tabla INESPECIA (CHAR 3, FK).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo de la Especialidad con la que fue creada la  agenda del medico se saca de la tabla AGAGEMEDC (Tabla que Contiene la Agenda Medica)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora exacta en que se registró la programación de la cirugía en el sistema (DATETIME).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'FECREGSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la Fecha y hora en que se registró la CIRUGIA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'FECREGSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'FECREGSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario que asignó/programó la cirugía en el sistema (CHAR 20).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'CODUSUASI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Usuario que Asigno la CIRUGIA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'CODUSUASI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'CODUSUASI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del estado actual del procedimiento quirúrgico: 0=Programada, 1=Paciente admitido, 2=En sala espera, 3=En sala quirúrgica, 4=En recuperación, 5=Con alta, 6=Anulada/cancelada (CHAR 1).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'CODESTPQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo estado del procedimiento  0. Cirugia Programada  1. Paciente admitido (Cirugia Origen Ambulatoria)  2. Paciente en sala de espera  3. Paciente en sala quirurgica  4. Paciente en recuperación  5.  Paciente con alta  6. Anulado - Cancelada  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'CODESTPQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'CODESTPQX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora final/cierre de la cirugía programada (término previsto de la intervención) (DATETIME).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'FECHORAFI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y hora final de la C?irugia programada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'FECHORAFI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'FECHORAFI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora inicial de la cirugía programada (inicio previsto de la intervención) (DATETIME).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'FECHORAIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y hora inicial de la cirugia programada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'FECHORAIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'FECHORAIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración estimada o real del procedimiento quirúrgico en minutos (INT).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'DURPROCQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Duracion del procedimiento qx', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'DURPROCQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'DURPROCQX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de la salud/cirujano. PII ofuscado. Referencia a tabla INPROFSAL (VARCHAR 25, FK, MASKED).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único de procedimiento/servicio (CUPS) según nomenclatura RIPS. Referencia a tabla INCUPSIPS (CHAR 20, FK).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Unico de Procedimientos y Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código/identificación del paciente (cédula, pasaporte, identificación). PII ofuscado. Referencia a tabla INPACIENT (VARCHAR 25, FK, MASKED).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención/institución donde se realiza la cirugía. Referencia a tabla ADCENATEN (CHAR 10, FK).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Autonúmerico secuencial de la tabla AGEPROGQX. Identificador único del registro de programación quirúrgica (INT, PK).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'CODAUTONU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'CODAUTONU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX', @level2type = N'COLUMN', @level2name = N'CODAUTONU';


GO
CREATE NONCLUSTERED INDEX [IX_AGEPROGQX_CITA_PAGINACION]
    ON [dbo].[AGEPROGQX]([CODCENATE] ASC, [FECHORAIN] ASC, [FECREGSIS] ASC)
    INCLUDE([CODAUTONU]);


GO
CREATE NONCLUSTERED INDEX [IX_AGEPROGQX_Centro_FecRegSis_Cover]
    ON [dbo].[AGEPROGQX]([CODCENATE] ASC, [FECREGSIS] ASC)
    INCLUDE([CODAUTONU], [FECHORAIN], [FECHORAFI], [CODESPECI], [IPCODPACI], [CODPROSAL], [CODUSUASI], [CODUSUCAN], [CODUSUINA], [AGENSALAC], [CODSERIPS], [DiagnosisCode], [CODCAUCAN], [NUMINGRES], [ORIGENQX], [CODESTPQX], [OBSERVACION], [FECHACAN], [OBSERCAN], [CODCAUINA], [OBSCAUINA], [FECHAINA], [CONFASIST], [IDDESCRIPCIONRELACIONADA]);


GO
CREATE NONCLUSTERED INDEX [IX_AGEPROGQX_Centro_FecHorAin_Cover]
    ON [dbo].[AGEPROGQX]([CODCENATE] ASC, [FECHORAIN] ASC)
    INCLUDE([CODAUTONU], [FECREGSIS], [FECHORAFI], [CODESPECI], [IPCODPACI], [CODPROSAL], [CODUSUASI], [CODUSUCAN], [CODUSUINA], [AGENSALAC], [CODSERIPS], [DiagnosisCode], [CODCAUCAN], [NUMINGRES], [ORIGENQX], [CODESTPQX], [OBSERVACION], [FECHACAN], [OBSERCAN], [CODCAUINA], [OBSCAUINA], [FECHAINA], [CONFASIST], [IDDESCRIPCIONRELACIONADA]);


GO
CREATE NONCLUSTERED INDEX [IX_AGEPROGQX_CENATE_FECREGSIS]
    ON [dbo].[AGEPROGQX]([CODCENATE] ASC, [FECREGSIS] ASC)
    INCLUDE([CODAUTONU]);


GO
CREATE NONCLUSTERED INDEX [IX_AGEPROGQX_CENATE_FECHORAIN]
    ON [dbo].[AGEPROGQX]([CODCENATE] ASC, [FECHORAIN] ASC)
    INCLUDE([CODAUTONU]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Programación de cirugías y procedimientos quirúrgicos. Registra cada procedimiento quirúrgico agendado: sala, tiempos, profesional, estado, cancelaciones, inactivaciones y datos de anestesia para la gestión de salas de cirugía.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQX';
