CREATE TABLE [dbo].[HCORDPATO] (
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
    [INTERPRET]                     VARCHAR (4000) MASKED WITH (FUNCTION = 'partial(0, "Interpretation_Ofuscado", 0)') NULL,
    [CODPROINT]                     CHAR (20)                                                                          NULL,
    [NUMFOLINT]                     NCHAR (10)                                                                         NULL,
    [SERREAINT]                     BIT                                                                                NOT NULL,
    [INDAUDFOR]                     NUMERIC (18)                                                                       NOT NULL,
    [ESTALEPAT]                     BIT                                                                                NOT NULL,
    [FECRECEXA]                     DATETIME                                                                           NULL,
    [NOMARCPAT]                     CHAR (250)                                                                         NULL,
    [CONCURRE]                      ROWVERSION                                                                         NULL,
    [USURECEXA]                     CHAR (20)                                                                          NULL,
    [AUTO]                          INT                                                                                IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [GENSERVICEORDER]               INT                                                                                NULL,
    [RESULTADO]                     VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "Result_Ofuscado", 0)')          NULL,
    [FECHARESULT]                   DATETIME                                                                           NULL,
    [BIOSENCER]                     TINYINT MASKED WITH (FUNCTION = 'default()')                                       NULL,
    [BIOSENBACAF]                   TINYINT MASKED WITH (FUNCTION = 'default()')                                       NULL,
    [FECSENCER]                     DATETIME                                                                           NULL,
    [FECSENBACAF]                   DATETIME                                                                           NULL,
    [PROFRESULTADO]                 CHAR (20)                                                                          NULL,
    [FECRECEPMUES]                  DATETIME                                                                           NULL,
    [ESPEREALIZA]                   CHAR (3)                                                                           NULL,
    [CODSERIPSREAL]                 CHAR (20)                                                                          NULL,
    [TIPOFACTURACION]               INT                                                                                NULL,
    [IDRIASCUPS]                    INT                                                                                NULL,
    [IDDESCRIPCIONRELACIONADA]      INT                                                                                NULL,
    [CORRELACION]                   TINYINT                                                                            CONSTRAINT [DF__HCORDPATO__CORRE__7BB77E15] DEFAULT ((2)) NOT NULL,
    [OBSERVACIONCORRELA]            VARCHAR (4000)                                                                     NULL,
    [TraceabilityPaperworkEventsId] INT                                                                                NULL,
    [TraceabilityPaperworkId]       INT                                                                                NULL,
    [CAC23]                         DATE                                                                               NULL,
    [CAC24]                         DATE                                                                               NULL,
    [CAC27]                         INT                                                                                NULL,
    [CAC28]                         INT                                                                                NULL,
    [CAC139]                        INT                                                                                NULL,
    [CAC140]                        INT                                                                                NULL,
    [CAC141]                        INT                                                                                NULL,
    [SYNCMIRTH]                     BIT                                                                                NULL,
    [Laterality]                    INT                                                                                NULL,
    [Specimen]                      INT                                                                                NULL,
    [Technique]                     INT                                                                                NULL,
    [CollectionMedium]              INT                                                                                NULL,
    [AuthorizationEventId]          INT            NULL,
    CONSTRAINT [PK_HCORDPATO] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_HCORDPATO_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCORDPATO_DefinitionSamples_CollectionMedium] FOREIGN KEY ([CollectionMedium]) REFERENCES [ClinicalParameters].[DefinitionSamples] ([Id]),
    CONSTRAINT [FK_HCORDPATO_DefinitionSamples_Specimen] FOREIGN KEY ([Specimen]) REFERENCES [ClinicalParameters].[DefinitionSamples] ([Id]),
    CONSTRAINT [FK_HCORDPATO_DefinitionSamples_Technique] FOREIGN KEY ([Technique]) REFERENCES [ClinicalParameters].[DefinitionSamples] ([Id]),
    CONSTRAINT [FK_HCORDPATO_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCORDPATO].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCORDPATO].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCORDPATO].[CODDIAGNO]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCORDPATO].[INTERPRET]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCORDPATO].[RESULTADO]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCORDPATO].[BIOSENCER]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCORDPATO].[BIOSENBACAF]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');




