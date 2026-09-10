CREATE TABLE [dbo].[HCORDINTE] (
    [IDETIPHIS]                     CHAR (9)                                                                          NOT NULL,
    [NUMEFOLIO]                     NCHAR (10)                                                                        NOT NULL,
    [IPCODPACI]                     VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')  NOT NULL,
    [NUMINGRES]                     CHAR (10)                                                                         NOT NULL,
    [CODCENATE]                     CHAR (10)                                                                         NOT NULL,
    [UFUCODIGO]                     CHAR (10)                                                                         NOT NULL,
    [CODPROSAL]                     CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')     NOT NULL,
    [FECORDMED]                     DATETIME                                                                          NOT NULL,
    [CODSERIPS]                     CHAR (20)                                                                         NOT NULL,
    [CANSERIPS]                     INT                                                                               NOT NULL,
    [OBSSERIPS]                     VARCHAR (2000)                                                                    NULL,
    [PRISERIPS]                     CHAR (1)                                                                          NOT NULL,
    [ESTSERIPS]                     CHAR (1)                                                                          NULL,
    [MANEXTPRO]                     BIT                                                                               NOT NULL,
    [CODDIAGNO]                     CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')      NULL,
    [INDAUDFOR]                     NUMERIC (18)                                                                      NOT NULL,
    [CODESPECI]                     CHAR (3)                                                                          NOT NULL,
    [REPORAUTO]                     BIT                                                                               NULL,
    [CODPROENV]                     CHAR (20)                                                                         NULL,
    [MSGPENENV]                     BIT                                                                               NULL,
    [INTERPRET]                     VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "Interpretation_Ofuscado", 0)') NULL,
    [CODPROINT]                     CHAR (20)                                                                         NULL,
    [NUMFOLINT]                     NCHAR (10)                                                                        NULL,
    [CODPRONOT]                     CHAR (20)                                                                         NULL,
    [MOVPRONOT]                     CHAR (15)                                                                         NULL,
    [FECREGNOT]                     DATETIME                                                                          NULL,
    [AUTO]                          INT                                                                               IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [GENSERVICEORDER]               INT                                                                               NULL,
    [TIPOFACTURACION]               INT                                                                               NULL,
    [IDRIASCUPS]                    INT                                                                               NULL,
    [IDDESCRIPCIONRELACIONADA]      INT                                                                               NULL,
    [TraceabilityPaperworkEventsId] INT                                                                               NULL,
    [TraceabilityPaperworkId]       INT                                                                               NULL,
    [FECHAINT]                      DATETIME                                                                          NULL,
    [NUMFOLIORESIDENTE]             NCHAR (10)                                                                        NULL,
    [AuthorizationEventId]          INT                                                                               NULL,
    CONSTRAINT [PK_HCORDINTE] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_HCORDINTE_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCORDINTE_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCORDINTE_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_HCORDINTE_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_HCORDINTE_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_HCORDINTE_INPacient] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCORDINTE_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCORDINTE_INPROFSAL1] FOREIGN KEY ([CODPROENV]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCORDINTE_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_HCORDINTE_RIASCUPS] FOREIGN KEY ([IDRIASCUPS]) REFERENCES [dbo].[RIASCUPS] ([ID])
);


GO
ALTER TABLE [dbo].[HCORDINTE] NOCHECK CONSTRAINT [FK_HCORDINTE_INPROFSAL];


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCORDINTE].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCORDINTE].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCORDINTE].[CODDIAGNO]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCORDINTE].[INTERPRET]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');




GO
ALTER TABLE [dbo].[HCORDINTE] NOCHECK CONSTRAINT [FK_HCORDINTE_INPROFSAL];


GO



GO
ALTER TABLE [dbo].[HCORDINTE] NOCHECK CONSTRAINT [FK_HCORDINTE_INPROFSAL];



GO
ALTER TABLE [dbo].[HCORDINTE] NOCHECK CONSTRAINT [FK_HCORDINTE_INPROFSAL];


GO



GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_HCORDINTE_NUMINGRES_MANEXTPRO]
    ON [dbo].[HCORDINTE]([NUMINGRES] ASC, [MANEXTPRO] ASC)
    INCLUDE([CANSERIPS], [CODSERIPS], [IPCODPACI], [NUMFOLINT]);


