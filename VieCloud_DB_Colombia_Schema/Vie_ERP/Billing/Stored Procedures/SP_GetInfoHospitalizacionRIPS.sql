-- =============================================
-- Author:		Giovanny Plazas
-- Create date: 2024-09-12
-- Description:	Procedimiento que se encarga de el generar los datos relacionados a hospitalizacion para RIPS
-- LastModification: Jose Reyes Paez
-- ModificationDate: 2025-02-21
-- =============================================
CREATE PROCEDURE [Billing].[SP_GetInfoHospitalizacionRIPS]
	@Parameters AS XML
AS
BEGIN
	SET NOCOUNT ON;

	/****************************************************************************************
		Parametros y configuracion
	****************************************************************************************/

	DECLARE @EstadoFacturaActiva TINYINT = 1;
	DECLARE @TipoIngresoHospitalario INT = 2;
	DECLARE @TipoUnidadUrgencias INT = 1;
	DECLARE @EstadoPacienteFallecido INT = 3;
	DECLARE @EstadoReferenciaAnulada INT = 4;

	DECLARE @TipoCorteUsaFechaIngresoAdmision1 TINYINT = 1;
	DECLARE @TipoCorteUsaFechaIngresoAdmision2 TINYINT = 2;
	DECLARE @TipoCorteUsaFechaAltaHc1 TINYINT = 1;
	DECLARE @TipoCorteUsaFechaAltaHc4 TINYINT = 4;

	DECLARE @IndicacionTrasladarUrgencias TINYINT = 1;
	DECLARE @IndicacionTrasladarObservacionUrgencias TINYINT = 2;
	DECLARE @IndicacionTrasladarHospitalizacion TINYINT = 3;
	DECLARE @IndicacionTrasladarUciAdulto TINYINT = 4;
	DECLARE @IndicacionTrasladarUciPediatrica TINYINT = 5;
	DECLARE @IndicacionTrasladarUciNeonatal TINYINT = 6;
	DECLARE @IndicacionTrasladarConsultaExterna TINYINT = 7;
	DECLARE @IndicacionTrasladarCirugia TINYINT = 8;
	DECLARE @IndicacionHospitalizacionCasa TINYINT = 9;
	DECLARE @IndicacionReferenciaExterna TINYINT = 10;
	DECLARE @IndicacionMorgue TINYINT = 11;
	DECLARE @IndicacionSalida TINYINT = 12;
	DECLARE @IndicacionContinuaUnidad TINYINT = 13;
	DECLARE @IndicacionRetiroVoluntario TINYINT = 15;
	DECLARE @IndicacionFuga TINYINT = 16;

	DECLARE @CodigoViaIngresoError01 VARCHAR(5) = '01';
	DECLARE @CodigoViaIngresoError04 VARCHAR(5) = '04';
	DECLARE @CodigoViaIngresoCorregida VARCHAR(5) = '03';
	DECLARE @CodigoRipsReferenciaHospitalizacion INT = 13;

	DECLARE @CodigoCondicionDestinoDomicilio VARCHAR(2) = '01';
	DECLARE @CodigoCondicionDestinoPacienteMuerto VARCHAR(2) = '02';
	DECLARE @CodigoCondicionDestinoDerivadoOtroServicio VARCHAR(2) = '03';
	DECLARE @CodigoCondicionDestinoReferidoOtraInstitucion VARCHAR(2) = '04';
	DECLARE @CodigoCondicionDestinoContrarreferidoOtraInstitucion VARCHAR(2) = '05';
	DECLARE @CodigoCondicionDestinoHospitalizacionDomiciliaria VARCHAR(2) = '06';
	DECLARE @CodigoCondicionDestinoServicioSocial VARCHAR(2) = '07';
	DECLARE @CodigoCondicionDestinoContinuaServicio VARCHAR(2) = '08';

	DECLARE @CodigoDiagnosticoSinDefinir CHAR(4) = 'Z000';

	/****************************************************************************************
		Tablas de trabajo
	****************************************************************************************/

	DECLARE @Facturas TABLE
	(
		IdFactura                INT         NOT NULL,
		NumeroFactura            VARCHAR(20) NOT NULL,
		DiagnosticoSalidaFactura CHAR(4),
		TipoDocumento            TINYINT     NOT NULL,
		NumeroIngreso            CHAR(10)    NOT NULL,
		EstadoFactura            TINYINT     NOT NULL,
		EsCuentaCorte            BIT         NOT NULL,
		TipoCorte                TINYINT     NOT NULL,
		FechaInicialFactura      DATETIME    NOT NULL,
		FechaFinalFactura        DATETIME    NOT NULL
	);

	DECLARE @DatosHospitalizacion TABLE
	(
		IdFactura                 INT         NOT NULL,
		NumeroFactura             VARCHAR(20) NOT NULL,
		DiagnosticoSalidaFactura  VARCHAR(4),
		TipoDocumento             TINYINT     NOT NULL,
		CodigoPaciente            VARCHAR(25) NOT NULL,
		CodigoCentroAtencion      VARCHAR(10) NOT NULL,
		CodigoCausaIngreso        INT         NOT NULL,
		DiagnosticoEgresoAdmision VARCHAR(4),
		FechaIngresoRipsBase      DATETIME    NOT NULL,
		IdViaIngresoServicioSalud INT,
		NumeroIngreso             CHAR(10)    NOT NULL,
		IndicacionPaciente        VARCHAR(2),
		EstadoPacienteEgreso      INT,
		NumeroFolioEgresoHc       NCHAR(10),
		NumeroAutorizacion        VARCHAR(15),
		FechaAltaRipsBase         DATETIME    NOT NULL
	);

	DECLARE @DiagnosticosOrdenados TABLE
	(
		IdFactura                INT,
		NumeroIngreso            CHAR(10),
		NumeroOrdenDiagnostico   INT,
		CodigoDiagnostico        CHAR(4),
		EsDiagnosticoPrincipal   BIT,
		NombreColumnaDiagnostico VARCHAR(20)
	);

	DECLARE @DiagnosticosPivotados TABLE
	(
		IdFactura               INT,
		NumeroIngreso           CHAR(10),
		CodigoDiagnostico1      CHAR(4),
		CodigoDiagnostico2      CHAR(4),
		CodigoDiagnostico3      CHAR(4),
		EsDiagnosticoPrincipal  BIT
	);

	BEGIN TRY

		/****************************************************************************************
			Validaciones y normalizacion de parametros
		****************************************************************************************/

		INSERT INTO @Facturas
		(
			IdFactura,
			NumeroFactura,
			DiagnosticoSalidaFactura,
			TipoDocumento,
			NumeroIngreso,
			EstadoFactura,
			EsCuentaCorte,
			TipoCorte,
			FechaInicialFactura,
			FechaFinalFactura
		)
		SELECT
			factura.Id              AS IdFactura,
			factura.InvoiceNumber   AS NumeroFactura,
			factura.OutputDiagnosis AS DiagnosticoSalidaFactura,
			factura.DocumentType    AS TipoDocumento,
			factura.AdmissionNumber AS NumeroIngreso,
			factura.[Status]        AS EstadoFactura,
			factura.IsCutAccount    AS EsCuentaCorte,
			factura.CutType         AS TipoCorte,
			factura.InitialDate     AS FechaInicialFactura,
			factura.OutputDate      AS FechaFinalFactura
		FROM @Parameters.nodes('/Data/Document') documentoXml(Documento)
		JOIN Billing.Invoice factura
			ON documentoXml.Documento.value('InvoiceNumber[1]', 'VARCHAR(20)') = factura.InvoiceNumber
			AND documentoXml.Documento.value('DocumentType[1]', 'TINYINT') = factura.DocumentType
		WHERE factura.[Status] = @EstadoFacturaActiva
		GROUP BY
			factura.Id,
			factura.InvoiceNumber,
			factura.OutputDiagnosis,
			factura.DocumentType,
			factura.AdmissionNumber,
			factura.[Status],
			factura.IsCutAccount,
			factura.CutType,
			factura.InitialDate,
			factura.OutputDate;

		/****************************************************************************************
			Logica principal: admision, egreso y ultimo destino clinico
		****************************************************************************************/

		INSERT INTO @DatosHospitalizacion
		(
			IdFactura,
			NumeroFactura,
			DiagnosticoSalidaFactura,
			TipoDocumento,
			CodigoPaciente,
			CodigoCentroAtencion,
			CodigoCausaIngreso,
			DiagnosticoEgresoAdmision,
			FechaIngresoRipsBase,
			IdViaIngresoServicioSalud,
			NumeroIngreso,
			IndicacionPaciente,
			EstadoPacienteEgreso,
			NumeroFolioEgresoHc,
			NumeroAutorizacion,
			FechaAltaRipsBase
		)
		SELECT
			factura.IdFactura,
			factura.NumeroFactura,
			factura.DiagnosticoSalidaFactura,
			factura.TipoDocumento,
			ingreso.IPCODPACI,
			ingreso.CODCENATE,
			ingreso.ICAUSAING,
			ingreso.CODDIAEGR,
			CASE
				WHEN factura.TipoCorte IN (@TipoCorteUsaFechaIngresoAdmision1, @TipoCorteUsaFechaIngresoAdmision2)
					THEN ingreso.IFECHAING
				ELSE factura.FechaInicialFactura
			END AS FechaIngresoRipsBase,
			ingreso.IdEntryRoutesHealthServices,
			ingreso.NUMINGRES,
			IIF(factura.EsCuentaCorte = 1, @IndicacionContinuaUnidad, historiaSalida.INDICAPAC) AS IndicacionPaciente,
			egreso.ESTPACEGR,
			egresoHc.NUMEFOLIO,
			NULLIF(NULLIF(RTRIM(ingreso.IAUTORIZA), ''), '0') AS NumeroAutorizacion,
			CASE
				WHEN factura.TipoCorte IN (@TipoCorteUsaFechaAltaHc1, @TipoCorteUsaFechaAltaHc4)
					THEN COALESCE(egresoHc.FECALTPAC, factura.FechaFinalFactura)
				ELSE factura.FechaFinalFactura
			END AS FechaAltaRipsBase
		FROM @Facturas factura
		JOIN ADINGRESO ingreso
			ON ingreso.NUMINGRES = factura.NumeroIngreso
		LEFT JOIN dbo.CHREGEGRE egreso
			ON egreso.NUMINGRES = ingreso.NUMINGRES
		LEFT JOIN HCREGEGRE egresoHc
			ON egresoHc.NUMINGRES = ingreso.NUMINGRES
		OUTER APPLY
		(
			SELECT TOP 1
				historia.INDICAPAC
			FROM HCHISPACA historia
			JOIN INUNIFUNC unidadFuncional
				ON unidadFuncional.UFUCODIGO = historia.UFUCODIGO
			WHERE historia.NUMINGRES = ingreso.NUMINGRES
				AND unidadFuncional.UFUTIPUNI <> @TipoUnidadUrgencias
				AND NOT historia.INDICAPAC IN
				(
					@IndicacionTrasladarUrgencias,
					@IndicacionTrasladarObservacionUrgencias,
					@IndicacionTrasladarHospitalizacion,
					@IndicacionTrasladarUciAdulto,
					@IndicacionTrasladarUciPediatrica,
					@IndicacionTrasladarUciNeonatal,
					@IndicacionTrasladarConsultaExterna,
					@IndicacionTrasladarCirugia,
					@IndicacionContinuaUnidad
				)
				AND historia.FECHISPAC <= CASE
					WHEN factura.TipoCorte IN (@TipoCorteUsaFechaAltaHc1, @TipoCorteUsaFechaAltaHc4)
						THEN COALESCE(egresoHc.FECALTPAC, factura.FechaFinalFactura)
					ELSE factura.FechaFinalFactura
				END
			ORDER BY
				CASE WHEN egresoHc.NUMEFOLIO = historia.NUMEFOLIO THEN 0 ELSE 1 END,
				historia.FECHISPAC DESC
		) historiaSalida
		WHERE ingreso.TIPOINGRE = @TipoIngresoHospitalario
			AND
			(
				egresoHc.FECALTPAC IS NULL
				OR CAST(
					IIF(
						factura.TipoCorte IN (@TipoCorteUsaFechaIngresoAdmision1, @TipoCorteUsaFechaIngresoAdmision2),
						ingreso.IFECHAING,
						factura.FechaInicialFactura
					) AS DATE
				) <= CAST(egresoHc.FECALTPAC AS DATE)
			);

		/****************************************************************************************
			Transformaciones: priorizacion y pivote de diagnosticos
		****************************************************************************************/

		INSERT INTO @DiagnosticosOrdenados
		(
			IdFactura,
			NumeroIngreso,
			NumeroOrdenDiagnostico,
			CodigoDiagnostico,
			EsDiagnosticoPrincipal,
			NombreColumnaDiagnostico
		)
		SELECT
			diagnosticoOrdenado.IdFactura,
			diagnosticoOrdenado.NumeroIngreso,
			diagnosticoOrdenado.NumeroOrdenDiagnostico,
			diagnosticoOrdenado.CodigoDiagnostico,
			diagnosticoOrdenado.EsDiagnosticoPrincipal,
			CONCAT('CodigoDiagnostico', diagnosticoOrdenado.NumeroOrdenDiagnostico) AS NombreColumnaDiagnostico
		FROM
		(
			SELECT
				datos.IdFactura,
				datos.NumeroIngreso,
				ROW_NUMBER() OVER
				(
					PARTITION BY datos.IdFactura, datos.NumeroIngreso, diagnostico.CODDIAPRI
					ORDER BY
						-- Prioridad funcional existente: egreso, ambos, otros; luego el diagnostico mas reciente.
						CASE diagnostico.DIAINGEGR
							WHEN 'E' THEN 1
							WHEN 'A' THEN 2
							ELSE 3
						END ASC,
						diagnostico.FECDIAGNO DESC
				) AS NumeroOrdenDiagnostico,
				diagnostico.CODDIAGNO AS CodigoDiagnostico,
				diagnostico.CODDIAPRI AS EsDiagnosticoPrincipal
			FROM INDIAGNOP diagnostico
			JOIN @DatosHospitalizacion datos
				ON diagnostico.NUMINGRES = datos.NumeroIngreso
		) diagnosticoOrdenado;

		INSERT INTO @DiagnosticosPivotados
		SELECT
			IdFactura,
			NumeroIngreso,
			CodigoDiagnostico1,
			CodigoDiagnostico2,
			CodigoDiagnostico3,
			EsDiagnosticoPrincipal
		FROM
		(
			SELECT
				IdFactura,
				NumeroIngreso,
				CodigoDiagnostico,
				NombreColumnaDiagnostico,
				EsDiagnosticoPrincipal
			FROM @DiagnosticosOrdenados
		) diagnosticosFuente
		PIVOT
		(
			MAX(CodigoDiagnostico)
			FOR NombreColumnaDiagnostico IN (CodigoDiagnostico1, CodigoDiagnostico2, CodigoDiagnostico3)
		) diagnosticosPivote;

		/****************************************************************************************
			Resultado final RIPS hospitalizacion
		****************************************************************************************/

		SELECT
			LTRIM(RTRIM(centroAtencion.CODIPSSEC)) AS codPrestador,
			-- Regla historica: corrige vias de ingreso con codigos RIPS no validos para hospitalizacion.
			CASE
				WHEN viaIngreso.RIPSCode IN (@CodigoViaIngresoError01, @CodigoViaIngresoError04)
					THEN @CodigoViaIngresoCorregida
				ELSE viaIngreso.RIPSCode
			END AS viaIngresoServicioSalud,
			-- Fecha inicio: primera HC de hospitalizacion dentro del periodo facturado.
			CAST(FORMAT(fechasRips.FechaInicioHospitalizacionRips, 'yyyy-MM-dd HH:mm') AS VARCHAR(50)) AS fechaInicioAtencion,
			datos.NumeroAutorizacion AS numAutorizacion,
			causaAtencion.RIPSCode AS causaMotivoAtencion,
			COALESCE(
				diagnosticosPrincipales.CodigoDiagnostico2,
				diagnosticosPrincipales.CodigoDiagnostico1,
				datos.DiagnosticoEgresoAdmision,
				datos.DiagnosticoSalidaFactura,
				@CodigoDiagnosticoSinDefinir
			) AS codDiagnosticoPrincipal,
			diagnosticoEgreso.codDiagnosticoPrincipalE,
			diagnosticosRelacionadosEgreso.CodigoDiagnostico1 AS codDiagnosticoRelacionadoE1,
			diagnosticosRelacionadosEgreso.CodigoDiagnostico2 AS codDiagnosticoRelacionadoE2,
			diagnosticosRelacionadosEgreso.CodigoDiagnostico3 AS codDiagnosticoRelacionadoE3,
			NULL AS codComplicacion,
			CASE
				WHEN datos.EstadoPacienteEgreso = @EstadoPacienteFallecido THEN @CodigoCondicionDestinoPacienteMuerto
				WHEN datos.IndicacionPaciente = @IndicacionSalida AND referencia.EXTRAMURAL = 1 THEN @CodigoCondicionDestinoDerivadoOtroServicio
				WHEN datos.IndicacionPaciente IN (@IndicacionSalida, @IndicacionRetiroVoluntario, @IndicacionFuga) THEN @CodigoCondicionDestinoDomicilio
				WHEN datos.IndicacionPaciente = @IndicacionMorgue THEN @CodigoCondicionDestinoPacienteMuerto
				WHEN datos.IndicacionPaciente = @IndicacionReferenciaExterna AND viaIngreso.RIPSCode = @CodigoRipsReferenciaHospitalizacion THEN @CodigoCondicionDestinoContrarreferidoOtraInstitucion
				WHEN datos.IndicacionPaciente = @IndicacionReferenciaExterna THEN @CodigoCondicionDestinoReferidoOtraInstitucion
				WHEN datos.IndicacionPaciente = @IndicacionHospitalizacionCasa THEN @CodigoCondicionDestinoHospitalizacionDomiciliaria
				WHEN datos.IndicacionPaciente = @IndicacionContinuaUnidad THEN @CodigoCondicionDestinoContinuaServicio
				ELSE @CodigoCondicionDestinoDomicilio
			END AS condicionDestinoUsuarioEgreso,
			CASE datos.EstadoPacienteEgreso
				WHEN @EstadoPacienteFallecido THEN datos.DiagnosticoEgresoAdmision
				ELSE NULL
			END AS codDiagnosticoCausaMuerte,
			-- Fecha egreso: no debe superar la fecha final de la factura.
			CAST(FORMAT(fechasRips.FechaEgresoHospitalizacionRips, 'yyyy-MM-dd HH:mm') AS VARCHAR(50)) AS fechaEgreso,
			NULL AS consecutivo,
			CAST(NULL AS VARCHAR(20)) AS codDiagnosticoPrincipalCIE11,
			CAST(NULL AS VARCHAR(250)) AS nomCodDiagnosticoPrincipalCIE11,
			CAST(NULL AS VARCHAR(20)) AS codDiagnosticoPrincipalECIE11,
			CAST(NULL AS VARCHAR(250)) AS nomCodDiagnosticoPrincipalECIE11,
			CAST(NULL AS VARCHAR(20)) AS codDiagnosticoRelacionadoE1CIE11,
			CAST(NULL AS VARCHAR(250)) AS nomCodDiagnosticoRelacionadoE1CIE11,
			CAST(NULL AS VARCHAR(20)) AS codDiagnosticoRelacionadoE2CIE11,
			CAST(NULL AS VARCHAR(250)) AS nomCodDiagnosticoRelacionadoE2CIE11,
			CAST(NULL AS VARCHAR(20)) AS codDiagnosticoRelacionadoE3CIE11,
			CAST(NULL AS VARCHAR(250)) AS nomCodDiagnosticoRelacionadoE3CIE11,
			CAST(NULL AS VARCHAR(20)) AS codComplicacionCIE11,
			CAST(NULL AS VARCHAR(250)) AS nomCodComplicacionCIE11,
			CAST(NULL AS VARCHAR(20)) AS codDiagnosticoCausaMuerteCIE11,
			CAST(NULL AS VARCHAR(250)) AS nomCodDiagnosticoCausaMuerteCIE11,
			[rda].[GetCodigoVIDAByDocumentNumber](datos.CodigoPaciente) AS codigoVIDA,
			datos.NumeroFactura AS InvoiceNumber,
			datos.TipoDocumento AS DocumentType
		FROM @DatosHospitalizacion datos
		JOIN @Facturas factura
			ON factura.IdFactura = datos.IdFactura
		JOIN dbo.[ADCENATEN] centroAtencion
			ON centroAtencion.CODCENATE = datos.CodigoCentroAtencion
		JOIN Causesofattention causaAtencion
			ON causaAtencion.Code = datos.CodigoCausaIngreso
		JOIN @DiagnosticosPivotados diagnosticosPrincipales
			ON diagnosticosPrincipales.IdFactura = datos.IdFactura
			AND diagnosticosPrincipales.NumeroIngreso = datos.NumeroIngreso
			AND diagnosticosPrincipales.EsDiagnosticoPrincipal = 1
		JOIN CHREGEGRE egreso
			ON egreso.NUMINGRES = datos.NumeroIngreso
		-- Obtener fecha inicio hospitalizacion: inicio de la primera estancia en una unidad no urgencias.
		OUTER APPLY
		(
			SELECT TOP 1
				estancia.FECINIEST AS FechaInicioHospitalizacion
			FROM CHREGESTA estancia
			JOIN CHCAMASHO cama
				ON cama.CODICAMAS = estancia.CODICAMAS
			JOIN INUNIFUNC unidadFuncional
				ON unidadFuncional.UFUCODIGO = cama.UFUCODIGO
			WHERE estancia.NUMINGRES = datos.NumeroIngreso
				AND unidadFuncional.UFUTIPUNI <> @TipoUnidadUrgencias
				AND estancia.FECINIEST IS NOT NULL
				AND estancia.FECINIEST >= datos.FechaIngresoRipsBase
			ORDER BY estancia.FECINIEST ASC, estancia.ID ASC
		) primeraEstanciaHospitalizacion
		OUTER APPLY
		(
			SELECT TOP 1
				historia.FECHISPAC AS FechaInicioHospitalizacion
			FROM HCHISPACA historia
			JOIN INUNIFUNC unidadFuncional
				ON unidadFuncional.UFUCODIGO = historia.UFUCODIGO
				AND unidadFuncional.UFUTIPUNI <> @TipoUnidadUrgencias
			WHERE historia.NUMINGRES = datos.NumeroIngreso
			ORDER BY historia.FECHISPAC ASC
		) primeraHistoriaHospitalizacion
		CROSS APPLY
		(
			SELECT
				CASE
					WHEN COALESCE(
						primeraEstanciaHospitalizacion.FechaInicioHospitalizacion,
						primeraHistoriaHospitalizacion.FechaInicioHospitalizacion
					) > factura.FechaInicialFactura
						THEN COALESCE(
							primeraEstanciaHospitalizacion.FechaInicioHospitalizacion,
							primeraHistoriaHospitalizacion.FechaInicioHospitalizacion
						)
					ELSE factura.FechaInicialFactura
				END AS FechaInicioHospitalizacionRips,
				IIF(factura.FechaInicialFactura > egreso.FECEGRESO, factura.FechaInicialFactura, egreso.FECEGRESO) AS FechaEgresoBase
		) fechasBaseRips
		CROSS APPLY
		(
			SELECT
				CASE
					WHEN fechasBaseRips.FechaEgresoBase < fechasBaseRips.FechaInicioHospitalizacionRips
						THEN fechasBaseRips.FechaInicioHospitalizacionRips
					WHEN fechasBaseRips.FechaEgresoBase > factura.FechaFinalFactura
						THEN factura.FechaFinalFactura
					ELSE fechasBaseRips.FechaEgresoBase
				END AS FechaEgresoHospitalizacionRips,
				fechasBaseRips.FechaInicioHospitalizacionRips
		) fechasRips
		LEFT JOIN EntryRoutesHealthServices viaIngreso
			ON viaIngreso.Id = datos.IdViaIngresoServicioSalud
		LEFT JOIN HCREFCONP referencia
			ON referencia.NUMINGRES = datos.NumeroIngreso
			AND referencia.ESTADO <> @EstadoReferenciaAnulada -- Se omiten las referencias anuladas.
		LEFT JOIN @DiagnosticosPivotados diagnosticosRelacionados
			ON diagnosticosRelacionados.IdFactura = datos.IdFactura
			AND diagnosticosRelacionados.NumeroIngreso = datos.NumeroIngreso
			AND diagnosticosRelacionados.EsDiagnosticoPrincipal = 0
		CROSS APPLY
		(
			SELECT COALESCE(
				diagnosticosPrincipales.CodigoDiagnostico1,
				datos.DiagnosticoEgresoAdmision,
				datos.DiagnosticoSalidaFactura
			) AS codDiagnosticoPrincipalE
		) diagnosticoEgreso
		OUTER APPLY
		(
			SELECT
				MAX(CASE WHEN diagnosticosRelacionadosFiltrados.NumeroOrdenDiagnostico = 1 THEN diagnosticosRelacionadosFiltrados.CodigoDiagnostico END) AS CodigoDiagnostico1,
				MAX(CASE WHEN diagnosticosRelacionadosFiltrados.NumeroOrdenDiagnostico = 2 THEN diagnosticosRelacionadosFiltrados.CodigoDiagnostico END) AS CodigoDiagnostico2,
				MAX(CASE WHEN diagnosticosRelacionadosFiltrados.NumeroOrdenDiagnostico = 3 THEN diagnosticosRelacionadosFiltrados.CodigoDiagnostico END) AS CodigoDiagnostico3
			FROM
			(
				SELECT
					diagnosticoRelacionado.CodigoDiagnostico,
					ROW_NUMBER() OVER (ORDER BY diagnosticoRelacionado.OrdenDiagnostico) AS NumeroOrdenDiagnostico
				FROM (VALUES
					(1, diagnosticosRelacionados.CodigoDiagnostico1),
					(2, diagnosticosRelacionados.CodigoDiagnostico2),
					(3, diagnosticosRelacionados.CodigoDiagnostico3)
				) diagnosticoRelacionado(OrdenDiagnostico, CodigoDiagnostico)
				WHERE diagnosticoRelacionado.CodigoDiagnostico IS NOT NULL
					AND
					(
						diagnosticoEgreso.codDiagnosticoPrincipalE IS NULL
						OR diagnosticoRelacionado.CodigoDiagnostico <> diagnosticoEgreso.codDiagnosticoPrincipalE
					)
			) diagnosticosRelacionadosFiltrados
		) diagnosticosRelacionadosEgreso
		WHERE primeraHistoriaHospitalizacion.FechaInicioHospitalizacion IS NOT NULL
			AND primeraHistoriaHospitalizacion.FechaInicioHospitalizacion <= factura.FechaFinalFactura
			AND fechasRips.FechaInicioHospitalizacionRips <= factura.FechaFinalFactura;

	END TRY
	BEGIN CATCH
		PRINT ERROR_MESSAGE() + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20));
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el conjunto de datos RIPS de hospitalización requerido por la normativa colombiana, a partir de una lista de facturas enviadas como XML (número de factura y tipo de documento). Consolida dos fuentes de información: el flujo normal de hospitalización del sistema (admisiones, diagnósticos, fechas de ingreso y egreso) y los registros de estancias externas almacenados en RIPSSupportRecord (grupos clínicos con fechas mínimas y máximas de estancia). Para cada factura activa, determina la fuente más reciente como ''ganadora'' y produce un dataset con los campos exigidos por RIPS: centro de atención, causa de ingreso, vía de ingreso, fechas de ingreso y alta, diagnósticos CIE-10 (principal, relacionado, egreso, causa de muerte), condición al egreso, número de autorización y folio, necesario para la generación de reportes RIPS de hospitalización ante aseguradoras y entes de control en Colombia.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_GetInfoHospitalizacionRIPS';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_GetInfoHospitalizacionRIPS';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Construye el bloque de hospitalización RIPS para un conjunto de facturas, derivando vía de ingreso, fechas de atención y egreso, diagnósticos principal/relacionados, condición de destino del usuario y causa de muerte conforme a la normativa RIPS.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetInfoHospitalizacionRIPS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML @Parameters debe contener nodos /Data/Document con InvoiceNumber y DocumentType válidos.; Las facturas referenciadas deben existir en Billing.Invoice con Status=1 (activas).; La admisión asociada debe existir en ADINGRESO con TIPOINGRE=2 (hospitalización).; Debe cumplirse que HCE.FECALTPAC sea NULL o que la fecha de inicio (IFECHAING o InitialDate según CutType) sea menor o igual a FECALTPAC.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetInfoHospitalizacionRIPS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se procesan facturas con Status=1.; Solo se consideran admisiones de tipo hospitalización (TIPOINGRE=2).; En el join con HCHISPACA se excluyen los INDICAPAC en (1,2,3,4,5,6,7,8,13).; El diagnóstico principal cae a ''Z000'' si no se obtiene ningún diagnóstico ni egreso ni de salida de la factura.; El orden de prioridad de diagnósticos es: DIAINGEGR=''E'' (egreso) > ''A'' (admisión) > otros, y dentro de cada uno por FECDIAGNO descendente.; Se devuelven hasta 3 diagnósticos relacionados (DiagnosticCode1..3) vía PIVOT.; numAutorizacion nunca es NULL: se aplica ISNULL(RTRIM(IAUTORIZA),'''').; Las excepciones se silencian (sólo PRINT), no se relanzan.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetInfoHospitalizacionRIPS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'RIPS; Hospitalización; Admisión; Vía de ingreso a servicios de salud; Causa motivo de atención; Diagnóstico principal; Diagnósticos relacionados; Egreso hospitalario; Causa de muerte; Autorización; Cuenta de cobro/corte; Atención extramural; Centro de atención (prestador)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetInfoHospitalizacionRIPS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Devuelve un dataset por factura con campos RIPS de hospitalización (codPrestador, viaIngresoServicioSalud, fechaInicioAtencion, numAutorizacion, causaMotivoAtencion, diagnósticos principal y relacionados, condicionDestinoUsuarioEgreso, codDiagnosticoCausaMuerte, fechaEgreso).; [RAISERROR] ERROR_OUTPUT: En CATCH se imprime (PRINT) el ERROR_MESSAGE y la línea; no relanza la excepción.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetInfoHospitalizacionRIPS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si i.CutType IN (1,2) → Usa ad.IFECHAING como fecha de ingreso (IFECHAING) else Usa i.InitialDate como fecha de ingreso; si i.CutType IN (1,4) → Usa COALESCE(HCE.FECALTPAC, i.OutputDate) como fecha de alta (FECALTPAC) else Usa i.OutputDate como fecha de alta; si i.IsCutAccount = 1 → INDICAPAC se fuerza a 13 (cuenta de cobro parcial) else INDICAPAC se toma de HCHISPACA.INDICAPAC; si erhs.RIPSCode IN (''01'',''04'') → viaIngresoServicioSalud se reemplaza por ''03'' (corrección de vía de ingreso) else Se usa erhs.RIPSCode tal cual; si g.ESTPACEGR=3 o INDICAPAC=11 → condicionDestinoUsuarioEgreso=''02''; si INDICAPAC=12 y refRegister.EXTRAMURAL=1 → condicionDestinoUsuarioEgreso=''03'' else Aplica resto de la cascada CASE sobre INDICAPAC; si INDICAPAC IN (12,15,16) → condicionDestinoUsuarioEgreso=''01''; si INDICAPAC=10 y erhs.RIPSCode=13 → condicionDestinoUsuarioEgreso=''05'' else Si INDICAPAC=10 entonces ''04''; si INDICAPAC=9 → condicionDestinoUsuarioEgreso=''06''; si INDICAPAC=13 → condicionDestinoUsuarioEgreso=''08''; si g.ESTPACEGR=3 (paciente fallecido) → codDiagnosticoCausaMuerte = g.CODDIAEGR else codDiagnosticoCausaMuerte = NULL; si g.IFECHAING > g.FECALTPAC → fechaEgreso = IFECHAING (se devuelve la mayor) else fechaEgreso = FECALTPAC; si Existe HC en HCHISPACA con UFUTIPUNI<>1 (no urgencias) → fechaInicioAtencion toma la primera HC de hospitalización (FECHISPAC mínima) else fechaInicioAtencion = g.IFECHAING', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetInfoHospitalizacionRIPS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; dbo.ADINGRESO; dbo.CHREGEGRE; dbo.HCREGEGRE; dbo.HCHISPACA; dbo.INDIAGNOP; dbo.ADCENATEN; dbo.Causesofattention; dbo.INUNIFUNC; dbo.EntryRoutesHealthServices; dbo.HCREFCONP', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetInfoHospitalizacionRIPS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetInfoHospitalizacionRIPS';
-- GO