GO
CREATE NONCLUSTERED INDEX [IX_HCORDPATO_NUMINGRES_MANEXTPRO]
    ON [dbo].[HCORDPATO]([NUMINGRES] ASC, [MANEXTPRO] ASC)
    INCLUDE([AUTO], [CANSERIPS], [CODPROINT], [CODPROSAL], [CODSERIPS], [FECORDMED], [GENSERVICEORDER], [INTERPRET], [IPCODPACI], [NUMEFOLIO], [NUMFOLINT], [OBSSERIPS], [UFUCODIGO]);


GO
CREATE NONCLUSTERED INDEX [IX_HCORDPATO]
    ON [dbo].[HCORDPATO]([NUMEFOLIO] ASC, [IPCODPACI] ASC, [NUMINGRES] ASC, [MANEXTPRO] ASC, [IDDESCRIPCIONRELACIONADA] ASC, [CODSERIPS] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_HCORDPATO_File]
    ON [dbo].[HCORDPATO]([IPCODPACI] ASC, [NUMINGRES] ASC, [NOMARCPAT] ASC);


GO
ALTER INDEX [IX_HCORDPATO_File]
    ON [dbo].[HCORDPATO] DISABLE;




GO
CREATE NONCLUSTERED INDEX [IX_HCORDPATO_DashboardMedicalOrders_CareCenter_RequestDate]
    ON [dbo].[HCORDPATO]([CODCENATE] ASC, [FECORDMED] DESC)
    INCLUDE([AUTO], [UFUCODIGO], [NUMINGRES], [NUMEFOLIO], [IPCODPACI], [CODPROSAL], [CANSERIPS], [CODSERIPS], [IDDESCRIPCIONRELACIONADA], [OBSSERIPS])
    WHERE [MANEXTPRO] = 1 AND [ESTSERIPS] <> '6' AND [FECORDMED] >= '20260102';


GO

CREATE TRIGGER [dbo].[HospitalCima_Softland_PathologyOrder] 
   ON [dbo].[HCORDPATO]
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
			   (@dataid,5,@action,GETDATE(),0,NULL)