GO
CREATE NONCLUSTERED INDEX [IX_OrdenesInterconsulta]
    ON [dbo].[HCORDINTE]([IPCODPACI] ASC, [NUMINGRES] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_HCORDINTE_CODCENATE_CODESPECI_ESTSERIPS_CODPROSAL_FECORDMED_IDETIPHIS_IPCODPACI_NUMFOLIORESIDENTE_NUMINGRES]
    ON [dbo].[HCORDINTE]([CODCENATE] ASC, [CODESPECI] ASC, [ESTSERIPS] ASC)
    INCLUDE([CODPROSAL], [FECORDMED], [IDETIPHIS], [IPCODPACI], [NUMFOLIORESIDENTE], [NUMINGRES]);


GO
CREATE NONCLUSTERED INDEX [IX_HCORDINTE]
    ON [dbo].[HCORDINTE]([NUMEFOLIO] ASC, [IPCODPACI] ASC, [NUMINGRES] ASC, [MANEXTPRO] ASC, [CODSERIPS] ASC, [IDDESCRIPCIONRELACIONADA] ASC);

GO
  CREATE INDEX IX_HCORDINTE_RIPS
  ON HCORDINTE (CODCENATE, FECORDMED)
  INCLUDE (NUMEFOLIO, IPCODPACI, NUMINGRES, CODSERIPS, FECHAINT, ESTSERIPS, NUMFOLINT)

GO
CREATE NONCLUSTERED INDEX [IX_HCORDINTE_DashboardMedicalOrders_CareCenter_RequestDate]
    ON [dbo].[HCORDINTE]([CODCENATE] ASC, [FECORDMED] DESC)
    INCLUDE([AUTO], [UFUCODIGO], [NUMINGRES], [NUMEFOLIO], [IPCODPACI], [CODPROSAL], [CANSERIPS], [CODSERIPS], [IDDESCRIPCIONRELACIONADA], [OBSSERIPS])
    WHERE [MANEXTPRO] = 1 AND [ESTSERIPS] <> '5' AND [FECORDMED] >= '20260102';


GO

