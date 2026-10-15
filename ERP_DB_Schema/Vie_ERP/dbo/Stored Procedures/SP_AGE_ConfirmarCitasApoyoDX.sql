-- =============================================
-- Author:		Rafael Patiño
-- Create date: 23-09-2020
-- Description:	Sp que confirma los medicamentos de citas de apoyo diagnostico (imagenes - otros procedimientos)
-- =============================================
CREATE PROCEDURE [dbo].[SP_AGE_ConfirmarCitasApoyoDX]
	@IdCita int,
	@Ingreso varchar(100),
	@CodigoProfesional varchar(100),
	@user varchar(20),
	@Xml xml
AS
BEGIN

	SET NOCOUNT ON;

	declare @Identificacion as varchar(25)
	declare @CODENTIDAD as varchar(50)
	declare @CentroAtencion as varchar(50)
	declare @UnidadFuncional as varchar(50)
	declare @CareGroupId as integer
	declare @HealthAdministratorId as integer
	declare @FechaProceso as datetime = [Common].[GETDATE]()
	declare @CentroCosto as varchar(20)
	declare @Almacen as varchar(20)
	declare @IDHCFARMAPC as integer
	declare @EstadoCita as varchar(1)

	declare @FechaCita as datetime
	declare @IdOrdenQuimio as integer
	declare @IdCiclo as integer
	


	declare @TablaListaProductos table (
				RowId Int Identity(1,1) Primary Key,
				CODPRODUC varchar(100), 
				DESPRODUC varchar(200),
				DOSIS numeric(18,2),
				UNIDAD varchar(20),
				VIA varchar(20),
				CANTIDAD int,
				Tipo int
				)

	BEGIN TRY 


	SELECT   @Identificacion  = A.IPCODPACI,@CODENTIDAD = B.CODENTIDA, @CentroAtencion = A.CODCENATE , @UnidadFuncional = Sala.UFUCODIGO, 
			 @CareGroupId = CG.Id , @HealthAdministratorId = HA.Id, @FechaCita = A.FECHORAIN, @IdCiclo = A.IDHCORDCICLOSD  , @EstadoCita = A.CODESTCIT 
	FROM	dbo.AGASICITA  AS A with(nolock) INNER JOIN
			dbo.INPACIENT AS B with(nolock) ON A.IPCODPACI = B.IPCODPACI INNER JOIN
			dbo.AGENSALAC AS Sala with(nolock) ON Sala.CODCONCEC = A.IDSALA left JOIN
			Contract.CareGroup as CG on CG.Id = B.GENCAREGROUP left JOIN 
			Contract.HealthAdministrator HA on HA.ID = B.GENCONENTITY 
	WHERE	A.CODAUTONU = @IdCita  

	if @EstadoCita <> 0 begin
	      select '999' as CodeMessage, 'La cita esta en estado diferente a asignada, no se puede confirmar cita' Message
	end


	insert @TablaListaProductos
	select 
		t.x.value('CodigoProducto[1]','varchar(100)'),
		t.x.value('NombreProducto[1]','varchar(200)'),
		convert(numeric(18,2),t.x.value('Dosis[1]','numeric(18,2)')),
		t.x.value('CODUNIMED[1]','varchar(20)'),
		t.x.value('CODVIAADM[1]','varchar(20)'),
		t.x.value('Cantidad[1]','int'),
		t.x.value('Tipo[1]','int')
	from @xml.nodes('/ListaProductos/MedQuimio') t(x)
	
	
	if @Ingreso <> '' begin 
		if not exists(SELECT TOP 1 NUMINGRES FROM ADINGRESO WHERE NUMINGRES  = @Ingreso AND IESTADOIN IN ('','P')) begin
			select '999' as CodeMessage, 'El ingreso no se encuentra abierto: ' + @Ingreso as Message
			return
		end
	end

	select top 1 @centroCosto = CODCENCOS, @Almacen =CODBODEGA  from HCUNITHIS with(nolock) where CODCENATE =@CentroAtencion AND UFUCODIGO =@UnidadFuncional AND CODTIPHIS <> 'ENF'
	if @centroCosto is null begin
		select '999' as CodeMessage, 'No se encontraron parametros de centro costo para la unidad funcional: ' + @UnidadFuncional as Message
		return
	end
	if @Almacen is null begin
		select '999' as CodeMessage, 'No se encontraron parametros de Almacen para la unidad funcional: ' + @UnidadFuncional as Message
		return
	end


	IF exists( select CODPRODUC from @TablaListaProductos) BEGIN


			--insertamos cabecera de farmacia
			INSERT INTO [dbo].[HCFARMEPC]
				   ([IDETIPHIS],[NUMEFOLIO],[FECHAORDE],[CODPROSAL],[IPCODPACI],[NUMINGRES],[CODCENATE],[UFUCODIGO],[CODCONCEP],[CODCONCES],[ORDESTADO],[CODBODEGA],[CODCENCOS],[ORDTRANUE]
				   ,[JUSANULAC],[MEDICAMENTOVALIDADO],[IDAGEPROGQX],[TIPOSOLQX],[ORDENQUIMIO],[IDCITA])
			VALUES
			(null
			,null
			,@FechaProceso
			,@CodigoProfesional
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
			,NULL,NULL,NULL,@IdCita) 

			--capturamos ID generado
			SET @IDHCFARMAPC  = SCOPE_IDENTITY()

			--insertamos en tabla detalle de farmacia
			INSERT INTO [dbo].[HCFARMEPD]([CODCONCEC],[IDETIPHIS],[NUMEFOLIO],[IPCODPACI],[NUMINGRES],[CODCENATE],[UFUCODIGO],[CODPROSAL],[CODPRODUC],[DOSISPROD],[CODUNIMED]
						,[FRECUENCI],[UNIFRECUE],[FECINIDOS],[TIPFORMED],[DURACIDOS],[VALDURFIJ],[UNIDURFIJ],[CANPEDPRO],[CANENTPRO],[CANPENPRO],[PROESTADO],[TIPOREGIS],[JUSTIINSU]
						,[RECIENACIDO],[NOPOSPROD],[IDAGEPROGQX],[IDCITA])
			SELECT 
					@IDHCFARMAPC
					,null
					,null
					,@identificacion
					,@ingreso
					,@CentroAtencion
					,@UnidadFuncional
					,@CodigoProfesional
					,PRO.CODPRODUC
					,A.DOSIS
					,iif(A.UNIDAD = 0, null,A.UNIDAD)
					,null
					,null
					,@fechaproceso
					,null
					,'Dosis Unica'
					,null
					,null
					,A.CANTIDAD  AS CantidadSolicitada
					,0 AS CantidadEntregada 
					,A.CANTIDAD  AS CantidadPendiente
					,1 --pendiente
					,PRO.TIPPRODUC 
					,null
					,null
					,PRO.NOPOSPROD
					,NULL
					,@IdCita 
					FROM 
		   			@TablaListaProductos A inner join dbo.IHLISTPRO PRO  with(nolock) ON PRO.CODPRODUC=A.CODPRODUC 

			
			--Insertamos en la tabla de kardex
			declare @NombreUsuario as varchar(200) = (select rtrim(ltrim(CODUSUARI)) + ' - ' + rtrim(ltrim(NOMUSUARI)) as Usuario  from SEGusuaru where CODUSUARI = @user)
			declare @Contador int = 0, @cantidadRegistro Int
			declare @Tablaproducto as table(id integer identity(1,1),codigoProducto varchar(100),CantidadProducto int)
			
     
			insert into @Tablaproducto 
			select PRO.CODPRODUC ,A.CANTIDAD FROM @TablaListaProductos A inner join dbo.IHLISTPRO PRO  with(nolock) ON PRO.CODPRODUC=A.CODPRODUC 

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
				   ,@CodigoProfesional
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
				   ,'Solicitud desde confirmación de citas de Apoyo Diagnóstico - (Agendamiento) - Usuario: ' + @NombreUsuario + ' Fecha: ' + convert(varchar(20),[Common].[GETDATE](),22)
				   ,NULL
				   ,NULL
				   ,NULL
				   ,NULL)	

			END

	END 

	 update AGASICITA set CONFIRMCITA = 1, USUCONFIRM = @user, FECHACONFIRM = [Common].[GETDATE](), NUMINGRESCONFIRM = IIF(@Ingreso = '',null,@Ingreso )   where CODAUTONU = @IdCita 

	select '0' as CodeMessage, 'Se guardo correctamente'  Message 

	end try
	begin catch
		select '999' as CodeMessage, ERROR_MESSAGE() + ', Linea: ' + cast(ERROR_LINE() as varchar(20)) Message
	end catch
END