END
--------------------------------------------------------------------------------------------------------------------
/****** Object:  Trigger [dbo].[HospitalCima_Softland_SurgicalProcedureOrder]    Script Date: 8/24/2021 8:37:34 AM ******/
SET ANSI_NULLS ON
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Medio de recolección de muestra (tubo, frasco, medio de transporte) solicitado en orden médica de patología. Referencia a maestro ClinicalParameters.DefinitionSamples, tipo INT, FK requerida.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'CollectionMedium';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Medio de recolección campo que solicita en la orden medica de patologias y valor que viene de un maestro Definicion de muestras del tipo Medio de recolección    Relacion con la tabla:  ClinicalParameters.DefinitionSamples    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'CollectionMedium';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'CollectionMedium';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Técnica de análisis patológico solicitada en orden médica (tinción, immunohistoquímica, etc). Referencia a maestro ClinicalParameters.DefinitionSamples, tipo INT, FK requerida.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'Technique';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tecnica: campo que solicita en la orden medica de patologias y valor que viene de un maestro Definicion de muestras del tipo Tecnica    Relacion con la tabla:  ClinicalParameters.DefinitionSamples      ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'Technique';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'Technique';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especimen o tipo de muestra patológica (tejido, biopsia, citología, órgano). Referencia a maestro ClinicalParameters.DefinitionSamples, tipo INT, FK requerida.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'Specimen';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especimen: campo que solicita en la orden medica de patologias y valor que viene de un maestro Definicion de muestras del tipo Especimen     Relacion con la tabla:  ClinicalParameters.DefinitionSamples      ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'Specimen';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'Specimen';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lateralidad anatómica del especimen: 0=No aplica, 1=Izquierda, 2=Derecha, 3=Bilateral, 4=Multilateral. INT, crítico para casos de mama/cérvix.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'Laterality';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'0 = No aplica   1 = Izquierda   2 = Derecha   3 = Bilateral  4 = Multilateral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'Laterality';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'Laterality';




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Control de sincronización con interfaz Mirth Connect de laboratorio. Valores: 0/NULL=Sin sincronizar, 1=Sincronizado. Solo aplica cuando estado=2 (Muestra recolectada). BIT, auditoría de integración.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'SYNCMIRTH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo que se diligencia si la interfaz de laboratorio se realiza por los canales de mirth connect, solo aplica para registros que en el momento de lectura del canal estan en estado 2 : Muestra recolectada     0 o NULL: - Sin Sincronizar  1: Sincronizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'SYNCMIRTH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'SYNCMIRTH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de cumplimiento de criterios de calidad en patología (Pregunta CAC 141). BIT, control de garantía de calidad en reporte patológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'CAC141';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'La patología cumple con lo criterios de calidad (Pregunta 141).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'CAC141';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'CAC141';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ganglios linfáticos positivos/comprometidos reportados en análisis patológico (Pregunta CAC 140). INT, estadificación oncológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'CAC140';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de ganglios positivos. (Pregunta 140).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'CAC140';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'CAC140';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número total de ganglios extraídos y reportados en informe de patología (Pregunta CAC 139). INT, criterio de calidad en procedimientos oncológicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'CAC139';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de ganglios extraídos reportados en el informe. (Pregunta 139)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'CAC139';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'CAC139';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Grado histológico de diferenciación del tumor (G1-G4, bien/moderado/poco diferenciado). INT, Pregunta CAC 28, pronóstico oncológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'CAC28';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Grado de diferenciación del tumor (pregunta 28 CAC)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'CAC28';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'CAC28';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación histológica del tumor (adenocarcinoma, carcinoma escamocelular, etc). INT, Pregunta CAC 27, tipo histológico diagnóstico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'CAC27';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Histologia del tumor (pregunta 27)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'CAC27';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'CAC27';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del informe patológico. DATE, Pregunta CAC 24. No se solicita si existe resultado en ALULA (integración externa).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'CAC24';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del informe - No se hace la pregunta si existe resultado en ALULA y el dato se saca de la tabla de integración', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'CAC24';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'CAC24';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de recolección de muestra(s) patológica(s). DATE, Pregunta CAC 23, trazabilidad de muestra.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'CAC23';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de recolección de muestra(s) (Pregunta 23).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'CAC23';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'CAC23';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cabecera del trámite administrativo de patología. INT, FK a traceability. Puede ser NULL si solicitud fue cancelada sin eventos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera del trámite, pueda que no tenga eventos relacionados y esto se da cuando se cancela una solicitud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del último evento registrado en el trámite de patología. INT, FK a eventos, auditoría de flujo de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkEventsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del último evento registrado al trámite', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkEventsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkEventsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Notas adicionales sobre la correlación clínico-patológica. VARCHAR(4000), interpretación médica enmascarada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'OBSERVACIONCORRELA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciòn de la correlacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'OBSERVACIONCORRELA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'OBSERVACIONCORRELA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de correlación clínico-patológica: 1=Sí, 2=No, 3=Sin especificar. TINYINT, criterio de calidad diagnóstica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'CORRELACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Determina si se maneja correlación :   1-Si   2-No   3-Sin especificar ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'CORRELACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'CORRELACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de descripción CUPS relacionada. INT, FK a contract.CUPSEntityContractDescriptions (VIE ERP), facturación y servicios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de código RIAS aplicable cuando TIPOFACTURACION=1. INT, FK a RIASCUPS, contratación y normativa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'IDRIASCUPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuando campo TIPO FACTURACION sea 1 guardamos  el IdRiasCups a la que aplica de lo contrario queda NULL  Relacion con la tabla RIASCUPS.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'IDRIASCUPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'IDRIASCUPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modalidad de facturación del servicio patológico: 1=RIAS, 2=Consulta Externa. INT, determinante de codificación y factura.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'TIPOFACTURACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'RIAS - Determina como se debe facturar el servicio:  1 - Rias  2 - Consulta Externa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'TIPOFACTURACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'TIPOFACTURACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de servicio CUPS realmente ejecutado en la orden patológica. CHAR(20), procedimiento realizado facturación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'CODSERIPSREAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo de servicio ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'CODSERIPSREAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'CODSERIPSREAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especialidad médica que realiza el análisis patológico. CHAR(3), anatomía patológica, citopatología, etc.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'ESPEREALIZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'ESPEREALIZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'ESPEREALIZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de recepción física de la muestra en laboratorio de patología. DATETIME, trazabilidad de cadena de custodia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'FECRECEPMUES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Recepción de la muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'FECRECEPMUES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'FECRECEPMUES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional patólogo que interpreta y firma el resultado. CHAR(20), responsable médico del informe.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'PROFRESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Profesional que devuelve el resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'PROFRESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'PROFRESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del resultado de biopsia de seno por BACAF (Biopsia por Aspiración con aguja fina). DATETIME, trazabilidad de mama.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'FECSENBACAF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de resultado de Biopsia de seno por bacaf', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'FECSENBACAF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'FECSENBACAF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del resultado de biopsia cervical/endometrial. DATETIME, trazabilidad de citología cervical.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'FECSENCER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de resultado de Biopsia Seno Cervical', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'FECSENCER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'FECSENCER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de biopsia de seno por BACAF: 1=Benigna, 2=Atípica/Indeterminada, 3=Sospecha maligna, 4=Maligna, 5=No satisfactoria, 0=No aplica. TINYINT, Bethesda-BACAF.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'BIOSENBACAF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'resultado de biopsia de seno por bacaf  1-> Benigna  2-> Atípica (Interdeterminada)  3-> Malignidad Sospechosa/Probable  4-> Maligna  5-> No Satisfactoria  0-> No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'BIOSENBACAF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'BIOSENBACAF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de biopsia cervical: 1=Negativo, 2=VPH+, 3=NIC I, 4=NIC II-III, 5=Neoplasia microinfiltrante, 6=Neoplasia infiltrante, 0=No aplica. TINYINT, Bethesda cervical.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'BIOSENCER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado de Biopsia cervical  1-> Negativo para Neoplasia  2-> Infección por VPH  3-> NIC de Bajo Grado - NIC I  4-> NIC de Alto Grado: NIC II - NIC III  5-> Neoplasia Micro infiltrante:   Escamocelular o Adenocarcinoma  6-> Neoplasia Infiltrante: Escamocelular o Adenocarcinoma  0-> No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'BIOSENCER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'BIOSENCER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de recepción del informe patológico final. DATETIME, control administrativo de entrega.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'FECHARESULT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de recepción del informe patológico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'FECHARESULT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'FECHARESULT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Texto del resultado patológico completo retornado por interfaz de laboratorio. VARCHAR(MAX), enmascarado, contiene hallazgos y diagnóstico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'RESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado de patología que devuelve la interfaz', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'RESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'RESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de orden de servicio que incluye este servicio de patología. INT, FK a órdenes generadas desde Control de Cuenta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'GENSERVICEORDER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id de la orden de servicio en la cual quedo incluido el servicio de Patologia, Este campo se llena cuando se genera la orden de servicio desde el formulario de Control de Cuenta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'GENSERVICEORDER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'GENSERVICEORDER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Autonumérico (IDENTITY) de la tabla HCORDPATO. INT, clave primaria, identificador único del registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario que registró/recibió el examen patológico. CHAR(20), auditoría de quién recibió muestra.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'USURECEXA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'USURECEXA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'USURECEXA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Timestamp de concurrencia/versión optimista. TIMESTAMP, control de actualizaciones simultáneas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'CONCURRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concurrecia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'CONCURRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'CONCURRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del archivo adjunto (reporte, imagen, documento) de patología. CHAR(250), trazabilidad documental.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'NOMARCPAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del Archivo Adjunto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'NOMARCPAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'NOMARCPAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación/registro del orden patológico en el sistema. DATETIME, auditoría de cuándo se ordenó.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'FECRECEXA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Creacion del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'FECRECEXA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'FECRECEXA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la alerta clínica relacionada con resultado patológico. BIT, flag de hallazgo crítico/urgente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'ESTALEPAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la Alerta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'ESTALEPAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'ESTALEPAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de auditoría/folio de seguimiento administrativo. NUMERIC(18), trazabilidad y control regulatorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de si el servicio requiere interfaz con laboratorio externo. BIT, 1=Sí, 0=No.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'SERREAINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el Servicio Realiza Interfaz', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'SERREAINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'SERREAINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio donde se registra la interpretación médica del resultado. NCHAR(10), referencia a documento interpretación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'NUMFOLINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Folio donde se Interpreta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'NUMFOLINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'NUMFOLINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del médico profesional que interpreta el resultado patológico. CHAR(20), responsable de diagnóstico clínico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'CODPROINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Medico que interpreta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'CODPROINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'CODPROINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Interpretación clínica de resultados paraclínicos de patología. VARCHAR(4000), enmascarada, análisis médico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'INTERPRET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Interpretacion de Resultados (paraclinicos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'INTERPRET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'INTERPRET';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CIE-10 del diagnóstico principal relacionado con hallazgo patológico. CHAR(4), enmascarado, diagnóstico linked.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Diagnostico Principal Relacionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si este producto corresponde a plan de manejo/seguimiento extramural. BIT, 1=Sí, 0=No.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'MANEXTPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Este Producto Corresponde a un plan de Manejo Externo?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'MANEXTPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'MANEXTPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del servicio patológico: 1=Solicitado, 2=Muestra recolectada, 3=Resultado entregado, 4=Examen interpretado, 5=Remitido, 6=Anulado, 7=Extramural. CHAR(1), flujo de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'ESTSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1: Solicitado 
2: Muestra Recolectada 
3: Resultado Entregado 
4: Examen Interpretado 
5: Remitido 
6: Anulado 
7: Extramural

', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'ESTSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'ESTSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prioridad de ejecución del servicio: 1=Urgente, 2=Rutina. CHAR(1), clasificación clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'PRISERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Prioridad del Servicio Solicitado  1: Urgente  2: Rutina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'PRISERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'PRISERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones clínicas adicionales en la solicitud de patología. VARCHAR(2000), notas del ordenador.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'OBSSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observacion Servicio IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'OBSSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'OBSSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad/número de servicios patológicos solicitados en esta orden. INT, múltiples análisis.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'CANSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Servicio IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'CANSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'CANSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único CUPS del procedimiento/servicio patológico solicitado. CHAR(20), identificador nacional de servicio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Unico de Procedimientos y Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de solicitud/generación de la orden médica de patología. DATETIME, cuándo se ordena.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'FECORDMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Solicitud de la Orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'FECORDMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'FECORDMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud (médico ordenador) solicitante. VARCHAR(25), enmascarado PII, responsable orden.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional donde se ordena patología (urgencias, consulta, hospitalización). CHAR(10), ubicación dentro centro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro/institución de atención de salud donde se ordena. CHAR(10), entidad prestadora.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número único del ingreso/admisión del paciente para este acto patológico. CHAR(10), vínculo a internación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificación del paciente (cédula, documento, equivalente). VARCHAR(25), enmascarado PII, clave de paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número secuencial de folio/consecutivo de la orden patológica. NCHAR(10), identificador operacional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de historia clínica/identificador interno del registro de patología. CHAR(9), clasificación administrativa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Órdenes médicas de patología e imágenes diagnósticas solicitadas durante un ingreso hospitalario o ambulatorio. Registra cada examen o procedimiento diagnóstico ordenado por un profesional de salud, incluyendo el estado, resultados, interpretaciones y trazabilidad del proceso desde la orden hasta la entrega del resultado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del último evento de autorización con estado Autorizado registrado para esta orden. Actualizado automáticamente en la misma transacción al guardar el evento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'AuthorizationEventId';
