CREATE TABLE [dbo].[HCORDIMAG] (
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
    [ESTALEIMG]                     BIT                                                                                NOT NULL,
    [FECRECEXA]                     DATETIME                                                                           NULL,
    [NOMARCIMG]                     CHAR (250)                                                                         NULL,
    [CONCURRE]                      ROWVERSION                                                                         NULL,
    [USURECEXA]                     CHAR (20)                                                                          NULL,
    [REALINOTIF]                    BIT                                                                                NOT NULL,
    [NOMRESULT]                     VARCHAR (100)                                                                      NULL,
    [SERTRANSC]                     BIT                                                                                NULL,
    [SERVALMED]                     BIT                                                                                NULL,
    [CODUSUTRA]                     CHAR (20)                                                                          NULL,
    [CODPROVAL]                     CHAR (20)                                                                          NULL,
    [FECTRASER]                     DATETIME                                                                           NULL,
    [FECVALSER]                     DATETIME                                                                           NULL,
    [ESTTRASER]                     BIT                                                                                NULL,
    [USUOCUREG]                     CHAR (20)                                                                          NULL,
    [LECTURA]                       BIT                                                                                NULL,
    [MEDREALEC]                     CHAR (20)                                                                          NULL,
    [FECHLECT]                      DATETIME                                                                           NULL,
    [GENSERVICEORDER]               INT                                                                                NULL,
    [AUTO]                          INT                                                                                IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TIENEGRABACION]                BIT                                                                                CONSTRAINT [DF_HCORDIMAG_TIENEGRABACION] DEFAULT ((0)) NOT NULL,
    [CLASIFICABIRADS]               TINYINT                                                                            NULL,
    [FECHARESCLA]                   DATETIME                                                                           NULL,
    [ESPEREALIZA]                   CHAR (3)                                                                           NULL,
    [TIPOFACTURACION]               INT                                                                                NULL,
    [IDRIASCUPS]                    INT                                                                                NULL,
    [LATERALIDAD]                   INT                                                                                NULL,
    [FECHASUGE]                     DATETIME                                                                           NULL,
    [IDDESCRIPCIONRELACIONADA]      INT                                                                                NULL,
    [CORRELACION]                   TINYINT                                                                            CONSTRAINT [DF__HCORDIMAG__CORRE__7AC359DC] DEFAULT ((2)) NOT NULL,
    [OBSERVACIONCORRELA]            VARCHAR (4000)                                                                     NULL,
    [TraceabilityPaperworkEventsId] INT                                                                                NULL,
    [TraceabilityPaperworkId]       INT                                                                                NULL,
    [RelativeURI]                   VARCHAR (500)                                                                      NULL,
    [IdAGENSALAC]                   INT                                                                                NULL,
    [IdServerOrderDetail]           INT                                                                                NULL,
    [ExamProfessionalCode]          CHAR (20)                                                                          NULL,
    [ExamSpecialtyCode]             CHAR (3)                                                                           NULL,
    [ExamTakenDate]                 DATETIME                                                                           NULL,
    [SendToInterface]               INT                                                                                NULL,
    [SendToInterfaceProfessional]   CHAR (20)                                                                          NULL,
    [SendToInterfaceDate]           DATETIME                                                                           NULL,
    [EXMREASIT]                     BIT                                                                                CONSTRAINT [DF_EXMREASIT_Default] DEFAULT ((0)) NULL,
    [CodeCareCenterProcess]         CHAR (10)                                                                          NULL,
    [ExamCompletedDate]             DATETIME                                                                           NULL,
    [AttendingProfessionalCode]     CHAR (20)                                                                          NULL,
    [AttendingSpecialty]            CHAR (3)                                                                           NULL,
    CONSTRAINT [PK_HCORDIMAG] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_CodeCareCenterProcess] FOREIGN KEY ([CodeCareCenterProcess]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCORDIMAG_INESPECIA] FOREIGN KEY ([ExamSpecialtyCode]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_HCORDIMAG_INPROFSAL] FOREIGN KEY ([MEDREALEC]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_IdServerOrderDetail_HCORDIMAG] FOREIGN KEY ([IdServerOrderDetail]) REFERENCES [Billing].[ServiceOrderDetail] ([Id]),
    CONSTRAINT [FK_INPROFSAL_HCORDIMAG] FOREIGN KEY ([ExamProfessionalCode]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_INPROFSAL_HCORDIMAG_SendToInterfaceProfessional] FOREIGN KEY ([SendToInterfaceProfessional]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL])
);


GO
ALTER TABLE [dbo].[HCORDIMAG] NOCHECK CONSTRAINT [FK_HCORDIMAG_INPROFSAL];


GO
CREATE NONCLUSTERED INDEX [IX_HCORDIMAG_CODCENATE]
    ON [dbo].[HCORDIMAG]([CODCENATE] ASC)
    INCLUDE([FECTRASER]);


GO
CREATE NONCLUSTERED INDEX [IX_HCORDIMAG_CODCENATE_ESTSERIPS_NUMEFOLIO_IPCODPACI_NUMINGRES_CODPROSAL_FECORDMED_CODSERIPS_SERREAINT_NOMARCIMG]
    ON [dbo].[HCORDIMAG]([CODCENATE] ASC, [ESTSERIPS] ASC)
    INCLUDE([NUMEFOLIO], [IPCODPACI], [NUMINGRES], [CODPROSAL], [FECORDMED], [CODSERIPS], [SERREAINT], [NOMARCIMG]);


