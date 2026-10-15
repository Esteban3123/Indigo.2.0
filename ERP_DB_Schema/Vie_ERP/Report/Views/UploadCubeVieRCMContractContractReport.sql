
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