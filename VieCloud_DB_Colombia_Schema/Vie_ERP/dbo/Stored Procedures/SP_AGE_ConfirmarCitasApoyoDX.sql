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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Confirma citas de apoyo diagnóstico (imágenes y otros procedimientos) generando las órdenes de medicamentos o insumos asociadas. A partir del identificador de la cita y un XML con la lista de productos (medicamentos de quimioterapia u otros), valida que la cita esté en estado asignado, verifica que el ingreso hospitalario esté abierto, consulta los parámetros de centro de costo y almacén de la unidad funcional, y luego registra la cabecera y el detalle de la orden farmacéutica (tablas HCFARMEPC y HCFARMEPD), actualizando además el kardex de medicamentos del paciente. Integra información del agendamiento (AGASICITA), datos del paciente (INPACIENT), sala de atención (AGENSALAC), grupo de cuidado del contrato (CareGroup) y administradora de salud (HealthAdministrator) para dejar trazabilidad completa de la dispensación de insumos en citas de apoyo diagnóstico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AGE_ConfirmarCitasApoyoDX';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AGE_ConfirmarCitasApoyoDX';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Confirma una cita de apoyo diagnóstico (imágenes/procedimientos) registrando los productos/medicamentos asociados en farmacia y kardex, y marcando la cita como confirmada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ConfirmarCitasApoyoDX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La cita debe existir en AGASICITA y estar en estado asignada (CODESTCIT = 0); de lo contrario se devuelve mensaje de error.; Si se proporciona un ingreso, éste debe existir en ADINGRESO con estado '''' o ''P'' (abierto).; Debe existir parametrización de centro de costo y bodega/almacén en HCUNITHIS para el centro de atención y unidad funcional, con CODTIPHIS distinto de ''ENF''.; El XML de productos debe seguir la estructura /ListaProductos/MedQuimio con los nodos esperados.; Cada producto en el XML debe existir en el catálogo IHLISTPRO.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ConfirmarCitasApoyoDX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Toda cita confirmada queda con CONFIRMCITA=1 y registro de usuario y fecha de confirmación.; El ingreso asociado a la confirmación se guarda como NULL cuando se recibe vacío.; Las órdenes de farmacia generadas siempre nacen en estado pendiente (ORDESTADO=1) y tipo tratamiento nuevo (ORDTRANUE=1).; Los detalles de farmacia se crean con cantidad entregada en 0 y cantidad pendiente igual a la solicitada.; Los movimientos de kardex generados son siempre de tipo entrada (TIPREGIST=1) con TIPORIREG=0.; Los productos insertados en farmacia y kardex se restringen a los que existan en IHLISTPRO (inner join).; Cualquier excepción es capturada y devuelta como CodeMessage=''999'' con mensaje y línea de error.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ConfirmarCitasApoyoDX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cita de apoyo diagnóstico; Confirmación de cita; Agendamiento; Ingreso del paciente; Centro de atención; Unidad funcional; Centro de costo; Almacén/Bodega de farmacia; Orden de farmacia (cabecera y detalle); Kardex de paciente; Medicamentos/Insumos; Dosis única; Profesional de salud; Administradora de salud (EPS); Grupo de cuidado (CareGroup)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ConfirmarCitasApoyoDX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] dbo.HCFARMEPC: Cuando existe al menos un producto en la lista del XML, se inserta una cabecera de orden de farmacia con estado 1 (pendiente), tratamiento nuevo (ORDTRANUE=1), asociada a la cita y al paciente.; [INSERT] dbo.HCFARMEPD: Por cada producto del XML cruzado con IHLISTPRO, se inserta un detalle de farmacia con tipo de formulación ''Dosis Unica'', estado 1 (pendiente), cantidad solicitada=cantidad XML, cantidad entregada=0 y cantidad pendiente=cantidad XML.; [INSERT] dbo.HCKARDPAC: Por cada producto del XML se inserta un movimiento de kardex tipo 1 (entrada) con descripción ''Solicitud desde confirmación de citas de Apoyo Diagnóstico - (Agendamiento)'' incluyendo usuario y fecha.; [UPDATE] dbo.AGASICITA: Al final del flujo exitoso, marca la cita con CONFIRMCITA=1, registra usuario y fecha de confirmación, y NUMINGRESCONFIRM con el ingreso (null si vino vacío).; [RETURN_RESULT] : Devuelve resultset con CodeMessage=''0'' y mensaje ''Se guardo correctamente'' al finalizar; o CodeMessage=''999'' con descripción de error en validaciones o catch.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ConfirmarCitasApoyoDX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Estado de la cita distinto de 0 (no asignada) → Devuelve mensaje ''999'' indicando que no se puede confirmar (no hace return, continúa el flujo); si Se proporciona un ingreso y no existe abierto en ADINGRESO (estado '''' o ''P'') → Retorna mensaje ''999'' indicando que el ingreso no se encuentra abierto y termina la ejecución; si No se encuentra centro de costo o almacén en HCUNITHIS para la unidad funcional → Retorna mensaje ''999'' con el parámetro faltante y termina la ejecución; si Existe al menos un producto en la lista del XML → Inserta cabecera HCFARMEPC, detalles HCFARMEPD y movimientos en HCKARDPAC por cada producto else Omite registros de farmacia y kardex y solo confirma la cita', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ConfirmarCitasApoyoDX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ConfirmarCitasApoyoDX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGASICITA; dbo.INPACIENT; dbo.AGENSALAC; Contract.CareGroup; Contract.HealthAdministrator; dbo.ADINGRESO; dbo.HCUNITHIS; dbo.IHLISTPRO; dbo.SEGusuaru', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ConfirmarCitasApoyoDX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ConfirmarCitasApoyoDX';
-- GO
