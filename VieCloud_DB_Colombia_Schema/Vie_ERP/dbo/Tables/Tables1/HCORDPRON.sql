CREATE TABLE [dbo].[HCORDPRON] (
    [IDETIPHIS]                     CHAR (9)                                                                           NOT NULL,
    [NUMEFOLIO]                     NCHAR (10)                                                                         NOT NULL,
    [IPCODPACI]                     VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')   NOT NULL,
    [NUMINGRES]                     CHAR (10)                                                                          NOT NULL,
    [CODCENATE]                     CHAR (10)                                                                          NOT NULL,
    [UFUCODIGO]                     CHAR (10)                                                                          NOT NULL,
    [CODPROSAL]                     CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')      NOT NULL,
    [FECORDMED]                     DATETIME                                                                           NOT NULL,
    [CODSERIPS]                     CHAR (20)                                                                          NOT NULL,
    [CANSERIPS]                     INT                                                                                NOT NULL,
    [OBSSERIPS]                     VARCHAR (2000)                                                                     NULL,
    [PRISERIPS]                     CHAR (1)                                                                           NOT NULL,
    [ESTSERIPS]                     CHAR (1)                                                                           NULL,
    [MANEXTPRO]                     BIT                                                                                NOT NULL,
    [CODDIAGNO]                     CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')       NULL,
    [EXREASITI]                     BIT                                                                                CONSTRAINT [DF_HCORDPRON_EXREASITI] DEFAULT ((0)) NOT NULL,
    [INDAUDFOR]                     NUMERIC (18)                                                                       NOT NULL,
    [AUTO]                          INT                                                                                IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [GENSERVICEORDER]               INT                                                                                NULL,
    [NOMARCH]                       VARCHAR (250)                                                                      NULL,
    [TIPENFERME]                    TINYINT                                                                            NULL,
    [NUMDIALSEM]                    INT                                                                                NULL,
    [TIEMPSESION]                   INT                                                                                NULL,
    [UF]                            INT                                                                                NULL,
    [INTERPRET]                     VARCHAR (4000) MASKED WITH (FUNCTION = 'partial(0, "interpretation_Ofuscado", 0)') NULL,
    [CODPROINT]                     CHAR (20)                                                                          NULL,
    [NUMFOLINT]                     NCHAR (10)                                                                         NULL,
    [SOLICITASALA]                  BIT                                                                                NULL,
    [MEDREALI]                      CHAR (20)                                                                          NULL,
    [ESPEREALI]                     CHAR (3)                                                                           NULL,
    [FECHREALI]                     DATETIME                                                                           NULL,
    [TIPOFACTURACION]               INT                                                                                NULL,
    [IDRIASCUPS]                    INT                                                                                NULL,
    [CODVIAADMIN]                   CHAR (3)                                                                           NULL,
    [LITRXMINUT]                    DECIMAL (18, 2)                                                                    NULL,
    [TIPOOXIGENO]                   INT                                                                                NULL,
    [DURACION]                      INT                                                                                NULL,
    [UNIDADDURACION]                INT                                                                                NULL,
    [LATERALIDAD]                   INT                                                                                CONSTRAINT [DF__HCORDPRON__LATER__3EB89176] DEFAULT ((0)) NOT NULL,
    [IDDESCRIPCIONRELACIONADA]      INT                                                                                NULL,
    [CORRELACION]                   TINYINT                                                                            CONSTRAINT [DF__HCORDPRON__CORRE__7CABA24E] DEFAULT ((2)) NOT NULL,
    [OBSERVACIONCORRELA]            VARCHAR (4000)                                                                     NULL,
    [TraceabilityPaperworkEventsId] INT                                                                                NULL,
    [TraceabilityPaperworkId]       INT                                                                                NULL,
    [IDESQUEMAONC]                  INT                                                                                NULL,
    [IDADRADICACIONQX]              INT                                                                                NULL,
    [CODCENATEPRO]                  CHAR (10)                                                                          NULL,
    [UFUCODIGOPRO]                  CHAR (10)                                                                          NULL,
    [IDAREAPRO]                     INT                                                                                NULL,
    [CODUSUPRO]                     CHAR (20)                                                                          NULL,
    [FECHAPRO]                      DATETIME                                                                           NULL,
    [AUTOCITA]                      INT                                                                                NULL,
    [OBSANULADO]                    VARCHAR (300)                                                                      NULL,
    [PROANULADO]                    CHAR (20)                                                                          NULL,
    [FECHAANULADO]                  DATETIME                                                                           NULL,
    [CODMOTANU]                     CHAR (4)                                                                           NULL,
    [Principal] BIT NULL,
    [AuthorizationEventId]          INT            NULL,
    CONSTRAINT [PK_HCORDPRON_1] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_HCORDPRON_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCORDPRON_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCORDPRON_Area] FOREIGN KEY ([IDAREAPRO]) REFERENCES [dbo].[HCAREASC] ([ID]),
    CONSTRAINT [FK_HCORDPRON_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]) ON UPDATE CASCADE,
    CONSTRAINT [FK_HCORDPRON_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_HCORDPRON_INESPECIA] FOREIGN KEY ([ESPEREALI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_HCORDPRON_INPacient] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCORDPRON_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCORDPRON_INPROFSAL1] FOREIGN KEY ([MEDREALI]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCORDPRON_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_HCORDPRON_RIASCUPS] FOREIGN KEY ([IDRIASCUPS]) REFERENCES [dbo].[RIASCUPS] ([ID]),
    CONSTRAINT [FK_HCORDPRON_Schemes] FOREIGN KEY ([IDESQUEMAONC]) REFERENCES [EHR].[Schemes] ([Id]),
    CONSTRAINT [FK_PROANULADO] FOREIGN KEY ([PROANULADO]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [IX_HCORDPRON_1] UNIQUE NONCLUSTERED ([IDETIPHIS] ASC, [NUMEFOLIO] ASC, [IPCODPACI] ASC, [NUMINGRES] ASC, [CODSERIPS] ASC, [MANEXTPRO] ASC, [IDDESCRIPCIONRELACIONADA] ASC)
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCORDPRON].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCORDPRON].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCORDPRON].[CODDIAGNO]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCORDPRON].[INTERPRET]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');




