-- =============================================
-- Author:		Rafael Patiño
-- Create date: 28-02-2020
-- Description:	Sp que confirma los medicamentos de quimio
-- =============================================
CREATE PROCEDURE [dbo].[SP_AGE_ConfirmarCitasQuimioterapia]
	@IdCita int,
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
	declare @EstadoCita as varchar(1)

	declare @FechaCita as datetime
	declare @IdOrdenQuimio as integer
	declare @IdCiclo as integer
	
	declare @ActivateMixingStation as bit = 0

	declare @TablaListaProductos table (
				RowId Int Identity(1,1) Primary Key,
				TypePrescription int, --1:Estandar 2:Frecuencia
				HoraFrecuencia datetime, 
				HoraFrecuencia2 varchar(40),
				CODPRODUC varchar(100), 
				DESPRODUC varchar(200),
				DOSIS numeric(18,2),
				UNIDAD varchar(20),
				VIA varchar(20),
				CANTIDAD int,
				Tipo int,
				Administracion varchar(max),
				InstruccionesAdministracion varchar(max),
				SENDTO tinyint,
				CodigoAgrupador UNIQUEIDENTIFIER,
				ActivateMixingStation bit,
				mixingStationID integer,
				UnitDoseTypeID integer,
				IdOrigin integer,
				Origin varchar(20),
				CenterAttentionCode varchar(20),
				FunctionalUnitCode varchar(20)
				)

	BEGIN TRY 

	SELECT  @Identificacion  = A.IPCODPACI,@CODENTIDAD = B.CODENTIDA, @CentroAtencion = A.CODCENATE , @UnidadFuncional = Sala.UFUCODIGO, 
			@CareGroupId = CG.Id , @HealthAdministratorId = HA.Id, @FechaCita = A.FECHORAIN, @IdCiclo = A.IDHCORDCICLOSD  , @EstadoCita = A.CODESTCIT   
	FROM	dbo.AGASICITA  AS A  INNER JOIN
			dbo.INPACIENT AS B  ON A.IPCODPACI = B.IPCODPACI INNER JOIN
			dbo.AGENSALAC AS Sala  ON Sala.CODCONCEC = A.IDSALA left JOIN
			Contract.CareGroup as CG on CG.Id = B.GENCAREGROUP left JOIN 
			Contract.HealthAdministrator HA on HA.ID = B.GENCONENTITY 
	WHERE	A.CODAUTONU = @IdCita  

	if @EstadoCita <> 0 begin
	      select '999' as CodeMessage, 'La cita esta en estado diferente a asignada, no se puede confirmar cita' Message
	end

	insert @TablaListaProductos
	select 
		t.x.value('TypePrescription[1]','int'),
		t.x.value('HoraFrecuencia[1]','datetime'),
		t.x.value('HoraFrecuencia[1]','varchar(40)'),
		t.x.value('CodigoProducto[1]','varchar(100)'),
		t.x.value('NombreProducto[1]','varchar(200)'),
		convert(numeric(18,2),t.x.value('Dosis[1]','numeric(18,2)')),
		t.x.value('CODUNIMED[1]','varchar(20)'),
		t.x.value('CODVIAADM[1]','varchar(20)'),
		t.x.value('Cantidad[1]','int'),
		t.x.value('Tipo[1]','int'),
		t.x.value('Administracion[1]','varchar(MAX)'),
		t.x.value('InstruccionesAdministracion[1]','varchar(MAX)'),
		t.x.value('SENDTO[1]', 'tinyint'),
		t.x.value('CodigoAgrupador[1]', 'varchar(40)'),
		t.x.value('ActivateMixingStation[1]', 'bit'),
		t.x.value('mixingStationID[1]', 'integer'),
		t.x.value('UnitDoseTypeId[1]', 'integer'),
		t.x.value('IdOrigin[1]', 'integer'),
		t.x.value('Origin[1]', 'varchar(20)'),
		t.x.value('CenterAttentionCode[1]', 'varchar(20)'),
		t.x.value('FunctionalUnitCode[1]', 'varchar(20)')
	from @xml.nodes('/ListaProductos/MedQuimio') t(x)
	
	--select *,@FechaCita as fechacita,DATEPART(HOUR, HoraFrecuencia) as DATEPARTHORA, datediff(HOUR, HoraFrecuencia,@FechaCita) diff, convert(datetime,HoraFrecuencia) from @TablaListaProductos
	--select * from @TablaListaProductos
	
	if exists(select 1 from @TablaListaProductos where ActivateMixingStation =1 )
	begin 
		set	 @ActivateMixingStation = 1
	end

	
	SELECT TOP 1 @Ingreso = NUMINGRES FROM ADINGRESO WHERE IPCODPACI = @Identificacion AND TRATAESPECIA = 3 AND IESTADOIN IN ('','P')
	print @Identificacion
	--print 'sadasd' + @ingreso

	--si el paciente esta hospitalizado en el centro de atencion de la cita (esta en cama actualmente) tomamos ingreso de hospitalizacion
	IF exists( SELECT RTRIM(I.NUMINGRES)  FROM dbo.ADINGRESO I  INNER JOIN 
				dbo.CHCAMASHO C  ON C.CODICAMAS = I.CODCAMACT AND C.ESTADCAMA = 2 INNER JOIN 
				dbo.CHREGESTA R  ON R.NUMINGRES = I.NUMINGRES AND R.CODICAMAS = C.CODICAMAS AND R.REGESTADO = 1 
				WHERE I.IPCODPACI = @Identificacion AND TIPOINGRE = 2 AND C.CODCENATE = @CentroAtencion) BEGIN
		
		--tomo ingreso de hospitalizacion
		SELECT @Ingreso = RTRIM(I.NUMINGRES), @UnidadFuncional = RTRIM(C.UFUCODIGO)   FROM dbo.ADINGRESO I  INNER JOIN 
		dbo.CHCAMASHO C  ON C.CODICAMAS = I.CODCAMACT AND C.ESTADCAMA = 2 INNER JOIN 
		dbo.CHREGESTA R  ON R.NUMINGRES = I.NUMINGRES AND R.CODICAMAS = C.CODICAMAS AND R.REGESTADO = 1 
		WHERE I.IPCODPACI = @Identificacion AND TIPOINGRE = 2 AND C.CODCENATE = @CentroAtencion

	END ELSE BEGIN

		--Si el paciente tiene un ingreso hospitalario en estado abierto o parcialmente facturado marcado como tipo oncológico - quimioterapia y no tiene cama activa con ese ingreso se debe realizar:
		IF exists(	SELECT RTRIM(I.NUMINGRES)  FROM dbo.ADINGRESO I  INNER JOIN 
				dbo.CHCAMASHO C  ON C.CODICAMAS = I.CODCAMACT INNER JOIN 
				dbo.CHREGESTA R  ON R.NUMINGRES = I.NUMINGRES AND R.CODICAMAS = C.CODICAMAS AND R.REGESTADO = 2 
				WHERE I.IPCODPACI = @Identificacion  AND  I.TIPOINGRE = 2 AND I.IESTADOIN IN (' ','P') AND I.TRATAESPECIA = 3) BEGIN
					
									   					 
					declare @ingresoAmbulatorio as varchar(20)
					declare @ingresoHospitalario as varchar(20)

					--Leemos el ingreso hospitalario en estado abierto o parcialmente facturado marcado como tipo oncológico - quimioterapia y no tiene cama activa
					SELECT @ingresoHospitalario = RTRIM(I.NUMINGRES)  FROM dbo.ADINGRESO I  INNER JOIN 
					dbo.CHCAMASHO C  ON C.CODICAMAS = I.CODCAMACT INNER JOIN 
					dbo.CHREGESTA R  ON R.NUMINGRES = I.NUMINGRES AND R.CODICAMAS = C.CODICAMAS AND R.REGESTADO = 2 
					WHERE I.IPCODPACI = @Identificacion  AND  I.TIPOINGRE = 2 AND I.IESTADOIN IN (' ','P') AND I.TRATAESPECIA = 3

					--Si tiene por lo menos un ingreso ambulatorio en estado abierto o parcialmente facturado que ha sido utilizado para la dispensación de medicamentos de quimio o para la administración de medicamentos de quimio tomamos ingreso
					SELECT top 1 @ingresoAmbulatorio=  RTRIM(I.NUMINGRES) FROM dbo.ADINGRESO I  inner join 
					HCFARMEPC F on F.NUMINGRES = I.NUMINGRES AND F.ORDESTADO = 2 AND F.ORDENQUIMIO = 1 inner join 
					HCHOJAMED A on A.NUMINGRES = I.NUMINGRES AND A.IDHCORDQUIMIO IS NOT NULL
					WHERE I.IPCODPACI = @Identificacion  AND  I.TIPOINGRE = 1 AND I.IESTADOIN IN (' ','P') Order by F.FECHAORDE DESC

					if @ingresoAmbulatorio <> '' begin
							--se debe desmarcar como tipo oncológico - quimioterapia el ingreso hospitalario
							Update ADINGRESO set TRATAESPECIA = NULL where  NUMINGRES = @ingresoHospitalario 	
							--y marcar el ingreso ambulatorio como tipo oncológico - quimioterapia
							Update ADINGRESO set TRATAESPECIA = 3 where NUMINGRES = @ingresoAmbulatorio 
							set @Ingreso = @ingresoAmbulatorio
					end else begin
							--Si no tiene un ingreso ambulatorio en estado abierto o parcialmente facturado que haya sido utilizado para la dispensación de medicamentos o aplicacion de medicamentos quimio
							--tomamos el ultimo ingreso de tipo oncologico - quimioterapia abierto o parcialmente facturado
							SELECT TOP 1 @Ingreso = NUMINGRES FROM ADINGRESO WHERE IPCODPACI = @Identificacion AND TRATAESPECIA = 3 AND IESTADOIN IN ('','P') order by IFECHAING DESC											
					end
									   					

		END ELSE BEGIN
			--tomamos el ultimo ingreso de tipo oncologico - quimioterapia abierto o parcialmente facturado
			SELECT TOP 1 @Ingreso = NUMINGRES FROM ADINGRESO WHERE IPCODPACI = @Identificacion AND TRATAESPECIA = 3 AND IESTADOIN IN ('','P') order by IFECHAING DESC

		END   

	END

	if @Ingreso = '' begin
		select '999' as CodeMessage, 'El paciente no tiene un ingreso activo de tipo oncológico - quimioterapia' as Message
		return
	end

	

	select @IdOrdenQuimio = O.ID , @Profesional = O.CODPROSAL from EHR.HCORDCICLOSD C inner join EHR.HCORDQUIMIO O on o.ID = C.IDHCORDQUIMIO where C.ID = @IdCiclo 

	select top 1 @centroCosto = CODCENCOS, @Almacen =CODBODEGA  from HCUNITHIS  where CODCENATE =@CentroAtencion AND UFUCODIGO =@UnidadFuncional AND CODTIPHIS <> 'ENF'
	
	if @centroCosto is null begin
		select '999' as CodeMessage, 'No se encontraron parametros de centro costo para la unidad funcional: ' + @UnidadFuncional as Message
		return
	end
	if @Almacen is null begin
		select '999' as CodeMessage, 'No se encontraron parametros de Almacen para la unidad funcional: ' + @UnidadFuncional as Message
		return
	end

	DECLARE @DrugConfirmationStatus AS TINYINT
 IF EXISTS (SELECT ID FROM MixingStation.MixingStationSettingAttentionCenter WHERE CODCENATE = @CentroAtencion AND UFUCODIGO = @UnidadFuncional)
 SET @DrugConfirmationStatus = 1 ELSE SET @DrugConfirmationStatus = NULL

	--insertamos cabecera de farmacia
	INSERT INTO [dbo].[HCFARMEPC]
           ([IDETIPHIS],[NUMEFOLIO],[FECHAORDE],[CODPROSAL],[IPCODPACI],[NUMINGRES],[CODCENATE],[UFUCODIGO],[CODCONCEP],[CODCONCES],[ORDESTADO],[CODBODEGA],[CODCENCOS],[ORDTRANUE]
           ,[JUSANULAC],[MEDICAMENTOVALIDADO],[IDAGEPROGQX],[TIPOSOLQX],[ORDENQUIMIO],[IDCITA],[ConfirmationStatus],[USUAREGISTRO])
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
	,NULL,1,1,@IdCita,@DrugConfirmationStatus, @user) 

	--capturamos ID generado
	SET @IDHCFARMAPC  = SCOPE_IDENTITY()

	--insertamos en tabla detalle de farmacia
	INSERT INTO [dbo].[HCFARMEPD]([CODCONCEC],[IDETIPHIS],[NUMEFOLIO],[IPCODPACI],[NUMINGRES],[CODCENATE],[UFUCODIGO],[CODPROSAL],[CODPRODUC],[DOSISPROD],[CODUNIMED]
				,[FRECUENCI],[UNIFRECUE],[FECINIDOS],[TIPFORMED],[DURACIDOS],[VALDURFIJ],[UNIDURFIJ],[CANPEDPRO],[CANENTPRO],[CANPENPRO],[PROESTADO],[TIPOREGIS],[JUSTIINSU]
				,[RECIENACIDO],[NOPOSPROD],[IDAGEPROGQX],[IDCITA],[SENDTO],CodeSusceptibleMixingStation,VIEPROCESSED,SourceTable,IdSourceTable)
	SELECT 
			@IDHCFARMAPC
			,null
			,null
			,@identificacion
			,@ingreso
			,@CentroAtencion
			,@UnidadFuncional
			,@Profesional
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
			,case A.Tipo when 1 then 1 else 2 END AS Tipo --Tipo 1 = Medicamentos. 2 = Diluyentes. 3 = Insumos Comentario realizado en el formulario de confirmación de citas
			,null
			,null
			,PRO.NOPOSPROD
			,NULL
			,@IdCita
			,SENDTO
			,A.CodigoAgrupador 
			,0 --vieprocessed
			,A.Origin 
			,A.IdOrigin
			FROM 
		   	@TablaListaProductos A inner join dbo.IHLISTPRO PRO   ON PRO.CODPRODUC=A.CODPRODUC 
			WHERE A.Tipo <> 2 --se excluyen los diluyentes, no se deben registrar en HCFARMEPD

    --Insertamos en HCFISIPRO para poder realizar los cálculos de los medicamentos
	if NOT Exists (select IPCODPACI from dbo.HCFISIPRO A
				   inner join @TablaListaProductos B ON A.CODPRODUC = B.CODPRODUC
				   where A.IPCODPACI = @identificacion and A.NUMINGRES = @ingreso and A.CODCENATE = @CentroAtencion 
				   And A.UFUCODIGO = @UnidadFuncional
												   ) Begin
		INSERT INTO [dbo].[HCFISIPRO]
		   ([IPCODPACI]
		   ,[NUMINGRES]
		   ,[CODCENATE]
		   ,[UFUCODIGO]
		   ,[CODPRODUC]
		   ,[TIPPRODUC]
		   ,[CANACTPRO]
		   ,[CANPEDPRO]
		   ,[CANPENPRO]
		   ,[TOTPROUNI]
		   ,[DOSPROACU]
		   ,[TOTHORACU]
		   ,[CODUNIMED]
		   ,[INDAUDFOR])
		SELECT
		   @identificacion
		   ,@ingreso
		   ,@CentroAtencion
		   ,@UnidadFuncional
		   ,PRO.CODPRODUC
		   ,case PRO.TIPPRODUC when 1 then 1 else 2 END AS Tipo
		   ,0  AS CantidadActual
		   ,A.CANTIDAD  AS CantidadSolicitada
		   ,A.CANTIDAD  AS CantidadPendiente
		   ,NULL
		   ,NULL
		   ,NULL
		   ,NULL
		   ,0
		FROM 
		   	@TablaListaProductos A inner join dbo.IHLISTPRO PRO   ON PRO.CODPRODUC=A.CODPRODUC 

    END

    --como se puede confirmar varias veces, debemos eliminar los registro de HCHOJAMED que existan para el IDCITA y IDHCORDCICLOSD
	DELETE FROM [dbo].[HCHOJAMED] where IDCITA = @IdCita AND IDHCORDCICLOSD = @IdCiclo AND MEDESTADO = 1

	

	--insertamos en hoja de medicamentos los productos como medicamentos
	INSERT INTO [dbo].[HCHOJAMED]([IPCODPACI],[NUMINGRES],[CODCENATE],[UFUCODIGO],[CODPROSAL],[CODPRODUC],[FECINITRA],[FECPROAPL],[FECAPLMED],[FECREGSIS],[CODVIAADM],[DOSISPROD]
           ,[CODUNIMED],[FRECUENCI],[UNIFRECUE],[DURACIDOS],[VALDURFIJ],[UNIDURFIJ],[FECFINDOS],[MEDESTADO],[CABESTADO],[INDAPLMED],[MOTSUSMED],[CODUSUSUS],[CANDESCON],[FORMUMANU]
           ,[DESADMINI],[CODTIPEST],[CONSECPRESCRA],[MEDICACUSTODIA],[IDAUTORIZACION],[IDHCORDQUIMIO],[IDHCORDCICLOSD],[IDCITA])
   select 
           @Identificacion 
           ,@Ingreso 
           ,@CentroAtencion
           ,@UnidadFuncional 
           ,@Profesional 
           ,PRO.CODPRODUC
           ,@FechaCita 
           ,case TypePrescription when 1 then  @FechaCita when 2 then dateadd(HOUR,DATEPART(HOUR, HoraFrecuencia),@FechaCita) end --@FechaCita 
           ,NULL
           ,case TypePrescription when 1 then  @FechaCita when 2 then dateadd(HOUR,DATEPART(HOUR, HoraFrecuencia),@FechaCita) end --@FechaCita 
           ,A.VIA
           ,A.DOSIS
           ,A.UNIDAD
           ,null
           ,null
           ,'Dosis Unica'
           ,null
           ,null
           ,null
           ,1
           ,1
           ,InstruccionesAdministracion
           ,NULL
           ,NULL
           ,A.CANTIDAD
           ,0
           ,Administracion 
           ,null
           ,null
           ,null
           ,null
           ,@IdOrdenQuimio
           ,@IdCiclo  
           ,@IdCita 
	FROM @TablaListaProductos A inner join dbo.IHLISTPRO PRO   ON PRO.CODPRODUC=A.CODPRODUC
	 inner join dbo.INUNIMEDI U on A.UNIDAD = U.CODUNIMED 
	 inner join dbo.HCVIAADMI V on A.VIA = V.CODVIAADM
	WHERE A.Tipo = 1 --tipo medicamentos

	  --Insertamos en la tabla de kardex
	declare @IdConsecu Varchar(8) = '00000011'
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
           ,'Solicitud desde confirmación de citas de quimioterapia - (Agendamiento) - Usuario: ' + @NombreUsuario
           ,NULL
           ,NULL
           ,NULL
           ,NULL)

	END

	--insertamos en la tabla de central de mezclas si hay por lo menos un medicamento susceptible de central mezcla y que tenga por ende
	--codigoagrupador: EJ: EB49EEBB-AC21-40DA-B507-9361D05A299B
	if @ActivateMixingStation = 1 begin
		--print 'paso'
		--insertamos en productos susceptibles de mezclas
		INSERT INTO [MedicalHistory].[ProductSusceptibleMixingStation]
           ([CodeSusceptibleMixingStation]
           ,[Origin]
           ,[IdOrigin]
           ,[FullProductName]
           ,[ApplicationsNumber]
           ,[MainDrugCode]
           ,[CenterAttentionCode]
           ,[FunctionalUnitCode]
           ,[ProfessionalCode]
           ,[CreationDate])
		select 
			A.CodigoAgrupador --EB49EEBB-AC21-40DA-B507-9361D05A299B
			,A.Origin  --'HCORDMEDICAM'
			,A.IdOrigin -- ID HCORDMEDICAM
			,concat(A.CODPRODUC,' - ',A.DESPRODUC) --concateno
			,1 --cantidad 1 med. quimio
			,rtrim(ltrim(A.CODPRODUC)) --codigo producto principal
			,A.CenterAttentionCode --codigo cengtro atencion
			,A.FunctionalUnitCode  --unidad funcional
			,@Profesional --codigo profesional
			,COMMON.GETDATE() --fecha actual
		FROM @TablaListaProductos A 
			 inner join dbo.IHLISTPRO PRO   ON PRO.CODPRODUC=A.CODPRODUC
			 inner join dbo.INUNIMEDI U on A.UNIDAD = U.CODUNIMED 
			 inner join dbo.HCVIAADMI V on A.VIA = V.CODVIAADM 
	 	 WHERE A.Tipo = 1 AND A.CodigoAgrupador is not null  --solo Tipo medicamentos y que tenga agrupador :  EJ EB49EEBB-AC21-40DA-B507-9361D05A299B
		-- print 'paso 2'

		declare @TablaAgrupadores table (
			RowId Int Identity(1,1) Primary Key,
			CodigoAgrupadorFiltro UNIQUEIDENTIFIER,
			CodigoAgrupadorDosis UNIQUEIDENTIFIER
		)

		 --generamos codigo unico agrupador x paquetes - medicamento principal y vehiculo del medicamento quimio
		 INSERT INTO @TablaAgrupadores
		 select CodigoAgrupador, NEWID() FROM @TablaListaProductos 
		 WHERE CodigoAgrupador is not null 
		 group by CodigoAgrupador

		 Declare @CodigoAgrupadorFiltro uniqueidentifier 
		 declare @CodigoAgrupadorDosis uniqueidentifier
		 Declare @Rows Int, @RowId Int
		 Set @Rows = 1
		 Set @RowId = 1

		 WHILE @Rows > 0
		 BEGIN
			Select Top 1 @RowId = RowId 
					,@CodigoAgrupadorFiltro = CodigoAgrupadorFiltro 
					,@CodigoAgrupadorDosis = CodigoAgrupadorDosis 
			From @TablaAgrupadores Where RowId >= @RowId Order By RowId

			--si hubieron afectaciones
			Set @Rows = @@ROWCOUNT
			--si no hay registro o afectaciones del query anterior termino ciclo
			If @Rows = 0 
				Break

				
				--insertamos en farmacia dosis
				INSERT INTO [MedicalHistory].[PharmaDose]
				([IDHCFARMEPC]
				,[CodeSusceptibleMixingStation]
				,[ProductCode]
				,[MeasurementUnitCode]
				,[UnitDoseTypeId]
				,[Dose]
				,[GroupingCodeDose]
				,[DeliveryStatus]
				,[QuantityReceivable]
				,[AppliedDose]
				,[AppliedDateDose]
				,[MixingStationId])
			SELECT
				@IDHCFARMAPC  --ID generado
				,A.CodigoAgrupador --codigo agrupador
				,rtrim(ltrim(A.CODPRODUC)) --codigo producto
				,A.UNIDAD --unidad medidad
				,A.UnitDoseTypeID --id tipo dosis central mezclas
				,A.DOSIS --dosis del prodcuto a procesar
				,@CodigoAgrupadorDosis --nuevo ID agrupador
				,1 --estado 1
				,A.CANTIDAD --cantidad producto requerido 	 
				,0 --cantidad aplicada
				,NULL --dosis aplicada
				,A.mixingStationID  --id central de mezcla a procesar
			FROM @TablaListaProductos A 
				inner join dbo.IHLISTPRO PRO   ON PRO.CODPRODUC=A.CODPRODUC
				inner join dbo.INUNIMEDI U on A.UNIDAD = U.CODUNIMED 
				inner join dbo.HCVIAADMI V on A.VIA = V.CODVIAADM 
			WHERE  A.CodigoAgrupador = @CodigoAgrupadorFiltro AND A.Tipo <> 2 --se excluyen los diluyentes, no se deben registrar en PharmaDose

			SET @RowId += 1
		END --fin while
	 END --fin if si aplica central mezclas

	 update AGASICITA set CONFIRMQUIMIO = 1, USUCONFIRMQUIMIO = @user where CODAUTONU = @IdCita 

	select '0' as CodeMessage, @IDHCFARMAPC as 'ConsecutivoFarmacia','Se guardo correctamente' Message 

	end try
	begin catch
		select '999' as CodeMessage, ERROR_MESSAGE() + ', Linea: ' + cast(ERROR_LINE() as varchar(20)) Message
	end catch
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que confirma las citas de quimioterapia agendadas para un paciente, procesando y registrando los medicamentos oncológicos (quimio) asociados al ciclo de tratamiento. Recibe el identificador de la cita, el usuario que ejecuta la acción y un XML con la lista de productos/medicamentos a administrar, validando que la cita esté en estado ''asignada'' antes de proceder. Determina el ingreso correcto del paciente (ambulatorio oncológico u hospitalario con cama activa) consultando AGASICITA, INPACIENT y AGENSALAC, y resuelve el grupo de atención (CareGroup) y la entidad pagadora (HealthAdministrator/EPS) del paciente para el proceso de dispensación farmacéutica. Gestiona también el flujo hacia la estación de mezclas (MixingStation) si alguno de los medicamentos del ciclo lo requiere, siendo clave en el proceso de atención oncológica ambulatoria y hospitalaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AGE_ConfirmarCitasQuimioterapia';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AGE_ConfirmarCitasQuimioterapia';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Confirma una cita de quimioterapia generando la orden de farmacia (cabecera y detalle), la hoja de medicamentos, kardex e información de central de mezclas, y marca la cita como confirmada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ConfirmarCitasQuimioterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La cita debe existir en AGASICITA y estar en estado 0 (asignada); en otro estado se devuelve mensaje de error.; El paciente debe tener un ingreso activo de tipo oncológico-quimioterapia (TRATAESPECIA=3 e IESTADOIN en ('' '',''P'')); de lo contrario se aborta.; La unidad funcional/centro de atención deben tener parámetros configurados en HCUNITHIS (centro de costo y almacén/bodega); si no se aborta.; El XML de entrada debe contener la lista de productos en /ListaProductos/MedQuimio.; Debe existir el ciclo en EHR.HCORDCICLOSD para obtener la orden de quimio y el profesional.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ConfirmarCitasQuimioterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] dbo.ADINGRESO: Si el paciente tiene un ingreso hospitalario oncológico abierto/parcialmente facturado SIN cama activa Y existe un ingreso ambulatorio abierto/parcial usado para dispensación o administración de medicamentos de quimio, se desmarca TRATAESPECIA del ingreso hospitalario (NULL) y se marca TRATAESPECIA=3 en el ambulatorio.; [INSERT] dbo.HCFARMEPC: Inserta la cabecera de orden de farmacia con ORDESTADO=1, ORDTRANUE=1 (tratamiento nuevo), ORDENQUIMIO=1, IDCITA=cita y ConfirmationStatus=1 si existe configuración en MixingStation.MixingStationSettingAttentionCenter para el centro/unidad, sino NULL.; [INSERT] dbo.HCFARMEPD: Por cada producto del XML inserta detalle de farmacia con PROESTADO=1 (pendiente), TIPFORMED=''Dosis Unica'', cantidad pendiente=cantidad solicitada y TIPOREGIS=1 si es Tipo=1 (medicamento) o 2 en otro caso.; [INSERT] dbo.HCFISIPRO: Si no existe ya registro fisiológico para el paciente/ingreso/centro/unidad y producto, inserta un registro por producto con cantidad actual=0 y cantidad pendiente=cantidad solicitada.; [DELETE] dbo.HCHOJAMED: Antes de insertar, elimina registros previos de la hoja de medicamentos para la misma cita y ciclo con MEDESTADO=1 (permite reconfirmar).; [INSERT] dbo.HCHOJAMED: Inserta en la hoja de medicamentos solo los productos con Tipo=1; FECPROAPL/FECAPLMED se calculan según TypePrescription (1=fecha de la cita; 2=fecha cita + hora de frecuencia).; [INSERT] dbo.HCKARDPAC: Por cada producto inserta movimiento de kardex con TIPREGIST=1 (entrada) y observación ''Solicitud desde confirmación de citas de quimioterapia - (Agendamiento) - Usuario: <usuario>''.; [INSERT] MedicalHistory.ProductSusceptibleMixingStation: Si existe al menos un producto con ActivateMixingStation=1, inserta los productos Tipo=1 con CodigoAgrupador no nulo como susceptibles de central de mezclas con ApplicationsNumber=1.; [INSERT] MedicalHistory.PharmaDose: Para cada CodigoAgrupador distinto se genera un GroupingCodeDose (NEWID) y se insertan las dosis de farmacia asociadas con DeliveryStatus=1 y AppliedDose=0.; [UPDATE] dbo.AGASICITA: Al finalizar marca la cita con CONFIRMQUIMIO=1 y registra USUCONFIRMQUIMIO con el usuario que confirmó.; [RETURN_RESULT] RESULT: Devuelve CodeMessage=''0'' y el ID generado de farmacia al éxito; ''999'' con mensaje específico si la cita no está asignada, no hay ingreso oncológico, faltan parámetros de centro de costo/almacén o si ocurre error capturado en CATCH.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ConfirmarCitasQuimioterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ConfirmarCitasQuimioterapia';
-- GO
