
-- =============================================
-- Author:		PABLO ALEXANDER SALAZAR SANCHEZ
-- Create date: 2023-12-26
-- Description:	Procedimiento que se encarga de el generar los datos relacionados al archivo FURTRAN
-- =============================================
CREATE PROCEDURE [Glosas].[SP_GenerateFURTRANFileData] 
	@XmlParameters as XML
AS
BEGIN
	SET NOCOUNT ON;
	
	/***************************************** VARIABLES *****************************************/

	--Variables de control
	DECLARE	@AdmissionNumber nvarchar(10)

	BEGIN TRY

		/*************************************** CRITERIOS ***************************************/

		SELECT	@AdmissionNumber = t.x.value('AdmissionNumber[1]','varchar(10)')
		FROM @XmlParameters.nodes('/Data') t(x)

		/*****************************************************************************************/
		-- Se realiza la consulta del archivo plano FURTRAN
	SELECT 
		ISNULL(f.PreviousFilingNumber,'') AS NumeroRadicadoAnterior, --1
		case f.GlossyResponse
			when 1 THEN '0'
			when 2 THEN '1'
			when 3 THEN '6'
			ELSE ''
		end AS RespuestaObjecionGlosaObjecion, --2
		ISNULL(F.InvoiceNumber,'') AS NumeroFactura, --3
		ISNULL(ips.CODHABILITA,'') AS CodigoHabilitacion, --4
		ISNULL(f.DriverFirstLastName,'') AS PrimerApellidoConductor, --5
		ISNULL(f.DriverSecondLastName,'') AS SegundoApellidoConductor, --6
		ISNULL(f.DriverFirstName,'') AS PrimerNombreConductor, --7
		ISNULL(f.DriverSecondName,'') AS SegundoNombreConductor, --8
		CASE WHEN f.DriverIdentificationType IS NULL THEN '' ELSE Common.GetIdentificationTypeCode(f.DriverIdentificationType, 1) END AS TipoDocumentoReclamante_o_ConductorAmbulancia, --9
		ISNULL(f.DriverIdentificationNumber,'') AS NumeroDocumentoReclamante_o_ConductorAmbulancia, --10
		ISNULL(f.DriverVehicleType,'') AS TipoVehiculo, --11
		ISNULL(f.DriverVehiclePlate,'') AS PlacaVehiculo, --12
		ISNULL(f.DriverAddress,'') AS DireccionReclamante, --13
		ISNULL(f.DriverPhoneNumber,'') AS TelefonoReclamante, --14
		ISNULL(f.DriverDepartmentCode,'') AS CodigoDepartamento, --15
		RIGHT(ISNULL(f.DriverMunicipalityCode,''), 3) AS CodigoMunicipio, --16
		Common.GetIdentificationTypeCode(p.IPTIPODOC, 1) AS TipoDocumentoVictima, --17
		ISNULL(p.IPCODPACI,'') AS NumeroDocumentoVictima, --18
		ISNULL(p.IPPRINOMB,'') AS PrimerNombreVictima, --19
		ISNULL(p.IPSEGNOMB,'') AS SegundoNombreVictima, --20
		ISNULL(p.IPPRIAPEL,'') AS PrimerApellidoVictima, --21
		ISNULL(p.IPSEGAPEL,'') AS SegundoApellidoVictima, --22
		ISNULL(CONVERT(VARCHAR(10),p.IPFECNACI,103),'') AS FechaNacimientoVictima, --23
		CASE f.VictimSex WHEN 1 THEN 'M' WHEN 2 THEN 'F' WHEN 3 THEN 'O' ELSE '' END AS SexoVictima, --24
		ISNULL(f.EvenType,'') AS TipoEvento, --25
		ISNULL(f.VictimPlaceAddress,'') AS DireccionLugarVictima, --26
		ISNULL(f.VictimPlaceDepartmentCode,'') AS DepartamentoLugarVictima, --27
		RIGHT(ISNULL(f.VictimPlaceMunicipalityCode,''), 3) AS MunicipioLugarVictima, --28
		CASE WHEN f.VictimPlaceZone = 1 THEN 'U' ELSE 'R' END AS ZonaRecogeVictima, --29
		ISNULL(CONVERT(VARCHAR(10),f.VictimCertificateTransferDate,103),'') AS FechaTrasladoVictima, --30
		ISNULL(f.VictimCertificateTransferTime,'') AS HoraTrasladoVictima, --31
		ISNULL(ipsv.CODHABILITA,'') AS CodigoHabilitacionIPSVictima, --32
		ISNULL(f.VictimCertificateDepartmentCode,'') AS CodigoDepartamentoTrasladoVictima, --33
		RIGHT(ISNULL(f.VictimCertificateMunicipalityCode,''), 3) AS CodigoMunicipioTrasladoVictima, --34
		ISNULL(f.VictimCondition,'') AS CondicionVictima, --35
		CASE f.VictimAssuranceStatus WHEN 5 THEN '7' WHEN 6 THEN '8' ELSE ISNULL(CONVERT(VARCHAR(2),f.VictimAssuranceStatus),'') END AS EstadoAseguradoraVictima, --36
		CASE f.VictimVehicleType WHEN 9 THEN '10' WHEN 10 THEN '14' WHEN 11 THEN '17' WHEN 12 THEN '19' WHEN 13 THEN '20' WHEN 14 THEN '21' WHEN 15 THEN '22' ELSE ISNULL(CONVERT(VARCHAR(2),f.VictimVehicleType),'') END AS TipoVehiculoVictima, --37
		ISNULL(f.VictimVehiclePlate,'') AS PlacaVehiculoVictima, --38
		ISNULL(f.VictimInsuranceCode,'') AS CodigoAseguradoraVictima, --39
		ISNULL(f.VictimPolicyNumber,'') AS NumeroPolizaSOAT, --40
		ISNULL(CONVERT(VARCHAR(10),f.VictimPolicyStartDate,103),'') AS FechaInicioPoliza, --41
		ISNULL(CONVERT(VARCHAR(10),f.VictimPolicyEndDate,103),'') AS FechaFinalPoliza, --42
		ISNULL(f.VictimSIRASFilingNumber,'') AS NumeroRadicadoSIRAS, --43
		ISNULL(f.InvoicedValue,'') AS ValorFacturado, --44
		ISNULL(f.ClaimedValue,'') AS ValorReclamado, --45
		CASE WHEN f.ManifestationOfEnabledServices = 1 THEN '1' ELSE '0' END AS ManifestacionServiciosHabilitados --46
	FROM Admissions.Furtran f
		JOIN dbo.ADINGRESO i on i.NUMINGRES = f.EntryNumber
		JOIN dbo.INPACIENT p on p.IPCODPACI = i.IPCODPACI
		LEFT JOIN dbo.ADCONTIPS ips on ips.CODIGOIPS = f.IPSCode
		LEFT JOIN dbo.ADCONTIPS ipsv on ipsv.CODIGOIPS = f.VictimCertificateIPS
	WHERE f.EntryNumber = @AdmissionNumber
	END TRY
	BEGIN CATCH
		PRINT ERROR_MESSAGE() + ' Linea: ' + cast(ERROR_LINE() AS VARCHAR(20))
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera los datos estructurados del archivo plano FURTRAN para reclamaciones ante el Fondo de Atención de Urgencias por Accidentes de Tránsito (SOAT/FAT). Recibe como parámetro el número de ingreso del paciente en formato XML y combina la información del evento vial registrada en la tabla Furtran (conductor, vehículo, póliza SOAT, valores facturados y reclamados, respuesta a glosa/objeción) con los datos del episodio de atención de ADINGRESO y la información demográfica de la víctima-paciente de INPACIENT. El resultado es un conjunto de columnas ordenadas y etiquetadas según el estándar del archivo FURTRAN, listo para ser exportado o procesado en el módulo de glosas y radicación ante aseguradoras de accidentes de tránsito.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateFURTRANFileData';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateFURTRANFileData';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Construir y devolver, a partir de un número de admisión recibido por XML, el conjunto de datos del archivo plano FURTRAN (información del conductor/reclamante, víctima, vehículo, póliza SOAT y valores facturados/reclamados) para su exportación.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateFURTRANFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe contener el nodo /Data/AdmissionNumber con el número de ingreso a consultar; Debe existir un registro en Admissions.Furtran cuyo EntryNumber coincida con el número de admisión recibido; El ingreso referenciado debe existir en dbo.ADINGRESO y tener un paciente asociado existente en dbo.INPACIENT (joins internos)', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateFURTRANFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Todos los campos de salida se entregan con cadena vacía cuando el valor original es NULL (uso sistemático de ISNULL); Las fechas de nacimiento, traslado, inicio y fin de póliza se devuelven como texto en formato fecha o cadena vacía si son NULL; Los datos de la víctima provienen del paciente asociado al ingreso vinculado al registro FURTRAN; El cruce entre FURTRAN, ingreso y paciente se hace mediante EntryNumber → NUMINGRES → IPCODPACI; Los errores se capturan y solo se imprimen, sin propagarse ni revertir; el procedimiento no lanza excepción al consumidor', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateFURTRANFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'FURTRAN; Glosa (respuesta de objeción); Radicación SOAT/SIRAS; Conductor de ambulancia/reclamante; Víctima de accidente de tránsito; Póliza SOAT; Aseguradora; Ingreso/admisión hospitalaria; Paciente; IPS habilitada; Factura', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateFURTRANFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Admissions.Furtran: Cuando f.EntryNumber = AdmissionNumber del XML, se retorna un resultset con los 46 campos del FURTRAN combinando datos de Furtran, ADINGRESO e INPACIENT; [RAISERROR] (stdout): En caso de error en el TRY, se imprime ERROR_MESSAGE() concatenado con el número de línea, sin relanzar la excepción', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateFURTRANFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si GlossyResponse = 1 → Se mapea la respuesta de objeción de glosa a ''0''; si GlossyResponse = 2 → Se mapea la respuesta de objeción de glosa a ''1''; si GlossyResponse = 3 → Se mapea la respuesta de objeción de glosa a ''6'' else Se devuelve cadena vacía cuando GlossyResponse no coincide con 1, 2 o 3', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateFURTRANFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Admissions.Furtran; dbo.ADINGRESO; dbo.INPACIENT', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateFURTRANFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateFURTRANFileData';
-- GO
