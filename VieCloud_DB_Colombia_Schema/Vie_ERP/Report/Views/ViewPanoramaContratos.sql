

CREATE VIEW [Report].[ViewPanoramaContratos] AS
SELECT DISTINCT
 CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY, 
 CE.Code AS [CODIGO ENTIDADA CONTRATO],
 CE.Name as [ENTIDAD CONTRATO],
 HA.Code as [CODIGO ENTIDAD ADMINISTRADORA],
 HA.Name as [ENTIDAD ADMINISTRADORA],
 C.Code as [CODIGO CONTRATO],
 CD.ContractName as [NOMBRE CONTRATO],
 ContractValue as [VALOR CONTRATO],
 ExecuteValue as [VALOR EJECUTADO],
 ContractObject as [OBJETO CONTRATO],
 CD.ContractNumber as [NUMERO CONTRATO],
 case Type 
      when 1 then 'Nuevo' 
	  when 2 then 'Inclusión'
	  when 3 then 'Otro Si' 
	  when 4 then 'Adición' 
	  when 5 then 'Prorroga'
	  when 6 then 'Otro' End as [TIPO],
 cast(CD.InitialDate as date ) as [FECHA INICIAL],
 cast(Cd.EndDate as date ) as [FECHA FINAL],
 cast(CD.BillingInitialDate as date ) as [FECHA INICIAL FACTURACION],
 cast(Cd.BillingEndDate as date ) as [FECHA FINAL FACTURACION],
 case CD.Legalized when 1 then 'Si' else 'No' end as [LEGALIZADO],
 Cd.DateLegalization as [FECHA LEGALIZACION],
 cast(CD.RadicatedBillingDate AS date ) AS [FECHA RADICACION],
 cd.Observations as [OBSERVACIONES],
 CASE CD.PrintingMode 
      WHEN 1 THEN 'MANUAL' 
	  WHEN 2 THEN 'CUPS' 
	  WHEN 3 THEN 'RIPS' 
	  WHEN 4 THEN 'Descripción Relacionada' 
	  WHEN 5 THEN 'CUPS Descripción Relacionada' 
  End [MODO DE IMPRESION],
  CASE CD.TerminationControl 
       WHEN 1 THEN 'Ninguno' 
	   WHEN 2 THEN 'Fecha Termiancion' 
	   WHEN 3 THEN 'Valor Contrato' 
	   WHEN 4 THEN 'Fecha Terminacion o Valor contrato' 
  End [TERMINACION],
  case CD.NotificationValueType 
       when 1 then 'Ninguno' 
	   when 2 then '% Valor Contrato' 
	   when 3 then 'Valor Fijo' 
	   else 'N/A' End [NOTIFICACION POR VALOR],
  CD.PercentageNotification AS [% NOTIFICACION],
  cd.NotificationValue as [VALOR NOTIFICACION],
  CASE CD.NotificationTimeType 
       WHEN 1 THEN 'Ninguno' 
	   WHEN 2 THEN 'Dias Anterioridad' 
  else 'N/A' End [NOTIFICACION POR TIEMPO],
  CD.NotificationDays AS [DIAS RESTANTES],
  PercentageApplyPaymentSoon as [% APLICA PRONTO PAGO],
  CD.AgesPortfolioId [EDAD CARTERA PAGO],
  case ValidRecord when 1 then 'Si' else 'No' end [VIGENTE],
  CG.Code [CODIGO GRUPO ATENCION],
  CG.Name [GRUPO ATENCION],
  PR.Code [CODIGO TARIFA],
  PR.Name [TARIFA DE PRODUCTO],
  PT.Code [CODIGO PLANTILLA CUBRIMIENTO],
  PT.Name [PLANTILLA DE CUBRIMIENTO],
  DR.Code [CODIGO DEFINICION TARIFAS],
  DR.Name [DEFINICION DE TARIFAS],
  CGDR.InitialDate [FECHA INICIAL DEFINICION],
  CGDR.EndDate [FECHA FINAL DEFINICION],
  1 as 'CANTIDAD',
  CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM 
  Contract.Contract AS C 
  inner join Contract .HealthAdministrator AS HA ON HA.Id =C.HealthAdministratorId 
  inner join Contract .ContractEntity AS CE ON CE.Id =C.ContractEntityId 
  inner join Contract .ContractDetail as CD on CD.ContractId =C.Id
  LEFT JOIN Contract.CareGroup AS CG WITH (NOLOCK) ON C.Id =CG.ContractId
  LEFT JOIN Inventory.ProductRate AS PR WITH (NOLOCK) ON CG.ProductRateId =PR.Id
  LEFT JOIN Contract.ProcedureTemplate AS PT WITH (NOLOCK) ON PT.Id =CG.ProcedureTemplateId
  LEFT JOIN Contract.CareGroupDefinitionRate CGDR WITH (NOLOCK) ON CG.Id =CGDR.CareGroupId
  LEFT JOIN Contract .DefinitionRate AS DR WITH (NOLOCK) ON CGDR.DefinitionRateId =DR.Id