GO
CREATE NONCLUSTERED INDEX [IX_HCORDIMAG_CODCENATE_SERTRANSC_SERVALMED_ESTTRASER]
    ON [dbo].[HCORDIMAG]([CODCENATE] ASC, [SERTRANSC] ASC, [SERVALMED] ASC, [ESTTRASER] ASC)
    INCLUDE([CANSERIPS], [CODDIAGNO], [CODPROSAL], [CODSERIPS], [CONCURRE], [ESTALEIMG], [ESTSERIPS], [FECORDMED], [IPCODPACI], [NUMEFOLIO], [NUMINGRES], [OBSSERIPS], [SERREAINT], [UFUCODIGO]);


GO
CREATE NONCLUSTERED INDEX [IX_HCORDIMAG_CODCENATE_FECVALSER]
    ON [dbo].[HCORDIMAG]([CODCENATE] ASC)
    INCLUDE([FECVALSER]);


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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la especialidad del profesional registrado como profesional que asiste y guardado el la columna AttendingProfessionalCode', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'AttendingSpecialty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el codigo del profesional el cual fue registrado como profesional que asiste', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'AttendingProfessionalCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo del centro de atencion donde se proceso la imagen desde Aquila ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'CodeCareCenterProcess';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indetifica si el examen se realiza en sitio, esto es lo que el medico termina seleccionando en su historia clinica.
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'EXMREASIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'19-05-2025
 


Se crea campo para indentificar la fecha en la cual se determino la interfaz  de Indira o no, esto por el tema de ecografias que algunas no se procesan directamente desde la interfaz y por ende no debe llegar el registro a Indira.


', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'SendToInterfaceDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'19-05-2025
 


Se crea campo para indentificar el profesional que determino la interfaz  de Indira o no, esto por el tema de ecografias que algunas no se procesan directamente desde la interfaz y por ende no debe llegar el registro a Indira.


', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'SendToInterfaceProfessional';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'19-05-2025
 


Se crea campo para indentificar si el registro o servicio se envia a la interfaz de Indira o no, esto por el tema de ecografias que algunas no se procesan directamente desde la interfaz y por ende no debe llegar el registro a Indira.

Indira debe hacer lectura de este campo para identificar los registros que se deben listar en sus dashboard.

0- En espera de ser enrutado
1-  Se envia a la interfaz  de Indira

2 - No se envia a la interfaz de Indira se queda en VIE 


', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'SendToInterface';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la fecha que se registra como fecha de inicio del procesamiento en el dashboard de imagenologia tecnologo al momento de tomar el examen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'ExamTakenDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el codigo de la especialidad seleccionada del profesional seleccionado al momento de tomar el examen en el dashboar de imagenologia tecnologo en examenes solicitados', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'ExamSpecialtyCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el codigo del profesional seleccionado al momento de tomar el examen en el dashboar de imagenologia tecnologo en examenes solicitados', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'ExamProfessionalCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con la tabla ServiceOrderDetail', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'IdServerOrderDetail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'24-05-2023: Columna que identifica la sala en la cual se realiza la toma de la imagen diagnostica.  (pbi: 10652)    En el Dashboard Imagenología tecnólogo dependiendo de la modalidad se solicita ó no la sala, por ello el campo permite NULL            

15-07-2025: Se realiza filtración de la modalidad asociada al servicio. Con esta modalidad, se verifica si alguna sala dispone de la misma. En caso afirmativo, se asigna el valor de dicha sala al campo correspondiente en la tabla HCORDIMAG automaticamente cuando se termina la historia clinica.

', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'IdAGENSALAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'La URI Relativa se usa para identificar la ruta web o fisica en la que se encuentra el recurso, en este caso la imagen.    Ejemplo: URIRealtiva de imagen en indira: /study/7/4  La URL Base del recurso se guarda en la tabla de contenedores', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'RelativeURI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina si se maneja correlación :   1-Si   2-No   3-Sin especificar ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'CORRELACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo de Lateralidad:   0 - No aplica  1 - Izquierda  2 - Derecha  3 - Ambos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'LATERALIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuando campo TIPO FACTURACION sea 1 guardamos  el IdRiasCups a la que aplica de lo contrario queda NULL  Relacion con la tabla RIASCUPS.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'IDRIASCUPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'RIAS - Determina como se debe facturar el servicio:  1 - Rias  2 - Consulta Externa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'TIPOFACTURACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especialidad Realizó', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'ESPEREALIZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Guarda la calificación BIRADS:  1- BIRADS 0: Necesidad de Nuevo Estudio Imagenológico o Mamograma previo para evaluación  2- BIRADS 1: Negativo  3- BIRADS 2: Hallazgos Benignos  4- BIRADS 3: Probablemente Benigno  5- BIRADS 4: Anormalidad Sospechosa  6- BIRADS 5: Altamente Sospechoso de Malignidad  7- BIRADS 6: Malignidad por Biopsia ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'CLASIFICABIRADS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado Servicio IPS  
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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Prioridad del Servicio Solicitado  1: Urgente  2: Rutina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG', @level2type = N'COLUMN', @level2name = N'PRISERIPS';

