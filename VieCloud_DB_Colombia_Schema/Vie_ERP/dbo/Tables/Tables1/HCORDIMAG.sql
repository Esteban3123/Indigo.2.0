CREATE TABLE [dbo].[HCORDIMAG] (
    [IDETIPHIS]                     CHAR (9)       NOT NULL,
    [NUMEFOLIO]                     NCHAR (10)     NOT NULL,
    [IPCODPACI]                     VARCHAR (25)   NOT NULL,
    [NUMINGRES]                     CHAR (10)      NOT NULL,
    [CODCENATE]                     CHAR (10)      NOT NULL,
    [UFUCODIGO]                     CHAR (10)      NOT NULL,
    [CODPROSAL]                     CHAR (20)      NOT NULL,
    [FECORDMED]                     DATETIME       NOT NULL,
    [CODSERIPS]                     CHAR (20)      NOT NULL,
    [CANSERIPS]                     INT            NOT NULL,
    [OBSSERIPS]                     VARCHAR (2000) NULL,
    [PRISERIPS]                     CHAR (1)       NOT NULL,
    [ESTSERIPS]                     CHAR (1)       NULL,
    [MANEXTPRO]                     BIT            NOT NULL,
    [CODDIAGNO]                     CHAR (4)       NULL,
    [INTERPRET]                     VARCHAR (4000) NULL,
    [CODPROINT]                     CHAR (20)      NULL,
    [NUMFOLINT]                     NCHAR (10)     NULL,
    [SERREAINT]                     BIT            NOT NULL,
    [INDAUDFOR]                     NUMERIC (18)   NOT NULL,
    [ESTALEIMG]                     BIT            NOT NULL,
    [FECRECEXA]                     DATETIME       NULL,
    [NOMARCIMG]                     CHAR (250)     NULL,
    [CONCURRE]                      ROWVERSION     NULL,
    [USURECEXA]                     CHAR (20)      NULL,
    [REALINOTIF]                    BIT            NOT NULL,
    [NOMRESULT]                     VARCHAR (100)  NULL,
    [SERTRANSC]                     BIT            NULL,
    [SERVALMED]                     BIT            NULL,
    [CODUSUTRA]                     CHAR (20)      NULL,
    [CODPROVAL]                     CHAR (20)      NULL,
    [FECTRASER]                     DATETIME       NULL,
    [FECVALSER]                     DATETIME       NULL,
    [ESTTRASER]                     BIT            NULL,
    [USUOCUREG]                     CHAR (20)      NULL,
    [LECTURA]                       BIT            NULL,
    [MEDREALEC]                     CHAR (20)      NULL,
    [FECHLECT]                      DATETIME       NULL,
    [GENSERVICEORDER]               INT            NULL,
    [AUTO]                          INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TIENEGRABACION]                BIT            CONSTRAINT [DF_HCORDIMAG_TIENEGRABACION] DEFAULT ((0)) NOT NULL,
    [CLASIFICABIRADS]               TINYINT        NULL,
    [FECHARESCLA]                   DATETIME       NULL,
    [ESPEREALIZA]                   CHAR (3)       NULL,
    [TIPOFACTURACION]               INT            NULL,
    [IDRIASCUPS]                    INT            NULL,
    [LATERALIDAD]                   INT            NULL,
    [FECHASUGE]                     DATETIME       NULL,
    [IDDESCRIPCIONRELACIONADA]      INT            NULL,
    [CORRELACION]                   TINYINT        CONSTRAINT [DF__HCORDIMAG__CORRE__7AC359DC] DEFAULT ((2)) NOT NULL,
    [OBSERVACIONCORRELA]            VARCHAR (4000) NULL,
    [TraceabilityPaperworkEventsId] INT            NULL,
    [TraceabilityPaperworkId]       INT            NULL,
    [RelativeURI]                   VARCHAR (500)  NULL,
    [IdAGENSALAC]                   INT            NULL,
    [IdServerOrderDetail]           INT            NULL,
    [EXMREASIT]                     BIT            CONSTRAINT [DF_EXMREASIT_Default] DEFAULT ((0)) NULL,
    [ExamProfessionalCode]          CHAR (20)      NULL,
    [ExamSpecialtyCode]             CHAR (3)       NULL,
    [ExamTakenDate]                 DATETIME       NULL,
    [SendToInterface]               INT            NULL,
    [SendToInterfaceProfessional]   CHAR (20)      NULL,
    [SendToInterfaceDate]           DATETIME       NULL,
    [CodeCareCenterProcess]         CHAR (10)      NULL,
    [ExamCompletedDate]             DATETIME       NULL,
    [AttendingProfessionalCode]     CHAR (20)      NULL,
    [AttendingSpecialty]            CHAR (3)       NULL,
    [RoutedFrom]                    TINYINT        NULL,
    [Principal] BIT NULL,
    [AuthorizationEventId]          INT            NULL,
    CONSTRAINT [PK_HCORDIMAG] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_CodeCareCenterProcess] FOREIGN KEY ([CodeCareCenterProcess]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCORDIMAG_INESPECIA] FOREIGN KEY ([ExamSpecialtyCode]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_HCORDIMAG_INPROFSAL] FOREIGN KEY ([MEDREALEC]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_IdServerOrderDetail_HCORDIMAG] FOREIGN KEY ([IdServerOrderDetail]) REFERENCES [Billing].[ServiceOrderDetail] ([Id]),
    CONSTRAINT [FK_INPROFSAL_HCORDIMAG] FOREIGN KEY ([ExamProfessionalCode]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_INPROFSAL_HCORDIMAG_SendToInterfaceProfessional] FOREIGN KEY ([SendToInterfaceProfessional]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCORDIMAG].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCORDIMAG].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCORDIMAG].[CODDIAGNO]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCORDIMAG].[INTERPRET]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');




