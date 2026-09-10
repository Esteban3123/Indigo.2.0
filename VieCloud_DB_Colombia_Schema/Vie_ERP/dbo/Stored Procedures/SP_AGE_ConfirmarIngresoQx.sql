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
								INDAUDFOR, IdHealthPurposes, IdEntryRoutesHealthServices,IdAdmissionModalities, CareSettingCode)
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
								(SELECT top 1 Id FROM Admissions.AdmissionModalities WHERE code = '1') AS IdAdmissionModalities, 5 CareSettingCode

			update AGEPROGQX set ESTADOFARM = 2, NUMINGRES = @ingreso WHERE CODAUTONU = @IDProgramacion or IDPADRE = @IDProgramacion
			SELECT '0' AS CodeMessage, 'Se guardo correctamente'  Message

		END 

	END TRY
	BEGIN catch
			SELECT '999' AS CodeMessage, ERROR_MESSAGE() + ', Linea: ' + cASt(ERROR_LINE() AS VARCHAR(20)) Message
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Confirma el ingreso quirúrgico de un paciente previamente programado en la agenda de cirugías (AGEPROGQX). A partir del ID de programación, valida que el paciente tenga grupo de atención (CareGroup) y entidad administradora de salud (EPS/pagador) correctamente configurados, y verifica que no exista una hospitalización activa que impida el proceso. Si el procedimiento es de origen ambulatorio, genera un nuevo número de ingreso, crea el registro de admisión en ADINGRESO y actualiza el estado farmacéutico de la programación quirúrgica; si además existe una radicación quirúrgica (ADRADICACIONQX), toma de ella la entidad y grupo de atención para el ingreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AGE_ConfirmarIngresoQx';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AGE_ConfirmarIngresoQx';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Confirma el ingreso administrativo de un paciente programado para cirugía ambulatoria, generando un consecutivo de ingreso, creando el registro de admisión y actualizando el estado de la programación quirúrgica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ConfirmarIngresoQx';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La programación quirúrgica debe existir en AGEPROGQX y poder cruzarse con paciente, servicio CUPS, sala, grupo de atención y administradora de salud.; El paciente debe tener asignado grupo de atención (GENCAREGROUP) y entidad administradora (GENCONENTITY) en INPACIENT.; Para cirugía ambulatoria (ORIGENQX=1), el paciente no debe tener una estancia hospitalaria activa (CHREGESTA.REGESTADO=1).; Debe existir el consecutivo ''00000001'' en INCONSECU para generar el número de ingreso.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ConfirmarIngresoQx';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se crea ingreso administrativo para cirugías con ORIGENQX=1 (ambulatorias).; Nunca se crea un ingreso si el paciente carece de grupo de atención o entidad administradora.; Nunca se crea un ingreso ambulatorio si el paciente tiene una estancia hospitalaria activa.; El número de ingreso (NUMINGRES) siempre proviene del consecutivo INCONSECU ''00000001'' incrementado.; Todo ingreso creado se asocia con finalidad de salud ''16'', vía de ingreso ''6'' y modalidad de admisión ''1''.; El número de ingreso se propaga a la programación quirúrgica y a todas sus programaciones hijas.; Si hay radicación quirúrgica, ésta tiene precedencia sobre el paciente para definir grupo de atención y entidad administradora del ingreso.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ConfirmarIngresoQx';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Programación quirúrgica; Cirugía ambulatoria; Ingreso administrativo / admisión; Radicación quirúrgica; Grupo de atención (CareGroup); Entidad administradora de salud (EPS/HealthAdministrator); Estancia hospitalaria activa; Consecutivo de ingreso; Paquete quirúrgico; Unidad funcional; Centro de atención; Finalidad de salud; Vía de ingreso; Modalidad de admisión', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ConfirmarIngresoQx';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] dbo.INCONSECU: Cuando ORIGENQX=1, incrementa CONNUMACT en 1 sobre el consecutivo IDCONSECU=''00000001'' y captura el nuevo valor como número de ingreso.; [INSERT] dbo.ADINGRESO: Cuando la cirugía es ambulatoria y el paciente no tiene estancia hospitalaria activa, crea un ingreso con TIPOINGRE=1, IINGREPOR=1, ITIPORIES=1, ICAUSAING=11, IREINGRES=0, PACIENTESITIOQX=0, finalidad de salud código ''16'', vía de ingreso código ''6'' y modalidad de admisión código ''1''.; [INSERT] dbo.ADINGRESO: Si existe radicación quirúrgica (ADRADICACIONQX) asociada a la programación, se usa GENCAREGROUP y GENCONENTITY de la radicación en vez de los del paciente al insertar el ingreso.; [UPDATE] dbo.AGEPROGQX: Tras crear el ingreso, marca ESTADOFARM=2 y asigna NUMINGRES en la programación y en todas las programaciones hijas (IDPADRE = programación actual).; [RETURN_RESULT] (resultado): Si el paciente no tiene grupo de atención y/o entidad administradora, retorna CodeMessage ''999'' con mensaje indicando que debe actualizarse la información del paciente y termina.; [RETURN_RESULT] (resultado): Si existe un ingreso hospitalario activo (CHREGESTA.REGESTADO=1) para el paciente, retorna CodeMessage ''999'' indicando que no se puede confirmar el paquete quirúrgico y termina.; [RETURN_RESULT] (resultado): Al completar la inserción y actualización exitosamente retorna CodeMessage ''0'' con mensaje ''Se guardo correctamente''.; [RETURN_RESULT] (resultado): Ante cualquier excepción retorna CodeMessage ''999'' con el mensaje de error y la línea donde ocurrió.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ConfirmarIngresoQx';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si No existen datos completos del paciente con grupo de atención y administradora de salud para la programación → Retorna mensaje de error ''999'' y termina sin crear ingreso; si ORIGENQX = 1 (cirugía ambulatoria) → Genera consecutivo, valida estancia activa, crea ingreso y actualiza programación else No realiza ninguna acción de ingreso (solo se procesa el flujo ambulatorio); si Existe radicación quirúrgica (ADRADICACIONQX) asociada a la programación → Sustituye CareGroup y HealthAdministrator del paciente por los registrados en la radicación quirúrgica else Mantiene CareGroup y HealthAdministrator del paciente; si El paciente tiene una estancia hospitalaria activa (CHREGESTA.REGESTADO=1) → Retorna error ''999'' y no permite confirmar el paquete quirúrgico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ConfirmarIngresoQx';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ConfirmarIngresoQx';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGEPROGQX; dbo.INPACIENT; dbo.INCUPSIPS; dbo.AGENSALAC; Contract.CareGroup; Contract.HealthAdministrator; dbo.ADRADICACIONQX; dbo.CHREGESTA; dbo.INCONSECU; Admissions.HealthPurposes; dbo.EntryRoutesHealthServices; Admissions.AdmissionModalities', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ConfirmarIngresoQx';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ConfirmarIngresoQx';
-- GO
