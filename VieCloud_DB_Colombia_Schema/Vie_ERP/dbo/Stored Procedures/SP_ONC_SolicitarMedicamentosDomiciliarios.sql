-- =============================================
-- Author:		Juan David Patiño Cabrera
-- Create date: 28-02-2020
-- Description:	Sp Solicita los medicamentos domiciliarios a FArmacia
-- =============================================
CREATE PROCEDURE [dbo].[SP_ONC_SolicitarMedicamentosDomiciliarios]
	@IDHCORDQUIMIO as integer,
	@CICLO AS Integer,
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
	declare @Profesional as varchar(50)
	declare @Ingreso as varchar(100)
	declare @CentroCosto as varchar(20)
	declare @Almacen as varchar(20)
	declare @IDHCFARMAPC as integer
	declare @IDHCORDPRON as  integer
	declare @CODPROSAL as varchar(20)

	declare @TablaListaProductos table (
				RowId Int Identity(1,1) Primary Key,
				CODPRODUC varchar(100), 
				DESPRODUC varchar(200),
				DOSIS numeric(18,2),
				UNIDAD varchar(20),
				VIA varchar(20),
				CANTIDAD int
				)

	BEGIN TRY 

	SELECT		@Identificacion  = A.IPCODPACI, 
				@CODENTIDAD = B.CODENTIDA, 
				@CentroAtencion = C.CODCENATE ,
				@UnidadFuncional = C.UFUCODIGO, 
				@CareGroupId = CG.Id , 
				@HealthAdministratorId = HA.Id,
				@IDHCORDPRON = c.IDHCORDPRON,
				@CODPROSAL = c.CODPROSAL 
	FROM	ehr.HCORDQUIMIO A 
				inner join INPACIENT AS B with(nolock) ON A.IPCODPACI = B.IPCODPACI
				INNER JOIN ehr.HCORDCICLOS AS c with(nolock) ON c.IDHCORDQUIMIO = a.ID and c.CICLO = @CICLO
				INNER JOIN Contract.CareGroup as CG on CG.Id = B.GENCAREGROUP 
				left JOIN  Contract.HealthAdministrator HA on HA.ID = B.GENCONENTITY 
	WHERE	A.ID = @IDHCORDQUIMIO

	insert @TablaListaProductos
	select 
		t.x.value('CodigoProducto[1]','varchar(100)'),
		t.x.value('NombreProducto[1]','varchar(200)'),
		convert(numeric(18,2),t.x.value('Dosis[1]','numeric(18,2)')),
		t.x.value('CODUNIMED[1]','varchar(20)'),
		t.x.value('CODVIAADM[1]','varchar(20)'),
		t.x.value('Cantidad[1]','int')
	from @xml.nodes('/ListaProductos/MedicamentosCasa') t(x)
	
	--select * from @TablaListaProductos
	   	 			
	/*SELECT TOP 1 @Ingreso = NUMINGRES FROM ADINGRESO WHERE IPCODPACI = @Identificacion AND TRATAESPECIA = 3 AND IESTADOIN IN ('','P')
	print @Identificacion
	print 'sadasd' + @ingreso*/
	
	SELECT @Ingreso  = dbo.IngresoOncologico(@Identificacion)
	
	select top 1 @centroCosto = CODCENCOS, @Almacen =CODBODEGA  from HCUNITHIS with(nolock) where CODCENATE =@CentroAtencion AND UFUCODIGO =@UnidadFuncional AND CODTIPHIS <> 'ENF'
	
	if @centroCosto is null begin
		select '999' as CodeMessage, 'No se encontraron parametros de centro costo para la unidad funcional: ' + @UnidadFuncional as Message
		return
	end
	if @Almacen is null begin
		select '999' as CodeMessage, 'No se encontraron parametros de Almacen para la unidad funcional: ' + @UnidadFuncional as Message
		return
	end

	--insertamos cabecera de farmacia
	INSERT INTO [dbo].[HCFARMEPC]
           ([IDETIPHIS],[NUMEFOLIO],[FECHAORDE],[CODPROSAL],[IPCODPACI],[NUMINGRES],[CODCENATE],[UFUCODIGO],[CODCONCEP],[CODCONCES],[ORDESTADO],[CODBODEGA],[CODCENCOS],[ORDTRANUE]
           ,[JUSANULAC],[MEDICAMENTOVALIDADO],[IDAGEPROGQX],[TIPOSOLQX],[ORDENQUIMIO],[IDCITA],[IDHCORDPRON])
	VALUES
    (null
    ,null
    ,@FechaProceso
    ,@CODPROSAL
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
	,NULL,1,1,null,
	@IDHCORDPRON
	) 

	--capturamos ID generado
	SET @IDHCFARMAPC  = SCOPE_IDENTITY()

	--insertamos en tabla detalle de farmacia
	INSERT INTO [dbo].[HCFARMEPD]([CODCONCEC],[IDETIPHIS],[NUMEFOLIO],[IPCODPACI],[NUMINGRES],[CODCENATE],[UFUCODIGO],[CODPROSAL],[CODPRODUC],[DOSISPROD],[CODUNIMED]
				,[FRECUENCI],[UNIFRECUE],[FECINIDOS],[TIPFORMED],[DURACIDOS],[VALDURFIJ],[UNIDURFIJ],[CANPEDPRO],[CANENTPRO],[CANPENPRO],[PROESTADO],[TIPOREGIS],[JUSTIINSU]
				,[RECIENACIDO],[NOPOSPROD],[IDAGEPROGQX],[IDCITA],[EXTRAMURAL])
	SELECT 
			@IDHCFARMAPC
			,null
			,null
			,@identificacion
			,@ingreso
			,@CentroAtencion
			,@UnidadFuncional
			,@CODPROSAL
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
			,case PRO.TIPPRODUC when 1 then 1 else 2 END AS Tipo
			,null
			,null
			,PRO.NOPOSPROD
			,NULL
			,null 
			,1
			FROM 
		   	@TablaListaProductos A inner join dbo.IHLISTPRO PRO  with(nolock) ON PRO.CODPRODUC=A.CODPRODUC 

     update ehr.HCORMEDICAMESQUEMA set SOLFARMACIAREALIZADA = 1, IDHCFARMEPC = @IDHCFARMAPC  
		 FROM ehr.HCORMEDICAMESQUEMA M 
		 INNER JOIN @TablaListaProductos A on m.CODPRODUC = a.CODPRODUC 
	 where IDHCORDQUIMIO = @IDHCORDQUIMIO and CICLO = @CICLO 

	select '0' as CodeMessage, 'Se guardo correctamente'  Message 

	end try
	begin catch
		select '999' as CodeMessage, ERROR_MESSAGE() + ', Linea: ' + cast(ERROR_LINE() as varchar(20)) Message
	end catch
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera una solicitud de medicamentos domiciliarios (para casa) a la farmacia, a partir de un ciclo específico de quimioterapia oncológica. Toma el identificador de la orden de quimioterapia y el número de ciclo, lee los datos del paciente (cédula, entidad pagadora/EPS, grupo de atención, centro de atención y unidad funcional) cruzando las órdenes de quimioterapia con la tabla de pacientes y los ciclos ordenados. Parsea un XML de entrada con la lista de medicamentos solicitados (código, nombre, dosis, unidad, vía de administración y cantidad) y crea el encabezado y el detalle de la orden de farmacia en las tablas HCFARMEPC y HCFARMEPD, vinculándola al ingreso oncológico activo del paciente y al centro de costo y almacén configurados para la unidad funcional. Finalmente, marca los medicamentos del esquema de quimioterapia como ''solicitud a farmacia realizada'', cerrando el flujo de dispensación domiciliaria en el módulo de oncología.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ONC_SolicitarMedicamentosDomiciliarios';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ONC_SolicitarMedicamentosDomiciliarios';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera una solicitud de medicamentos domiciliarios a farmacia (cabecera y detalle) a partir de un ciclo de quimioterapia y un XML con los productos, marcando el esquema de medicamentos como ya solicitado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONC_SolicitarMedicamentosDomiciliarios';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir la orden de quimioterapia y el ciclo indicado en ehr.HCORDQUIMIO/ehr.HCORDCICLOS para obtener paciente, profesional y orden de pronóstico.; El paciente debe tener un ingreso oncológico vigente resoluble por dbo.IngresoOncologico.; La unidad funcional/centro de atención debe estar parametrizada en HCUNITHIS con centro de costo y bodega y tipo de historia distinto a ''ENF''.; Los productos enviados en el XML deben existir en dbo.IHLISTPRO (join inner).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONC_SolicitarMedicamentosDomiciliarios';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Toda solicitud generada queda marcada como tratamiento nuevo (ORDTRANUE=1), orden de quimioterapia (ORDENQUIMIO=1) y tipo solicitud quirúrgica/quimio=1.; Los detalles se crean siempre como pendientes (PROESTADO=1) con cantidad entregada en 0 y cantidad pendiente igual a la cantidad solicitada.; Los medicamentos solicitados son extramurales (EXTRAMURAL=1) y de dosis única.; El esquema de medicamentos del ciclo no se marca como solicitado a farmacia salvo que se haya creado exitosamente la cabecera HCFARMEPC asociada.; Errores en cualquier paso son capturados y devueltos como resultado con código ''999'' sin propagar excepción.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONC_SolicitarMedicamentosDomiciliarios';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Quimioterapia; Ciclo de quimioterapia; Medicamentos domiciliarios; Solicitud a farmacia; Ingreso oncológico; Unidad funcional; Centro de costo; Bodega/Almacén; Esquema de medicamentos; Dosis única; Producto POS/NoPOS; Paciente; Profesional de salud', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONC_SolicitarMedicamentosDomiciliarios';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] dbo.HCFARMEPC: Si se obtienen centro de costo y almacén válidos para la unidad funcional, se inserta una cabecera de orden de farmacia con ORDESTADO=1, ORDTRANUE=1 (tratamiento nuevo), ORDENQUIMIO=1 y TIPOSOLQX=1, asociada al paciente, ingreso oncológico, profesional y orden de pronóstico del ciclo.; [INSERT] dbo.HCFARMEPD: Por cada producto del XML que exista en IHLISTPRO se inserta un detalle con TIPFORMED=''Dosis Unica'', PROESTADO=1 (pendiente), CANPEDPRO=CANPENPRO=Cantidad, CANENTPRO=0, EXTRAMURAL=1 y TIPOREGIS=1 si TIPPRODUC=1 o 2 en otro caso; UNIDAD=0 se guarda como NULL.; [UPDATE] ehr.HCORMEDICAMESQUEMA: Para los medicamentos del esquema cuyo CODPRODUC esté en el XML y correspondan al ciclo y orden indicados, se marca SOLFARMACIAREALIZADA=1 y se enlaza IDHCFARMEPC con la cabecera recién creada.; [RETURN_RESULT] (resultset): Si HCUNITHIS no devuelve centro de costo o almacén para la unidad funcional, se retorna CodeMessage=''999'' con mensaje de parámetro faltante y se aborta el flujo.; [RETURN_RESULT] (resultset): Si todo el proceso termina sin error se retorna CodeMessage=''0'' y mensaje ''Se guardo correctamente''; ante excepción se retorna ''999'' con ERROR_MESSAGE y línea.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONC_SolicitarMedicamentosDomiciliarios';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @centroCosto IS NULL tras consultar HCUNITHIS → Retorna mensaje de error ''999'' indicando falta de parámetros de centro de costo y termina sin insertar. else Continúa con la validación de almacén.; si @Almacen IS NULL tras consultar HCUNITHIS → Retorna mensaje de error ''999'' indicando falta de parámetros de almacén y termina sin insertar. else Procede a insertar cabecera y detalle de farmacia.; si PRO.TIPPRODUC = 1 en el catálogo de productos → El detalle se inserta con TIPOREGIS=1. else El detalle se inserta con TIPOREGIS=2.; si A.UNIDAD = 0 en el XML → Se persiste CODUNIMED como NULL. else Se persiste el valor de UNIDAD recibido.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONC_SolicitarMedicamentosDomiciliarios';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.IngresoOncologico; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONC_SolicitarMedicamentosDomiciliarios';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'ehr.HCORDQUIMIO; ehr.HCORDCICLOS; dbo.INPACIENT; Contract.CareGroup; Contract.HealthAdministrator; dbo.HCUNITHIS; dbo.IHLISTPRO; ehr.HCORMEDICAMESQUEMA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONC_SolicitarMedicamentosDomiciliarios';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONC_SolicitarMedicamentosDomiciliarios';
-- GO
