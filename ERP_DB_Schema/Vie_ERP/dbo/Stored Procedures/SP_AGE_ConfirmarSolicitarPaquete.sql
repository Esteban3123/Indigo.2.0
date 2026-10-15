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
			@TIPSERIPS = Ser.TIPSERIPS
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
								INDAUDFOR, IdHealthPurposes, IdEntryRoutesHealthServices,IdAdmissionModalities)
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
								(select top 1 Id From Admissions.AdmissionModalities where code = '1') as IdAdmissionModalities

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
					,[SENDTO])
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