GO

CREATE NONCLUSTERED INDEX [HCORDPRON_CODCENATE_CODSERIPS_ESTSERIPS_MANEXTPRO_SOLICITASALA]
    ON [dbo].[HCORDPRON]([CODCENATE] ASC, [CODSERIPS] ASC, [ESTSERIPS] ASC, [MANEXTPRO] ASC, [SOLICITASALA] ASC)
    INCLUDE([NUMEFOLIO], [IPCODPACI], [NUMINGRES], [UFUCODIGO], [CODPROSAL], [FECORDMED], [PRISERIPS], [IDDESCRIPCIONRELACIONADA]);


GO
CREATE NONCLUSTERED INDEX [IX_HCORDPRON_NUMINGRES_MANEXTPRO]
    ON [dbo].[HCORDPRON]([NUMINGRES] ASC, [MANEXTPRO] ASC)
    INCLUDE([CANSERIPS], [CODSERIPS], [ESTSERIPS], [IPCODPACI], [NUMEFOLIO]);


GO
CREATE NONCLUSTERED INDEX [IX_HCORDPRON_CODPROSAL_MANEXTPRO]
    ON [dbo].[HCORDPRON]([CODPROSAL] ASC, [MANEXTPRO] ASC)
    INCLUDE([AUTO], [CANSERIPS], [CODSERIPS], [FECORDMED], [GENSERVICEORDER], [IPCODPACI], [NUMEFOLIO], [NUMINGRES], [OBSSERIPS], [UFUCODIGO]);


GO
ALTER INDEX [IX_HCORDPRON_CODPROSAL_MANEXTPRO]
    ON [dbo].[HCORDPRON] DISABLE;




GO
CREATE NONCLUSTERED INDEX [IX_HCORDPRON]
    ON [dbo].[HCORDPRON]([NUMEFOLIO] ASC, [IPCODPACI] ASC, [NUMINGRES] ASC, [MANEXTPRO] ASC, [IDDESCRIPCIONRELACIONADA] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_HCORDPRON_ESTSERIPS_SOLICITASALA_CODCENATE_MANEXTPRO]
    ON [dbo].[HCORDPRON]([ESTSERIPS] ASC, [SOLICITASALA] ASC, [CODCENATE] ASC, [MANEXTPRO] ASC)
    INCLUDE([CANSERIPS], [CODDIAGNO], [CODPROSAL], [CODSERIPS], [FECORDMED], [IDDESCRIPCIONRELACIONADA], [IPCODPACI], [NUMEFOLIO], [NUMINGRES], [OBSSERIPS], [PRISERIPS], [UFUCODIGO]);


