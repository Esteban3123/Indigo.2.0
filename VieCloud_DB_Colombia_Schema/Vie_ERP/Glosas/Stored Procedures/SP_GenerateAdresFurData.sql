-- =============================================
-- Author:		Felix Salazar
-- Create date: 2026-05-24
-- Description:	Genera los datos del archivo FUR (Formulario Único de Reclamaciones) conforme a la Circular Externa 003 de 2026 de ADRES.
-- =============================================
CREATE PROCEDURE [Glosas].[SP_GenerateAdresFurData]
	@XmlParameters AS XML,
	@XmlInvoices AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE	@RadicateInvoiceId INT,
			@FilterByInvoices BIT = 0

	DECLARE @Table_Invoices TABLE (InvoiceId INT)

	DECLARE @Invoices TABLE
	(
		InvoiceId INT,
		InvoiceNumber VARCHAR(20),
		DetailInvoiceId INT,
		DetailInvoiceNumber VARCHAR(20)
	)

	BEGIN TRY

		/*************************************** CRITERIOS ***************************************/

		SELECT @RadicateInvoiceId = t.x.value('RadicateInvoiceId[1]','int')
		FROM @XmlParameters.nodes('/Data') t(x)

		INSERT INTO @Table_Invoices
			SELECT DISTINCT t.x.value('InvoiceId[1]','int') InvoiceId
			FROM @XmlInvoices.nodes('/Data') t(x)

		IF EXISTS(SELECT 1 FROM @Table_Invoices)
		BEGIN
			SET @FilterByInvoices = 1
		END

		INSERT INTO @Invoices
			(InvoiceId, InvoiceNumber, DetailInvoiceId, DetailInvoiceNumber)
			SELECT DISTINCT
				i.Id,
				i.InvoiceNumber,
				IIF(i.DocumentType = 4, cc.Id, i.Id),
				IIF(i.DocumentType = 4, cc.InvoiceNumber, i.InvoiceNumber)
			FROM @Table_Invoices ti
			JOIN Billing.Invoice i ON ti.InvoiceId = i.Id
			LEFT JOIN Billing.Invoice cc ON i.DocumentType = 4 AND cc.DocumentType = 5 AND cc.Status = 1
				AND i.ThirdPartyId = cc.ThirdPartyId
				AND i.CareGroupId = cc.CareGroupId
				AND i.InvoiceCategoryId = cc.InvoiceCategoryId
				AND CAST(cc.InvoiceDate AS DATE) BETWEEN i.CapitationInitialDate AND i.CapitationEndDate
			WHERE ISNULL(@RadicateInvoiceId, 0) = 0
		UNION ALL
			SELECT DISTINCT
				i.Id,
				i.InvoiceNumber,
				IIF(i.DocumentType = 4, cc.Id, i.Id),
				IIF(i.DocumentType = 4, cc.InvoiceNumber, i.InvoiceNumber)
			FROM Portfolio.RadicateInvoiceC ri
			JOIN Portfolio.RadicateInvoiceD rid ON ri.Id = rid.RadicateInvoiceCId AND rid.State <> 4
			JOIN Billing.Invoice i ON rid.InvoiceNumber = i.InvoiceNumber AND i.Status = 1
			LEFT JOIN @Table_Invoices ti ON i.Id = ti.InvoiceId
			LEFT JOIN Billing.Invoice cc ON i.DocumentType = 4 AND cc.DocumentType = 5 AND cc.Status = 1
				AND i.ThirdPartyId = cc.ThirdPartyId
				AND i.CareGroupId = cc.CareGroupId
				AND i.InvoiceCategoryId = cc.InvoiceCategoryId
				AND CAST(cc.InvoiceDate AS DATE) BETWEEN i.CapitationInitialDate AND i.CapitationEndDate
			WHERE ri.Id = @RadicateInvoiceId
				AND (@FilterByInvoices = 0 OR ti.InvoiceId IS NOT NULL)

		/*************************************** SELECT FUR ***************************************/

		;WITH FurData AS
		(
			-- =============================================================================
			-- tabla NUEVA dbo.ADFURIPS (Circular 003 ADRES - EHR)
			-- =============================================================================
			SELECT
				i.InvoiceId AS InvoiceId,
				LTRIM(RTRIM(ii.InvoiceNumber)) AS NUM_FACTURA,

				-- Datos víctima
				-- Los tipos de documento de ADFURIPS almacenan el CODIGO del catálogo;
				-- para ADRES se exporta su SIGLA vigente en dbo.ADTIPOIDENTIFICA.
				(
					SELECT TOP (1) LTRIM(RTRIM(ti.SIGLA))
					FROM dbo.ADTIPOIDENTIFICA ti
					WHERE ti.CODIGO = f.PatientDocType
					ORDER BY ti.ESTADO DESC, ti.ID DESC
				) AS Tipo_documento_identidad_victima,
				LTRIM(RTRIM(f.PatientCode)) AS Numero_documento_identidad_victima,
				LTRIM(RTRIM(f.SpecialPopulationType)) AS Tipo_de_poblacion_especial,
				LTRIM(RTRIM(f.PatientFirstName)) AS Primer_nombre_victima,
				LTRIM(RTRIM(f.PatientSecondName)) AS Segundo_nombre_victima,
				LTRIM(RTRIM(f.PatientFirstLastName)) AS Primer_apellido_victima,
				LTRIM(RTRIM(f.PatientSecondLastName)) AS Segundo_apellido_victima,
				LEFT(LTRIM(RTRIM(f.PatientAddress)), 100) AS Direccion_residencia_victima,
				LTRIM(RTRIM( isnull(u.DEPMUNCOD,f.PatientCityCode))) AS Codigo_municipio_residencia_victima,
				LTRIM(RTRIM(f.PatientPhone)) AS Telefono_victima,

				-- Datos sitio evento
				LTRIM(RTRIM(f.EventNature)) AS Naturaleza_del_evento,
				IIF(LTRIM(RTRIM(f.EventNature)) = '17',
								REPLACE(REPLACE(LTRIM(RTRIM(f.OtherEventDescription)), ',', ' '), '"', ' '),
								NULL) AS Descripcion_del_otro_evento,
				LTRIM(RTRIM(f.VictimCondition)) AS Condicion_victima,
				CONVERT(VARCHAR(10), f.EventDate, 23) AS Fecha_de_ocurrencia_evento,
				LTRIM(RTRIM(f.EventZone)) AS Zona_de_ocurrencia_evento,
				LTRIM(RTRIM(f.EventDepartment + f.EventCity)) AS Codigo_municipio_ocurrencia_evento,
				LEFT(LTRIM(RTRIM(f.EventAddress)), 100) AS Direccion_de_ocurrencia_evento,
				REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(f.EventShortDescription)), ',', ' '), '"', ' '), '    ', ' ') AS Descripcion_corta_de_lo_ocurrido_en_el_evento,

				-- Datos vehículo (solo cuando EventNature = '01')
				CASE WHEN f.EventNature = '01' THEN LTRIM(RTRIM(f.InsuranceStatus)) END AS Estado_de_aseguramiento,
				CASE WHEN f.EventNature = '01' THEN LTRIM(RTRIM(f.VehiclePlateNumber)) END AS Placa_vehiculo,
				CASE WHEN f.EventNature = '01' THEN LTRIM(RTRIM(f.VehicleType)) END AS Tipo_de_Vehiculo,
				CASE WHEN f.EventNature = '01' THEN LTRIM(RTRIM(f.InsurerCode)) END AS Codigo_de_la_aseguradora,
				CASE WHEN f.EventNature = '01' THEN LTRIM(RTRIM(f.SoatPolicyNumber)) END AS Numero_de_poliza_SOAT,
				CASE WHEN f.EventNature = '01' THEN CONVERT(VARCHAR(10), f.PolicyStartDate, 23) END AS Fecha_de_inicio_de_vigencia_de_la_poliza,
				CASE WHEN f.EventNature = '01' THEN CONVERT(VARCHAR(10), f.PolicyEndDate, 23) END AS Fecha_final_de_vigencia_de_la_poliza,
				CASE WHEN f.EventNature = '01' THEN LTRIM(RTRIM(f.SirasNumber)) END AS Numero_de_radicado_SIRAS,
				CASE
					WHEN f.EventNature = '01'
						 AND f.InsurerCeilingCharge IN (0, 1)
					THEN CONVERT(tinyint, f.InsurerCeilingCharge)
					ELSE NULL
				END AS Cobro_por_agotamiento_tope_Aseguradora,
				-- Datos propietario (solo cuando EventNature = '01')
				CASE WHEN f.EventNature = '01' THEN (
					SELECT TOP (1) LTRIM(RTRIM(ti.SIGLA))
					FROM dbo.ADTIPOIDENTIFICA ti
					WHERE ti.CODIGO = TRY_CAST(f.OwnerDocType AS INT)
					ORDER BY ti.ESTADO DESC, ti.ID DESC
				) END AS Tipo_de_documento_de_identidad_del_propietario,
				CASE WHEN f.EventNature = '01' THEN LTRIM(RTRIM(f.OwnerDocNumber)) END AS Numero_de_documento_de_identidad_del_propietario,
				CASE WHEN f.EventNature = '01' THEN LTRIM(RTRIM(f.OwnerFirstName)) END AS Primer_nombre_del_propietario_o_razon_social,
				CASE WHEN f.EventNature = '01' THEN LTRIM(RTRIM(f.OwnerSecondName)) END AS Segundo_nombre_del_propietario,
				CASE WHEN f.EventNature = '01' THEN LTRIM(RTRIM(f.OwnerFirstLastName)) END AS Primer_apellido_del_propietario,
				CASE WHEN f.EventNature = '01' THEN LTRIM(RTRIM(f.OwnerSecondLastName)) END AS Segundo_apellido_del_propietario,
				CASE WHEN f.EventNature = '01' THEN LEFT(LTRIM(RTRIM(f.OwnerAddress)), 100) END AS Direccion_de_residencia_del_propietario,
				CASE WHEN f.EventNature = '01' THEN LTRIM(RTRIM(f.OwnerPhone)) END AS Telefono_de_residencia_del_propietario,
				CASE WHEN f.EventNature = '01' THEN LTRIM(RTRIM(f.OwnerDepartment + f.OwnerCity)) END AS Codigo_del_municipio_de_residencia_del_propietario,

				-- Datos conductor (solo cuando EventNature = '01')
				CASE WHEN f.EventNature = '01' THEN (
					SELECT TOP (1) LTRIM(RTRIM(ti.SIGLA))
					FROM dbo.ADTIPOIDENTIFICA ti
					WHERE ti.CODIGO = TRY_CAST(f.DriverDocType AS INT)
					ORDER BY ti.ESTADO DESC, ti.ID DESC
				) END AS Tipo_de_documento_de_identidad_del_conductor,
				CASE WHEN f.EventNature = '01' THEN LTRIM(RTRIM(f.DriverDocNumber)) END AS Numero_de_documento_de_identidad_del_conductor,
				CASE WHEN f.EventNature = '01' THEN LTRIM(RTRIM(f.DriverFirstName)) END AS Primer_nombre_del_conductor,
				CASE WHEN f.EventNature = '01' THEN LTRIM(RTRIM(f.DriverSecondName)) END AS Segundo_nombre_del_conductor,
				CASE WHEN f.EventNature = '01' THEN LTRIM(RTRIM(f.DriverFirstLastName)) END AS Primer_apellido_del_conductor,
				CASE WHEN f.EventNature = '01' THEN LTRIM(RTRIM(f.DriverSecondLastName)) END AS Segundo_apellido_del_conductor,
				CASE WHEN f.EventNature = '01' THEN LTRIM(RTRIM(f.DriverDepartment + f.DriverCity)) END AS Codigo_del_municipio_de_residencia_del_conductor,
				CASE WHEN f.EventNature = '01' THEN LEFT(LTRIM(RTRIM(f.DriverAddress)), 100) END AS Direccion_de_residencia_del_conductor,
				CASE WHEN f.EventNature = '01' THEN LTRIM(RTRIM(f.DriverPhone)) END AS Telefono_de_residencia_del_conductor,

				-- Datos atención víctima (ya disponibles en ADFURIPS)
				CASE
					WHEN f.OsteosynthesisMaterial = 1 THEN CONVERT(tinyint, 1)
					WHEN f.OsteosynthesisMaterial = 0 THEN CONVERT(tinyint, 2)
					ELSE CONVERT(tinyint, 2)
				END AS Uso_material_de_osteosintesis_en_la_atencion,
				LTRIM(RTRIM(f.AttendanceType)) AS Es_atencion_inicial_paciente_remitido_o_control,

				-- Datos remisión (ya disponibles en ADFURIPS)
				LTRIM(RTRIM(f.ReferringProviderCode)) AS Codigo_de_habilitacion_del_prestador_que_remite,
				(
					SELECT TOP (1) LTRIM(RTRIM(ti.SIGLA))
					FROM dbo.ADTIPOIDENTIFICA ti
					WHERE ti.CODIGO = TRY_CAST(f.ReceivingProfessionalDocType AS INT)
					ORDER BY ti.ESTADO DESC, ti.ID DESC
				) AS TIPO_de_documento_Profesional_que_recibe,
				LTRIM(RTRIM(f.ReceivingProfessionalDocNumber)) AS Numero_de_documento_Profesional_que_recibe,
				LTRIM(RTRIM(f.ReceivingProviderCode)) AS Codigo_de_habilitacion_del_prestador_que_recibe,
				CONVERT(VARCHAR(10), f.AcceptanceDate, 23) AS Fecha_de_aceptacion,
				LEFT(CONVERT(VARCHAR(8), f.AcceptanceTime, 108), 5) AS Hora_aceptacion,
				LTRIM(RTRIM(f.SecondaryTransportPlate)) AS Placa_ambulancia_que_realiza_el_traslado_secundario,
				CASE
					WHEN LTRIM(RTRIM(f.AttendanceType)) IN ('3', '7', '8')
						 AND LTRIM(RTRIM(f.SecondaryTransportType)) IN ('1', '2')
					THEN LTRIM(RTRIM(f.SecondaryTransportType))
					ELSE NULL
				END AS Tipo_de_servicio_del_transporte_secundario,

				-- Datos transporte primario (ya disponibles en ADFURIPS)
				LTRIM(RTRIM(f.PrimaryTransportPlate)) AS Placa_ambulancia_que_realiza_el_traslado,
				LTRIM(RTRIM(f.ReceptorProviderCode)) AS Codigo_de_habilitacion_del_prestador_que_recibe_transporte_primario,
				LEFT(LTRIM(RTRIM(f.EventSiteAddress)), 100) AS Transporte_de_la_victima_desde_el_sitio_del_evento_Direccion,
				LEFT(LTRIM(RTRIM(f.DestinationIpsAddress)), 100) AS Transporte_de_la_victima_hasta_el_fin_del_recorrido_direccion_IPS,
				CASE
					WHEN LTRIM(RTRIM(f.AttendanceType)) IN ('2', '6', '8')
						 AND LTRIM(RTRIM(f.PrimaryTransportType)) IN ('1', '2')
					THEN LTRIM(RTRIM(f.PrimaryTransportType))
					ELSE NULL
				END AS Tipo_de_servicio_del_transporte
			FROM @Invoices i
			JOIN Billing.Invoice ii ON ii.Id = i.InvoiceId
			INNER JOIN ADINGRESO AI ON AI.NUMINGRES = II.AdmissionNumber
			INNER JOIN dbo.INPACIENT a ON A.IPCODPACI = AI.IPCODPACI
			LEFT JOIN .dbo.INUBICACI u ON u.AUUBICACI = a.AUUBICACI
			-- Tomamos solo el ADFURIPS más reciente confirmado por factura
			-- (ADFURIPSInvoice tiene histórico de cambios de status)
			JOIN (
				SELECT InvoiceId, MAX(ADFURIPSId) AS ADFURIPSId
				FROM dbo.ADFURIPSInvoice
				WHERE StatusTo = 1
				GROUP BY InvoiceId
			) fi ON fi.InvoiceId = i.InvoiceId
			JOIN dbo.ADFURIPS f ON fi.ADFURIPSId = f.Id

			UNION ALL

			-- =============================================================================
			-- tabla antigua dbo.ADFURIPSU (FURIPS clásico) Solo se aplica cuando la factura NO tiene registro confirmado en la nueva.
			-- Campos exclusivos de Circular 003 quedan NULL (no existen en ADFURIPSU).
			-- =============================================================================
			SELECT
				i.InvoiceId AS InvoiceId,
				LTRIM(RTRIM(ii.InvoiceNumber)) AS NUM_FACTURA,

				LTRIM(RTRIM(FURIPS.TIPDOCVIC)) AS Tipo_documento_identidad_victima,
				LTRIM(RTRIM(FURIPS.NUMDOCVIC)) AS Numero_documento_identidad_victima,
				CAST(NULL AS VARCHAR(5)) AS Tipo_de_poblacion_especial,
				LTRIM(RTRIM(FURIPS.PRINOMVIC)) AS Primer_nombre_victima,
				LTRIM(RTRIM(FURIPS.SEGNOMVIC)) AS Segundo_nombre_victima,
				LTRIM(RTRIM(FURIPS.PRIAPEVIC)) AS Primer_apellido_victima,
				LTRIM(RTRIM(FURIPS.SEGAPEVIC)) AS Segundo_apellido_victima,
				LEFT(LTRIM(RTRIM(FURIPS.DIRRESVIC)), 100) AS Direccion_residencia_victima,
				MunVic.DEPMUNCOD AS Codigo_municipio_residencia_victima,
				LTRIM(RTRIM(FURIPS.TELVIC)) AS Telefono_victima,

				CASE FURIPS.NATEVE
					WHEN '01' THEN '01' WHEN '02' THEN '02' WHEN '03' THEN '03' WHEN '04' THEN '04'
					WHEN '05' THEN '05' WHEN '06' THEN '06' WHEN '07' THEN '07' WHEN '08' THEN '08'
					WHEN '09' THEN '09' WHEN '10' THEN '10' WHEN '11' THEN '11' WHEN '12' THEN '12'
					WHEN '13' THEN '13' WHEN '14' THEN '14' WHEN '15' THEN '15' WHEN '16' THEN '16'
					WHEN '17' THEN '17' WHEN '25' THEN '25' WHEN '26' THEN '26' WHEN '27' THEN '27'
					WHEN '1'  THEN '01' WHEN '2'  THEN '02' WHEN '3'  THEN '03' WHEN '4'  THEN '04'
					WHEN '5'  THEN '05' WHEN '6'  THEN '06' WHEN '7'  THEN '07' WHEN '8'  THEN '08'
					WHEN '9'  THEN '09'
					ELSE NULL
				END AS Naturaleza_del_evento,
				IIF(FURIPS.NATEVE IN ('17', '16'),
					REPLACE(REPLACE(LTRIM(RTRIM(FURIPS.DESEVE)), ',', ' '), '"', ' '),
					NULL) AS Descripcion_del_otro_evento,
				CASE LTRIM(RTRIM(FURIPS.CONVIC))
					WHEN '1' THEN '01' WHEN '2' THEN '02' WHEN '3' THEN '03' WHEN '4' THEN '04'
					ELSE NULL
				END AS Condicion_victima,
				CONVERT(VARCHAR(10), FURIPS.FECOCUEVE, 23) AS Fecha_de_ocurrencia_evento,
				CASE LTRIM(RTRIM(FURIPS.ZONOCUEVE))
					WHEN '1' THEN '01' WHEN '2' THEN '02'
					ELSE NULL
				END AS Zona_de_ocurrencia_evento,
				MunEve.DEPMUNCOD AS Codigo_municipio_ocurrencia_evento,
				LEFT(LTRIM(RTRIM(FURIPS.DIROCUEVE)), 100) AS Direccion_de_ocurrencia_evento,
				REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(FURIPS.DESEVEACC)), ',', ' '), '"', ' '), '    ', ' ') AS Descripcion_corta_de_lo_ocurrido_en_el_evento,

				CASE WHEN FURIPS.NATEVE IN ('01', '1') THEN LTRIM(RTRIM(FURIPS.ESTASE)) END AS Estado_de_aseguramiento,
				CASE WHEN FURIPS.NATEVE IN ('01', '1') THEN LTRIM(RTRIM(FURIPS.PLAVEHACC)) END AS Placa_vehiculo,
				CASE WHEN FURIPS.NATEVE IN ('01', '1') THEN LTRIM(RTRIM(FURIPS.TIPVEH)) END AS Tipo_de_Vehiculo,
				CASE WHEN FURIPS.NATEVE IN ('01', '1') THEN LTRIM(RTRIM(REPLACE(REPLACE(FURIPS.CODASE, ' ', ''), '-', ''))) END AS Codigo_de_la_aseguradora,
				CASE WHEN FURIPS.NATEVE IN ('01', '1') THEN LTRIM(RTRIM(FURIPS.NUMSOA)) END AS Numero_de_poliza_SOAT,
				CASE WHEN FURIPS.NATEVE IN ('01', '1') THEN CONVERT(VARCHAR(10), FURIPS.FECINIPOL, 23) END AS Fecha_de_inicio_de_vigencia_de_la_poliza,
				CASE WHEN FURIPS.NATEVE IN ('01', '1') THEN CONVERT(VARCHAR(10), FURIPS.FECFINPOL, 23) END AS Fecha_final_de_vigencia_de_la_poliza,
				CASE WHEN FURIPS.NATEVE IN ('01', '1') THEN LTRIM(RTRIM(FURIPS.FiledSIRAS)) END AS Numero_de_radicado_SIRAS,
				CASE WHEN FURIPS.NATEVE IN ('01', '1') THEN LTRIM(RTRIM(FURIPS.COBEXCPOL)) END AS Cobro_por_agotamiento_tope_Aseguradora,
				CASE WHEN FURIPS.NATEVE IN ('01', '1') THEN LTRIM(RTRIM(FURIPS.TIPDOCPRO)) END AS Tipo_de_documento_de_identidad_del_propietario,
				CASE WHEN FURIPS.NATEVE IN ('01', '1') THEN LTRIM(RTRIM(FURIPS.NUMDOCPRO)) END AS Numero_de_documento_de_identidad_del_propietario,
				CASE WHEN FURIPS.NATEVE IN ('01', '1') THEN LTRIM(RTRIM(FURIPS.PRINOMPRO)) END AS Primer_nombre_del_propietario_o_razon_social,
				CASE WHEN FURIPS.NATEVE IN ('01', '1') THEN LTRIM(RTRIM(FURIPS.SEGNOMPRO)) END AS Segundo_nombre_del_propietario,
				CASE WHEN FURIPS.NATEVE IN ('01', '1') THEN LTRIM(RTRIM(FURIPS.PRIAPEPRO)) END AS Primer_apellido_del_propietario,
				CASE WHEN FURIPS.NATEVE IN ('01', '1') THEN LTRIM(RTRIM(FURIPS.SEGAPEPRO)) END AS Segundo_apellido_del_propietario,
				CASE WHEN FURIPS.NATEVE IN ('01', '1') THEN LEFT(LTRIM(RTRIM(FURIPS.DIRRESPRO)), 100) END AS Direccion_de_residencia_del_propietario,
				CASE WHEN FURIPS.NATEVE IN ('01', '1') THEN LTRIM(RTRIM(FURIPS.TELRESPRO)) END AS Telefono_de_residencia_del_propietario,
				CASE WHEN FURIPS.NATEVE IN ('01', '1') THEN MunPro.DEPMUNCOD END AS Codigo_del_municipio_de_residencia_del_propietario,

				CASE WHEN FURIPS.NATEVE IN ('01', '1') THEN LTRIM(RTRIM(FURIPS.TIPDOCCON)) END AS Tipo_de_documento_de_identidad_del_conductor,
				CASE WHEN FURIPS.NATEVE IN ('01', '1') THEN LTRIM(RTRIM(FURIPS.NUMDOCCON)) END AS Numero_de_documento_de_identidad_del_conductor,
				CASE WHEN FURIPS.NATEVE IN ('01', '1') THEN LTRIM(RTRIM(FURIPS.PRINOMCON)) END AS Primer_nombre_del_conductor,
				CASE WHEN FURIPS.NATEVE IN ('01', '1') THEN LTRIM(RTRIM(FURIPS.SEGNOMCON)) END AS Segundo_nombre_del_conductor,
				CASE WHEN FURIPS.NATEVE IN ('01', '1') THEN LTRIM(RTRIM(FURIPS.PRIAPECON)) END AS Primer_apellido_del_conductor,
				CASE WHEN FURIPS.NATEVE IN ('01', '1') THEN LTRIM(RTRIM(FURIPS.SEGAPECON)) END AS Segundo_apellido_del_conductor,
				CASE WHEN FURIPS.NATEVE IN ('01', '1') THEN MunCon.DEPMUNCOD END AS Codigo_del_municipio_de_residencia_del_conductor,
				CASE WHEN FURIPS.NATEVE IN ('01', '1') THEN LEFT(LTRIM(RTRIM(FURIPS.DIRRESCON)), 100) END AS Direccion_de_residencia_del_conductor,
				CASE WHEN FURIPS.NATEVE IN ('01', '1') THEN LTRIM(RTRIM(FURIPS.TELRESCON)) END AS Telefono_de_residencia_del_conductor,

				-- Campos Circular 003 NO existen en ADFURIPSU → NULL
				CAST(NULL AS VARCHAR(2)) AS Uso_material_de_osteosintesis_en_la_atencion,
				CAST(NULL AS VARCHAR(5)) AS Es_atencion_inicial_paciente_remitido_o_control,

				LTRIM(RTRIM(FURIPS.CODHABLENV)) AS Codigo_de_habilitacion_del_prestador_que_remite,
				CAST(NULL AS VARCHAR(10)) AS TIPO_de_documento_Profesional_que_recibe,
				CAST(NULL AS VARCHAR(20)) AS Numero_de_documento_Profesional_que_recibe,
				LTRIM(RTRIM(FURIPS.CODHABREC)) AS Codigo_de_habilitacion_del_prestador_que_recibe,
				CONVERT(VARCHAR(10), FURIPS.FECING, 23) AS Fecha_de_aceptacion,
				LTRIM(RTRIM(FURIPS.HORING)) AS Hora_aceptacion,
				LTRIM(RTRIM(FURIPS.AmbulancePlate)) AS Placa_ambulancia_que_realiza_el_traslado_secundario,
				CAST(NULL AS VARCHAR(5)) AS Tipo_de_servicio_del_transporte_secundario,

				LTRIM(RTRIM(FURIPS.PLATRAVIC)) AS Placa_ambulancia_que_realiza_el_traslado,
				CAST(NULL AS VARCHAR(12)) AS Codigo_de_habilitacion_del_prestador_que_recibe_transporte_primario,
				LEFT(LTRIM(RTRIM(FURIPS.TRASITEVE)), 100) AS Transporte_de_la_victima_desde_el_sitio_del_evento_Direccion,
				LEFT(LTRIM(RTRIM(FURIPS.TRAFINREC)), 100) AS Transporte_de_la_victima_hasta_el_fin_del_recorrido_direccion_IPS,
				LTRIM(RTRIM(FURIPS.TIPSERAMB)) AS Tipo_de_servicio_del_transporte

			FROM @Invoices i
			JOIN Billing.Invoice ii ON ii.Id = i.InvoiceId
			JOIN dbo.ADFURIPSU FURIPS ON i.InvoiceId = FURIPS.IdInvoice
			LEFT JOIN dbo.INMUNICIP MunVic ON MunVic.DEPCODIGO = FURIPS.CODDEPVIC AND MunVic.MUNCODIGO = FURIPS.CODMUNVIC
			LEFT JOIN dbo.INMUNICIP MunEve ON MunEve.DEPCODIGO = FURIPS.CODDEPEVE AND MunEve.MUNCODIGO = FURIPS.CODMUNEVE
			LEFT JOIN dbo.INMUNICIP MunPro ON MunPro.DEPCODIGO = FURIPS.CODDEPPRO AND MunPro.MUNCODIGO = FURIPS.CODMUNPRO
			LEFT JOIN dbo.INMUNICIP MunCon ON MunCon.DEPCODIGO = FURIPS.CODDEPCON AND MunCon.MUNCODIGO = FURIPS.CODMUNCON
			WHERE NOT EXISTS (
				-- Solo entramos a la tabla antigua si la factura NO tiene FUR confirmado
				-- en la tabla nueva (Circular 003).
				SELECT 1
				FROM dbo.ADFURIPSInvoice fi
				WHERE fi.InvoiceId = i.InvoiceId AND fi.StatusTo = 1
			)
		)
		SELECT *
		FROM FurData
		ORDER BY CAST(SUBSTRING(NUM_FACTURA + '0', PATINDEX('%[0-9]%', NUM_FACTURA + '0'), LEN(NUM_FACTURA + '0')) AS DECIMAL)

	END TRY
	BEGIN CATCH
		PRINT ERROR_MESSAGE() + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20))
	END CATCH
END
