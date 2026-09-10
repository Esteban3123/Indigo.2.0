-- =========================================================================
-- Author:		Rafael Eduardo patiño
-- Create date: 10/12/2018
-- Description:	se encarga de la inscripción de RIAS CUPS x Paciente desde plan de manejo al guarda la HC
-- =========================================================================
CREATE PROCEDURE [dbo].[SP_RIASInscriptionMedicalOrder]
	@Xml as xml,
	@PatientCode as varchar(25),
	@UserCode as varchar(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON

	--Tabla temporal para obtener los datos del xml
	declare @TableXml table(CupsCode varchar(20), RIASCupsId int, quantity int,Folio int, RIASId int, Processed bit)

	
	begin try

		--Se insertan los registros que vienen del xml
		insert into @TableXml
		select 
			t.x.value('CupsCode[1]','varchar(20)') as CupsCode,
			t.x.value('RIASCupsId[1]','int') as RIASCupsId,
			t.x.value('Quantity[1]','int') as quantity,
			t.x.value('Folio[1]','int') as Folio,
			rc.IDRIAS as RIASId,
			0 as Processed
		from @Xml.nodes('/RIASForPatient') t(x)
		inner join dbo.RIASCUPS rc on rc.ID = t.x.value('RIASCupsId[1]','int')
		
	
		--Se obtiene la fecha de nacimiento del paciente para calcular la edad en días
		declare @DatePatient datetime = (select IPFECNACI from INPACIENT where IPCODPACI = @PatientCode)

		--Variable para almacenar la edad del paciente en días
		declare @AgePatientInDays int = DATEDIFF(day, @DatePatient, [Common].[GETDATE]())
	
		--Variables que se obtienen dentro del while para realizar validaciones
		declare @CupsCode varchar(20)
		declare @RIASCupsId int
		declare @RiasCupsDetailId int
		declare @Quantity as int
		declare @Folio as char(10)
		declare @AgeDaysMinimum int
		declare @AgeDaysMaximum int
		declare @MinimumDate datetime
		declare @MaximumDate datetime
		declare @Count int = (select count(*) from @TableXml ta where ta.Processed = 0)

		
		while @Count > 0
		begin
			--Se otienen los campos necesarios de la tabla temporal que viene del xml
			select top 1 @CupsCode = ta.CupsCode, @RIASCupsId = ta.RIASCupsId , @Quantity = quantity , @Folio = Folio 
			From @TableXml ta
			Where ta.Processed = 0

				

			--Se obtiene el rango al cual la edad del paciente aplica por rias
			select @RiasCupsDetailId = ID , @AgeDaysMinimum = EDADMINIMA_DIAS , @AgeDaysMaximum = EDADMAXIMA_DIAS 
			from dbo.fnValidaRangoEdadRIASCUPS(@AgePatientInDays, @RIASCupsId) where REQUIEREORDENMED = 0
			
			print '@AgePatientInDays: ' + cast(@AgePatientInDays as varchar(50))
			print '@RIASCupsId: ' + cast(@RIASCupsId as varchar(50))
			print '@RiasCupsDetailId: ' + cast(@RiasCupsDetailId as varchar(50))
			

			--Si se encontró rango al cual aplicar se inserta en la tabla RiasCupsPaciente
			if @RiasCupsDetailId is not null and @RiasCupsDetailId > 0
			begin
				--Si la edad del paciente esta dentro de un rango entonces la fecha minima es la actual, y la fecha maxima es cuando el paciente cumpla la edad maxima del rango
				if (@AgePatientInDays BETWEEN @AgeDaysMinimum AND @AgeDaysMaximum)
				begin
					set @MinimumDate = [Common].[GETDATE]()
					set @MaximumDate = DATEADD(day, @AgeDaysMaximum, @DatePatient)
				end
			

				if exists(select * from RIASCUPSPACIENTE where IPCODPACI = @PatientCode  AND CODSERIPS = @CupsCode  AND IDRIASCUPS = @RIASCupsId  AND [Common].[GETDATE]() < FECHAMAXREALIZAR AND ESTADO = 1 )
				begin
					update dbo.RIASCUPSPACIENTE set FOLIO = @Folio , IDCITA = null, FECHAREALIZACION = null, ACCION = 2, CODUSUARUMOD = @UserCode,FECHAMODIFICACION = getdate()  where IPCODPACI = @PatientCode  AND CODSERIPS = @CupsCode  AND IDRIASCUPS = @RIASCupsId AND [Common].[GETDATE]() < FECHAMAXREALIZAR AND ESTADO = 1 
				end else begin
				   INSERT INTO dbo.RIASCUPSPACIENTE([IPCODPACI],[IDRIASCUPS],[CODSERIPS],[CANTIDAD],[ESTADO],[FOLIO],[NUMINGRES],[FECHAMINREALIZAR],[FECHAMAXREALIZAR],[FECHAREALIZACION],[IDCITA],[IDDETALLEORDENSERVICIO],[IDFACTURA],[VALORCUPS],[CODPROSAL],[CODUSUARU],[IDRIASCUPSD],[ORIGEN],[ACCION],[FECHACREACION])
				   VALUES(@PatientCode ,@RIASCupsId,@CupsCode,@Quantity,1,@Folio,NULL,@MinimumDate,@MaximumDate,@MinimumDate,NULL,NULL,NULL,NULL,NULL,@UserCode,@RiasCupsDetailId,2,1,[Common].[GETDATE]() )	
				end

			
			end

			--Se actualiza el estado Processed para evitar que siga en el loop
			update @TableXml  set Processed = 1 where CupsCode = @CupsCode and RIASCupsId = @RIASCupsId

			--Se disminuye el contador
			set @Count -= 1
		end
			
							
		select '0' as CodeMessage, 'Se realizó el proceso de inscripción correctamente' as [Message]

	end try
	begin catch
		select '999' as CodeMessage, ERROR_MESSAGE() as [Message]
	end catch

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que inscribe servicios o procedimientos CUPS de una orden médica (plan de manejo) en la ruta integral de atención en salud (RIAS) correspondiente a un paciente. Recibe un XML con los códigos CUPS y sus identificadores RIAS, calcula la edad del paciente en días a partir de su fecha de nacimiento (tabla INPACIENT), y valida mediante la función fnValidaRangoEdadRIASCUPS si el paciente cumple el rango de edad requerido para cada servicio RIAS sin necesidad de orden médica adicional. Si el rango es válido y el servicio ya existe pendiente en la tabla RIASCUPSPACIENTE actualiza el folio, de lo contrario crea un nuevo registro con las fechas mínima y máxima para realizar el procedimiento, el código del paciente, la cantidad indicada y el estado activo, dejando así la trazabilidad de los compromisos de atención preventiva o de seguimiento del paciente dentro del modelo de rutas integrales RIAS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_RIASInscriptionMedicalOrder';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_RIASInscriptionMedicalOrder';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Inscribe en el plan de manejo del paciente los CUPS de RIAS aplicables según su edad en días, creando o actualizando registros vigentes en RIASCUPSPACIENTE al guardar la historia clínica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIASInscriptionMedicalOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML debe contener nodos /RIASForPatient con CupsCode, RIASCupsId, Quantity y Folio; El RIASCupsId del XML debe existir en dbo.RIASCUPS; El paciente debe existir en INPACIENT con fecha de nacimiento (IPFECNACI) registrada para poder calcular su edad en días; Debe existir configuración de rangos de edad en fnValidaRangoEdadRIASCUPS para el RIAS-CUPS, marcada como que no requiere orden médica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIASInscriptionMedicalOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se inscriben CUPS de RIAS cuyo detalle de rango de edad NO requiere orden médica (REQUIEREORDENMED = 0); La inscripción solo aplica si la edad del paciente en días cae dentro de un rango configurado para el RIAS-CUPS; Nunca se duplican inscripciones activas: si existe una vigente (ESTADO=1 y FECHAMAXREALIZAR futura) se actualiza en lugar de insertar; Las nuevas inscripciones se crean con ESTADO=1, ORIGEN=1 y ACCION=2; La fecha máxima para realizar el CUPS se calcula como la fecha en que el paciente cumple la edad máxima del rango; Los errores se capturan y se devuelven como resultado con CodeMessage=''999'' en lugar de propagarse', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIASInscriptionMedicalOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; RIAS (Rutas Integrales de Atención en Salud); CUPS; Plan de manejo; Historia Clínica; Rango de edad por RIAS-CUPS; Orden médica; Folio; Inscripción de actividades RIAS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIASInscriptionMedicalOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] dbo.RIASCUPSPACIENTE: Cuando ya existe un registro con mismo IPCODPACI, CODSERIPS, IDRIASCUPS, ESTADO=1 y FECHAMAXREALIZAR > fecha actual, se actualizan FOLIO, IDCITA=NULL, FECHAREALIZACION=NULL, ACCION=2, usuario y fecha de modificación; [INSERT] dbo.RIASCUPSPACIENTE: Cuando no existe inscripción vigente y la edad del paciente cae en un rango RIAS sin requerir orden médica, se inserta el CUPS con ESTADO=1, ORIGEN=1, ACCION=2, fechas mín/máx calculadas según el rango y FECHACREACION = fecha actual; [RETURN_RESULT] ResultSet: Al finalizar sin error retorna CodeMessage=''0'' y mensaje de éxito; ante excepción retorna CodeMessage=''999'' con ERROR_MESSAGE()', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIASInscriptionMedicalOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe un detalle RIAS-CUPS aplicable a la edad del paciente (en días) y que NO requiere orden médica → Se procede a calcular fechas y a insertar/actualizar la inscripción del CUPS al paciente else Se omite el registro y se pasa al siguiente ítem del XML; si La edad del paciente en días está entre el mínimo y máximo del rango RIAS encontrado → Fecha mínima de realización = fecha actual; fecha máxima = fecha de nacimiento + edad máxima del rango en días; si Ya existe en RIASCUPSPACIENTE un registro activo (ESTADO=1) para el mismo paciente, CUPS y RIAS, cuya FECHAMAXREALIZAR aún no ha vencido → Se actualiza ese registro: nuevo FOLIO, ACCION=2, se limpian IDCITA y FECHAREALIZACION, y se registran usuario y fecha de modificación else Se inserta un nuevo registro en RIASCUPSPACIENTE con ESTADO=1, ORIGEN=1, ACCION=2 y las fechas calculadas', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIASInscriptionMedicalOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.RIASCUPS; INPACIENT; dbo.fnValidaRangoEdadRIASCUPS; dbo.RIASCUPSPACIENTE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIASInscriptionMedicalOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIASInscriptionMedicalOrder';
-- GO
