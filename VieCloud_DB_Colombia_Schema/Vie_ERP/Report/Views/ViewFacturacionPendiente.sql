

CREATE VIEW [Report].[ViewFacturacionPendiente]
	
AS

	WITH CTE_FACTURACION_PENDIENTE
	AS
	(
		SELECT RTRIM(CEN.NOMCENATE) AS [CentroAtencion],
			RCD.CAREGROUPID AS [GrupoID],
			RCD.ID AS [ID],
			CASE
				WHEN ING.IESTADOIN='' THEN 'ABIERTO'
				WHEN ING.IESTADOIN='P' THEN 'PARCIAL' END AS [EstadoIngreso],
			RC.ADMISSIONNUMBER AS [Ingreso],
			CASE ING.TIPOINGRE
				WHEN 1 THEN 'AMBULATORIO'
				WHEN 2 THEN 'HOSPITALARIO' END [TipoIngreso],
			ING.IFECHAING AS [FechaIngreso],
			RTRIM(tdoc.sigla) AS [TipoIdentificacion],
			RC.PATIENTCODE AS [NroIdentificacion],
			P.IPNOMCOMP AS [Paciente],
			P.IPFECNACI AS [FechaNacimiento],
			DATEDIFF(YEAR, P.IPFECNACI, ING.IFECHAING) AS [EdadConsulta],
			T .NAME AS [Entidad],
			RCD.FOLIOORDER AS [FoliosFacturar],
			CASE RCD.FOLIOTYPE
				WHEN '1' THEN 'EAPB CON CONTRATO'
				WHEN '2' THEN 'EAPB SIN CONTRATO'
				WHEN '3' THEN 'PARTICULARES'
				WHEN '4' THEN 'ASEGURADORAS' END AS [TipoFolio],
			CASE RCD.LIQUIDATIONTYPE
				WHEN '1' THEN 'PAGO POR SERVICIOS'
				WHEN '2' THEN 'CAPITACIÓN'
				WHEN '3' THEN 'FACTURA GLOBAL'
				WHEN '4' THEN 'CAPITACIÓN GLOBAL'
				ELSE 'CONTROL CAPITACIÓN' END AS [TipoLiquidacion],
			EA.HEALTHENTITYCODE AS [EntidadAdministradora],
			T .NIT AS [NITEntidad],
			GA.CODE AS [GrupoAtencion],
			GA.NAME AS [DescripcionGrupoAtencion],
			CASE EA.EntityType
				WHEN 1 THEN 'EPS Contributivo'
				WHEN 2 THEN 'EPS Subsidiado'
				WHEN 3 THEN 'ET Vinculados Municipios'
				WHEN 4 THEN 'ET Vinculados Departamentos'
				WHEN 5 THEN 'ARL Riesgos Laborales'
				WHEN 6 THEN 'MP Medicina Prepagada'
				WHEN 7 THEN 'IPS Privada'
				WHEN 8 THEN 'IPS Publica'
				WHEN 9 THEN 'Regimen Especial'
				WHEN 10 THEN 'Accidentes de transito'
				WHEN 11 THEN 'Fosyga'
				WHEN 12 THEN 'Otros' END AS [Regimen],
			RCD.TOTALFOLIO AS [ValTotalFolioPendienteFacturar],
			DQ.INVOICEDQUANTITY AS [CantidadCirugia],
			DQ.TOTALSALESPRICE AS [TotalFacturadoCirugia],
			DQ.PERFORMSHEALTHPROFESSIONALCODE AS [CodProfesionalCirugia],
			RTRIM(MEDQX.CODPROSAL) + ' - ' + LTRIM(MEDQX.NOMMEDICO) AS [ProfesionalCirugia],
			CASE RCD.RESPONSIBLERECOVERYFEE
				WHEN '1' THEN 'NINGUNO'
				WHEN '2' THEN 'PACIENTE'
				WHEN '3' THEN 'TERCERO' END AS [ResponsableCuotaRecuperacion],
			CASE P.TIPCOBSAL
				WHEN '1' THEN 'CONTRIBUTIVO'
				WHEN '2' THEN 'SUBSIDIADO TOTAL'
				WHEN '3' THEN 'SUBSIDIADO PARCIAL'
				WHEN '4' THEN 'POBLACION POBRE SIN ASEGURAR CON SISBEN'
				WHEN '5' THEN 'POBLACION POBRE SIN ASEGURAR SIN SISBEN'
				WHEN '6' THEN 'DESPLAZADOS'
				WHEN '7' THEN 'PLAN DE SALUD ADICIONAL'
				WHEN '8' THEN 'OTROS'
				ELSE 'DESCONOCIDO' END AS [TipoPaciente],
			RCD.TOTALPATIENTWITHDISCOUNT AS [ValCobradoPaciente],
			RCD.VALUECOPAY AS [ValCuotaRecuperacionFolio],
			RCD.VALUEFEEMODERATOR AS [ValCuotaModeradoraFolio],
			RCD.OBSERVATION AS [Observaciones],
			CAT.NAME AS [CategoriaRIPS],
			CASE RCD.STATUS
				WHEN '1' THEN 'REGISTRADO'
				WHEN '2' THEN 'FACTURADO'
				WHEN '3' THEN 'BLOQUEADO' END AS [Estado],
			PER.FULLNAME AS [UsuarioCreo],
			RCD.CREATIONDATE AS [FechaCreacion],
			PERM .FULLNAME AS [UsuarioModifico],
			RCD.MODIFICATIONDATE AS [FechaModificacion],
			CASE SOD.SERVICETYPE
				WHEN '1' THEN 'SOAT'
				WHEN '2' THEN 'ISS'
				WHEN '3' THEN 'CUPS' END AS [ClaseServicio],
			CASE SOD.RECORDTYPE
				WHEN '1' THEN 'SERVICIOS'
				WHEN '2' THEN 'MEDICAMENTOS/INSUMOS' END AS [TipoOrden],
			CASE PT.Class
				WHEN '2' THEN 'MEDICAMENTOS'
				WHEN '3' THEN 'INSUMOS' ELSE 'SERVICIOS' END AS [TipoServicio],
			ISNULL(CG.Code + '-' + CG.Name, PG.Code + '-' + PG.NAME) [Grupo],
			ISNULL(CSG.CODE + '-' + CSG.Name, PSG.Code + '-' + PSG.NAME) [Subgrupo],
			COALESCE(CUPS.Code, SERVICIOSIPS.CODE, PR.CODE) [CodigoCUP],
			CASE 
				WHEN CDD.Name IS NOT NULL THEN CDD.Name
				ELSE COALESCE(CUPS.Description, SERVICIOSIPS.NAME, PR.NAME) END [DescripcionServicio],
			CASE SOD.PRESENTATION
				WHEN '1' THEN 'NO QUIRÚRGICO'
				WHEN '2' THEN 'QUIRÚRGICO'
				WHEN '3' THEN 'PAQUETE' END AS [PresentacionServicio],
			SOD.SUPPLYQUANTITY AS [CantidadEntregada],
			SOD.DEVOLUTIONQUANTITY AS [CantidadDevuelta],
			ISNULL(DQ.INVOICEDQUANTITY, SOD.INVOICEDQUANTITY) AS [CantidadFacturar],
			ISNULL(DQ.TOTALSALESPRICE, SOD.SubTotalSalesPrice) AS [ValUnitarioFacturar],
			ISNULL((DQ.INVOICEDQUANTITY)*(DQ.TOTALSALESPRICE), (SOD.INVOICEDQUANTITY) * (SOD.SubTotalSalesPrice)) AS [TotalFacturar],
			CASE SOD.ISPACKAGE WHEN 0 THEN 'NO' ELSE 'SI' END AS [Paquete],
			CASE SOD.PACKAGING WHEN 0 THEN 'NO' ELSE 'SI' END AS [ItemPaquete],
			CASE SOD.SettlementType WHEN 3 THEN 'SI (No se cobra nada)' ELSE 'NO (Se cobra)' END [ServiciosIncluidos],
			CUPS2.Code AS [AgrupadorCUP],
			CUPS2.Description AS [DescripcionAgrupadorCUP],
			SOD2.InvoicedQuantity [CantidadAgrupador],
			SOD2.SubTotalSalesPrice [ValUnitarioAgrupador],
			SOD2.GrandTotalSalesPrice [TotalAgrupador],
			ISNULL(DQ.TOTALSALESPRICE, SOD.RateManualSalePrice) AS [TarifaServicioUnitario],
			ISNULL((DQ.INVOICEDQUANTITY)*(DQ.TOTALSALESPRICE),(SOD.InvoicedQuantity * SOD.RateManualSalePrice)) AS [TotalTarifaServicio],
			SODD.SubTotalPatientSalesPrice AS [TotalCuotaRecuperacion],
			SOD.SERVICEDATE AS [FechaServicio],
			SOD.AUTHORIZATIONNUMBER AS [Autorizacion],
			UF2.CODE AS [CodUnidadFuncionalIngreso],
			UF2.NAME AS [UnidadFuncionalIngreso],
			IIF (ING.TIPOINGRE = 1
			AND CUPS.ServiceType IN (1, 2, 3, 8)
			AND SALIDA.FECALTPAC IS NULL, DATEADD(MINUTE, 10, ING.IFECHAING), SALIDA.FECALTPAC) AS [FechaAltaMedica],
			IIF (ING.TIPOINGRE = 1
			AND CUPS.ServiceType IN (1, 2, 3, 8)
			AND SALIDA.FECALTPAC IS NULL, YEAR(ING.IFECHAING), YEAR(SALIDA.FECALTPAC)) AS [AñoAltaMedica],
			IIF (ING.TIPOINGRE = 1
			AND CUPS.ServiceType IN (1, 2, 3, 8)
			AND SALIDA.FECALTPAC IS NULL, MONTH(ING.IFECHAING), MONTH(SALIDA.FECALTPAC)) AS [MesAltaMedica],
			IIF (ING.TIPOINGRE = 1
			AND CUPS.ServiceType IN (1, 2, 3, 8)
			AND SALIDA.FECALTPAC IS NULL, 
			CASE MONTH(ING.IFECHAING)
				WHEN 1 THEN 'ENERO'
				WHEN 2 THEN 'FEBRERO'
				WHEN 3 THEN 'MARZO'
				WHEN 4 THEN 'ABRIL'
				WHEN 5 THEN 'MAYO'
				WHEN 6 THEN 'JUNIO'
				WHEN 7 THEN 'JULIO'
				WHEN 8 THEN 'AGOSTO'
				WHEN 9 THEN 'SEPTIEMBRE'
				WHEN 10 THEN 'OCTUBRE'
				WHEN 11 THEN 'NOVIEMBRE'
				WHEN 12 THEN 'DICIEMBRE' END,
			CASE MONTH(SALIDA.FECALTPAC)
				WHEN 1 THEN 'ENERO'
				WHEN 2 THEN 'FEBRERO'
				WHEN 3 THEN 'MARZO'
				WHEN 4 THEN 'ABRIL'
				WHEN 5 THEN 'MAYO'
				WHEN 6 THEN 'JUNIO'
				WHEN 7 THEN 'JULIO'
				WHEN 8 THEN 'AGOSTO'
				WHEN 9 THEN 'SEPTIEMBRE'
				WHEN 10 THEN 'OCTUBRE'
				WHEN 11 THEN 'NOVIEMBRE'
				WHEN 12 THEN 'DICIEMBRE' ELSE 'DESCONOCIDO' END) AS [NombreMesAltaMedica],
			IIF (ING.TIPOINGRE = 1
			AND CUPS.ServiceType IN (1, 2, 3, 8)
			AND SALIDA.FECALTPAC IS NULL, DAY(ING.IFECHAING), MONTH(SALIDA.FECALTPAC)) AS [DiaAltaMedica],
			YEAR(ING.IFECHAING) AS [AñoIngreso],
			MONTH(ING.IFECHAING) AS [MesIngreso],
			CASE MONTH(ING.IFECHAING)
				WHEN 1 THEN 'ENERO'
				WHEN 2 THEN 'FEBRERO'
				WHEN 3 THEN 'MARZO'
				WHEN 4 THEN 'ABRIL'
				WHEN 5 THEN 'MAYO'
				WHEN 6 THEN 'JUNIO'
				WHEN 7 THEN 'JULIO'
				WHEN 8 THEN 'AGOSTO'
				WHEN 9 THEN 'SEPTIEMBRE'
				WHEN 10 THEN 'OCTUBRE'
				WHEN 11 THEN 'NOVIEMBRE'
				WHEN 12 THEN 'DICIEMBRE'
				ELSE 'DESCONOCIDO' END [NombreMesIngreso],
			DAY(ING.IFECHAING) AS [DiaIngreso],
			IIF (ING.TIPOINGRE = 1 AND CUPS.ServiceType IN (1, 2, 3, 8) AND SALIDA.FECALTPAC IS NULL, 
			CASE WHEN ING.IFECHAING IS NULL THEN 'SIN ALTA MÉDICA' ELSE 'CON ALTA MÉDICA' END, 
			CASE WHEN SALIDA.FECALTPAC IS NULL THEN 'SIN ALTA MÉDICA' ELSE 'CON ALTA MÉDICA' END) AS [IngresoAltaMedica],
			IIF (ING.TIPOINGRE = 1 AND CUPS.ServiceType IN (1, 2, 3, 8) AND SALIDA.FECALTPAC IS NULL, UF.NAME, UFE.UFUDESCRI) AS [UnidadFuncionalEgreso],
			CAMA.DESCCAMAS AS [CamaEgreso],
			UF.Name [UnidadFuncionalServicio],
			COST.Code [CodCentroCosto],
			COST.Name [CentroCosto],
			OU.UnitName [CiudadOrdenamiento],
			ISNULL(SOD.ID, 0) [OrdenID],
			ISNULL(DQ.ID, 0) [CirugiaID],
			CAST(ING.IFECHAING AS DATE) AS [FechaBusqueda],
			/*
			CASE CEN.NOMCENATE
				WHEN 'CLINICA DE ALTA TECNOLOGIA MARAYA' THEN '4.812775'
				WHEN 'CIRCUNVALAR' THEN '4.808665'
				WHEN 'SAN MARCEL' THEN '5.036220'
				WHEN 'HOSPITALIZACION INFANTIL' THEN '5.064226'
				WHEN 'CENTENARIO' THEN '4.542379'
				WHEN 'SAN JUAN DE DIOS' THEN '4.557683'
				WHEN 'CLINICA ARTURO LOPEZ CARDONA' THEN '4.545148'
				WHEN 'CARTAGO UNIDAD ONCOLOGICA' THEN '4.754317' END [UbicacionLatirud],
			CASE CEN.NOMCENATE
				WHEN 'CLINICA DE ALTA TECNOLOGIA MARAYA' THEN '-75.721576'
				WHEN 'CIRCUNVALAR' THEN '-75.680038'
				WHEN 'SAN MARCEL' THEN '-75.469270'
				WHEN 'HOSPITALIZACION INFANTIL' THEN '-75.498604'
				WHEN 'CENTENARIO' THEN '-75.658726'
				WHEN 'SAN JUAN DE DIOS' THEN '-75.656878'
				WHEN 'CLINICA ARTURO LOPEZ CARDONA' THEN '-75.661106'
				WHEN 'CARTAGO UNIDAD ONCOLOGICA' THEN '-75.926218' END [UbicacionLongitud], --INTO [BILLING].[STG_PENDIENTE_FACTURACION]*/
			--AC.DESCRIPCION AS [Agrupador],
			CCSF.Name [EstadoFolio],
			(SELECT TOP 1 RTRIM(dx.coddiagno) + ' - ' + RTRIM(dx.nomdiagno) FROM dbo.indiagnop AS dxp INNER JOIN dbo.indiagnos AS dx ON dxp.coddiagno = dx.coddiagno  WHERE dxp.coddiapri = 1 AND numingres = ing.numingres) AS [Diagnostico],
			RTRIM(srvp.identification) + ' - ' + RTRIM(srvp.fullname) [UsuarioServicio],
			so.creationdate AS [FechaOrdenServicio]
		FROM BILLING.REVENUECONTROLDETAIL AS RCD 
		INNER JOIN BILLING.SERVICEORDERDETAILDISTRIBUTION AS SODD  ON RCD.ID = SODD.REVENUECONTROLDETAILID AND RCD.STATUS IN ('1', '3')
		INNER JOIN BILLING.SERVICEORDERDETAIL AS SOD  ON SODD.SERVICEORDERDETAILID = SOD.ID
		INNER JOIN BILLING.REVENUECONTROL AS RC  ON RCD.REVENUECONTROLID = RC.ID
		LEFT JOIN Billing .ConceptsCausesStatusFolio AS CCSF  ON CCSF.Id =RCD.StatusFolioId 
		LEFT JOIN DBO.ADINGRESO AS ING  ON ING.NUMINGRES = RC.ADMISSIONNUMBER
		LEFT JOIN DBO.INPACIENT AS P  ON P.IPCODPACI = RC.PATIENTCODE
		LEFT JOIN dbo.adtipoidentifica AS tdoc ON p.iptipodoc = tdoc.codigo
		LEFT JOIN CONTRACT.CONTRACTENTITY AS E  ON E.ID = RCD.CONTRACTENTITYID
		LEFT JOIN CONTRACT.CAREGROUP AS GA  ON GA.ID = RCD.CAREGROUPID
		LEFT JOIN BILLING.INVOICECATEGORIES AS CAT  ON CAT.ID = RCD.INVOICECATEGORYID
		LEFT JOIN [SECURITY].[USER] AS U  ON U.USERCODE = RCD.CREATIONUSER
		LEFT JOIN [SECURITY].PERSON AS PER  ON PER.ID = U.IDPERSON
		LEFT JOIN [SECURITY].[USER] AS UM  ON UM.USERCODE = RCD.MODIFICATIONUSER
		LEFT JOIN [SECURITY].PERSON AS PERM  ON PERM .ID = UM.IDPERSON
		LEFT JOIN CONTRACT.CUPSENTITY AS CUPS  ON CUPS.ID = SOD.CUPSENTITYID
		LEFT JOIN Contract.CupsSubgroup AS CSG  ON CUPS.CUPSSubGroupId=CSG.ID
		LEFT JOIN Contract.CupsGroup AS CG  ON CG.ID=CSG.CupsGroupId
		LEFT JOIN CONTRACT.IPSSERVICE AS SERVICIOSIPS  ON SERVICIOSIPS.ID = SOD.IPSSERVICEID
		LEFT JOIN INVENTORY.INVENTORYPRODUCT AS PR  ON PR.ID = SOD.PRODUCTID AND PR.Status = 1
		LEFT JOIN Inventory.ProductType AS PT  ON PR.ProductTypeId = PT.Id
		LEFT JOIN Inventory.ProductGroup AS PG  ON PG.ID =PR.ProductGroupId
		LEFT JOIN Inventory.ProductSubGroup AS PSG  ON PSG.ID =PR.ProductSubGroupId
		LEFT JOIN PAYROLL.FUNCTIONALUNIT AS UF  ON UF.ID = SOD.PERFORMSFUNCTIONALUNITID
		LEFT JOIN PAYROLL.FUNCTIONALUNIT AS UF2  ON UF2.Code = ING.UFUCODIGO
		LEFT JOIN DBO.HCREGEGRE AS SALIDA  ON SALIDA.NUMINGRES = RC.ADMISSIONNUMBER
		LEFT JOIN DBO.HCREGEST AS CAME  ON SALIDA.NUMEFOLIO = CAME.NUMEFOLIO AND SALIDA.IPCODPACI = CAME.IPCODPACI AND SALIDA.NUMINGRES = CAME.NUMINGRES AND SALIDA.UFUCODIGO = CAME.UFUCODIGO 
		LEFT JOIN DBO.CHCAMASHO AS CAMA  ON CAME.UFUCODIGO = CAMA.UFUCODIGO AND CAME.CODICAMAS = CAMA.CODICAMAS
		LEFT JOIN CONTRACT.HEALTHADMINISTRATOR AS EA  ON EA.ID = RCD.HEALTHADMINISTRATORID
		LEFT JOIN COMMON.THIRDPARTY AS T  ON T .ID = SOD.THIRDPARTYID
		INNER JOIN DBO.ADCENATEN AS CEN ON CEN.CODCENATE = ING.CODCENATE
		LEFT JOIN DBO.INUNIFUNC AS UFE  ON ING.UFUEGRMED = UFE.UFUCODIGO
		LEFT JOIN BILLING.SERVICEORDERDETAILSURGICAL AS DQ  ON DQ.SERVICEORDERDETAILID = SOD.ID
		LEFT JOIN CONTRACT.IPSSERVICE AS SERVICIOSIPSQ  ON SERVICIOSIPSQ.ID = DQ.IPSSERVICEID
		LEFT JOIN DBO.INPROFSAL AS MEDQX  ON MEDQX.CODPROSAL = DQ.PERFORMSHEALTHPROFESSIONALCODE
		LEFT JOIN BILLING.SERVICEORDER AS SO  ON SO.ID = SOD.SERVICEORDERID
		LEFT JOIN Payroll.CostCenter AS COST  ON COST.Id =SOD.CostCenterId
		LEFT JOIN Billing.ServiceOrderDetail AS SOD2  ON SOD2.Id=SOD.IncludeServiceOrderDetailId
		LEFT JOIN Contract.CUPSEntity AS CUPS2  ON CUPS2.Id = SOD2.CUPSEntityId
		LEFT JOIN Contract.CUPSEntityContractDescriptions AS CECD  ON CECD.ID=SOD.CUPSEntityContractDescriptionId
		LEFT JOIN Contract.ContractDescriptions AS CDD  ON CECD.ContractDescriptionId =CDD.Id
		LEFT JOIN Common.OperatingUnit AS OU ON OU.Id =SO.OperatingUnitId
		--LEFT JOIN dbo.DWH_AGRUPADORES_CONTABILIDAD AC ON COST.Code=AC.CODECC
		LEFT JOIN [security].[user] AS srvu ON so.creationuser = srvu.usercode
		LEFT JOIN [security].person AS srvp ON  srvu.idperson = srvp.id
		WHERE RCD.STATUS IN ('1', '3')
		AND SOD.ISDELETE = '0'
		AND ING.IESTADOIN <> 'A'
		AND ING.IESTADOIN <> 'F'
		AND ING.IESTADOIN <> 'C'
	--AND CAST(ING.IFECHAING AS DATE) BETWEEN @FechaInicio AND @FechaFin

	)

	SELECT * FROM CTE_FACTURACION_PENDIENTE WHERE [TipoLiquidacion] NOT IN ('CONTROL CAPITACIÓN', 'CAPITACIÓN','FACTURA GLOBAL')
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporte orientada a consumo por áreas de facturación y auditoría, que consolida los folios pendientes de facturar agrupando ingresos (ambulatorios y hospitalarios) con sus órdenes de servicio, detalle de procedimientos CUPS, medicamentos e insumos, datos del paciente, entidad administradora, tipo de régimen, valores a cobrar (cuota moderadora, cuota de recuperación, total folio) y estado del folio, incluyendo información de alta médica, unidad funcional, cama y diagnóstico principal para facilitar el seguimiento y cierre del proceso de facturación.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewFacturacionPendiente';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewFacturacionPendiente';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida los folios de facturación pendiente con su detalle de órdenes de servicio, paciente, ingreso, contrato, profesional, unidad funcional y datos del alta médica, excluyendo modalidades capitadas y globales.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewFacturacionPendiente';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen registros en BILLING.REVENUECONTROLDETAIL con STATUS ''1'' (REGISTRADO) o ''3'' (BLOQUEADO); Las órdenes de servicio referenciadas no están marcadas como eliminadas (SOD.ISDELETE=''0''); El ingreso (ADINGRESO) referenciado no está en estado ''A'', ''F'' ni ''C''', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewFacturacionPendiente';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran folios en estado REGISTRADO (''1'') o BLOQUEADO (''3''); se excluyen los FACTURADOS; Se excluyen ingresos anulados/finalizados/cerrados (IESTADOIN en ''A'',''F'',''C''); Se excluyen las modalidades de liquidación CAPITACIÓN, FACTURA GLOBAL y CONTROL CAPITACIÓN, dejando solo PAGO POR SERVICIOS y CAPITACIÓN GLOBAL; Solo se incluyen detalles de orden no eliminados (ISDELETE=''0''); Solo se consideran productos de inventario activos (PR.Status=1); Para servicios quirúrgicos (tabla SERVICEORDERDETAILSURGICAL), la cantidad y precio facturado se priorizan sobre los del detalle de orden estándar; Código CUP se resuelve por prioridad: CUPSENTITY → IPSSERVICE → INVENTORYPRODUCT; Diagnóstico mostrado es siempre el principal (coddiapri = 1) del ingreso; EdadConsulta se calcula como diferencia en años entre fecha de nacimiento y fecha de ingreso', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewFacturacionPendiente';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.ViewFacturacionPendiente: Devuelve folios pendientes (STATUS 1 o 3) con detalle de servicios; excluye TipoLiquidacion en (''CONTROL CAPITACIÓN'',''CAPITACIÓN'',''FACTURA GLOBAL'')', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewFacturacionPendiente';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ING.IESTADOIN = '''' (vacío) → EstadoIngreso = ''ABIERTO'' else Si IESTADOIN=''P'' → ''PARCIAL''; si RCD.FOLIOTYPE 1/2/3/4 → Clasifica TipoFolio como EAPB CON CONTRATO / EAPB SIN CONTRATO / PARTICULARES / ASEGURADORAS; si RCD.LIQUIDATIONTYPE 1/2/3/4 → TipoLiquidacion: PAGO POR SERVICIOS / CAPITACIÓN / FACTURA GLOBAL / CAPITACIÓN GLOBAL else ''CONTROL CAPITACIÓN'' (cualquier otro valor); si RCD.STATUS 1/2/3 → Estado: REGISTRADO / FACTURADO / BLOQUEADO (filtra solo 1 y 3); si ING.TIPOINGRE=1 AND CUPS.ServiceType IN (1,2,3,8) AND SALIDA.FECALTPAC IS NULL → Para ambulatorios sin alta registrada, FechaAltaMedica = IFECHAING + 10 minutos y UnidadFuncionalEgreso = UF.NAME (unidad del servicio) else Usa SALIDA.FECALTPAC y UFE.UFUDESCRI (unidad de egreso del ingreso); si PT.Class 2/3 → TipoServicio: MEDICAMENTOS / INSUMOS else ''SERVICIOS''; si SOD.SettlementType = 3 → ServiciosIncluidos = ''SI (No se cobra nada)'' else ''NO (Se cobra)''; si EA.EntityType 1..12 → Mapea Régimen (EPS Contributivo, Subsidiado, ARL, Medicina Prepagada, IPS, Fosyga, etc.); si P.TIPCOBSAL 1..8 → TipoPaciente: CONTRIBUTIVO / SUBSIDIADO / POBLACION POBRE / DESPLAZADOS / etc. else ''DESCONOCIDO''; si CDD.Name IS NOT NULL → DescripcionServicio toma la descripción contractual (ContractDescriptions) else Usa COALESCE(CUPS.Description, SERVICIOSIPS.NAME, PR.NAME)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewFacturacionPendiente';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewFacturacionPendiente';
GO
