-- =============================================
-- Author:		Giovanny Plazas
-- Create date: 2024-09-12
-- Description:	Procedimiento que se encarga de el generar los datos relacionados a urgencias para RIPS
-- =============================================
CREATE PROCEDURE [Billing].[SP_GetInfoUrgenciasRIPS]
	@Parameters AS XML
AS
BEGIN
	SET NOCOUNT ON;

	/****************************************************************************************
		Parametros y configuracion
	****************************************************************************************/

	DECLARE @EstadoFacturaActiva TINYINT = 1;
	DECLARE @TipoIngresoHospitalario INT = 2;
	DECLARE @FormaIngresoUrgencias INT = 1;
	DECLARE @FormaIngresoRemitido INT = 4;
	DECLARE @FormaIngresoHospitalizacionUrgencias INT = 5;
	DECLARE @TipoUnidadUrgencias INT = 1;
	DECLARE @ClaseCamaObservacionUrgencias INT = 1;

	DECLARE @IndicacionTrasladarHospitalizacion TINYINT = 3;
	DECLARE @IndicacionTrasladarUciAdulto TINYINT = 4;
	DECLARE @IndicacionTrasladarUciPediatrica TINYINT = 5;
	DECLARE @IndicacionTrasladarUciNeonatal TINYINT = 6;
	DECLARE @IndicacionHospitalizacionCasa TINYINT = 9;
	DECLARE @IndicacionReferenciaExterna TINYINT = 10;
	DECLARE @IndicacionMorgue TINYINT = 11;
	DECLARE @IndicacionSalida TINYINT = 12;
	DECLARE @IndicacionContinuaUnidad TINYINT = 13;
	DECLARE @IndicacionRetiroVoluntario TINYINT = 15;
	DECLARE @IndicacionFuga TINYINT = 16;

	DECLARE @EstadoReferenciaAceptada INT = 5;
	DECLARE @CodigoRipsReferenciaUrgencias INT = 13;

	DECLARE @CodigoCondicionDestinoDomicilio VARCHAR(2) = '01';
	DECLARE @CodigoCondicionDestinoPacienteMuerto VARCHAR(2) = '02';
	DECLARE @CodigoCondicionDestinoDerivadoOtroServicio VARCHAR(2) = '03';
	DECLARE @CodigoCondicionDestinoReferidoOtraInstitucion VARCHAR(2) = '04';
	DECLARE @CodigoCondicionDestinoContrarreferidoOtraInstitucion VARCHAR(2) = '05';
	DECLARE @CodigoCondicionDestinoHospitalizacionDomiciliaria VARCHAR(2) = '06';
	DECLARE @CodigoCondicionDestinoServicioSocial VARCHAR(2) = '07';
	DECLARE @CodigoCondicionDestinoContinuaServicio VARCHAR(2) = '08';

	DECLARE @TipoDiagnosticoUrgencias VARCHAR(2) = 'U';
	DECLARE @TipoDiagnosticoEgresoTraslado VARCHAR(2) = 'ET';
	DECLARE @TipoDiagnosticoEgreso VARCHAR(2) = 'E';
	DECLARE @MomentoDiagnosticoEgreso CHAR(1) = 'E';
	DECLARE @MomentoDiagnosticoAmbos CHAR(1) = 'A';

	DECLARE @CodigoDiagnosticoSinDefinir CHAR(4) = 'Z000';
	DECLARE @FormatoFechaRips VARCHAR(16) = 'yyyy-MM-dd HH:mm';
	DECLARE @NombreServicioUrgenciasJson VARCHAR(250) = 'urgencias';

	/****************************************************************************************
		Tablas de trabajo
	****************************************************************************************/

	DECLARE @Facturas TABLE
	(
		IdFactura                       INT         NOT NULL,
		NumeroFactura                   VARCHAR(20) NOT NULL,
		DiagnosticoSalidaFactura        CHAR(4),
		TipoDocumento                   TINYINT     NOT NULL,
		NumeroIngreso                   CHAR(10)    NOT NULL,
		EstadoFactura                   TINYINT     NOT NULL,
		EsCuentaCorte                   BIT         NOT NULL,
		TipoCorte                       TINYINT     NOT NULL,
		FechaInicialFactura             DATETIME    NOT NULL,
		FechaFinalFactura               DATETIME    NOT NULL,
		IdFacturaAnterior               INT,
		TipoCorteFacturaAnterior        TINYINT,
		FechaInicialFacturaAnterior     DATETIME,
		FechaFinalFacturaAnterior       DATETIME
	);

	DECLARE @DatosGenerales TABLE
	(
		IdFactura                    INT         NOT NULL,
		NumeroFactura                VARCHAR(20) NOT NULL,
		DiagnosticoSalidaFactura     VARCHAR(4),
		TipoDocumento                TINYINT     NOT NULL,
		CodigoPaciente               VARCHAR(25) NOT NULL,
		CodigoCentroAtencion         VARCHAR(10) NOT NULL,
		CodigoCausaIngreso           INT         NOT NULL,
		DiagnosticoEgresoAdmision    VARCHAR(4),
		FechaInicioAtencion          DATETIME    NOT NULL,
		IdViaIngresoServicioSalud    INT,
		NumeroIngreso                CHAR(10)    NOT NULL,
		IndicacionPaciente           VARCHAR(2),
		EstadoPacienteEgreso         INT
	);

	DECLARE @HistoriasClinicas TABLE
	(
		NumeroFolio        NCHAR(10) NOT NULL,
		NumeroIngreso      CHAR(10)  NOT NULL,
		IdHistoriaClinica   INT       NOT NULL,
		TipoUnidadFuncional INT       NOT NULL,
		IndicacionPaciente  CHAR(2)   NOT NULL,
		FechaHistoria       DATETIME  NOT NULL
	);

	DECLARE @UrgenciasIdentificadas TABLE
	(
		NumeroIngreso           CHAR(10)  NOT NULL,
		NumeroFolioUrgencias    NCHAR(10) NOT NULL,
		IdHistoriaUrgencias     INT       NOT NULL,
		IndicacionUrgencias     CHAR(2)   NULL,
		FechaHistoriaUrgencias  DATETIME  NULL,
		IdHistoriaEgreso        INT       NULL,
		NumeroFolioEgreso       NCHAR(10),
		FechaHistoriaEgreso     DATETIME  NULL
	);

	DECLARE @DiagnosticosHistoria TABLE
	(
		IdFactura                 INT,
		NumeroIngreso             CHAR(10),
		CodigoDiagnostico         CHAR(4),
		EsDiagnosticoPrincipal    BIT,
		MomentoDiagnostico        CHAR(1),
		FechaDiagnostico          DATETIME,
		TipoDiagnosticoRips       VARCHAR(2)
	);

	DECLARE @DiagnosticosOrdenados TABLE
	(
		IdFactura                 INT,
		NumeroIngreso             CHAR(10),
		NumeroOrdenDiagnostico    INT,
		CodigoDiagnostico         CHAR(4),
		EsDiagnosticoPrincipal    BIT,
		NombreColumnaDiagnostico  VARCHAR(20),
		TipoDiagnosticoRips       VARCHAR(2)
	);

	DECLARE @DiagnosticosPivotados TABLE
	(
		IdFactura                 INT,
		NumeroIngreso             CHAR(10),
		CodigoDiagnostico1        CHAR(4),
		CodigoDiagnostico2        CHAR(4),
		CodigoDiagnostico3        CHAR(4),
		EsDiagnosticoPrincipal    BIT,
		TipoDiagnosticoRips       VARCHAR(2)
	);

	DECLARE @AltasUrgencias TABLE
	(
		NumeroIngreso      CHAR(10),
		FechaAltaUrgencias DATETIME,
		IdFactura          INT NOT NULL
	);

	IF OBJECT_ID('tempdb..#ResultadoUrgenciasRips') IS NOT NULL
		DROP TABLE #ResultadoUrgenciasRips;

	CREATE TABLE #ResultadoUrgenciasRips
	(
		codPrestador                             VARCHAR(13),
		fechaInicioAtencion                     VARCHAR(17),
		causaMotivoAtencion                     VARCHAR(3),
		codDiagnosticoPrincipal                 VARCHAR(10),
		codDiagnosticoPrincipalE                VARCHAR(10),
		codDiagnosticoRelacionadoE1             VARCHAR(10),
		codDiagnosticoRelacionadoE2             VARCHAR(10),
		codDiagnosticoRelacionadoE3             VARCHAR(10),
		condicionDestinoUsuarioEgreso           VARCHAR(2),
		codDiagnosticoCausaMuerte               VARCHAR(10),
		fechaEgreso                             VARCHAR(17),
		consecutivo                             INT,
		codigoVIDA                              VARCHAR(36),
		codDiagnosticoPrincipalCIE11            VARCHAR(20),
		nomCodDiagnosticoPrincipalCIE11         VARCHAR(250),
		codDiagnosticoPrincipalECIE11           VARCHAR(20),
		nomCodDiagnosticoPrincipalECIE11        VARCHAR(250),
		codDiagnosticoRelacionadoE1CIE11        VARCHAR(20),
		nomCodDiagnosticoRelacionadoE1CIE11     VARCHAR(250),
		codDiagnosticoRelacionadoE2CIE11        VARCHAR(20),
		nomCodDiagnosticoRelacionadoE2CIE11     VARCHAR(250),
		codDiagnosticoRelacionadoE3CIE11        VARCHAR(20),
		nomCodDiagnosticoRelacionadoE3CIE11     VARCHAR(250),
		codDiagnosticoCausaMuerteCIE11          VARCHAR(20),
		nomCodDiagnosticoCausaMuerteCIE11       VARCHAR(250),
		InvoiceNumber                           VARCHAR(20),
		DocumentType                            TINYINT
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

		;WITH FacturasAnteriores AS
		(
			SELECT
				factura.IdFactura,
				facturaAnterior.*
			FROM @Facturas factura
			CROSS APPLY
			(
				SELECT TOP 1
					facturaPrevia.Id          AS IdFacturaAnterior,
					facturaPrevia.InitialDate AS FechaInicialFacturaAnterior,
					facturaPrevia.OutputDate  AS FechaFinalFacturaAnterior,
					facturaPrevia.CutType     AS TipoCorteFacturaAnterior
				FROM Billing.Invoice facturaPrevia
				WHERE factura.EstadoFactura = @EstadoFacturaActiva
					AND facturaPrevia.AdmissionNumber = factura.NumeroIngreso
					AND facturaPrevia.Id <> factura.IdFactura
					AND facturaPrevia.OutputDate < factura.FechaFinalFactura
				ORDER BY facturaPrevia.OutputDate DESC
			) facturaAnterior
		)
		UPDATE factura
		SET
			factura.IdFacturaAnterior = facturaAnterior.IdFacturaAnterior,
			factura.TipoCorteFacturaAnterior = facturaAnterior.TipoCorteFacturaAnterior,
			factura.FechaInicialFacturaAnterior = facturaAnterior.FechaInicialFacturaAnterior,
			factura.FechaFinalFacturaAnterior = facturaAnterior.FechaFinalFacturaAnterior
		FROM @Facturas factura
		JOIN FacturasAnteriores facturaAnterior
			ON facturaAnterior.IdFactura = factura.IdFactura;

		/****************************************************************************************
			Logica principal: admision, folios de urgencias y alta aplicable
		****************************************************************************************/

		INSERT INTO @DatosGenerales
		(
			IdFactura,
			NumeroFactura,
			DiagnosticoSalidaFactura,
			TipoDocumento,
			CodigoPaciente,
			CodigoCentroAtencion,
			CodigoCausaIngreso,
			DiagnosticoEgresoAdmision,
			FechaInicioAtencion,
			IdViaIngresoServicioSalud,
			NumeroIngreso,
			IndicacionPaciente,
			EstadoPacienteEgreso
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
			factura.FechaInicialFactura AS FechaInicioAtencion,
			ingreso.IdEntryRoutesHealthServices,
			ingreso.NUMINGRES,
			-- En cortes sin indicacion al cierre se fuerza "continua en unidad" para reportar destino 08.
			IIF(ultimaIndicacion.INDICAPAC IS NULL AND factura.EsCuentaCorte = 1, @IndicacionContinuaUnidad, ultimaIndicacion.INDICAPAC) AS IndicacionPaciente,
			egreso.ESTPACEGR
		FROM @Facturas factura
		JOIN ADINGRESO ingreso
			ON ingreso.NUMINGRES = factura.NumeroIngreso
		LEFT JOIN dbo.CHREGEGRE egreso
			ON egreso.NUMINGRES = ingreso.NUMINGRES
		OUTER APPLY
		(
			SELECT TOP 1
				historia.INDICAPAC
			FROM HCHISPACA historia
			WHERE historia.NUMINGRES = ingreso.NUMINGRES
				AND historia.INDICAPAC IN
				(
					@IndicacionTrasladarHospitalizacion,
					@IndicacionTrasladarUciAdulto,
					@IndicacionTrasladarUciPediatrica,
					@IndicacionTrasladarUciNeonatal,
					@IndicacionReferenciaExterna,
					@IndicacionMorgue,
					@IndicacionSalida,
					@IndicacionContinuaUnidad
				)
				AND CAST(historia.FECHISPAC AS DATE) <= CAST(factura.FechaFinalFactura AS DATE)
			ORDER BY historia.FECHISPAC DESC
		) ultimaIndicacion
		-- POSIBLE PROBLEMA
		WHERE ingreso.TIPOINGRE = @TipoIngresoHospitalario
			AND ingreso.IINGREPOR IN
			(
				@FormaIngresoUrgencias,
				@FormaIngresoRemitido,
				@FormaIngresoHospitalizacionUrgencias
			);

		INSERT INTO @HistoriasClinicas
		(
			NumeroFolio,
			NumeroIngreso,
			IdHistoriaClinica,
			TipoUnidadFuncional,
			IndicacionPaciente,
			FechaHistoria
		)
		SELECT
			historia.NUMEFOLIO,
			historia.NUMINGRES,
			historia.ID,
			unidadFuncional.UFUTIPUNI,
			historia.INDICAPAC,
			historia.FECHISPAC
		FROM @DatosGenerales datos
		JOIN HCHISPACA historia
			ON historia.NUMINGRES = datos.NumeroIngreso
		JOIN INUNIFUNC unidadFuncional
			ON unidadFuncional.UFUCODIGO = historia.UFUCODIGO;

		INSERT INTO @UrgenciasIdentificadas
		(
			NumeroIngreso,
			NumeroFolioUrgencias,
			IdHistoriaUrgencias,
			IndicacionUrgencias,
			FechaHistoriaUrgencias,
			IdHistoriaEgreso,
			NumeroFolioEgreso,
			FechaHistoriaEgreso
		)
		SELECT
			ultimaHistoriaUrgencias.NumeroIngreso,
			historiaUrgencias.NumeroFolio AS NumeroFolioUrgencias,
			ultimaHistoriaUrgencias.IdHistoriaUrgencias,
			historiaUrgencias.IndicacionPaciente AS IndicacionUrgencias,
			historiaUrgencias.FechaHistoria AS FechaHistoriaUrgencias,
			primeraHistoriaEgreso.IdHistoriaEgreso,
			historiaEgreso.NumeroFolio AS NumeroFolioEgreso,
			historiaEgreso.FechaHistoria AS FechaHistoriaEgreso
		FROM
		(
			SELECT
				historia.NumeroIngreso,
				MAX(historia.IdHistoriaClinica) AS IdHistoriaUrgencias
			FROM @HistoriasClinicas historia
			WHERE historia.TipoUnidadFuncional = @TipoUnidadUrgencias
			GROUP BY historia.NumeroIngreso
		) ultimaHistoriaUrgencias
		JOIN @HistoriasClinicas historiaUrgencias
			ON historiaUrgencias.IdHistoriaClinica = ultimaHistoriaUrgencias.IdHistoriaUrgencias
		OUTER APPLY
		(
			SELECT
				historiaPosterior.NumeroIngreso,
				MIN(historiaPosterior.IdHistoriaClinica) AS IdHistoriaEgreso
			FROM @HistoriasClinicas historiaPosterior
			WHERE historiaPosterior.TipoUnidadFuncional <> @TipoUnidadUrgencias
				AND historiaPosterior.NumeroIngreso = ultimaHistoriaUrgencias.NumeroIngreso
				AND historiaPosterior.NumeroFolio > historiaUrgencias.NumeroFolio
			GROUP BY historiaPosterior.NumeroIngreso
		) primeraHistoriaEgreso
		LEFT JOIN @HistoriasClinicas historiaEgreso
			ON historiaEgreso.IdHistoriaClinica = primeraHistoriaEgreso.IdHistoriaEgreso;

		/****************************************************************************************
			Transformaciones: origen, priorizacion y pivote de diagnosticos
		****************************************************************************************/

		INSERT INTO @DiagnosticosHistoria
		SELECT *
		FROM
		(
			SELECT
				datos.IdFactura,
				diagnostico.NUMINGRES,
				diagnostico.CODDIAGNO AS CodigoDiagnostico,
				diagnostico.CODDIAPRI AS EsDiagnosticoPrincipal,
				diagnostico.DIAINGEGR AS MomentoDiagnostico,
				diagnostico.FECDIAGNO AS FechaDiagnostico,
				@TipoDiagnosticoUrgencias AS TipoDiagnosticoRips
			FROM INDIAGNOH diagnostico
			JOIN @UrgenciasIdentificadas urgencia
				ON diagnostico.NUMINGRES = urgencia.NumeroIngreso
				AND diagnostico.NUMEFOLIO = urgencia.NumeroFolioUrgencias
			JOIN @DatosGenerales datos
				ON datos.NumeroIngreso = urgencia.NumeroIngreso

			UNION ALL

			SELECT
				datos.IdFactura,
				diagnostico.NUMINGRES,
				diagnostico.CODDIAGNO AS CodigoDiagnostico,
				diagnostico.CODDIAPRI AS EsDiagnosticoPrincipal,
				diagnostico.DIAINGEGR AS MomentoDiagnostico,
				diagnostico.FECDIAGNO AS FechaDiagnostico,
				@TipoDiagnosticoEgresoTraslado AS TipoDiagnosticoRips
			FROM INDIAGNOH diagnostico
			JOIN @UrgenciasIdentificadas urgencia
				ON diagnostico.NUMINGRES = urgencia.NumeroIngreso
				AND diagnostico.NUMEFOLIO = urgencia.NumeroFolioEgreso
			JOIN @DatosGenerales datos
				ON datos.NumeroIngreso = urgencia.NumeroIngreso
			WHERE diagnostico.CODDIAPRI = 1

			UNION ALL

			SELECT
				datos.IdFactura,
				diagnostico.NUMINGRES,
				diagnostico.CODDIAGNO AS CodigoDiagnostico,
				diagnostico.CODDIAPRI AS EsDiagnosticoPrincipal,
				diagnostico.DIAINGEGR AS MomentoDiagnostico,
				diagnostico.FECDIAGNO AS FechaDiagnostico,
				@TipoDiagnosticoEgreso AS TipoDiagnosticoRips
			FROM HCREGEGRE egresoHc
			JOIN @UrgenciasIdentificadas urgencia
				ON egresoHc.NUMINGRES = urgencia.NumeroIngreso
				AND egresoHc.NUMEFOLIO = urgencia.NumeroFolioUrgencias
			JOIN INDIAGNOH diagnostico
				ON diagnostico.NUMINGRES = urgencia.NumeroIngreso
				AND diagnostico.NUMEFOLIO = urgencia.NumeroFolioUrgencias
			JOIN @DatosGenerales datos
				ON datos.NumeroIngreso = urgencia.NumeroIngreso
			WHERE diagnostico.CODDIAPRI = 1
		) diagnosticosFuente;

		INSERT INTO @DiagnosticosOrdenados
		(
			IdFactura,
			NumeroIngreso,
			NumeroOrdenDiagnostico,
			CodigoDiagnostico,
			EsDiagnosticoPrincipal,
			NombreColumnaDiagnostico,
			TipoDiagnosticoRips
		)
		SELECT
			diagnosticoOrdenado.IdFactura,
			diagnosticoOrdenado.NumeroIngreso,
			diagnosticoOrdenado.NumeroOrdenDiagnostico,
			diagnosticoOrdenado.CodigoDiagnostico,
			diagnosticoOrdenado.EsDiagnosticoPrincipal,
			CONCAT('CodigoDiagnostico', diagnosticoOrdenado.NumeroOrdenDiagnostico) AS NombreColumnaDiagnostico,
			diagnosticoOrdenado.TipoDiagnosticoRips
		FROM
		(
			SELECT
				diagnostico.IdFactura,
				diagnostico.NumeroIngreso,
				ROW_NUMBER() OVER
				(
					PARTITION BY
						diagnostico.IdFactura,
						diagnostico.NumeroIngreso,
						diagnostico.TipoDiagnosticoRips,
						diagnostico.EsDiagnosticoPrincipal
					ORDER BY
						-- Prioridad historica: egreso, ambos, otros; luego el diagnostico mas reciente.
						CASE diagnostico.MomentoDiagnostico
							WHEN @MomentoDiagnosticoEgreso THEN 1
							WHEN @MomentoDiagnosticoAmbos THEN 2
							ELSE 3
						END ASC,
						diagnostico.FechaDiagnostico DESC
				) AS NumeroOrdenDiagnostico,
				diagnostico.CodigoDiagnostico,
				diagnostico.EsDiagnosticoPrincipal,
				diagnostico.TipoDiagnosticoRips
			FROM @DiagnosticosHistoria diagnostico
		) diagnosticoOrdenado;

		INSERT INTO @DiagnosticosPivotados
		SELECT
			IdFactura,
			NumeroIngreso,
			CodigoDiagnostico1,
			CodigoDiagnostico2,
			CodigoDiagnostico3,
			EsDiagnosticoPrincipal,
			TipoDiagnosticoRips
		FROM
		(
			SELECT
				IdFactura,
				NumeroIngreso,
				CodigoDiagnostico,
				NombreColumnaDiagnostico,
				EsDiagnosticoPrincipal,
				TipoDiagnosticoRips
			FROM @DiagnosticosOrdenados
		) diagnosticosFuente
		PIVOT
		(
			MAX(CodigoDiagnostico)
			FOR NombreColumnaDiagnostico IN (CodigoDiagnostico1, CodigoDiagnostico2, CodigoDiagnostico3)
		) diagnosticosPivote;

		;WITH AltaObservacionUrgencias AS
		(
			SELECT
				estancia.NUMINGRES AS NumeroIngreso,
				MAX(estancia.FECFINEST) AS FechaAltaUrgencias
			FROM @DatosGenerales datos
			JOIN dbo.CHREGESTA estancia
				ON estancia.NUMINGRES = datos.NumeroIngreso
			JOIN dbo.CHCAMASHO cama
				ON cama.CODICAMAS = estancia.CODICAMAS
			WHERE cama.CODCLACAM = @ClaseCamaObservacionUrgencias
			GROUP BY estancia.NUMINGRES
		),
		PrimeraEstanciaNoUrgencias AS
		(
			SELECT
				datos.NumeroIngreso,
				primeraEstancia.FechaInicioHospitalizacion
			FROM @DatosGenerales datos
			OUTER APPLY
			(
				SELECT TOP 1
					estancia.FECINIEST AS FechaInicioHospitalizacion
				FROM dbo.CHREGESTA estancia
				JOIN dbo.CHCAMASHO cama
					ON cama.CODICAMAS = estancia.CODICAMAS
				JOIN dbo.INUNIFUNC unidadFuncional
					ON unidadFuncional.UFUCODIGO = cama.UFUCODIGO
				WHERE estancia.NUMINGRES = datos.NumeroIngreso
					AND unidadFuncional.UFUTIPUNI <> @TipoUnidadUrgencias
					AND estancia.FECINIEST IS NOT NULL
					AND estancia.FECINIEST >= datos.FechaInicioAtencion
				ORDER BY estancia.FECINIEST ASC, estancia.ID ASC
			) primeraEstancia
		)
		INSERT INTO @AltasUrgencias
		(
			NumeroIngreso,
			FechaAltaUrgencias,
			IdFactura
		)
		SELECT
			datos.NumeroIngreso,
			altaCalculada.FechaAltaUrgencias,
			datos.IdFactura
		FROM @DatosGenerales datos
		JOIN @Facturas factura
			ON factura.IdFactura = datos.IdFactura
		LEFT JOIN AltaObservacionUrgencias altaObservacion
			ON altaObservacion.NumeroIngreso = datos.NumeroIngreso
		LEFT JOIN PrimeraEstanciaNoUrgencias primeraEstancia
			ON primeraEstancia.NumeroIngreso = datos.NumeroIngreso
		CROSS APPLY
		(
			SELECT
				CASE
					-- El traslado a una unidad no urgencias cierra el episodio de urgencias,
					-- aunque posteriormente el paciente regrese a una cama de urgencias.
					WHEN primeraEstancia.FechaInicioHospitalizacion IS NOT NULL
						AND primeraEstancia.FechaInicioHospitalizacion <= factura.FechaFinalFactura
						THEN primeraEstancia.FechaInicioHospitalizacion
					WHEN altaObservacion.FechaAltaUrgencias IS NULL
						THEN factura.FechaFinalFactura
					WHEN factura.FechaFinalFactura <= altaObservacion.FechaAltaUrgencias
						THEN factura.FechaFinalFactura
					ELSE altaObservacion.FechaAltaUrgencias
				END AS FechaAltaUrgencias
		) altaCalculada
		WHERE altaObservacion.FechaAltaUrgencias IS NULL
			OR
			(
				CAST(datos.FechaInicioAtencion AS DATE) <= altaCalculada.FechaAltaUrgencias
				AND NOT
				(
					factura.IdFacturaAnterior IS NOT NULL
					AND
					(
						altaCalculada.FechaAltaUrgencias BETWEEN factura.FechaInicialFacturaAnterior AND factura.FechaFinalFacturaAnterior
						OR factura.FechaInicialFacturaAnterior > altaCalculada.FechaAltaUrgencias
					)
				)
			);

		/****************************************************************************************
			Resultado final RIPS urgencias
		****************************************************************************************/

		INSERT INTO #ResultadoUrgenciasRips
		(
			codPrestador,
			fechaInicioAtencion,
			causaMotivoAtencion,
			codDiagnosticoPrincipal,
			codDiagnosticoPrincipalE,
			codDiagnosticoRelacionadoE1,
			codDiagnosticoRelacionadoE2,
			codDiagnosticoRelacionadoE3,
			condicionDestinoUsuarioEgreso,
			codDiagnosticoCausaMuerte,
			fechaEgreso,
			consecutivo,
			codigoVIDA,
			codDiagnosticoPrincipalCIE11,
			nomCodDiagnosticoPrincipalCIE11,
			codDiagnosticoPrincipalECIE11,
			nomCodDiagnosticoPrincipalECIE11,
			codDiagnosticoRelacionadoE1CIE11,
			nomCodDiagnosticoRelacionadoE1CIE11,
			codDiagnosticoRelacionadoE2CIE11,
			nomCodDiagnosticoRelacionadoE2CIE11,
			codDiagnosticoRelacionadoE3CIE11,
			nomCodDiagnosticoRelacionadoE3CIE11,
			codDiagnosticoCausaMuerteCIE11,
			nomCodDiagnosticoCausaMuerteCIE11,
			InvoiceNumber,
			DocumentType
		)
		SELECT
			LTRIM(RTRIM(centroAtencion.CODIPSSEC)) AS codPrestador,
			FORMAT(datos.FechaInicioAtencion, @FormatoFechaRips) AS fechaInicioAtencion,
			causaAtencion.RIPSCode AS causaMotivoAtencion,
			COALESCE(
				diagnosticoPrincipalUrgencias.CodigoDiagnostico1,
				datos.DiagnosticoEgresoAdmision,
				datos.DiagnosticoSalidaFactura,
				@CodigoDiagnosticoSinDefinir
			) AS codDiagnosticoPrincipal,
			diagnosticoEgreso.codDiagnosticoPrincipalE,
			diagnosticosRelacionadosEgreso.CodigoDiagnostico1 AS codDiagnosticoRelacionadoE1,
			diagnosticosRelacionadosEgreso.CodigoDiagnostico2 AS codDiagnosticoRelacionadoE2,
			diagnosticosRelacionadosEgreso.CodigoDiagnostico3 AS codDiagnosticoRelacionadoE3,
			destino.condicionDestinoUsuarioEgreso,
			CASE
				WHEN destino.condicionDestinoUsuarioEgreso = @CodigoCondicionDestinoPacienteMuerto
					THEN diagnosticoEgreso.codDiagnosticoPrincipalE
				ELSE NULL
			END AS codDiagnosticoCausaMuerte,
			FORMAT(alta.FechaAltaUrgencias, @FormatoFechaRips) AS fechaEgreso,
			NULL AS consecutivo,
			[rda].[GetCodigoVIDAByDocumentNumber](datos.CodigoPaciente) AS codigoVIDA,
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
			CAST(NULL AS VARCHAR(20)) AS codDiagnosticoCausaMuerteCIE11,
			CAST(NULL AS VARCHAR(250)) AS nomCodDiagnosticoCausaMuerteCIE11,
			datos.NumeroFactura AS InvoiceNumber,
			datos.TipoDocumento AS DocumentType
		FROM @DatosGenerales datos
		JOIN dbo.[ADCENATEN] centroAtencion
			ON centroAtencion.CODCENATE = datos.CodigoCentroAtencion
		JOIN Causesofattention causaAtencion
			ON causaAtencion.Code = datos.CodigoCausaIngreso
		JOIN @AltasUrgencias alta
			ON alta.NumeroIngreso = datos.NumeroIngreso
			AND alta.IdFactura = datos.IdFactura
		-- Regla RIPS: solo se genera urgencias si existio estancia en unidad funcional de urgencias.
		JOIN @UrgenciasIdentificadas urgencia
			ON urgencia.NumeroIngreso = datos.NumeroIngreso
		LEFT JOIN @DiagnosticosPivotados diagnosticoPrincipalUrgencias
			ON diagnosticoPrincipalUrgencias.IdFactura = datos.IdFactura
			AND diagnosticoPrincipalUrgencias.NumeroIngreso = datos.NumeroIngreso
			AND diagnosticoPrincipalUrgencias.TipoDiagnosticoRips = @TipoDiagnosticoUrgencias
			AND diagnosticoPrincipalUrgencias.EsDiagnosticoPrincipal = 1
		LEFT JOIN EntryRoutesHealthServices viaIngreso
			ON viaIngreso.Id = datos.IdViaIngresoServicioSalud
		LEFT JOIN HCREFCONP referencia
			ON referencia.NUMINGRES = datos.NumeroIngreso
		LEFT JOIN @DiagnosticosPivotados diagnosticosRelacionadosUrgencias
			ON diagnosticosRelacionadosUrgencias.IdFactura = datos.IdFactura
			AND diagnosticosRelacionadosUrgencias.NumeroIngreso = datos.NumeroIngreso
			AND diagnosticosRelacionadosUrgencias.TipoDiagnosticoRips = @TipoDiagnosticoUrgencias
			AND diagnosticosRelacionadosUrgencias.EsDiagnosticoPrincipal = 0
		LEFT JOIN @DiagnosticosPivotados diagnosticoEgresoUrgencias
			ON diagnosticoEgresoUrgencias.IdFactura = datos.IdFactura
			AND diagnosticoEgresoUrgencias.NumeroIngreso = datos.NumeroIngreso
			AND diagnosticoEgresoUrgencias.TipoDiagnosticoRips = @TipoDiagnosticoEgreso
		LEFT JOIN @DiagnosticosPivotados diagnosticoTrasladoEgreso
			ON diagnosticoTrasladoEgreso.IdFactura = datos.IdFactura
			AND diagnosticoTrasladoEgreso.NumeroIngreso = datos.NumeroIngreso
			AND diagnosticoTrasladoEgreso.TipoDiagnosticoRips = @TipoDiagnosticoEgresoTraslado
		CROSS APPLY
		(
			SELECT COALESCE(
				diagnosticoEgresoUrgencias.CodigoDiagnostico1,
				diagnosticoTrasladoEgreso.CodigoDiagnostico1,
				datos.DiagnosticoEgresoAdmision,
				datos.DiagnosticoSalidaFactura
			) AS codDiagnosticoPrincipalE
		) diagnosticoEgreso
		OUTER APPLY
		(
			SELECT
				MAX(CASE WHEN diagnosticosFiltrados.NumeroOrdenDiagnostico = 1 THEN diagnosticosFiltrados.CodigoDiagnostico END) AS CodigoDiagnostico1,
				MAX(CASE WHEN diagnosticosFiltrados.NumeroOrdenDiagnostico = 2 THEN diagnosticosFiltrados.CodigoDiagnostico END) AS CodigoDiagnostico2,
				MAX(CASE WHEN diagnosticosFiltrados.NumeroOrdenDiagnostico = 3 THEN diagnosticosFiltrados.CodigoDiagnostico END) AS CodigoDiagnostico3
			FROM
			(
				SELECT
					diagnosticoRelacionado.CodigoDiagnostico,
					ROW_NUMBER() OVER (ORDER BY diagnosticoRelacionado.OrdenDiagnostico) AS NumeroOrdenDiagnostico
				FROM (VALUES
					(1, diagnosticosRelacionadosUrgencias.CodigoDiagnostico1),
					(2, diagnosticosRelacionadosUrgencias.CodigoDiagnostico2),
					(3, diagnosticosRelacionadosUrgencias.CodigoDiagnostico3)
				) diagnosticoRelacionado(OrdenDiagnostico, CodigoDiagnostico)
				WHERE diagnosticoRelacionado.CodigoDiagnostico IS NOT NULL
					AND
					(
						diagnosticoEgreso.codDiagnosticoPrincipalE IS NULL
						OR diagnosticoRelacionado.CodigoDiagnostico <> diagnosticoEgreso.codDiagnosticoPrincipalE
					)
			) diagnosticosFiltrados
		) diagnosticosRelacionadosEgreso
		CROSS APPLY
		(
			SELECT COALESCE(urgencia.IndicacionUrgencias, datos.IndicacionPaciente) AS IndicacionPaciente
		) indicador
		CROSS APPLY
		(
			SELECT
				CASE
					WHEN indicador.IndicacionPaciente = @IndicacionSalida
						AND referencia.EXTRAMURAL = 1
						AND referencia.FECSOLICIT <= alta.FechaAltaUrgencias
						AND referencia.ESTADO = @EstadoReferenciaAceptada THEN @CodigoCondicionDestinoDerivadoOtroServicio
					WHEN indicador.IndicacionPaciente IN (@IndicacionSalida, @IndicacionRetiroVoluntario, @IndicacionFuga) THEN @CodigoCondicionDestinoDomicilio
					WHEN indicador.IndicacionPaciente = @IndicacionMorgue THEN @CodigoCondicionDestinoPacienteMuerto
					WHEN indicador.IndicacionPaciente = @IndicacionReferenciaExterna
						AND viaIngreso.RIPSCode = @CodigoRipsReferenciaUrgencias THEN @CodigoCondicionDestinoContrarreferidoOtraInstitucion
					WHEN indicador.IndicacionPaciente = @IndicacionReferenciaExterna THEN @CodigoCondicionDestinoReferidoOtraInstitucion
					WHEN indicador.IndicacionPaciente = @IndicacionHospitalizacionCasa THEN @CodigoCondicionDestinoHospitalizacionDomiciliaria
					WHEN indicador.IndicacionPaciente IN (@IndicacionTrasladarHospitalizacion, @IndicacionTrasladarUciAdulto) THEN @CodigoCondicionDestinoDerivadoOtroServicio
					WHEN indicador.IndicacionPaciente = @IndicacionContinuaUnidad THEN @CodigoCondicionDestinoContinuaServicio
					ELSE @CodigoCondicionDestinoDomicilio
				END AS condicionDestinoUsuarioEgreso
		) destino
		WHERE datos.FechaInicioAtencion <= alta.FechaAltaUrgencias;

		;WITH FacturaPermitidaUrgencias AS
		(
			SELECT TOP 1
				relacionServicio.*,
				facturaSolicitada.NumeroIngreso
			FROM @Facturas facturaSolicitada
			JOIN Billing.Invoice facturaActiva
				ON facturaActiva.AdmissionNumber = facturaSolicitada.NumeroIngreso
				AND facturaActiva.[Status] = @EstadoFacturaActiva
			JOIN Billing.RIPSServiceHospitalRelation relacionServicio
				ON relacionServicio.InvoiceNumber = facturaActiva.InvoiceNumber
				AND relacionServicio.ServiceNameJson = @NombreServicioUrgenciasJson
		)
		DELETE resultado
		FROM #ResultadoUrgenciasRips resultado
		JOIN @Facturas factura
			ON factura.NumeroFactura = resultado.InvoiceNumber
		JOIN FacturaPermitidaUrgencias facturaPermitida
			ON facturaPermitida.NumeroIngreso = factura.NumeroIngreso
		WHERE resultado.condicionDestinoUsuarioEgreso <> @CodigoCondicionDestinoContinuaServicio
			AND factura.NumeroFactura <> facturaPermitida.InvoiceNumber;

		SELECT *
		FROM #ResultadoUrgenciasRips;

	END TRY
	BEGIN CATCH
		PRINT ERROR_MESSAGE() + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20));
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera la informacion de urgencias requerida para el reporte RIPS (Registro Individual de Prestacion de Servicios de Salud). Recibe como entrada @Parameters con una lista de facturas (numero de factura y tipo de documento), consulta Billing.Invoice para obtener los ingresos de urgencias y consolida centro de atencion, causa de ingreso, diagnosticos principal y relacionados, fecha de inicio de atencion, condicion de egreso del paciente y fecha de egreso. Adicionalmente, detecta cortes de cuenta y facturas previas del mismo ingreso para encadenar correctamente los periodos de atencion. El resultado final es un conjunto de registros con los campos exigidos por la norma RIPS para el modulo de urgencias, incluyendo codigo de prestador, fechas de atencion y egreso, diagnosticos CIE-10, condicion de destino del usuario al egreso y codigo VIDA.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_GetInfoUrgenciasRIPS';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_GetInfoUrgenciasRIPS';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name = N'MS_BR_Purpose', @value = N'Construye el bloque de informacion de urgencias para el reporte RIPS a partir de las facturas recibidas en @Parameters, recopilando ingreso, diagnosticos, fecha de egreso, condicion de destino del usuario y codigo VIDA.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_GetInfoUrgenciasRIPS';
-- GO
-- EXEC sys.sp_addextendedproperty @name = N'MS_BR_Preconditions', @value = N'El parametro @Parameters debe traer nodos /Data/Document con InvoiceNumber y DocumentType.; Las facturas referenciadas deben existir en Billing.Invoice con Status = 1 (activas).; La admision asociada en ADINGRESO debe tener TIPOINGRE = 2 (hospitalario) e IINGREPOR en (1,4,5).; Debe existir al menos una historia clinica en HCHISPACA ligada a INUNIFUNC con UFUTIPUNI = 1 (unidad funcional de urgencias); sin esta estancia no se genera segmento de urgencias.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_GetInfoUrgenciasRIPS';
-- GO
-- EXEC sys.sp_addextendedproperty @name = N'MS_BR_SideEffects', @value = N'[INSERT] #ResultadoUrgenciasRips: Por cada factura activa con admision de urgencias se inserta una fila con codPrestador, fechas formateadas yyyy-MM-dd HH:mm, causa de atencion, diagnosticos pivotados, condicion de destino y codigo VIDA.; [DELETE] #ResultadoUrgenciasRips: Cuando existe otra factura activa para la misma admision con relacion RIPS de servicio urgencias, se eliminan filas cuya condicionDestinoUsuarioEgreso <> 08 y cuyo InvoiceNumber sea distinto al autorizado para evitar duplicidad.; [RETURN_RESULT] RESULT_SET: Devuelve SELECT * FROM #ResultadoUrgenciasRips.; [RAISERROR] ERROR_OUTPUT: En caso de excepcion imprime ERROR_MESSAGE() junto con el numero de linea; no relanza la excepcion.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_GetInfoUrgenciasRIPS';
-- GO
-- EXEC sys.sp_addextendedproperty @name = N'MS_BR_Decisions', @value = N'si no existe historia clinica con UFUTIPUNI = 1 para el ingreso -> no se genera segmento de urgencias; si ultima indicacion INDICAPAC es NULL AND factura.EsCuentaCorte = 1 -> INDICAPAC se fuerza a 13 (continua en unidad/corte parcial); si existe una primera estancia en unidad no urgencias posterior al inicio de atencion y dentro del periodo de la factura -> FechaAltaUrgencias usa FECINIEST de esa estancia, porque el traslado cierra el episodio de urgencias aunque el paciente regrese posteriormente a una cama de urgencias; si no existe dicha estancia -> FechaAltaUrgencias usa OutputDate cuando no existe alta de observacion o la fecha menor entre OutputDate y la maxima FECFINEST de CHREGESTA con cama CODCLACAM = 1; si la fecha de alta cae dentro del periodo de una factura previa del mismo ingreso -> no se incluye para la factura actual; el diagnostico principal de urgencias prioriza diagnosticos del folio de urgencias y cae a CODDIAEGR, OutputDiagnosis y Z000; el diagnostico de egreso prioriza diagnostico de egreso HC, diagnostico de traslado, CODDIAEGR y OutputDiagnosis; la condicion de destino se deriva de INDICAPAC, referencia extramural aceptada, via RIPS 13 y reglas historicas de salida, referencia, morgue, hospitalizacion en casa, traslado y corte parcial; si condicionDestinoUsuarioEgreso = 02 -> codDiagnosticoCausaMuerte toma el diagnostico principal de egreso.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_GetInfoUrgenciasRIPS';
-- GO
-- EXEC sys.sp_addextendedproperty @name = N'MS_BR_Consumes', @value = N'Billing.Invoice; dbo.ADINGRESO; dbo.CHREGEGRE; dbo.HCHISPACA; dbo.INUNIFUNC; dbo.INDIAGNOH; dbo.HCREGEGRE; dbo.CHREGESTA; dbo.CHCAMASHO; dbo.ADCENATEN; dbo.Causesofattention; dbo.EntryRoutesHealthServices; dbo.HCREFCONP; Billing.RIPSServiceHospitalRelation; rda.GetCodigoVIDAByDocumentNumber', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_GetInfoUrgenciasRIPS';
-- GO
-- EXEC sys.sp_addextendedproperty @name = N'MS_BR_Source', @value = N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_GetInfoUrgenciasRIPS';
-- GO