GO

CREATE NONCLUSTERED INDEX [IX_HCORDIMAG_CODCENATE]
    ON [dbo].[HCORDIMAG]([CODCENATE] ASC)
    INCLUDE([FECTRASER]);


GO
ALTER INDEX [IX_HCORDIMAG_CODCENATE]
    ON [dbo].[HCORDIMAG] DISABLE;




GO
CREATE NONCLUSTERED INDEX [IX_HCORDIMAG_CODSERIPS_MANEXTPRO]
    ON [dbo].[HCORDIMAG]([CODSERIPS] ASC, [MANEXTPRO] ASC)
    INCLUDE([CANSERIPS], [ESTSERIPS], [IPCODPACI], [NUMINGRES]);


GO
CREATE NONCLUSTERED INDEX [IX_HCORDIMAG_CODCENATE_ESTSERIPS_NUMEFOLIO_IPCODPACI_NUMINGRES_CODPROSAL_FECORDMED_CODSERIPS_SERREAINT_NOMARCIMG]
    ON [dbo].[HCORDIMAG]([CODCENATE] ASC, [ESTSERIPS] ASC)
    INCLUDE([NUMEFOLIO], [IPCODPACI], [NUMINGRES], [CODPROSAL], [FECORDMED], [CODSERIPS], [SERREAINT], [NOMARCIMG]);


GO
CREATE NONCLUSTERED INDEX [IX_HCORDIMAG_File]
    ON [dbo].[HCORDIMAG]([IPCODPACI] ASC, [NUMINGRES] ASC, [NOMARCIMG] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_HCORDIMAG_CODPROSAL_MANEXTPRO]
    ON [dbo].[HCORDIMAG]([CODPROSAL] ASC, [MANEXTPRO] ASC)
    INCLUDE([AUTO], [CANSERIPS], [CODPROINT], [CODSERIPS], [NUMINGRES], [OBSSERIPS], [UFUCODIGO], [FECORDMED], [GENSERVICEORDER], [INTERPRET], [IPCODPACI], [NUMEFOLIO], [NUMFOLINT]);


