-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-11-12
-- Description:	Procedimiento que se encarga de el generar los datos relacionados al archivo FURIPS1
-- =============================================
CREATE PROCEDURE [Glosas].[SP_GenerateFURIPS1FileData] 
	@XmlParameters as XML,
	@XmlInvoices AS XML
AS
BEGIN
	SET NOCOUNT ON;
	
	/***************************************** VARIABLES *****************************************/

	--Variables de control
	DECLARE	@RadicateInvoiceId INT,
			---------------------------------------------------------------------------------------
			@FilterByInvoices BIT = 0

	--Tabla para almacenar los items del listado que viene en el xml
	DECLARE @Table_Invoices TABLE(InvoiceId INT)

	--Facturas a generar
	DECLARE @Invoices TABLE
	(
		InvoiceId INT,
		InvoiceNumber VARCHAR(20),
		DetailInvoiceId INT,
		DetailInvoiceNumber VARCHAR(20)
	)

	BEGIN TRY

		/*************************************** CRITERIOS ***************************************/

		SELECT	@RadicateInvoiceId = t.x.value('RadicateInvoiceId[1]','int')
		FROM @XmlParameters.nodes('/Data') t(x)

		INSERT INTO @Table_Invoices
			SELECT DISTINCT
				t.x.value('InvoiceId[1]','int') InvoiceId
			FROM @XmlInvoices.nodes('/Data') t(x)

		IF EXISTS(SELECT 1 FROM @Table_Invoices)
		BEGIN
			SET @FilterByInvoices = 1
		END

		INSERT INTO @Invoices 
			(
				InvoiceId, InvoiceNumber, DetailInvoiceId, DetailInvoiceNumber
			)
			SELECT DISTINCT
				i.Id InvoiceId,
				i.InvoiceNumber InvoiceDate,
				IIF(i.DocumentType = 4, cc.Id, i.Id) DetailInvoiceId,
				IIF(i.DocumentType = 4, cc.InvoiceNumber, i.InvoiceNumber) DetailInvoiceNumber
			FROM @Table_Invoices ti
			JOIN Billing.Invoice i ON ti.InvoiceId = i.Id
			LEFT JOIN Billing.Invoice cc WITH (NOLOCK) ON i.DocumentType = 4 AND cc.DocumentType = 5 AND cc.Status = 1
				AND i.ThirdPartyId = cc.ThirdPartyId
				AND i.CareGroupId = cc.CareGroupId
				AND i.InvoiceCategoryId = cc.InvoiceCategoryId
				AND CAST(cc.InvoiceDate AS DATE) BETWEEN i.CapitationInitialDate AND i.CapitationEndDate
			WHERE ISNULL(@RadicateInvoiceId, 0) = 0
		UNION ALL
			SELECT DISTINCT 
				i.Id InvoiceId,
				i.InvoiceNumber InvoiceDate,
				IIF(i.DocumentType = 4, cc.Id, i.Id) DetailInvoiceId,
				IIF(i.DocumentType = 4, cc.InvoiceNumber, i.InvoiceNumber) DetailInvoiceNumber
			FROM Portfolio.RadicateInvoiceC ri WITH (NOLOCK)
			JOIN Portfolio.RadicateInvoiceD rid WITH (NOLOCK) ON ri.Id = rid.RadicateInvoiceCId AND	rid.State <> 4
			JOIN Billing.Invoice i WITH (NOLOCK) ON rid.InvoiceNumber = i.InvoiceNumber AND i.Status = 1
			LEFT JOIN @Table_Invoices ti ON i.Id = ti.InvoiceId
			LEFT JOIN Billing.Invoice cc WITH (NOLOCK) ON i.DocumentType = 4 AND cc.DocumentType = 5 AND cc.Status = 1
				AND i.ThirdPartyId = cc.ThirdPartyId
				AND i.CareGroupId = cc.CareGroupId
				AND i.InvoiceCategoryId = cc.InvoiceCategoryId
				AND CAST(cc.InvoiceDate AS DATE) BETWEEN i.CapitationInitialDate AND i.CapitationEndDate
			WHERE ri.Id = @RadicateInvoiceId
				AND (@FilterByInvoices = 0 OR ti.InvoiceId IS NOT NULL)

		/*****************************************************************************************/

		-- Se agrega el valor del copago y cuota moderadora al primer registro de cada factura
		SELECT	LTRIM(RTRIM(NUMRADANT)) as NumeroRadicadoAnterior, 
				ISNULL(LTRIM(RTRIM(RESGLO)),'') as RespuestaGlosa, 
				LTRIM(RTRIM(I.InvoiceNumber)) AS NumeroFactura, 
				LTRIM(RTRIM(NUMCONREC)) as ConsecutivoReclamacion,
				ca.CODCENATE as codigo,
				CA.CODIPSSEC AS PrestadorServicioSalud, 
				PRIAPEVIC as PrimerApellidoVictima, 
				SEGAPEVIC as SegundoApellidoVictima, 
				PRINOMVIC as PrimerNombreVictima, 
				SEGNOMVIC as SegundoNombreVictima, 
				LTRIM(RTRIM(TIPDOCVIC)) as TipoDocumentoVictima, 
				RTRIM(LTRIM(NumDocVic)) as NumeroDocumentoVictima, 
				CONVERT(VARCHAR(10), FECNACVIC,103) as FechaNacimientoVictima,
				ISNULL(CONVERT(VARCHAR(10), FECMUEPAC,103), '') as FechaFallecimientoVictima,
				SEXVIC as SexoVictima, 
				left(LTRIM(RTRIM(DIRRESVIC)),100)as DireccionVictima, --15
				CODDEPVIC AS DepartamentoResidenciaVictima, 
				CODMUNVIC as CodigoMunicipioVictima, 
				LTRIM(RTRIM(TELVIC)) as TelefonoVictima, 
				CONVIC as CondicionVictima, 
				CASE NATEVE 
					WHEN 1 THEN '01' 
					WHEN 2 THEN '02' 
					WHEN 3 THEN '03' 
					WHEN 4 THEN '04' 
					WHEN 5 THEN '05' 
					WHEN 6 THEN '06' 
					WHEN 7 THEN '07' 
					WHEN 8 THEN '08' 
					WHEN 9 THEN '09' 
					WHEN 10 THEN '10' 
					WHEN 11 THEN '11' 
					WHEN 12 THEN '12' 
					WHEN 13 THEN '13' 
					WHEN 14 THEN '15' 
					WHEN 15 THEN '16' 
					WHEN 16 THEN '17' 
				END AS NaturalezaEvento, 
				IIF(NATEVE = 16, REPLACE(REPLACE(DESEVE, ',', ' '), '"', ' '),'')as DescripcionOtherEvento, --21
				left(RTRIM(DIROCUEVE), 100)as DireccionEvento, --22
				CONVERT(VARCHAR(10),FECOCUEVE,103) as FechaEvento, 
				LTRIM(RTRIM(HOROCUEVE)) as HoraEvento, 
				CODDEPEVE as DepartamentoEvento, 
				CODMUNEVE as CiudadEvento, 
				ZONOCUEVE as ZonaEvento, 
				ESTASE as EstadoAsegurado, --28
				ISNULL(LTRIM(RTRIM(MARCA)),'') as Marca, 
				ISNULL(LTRIM(RTRIM(PLATRAVIC)),'') AS PlacaTrasladoInterinstitucional,
				ISNULL(LTRIM(RTRIM(PLAVEHACC)),'') AS Placa,
				ISNULL(TIPVEH,'') as TipoVehiculo, 
				ISNULL(LTRIM(RTRIM(REPLACE(REPLACE(CODASE, ' ', ''),'-', ''))),'') as CodigoAseguradora, 
				ISNULL(LTRIM(RTRIM(NUMSOA)),'') as NumeroSOAT, 
				ISNULL(CONVERT(VARCHAR(10),FECINIPOL,103),'') as FechaInicioVigenciaPoliza, 
				ISNULL(CONVERT(VARCHAR(10),FECFINPOL,103),'') as FechaFinVigenciaPoliza, 
				ISNULL(FiledSIRAS, '') as NumeroRadicadoSIRAS,
				ISNULL(LTRIM(RTRIM(INTAUT)),'') as IntervencionAutoridad,
				ISNULL(LTRIM(RTRIM(COBEXCPOL)),'') as CobroExcedente,
				ISNULL(LTRIM(RTRIM(MainHospitalizationServiceCode)),'') as CodigoCupsServicio,
				ISNULL(CONVERT(VARCHAR(1),ComplexitySurgicalProcedure,103), '') as ComplejidadProcedimientoQuirurgico,
				ISNULL(LTRIM(RTRIM(MainSurgicalService)),'') as CodigoCupsProcedimientoQuirurgicoPrincipal,
				ISNULL(LTRIM(RTRIM(SecondarySurgicalProcedure)),'') as CodigoCupsProcedimientoQuirurgicoSecundario,
				CAST(ISNULL(ServiceUCI, '') AS VARCHAR(1)) as SePrestoServicioUCI,
				ISNULL(DaysStayUCI,'') as DiasDeUciReclamados,
				ISNULL(LTRIM(RTRIM(TIPDOCPRO)),'') as TipoDocumentoPropietario, 
				ISNULL(LTRIM(RTRIM(NUMDOCPRO)),'') as NumeroDocumentoPropietario, --45
				ISNULL(LTRIM(RTRIM(PRIAPEPRO)),'') as PrimerApellidoPropietario, 
				ISNULL(LTRIM(RTRIM(SEGAPEPRO)),'') as SegundoApellidoPropietario, 
				ISNULL(LTRIM(RTRIM(PRINOMPRO)),'') as PrimerNombrePropietario, 
				ISNULL(LTRIM(RTRIM(SEGNOMPRO)),'') as SegundoNombrePropietario, 
				ISNULL(LEFT(LTRIM(RTRIM(DIRRESPRO)),200),'') as DireccionPropietario, --50 
				ISNULL(LTRIM(RTRIM(TELRESPRO)),'') as TelefonoPropietario, 
				ISNULL(LTRIM(RTRIM(CODDEPPRO)),'') as DepartamentoPropietario, 
				ISNULL(LTRIM(RTRIM(CODMUNPRO)),'') as MunicipioPropietario, 
				ISNULL(LTRIM(RTRIM(PRIAPECON)),'') as PrimerApellidoConductor, 
				ISNULL(LTRIM(RTRIM(SEGAPECON)),'') as SegundoApellidoConductor, 
				ISNULL(LTRIM(RTRIM(PRINOMCON)),'') as PrimerNombreConductor, 
				ISNULL(LTRIM(RTRIM(SEGNOMCON)),'') as SegundoNombreConductor, 
				ISNULL(LTRIM(RTRIM(TIPDOCCON)),'') as TipoDocumentoConductor, 
				ISNULL(LTRIM(RTRIM(NUMDOCCON)),'') as NumeroDocumentoConductor, 
				ISNULL(LEFT(LTRIM(RTRIM(DIRRESCON)),200),'') as DireccionConductor, 
				ISNULL(LTRIM(RTRIM(CODDEPCON)),'') as DepartamentoConductor, 
				ISNULL(LTRIM(RTRIM(CODMUNCON)),'') as MunicipioConductor, 
				ISNULL(LTRIM(RTRIM(TELRESCON)),'') as TelefonoConductor, 
				ISNULL(LTRIM(RTRIM(TIPREF)),'') as TipoReferencia, 
				ISNULL(CONVERT(VARCHAR(10),FECREM,103),'') as FechaRemison, 
				ISNULL(LTRIM(RTRIM(HORSAL)),'') as HoraSalida, 
				ISNULL(LTRIM(RTRIM(CODHABLENV)),'') as CodigoHabilitacionPrestador, 
				ISNULL(LTRIM(RTRIM(PROREM)),'') AS ProfesionalRemite, 
				ISNULL(LTRIM(RTRIM(CARPERREM)),'') AS CargoProfesionalRemite,
				ISNULL(CONVERT(VARCHAR(10),FECING,103),'') as FechaIngreso, 
				ISNULL(LTRIM(RTRIM(HORING)),'') AS HoraIngreso,
				ISNULL(LTRIM(RTRIM(CODHABREC)),'') as CodigoHabilitacionRecibe, 
				ISNULL(LTRIM(RTRIM(PROREC)),'') as ProfesionalRecibe, 
				ISNULL(LTRIM(RTRIM(CARPERREC)),'') as CargoProfesionalRecibe, 
				ISNULL(FURIPS.AmbulancePlate,'') as PlacaAmbulancia,
				ISNULL(LTRIM(RTRIM(TRASITEVE)),'') as TransporteDesde, 
				ISNULL(LTRIM(RTRIM(TRAFINREC)),'') as TransporteHasta, 
				ISNULL(LTRIM(RTRIM(TIPSERAMB)),'') as TipoServicioAmbulancia, 
				ISNULL(LTRIM(RTRIM(ZONRECVIC)),'') as ZonaRecogeAmbulancia, 
				ISNULL(CONVERT(VARCHAR(10),FECDEING,103),'') as FechaIngresoVictima, 
				ISNULL(LTRIM(RTRIM(HORDEING)),'') as HoraIngresoVictima, 
				ISNULL(CONVERT(VARCHAR(10),FECEGR,103),'') as FechaEgresoVictima, 
				ISNULL(LTRIM(RTRIM(HOREGR)),'') as HoraEgresoVictima, 
				ISNULL(LTRIM(RTRIM(FURIPS.CODDIAING)),'') as CodigoDiagnosticoIngresoPrincipal, 
				ISNULL(LTRIM(RTRIM(CODINGAS1)),'') as CodigoDiagnosticoIngresoUno, 
				ISNULL(LTRIM(RTRIM(CODINGAS2)),'') as CodigoDiagnosticoIngresoDos, 
				ISNULL(LTRIM(RTRIM(FURIPS.CODDIAEGR)),'') as CodigoDiagnosticoEgresoPrincipal, 
				ISNULL(LTRIM(RTRIM(CODEGRAS1)),'') as CodigoDiagnosticoEgresoUno, 
				ISNULL(LTRIM(RTRIM(CODEGRAS2)),'') as CodigoDiagnosticoEgresoDos,
				ISNULL(LTRIM(RTRIM(PRIAPEMED)),'') as PrimerApellidoMedico, 
				ISNULL(LTRIM(RTRIM(SEGAPEMED)),'') as SegundoApellidoMedico, 
				ISNULL(LTRIM(RTRIM(PRINOMMED)),'') as PrimerNombreMedico, 
				ISNULL(LTRIM(RTRIM(SEGNOMMED)),'') as SegundoNombreMedico, 
				ISNULL(LTRIM(RTRIM(TIPDOCMED)),'') as TipoDocumentoMedico, 
				RTRIM(LTRIM(substring(NUMDOCMED, patindex('%[^0]%',NUMDOCMED), 10))) as NumeroDocumentoMedico, 
				ISNULL(LTRIM(RTRIM(NUMREGMED)),'') as NumeroRegistroMedico, 
				ISNULL(LTRIM(RTRIM(FACGASMED)),'') as TotalFacturadoGastosMedicos, 
				ISNULL(LTRIM(RTRIM(FACGASMED)),'') as TotalReclamadoGastosMedicos, 
				ISNULL(NULLIF(LTRIM(RTRIM(FACGASTRA)),''),'0') as TotalFacturadoMovilizacion, 
				ISNULL(NULLIF(LTRIM(RTRIM(RECGASTRA)),''),'0') as TotalReclamadoMovilizacion, 
				ISNULL(NULLIF(LTRIM(RTRIM(FURIPS.NUMEFOLIO)),''),'0') as TotalFolios,
				CAST(ISNULL(ManifestationService, '') AS VARCHAR(1)) as ManifestacionServiciosHabilitados,
				REPLACE(REPLACE(REPLACE(DESEVEACC, ',', ' '), '"', ' '), '    ', ' ') as DescripcionEvento
		FROM @Invoices i
		JOIN  Billing.Invoice ii on ii.id = i.InvoiceId	
		JOIN dbo.ADFURIPSU FURIPS WITH (NOLOCK) ON i.InvoiceId = FURIPS.IdInvoice
		JOIN dbo.ADINGRESO Ingreso WITH (NOLOCK) ON ii.AdmissionNumber = Ingreso.NUMINGRES  															
		JOIN dbo.[ADCENATEN] CA WITH (NOLOCK) ON CA.CODCENATE = Ingreso.CODCENATE
		LEFT JOIN dbo.HCREGEGRE Egreso WITH (NOLOCK) ON FURIPS.NUMINGRES = Egreso.NUMINGRES
		ORDER BY CAST(SUBSTRING(i.InvoiceNumber + '0', PATINDEX('%[0-9]%', i.InvoiceNumber + '0'), LEN(i.InvoiceNumber + '0')) AS DECIMAL)
	END TRY
	BEGIN CATCH
		PRINT ERROR_MESSAGE() + ' Linea: ' + cast(ERROR_LINE() AS VARCHAR(20))
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera los datos del archivo FURIPS1 (Fondo de Atención de Urgencias de Víctimas de Accidentes de Tránsito), que es el reporte estructurado requerido para el cobro y radicación de facturas ante el SOAT y fondos especiales. Recibe como entrada un XML con parámetros de filtro (identificador de radicado) y un XML con las facturas a procesar, combinando información de facturas de cobro (Billing.Invoice), radicados de cartera (Portfolio.RadicateInvoiceC y RadicateInvoiceD) y datos clínicos del evento (víctima, fecha, lugar, vehículo, aseguradora, diagnóstico, servicios prestados). El procedimiento resuelve tanto facturas individuales filtradas por lista como todas las facturas asociadas a un radicado de cobro específico, incluyendo la lógica de capitación para facturas de tipo 4, y produce el conjunto de campos exigidos por el estándar RIPS/SOAT para su posterior exportación o transmisión.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateFURIPS1FileData';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateFURIPS1FileData';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el conjunto de datos del archivo FURIPS1 (Formulario Único de Reclamación SOAT/ADRES) consolidando información de la víctima, evento, vehículo, traslado e ingreso clínico para las facturas seleccionadas o radicadas.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateFURIPS1FileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe recibirse un XML de parámetros con el nodo Data/RadicateInvoiceId (puede ser nulo o 0).; Debe recibirse un XML con el listado de facturas (puede estar vacío para no filtrar por facturas).; Las facturas deben tener registro asociado en dbo.ADFURIPSU vinculado por IdInvoice.; Las facturas deben tener un AdmissionNumber válido que exista en dbo.ADINGRESO.; El centro de atención del ingreso debe existir en dbo.ADCENATEN.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateFURIPS1FileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran facturas con Status=1 en la rama de radicado.; Solo se consideran detalles de radicación con State<>4 (excluye anulados/rechazados).; La búsqueda de factura complementaria capitada exige coincidencia exacta de tercero, grupo de atención y categoría de factura, dentro de la vigencia de capitación.; Todos los campos de texto se entregan con LTRIM/RTRIM e ISNULL para evitar nulos en el archivo plano.; Las fechas se entregan en formato 103 (dd/mm/yyyy).; El número de documento del médico se normaliza eliminando ceros a la izquierda y limitándose a 10 caracteres.; Las direcciones de víctima/evento se truncan a 100 caracteres y las de propietario/conductor a 200.; Los totales de movilización y folios se devuelven ''0'' cuando están vacíos o nulos.; El total facturado y reclamado de gastos médicos se reportan con el mismo valor (FACGASMED).; La descripción del evento se sanitiza removiendo comas, comillas dobles y espacios cuádruples.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateFURIPS1FileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'FURIPS (Formulario Único de Reclamación SOAT/ADRES); Accidente de tránsito; Víctima; Propietario del vehículo; Conductor; SOAT (póliza); Aseguradora; Radicación de facturas; Factura capitada y nota complementaria; Ingreso/admisión hospitalaria; Egreso hospitalario; Diagnósticos de ingreso y egreso (CIE); Procedimientos quirúrgicos (CUPS); Servicio de UCI; Traslado en ambulancia; Centro de atención / prestador de servicios de salud; SIRAS', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateFURIPS1FileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve un único conjunto de resultados con los campos del FURIPS1 por cada factura encontrada, ordenado por la parte numérica del InvoiceNumber.; [RAISERROR] resultset: Ante cualquier error en TRY, en lugar de propagar la excepción imprime el mensaje y la línea con PRINT (no lanza error al cliente).', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateFURIPS1FileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existen filas en el listado de facturas del XML (@Table_Invoices no vacío) → Activa @FilterByInvoices=1 para restringir el resultado solo a esas facturas cuando se consulta por radicado. else No filtra por listado de facturas y trae todas las facturas del radicado.; si ISNULL(@RadicateInvoiceId,0) = 0 → Procesa las facturas indicadas en el XML uniéndolas con Billing.Invoice. else Procesa las facturas asociadas al radicado de cartera (Portfolio.RadicateInvoiceC/D) con detalle State<>4 y factura Status=1.; si Billing.Invoice.DocumentType = 4 (factura capitada) → Busca la nota/factura complementaria con DocumentType=5, Status=1, mismo tercero/grupo/categoría y cuya InvoiceDate caiga entre CapitationInitialDate y CapitationEndDate, y usa esa como detalle. else Usa la propia factura como detalle (DetailInvoiceId=Id).; si NATEVE entre 1 y 16 → Mapea el código interno de naturaleza del evento al código oficial FURIPS (''01''..''13'',''15'',''16'',''17''); el valor 14 se mapea a ''15'' y se omite ''14''.; si NATEVE = 16 (otro evento) → Incluye DescripcionOtherEvento limpiando comas y comillas dobles del campo DESEVE. else DescripcionOtherEvento queda en cadena vacía.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateFURIPS1FileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; Portfolio.RadicateInvoiceC; Portfolio.RadicateInvoiceD; dbo.ADFURIPSU; dbo.ADINGRESO; dbo.ADCENATEN; dbo.HCREGEGRE', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateFURIPS1FileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateFURIPS1FileData';
-- GO
