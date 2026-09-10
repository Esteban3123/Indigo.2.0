

-- =============================================
-- Author:		Carlos Mario Arias Rubiano-- Create date: 25/02/2019
-- Description:	Se encarga de validar el estado del proceso y cambiarlo para poder realizar la orden de servicio
-- =============================================
CREATE PROCEDURE [Billing].[SP_ProcessAccountControlAmbulatory]
	@AdmissionNumber varchar(20), --Si viene lleno es porque se va a validar el ingreso(Ya sea para validar o crearlo) y si viene vacio es porque se va a insertar en la tabla en donde se relaciona los procedimientos odontologicos con la orden de servicio
	@IsCurrentAdmission bit, --Permite saber si se esta realizando el proceso con el ingreso actual o se debe crear un nuevo ingreso
	@Xml xml --Es en donde se obtiene la información para realizar los diferentes procesos
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	--Id de la tabla INCONSECU para obtener el consecutivo del numero de ingreso y generar el ingreso como tal
	declare @IDCONSECU varchar(8) = '00000001'

	--Consecutivo que se obtiene para generar el numero de ingreso
	declare @CONNUMACT varchar(10)

	--Variables que se utilizan para la creación del nuevo ingreso
	declare @IPCODPACI varchar(25), 
			@ITIPORIES int, 
			@ICAUSAING int, 
			@CODENTIDA varchar(20), 
			@CODCENATE varchar(20), 
			@UFUCODIGO varchar(20), 
			@ILIQUIDAC int,
			@GENCAREGROUP int, 
			@GENCONENTITY int, 
			@CODUSUCRE varchar(20), 
			@ISOATVALO numeric(18, 2), 
			@ISALCODIG varchar(5), 
			@IOBSERVAC varchar(max), 
			@IAUTORIZA varchar(15),
			@IdEntryRoutesHealthServices int,
			@IdHealthPurposes int,
			@IdAdmissionModalities int 

	--Variable que me identifica desde donde se estan generando los detalles para asi poder realizar las acciones especificas
	declare @TypeGrid int

	--Tabla temporal para recibir la información del xml y poder generar registros en la tabla correspondiente
	declare @TableInfoXml table(AdmissionNumber varchar(20), Folio varchar(10), ProcedureCode int, EntityId int, ServiceOrderId int, CareCenterCode varchar(20), FunctionalUnitCode varchar(20),
	PatientCode varchar(20), ProfessionalCode varchar(20), CupsEntityCode varchar(20), Quantity int, [Date] DateTime)

	begin try
		
		--Si viene lleno es porque se va a validar o crear el ingreso
		if @AdmissionNumber <> ''
		begin
			--Se valida si se esta realizando el proceso con el ingreso actual
			if @IsCurrentAdmission = 1
			begin
				--Se obtiene el estado del ingreso
				declare @AdmissionStatus varchar(5) = (select top 1 IESTADOIN from .ADINGRESO where NUMINGRES = @AdmissionNumber)

				--Se valida si el ingreso esta anulado
				if @AdmissionStatus = 'A'
				begin
					--Retorna validación
					select 999 as CodeResult, 'No se puede generar la orden de servicio con el ingreso actual porque se encuentra anulado' as MessageResult, '' as AdmissionNumber		
					return
				end

				--Se valida si el ingreso esta facturado o cerrado
				if @AdmissionStatus = 'F' or @AdmissionStatus = 'C'
				begin
					--Se actualiza el estado a P para poder generar la orden de servicio
					update .ADINGRESO set IESTADOIN = 'P' where NUMINGRES = @AdmissionNumber
				end
			end
			else begin --Si se esta realizando el proceso con un ingreso nuevo
			
				----Se actualiza el nuevo consecutivo
				--update dbo.INCONSECU set @CONNUMACT = CONNUMACT = CONNUMACT  + 1 where IDCONSECU = @IDCONSECU
				set @CONNUMACT = CONVERT(varchar(10), right(newid(),10))
				
				--Se obtienen los campos necesarios del xml para crear el ingreso
				select 
				@IPCODPACI = t.x.value('IPCODPACI[1]','varchar(15)'),
				@ITIPORIES = t.x.value('ITIPORIES[1]','int'),
				@ICAUSAING = t.x.value('ICAUSAING[1]','int'),
				@CODENTIDA = t.x.value('CODENTIDA[1]','varchar(20)'),
				@CODCENATE = t.x.value('CODCENATE[1]','varchar(20)'),
				@GENCAREGROUP = t.x.value('GENCAREGROUP[1]','int'),
				@GENCONENTITY = t.x.value('GENCONENTITY[1]','int'),
				@CODUSUCRE = t.x.value('CODUSUCRE[1]','varchar(20)'),
				@ISOATVALO = t.x.value('ISOATVALO[1]','numeric(18, 2)'),
				@ISALCODIG = t.x.value('ISALCODIG[1]','varchar(5)'),
				@IOBSERVAC = t.x.value('IOBSERVAC[1]','varchar(max)'),
				@UFUCODIGO = t.x.value('UFUCODIGO[1]','varchar(20)'),
				@IAUTORIZA = t.x.value('IAUTORIZA[1]','varchar(15)'),
				@IdEntryRoutesHealthServices = t.x.value(' IdEntryRoutesHealthServices[1]','int'),
				@IdHealthPurposes = t.x.value('IdHealthPurposes[1]','int'),
				@IdAdmissionModalities = t.x.value('IdAdmissionModalities[1]','int')
				from @Xml.nodes('/Header') t(x)

				--Se obtienen los campos ILIQUIDAC y UFUCODIGO del ingreso actual
				select top 1 @ILIQUIDAC = ILIQUIDAC from .ADINGRESO where NUMINGRES = @AdmissionNumber

				--Se obtiene el código de la entidad administradora de salud, se realiza asi con este where ya que en esa variable(@CODENTIDA) se envia el id de la entidad desde el aplicativo
				select top 1 @CODENTIDA = Code from Contract.HealthAdministrator where Id = @CODENTIDA

				--Se valida si la entidad existe en crystal
				if not exists(select top 1 * from .INENTIDAD where CODENTIDA = @CODENTIDA)
				begin
					--Retorna validación
					select 999 as CodeResult, 'No existe la entidad administradora de salud ' + @CODENTIDA + ' en Indigo Crystal' as MessageResult, '' as AdmissionNumber		
					return
				end

				--Se crea el ingreso
				insert into dbo.ADINGRESO(NUMINGRES, IPCODPACI, TIPOINGRE, IINGREPOR, ITIPORIES, ICAUSAING, CODENTIDA, IFECHAING, ILIQUIDAC,
				ICONTROLI, CODCENATE, UFUCODIGO, IESTADOIN, IREINGRES, UFUACTPAC, GENCAREGROUP, GENCONENTITY, CODUSUCRE, FECREGCRE, INDAUDFOR,
				ISOATVALO, ISALCODIG, IOBSERVAC, IAUTORIZA,IdEntryRoutesHealthServices,IdHealthPurposes,IdAdmissionModalities, CareSettingCode)
				values(CAST((@CONNUMACT) as varchar(10)), @IPCODPACI, 1, 2, @ITIPORIES, @ICAUSAING, @CODENTIDA, [Common].[GETDATE](), @ILIQUIDAC, '', @CODCENATE, @UFUCODIGO, '', 0, @UFUCODIGO, 
				@GENCAREGROUP, @GENCONENTITY, @CODUSUCRE, [Common].[GETDATE](), 0,
				IIF(@ITIPORIES = 2, @ISOATVALO, NULL), IIF(@ITIPORIES = 2, @ISALCODIG, NULL), @IOBSERVAC, @IAUTORIZA, @IdEntryRoutesHealthServices,@IdHealthPurposes,@IdAdmissionModalities, 5)

				--Se asigna el numero de ingreso que se acaba de crear para poder devolver
				set @AdmissionNumber = CAST((@CONNUMACT) as varchar(10))
			end
		end
		else begin --Si viene vacio es porque se va a insertar registros en la tabla en donde se almacena la relacion de los procedimientos odontologicos y la orden de servicio
			--Se obtiene el campo de la cabecera que me identifica desde donde se generaron los detalles
			select 
			@TypeGrid = t.x.value('TypeGrid[1]','int')
			from @Xml.nodes('/Header') t(x)

			--Se obtiene la info del xml
			insert into @TableInfoXml
			select 
			t.x.value('AdmissionNumber[1]','varchar(20)') as AdmissionNumber,
			t.x.value('Folio[1]','varchar(10)') as Folio,
			t.x.value('ProcedureCode[1]','int') as ProcedureCode,
			t.x.value('EntityId[1]','int') as EntityId,
			t.x.value('ServiceOrderId[1]','int') as ServiceOrderId,
			t.x.value('CareCenterCode[1]','varchar(20)') as CareCenterCode,
			t.x.value('FunctionalUnitCode[1]','varchar(20)') as FunctionalUnitCode,
			t.x.value('PatientCode[1]','varchar(20)') as PatientCode,
			t.x.value('ProfessionalCode[1]','varchar(20)') as ProfessionalCode,
			t.x.value('CupsEntityCode[1]','varchar(20)') as CupsEntityCode,
			t.x.value('Quantity[1]','int') as Quantity,
			t.x.value('Date[1]','datetime') as [Date]
			from @Xml.nodes('/Header/Details') t(x)

			if @TypeGrid = 0 --Si los detalles fueron generados desde la rejilla de procedimientos odontologicos se guarda en la tabla de control
			begin
				--Se inserta la info obtenida en la tabla donde relaciono los procedimientos odontologicos con la orden de servicio
				insert into Billing.BillingOdontologyProcedureServiceOrder(AdmissionNumber, Folio, ProcedureCode, ServiceOrderId)
				select AdmissionNumber, Folio, ProcedureCode, ServiceOrderId
				from @TableInfoXml
			end
			else if @TypeGrid = 1 --Si los detalles fueron generados desde la rejilla de laboratorios se actualiza el campo GENSERVICEORDER la tabla HCORDLABO
			begin
				--Se actualiza el campo GENSERVICEORDER de la tabla HCORDLABO
				update h set h.GENSERVICEORDER = t.ServiceOrderId
				from @TableInfoXml t
				inner join .HCORDLABO h on h.[AUTO] = t.EntityId

				--Se insertan registros en la tabla AMBORDLAB
				insert into .AMBORDLAB(IPCODPACI,NUMINGRES,CODCENATE,UFUCODIGO,CODPROSAL,FECORDMED,CODSERIPS,CANSERIPS,ESTSERIPS,INDAUDFOR,ESTALELAB,
				IPFECHACO,NUMCONCIT,GENCAREGROUP,GENCONENTITY,GENINVOICE,GENINVOICEID,GENSERVICEORDER)
				select PatientCode, AdmissionNumber, CareCenterCode, FunctionalUnitCode, ProfessionalCode, Common.GETDATE(), CupsEntityCode, Quantity, 1, 0, 0, [Date], null, null, null, null, null,ServiceOrderId
				from @TableInfoXml
			end
			else if @TypeGrid = 2 --Si los detalles fueron generados desde la rejilla de patologias se actualiza el campo GENSERVICEORDER la tabla HCORDPATO
			begin
				--Se actualiza el campo GENSERVICEORDER de la tabla HCORDPATO
				update h set h.GENSERVICEORDER = t.ServiceOrderId
				from @TableInfoXml t
				inner join .HCORDPATO h on h.[AUTO] = t.EntityId
			end
			else if @TypeGrid = 3 --Si los detalles fueron generados desde la rejilla de imagenes se actualiza el campo GENSERVICEORDER la tabla HCORDIMAG
			begin
				--Se actualiza el campo GENSERVICEORDER de la tabla HCORDIMAG
				update h set h.GENSERVICEORDER = t.ServiceOrderId
				from @TableInfoXml t
				inner join .HCORDIMAG h on h.[AUTO] = t.EntityId

				--Se insertan registros en la tabla AMBORDIMA
				insert into .AMBORDIMA(IPCODPACI,NUMINGRES,CODCENATE,UFUCODIGO,CODPROSAL,FECORDMED,CODSERIPS,CANSERIPS,
				ESTSERIPS,INDAUDFOR,SERREAINT,SERTRANSC,SERVALMED,ESTALEIMG,REALINOTIF,IPFECHACO,NUMCONCIT,GENCAREGROUP,
				GENCONENTITY,GENINVOICE,GENINVOICEID, TIENEGRABACION,GENSERVICEORDER)
				select PatientCode, AdmissionNumber, CareCenterCode, FunctionalUnitCode, ProfessionalCode, Common.GETDATE(), CupsEntityCode, Quantity, 1, 0, 0, 0, 0, 0, 0, 
				[Date], null, null, null, null, null, 0 ,(SELECT TOP 1 t.ServiceOrderId  from @TableInfoXml t)
				from @TableInfoXml
			end
			else if @TypeGrid = 4 --Si los detalles fueron generados desde la rejilla de interconsultas se actualiza el campo GENSERVICEORDER la tabla HCORDINTE
			begin
				--Se actualiza el campo GENSERVICEORDER de la tabla HCORDINTE
				update h set h.GENSERVICEORDER = t.ServiceOrderId
				from @TableInfoXml t
				inner join .HCORDINTE h on h.[AUTO] = t.EntityId
			end
			else if @TypeGrid = 5 --Si los detalles fueron generados desde la rejilla de notas administrativas se actualiza el campo GENSERVICEORDER la tabla NTNOTASADMINISTRATIVASCUPS
			begin
				--Se actualiza el campo GENSERVICEORDER de la tabla NTNOTASADMINISTRATIVASCUPS
				update h set h.GENSERVICEORDER = t.ServiceOrderId
				from @TableInfoXml t
				inner join .NTNOTASADMINISTRATIVASCUPS h on h.ID = t.EntityId
			end
			
		end
		
		--Retorna el ok
		select 0 as CodeResult, 'Se realizó el proceso correctamente' as MessageResult, @AdmissionNumber as AdmissionNumber

	end try
	begin catch

		--Retorna el error
		select 999 as CodeResult, ERROR_MESSAGE() as MessageResult, '' as AdmissionNumber

	end catch

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que gestiona el control de cuentas ambulatorias (incluyendo odontología) para la generación de órdenes de servicio. Según los parámetros recibidos, valida el estado del ingreso actual en ADINGRESO —permitiendo reabrirlo si está facturado o cerrado, o bloqueando el proceso si está anulado— o crea un nuevo ingreso ambulatorio a partir de datos enviados en formato XML, verificando previamente que la entidad administradora de salud (EPS/ARS) exista tanto en HealthAdministrator como en INENTIDAD. También registra la relación entre procedimientos odontológicos y órdenes de servicio cuando no se indica un número de ingreso. Es clave en el flujo de facturación ambulatoria y consulta externa del ERP/EHR.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_ProcessAccountControlAmbulatory';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_ProcessAccountControlAmbulatory';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Valida o crea el ingreso ambulatorio para habilitar la generación de la orden de servicio, o registra la relación entre los procedimientos (odontología, laboratorio, patología, imágenes, interconsulta, notas administrativas) y la orden de servicio según el origen indicado.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ProcessAccountControlAmbulatory';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cuando se valida o crea ingreso, el XML debe traer en /Header los datos del paciente, tipo de ingreso, causa, entidad, centro de atención, unidad funcional, usuario y autorización.; Cuando se relacionan detalles, el XML debe traer /Header/Details con AdmissionNumber, ProcedureCode, EntityId, ServiceOrderId y demás datos del procedimiento.; El @CODENTIDA recibido en el XML representa el Id en Contract.HealthAdministrator (no el código), y debe existir tanto en Contract.HealthAdministrator como en INENTIDAD (Crystal).; Si @IsCurrentAdmission=1, el ingreso identificado por @AdmissionNumber debe existir en ADINGRESO.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ProcessAccountControlAmbulatory';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] dbo.ADINGRESO: Si @IsCurrentAdmission=1 y el estado IESTADOIN del ingreso es ''F'' (facturado) o ''C'' (cerrado), se actualiza IESTADOIN=''P'' para permitir generar la orden de servicio.; [INSERT] dbo.ADINGRESO: Si @IsCurrentAdmission=0 y la entidad existe en INENTIDAD, se crea un nuevo ingreso con NUMINGRES generado a partir de RIGHT(NEWID(),10), TIPOINGRE=1, IINGREPOR=2, IESTADOIN='''', IREINGRES=0, INDAUDFOR=0, fechas vía Common.GETDATE(); ISOATVALO e ISALCODIG sólo se guardan si ITIPORIES=2 (SOAT), de lo contrario NULL.; [INSERT] Billing.BillingOdontologyProcedureServiceOrder: Si @AdmissionNumber está vacío y @TypeGrid=0, inserta la relación procedimiento odontológico–orden de servicio (AdmissionNumber, Folio, ProcedureCode, ServiceOrderId) por cada detalle del XML.; [UPDATE] dbo.HCORDLABO: Si @TypeGrid=1, actualiza GENSERVICEORDER con el ServiceOrderId del XML para los registros cuyo AUTO coincide con EntityId.; [INSERT] dbo.AMBORDLAB: Si @TypeGrid=1, inserta la orden de laboratorio ambulatoria con ESTSERIPS=1, INDAUDFOR=0, ESTALELAB=0 y el GENSERVICEORDER recibido.; [UPDATE] dbo.HCORDPATO: Si @TypeGrid=2, actualiza GENSERVICEORDER con el ServiceOrderId del XML para los registros cuyo AUTO coincide con EntityId.; [UPDATE] dbo.HCORDIMAG: Si @TypeGrid=3, actualiza GENSERVICEORDER con el ServiceOrderId del XML para los registros cuyo AUTO coincide con EntityId.; [INSERT] dbo.AMBORDIMA: Si @TypeGrid=3, inserta la orden de imágenes ambulatoria con ESTSERIPS=1, banderas auxiliares en 0 y GENSERVICEORDER tomado del primer registro de la tabla temporal.; [UPDATE] dbo.HCORDINTE: Si @TypeGrid=4, actualiza GENSERVICEORDER con el ServiceOrderId del XML para los registros cuyo AUTO coincide con EntityId.; [UPDATE] dbo.NTNOTASADMINISTRATIVASCUPS: Si @TypeGrid=5, actualiza GENSERVICEORDER con el ServiceOrderId del XML para los registros cuyo ID coincide con EntityId.; [RETURN_RESULT] (resultset): Devuelve CodeResult=999 con mensaje de error y AdmissionNumber='''' cuando el ingreso está anulado (IESTADOIN=''A''), cuando la entidad no existe en INENTIDAD, o cuando el bloque CATCH captura una excepción (ERROR_MESSAGE()). Devuelve CodeResult=0 con el AdmissionNumber resultante en caso de éxito.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ProcessAccountControlAmbulatory';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @AdmissionNumber <> '''' AND @IsCurrentAdmission = 1 → Lee IESTADOIN del ingreso y, si es ''A'' aborta con error; si es ''F'' o ''C'' lo cambia a ''P''. else Si @AdmissionNumber<>'''' y @IsCurrentAdmission=0, parsea el XML y crea un nuevo ingreso en ADINGRESO.; si @AdmissionNumber = '''' → Carga los detalles del XML en tabla temporal y enruta según @TypeGrid (0..5) hacia la tabla de procedimientos correspondiente.; si @ITIPORIES = 2 (ingreso SOAT) al crear el ingreso → Persiste ISOATVALO e ISALCODIG. else Guarda NULL en ambos campos.; si NOT EXISTS en INENTIDAD para @CODENTIDA tras resolverlo desde Contract.HealthAdministrator → Retorna error 999 indicando que la entidad no existe en Indigo Crystal y termina sin crear el ingreso.; si @TypeGrid IN (0,1,2,3,4,5) → Cada valor dispara una rama distinta: 0=odontología (insert relación), 1=laboratorios (update HCORDLABO + insert AMBORDLAB), 2=patologías (update HCORDPATO), 3=imágenes (update HCORDIMAG + insert AMBORDIMA), 4=interconsultas (update HCORDINTE), 5=notas administrativas (update NTNOTASADMINISTRATIVASCUPS).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ProcessAccountControlAmbulatory';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ProcessAccountControlAmbulatory';
-- GO