GO
CREATE NONCLUSTERED INDEX [IX_HCORDIMAG]
    ON [dbo].[HCORDIMAG]([NUMEFOLIO] ASC, [IPCODPACI] ASC, [NUMINGRES] ASC, [MANEXTPRO] ASC, [IDDESCRIPCIONRELACIONADA] ASC, [CODSERIPS] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_HCORDIMAG_CODCENATE_SERTRANSC_SERVALMED_ESTTRASER]
    ON [dbo].[HCORDIMAG]([CODCENATE] ASC, [SERTRANSC] ASC, [SERVALMED] ASC, [ESTTRASER] ASC)
    INCLUDE([CANSERIPS], [CODDIAGNO], [CODPROSAL], [CODSERIPS], [CONCURRE], [ESTALEIMG], [ESTSERIPS], [FECORDMED], [IPCODPACI], [NUMEFOLIO], [NUMINGRES], [OBSSERIPS], [SERREAINT], [UFUCODIGO]);


GO
CREATE NONCLUSTERED INDEX [IX_HCORDIMAG_CODCENATE_FECVALSER]
    ON [dbo].[HCORDIMAG]([CODCENATE] ASC)
    INCLUDE([FECVALSER]);


GO
ALTER INDEX [IX_HCORDIMAG_CODCENATE_FECVALSER]
    ON [dbo].[HCORDIMAG] DISABLE;




GO
CREATE NONCLUSTERED INDEX [_dta_index_HCORDIMAG_6_1607064861__K5_K8_K9_K15_K3_K4_K22]
    ON [dbo].[HCORDIMAG]([CODCENATE] ASC, [FECORDMED] ASC, [CODSERIPS] ASC, [CODDIAGNO] ASC, [IPCODPACI] ASC, [NUMINGRES] ASC, [FECRECEXA] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_HCORDIMAG_DashboardMedicalOrders_CareCenter_RequestDate]
    ON [dbo].[HCORDIMAG]([CODCENATE] ASC, [FECORDMED] DESC)
    INCLUDE([AUTO], [UFUCODIGO], [NUMINGRES], [NUMEFOLIO], [IPCODPACI], [CODPROSAL], [CANSERIPS], [CODSERIPS], [IDDESCRIPCIONRELACIONADA], [OBSSERIPS])
    WHERE [MANEXTPRO] = 1 AND [ESTSERIPS] <> '6' AND [FECORDMED] >= '20260102';


GO
CREATE NONCLUSTERED INDEX [IX_HCORDIMAG_NUMINGRES_MANEXTPRO]
    ON [dbo].[HCORDIMAG]([NUMINGRES] ASC, [MANEXTPRO] ASC)
    INCLUDE([CANSERIPS], [CODSERIPS], [ESTSERIPS], [IPCODPACI]);

GO

CREATE NONCLUSTERED INDEX IX_HCORDIMAG_Paciente_Ingreso_Estado_Cover
ON dbo.HCORDIMAG
(
    IPCODPACI,
    NUMINGRES,
    ESTSERIPS,
    MANEXTPRO
)
INCLUDE
(
    AUTO,
    CODSERIPS,
    NUMEFOLIO,
    CODCENATE,
    FECORDMED,
    IDETIPHIS,
    NOMARCIMG,
    SERREAINT,
    IDDESCRIPCIONRELACIONADA
); 



GO
CREATE TRIGGER [dbo].[HospitalCima_Softland_ImageOrder] 
   ON dbo.HCORDIMAG
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
			   (@dataid,4,@action,GETDATE(),0,NULL)

END


--------------------------------------------------------------------------------------------------------------------
/****** Object:  Trigger [dbo].[HospitalCima_Softland_PathologyOrder]    Script Date: 8/24/2021 8:37:34 AM ******/
SET ANSI_NULLS ON
GO
CREATE TRIGGER [dbo].[EstadoElectros] 
   ON  dbo.HCORDIMAG 
   AFTER INSERT
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for trigger here
	declare @codserips as varchar(20) = ''
	declare @AUto as integer
	
	select top 1 @codserips = CODSERIPS , @AUto =AUTO from inserted where CODSERIPS = '895100'

	if @codserips <> '' begin
		update HCORDIMAG set ESTSERIPS = 3 where AUTO = @AUto  --04-04-2023 3:46pm Edna desde casanare, solicita hacer el cambio de pasarlo a ( 3 ), antes pasaba a estado ( 2 )
	end

