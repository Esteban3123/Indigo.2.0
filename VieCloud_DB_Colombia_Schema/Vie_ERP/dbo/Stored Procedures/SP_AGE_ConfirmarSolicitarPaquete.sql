-- =============================================
-- Author:		Rafael Patiño
-- Create date: 03-09-2018
-- Description:	Sp que registra los paquetes QX
-- =============================================
CREATE PROCEDURE [dbo].[SP_AGE_ConfirmarSolicitarPaquete]
	@Idprogramacion int,
	@user varchar(20),
	@Xml xml
AS
BEGIN

	SET NOCOUNT ON;

	declare @OrigenQX as integer
	declare @Identificacion as varchar(25)
	declare @CODENTIDAD as varchar(50)
	declare @CentroAtencion as varchar(50)
	declare @UnidadFuncional as varchar(50)
	declare @CareGroupId as integer
	declare @HealthAdministratorId as integer
	declare @FechaProceso as datetime = [Common].[GETDATE]()
	declare @Profesional as varchar(50)
	declare @Ingreso as decimal(18,0)
	declare @CentroCosto as varchar(20)
	declare @Almacen as varchar(20)
	declare @IDHCFARMAPC as integer
	declare @IdConsecu Varchar(8) = '00000001'
	declare @TIPSERIPS as integer
	declare @CODSERIPSQX as char(20)

	declare @TablaListaProductos table (
				RowId Int Identity(1,1) Primary Key,
				CODPRODUC varchar(100), 
				DESPRODUC varchar(200), 
				CANTIDAD int,
				ID int
				)

	BEGIN TRY 


	SELECT  @OrigenQX = A.ORIGENQX ,
			@Identificacion  = A.IPCODPACI, 
			@CODENTIDAD = B.CODENTIDA,
			@CentroAtencion = A.CODCENATE , 
			@UnidadFuncional = Sala.UFUCODIGO, 
			@CareGroupId = CG.Id , 
			@HealthAdministratorId = HA.Id,
			@Profesional = A.CODPROSAL , 
			@TIPSERIPS = Ser.TIPSERIPS,
			@CODSERIPSQX = A.CODSERIPS
	FROM	dbo.AGEPROGQX AS A  INNER JOIN
			dbo.INPACIENT AS B  ON A.IPCODPACI = B.IPCODPACI INNER JOIN
			dbo.INCUPSIPS  AS Ser  ON Ser.CODSERIPS = A.CODSERIPS INNER JOIN 
			dbo.AGENSALAC AS Sala  ON Sala.CODCONCEC = A.AGENSALAC INNER JOIN
			Contract.CareGroup as CG on CG.Id = B.GENCAREGROUP INNER JOIN 
			Contract.HealthAdministrator HA on HA.ID = B.GENCONENTITY 
	WHERE	A.CODAUTONU = @IDProgramacion



	if not exists (SELECT   *
	FROM	dbo.AGEPROGQX AS A  INNER JOIN
			dbo.INPACIENT AS B  ON A.IPCODPACI = B.IPCODPACI INNER JOIN
			dbo.INCUPSIPS  AS Ser  ON Ser.CODSERIPS = A.CODSERIPS INNER JOIN 
			dbo.AGENSALAC AS Sala  ON Sala.CODCONCEC = A.AGENSALAC INNER JOIN
			Contract.CareGroup as CG on CG.Id = B.GENCAREGROUP INNER JOIN 
			Contract.HealthAdministrator HA on HA.ID = B.GENCONENTITY 
	WHERE	A.CODAUTONU = @IDProgramacion) begin
			select '999' as CodeMessage, 0 as ConsecutivoFarmacia, 'paciente no tiene grupo de atención y/o entidad administradora de salud, debe actualizar información paciente' as Message
			return
	end


	IF Exists (SELECT t.x.value('CODPRODUC[1]','varchar(100)') FROM @Xml.nodes('/ListaProductos/CirugiaPrincipalPaquetesQX') t(x) where t.x.value('CODPRODUC[1]','varchar(100)') Is Null) Begin
		select '999' as CodeMessage, 0 as ConsecutivoFarmacia, 'No se encontró lista de productos' as Message
		return
	END


	insert @TablaListaProductos
	select 
		t.x.value('CODPRODUC[1]','varchar(100)'),
		t.x.value('DESPRODUC[1]','varchar(200)'),
		t.x.value('CANTIDAD[1]','int'),
		t.x.value('ID[1]','int')
	from @xml.nodes('/ListaProductos/CirugiaPrincipalPaquetesQX') t(x)

	--si es ambulatorio
	if @OrigenQX = 1 begin
		--si  hay radicacion 
		IF Exists(select  R.ID from AGEPROGQX A inner join ADRADICACIONQX R on A.IDRADICACIONQX = R.ID where A.CODAUTONU =@IDProgramacion ) BEGIN
			--tomamos entidad y grupo de atencion de la radicacion de cirugia
			set @CareGroupId = null
			set @HealthAdministratorId = null
			select  @CareGroupId = R.GENCAREGROUP , @HealthAdministratorId = R.GENCONENTITY from AGEPROGQX A  inner join ADRADICACIONQX R  on A.IDRADICACIONQX = R.ID where A.CODAUTONU =@IDProgramacion 
		END

		--Validamos si no tiene interfaz activa con UNIHEALTH ya que si no se tiene se debe crear un ingreso de tipo ambulatorio.
		IF not EXISTS(select INTFARMAC from HCPARFARM where CODCENATE = @CentroAtencion AND INTFARMAC = 1 AND ProveedorInterfaz = 1) begin 
			Update dbo.INCONSECU Set @Ingreso = CONNUMACT += 1  where IDCONSECU = @IdConsecu
										
			--Consultamos si el paciente cuenta con una estancia activa, para no permitir confirmar el paquete quirurgico 
			IF Exists(select  A.NUMINGRES from CHREGESTA A WHERE A.IPCODPACI = @Identificacion AND A.REGESTADO = 1) BEGIN
			SELECT '999' as CodeMessage, 0 as ConsecutivoFarmacia,  'No se puede confirmar el paquete quirurgico: Existe un ingreso de tipo hospitalario activo' as Message
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
								ICONTROLI,
								CODCENATE,
								UFUCODIGO,
								IESTADOIN,
								IREINGRES,
								UFUACTPAC,
								GENCAREGROUP,
								GENCONENTITY,
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
								29 ICAUSAING,
								@CODENTIDAD,
								@FechaProceso IFECHAING,
								1 ILIQUIDAC,
								'' ICONTROLI,
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
								0 AS INDAUDFOR, (SELECT TOP 1 Id FROM Admissions.HealthPurposes WHERE Code = '16') AS IdHealthPurposes, (select top 1 Id from EntryRoutesHealthServices where code = '6') as IdEntryRoutesHealthServices,
								(select top 1 Id From Admissions.AdmissionModalities where code = '1') as IdAdmissionModalities, 5 CareSettingCode

		END ELSE BEGIN --Si se tiene integración tomamos el ingreso creado desde el dashboard de procedimientos invasivos
				SELECT   top 1 @Ingreso = A.NUMINGRES 
					FROM	dbo.AGEPROGQX A  
					WHERE	A.CODAUTONU = @IDProgramacion AND A.IPCODPACI = @Identificacion
		END 

	end else begin
			
		--- TIPSERIPS  --> Tipo de Servicio 1: Laboratorios 2: Patologias 3: Imagenes Diagnosticas 
		--- 4: Procedimeintos no Qx 5: Procedimientos Qx 6: Interconsultas 7:Ninguno 8:Consulta Externa 

			--si es procedimiento No Qx
			if @TIPSERIPS = 4 
				begin
					SELECT   top 1 @Ingreso = N.NUMINGRES 
					FROM	dbo.AGEPROGQX A  
							INNER JOIN	dbo.HCORDPRON N  ON N.AUTO = A.AUTOHCORDPRON
							INNER JOIN  dbo.ADINGRESO I  ON I.NUMINGRES = N.NUMINGRES  
					WHERE	A.CODAUTONU = @IDProgramacion AND A.IPCODPACI = @Identificacion
				
					if @Ingreso is null begin
						select '999' as CodeMessage, 0 as ConsecutivoFarmacia, 'No se logró encontrar el numero del ingreso posiblemente porque se ha cambiado el tipo de servicio en CUPS de Qx a No Qx' as Message
						return
					end
					
					
					--Consultamos si el paciente cuenta con una estancia activa, para no permitir confirmar el paquete quirurgico 
					IF NOT Exists(select  A.NUMINGRES from CHREGESTA A WHERE A.IPCODPACI = @Identificacion AND A.REGESTADO = 1 AND A.NUMINGRES = cast(@Ingreso as varchar)) BEGIN
					SELECT '999' as CodeMessage, 0 as ConsecutivoFarmacia,  'No se puede confirmar el paquete quirurgico: El ingreso asociado no cuenta con una estancía activa' as Message
					return
					END


				end



			--si es procedimiento Qx
			else if @TIPSERIPS = 5 
				begin
						SELECT   top 1 @Ingreso = H.NUMINGRES 
						FROM	dbo.AGEPROGQX A  
								INNER JOIN	dbo.HCORDPROQ H  ON H.AUTO = A.AUTOHCORDPROQ 
								INNER JOIN  dbo.ADINGRESO I  ON I.NUMINGRES = H.NUMINGRES  
						WHERE	A.CODAUTONU = @IDProgramacion AND A.IPCODPACI = @Identificacion
		
			if @Ingreso is null begin
				select '999' as CodeMessage, 0 as ConsecutivoFarmacia, 'No se logró encontrar el numero del ingreso posiblemente porque se ha cambiado el tipo de servicio en CUPS de Qx a No Qx' as Message
				return
			end

				--Consultamos si el paciente cuenta con una estancia activa, para no permitir confirmar el paquete quirurgico 
				IF NOT Exists(select  A.NUMINGRES from CHREGESTA A WHERE A.IPCODPACI = @Identificacion AND A.REGESTADO = 1 AND A.NUMINGRES = cast(@Ingreso as varchar)) BEGIN
				SELECT '999' as CodeMessage, 0 as ConsecutivoFarmacia,  'No se puede confirmar el paquete quirurgico: El ingreso asociado no cuenta con una estancía activa' as Message
				return
				END
		end
											   		
	end


	select top 1 @centroCosto = CODCENCOS, @Almacen =CODBODEGA  from HCUNITHIS  where CODCENATE =@CentroAtencion AND UFUCODIGO =@UnidadFuncional AND CODTIPHIS <> 'ENF'
	
	if @centroCosto is null begin
		select '999' as CodeMessage, 0 as ConsecutivoFarmacia, 'No se encontraron parametros de centro costo para la unidad funcional: ' + @UnidadFuncional as Message
		return
	end
	if @Almacen is null begin
		select '999' as CodeMessage, 0 as ConsecutivoFarmacia, 'No se encontraron parametros de Almacen para la unidad funcional: ' + @UnidadFuncional as Message
		return
	end

	INSERT INTO [dbo].[HCFARMEPC]
           ([IDETIPHIS]
           ,[NUMEFOLIO]
           ,[FECHAORDE]
           ,[CODPROSAL]
           ,[IPCODPACI]
           ,[NUMINGRES]
           ,[CODCENATE]
           ,[UFUCODIGO]
           ,[CODCONCEP]
           ,[CODCONCES]
           ,[ORDESTADO]
           ,[CODBODEGA]
           ,[CODCENCOS]
           ,[ORDTRANUE]
           ,[JUSANULAC]
           ,[MEDICAMENTOVALIDADO]
		   ,[IDAGEPROGQX]
		   ,[TIPOSOLQX])
     VALUES
           (null
           ,null
           ,@FechaProceso
           ,@Profesional
           ,@Identificacion 
           ,@Ingreso
           ,@CentroAtencion 
           ,@UnidadFuncional 
           ,null
           ,null
           ,1
           ,@Almacen
           ,@centroCosto
           ,1 --tratamiento nuevo
           ,null
           ,null
		   ,@IDProgramacion,1)

		set @IDHCFARMAPC  = SCOPE_IDENTITY()

		INSERT INTO [dbo].[HCFARMEPD]
					([CODCONCEC]
					,[IDETIPHIS]
					,[NUMEFOLIO]
					,[IPCODPACI]
					,[NUMINGRES]
					,[CODCENATE]
					,[UFUCODIGO]
					,[CODPROSAL]
					,[CODPRODUC]
					,[DOSISPROD]
					,[CODUNIMED]
					,[FRECUENCI]
					,[UNIFRECUE]
					,[FECINIDOS]
					,[TIPFORMED]
					,[DURACIDOS]
					,[VALDURFIJ]
					,[UNIDURFIJ]
					,[CANPEDPRO]
					,[CANENTPRO]
					,[CANPENPRO]
					,[PROESTADO]
					,[TIPOREGIS]
					,[JUSTIINSU]
					,[RECIENACIDO]
					,[NOPOSPROD]
					,[IDAGEPROGQX]
					,[SENDTO],[CODSERIPS_QX])
			select 
					@IDHCFARMAPC
					,null
					,null
					,@identificacion
					,@ingreso
					,@CentroAtencion
					,@UnidadFuncional
					,@Profesional
					,PRO.CODPRODUC
					,null
					,null
					,null
					,null
					,@fechaproceso
					,null
					,'Dosis Unica'
					,null
					,null
					,SUM(A.CANTIDAD)  AS CantidadSolicitada
					,0 AS CantidadEntregada 
					,SUM(A.CANTIDAD)  AS CantidadPendiente
					,1 --pendiente
					,case PRO.TIPPRODUC when 1 then 1 else 2 END AS Tipo
					,null
					,null
					,PRO.NOPOSPROD
					,@IDProgramacion
					,1 --farmacia
					,@CODSERIPSQX  --[CODSERIPS_QX]
					FROM 
		   			@TablaListaProductos A inner join
					dbo.IHLISTPRO PRO   ON PRO.CODPRODUC=A.CODPRODUC WHERE A.CANTIDAD >  0 
					GROUP BY PRO.CODPRODUC, PRO.TIPPRODUC, PRO.NOPOSPROD

		------------Se guarda los paquetes solicitados en las tablas NursingPackagesOrder y NursingPackagesOrderDetail
		declare @IdNursingPackagesOrder as integer
		declare @Contador1 int = 0, @CantidadRegistro1 Int
		declare @TablaPaquete as table(id integer identity(1,1), IdPaquete int)
		     
		insert into @TablaPaquete 
		Select DISTINCT ID FROM @TablaListaProductos Where ID IS NOT NULL 

		set @CantidadRegistro1 = (select count(id) from @TablaPaquete)	
		WHILE @Contador1 < @cantidadRegistro1 BEGIN
			SET @Contador1  += 1
			declare @IdPaquete as int
			select @IdPaquete = IdPaquete from @TablaPaquete where id = @Contador1 

			INSERT INTO [MedicalHistory].[NursingPackagesOrder]
			   ([IDHCFARMEPC]
			   ,[IdAGPAQUETES]
			   ,[NUMINGRES]
			   ,[CODCENATE]
			   ,[UFUCODIGO]
			   ,[Status]
			   ,[CreationDate]
			   ,[CreationUser])
			VALUES
			   (@IDHCFARMAPC
			   ,@IdPaquete
			   ,@Ingreso
			   ,@CentroAtencion
			   ,@UnidadFuncional
			   ,1
			   ,@fechaproceso
			   ,@user)

			set @IdNursingPackagesOrder  = SCOPE_IDENTITY()

			INSERT INTO [MedicalHistory].[NursingPackagesOrderDetail]
						([IdNursingPackagesOrder]
						,[CODPRODUC]
						,[Quantity])
			Select 
						@IdNursingPackagesOrder
						,CODPRODUC
						,CANTIDAD  AS Quantity
						FROM 
		   				@TablaListaProductos WHERE ID = @IdPaquete
		END				
		-------------------------------------------------------------------------------------------------

				
				  --Insertamos en la tabla de kardex
					declare @IdConsecuKardex Varchar(8) = '00000011'
					declare @NombreUsuario as varchar(200) = (select rtrim(ltrim(CODUSUARI)) + ' - ' + rtrim(ltrim(NOMUSUARI)) as Usuario  from SEGusuaru where CODUSUARI = @user)
					declare @Contador int = 0, @cantidadRegistro Int
					declare @Tablaproducto as table(id integer identity(1,1),codigoProducto varchar(100),CantidadProducto int)
			
     
					insert into @Tablaproducto 
					select PRO.CODPRODUC ,A.CANTIDAD FROM @TablaListaProductos A inner join dbo.IHLISTPRO PRO   ON PRO.CODPRODUC=A.CODPRODUC 

					set @cantidadRegistro = (select count(id) from @Tablaproducto)
	
					WHILE @Contador < @cantidadRegistro BEGIN
						SET @Contador  += 1
						declare @Codigoproducto as varchar(200)
						declare @cantidad as int
						select @Codigoproducto = codigoProducto  , @cantidad = CantidadProducto from @Tablaproducto where id = @Contador 

						INSERT INTO [dbo].[HCKARDPAC]([NUMCONSEC],[IPCODPACI],[NUMINGRES],[CODCENATE],[UFUCODIGO],[CODPROSAL],[CODPRODUC],[CANPRODUCT],[TIPREGIST],[HCPRESCRN],[HCSOLINSN],[HCCTRAPLN],[HCCTRAPLM],[CODDOCUME],[FECREGKAR],[TIPORIREG]
						   ,[DESMOVPRO],[JUSANULAC],[CONSECFAR],[FECHAUTIL],[OBSERVACI])
						values(
							NEWID()
						   ,@Identificacion
						   ,@Ingreso
						   ,@CentroAtencion
						   ,@UnidadFuncional
						   ,@Profesional
						   ,@Codigoproducto
						   ,@cantidad
						   ,1--entrada
						   ,NULL
						   ,NULL
						   ,NULL
						   ,NULL
						   ,NULL
						   ,@FechaProceso
						   ,0
						   ,'Solicitud desde confirmación de paquetes quirúrgicos - (Cirugia) - Usuario: ' + @NombreUsuario
						   ,NULL
						   ,NULL
						   ,NULL
						   ,NULL)	

						--actualizamos consecutivo kardex
						-- Update dbo.INCONSECU Set CONNUMACT += 1 output inserted.CONNUMACT   where IDCONSECU = @IdConsecuKardex

					END

				--si es ambulatorio
				if @OrigenQX = 1 begin
					update AGEPROGQX set ESTADOFARM = 2, NUMINGRES = @ingreso where CODAUTONU = @IDProgramacion or IDPADRE = @IDProgramacion
				end else begin
					update AGEPROGQX set ESTADOFARM = 2 where CODAUTONU = @IDProgramacion or IDPADRE = @IDProgramacion
				end

				select '0' as CodeMessage, @IDHCFARMAPC as ConsecutivoFarmacia, 'Se guardo correctamente'  Message 

	end try
	begin catch
				select '999' as CodeMessage, 0 as ConsecutivoFarmacia, ERROR_MESSAGE() + ', Linea: ' + cast(ERROR_LINE() as varchar(20)) Message
	end catch
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra y confirma paquetes quirúrgicos (QX) en el sistema de agendamiento. A partir del identificador de una programación quirúrgica, recupera los datos del paciente (cédula, entidad pagadora, grupo de atención, centro de atención, unidad funcional y profesional) cruzando la programación con la tabla de pacientes, el catálogo de servicios CUPS/IPS, las salas de atención, el grupo de contrato y la administradora de salud (EPS). Según el origen de la cirugía (ambulatoria u hospitalaria), crea automáticamente un ingreso provisional en ADINGRESO si no existe integración activa con el sistema de farmacia externo (UNIHEALTH), o toma el ingreso ya creado; también valida que no exista un ingreso hospitalario activo que bloquee la confirmación. Finalmente, procesa la lista de productos/medicamentos enviada en formato XML para registrar la receta o despacho farmacéutico asociado al paquete quirúrgico, generando el consecutivo de farmacia correspondiente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AGE_ConfirmarSolicitarPaquete';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AGE_ConfirmarSolicitarPaquete';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Confirma la solicitud de un paquete quirúrgico: valida estado del paciente/ingreso, crea (o reutiliza) el ingreso, registra la receta/despacho farmacéutico, los paquetes de enfermería y los movimientos de kardex asociados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ConfirmarSolicitarPaquete';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La programación quirúrgica debe existir y el paciente debe tener grupo de atención (GENCAREGROUP) y entidad administradora (GENCONENTITY) asociados; El XML de productos debe contener al menos un nodo con CODPRODUC no nulo en /ListaProductos/CirugiaPrincipalPaquetesQX; La unidad funcional del centro de atención debe tener parametrizados centro de costo y almacén en HCUNITHIS (CODTIPHIS distinto de ''ENF''); Para procedimientos no Qx (TIPSERIPS=4) y Qx (TIPSERIPS=5) debe existir un ingreso asociado vía HCORDPRON/HCORDPROQ con estancia activa en CHREGESTA; Para origen ambulatorio sin interfaz UNIHEALTH activa, el paciente no debe tener un ingreso hospitalario activo (CHREGESTA con REGESTADO=1)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ConfirmarSolicitarPaquete';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] dbo.INCONSECU: Cuando origen es ambulatorio (ORIGENQX=1) y el centro no tiene interfaz farmacia activa con UNIHEALTH (HCPARFARM.INTFARMAC=1 y ProveedorInterfaz=1), incrementa CONNUMACT del consecutivo ''00000001'' para obtener un nuevo número de ingreso; [INSERT] dbo.ADINGRESO: Cuando origen es ambulatorio y no hay interfaz UNIHEALTH activa ni ingreso hospitalario activo, crea un ingreso ambulatorio con TIPOINGRE=1, ICAUSAING=29, HealthPurposes code=''16'', EntryRoute code=''6'' y AdmissionModality code=''1''; [INSERT] dbo.HCFARMEPC: Tras pasar todas las validaciones, inserta el encabezado de receta/despacho farmacéutico con ORDESTADO=1, ORDTRANUE=1 (tratamiento nuevo), TIPOSOLQX=1 y enlace a la programación QX; [INSERT] dbo.HCFARMEPD: Por cada producto del XML con CANTIDAD>0, inserta detalle agrupado por CODPRODUC con cantidad solicitada y pendiente igual a SUM(CANTIDAD), TIPFORMED=''Dosis Unica'', PROESTADO=1 (pendiente), SENDTO=1 (farmacia) y TIPOREGIS=1 si TIPPRODUC=1, sino 2; [INSERT] MedicalHistory.NursingPackagesOrder: Por cada ID de paquete distinto recibido en el XML (no nulo), inserta una orden de paquete de enfermería con Status=1 vinculada al encabezado de farmacia; [INSERT] MedicalHistory.NursingPackagesOrderDetail: Por cada paquete insertado, registra el detalle con los productos y cantidades del XML correspondientes a ese ID de paquete; [INSERT] dbo.HCKARDPAC: Por cada producto de la lista, registra movimiento de kardex con TIPREGIST=1 (entrada), TIPORIREG=0 y descripción ''Solicitud desde confirmación de paquetes quirúrgicos - (Cirugia) - Usuario: <usuario>''; [UPDATE] dbo.AGEPROGQX: Si origen es ambulatorio (ORIGENQX=1), actualiza ESTADOFARM=2 y NUMINGRES en la programación y sus hijas (IDPADRE); si no, solo actualiza ESTADOFARM=2; [RETURN_RESULT] RESULT: Devuelve CodeMessage=''999'' con mensaje específico ante cada validación fallida; si todo ok, devuelve CodeMessage=''0'', el ID de HCFARMEPC como ConsecutivoFarmacia y mensaje ''Se guardo correctamente''; [RETURN_RESULT] RESULT: Ante excepción no controlada, devuelve CodeMessage=''999'', ConsecutivoFarmacia=0 y el ERROR_MESSAGE con número de línea', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ConfirmarSolicitarPaquete';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ConfirmarSolicitarPaquete';
-- GO