GO
CREATE NONCLUSTERED INDEX [IX_HCORDPRON_MANEXTPRO]
    ON [dbo].[HCORDPRON]([MANEXTPRO] ASC)
    INCLUDE([CANSERIPS], [CODSERIPS], [ESTSERIPS], [IPCODPACI], [NUMEFOLIO], [NUMINGRES]);


GO
CREATE NONCLUSTERED INDEX [IX_HCORDPRON_DashboardMedicalOrders_CareCenter_RequestDate]
    ON [dbo].[HCORDPRON]([CODCENATE] ASC, [FECORDMED] DESC)
    INCLUDE([AUTO], [UFUCODIGO], [NUMINGRES], [NUMEFOLIO], [IPCODPACI], [CODPROSAL], [CANSERIPS], [CODSERIPS], [IDDESCRIPCIONRELACIONADA], [OBSSERIPS])
    WHERE [MANEXTPRO] = 1 AND [ESTSERIPS] <> '5' AND [FECORDMED] >= '20260102';


GO

 /* select * from Admission.AdmissionType */
create TRIGGER [dbo].[tgg_ActualizarCampo_ESTSERIPS] 
   ON  [dbo].[HCORDPRON]
   AFTER INSERT, UPDATE
AS 
BEGIN

	SET NOCOUNT ON;

	update HCORDPRON set ESTSERIPS = 2 from HCORDPRON i inner join inserted t on i.AUTO = t.AUTO where t.ESTSERIPS = 4
END

---update HCORDPRON set ESTSERIPS = 2 where ESTSERIPS = 4
GO
DISABLE TRIGGER [dbo].[tgg_ActualizarCampo_ESTSERIPS]
    ON [dbo].[HCORDPRON];


GO

CREATE TRIGGER [dbo].[HospitalCima_Softland_NotSurgicalProcedureOrder] 
   ON [dbo].[HCORDPRON]
   AFTER  INSERT,DELETE,UPDATE
AS 
BEGIN
	
	SET NOCOUNT ON;

	
	DECLARE @action as  int 
	declare @dataid as varchar(200)

	if exists(select AUTO from inserted ) and exists(select AUTO from deleted) begin
		set @action = 2 --actualizando
		set @dataid = (select top 1 AUTO from inserted)
	end else if exists(select AUTO from inserted ) and not exists(select AUTO from deleted) begin
		set @action = 1 --insertando
		set @dataid = (select top 1 AUTO from inserted)
	end else if not exists(select AUTO from inserted ) and exists(select AUTO from deleted) begin
		set @action = 3 --eliminando
		set @dataid = (select top 1 AUTO from deleted)
	end

	
	if @dataid is null begin
		return
	end


	insert into [integrations].[cimahospital_softland_synch]
			   ([dataid]
			   ,[type]
			   ,[action]
			   ,[transactiondate]
			   ,[state]
			   ,[errormessage])
		 values
			   (@dataid,7,@action,GETDATE(),0,NULL)