/*
Estado Servicio IPS de Imágenes 
	1: Solicitado
	2: Estudio Realizado
	3: Imagen Procesada
	4: Estudio Interpretado
	5: Remitido
	6: Anulado
	7: Extramural
*/
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de especialidad del profesional asistente registrado en AttendingProfessionalCode. Especialización médica del profesional que atiende el paciente durante la toma o validación del estudio imagenológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'AttendingSpecialty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la especialidad del profesional registrado como profesional que asiste y guardado el la columna AttendingProfessionalCode', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'AttendingSpecialty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'AttendingSpecialty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador del profesional de salud registrado como asistente o responsable de la atención durante el examen. Referencia a INPROFSAL.CODPROSAL, médico especialista o profesional sanitario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'AttendingProfessionalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el codigo del profesional el cual fue registrado como profesional que asiste', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'AttendingProfessionalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'AttendingProfessionalCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención donde se procesó la imagen desde el sistema Aquila. Centro médico, institución o IPS sede de realización del procedimiento imagenológico. Referencia a ADCENATEN.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'CodeCareCenterProcess';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del centro de atencion donde se proceso la imagen desde Aquila ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'CodeCareCenterProcess';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'CodeCareCenterProcess';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador binario (0=No, 1=Sí) que identifica si el examen se realizó en sitio, según la selección del médico en historia clínica. Especifica localización de toma del estudio diagnóstico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'EXMREASIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indetifica si el examen se realiza en sitio, esto es lo que el medico termina seleccionando en su historia clinica.
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'EXMREASIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'EXMREASIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se determinó si el registro se envía a interfaz Indira o no. Usado para ecografías y estudios que no se procesan directamente en RIS, evitando envíos innecesarios. Timestamp de decisión de enrutamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'SendToInterfaceDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'19-05-2025
 


Se crea campo para indentificar la fecha en la cual se determino la interfaz  de Indira o no, esto por el tema de ecografias que algunas no se procesan directamente desde la interfaz y por ende no debe llegar el registro a Indira.


', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'SendToInterfaceDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'SendToInterfaceDate';










GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional que determinó si el servicio se envía a interfaz Indira. Profesional responsable de la decisión de enrutamiento en modalidades que requieren selección manual. Referencia a INPROFSAL.CODPROSAL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'SendToInterfaceProfessional';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'19-05-2025
 


Se crea campo para indentificar el profesional que determino la interfaz  de Indira o no, esto por el tema de ecografias que algunas no se procesan directamente desde la interfaz y por ende no debe llegar el registro a Indira.


', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'SendToInterfaceProfessional';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'SendToInterfaceProfessional';










GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de enrutamiento a interfaz RIS Indira (0=Espera de enrutamiento, 1=Se envía a Indira, 2=Se procesa en VieCloud). Controla si el registro debe visualizarse en dashboard imagenología o enviarse a RIS. Crítico para Indira y flujo de procesamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'SendToInterface';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'19-05-2025
 


Se crea campo para indentificar si el registro o servicio se envia a la interfaz de Indira o no, esto por el tema de ecografias que algunas no se procesan directamente desde la interfaz y por ende no debe llegar el registro a Indira.

Indira debe hacer lectura de este campo para identificar los registros que se deben listar en sus dashboard.

0- En espera de ser enrutado
1-  Se envia a la interfaz  de Indira

2 - No se envia a la interfaz de Indira se queda en VIE 

Resumen:

Si la interfaz RIS Indira está activa, se debe validar la modalidad del servicio (según el CUPS de la orden) y actuar de acuerdo con su configuración:

Si la modalidad está configurada como “Requiere enrutamiento”, el servicio debe visualizarse en el formulario “Enrutador de imágenes intrahospitalarias”, desde donde se realizará el enrutamiento manual correspondiente.

Si la modalidad está configurada como “Procesamiento en interfaz”, el servicio debe enviarse automáticamente a la interfaz RIS Indira activa.

Si la modalidad está configurada como “Procesamiento en VieCloud”, el servicio debe visualizarse directamente en el Dashboard de Imagenología Tecnólogo, sin pasar por la interfaz.



', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'SendToInterface';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'SendToInterface';










GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de inicio del procesamiento registrada en dashboard imagenología tecnólogo al tomar el examen. Marca inicio de captura del estudio diagnóstico (radiografía, ecografía, mamografía, etc.).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'ExamTakenDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la fecha que se registra como fecha de inicio del procesamiento en el dashboard de imagenologia tecnologo al momento de tomar el examen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'ExamTakenDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'ExamTakenDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de especialidad seleccionada del profesional al momento de tomar el examen en dashboard imagenología tecnólogo. Especialidad asociada al servicio solicitado. Referencia a INESPECIA.CODESPECI.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'ExamSpecialtyCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el codigo de la especialidad seleccionada del profesional seleccionado al momento de tomar el examen en el dashboar de imagenologia tecnologo en examenes solicitados', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'ExamSpecialtyCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'ExamSpecialtyCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional seleccionado al tomar el examen en dashboard imagenología tecnólogo. Profesional responsable de la adquisición del estudio imagenológico (tecnólogo, técnico). Referencia a INPROFSAL.CODPROSAL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'ExamProfessionalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el codigo del profesional seleccionado al momento de tomar el examen en el dashboar de imagenologia tecnologo en examenes solicitados', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'ExamProfessionalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'ExamProfessionalCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de relación con tabla Billing.ServiceOrderDetail. Vincula el registro de imagen con detalles de la orden de servicio facturación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'IdServerOrderDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relación con la tabla ServiceOrderDetail', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'IdServerOrderDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'IdServerOrderDetail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador de sala/ambiente donde se realiza toma de imagen diagnóstica. Permitido NULL cuando según modalidad no se requiere. Se asigna automáticamente al terminar historia clínica validando modalidad del servicio CUPS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'IdAGENSALAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'24-05-2023: Columna que identifica la sala en la cual se realiza la toma de la imagen diagnostica.  (pbi: 10652)    En el Dashboard Imagenología tecnólogo dependiendo de la modalidad se solicita ó no la sala, por ello el campo permite NULL            

15-07-2025: Se realiza filtración de la modalidad asociada al servicio. Con esta modalidad, se verifica si alguna sala dispone de la misma. En caso afirmativo, se asigna el valor de dicha sala al campo correspondiente en la tabla HCORDIMAG automaticamente cuando se termina la historia clinica.

', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'IdAGENSALAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'IdAGENSALAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ruta relativa web o física donde se aloja el recurso imagenológico. Ejemplo: /study/7/4 en Indira. URL base se guarda en tabla de contenedores. Permite localizar el archivo de imagen almacenado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'RelativeURI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'La URI Relativa se usa para identificar la ruta web o fisica en la que se encuentra el recurso, en este caso la imagen.    Ejemplo: URIRealtiva de imagen en indira: /study/7/4  La URL Base del recurso se guarda en la tabla de contenedores', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'RelativeURI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'RelativeURI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cabecera del trámite en trazabilidad. Puede ser NULL si no hay eventos asociados (solicitud cancelada). Vincula historial administrativo del procedimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera del trámite, pueda que no tenga eventos relacionados y esto se da cuando se cancela una solicitud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del último evento registrado en el trámite. Rastrea estado más reciente de la solicitud imagenológica en cadena de procesamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkEventsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del último evento registrado al trámite', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkEventsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkEventsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observación o nota adicional sobre la correlación clínica del estudio. Texto descriptivo para fundamentar decisión de correlación con estudios previos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'OBSERVACIONCORRELA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciòn de la correlacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'OBSERVACIONCORRELA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'OBSERVACIONCORRELA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de manejo de correlación con estudios previos (1=Sí correlaciona, 2=No correlaciona, 3=Sin especificar). Determina si se comparan con imaging anterior.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'CORRELACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Determina si se maneja correlación :   1-Si   2-No   3-Sin especificar ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'CORRELACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'CORRELACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de descripción de relación con tabla VIE ERP (contract.CUPSEntityContractDescriptions). Liga servicio CUPS a contrato y descripción comercial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha sugerida para la toma de la imagen diagnóstica. Referencia temporal recomendada para realización del procedimiento imagenológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'FECHASUGE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que guarda la fecha sugerida para la toma de la imagen DX.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'FECHASUGE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'FECHASUGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de lateralidad anatómica (0=No aplica, 1=Izquierda, 2=Derecha, 3=Bilateral, 4=Multilateral). Especifica localización anatómica del estudio (mamografía, radiografía ósea, etc.).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'LATERALIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo de Lateralidad:   0 - No aplica  1 - Izquierda  2 - Derecha  3 - Bilateral  4 - Multilateral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'LATERALIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'LATERALIDAD';




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de CUPS-RIAS aplicable cuando TIPOFACTURACION=1. Referencia a RIASCUPS para facturación bajo régimen RIAS. NULL si facturación es consulta externa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'IDRIASCUPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuando campo TIPO FACTURACION sea 1 guardamos  el IdRiasCups a la que aplica de lo contrario queda NULL  Relacion con la tabla RIASCUPS.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'IDRIASCUPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'IDRIASCUPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modalidad de facturación del servicio (1=RIAS, 2=Consulta Externa). Determina esquema tarifario y normativa de cobro del procedimiento imagenológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'TIPOFACTURACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'RIAS - Determina como se debe facturar el servicio:  1 - Rias  2 - Consulta Externa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'TIPOFACTURACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'TIPOFACTURACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de especialidad que realizó el estudio (3 caracteres). Especialización técnica o médica responsable de adquisición del examen diagnóstico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'ESPEREALIZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especialidad Realizó', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'ESPEREALIZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'ESPEREALIZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de resultado de clasificación BI-RADS o categorización diagnóstica. Timestamp cuando se completa evaluación de hallazgos en estudios mamarios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'FECHARESCLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Resultado de Clasificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'FECHARESCLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'FECHARESCLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calificación BI-RADS (Breast Imaging-Reporting And Data System) para mamografía (0-6: desde necesidad nuevo estudio hasta malignidad confirmada). Estandarización internacional de reporte mamario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'CLASIFICABIRADS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la calificación BIRADS:  1- BIRADS 0: Necesidad de Nuevo Estudio Imagenológico o Mamograma previo para evaluación  2- BIRADS 1: Negativo  3- BIRADS 2: Hallazgos Benignos  4- BIRADS 3: Probablemente Benigno  5- BIRADS 4: Anormalidad Sospechosa  6- BIRADS 5: Altamente Sospechoso de Malignidad  7- BIRADS 6: Malignidad por Biopsia ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'CLASIFICABIRADS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'CLASIFICABIRADS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador binario que define si el servicio tiene grabaciones para pasar a pestaña transcripción. 0=Sin grabación, 1=Tiene grabación. Flag de disponibilidad de material multimedia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'TIENEGRABACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que define si el servicio ya tiene grabaciones para pasar a la pestaña de trascripción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'TIENEGRABACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'TIENEGRABACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo único (IDENTITY). Clave primaria de la tabla, generador secuencial para cada registro de orden imagenológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de orden de servicio en la cual se incluyó el servicio imagenológico. Se completa cuando se genera orden desde formulario Control de Cuenta. Referencia a facturación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'GENSERVICEORDER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id de la orden de servicio en la cual quedo incluido el servicio de Imagenologia, Este campo se llena cuando se genera la orden de servicio desde el formulario de Control de Cuenta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'GENSERVICEORDER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'GENSERVICEORDER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se realizó la lectura/interpretación del examen por profesional. Timestamp de validación diagnóstica del estudio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'FECHLECT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha que se realizó la lectura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'FECHLECT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'FECHLECT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del médico/profesional que realizó la lectura interpretativa del estudio. Radiólogo o especialista validador. Referencia a INPROFSAL.CODPROSAL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'MEDREALEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Medico que realiza la lectura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'MEDREALEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'MEDREALEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador binario de lectura (0=Pendiente, 1=Realizada). Flag que señala si el estudio ya cuenta con interpretación profesional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'LECTURA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para saber si ya se hizo lectura del servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'LECTURA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'LECTURA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario que ocupa/posee el registro en el sistema. Usuario responsable del trámite actual o que lo abrió.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'USUOCUREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que ocupa el registro ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'USUOCUREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'USUOCUREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de transcripción del servicio imagenológico (0=Pendiente, 1=Transcrito). Indica si se documentó en texto la lectura del estudio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'ESTTRASER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la Transcripcion del Servicio de Imagenologia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'ESTTRASER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'ESTTRASER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de validación de la transcripción por profesional supervisor. Timestamp de revisión y aprobación del documento transcrito.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'FECVALSER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Validacion de la Transcripcion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'FECVALSER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'FECVALSER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de transcripción de la imagen/interpretación. Momento en que se documentó por escrito el resultado del estudio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'FECTRASER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Transcripcion del la Imagen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'FECTRASER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'FECTRASER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional que valida/aprueba la transcripción. Médico supervisor responsable de revisión final. Referencia a INPROFSAL.CODPROSAL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'CODPROVAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional que valida la Transcripcion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'CODPROVAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'CODPROVAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario que realiza la transcripción del estudio imagenológico. Operario responsable de documentación escrita.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'CODUSUTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Usuario que Transcribe', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'CODUSUTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'CODUSUTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador binario de validación médica (0=No validado, 1=Validado). Señala si la transcripción fue aprobada por profesional supervisador.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'SERVALMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece si la Transcripcion Fue ya Validada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'SERVALMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'SERVALMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador binario de transcripción (0=Pendiente, 1=Transcrito). Especifica si el servicio fue transcrito por tecnólogo o radiólogo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'SERTRANSC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece si el Servicio ya se le realizo la Lectura por el Tecnologo o Radiologo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'SERTRANSC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'SERTRANSC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del archivo de resultado o documento final generado. Referencia a archivo almacenado con interpretación del estudio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'NOMRESULT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del Archivo del Resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'NOMRESULT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'NOMRESULT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de sincronización con RISTRACAB y RISTRADED (0=Pendiente, 1=Realizada). Flag de notificación a sistemas de radicación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'REALINOTIF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'especifica si ya realizo la sincronizacion con ristracab y ristraded', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'REALINOTIF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'REALINOTIF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador del usuario que recibe/registra el examen en el sistema. Operario responsable de ingreso del registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'USURECEXA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'USURECEXA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'USURECEXA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo de concurrencia (TIMESTAMP SQL Server). Usado para control de versiones optimista en actualizaciones concurrentes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'CONCURRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concurrencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'CONCURRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'CONCURRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del archivo adjunto de imagen (hasta 250 caracteres). Referencia al fichero de estudio radiológico, ecográfico, etc.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'NOMARCIMG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del Archivo Adjunto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'NOMARCIMG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'NOMARCIMG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación del registro en la tabla. Timestamp de ingreso inicial del servicio imagenológico al sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'FECRECEXA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Creacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'FECRECEXA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'FECRECEXA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de estado de alerta (0=Sin alerta, 1=Alerta activa). Flag para señalar hallazgos críticos o urgentes en el estudio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'ESTALEIMG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la Alerta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'ESTALEIMG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'ESTALEIMG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de auditoría numérico (18 dígitos). Identificador de control y verificación para auditoría interna.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de interfaz del servicio (0=Sin interfaz, 1=Con interfaz). Especifica si el servicio requiere envío a RIS externo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'SERREAINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el Servicio Realiza Interfaz', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'SERREAINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'SERREAINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio donde se realiza la interpretación del estudio. Referencia cruzada a documento de resultado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'NUMFOLINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Folio donde se Interpreta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'NUMFOLINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'NUMFOLINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional médico que interpreta el estudio (radiólogo, especialista). Referencia a INPROFSAL.CODPROSAL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'CODPROINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Medico que interpreta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'CODPROINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'CODPROINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Texto de interpretación de resultados para servicios paraclínicos (hasta 4000 caracteres, MASKED). Descripción clínica de hallazgos, diagnósticos y recomendaciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'INTERPRET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Interpretacion de Resultados (paraclinicos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'INTERPRET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'INTERPRET';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de diagnóstico principal asociado al estudio (4 caracteres, MASKED). Diagnóstico CIE-10 o local vinculado al hallazgo imagenológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Diagnostico Principal Relacionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador binario (0=No, 1=Sí) de si el estudio corresponde a plan de manejo externo. Flag de procedimiento derivado o coordinado con tercero.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'MANEXTPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Este Estudio Corresponde a un plan de Manejo Externo?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'MANEXTPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'MANEXTPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del servicio IPS (1=Solicitado, 2=Estudio realizado, 3=Imagen procesada, 4=Estudio interpretado, 5=Remitido, 6=Anulado, 7=Extramural, 8=Antes examen Indira, 9=Realizando). Flujo de procesamiento del servicio en sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'ESTSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado Servicio IPS  
1: Solicitado (cuando se realiza la historia clinica o desde el control de servicios ambulatorios)
2: Estudio Realizado (Toma del estudio desde la pestaña Examenes solicitados del dashboard imagenología tecnologo)
3: Imagen Procesada (Al momento de confirmar el examen desde la pestaña Examenes pendientes resultado del dashboard imagenologia tecnologo) 
4: Estudio Interpretado  (Cuando se realiza la interpretación desde el page de paraclinicos en la historia clinica)
5: Remitido (Cuando es trasladado a otro centro) 
6: Anulado 
7: Extramural
8: Antes examen realizado indira 
9: Realizando (Estado nuevo 15-05-2025 para integración de estados con INDIRA ó cualquier RIS que tenga este estado, recordar que normativamente se debe tener)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'ESTSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'ESTSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prioridad del servicio solicitado (1=Urgente, 2=Rutina). Nivel de urgencia del procedimiento imagenológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'PRISERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Prioridad del Servicio Solicitado  1: Urgente  2: Rutina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'PRISERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'PRISERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observación o nota adicional del servicio IPS (hasta 2000 caracteres). Comentarios clínicos o administrativos relevantes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'OBSSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observacion Servicio IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'OBSSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'OBSSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de servicios IPS solicitados (entero). Número de unidades del procedimiento a realizar.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'CANSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Servicio IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'CANSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'CANSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único de procedimiento y servicio (CUPS o código local). Identificador normalizado del tipo de estudio imagenológico (mamografía, ecografía, tomografía, etc.).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Unico de Procedimientos y Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de solicitud de la orden médica. Timestamp cuando el profesional genera la requisición del estudio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'FECORDMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Solicitud de la Orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'FECORDMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'FECORDMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud que solicita el estudio (MASKED). Médico tratante responsable del pedido. Referencia a INPROFSAL.CODPROSAL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de unidad funcional (servicio, departamento, área). Organización interna donde se origina la solicitud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención (IPS, institución, sede). Establecimiento de salud responsable de la atención. Referencia a ADCENATEN.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso o atención del paciente. Identificador del episodio asistencial vinculado al estudio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador del paciente (VARCHAR 25, MASKED). Cédula, documento o identificación única del paciente. Dato sensible PII.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio o consecutivo de la historia clínica. Referencia secuencial del documento médico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre interno de tipo de historia clínica. Clasificación o categoría de registro médico (HCP, HCH, etc.).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Origen del enrutamiento de la imagen (1=Historia clínica, 2=Enrutador de imágenes intrahospitalarias). Indica fuente de orden para procesamiento RIS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'RoutedFrom';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que me indica desde donde fue enrutada la imagen.
1 - Historia clínica
2 - Enrutador de imágenes intrahospitalarias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'RoutedFrom';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'RoutedFrom';


GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Indica cual es la imagen dx principal de la atención - Solo aplica para unidad de urgencias',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'HCORDIMAG',
    @level2type = N'COLUMN',
    @level2name = N'Principal'


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Órdenes de imágenes diagnósticas (radiología, ecografías, tomografías, resonancias y otros estudios de imagen) solicitadas dentro de la historia clínica. Registra la solicitud, el estado, la interpretación, la auditoría y el seguimiento completo de cada examen de imagen por paciente e ingreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que el examen de imagen fue completado o finalizado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'ExamCompletedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'ExamCompletedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del último evento de autorización con estado Autorizado registrado para esta orden. Actualizado automáticamente en la misma transacción al guardar el evento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'AuthorizationEventId';
