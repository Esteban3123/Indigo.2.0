
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
		ISNULL(f.PreviousFilingNumber,'') AS CodigoHabilitacion, --4
		ISNULL(f.DriverFirstName,'') AS PrimerApellidoConductor, --5
		ISNULL(f.DriverSecondLastName,'') AS SegundoApellidoConductor, --6
		ISNULL(f.DriverFirstName,'') AS PrimerNombreConductor, --7
		ISNULL(f.DriverSecondName,'') AS SegundoNombreConductor, --8
		ISNULL(f.DriverIdentificationType,'') AS TipoDocumentoReclamante_o_ConductorAmbulancia, --9
		ISNULL(f.DriverIdentificationNumber,'') AS NumeroDocumentoReclamante_o_ConductorAmbulancia, --10
		ISNULL(f.DriverVehicleType,'') AS TipoVehiculo, --11
		ISNULL(f.DriverVehiclePlate,'') AS PlacaVehiculo, --12
		ISNULL(f.DriverAddress,'') AS DireccionReclamante, --13
		ISNULL(f.DriverPhoneNumber,'') AS TelefonoReclamante, --14
		ISNULL(f.DriverDepartmentCode,'') AS CodigoDepartamento, --15
		ISNULL(f.DriverMunicipalityCode,'') AS CodigoMunicipio, --16
		ISNULL(p.IPTIPODOC,'') AS TipoDocumentoVictima, --17
		ISNULL(p.CODIGONIT,'') AS NumeroDocumentoVictima, --18
		ISNULL(p.IPPRINOMB,'') AS PrimerNombreVictima, --19
		ISNULL(p.IPSEGNOMB,'') AS SegundoNombreVictima, --20
		ISNULL(p.IPPRIAPEL,'') AS PrimerApellidoVictima, --21
		ISNULL(p.IPSEGAPEL,'') AS SegundoApellidoVictima, --22
		ISNULL(CONVERT(VARCHAR(10),p.IPFECNACI),'') AS FechaNacimientoVictima, --23
		ISNULL(f.VictimSex,'') AS SexoVictima, --24
		ISNULL(f.EvenType,'') AS TipoEvento, --25
		ISNULL(f.VictimPlaceAddress,'') AS DireccionLugarVictima, --26
		ISNULL(f.VictimPlaceDepartmentCode,'') AS DepartamentoLugarVictima, --27
		ISNULL(f.VictimPlaceMunicipalityCode,'') AS MunicipioLugarVictima, --28
		ISNULL(f.VictimPlaceZone,'') AS ZonaRecogeVictima, --29
		ISNULL(CONVERT(VARCHAR(10),f.VictimCertificateTransferDate),'') AS FechaTrasladoVictima, --30
		ISNULL(f.VictimCertificateTransferTime,'') AS HoraTrasladoVictima, --31
		ISNULL(f.IPSCode,'') AS CodigoHabilitacionIPSVictima, --32
		ISNULL(f.VictimCertificateDepartmentCode,'') AS CodigoDepartamentoTrasladoVictima, --33
		ISNULL(f.VictimCertificateMunicipalityCode,'') AS CodigoMunicipioTrasladoVictima, --34
		ISNULL(f.VictimCondition,'') AS CondicionVictima, --35
		ISNULL(f.VictimAssuranceStatus,'') as EstadoAseguradoraVictima, --36
		ISNULL(f.VictimVehicleType,'') AS TipoVehiculoVictima, --37
		ISNULL(f.VictimVehiclePlate,'') AS PlacaVehiculoVictima, --38
		ISNULL(f.VictimInsuranceCode,'') AS CodigoAseguradoraVictima, --39
		ISNULL(f.VictimPolicyNumber,'') AS NumeroPolizaSOAT, --40
		ISNULL(CONVERT(DATE,f.VictimPolicyStartDate),'') AS FechaInicioPoliza, --41
		ISNULL(CONVERT(DATE,f.VictimPolicyEndDate),'') AS FechaFinalPoliza, --42
		ISNULL(f.VictimSIRASFilingNumber,'') AS NumeroRadicadoSIRAS, --43
		ISNULL(f.InvoicedValue,'') AS ValorFacturado, --44
		ISNULL(f.ClaimedValue,'') AS ValorReclamado, --45
		ISNULL(f.ManifestationOfEnabledServices,'') AS ManifestacionServiciosHabilitados --46
	FROM Admissions.Furtran f
		JOIN dbo.ADINGRESO i WITH(NOLOCK) on i.NUMINGRES = f.EntryNumber
		JOIN dbo.INPACIENT p WITH(NOLOCK) on p.IPCODPACI = i.IPCODPACI
	WHERE f.EntryNumber = @AdmissionNumber
	END TRY
	BEGIN CATCH
		PRINT ERROR_MESSAGE() + ' Linea: ' + cast(ERROR_LINE() AS VARCHAR(20))
	END CATCH
END