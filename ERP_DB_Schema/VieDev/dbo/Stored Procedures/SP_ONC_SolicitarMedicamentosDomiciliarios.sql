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