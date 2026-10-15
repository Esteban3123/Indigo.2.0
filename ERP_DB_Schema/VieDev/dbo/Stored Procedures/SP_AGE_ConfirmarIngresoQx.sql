CREATE PROCEDURE [dbo].[SP_AGE_ConfirmarIngresoQx]
	@Idprogramacion INT,
	@user VARCHAR(20)

AS
BEGIN

	SET NOCOUNT ON;

	DECLARE @OrigenQX AS INTEGER
	DECLARE @Identificacion AS VARCHAR(25)
	DECLARE @CODENTIDAD AS VARCHAR(50)
	DECLARE @CentroAtencion AS VARCHAR(50)
	DECLARE @UnidadFuncional AS VARCHAR(50)
	DECLARE @CareGroupId AS INTEGER
	DECLARE @HealthAdministratorId AS INTEGER
	DECLARE @FechaProceso AS DATETIME = [Common].[GETDATE]()
	DECLARE @Profesional AS VARCHAR(50)
	DECLARE @Ingreso AS DECIMAL(18,0)
	DECLARE @IdConsecu VARCHAR(8) = '00000001'

	BEGIN TRY 
	
		SELECT  @OrigenQX = A.ORIGENQX ,
				@Identificacion  = A.IPCODPACI, 
				@CODENTIDAD = B.CODENTIDA,
				@CentroAtencion = A.CODCENATE , 
				@UnidadFuncional = Sala.UFUCODIGO, 
				@CareGroupId = CG.Id , 
				@HealthAdministratorId = HA.Id,
				@Profesional = A.CODPROSAL 
		FROM	dbo.AGEPROGQX AS A  INNER JOIN
				dbo.INPACIENT AS B  ON A.IPCODPACI = B.IPCODPACI INNER JOIN
				dbo.INCUPSIPS  AS Ser  ON Ser.CODSERIPS = A.CODSERIPS INNER JOIN 
				dbo.AGENSALAC AS Sala  ON Sala.CODConCEC = A.AGENSALAC INNER JOIN
				Contract.CareGroup AS CG ON CG.Id = B.GENCAREGROUP INNER JOIN 
				Contract.HealthAdministrator HA ON HA.ID = B.GENCONENTITY 
		WHERE	A.CODAUTONU = @IDProgramacion
			   
		IF not exists (SELECT   *
		FROM	dbo.AGEPROGQX AS A  INNER JOIN
				dbo.INPACIENT AS B  ON A.IPCODPACI = B.IPCODPACI INNER JOIN
				dbo.INCUPSIPS  AS Ser  ON Ser.CODSERIPS = A.CODSERIPS INNER JOIN 
				dbo.AGENSALAC AS Sala  ON Sala.CODCONCEC = A.AGENSALAC INNER JOIN
				Contract.CareGroup AS CG ON CG.Id = B.GENCAREGROUP INNER JOIN 
				Contract.HealthAdministrator HA ON HA.ID = B.GENCONENTITY 
		WHERE	A.CODAUTONU = @IDProgramacion) BEGIN
				SELECT '999' AS CodeMessage, 'paciente no tiene grupo de atención y/o entidad administradora de salud, debe actualizar información paciente' AS Message
				return
		END

		--si es ambulatorio
		IF @OrigenQX = 1 BEGIN
		
			UPDATE dbo.INCONSECU SET @Ingreso = CONNUMACT += 1 WHERE IDCONSECU = @IdConsecu
					
			--si  hay radicacion 
			IF Exists(SELECT  R.ID FROM AGEPROGQX A inner join ADRADICACIONQX R ON A.IDRADICACIONQX = R.ID WHERE A.CODAUTONU =@IDProgramacion ) BEGIN
			--tomamos entidad y grupo de atencion de la radicacion de cirugia
				set @CareGroupId = null
				set @HealthAdministratorId = null
				SELECT  @CareGroupId = R.GENCAREGROUP , @HealthAdministratorId = R.GENCONENTITY FROM AGEPROGQX A  inner join ADRADICACIONQX R  ON A.IDRADICACIONQX = R.ID WHERE A.CODAUTONU =@IDProgramacion 
			END

			--Consultamos si el paciente cuenta con una estancia activa, para no permitir confirmar el paquete quirurgico 
			IF Exists(SELECT  A.NUMINGRES FROM CHREGESTA A WHERE A.IPCODPACI = @Identificacion AND A.REGESTADO = 1) BEGIN
			SELECT '999' AS CodeMessage,  'No se puede confirmar el paquete quirurgico: Existe un ingreso de tipo hospitalario activo' AS Message
			return
			END


			INSERT INTO dbo.ADINGRESO
								(NUMINGRES,
								IPCODPACI,
								TIPOINGRE,
								IINGREPOR,
								ITIPORIES,
								ICAUSAING,
								CODENTIDA,
								IFECHAING,
								ILIQUIDAC,
								IConTROLI,
								CODCENATE,
								UFUCODIGO,
								IESTADOIN,
								IREINGRES,
								UFUACTPAC,
								GENCAREGROUP,
								GENConENTITY,
								CODUSUCRE,
								FECREGCRE,
								PACIENTESITIOQX,
								INDAUDFOR, IdHealthPurposes, IdEntryRoutesHealthServices,IdAdmissionModalities)
							SELECT
								@Ingreso AS NUMINGRES,
								@Identificacion AS IPCODPACI,
								1 TIPOINGRE,
								1 IINGREPOR,
								1 ITIPORIES,
								11 ICAUSAING,
								@CODENTIDAD,
								@FechaProceso IFECHAING,
								1 ILIQUIDAC,
								'' IConTROLI,
								@CentroAtencion  CODCENATE,
								@UnidadFuncional  UFUCODIGO,
								'' IESTADOIN,
								0 IREINGRES,
								@UnidadFuncional  UFUACTPAC,
								@CareGroupId,
								@HealthAdministratorId,
								@user AS CODUSUCRE,
								[Common].[GETDATE]() FECREGCRE,
								0 AS PACIENTESITIOQX,
								0 AS INDAUDFOR, (SELECT TOP 1 Id FROM Admissions.HealthPurposes WHERE Code = '16') AS IdHealthPurposes, (SELECT top 1 Id FROM EntryRoutesHealthServices WHERE code = '6') AS IdEntryRoutesHealthServices,
								(SELECT top 1 Id FROM Admissions.AdmissionModalities WHERE code = '1') AS IdAdmissionModalities

			update AGEPROGQX set ESTADOFARM = 2, NUMINGRES = @ingreso WHERE CODAUTONU = @IDProgramacion or IDPADRE = @IDProgramacion
			SELECT '0' AS CodeMessage, 'Se guardo correctamente'  Message

		END 

	END TRY
	BEGIN catch
			SELECT '999' AS CodeMessage, ERROR_MESSAGE() + ', Linea: ' + cASt(ERROR_LINE() AS VARCHAR(20)) Message
	END CATCH
END