CREATE TRIGGER [dbo].[HospitalCima_Softland_InterconsultationOrder] 
   ON [dbo].[HCORDINTE]
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
			   (@dataid,8,@action,GETDATE(),0,NULL)

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Folio de la interconsulta donde el médico residente asistió y requiere validación del especialista solicitante. VARCHAR(10)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'NUMFOLIORESIDENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Folio en la cual el medico residente asistio a una interconsulta del especialista y debe ser validada por el medico especialista.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'NUMFOLIORESIDENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'NUMFOLIORESIDENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de interpretación de resultados/paraclinicos registrada por el médico especialista en el dashboard. DATETIME', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'FECHAINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda fecha de interpretación que realizan los médicos especialistas a las interconsultas desde el dashboard especialistas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'FECHAINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'FECHAINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de cabecera del trámite administrativo; puede ser NULL si la solicitud fue cancelada sin eventos. INT, FK TRAZABILIDAD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera del trámite, pueda que no tenga eventos relacionados y esto se da cuando se cancela una solicitud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del último evento registrado en el trámite de la interconsulta. INT, FK EVENTOS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkEventsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del último evento registrado al trámite', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkEventsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkEventsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de descripción de relación con VIE ERP (contract.CUPSEntityContractDescriptions) para clasificación CUPS interconsulta. INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions) - CUPS interconsulta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de RIAS CUPS aplicable cuando TIPOFACTURACION=1; NULL para otros tipos de facturación. INT, FK RIASCUPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'IDRIASCUPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuando campo TIPO FACTURACION sea 1 guardamos  el IdRiasCups a la que aplica de lo contrario queda NULL  Relacion con la tabla RIASCUPS.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'IDRIASCUPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'IDRIASCUPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de facturación: 1=RIAS, 2=Consulta Externa. Determina modelo de cobro del servicio. INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'TIPOFACTURACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'RIAS - Determina como se debe facturar el servicio:  1 - Rias  2 - Consulta Externa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'TIPOFACTURACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'TIPOFACTURACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de orden de servicio generada en Control de Cuenta que incluye esta interconsulta. INT, FK ORDEN SERVICIO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'GENSERVICEORDER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id de la orden de servicio en la cual quedo incluido el servicio de interconsulta, Este campo se llena cuando se genera la orden de servicio desde el formulario de Control de Cuenta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'GENSERVICEORDER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'GENSERVICEORDER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (identity) de la interconsulta en historia clínica. INT PRIMARY KEY', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de notificación al profesional de salud sobre la interconsulta. DATETIME', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'FECREGNOT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y hora que se realiza la notificacion al medico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'FECREGNOT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'FECREGNOT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número celular/móvil del profesional notificado de la interconsulta. CHAR(15)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'MOVPRONOT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero movil del profesional que se le notifica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'MOVPRONOT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'MOVPRONOT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud a quien se notifica la interconsulta. CHAR(20)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'CODPRONOT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del profesional al que se le notifica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'CODPRONOT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'CODPRONOT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio donde se registra la interpretación de resultados paraclinicos. NCHAR(10)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'NUMFOLINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Folio donde se Interpreta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'NUMFOLINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'NUMFOLINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del médico especialista que realiza la interpretación de resultados. CHAR(20)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'CODPROINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Medico que interpreta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'CODPROINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'CODPROINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Interpretación clínica de resultados/paraclinicos redactada por el especialista. VARCHAR(MAX), MASKED', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'INTERPRET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Interpretacion de Resultados (paraclinicos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'INTERPRET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'INTERPRET';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de mensaje de texto pendiente de enviar al profesional. BIT (0=No, 1=Sí)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'MSGPENENV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'MSJ de Texto pendiente de enviar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'MSGPENENV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'MSGPENENV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional preferido para envío de SMS; si no está disponible, se asigna profesional de turno. CHAR(20), FK INPROFSAL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'CODPROENV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional Preferido al que se le envia el MSJ de Texto.    Si al momento de realizar el envio el profesional cambia, o ya no esta disponible, se notificara al de turno.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'CODPROENV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'CODPROENV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si el reporte/resultado se envía automáticamente por SMS al profesional. BIT (0=No, 1=Sí)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'REPORAUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el reporte se hace por medio de MSJ de texto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'REPORAUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'REPORAUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de especialidad médica solicitada para la interconsulta. CHAR(3), FK INESPECIA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de auditoría/trazabilidad de la solicitud. NUMERIC(18)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código diagnóstico principal que justifica/motiva la solicitud de interconsulta. CHAR(4), FK INDIAGNOS, MASKED', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Diagnostico Principal Relacionado Motivo por el cual se solicita la interconsulta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si el servicio corresponde a plan de manejo externo/extramuros. BIT (0=No, 1=Sí)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'MANEXTPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Este Producto Corresponde a un plan de Manejo Externo?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'MANEXTPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'MANEXTPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del servicio IPS: 1=Solicitado, 2=Enviado, 3=Realizado, 4=Extramural, 5=Anulado, 6=Pendiente validación residente. CHAR(1)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'ESTSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado Servicio IPS  1: Solicitado  2: Solicitud Enviada  3: Interconsulta Realizada  4: Extramural  5: Anulado  6: Pendiente de verificación por parte del especialista, esto por el tema de los residentes.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'ESTSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'ESTSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prioridad de atención: 1=Urgente, 2=Rutina. Define temporalidad de respuesta. CHAR(1)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'PRISERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Prioridad del Servicio Solicitado  1: Urgente  2: Rutina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'PRISERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'PRISERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones clínicas adicionales de la solicitud de interconsulta. VARCHAR(2000)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'OBSSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observacion Servicio IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'OBSSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'OBSSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad/número de veces que se solicita el servicio de interconsulta. INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'CANSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Servicio IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'CANSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'CANSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CUPS (código único de procedimientos y servicios) de la interconsulta. CHAR(20), FK INCUPSIPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Unico de Procedimientos y Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación/solicitud de la orden médica de interconsulta. DATETIME', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'FECORDMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Solicitud de la Orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'FECORDMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'FECORDMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud que solicita/ordena la interconsulta. CHAR(20), FK INPROFSAL, MASKED', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de unidad funcional (servicio/departamento) que solicita la interconsulta. CHAR(10), FK INUNIFUNC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención donde se genera la solicitud de interconsulta. CHAR(10), FK ADCENATEN', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso/admisión del paciente asociado a la interconsulta. CHAR(10), FK ADINGRESO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente (cédula, documento de identidad); valor PII enmascarado. VARCHAR(25), FK INPACIENT, MASKED', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número secuencial de folio/historia clínica de la interconsulta. NCHAR(10)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código interno del tipo de historia clínica (historia de interconsulta). CHAR(9)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Órdenes médicas internas de servicios de salud (exámenes, procedimientos, laboratorio, imágenes) generadas durante un ingreso hospitalario. Registra qué servicio ordenó el profesional de salud para el paciente, la fecha, la cantidad, el estado y la trazabilidad de cada orden.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del último evento de autorización con estado Autorizado registrado para esta orden. Actualizado automáticamente en la misma transacción al guardar el evento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'AuthorizationEventId';