END
GO
DISABLE TRIGGER [dbo].[HospitalCima_Softland_NotSurgicalProcedureOrder]
    ON [dbo].[HCORDPRON];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del motivo de anulación (referencia tabla HCMOANULB). Justificación por la cual se cancela la orden de procedimiento no quirúrgico desde dashboard.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'CODMOTANU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del motivo de anulacion (codigo de la tabla HCMOANULB) -->23-05-2025 - PBI 27070 Crear modal Justificacion anulación para las ordenes de Procedimientos no QX <--', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'CODMOTANU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'CODMOTANU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de anulación (DATETIME). Se registra cuando el procedimiento no quirúrgico se cancela desde dashboard de otros procedimientos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'FECHAANULADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fecha militar, que se llena cuando se anula el servicio desde dashboard de otros procedimientos. -->23-05-2025 - PBI 27070 Crear modal Justificacion anulación para las ordenes de Procedimientos no QX <--', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'FECHAANULADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'FECHAANULADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de la salud que anula (FK INPROFSAL). Identifica quién cancela la orden de procedimiento desde dashboard.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'PROANULADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'profesional de la salud, que se llena cuando se anula el servicio desde dashboard de otros procedimientos. -->23-05-2025 - PBI 27070 Crear modal Justificacion anulación para las ordenes de Procedimientos no QX <--', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'PROANULADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'PROANULADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observación/comentario de la anulación (VARCHAR 300). Justificación adicional cuando se cancela el servicio desde dashboard otros procedimientos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'OBSANULADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observacion cuando se anula el servicio desde dashboard de otros procedimientos. -->23-05-2025 - PBI 27070 Crear modal Justificacion anulación para las ordenes de Procedimientos no QX <--', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'OBSANULADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'OBSANULADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de la cita programada para procedimiento NO quirúrgico (FK AGEPROGQX). Vinculación con programa quirúrgico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'AUTOCITA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la cita con el que se programa el ´procedimiento NO QX (AGEPROGQX)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'AUTOCITA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'AUTOCITA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de procesamiento real (DATETIME). Cuándo se procesa efectivamente la orden desde dashboard.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'FECHAPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha real en que procesa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'FECHAPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'FECHAPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario que procesa (CHAR 20). Profesional que ejecuta el procesamiento de la orden.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'CODUSUPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo usuario procesa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'CODUSUPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'CODUSUPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del área de otros procedimientos que procesa (FK HCAREASC). Unidad responsable del procesamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'IDAREAPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del area de otro procedimiento que procesa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'IDAREAPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'IDAREAPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código Unidad Funcional que procesa (CHAR 10). Se genera desde dashboard cuando se procesa el procedimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'UFUCODIGOPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Unidad Funcional desde el que se procesa. Este dato se genera desde el dashboard otros procedimientos cuando se procese', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'UFUCODIGOPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'UFUCODIGOPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código Centro de Atención que procesa (CHAR 10). Se genera desde dashboard cuando se procesa la orden.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'CODCENATEPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion desde el que se procesa. Este dato se genera desde el dashboard otros procedimientos cuando se procese', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'CODCENATEPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'CODCENATEPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID radicación de cirugía para autorización (FK trámites QX). Primer paso autorización grupos procedimientos invasivos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'IDADRADICACIONQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID radicacion de cirugia con la que se tramita el proceso de autorizacion (primer pestaña de Grupos de procedimientos invasivos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'IDADRADICACIONQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'IDADRADICACIONQX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de esquema oncológico (FK EHR.Schemes). Para procedimientos relacionados con tratamiento oncológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'IDESQUEMAONC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador de esquema oncologico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'IDESQUEMAONC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'IDESQUEMAONC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID cabecera del trámite/paperwork (INT). Registro de gestión documental; puede estar vacío si solicitud fue cancelada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera del trámite, pueda que no tenga eventos relacionados y esto se da cuando se cancela una solicitud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID último evento registrado en trámite (INT). Trazabilidad del estado actual del procedimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkEventsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del último evento registrado al trámite', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkEventsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkEventsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observación de la correlación (VARCHAR 4000, PII ofuscada). Nota sobre relación con otros servicios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'OBSERVACIONCORRELA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciòn de la correlacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'OBSERVACIONCORRELA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'OBSERVACIONCORRELA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador manejo correlación (TINYINT: 1=Sí, 2=No, 3=Sin especificar). Determina si servicio se relaciona con otros.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'CORRELACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Determina si se maneja correlación :   1-Si   2-No   3-Sin especificar ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'CORRELACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'CORRELACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID descripción de relación (INT). Vinculación contrato CUPS (VIE ERP contract.CUPSEntityContractDescriptions).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lateralidad del procedimiento (TINYINT: 0=No aplica, 1=Izquierda, 2=Derecha, 3=Bilateral, 4=Multilateral). Para procedimientos segmentados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'LATERALIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo de Lateralidad:   0 - No aplica   1 - Izquierda   2 - Derecha   3 - Bilateral   4 - Multilateral ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'LATERALIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'LATERALIDAD';




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de duración servicio oxígeno (TINYINT: 1=Días, 2=Meses). Solo se diligencia para servicios oxigenoterapia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'UNIDADDURACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo se diligencia unicamente para servicio de tipo Oxigeno.  determina la unidad de la duracion  1) Dias  2) Meses    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'UNIDADDURACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'UNIDADDURACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad/duración servicio oxígeno (INT). Solo para oxígeno; número de días o meses según UNIDADDURACION.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'DURACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo se diligencia unicamente para servicio de tipo Oxigeno.  es una cantidad  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'DURACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'DURACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo oxígeno (TINYINT: 0=Fijo, 1=Continuo). Campo lista desplegable, obligatorio para servicios oxigenoterapia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'TIPOOXIGENO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo se diligencia unicamente para servicio de tipo Oxigeno.    campo tipo lista desplegable con los valores 0- "Fijo" y 1 - "Continuo", debe  ser obligatorio sin postular ningún valor.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'TIPOOXIGENO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'TIPOOXIGENO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Flujo oxígeno en litros por minuto (DECIMAL 18,2). Solo para oxigenoterapia; especifica caudal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'LITRXMINUT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo se diligencia unicamente para servicio de tipo Oxigeno.  Listros por minuto  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'LITRXMINUT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'LITRXMINUT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código vía de administración oxígeno (CHAR 3, FK HCPARCONO). Parámetros consumo oxígeno.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'CODVIAADMIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo se diligencia unicamente para servicio de tipo Oxigeno.  proviene del codigo de la tabla (Parámetros de Consumo Oxígeno)  (HCPARCONO)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'CODVIAADMIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'CODVIAADMIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID RIAS CUPS para facturación (FK RIASCUPS). Se llena cuando TIPOFACTURACION=1 (RIAS).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'IDRIASCUPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuando campo TIPO FACTURACION sea 1 guardamos  el IdRiasCups a la que aplica de lo contrario queda NULL  Relacion con la tabla RIASCUPS.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'IDRIASCUPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'IDRIASCUPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de facturación (INT: 1=RIAS, 2=Consulta Externa). Determina modelo de facturación del servicio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'TIPOFACTURACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'RIAS - Determina como se debe facturar el servicio:  1 - Rias  2 - Consulta Externa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'TIPOFACTURACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'TIPOFACTURACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha realización del procedimiento (DATETIME). Cuándo se ejecutó efectivamente el servicio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'FECHREALI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la fecha realización', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'FECHREALI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'FECHREALI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código especialidad que realiza (CHAR 3, FK INESPECIA). Profesional especialidad ejecutora.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'ESPEREALI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda codigo de la especialidad que realiza', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'ESPEREALI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'ESPEREALI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código profesional/médico ejecutor (CHAR 20, FK INPROFSAL). Quién realiza el procedimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'MEDREALI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el CodigoProfesional o medico quien realiza', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'MEDREALI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'MEDREALI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Requiere sala para procedimiento (BIT). Especifica si servicio exige recursos de quirófano.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'SOLICITASALA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si este servicio solicitado exige sala', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'SOLICITASALA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'SOLICITASALA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio de interpretación (NCHAR 10). Documento donde se registra resultado/interpretación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'NUMFOLINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Folio donde se Interpreta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'NUMFOLINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'NUMFOLINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código médico que interpreta (CHAR 20, FK INPROFSAL). Profesional que analiza resultados paraclínicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'CODPROINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Medico que interpreta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'CODPROINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'CODPROINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Interpretación de resultados (VARCHAR 4000, PII ofuscada). Análisis médico de examen/paraclínico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'INTERPRET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Interpretacion de Resultados (paraclinicos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'INTERPRET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'INTERPRET';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ultrafiltración diálisis (INT 4 dígitos). Volumen eliminado en proceso de hemodiálisis.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'UF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Proceso de Dialisis:  UF campo de 4 digitos, no permite decimales  ultrafiltración', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'UF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'UF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiempo sesión diálisis en horas (INT 2 dígitos). Duración de cada sesión de tratamiento renal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'TIEMPSESION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Proceso de Dialisis:  Tiempo de la sesion en horas, campo de 2 digitos, no permite decimales  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'TIEMPSESION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'TIEMPSESION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número diálisis por semana (INT 2 dígitos). Frecuencia semanal de tratamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'NUMDIALSEM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Proceso de Dialisis:  Numero de dialisis por semana  campo de 2 digitos, no permite decimales  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'NUMDIALSEM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'NUMDIALSEM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo enfermedad renal (TINYINT: 1=Crónico, 2=Agudo). Clasificación insuficiencia renal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'TIPENFERME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Proceso de Dialisis:  Tipo de enfermedad: 1->Crónico, 2-> Agudo  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'TIPENFERME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'TIPENFERME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre archivo adjunto (VARCHAR 250). Documento/evidencia anexa a la orden.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'NOMARCH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del Archivo Adjunto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'NOMARCH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'NOMARCH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID orden de servicio generada (INT, FK GENSERVICEORDER). Se llena al crear orden desde Control de Cuenta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'GENSERVICEORDER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id de la orden de servicio en la cual quedo incluido el servicio No Quirurgico, Este campo se llena cuando se genera la orden de servicio desde el formulario de Control de Cuenta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'GENSERVICEORDER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'GENSERVICEORDER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Autonumérico tabla HCORDPRON (INT IDENTITY). Clave primaria única orden procedimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'AQutonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código/indicador auditoría (NUMERIC 18). Referencia auditoria interna procedimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Examen realizado en sitio (BIT, default=0). Si se ejecutó en lugar de la solicitud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'EXREASITI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el examen es realizado en sitio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'EXREASITI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'EXREASITI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código diagnóstico principal (CHAR 4, FK INDIAGNOS, PII ofuscada). Motivo clínico procedimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Diagnostico Principal Relacionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Producto plan manejo externo (BIT). Si procedimiento es plan de tratamiento externalizado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'MANEXTPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Este Producto Corresponde a un plan de Manejo Externo?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'MANEXTPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'MANEXTPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado servicio (CHAR 1: 1=Ordenado, 2=Completado, 3=Interpretado, 4=Sin Interfaz, 5=Anulado). Ciclo vida.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'ESTSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado Servicio IPS  1: Ordenado  2: Completado  3: Interpretado  4: Sin Interfaz  5: Anulado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'ESTSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'ESTSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prioridad servicio (CHAR 1: 1=Urgente, 2=Rutina). Nivel de urgencia atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'PRISERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Prioridad del Servicio Solicitado  1: Urgente  2: Rutina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'PRISERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'PRISERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observación servicio (VARCHAR 2000). Nota médica/administrativa sobre procedimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'OBSSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observacion Servicio IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'OBSSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'OBSSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad servicio; fracciones radioterapia (INT). Repeticiones; para radioterapia número sesiones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'CANSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Servicio IPS - Aca tambien va el Numero de fracciones cuando el servicio es radioterapias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'CANSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'CANSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CUPS único (CHAR 20, FK INCUPSIPS). Identificador procedimiento/servicio normalizado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Unico de Procedimientos y Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha solicitud orden (DATETIME). Cuándo se prescribe el procedimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'FECORDMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Solicitud de la Orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'FECORDMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'FECORDMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código profesional solicitante (CHAR 20, FK INPROFSAL, PII ofuscada). Médico que ordena.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código Unidad Funcional (CHAR 10, FK INUNIFUNC). Departamento/área donde se solicita.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código Centro de Atención (CHAR 10, FK ADCENATEN). Institución/sede procedimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso/admisión (CHAR 10, FK ADINGRESO). Identificador hospitalización/atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código paciente (VARCHAR 25, FK INPACIENT, PII ofuscada). Cédula/identificación paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio (NCHAR 10). Identificador secuencial orden en la historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre interno tipo historia (CHAR 9). Clasificación/tipo documento clínico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Indica cual es el procedimiento no qx principal de la atención - Solo aplica para unidad de urgencias',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'HCORDPRON',
    @level2type = N'COLUMN',
    @level2name = N'Principal'


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Órdenes médicas de procedimientos y servicios de salud generadas en la historia clínica del paciente. Registra cada servicio ordenado (procedimientos, exámenes, terapias, oxígeno, diálisis, entre otros), su estado, prioridad, diagnóstico asociado y trazabilidad de anulaciones y ejecuciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del último evento de autorización con estado Autorizado registrado para esta orden. Actualizado automáticamente en la misma transacción al guardar el evento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'AuthorizationEventId';