--order by C.Code
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporte que consolida el panorama completo de contratos con entidades pagadoras y administradoras de salud, aplanando en una sola fila los datos del contrato (valores, fechas, tipo, legalización, control de terminación, notificaciones), junto con sus grupos de atención, tarifas de producto, plantillas de cubrimiento y definiciones tarifarias vigentes. Está diseñada para consumo en herramientas de reporting o BI, identificando la empresa mediante `DB_NAME()` y marcando la última actualización en zona horaria de Pakistán.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPanoramaContratos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPanoramaContratos';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de reporte que consolida un panorama de contratos de salud con sus entidades, valores, vigencias, parámetros de notificación/terminación, grupos de atención y tarifas asociadas para análisis gerencial.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPanoramaContratos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia del contrato con sus relaciones obligatorias a entidad contratante (ContractEntity), entidad administradora (HealthAdministrator) y detalle de contrato (ContractDetail); La base de datos actual debe tener un nombre cuyo prefijo (≤9 caracteres) identifique la compañía; El servidor debe reconocer la zona horaria ''Pakistan Standard Time'' para el cálculo de ULT_ACTUAL', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPanoramaContratos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada fila se asocia obligatoriamente a una entidad contrato (CE), una entidad administradora (HA) y un detalle de contrato (CD) por uso de INNER JOIN; Los grupos de atención, tarifas de producto, plantillas de cubrimiento y definiciones de tarifas son opcionales (LEFT JOIN); su ausencia no excluye al contrato del reporte; El identificador de compañía expuesto se trunca a 9 caracteres del nombre de la base de datos actual (DB_NAME()); La marca de última actualización se calcula convirtiendo GETDATE() a la zona horaria ''Pakistan Standard Time''; Las fechas de vigencia, facturación y radicación se exponen sin componente de hora (CAST a DATE); Se usa SELECT DISTINCT para evitar duplicados generados por la combinación de múltiples grupos de atención y definiciones de tarifa; Cada fila aporta una unidad fija al campo CANTIDAD (=1), pensada para conteos en el reporte; Las consultas a tablas de configuración tarifaria/plantillas usan NOLOCK, aceptando lecturas sucias para no bloquear operaciones transaccionales', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPanoramaContratos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Contrato de salud; Entidad administradora (EPS/aseguradora); Entidad contratante; Legalización de contrato; Vigencia de contrato; Facturación y radicación; Pronto pago; Edad de cartera; Notificación por valor o tiempo (% valor contrato, días de anterioridad); Terminación de contrato (por fecha o valor); Grupo de atención; Tarifa de producto; Plantilla de cubrimiento (procedimientos); Definición de tarifas; Modos de impresión CUPS/RIPS', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPanoramaContratos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Devuelve un conjunto único (DISTINCT) de contratos con detalle administrativo, financiero y de configuración tarifaria, incluyendo el identificador de compañía derivado de DB_NAME() y un timestamp en zona horaria ''Pakistan Standard Time''', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPanoramaContratos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CD.Type IN (1..6) → Se traduce a etiqueta de negocio: 1=Nuevo, 2=Inclusión, 3=Otro Si, 4=Adición, 5=Prorroga, 6=Otro; si CD.Legalized = 1 → Se reporta ''Si'' como estado de legalización else Se reporta ''No''; si CD.PrintingMode IN (1..5) → Se mapea a modo de impresión: 1=MANUAL, 2=CUPS, 3=RIPS, 4=Descripción Relacionada, 5=CUPS Descripción Relacionada; si CD.TerminationControl IN (1..4) → Se traduce a control de terminación: 1=Ninguno, 2=Fecha Terminación, 3=Valor Contrato, 4=Fecha Terminación o Valor contrato; si CD.NotificationValueType IN (1..3) → Se mapea: 1=Ninguno, 2=% Valor Contrato, 3=Valor Fijo else Se reporta ''N/A''; si CD.NotificationTimeType IN (1,2) → Se mapea: 1=Ninguno, 2=Dias Anterioridad else Se reporta ''N/A''; si C.ValidRecord = 1 → Se reporta como vigente (''Si'') else Se reporta ''No''', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPanoramaContratos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Contract.Contract; Contract.HealthAdministrator; Contract.ContractEntity; Contract.ContractDetail; Contract.CareGroup; Inventory.ProductRate; Contract.ProcedureTemplate; Contract.CareGroupDefinitionRate; Contract.DefinitionRate', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPanoramaContratos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPanoramaContratos';
GO
