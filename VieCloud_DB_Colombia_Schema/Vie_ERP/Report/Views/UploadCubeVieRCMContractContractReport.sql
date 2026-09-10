
create view [Report].[UploadCubeVieRCMContractContractReport] AS

	SELECT  DISTINCT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
		CE.Code AS 'CODIGO ENTIDAD CONTRATO',--[CodigoEntidadContrato],
		CE.Name as 'ENTIDAD CONTRATO',--[EntidadContrato], 
		HA.Code as 'CODIGO ENTIDAD ADMINISTRADORA',--[CodigoEntidadAdministradora],
		HA.Name as 'ENTIDAD ADMINISTRADORA',--[EntidadAdministradora], 
		C.Code as  'CODIGO CONTRATO',--[CodigoContrato],
		CD.ContractName as 'CONTRATO',--[Contrato], 
		ContractValue as 'VALOR CONTRATO',--[ValorContrato], 
		ExecuteValue as 'VALOR EJECUTADO',--[ValorEjecutado], 
		ContractObject as 'OBJETO CONTRATO',--[ObjetoContrato],
		CD.ContractNumber as 'NRO CONTRATO',--[NroContrato],
		case Type 
			when 1 then 'Nuevo' 
			when 2 then 'Inclusión' 
			when 3 then 'Otro Si' 
			when 4 then 'Adición'
			when 5 then 'Prorroga' 
			when 6 then 'Otro' End as 'TIPO',--[Tipo],
		cast(CD.InitialDate as date ) as 'FECHA INICIAL',--[FechaInicial], 
		cast(Cd.EndDate as date ) as 'FECHA FINAL',--[FechaFinal], 
		cast(CD.BillingInitialDate as date ) as 'FECHA INICIAL FACTURACION',--[FechaInicialFacturacion],
		cast(Cd.BillingEndDate as date ) as 'FECHA FINAL FACTURACION',--[FechaFinalFacturacion], 
		case CD.Legalized when 1 then 'Si' else 'No' end as 'LEGALIZADO',--[Legalizado],
		Cd.DateLegalization as 'FECHA LEGALIZACION',--[FechaLegalizacion], 
		cast(CD.RadicatedBillingDate AS date ) AS 'FECHA RADICACION',--[FechaRadicacion],
		cd.Observations as 'OBSERVACIONES',--[Observaciones], 
		CASE CD.PrintingMode 
			WHEN 1 THEN 'MANUAL' 
			WHEN 2 THEN 'CUPS' 
			WHEN 3 THEN 'RIPS' 
			WHEN 4 THEN 'Descripción Relacionada' 
			WHEN 5 THEN 'CUPS Descripción Relacionada' End 'MODO IMPRESION',--[ModoImpresion], 
		CASE CD.TerminationControl 
			WHEN 1 THEN 'Ninguno' 
			WHEN 2 THEN 'Fecha Termiancion' 
			WHEN 3 THEN 'Valor Contrato' 
			WHEN 4 THEN 'Fecha Terminacion o Valor contrato' End 'TERMINACION',--[Terminacion],
		case CD.NotificationValueType 
			when 1 then 'Ninguno' 
			when 2 then '% Valor Contrato' 
			when 3 then 'Valor Fijo' else 'N/A' End 'NOTIFICACION VALOR',--[NotificacionValor],
		CD.PercentageNotification AS '% NOTIFICACION',--[PorcentajeNotificacion], 
		cd.NotificationValue as 'VALOR NOTIFICACION',--[ValorNotificacion],
		CASE CD.NotificationTimeType 
			WHEN 1 THEN 'Ninguno' 
			WHEN 2 THEN 'Dias Anterioridad' 
			else 'N/A' End 'NOTIFICACION TIEMPO',--[NotificacionTiempo],
		CD.NotificationDays AS 'DIAS RESTANTE',--[DiasRestantes], 
		PercentageApplyPaymentSoon as '% APLICA PRONTO PAGO',--[PorcentajeAplicaProntoPago],
		CD.AgesPortfolioId 'EDAD CARTERA PAGO',--[EdadCarteraPago], 
			case ValidRecord 
			when 1  then 'Si' else 'No' end 'VIGENTE',--[Vigente],
		CG.Code 'CODIGO GRUPO ATENCION',--[CodigoGrupoAtencion], 
		CG.Name 'GRUPO ATENCION',--[GrupoAtencion],
		PR.Code 'CODIGO TARIFA',--[CodigoTarifa],
		PR.Name 'TARIFA PRODUCTO',--[TarifaProducto],
		PT.Code 'CODIGO PLANTILLA CUBRIMIENTO',--[CodigoPlantillaCubrimiento], 
		PT.Name 'PLANTILLA CUBRIMIENTO',--[PlantillaCubrimiento],
		DR.Code 'CODIGO DEFINICION TARIFAS',--[CodigoDefinicionTarifas], 
		DR.Name 'DEFINICION TARIFAS',--[DefinicionTarifas],
		CGDR.InitialDate 'FECHA INICIAL DEFINICION',--[FechaInicialDefinicion], 
		CGDR.EndDate 'FECHA FINAL DEFINICION',--[FechaFinalDefinicion]
        cast(CD.InitialDate as date ) [FECHA BUSQUEDA],
        CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
	FROM Contract.Contract AS C inner join
	Contract.HealthAdministrator AS HA ON HA.Id =C.HealthAdministratorId inner join
	Contract.ContractEntity AS CE ON CE.Id =C.ContractEntityId inner join
	Contract.ContractDetail as CD on CD.ContractId =C.Id
	LEFT JOIN Contract.CareGroup AS CG WITH (NOLOCK) ON C.Id =CG.ContractId 
	LEFT JOIN Inventory.ProductRate AS PR WITH (NOLOCK) ON CG.ProductRateId =PR.Id 
	LEFT JOIN Contract.ProcedureTemplate AS PT WITH (NOLOCK) ON PT.Id =CG.ProcedureTemplateId 
	LEFT JOIN Contract.CareGroupDefinitionRate CGDR WITH (NOLOCK) ON CG.Id =CGDR.CareGroupId 
	LEFT JOIN Contract .DefinitionRate AS DR WITH (NOLOCK) ON CGDR.DefinitionRateId =DR.Id
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de aplanado orientada a la carga de un cubo OLAP (reporting/BI) con la información consolidada de contratos de prestación de servicios de salud. Cruza el contrato principal con su detalle, la entidad contratante, la administradora de salud, los grupos de atención y sus tarifas vigentes. Expone atributos clave como tipo de contrato, valores pactados y ejecutados, fechas de vigencia y facturación, estado de legalización, modo de impresión, control de terminación y parámetros de notificación, identificando la compañía mediante `DB_NAME()`.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMContractContractReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMContractContractReport';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de reporte que consolida la información maestra de contratos con entidades administradoras y pagadoras, junto con sus detalles, grupos de atención, tarifas y plantillas, para alimentar un cubo de análisis (BI).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMContractContractReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existe al menos un registro en Contract.Contract con ContractEntityId y HealthAdministratorId válidos; Cada contrato debe tener su correspondiente fila en Contract.ContractDetail (INNER JOIN obligatorio)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMContractContractReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen contratos con entidad contratante, administradora y detalle (INNER JOIN sobre HealthAdministrator, ContractEntity y ContractDetail); Los grupos de atención, tarifas, plantillas y definiciones de tarifa son opcionales (LEFT JOIN), por lo que un contrato sin grupo de atención sigue apareciendo; ID_COMPANY se identifica con el nombre de la base de datos actual truncado a 9 caracteres; La marca de tiempo de actualización (ULT_ACTUAL) se calcula siempre en zona horaria ''Pakistan Standard Time''; Las fechas de vigencia, facturación y radicación se exponen como DATE (sin componente horario)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMContractContractReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Contrato; Entidad administradora de salud; Entidad contratante; Legalización de contrato; Facturación; Radicación; Pronto pago; Edad de cartera; Grupo de atención; Tarifa de producto; Plantilla de procedimiento; Definición de tarifas; Vigencia de contrato; Notificación de terminación de contrato', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMContractContractReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve filas DISTINCT por contrato/grupo de atención/tarifa, agregando ID_COMPANY=DB_NAME() y un timestamp ULT_ACTUAL convertido a la zona horaria ''Pakistan Standard Time''.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMContractContractReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Type IN (1..6) → Etiqueta Tipo de contrato como Nuevo/Inclusión/Otro Si/Adición/Prorroga/Otro respectivamente else NULL; si CD.Legalized = 1 → LEGALIZADO = ''Si'' else LEGALIZADO = ''No''; si CD.PrintingMode IN (1..5) → Modo de impresión: MANUAL/CUPS/RIPS/Descripción Relacionada/CUPS Descripción Relacionada else NULL; si CD.TerminationControl IN (1..4) → Tipo de terminación: Ninguno/Fecha Terminación/Valor Contrato/Fecha Terminación o Valor Contrato else NULL; si CD.NotificationValueType IN (1,2,3) → Notificación valor: Ninguno / % Valor Contrato / Valor Fijo else ''N/A''; si CD.NotificationTimeType IN (1,2) → Notificación tiempo: Ninguno / Días Anterioridad else ''N/A''; si ValidRecord = 1 → VIGENTE = ''Si'' else VIGENTE = ''No''', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMContractContractReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Contract.Contract; Contract.HealthAdministrator; Contract.ContractEntity; Contract.ContractDetail; Contract.CareGroup; Inventory.ProductRate; Contract.ProcedureTemplate; Contract.CareGroupDefinitionRate; Contract.DefinitionRate', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMContractContractReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMContractContractReport';
GO
