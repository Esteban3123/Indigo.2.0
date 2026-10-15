-- =============================================
-- Author:		Rafael Eduardo Patiño cabrera
-- Create date: 21/11/2018
-- Description:	validar CUPS RIAS 
-- SP para validar el rando de edad del paciente y verificar si el CUPS que solicita lo puede o no pedir el medico 
-- de acuerdo a la parametrizacion CUPS x RIAS (RIASCUPSD)


--codigo Mensajes :
--1. El CUPS "Nombre del CUPS" no aplica para la ruta "Nombre de la ruta", ¿desea ordenarlo?.
--2. El CUPS "nombre del CUPS" no aplica para la ruta "Nombre de la ruta" según la edad del paciente, ¿desea ordenarlo?
--3. Desde la ultima vez que se realizó el CUPS "Nombre del CUPS" (Fecha de realización) no ha pasado un año, ¿desea ordenarlo?
--4. El CUPS "Nombre del CUPS" se ha realizado x veces en el trimestre (cantidad limite es: xxxx), la ultima vez fue el xxxxx, ¿desea ordenarlo? 

--Se modifica este sp de validación para agregarle dos parámetros: 
--1. Identificar desde donde se esta consumiendo el sp  
--2. Obtener la fecha de realización para realizar la validación

-- =============================================
CREATE PROCEDURE [dbo].[SP_RIAS_ValidacionCUPSRIAS]
	-- Add the parameters for the stored procedure here
	@Identificacion varchar(25),
	@IdRiasCUPS int,
	@CUPS varchar(20),
	@CantidadSolicitada int,
	@Source int, --Permite saber desde donde se consume el sp: 1.Orden de servicio, 2 -Plan Maneja y favoritos, 3 - Agendamiento - cuando llegue Rafa que coloque las otras enumeraciones
	@RealizationDate datetime --Fecha para realizar la validación de las cantidades realizadas, esta viene llena cuando el @Source sea 1
AS
BEGIN

	SET NOCOUNT ON;
	
	declare @NombreCUPS as varchar(150) 
	declare @NombreRuta as varchar(150)
	declare @FechaNacimiento as datetime

	--select @IdRiasCUPS = ID from RIASCUPS where ID in (select IDRIASCUPS  from [dbo].[AGACTIMED] where CODACTMED = '226')
	set @NombreRuta = (select R.CODPRO + ' - ' + R.NOMBRE   from RIASCUPS RC inner join RIAS R on R.ID = RC.IDRIAS  where RC.ID = @IdRiasCUPS)
    set @NombreCUPS	= (select rtrim(CODSERIPS) + ' - ' + rtrim(DESSERIPS) from INCUPSIPS where CODSERIPS =  @CUPS)
	
	select  @FechaNacimiento = IPFECNACI from INPACIENT where IPCODPACI = @Identificacion	 

	declare @Edaddias as integer
	--si es de origen 3 - Agendamiento
	if @Source = 3 begin
		set @Edaddias = datediff(DAY,@FechaNacimiento,@RealizationDate) --calculamos edad del paciente apartir de la fecha de la cita medica 
	end else begin 
		set @Edaddias = datediff(DAY,@FechaNacimiento,[Common].[GETDATE]()) 
	end
	
	

	declare @IdRangoActualAplica as integer
	declare @Regla as integer
	declare @Frecuencia as integer 
	declare @UnidadFrecuencia as integer
	declare @NumeroVecesOrdenes as integer
	declare @EdadMinima as integer
	declare @EdadMaxima as integer
	declare @CantidadPeriodo as integer
	declare @UnidadRangoEdad as varchar(100)
	declare @TmpCantidadRealizada_CantidadSolicitada as integer


	declare @TablaRegistrosAplica as table(ID integer identity,IDRIASCUPPACIENTE integer,CODSERIPS varchar(20),CANTIDAD integer, ESTADO integer,FECHAREALIZACION datetime, IDRANGOAPLICA integer) 	
	declare @FechaUltimoServicioRealizado as datetime	
	declare @cantidadRealizadas as integer = 0


	
	BEGIN TRY 

	

	--validamos que el CUPS aplique a la RIAS    
	--If NOT exists (select * from RIAS R inner join RIASCUPS RC on R.ID = RC.IDRIAS inner join INCUPSIPS C on C.CODSERIPS = RC.CODSERIPS where RC.ID = @IdRiasCUPS ) begin
	--	--select '999' as CodeMessage, 'El CUPS ' + @NombreCUPS  + ' no aplica para la ruta ' + @NombreRuta  + ', ¿desea ordenarlo?.'   as Mensaje
	--	select '1' as CodigoMensaje, '' as  Mensaje,@NombreCUPS as NombreCUPS, @NombreRuta as NombreRuta, @cantidadRealizadas as cantidadRealizadas, @Frecuencia as Frecuencia, @FechaUltimoServicioRealizado as FechaUltimoServicioRealizado, 0 as CantidadPeriodo, '' as Periodo ,  dbo.[Edad](getdate(),@FechaNacimiento) as Edad, @EdadMinima  as EdadMinima, @EdadMaxima as EdadMaxima, @UnidadRangoEdad as UnidadRangoEdad
	--	Return
	--End

	 set @IdRiasCUPS = (select ID from RIASCUPS where IDRIAS in (select R.ID from RIAS R inner join RIASCUPS RC on R.ID = RC.IDRIAS inner join INCUPSIPS C on C.CODSERIPS = RC.CODSERIPS where RC.ID = @IdRiasCUPS and R.ESTADO = 1) and CODSERIPS = @CUPS)

	If @IdRiasCUPS = 0 begin
		--select '999' as CodeMessage, 'El CUPS ' + @NombreCUPS  + ' no aplica para la ruta ' + @NombreRuta  + ', ¿desea ordenarlo?.'   as Mensaje
		select '1' as CodigoMensaje, '' as  Mensaje,@NombreCUPS as NombreCUPS, @NombreRuta as NombreRuta, @cantidadRealizadas as cantidadRealizadas, @Frecuencia as Frecuencia, @FechaUltimoServicioRealizado as FechaUltimoServicioRealizado, 0 as CantidadPeriodo, '' as Periodo ,  dbo.[Edad](getdate(),@FechaNacimiento) as Edad, @EdadMinima  as EdadMinima, @EdadMaxima as EdadMaxima, @UnidadRangoEdad as UnidadRangoEdad
		Return	
	End

	print convert(varchar(20),@Edaddias) + ' @Edaddias'
	print convert(varchar(20),@IdRiasCUPS) + ' @IdRiasCUPSjj'
	--Validamos que RIASCUPS en su parametrizacion retorne unregistro es decir aplique a algun rango de edad del paciente
	IF NOT exists (select * from dbo.fnValidaRangoEdadRIASCUPS(@Edaddias,@IdRiasCUPS))  begin
		--select '999' as CodeMessage, 'El CUPS ' + @NombreCUPS  + ' no aplica para la ruta ' + @NombreRuta  + ' según la edad del paciente, ¿desea ordenarlo?.'   as Mensaje
		select '2' as CodigoMensaje, '' as  Mensaje,@NombreCUPS as NombreCUPS, @NombreRuta as NombreRuta, @cantidadRealizadas as cantidadRealizadas, @Frecuencia as Frecuencia, @FechaUltimoServicioRealizado as FechaUltimoServicioRealizado, 0 as CantidadPeriodo, '' as Periodo  , dbo.[Edad](getdate(),@FechaNacimiento) as Edad, @EdadMinima  as EdadMinima, @EdadMaxima as EdadMaxima, @UnidadRangoEdad as UnidadRangoEdad
		Return
	End

	
	--tomamos la informacion del rango de edad a la que aplica
	select @IdRangoActualAplica = ID, @Regla  = REGLA, @Frecuencia = FRECUENCIA , @UnidadFrecuencia = UNIDADFRECUENCIA , @NumeroVecesOrdenes = NUMEROVECESORDEN, @EdadMinima = EDADMINIMA , @EdadMaxima = EDADMAXIMA, @UnidadRangoEdad = UnidadRangoEdad,  @CantidadPeriodo = CANTIDADPERIODO  
	from fnValidaRangoEdadRIASCUPS(@Edaddias,@IdRiasCUPS)
	print 'regla'
	 print @Regla
	

	if @Regla = 2 begin --//////////////////////////////////////2 - Frecuencia por rango de edad	////////////////////////////////////////////////////////////////////////////////////////
		
		/*
		0 - obtenemos los registro del historico que aplican al mismo rango actual a la que aplica el paciente para ello:
		1 - Estoy leyendo la tabla de rias cups por paciente para extraer el historico de la misma RiasCups realizado al paciente (filtro por Rias Cups, paciente, y estado 3 - realizados)
		2 - El calculo de la edad se realiza por cada registro en base a la fecha de realizacion y la fecha de nacimiento del paciente ( ya que necesito saber cual era la edad paciente cuando le realizaron el servicio)
		3 - una vez tengo el calculo de edad en dias por cada registro del historico paso esta edad a la funcion --fnValidaRangoEdadRIASCUPS-- que retorna a que rango de edad aplica (se realiza en subconsulta ya que es ciclico, por ende solo obtengo el ID del rango a las que aplica)
		4 - Si el ID del rango al que aplica los registro del historico  es igual al ID rango actual detectado al paciente , insertamos esos registro en la tabla temporal
		*/
		insert into @TablaRegistrosAplica
		select * from (
		select RCP.ID as IDRIASCUPPACIENTE,RCP.CODSERIPS,RCP.CANTIDAD, RCP.ESTADO,RCP.FECHAREALIZACION,(select ID from fnValidaRangoEdadRIASCUPS(datediff(DAY,P.IPFECNACI,RCP.FECHAREALIZACION),@IdRiasCUPS)) as  IDRANGOAPLICA
		from RIASCUPSPACIENTE RCP inner join INPACIENT P on P.IPCODPACI = RCP.IPCODPACI 
		where P.IPCODPACI = @Identificacion  AND IDRIASCUPS = @IdRiasCUPS AND CODSERIPS = @CUPS  AND ESTADO = 2  
		) as TMP where IDRANGOAPLICA = @IdRangoActualAplica

		print @IdRiasCUPS
		print @IdRangoActualAplica

		select top 1 @FechaUltimoServicioRealizado = FECHAREALIZACION from @TablaRegistrosAplica order by FECHAREALIZACION desc
		
		select  @cantidadRealizadas = ISNULL(SUM(CANTIDAD),0) from @TablaRegistrosAplica
		set @TmpCantidadRealizada_CantidadSolicitada =  @cantidadRealizadas + @CantidadSolicitada
		if @TmpCantidadRealizada_CantidadSolicitada > @Frecuencia begin
			--select '999' as CodeMessage, 'El CUPS ' + @NombreCUPS + ' se ha realizado ' + convert(varchar(20),@cantidadRealizadas) + ' veces en el rando de edad del paciente (cantidad limite es: '+ convert(varchar(20),@Frecuencia)  +'), la ultima vez fue el - ' + convert(varchar(20),@FechaUltimoServicioRealizado,103)  + ' , ¿desea ordenarlo?' as Mensaje
			select '3' as CodigoMensaje, '' as  Mensaje,@NombreCUPS as NombreCUPS,@NombreRuta as NombreRuta, @cantidadRealizadas as cantidadRealizadas, @Frecuencia as Frecuencia, @FechaUltimoServicioRealizado as FechaUltimoServicioRealizado ,0 as CantidadPeriodo, '' as Periodo , dbo.[Edad](getdate(),@FechaNacimiento) as Edad, @EdadMinima  as EdadMinima, @EdadMaxima as EdadMaxima, @UnidadRangoEdad as UnidadRangoEdad
			return 
		end

	end	if @Regla = 3 begin --///////////////////////////////3 - Frecuencia por ultima fecha de realización/////////////////////////////////////////////////////////
	    /*
		0 - obtenemos los registro del historico que aplican al mismo rango actual a la que aplica el paciente para ello:
		1 - Estoy leyendo la tabla de rias cups por paciente para extraer el historico de la misma RiasCups realizado al paciente (filtro por Rias Cups, paciente, y estado 3 - realizados)
		2 - El calculo de la edad se realiza por cada registro en base a la fecha de realizacion y la fecha de nacimiento del paciente ( ya que necesito saber cual era la edad paciente cuando le realizaron el servicio)
		3 - una vez tengo el calculo de edad en dias por cada registro del historico paso esta edad a la funcion --fnValidaRangoEdadRIASCUPS-- que retorna a que rango de edad aplica (se realiza en subconsulta ya que es ciclico, por ende solo obtengo el ID del rango a las que aplica)
		4 - Si el ID del rango al que aplica los registro del historico  es igual al ID rango actual detectado al paciente , insertamos esos registro en la tabla temporal
		*/
		
		insert into @TablaRegistrosAplica
		select * from (
		select RCP.ID as IDRIASCUPPACIENTE,RCP.CODSERIPS,RCP.CANTIDAD, RCP.ESTADO,RCP.FECHAREALIZACION,(select ID from fnValidaRangoEdadRIASCUPS(datediff(DAY,P.IPFECNACI,RCP.FECHAREALIZACION),@IdRiasCUPS)) as  IDRANGOAPLICA
		from RIASCUPSPACIENTE RCP inner join INPACIENT P on P.IPCODPACI = RCP.IPCODPACI 
		where P.IPCODPACI = @Identificacion  AND IDRIASCUPS = @IdRiasCUPS AND CODSERIPS = @CUPS  AND ESTADO = 2  
		) as TMP where IDRANGOAPLICA = @IdRangoActualAplica

		--de los registro que aplican al rango actual del paciente tomamos el ultimo realizado
		select top 1 @FechaUltimoServicioRealizado = FECHAREALIZACION from @TablaRegistrosAplica order by FECHAREALIZACION desc

		--si se encontraron registro Historico que aplican rando edad actual
		if exists(select count(ID) from @TablaRegistrosAplica )begin			
			if @UnidadFrecuencia = 1 begin --1 cada año
				if  DATEDIFF(day,@FechaUltimoServicioRealizado,[Common].[GETDATE]()) < (360 * @CantidadPeriodo)  begin
					--select '999' as CodeMessage, 'Desde la ultima vez que se realizó el CUPS ' + @NombreCUPS  + ' - ' + convert(varchar(20),@FechaUltimoServicioRealizado,103)  + ' no ha pasado un año, ¿desea ordenarlo?' as Mensaje
					select '4' as CodigoMensaje, '' as  Mensaje,@NombreCUPS as NombreCUPS, @NombreRuta as NombreRuta,@cantidadRealizadas as cantidadRealizadas, @Frecuencia as Frecuencia, @FechaUltimoServicioRealizado as FechaUltimoServicioRealizado ,@CantidadPeriodo as CantidadPeriodo,'año' as Periodo, dbo.[Edad](getdate(),@FechaNacimiento) as Edad, @EdadMinima  as EdadMinima, @EdadMaxima as EdadMaxima, @UnidadRangoEdad as UnidadRangoEdad
					return 
				end
			end if @UnidadFrecuencia = 2 begin --1 cada semestre
				if  DATEDIFF(day,@FechaUltimoServicioRealizado,[Common].[GETDATE]()) < (180 * @CantidadPeriodo) begin
					--select '999' as CodeMessage, 'Desde la ultima vez que se realizó el CUPS ' + @NombreCUPS  + ' - ' + convert(varchar(20),@FechaUltimoServicioRealizado,103)  + ' no ha pasado un semestre, ¿desea ordenarlo?' as Mensaje
					select '4' as CodigoMensaje, '' as  Mensaje,@NombreCUPS as NombreCUPS, @NombreRuta as NombreRuta,@cantidadRealizadas as cantidadRealizadas, @Frecuencia as Frecuencia, @FechaUltimoServicioRealizado as FechaUltimoServicioRealizado,@CantidadPeriodo as CantidadPeriodo,'semestre' as Periodo , dbo.[Edad](getdate(),@FechaNacimiento) as Edad, @EdadMinima  as EdadMinima, @EdadMaxima as EdadMaxima, @UnidadRangoEdad as UnidadRangoEdad
					return 
				end
			end if @UnidadFrecuencia = 3 begin --1 cada trimestre
				if  DATEDIFF(day,@FechaUltimoServicioRealizado,[Common].[GETDATE]()) < (90 * @CantidadPeriodo) begin
					--select '999' as CodeMessage, 'Desde la ultima vez que se realizó el CUPS ' + @NombreCUPS  + ' - ' + convert(varchar(20),@FechaUltimoServicioRealizado,103)  + ' no ha pasado un trimestre, ¿desea ordenarlo?' as Mensaje
					select '4' as CodigoMensaje, '' as  Mensaje,@NombreCUPS as NombreCUPS, @NombreRuta as NombreRuta,@cantidadRealizadas as cantidadRealizadas, @Frecuencia as Frecuencia, @FechaUltimoServicioRealizado as FechaUltimoServicioRealizado,@CantidadPeriodo as CantidadPeriodo,'semestre' as Periodo ,dbo.[Edad](getdate(),@FechaNacimiento) as Edad, @EdadMinima  as EdadMinima, @EdadMaxima as EdadMaxima, @UnidadRangoEdad as UnidadRangoEdad
					return 
				end
			end if @UnidadFrecuencia = 4 begin --1 cada mes
				if  DATEDIFF(day,@FechaUltimoServicioRealizado,[Common].[GETDATE]()) < (30 * @CantidadPeriodo) begin
					--select '999' as CodeMessage, 'Desde la ultima vez que se realizó el CUPS ' + @NombreCUPS  + ' - ' + convert(varchar(20),@FechaUltimoServicioRealizado,103)  + ' no ha pasado un mes, ¿desea ordenarlo?' as Mensaje
					select '4' as CodigoMensaje, '' as  Mensaje,@NombreCUPS as NombreCUPS, @NombreRuta as NombreRuta,@cantidadRealizadas as cantidadRealizadas, @Frecuencia as Frecuencia, @FechaUltimoServicioRealizado as FechaUltimoServicioRealizado, @CantidadPeriodo as CantidadPeriodo, 'mes' as Periodo , dbo.[Edad](getdate(),@FechaNacimiento) as Edad, @EdadMinima  as EdadMinima, @EdadMaxima as EdadMaxima, @UnidadRangoEdad as UnidadRangoEdad
					return 
				end
			End
		end
			
	end if @Regla = 4 begin --///////////////////////////////////////4 -Frecuencia por periodo vigente///////////////////////////////////////////////////////////////////////
		
		declare @MesActual as integer = MONTH([Common].[GETDATE]()) 
		declare @FechaInicioTmp as datetime
		declare @FechaFinalTmp as datetime
	
				
		
		insert into @TablaRegistrosAplica
		select RCP.ID as IDRIASCUPPACIENTE,RCP.CODSERIPS,RCP.CANTIDAD, RCP.ESTADO,RCP.FECHAREALIZACION,NULL from RIASCUPSPACIENTE RCP inner join INPACIENT P on P.IPCODPACI = RCP.IPCODPACI 
		where P.IPCODPACI = @Identificacion  AND IDRIASCUPS = @IdRiasCUPS  AND CODSERIPS = @CUPS  AND ESTADO = 2  
	
		select top 1 @FechaUltimoServicioRealizado = FECHAREALIZACION from @TablaRegistrosAplica order by FECHAREALIZACION desc

		--si se encontraron registro Historico que aplican rando edad actual
		if exists(select count(ID) from @TablaRegistrosAplica )begin
			
			if @UnidadFrecuencia = 5 begin --X En el año vigente
				
				--Se valida desde donde se esta consumiendo el sp
				if @Source = 1 --Si es desde orden de servicio
				begin
					--Se obtiene las cantidades realizadas con la fecha de realización
					select  @cantidadRealizadas = ISNULL(SUM(CANTIDAD),0) from @TablaRegistrosAplica where year(FECHAREALIZACION) = year(@RealizationDate)
				end
				else begin --Si viene de otro proceso
					--Se obtiene las cantidades realizadas con la fecha actual
					select  @cantidadRealizadas = ISNULL(SUM(CANTIDAD),0) from @TablaRegistrosAplica where year(FECHAREALIZACION) = year(getdate())
				end	
				
				set @TmpCantidadRealizada_CantidadSolicitada =  @cantidadRealizadas + @CantidadSolicitada
				if @TmpCantidadRealizada_CantidadSolicitada > @Frecuencia begin
					--select '999' as CodeMessage, 'El CUPS ' + @NombreCUPS + ' se ha realizado ' + convert(varchar(20),@cantidadRealizadas) + ' veces en el año vigente (cantidad limite es: '+ convert(varchar(20),@Frecuencia)  +'), la ultima vez fue el - ' + convert(varchar(20),@FechaUltimoServicioRealizado,103)  + ' , ¿desea ordenarlo?' as Mensaje
					select '5' as CodigoMensaje, '' as  Mensaje,@NombreCUPS as NombreCUPS, @NombreRuta as NombreRuta,@cantidadRealizadas as cantidadRealizadas, @Frecuencia as Frecuencia, @FechaUltimoServicioRealizado as FechaUltimoServicioRealizado , 0 as CantidadPeriodo, 'año' as Periodo, dbo.[Edad](getdate(),@FechaNacimiento) as Edad, @EdadMinima  as EdadMinima, @EdadMaxima as EdadMaxima, @UnidadRangoEdad as UnidadRangoEdad   
					return 
				end

			end if @UnidadFrecuencia = 6 begin --X En el semestre vigente

				if @MesActual =1 or @MesActual =2 or @MesActual =3 or @MesActual =4 or @MesActual = 5 or @MesActual = 6 begin
					set @FechaInicioTmp =  '01/01/' + convert(varchar(20),year(getdate()))
					set @FechaFinalTmp =   DATEADD(day,-1, DATEADD(MONTH,6,@FechaInicioTmp))
				end else begin
					set @FechaInicioTmp =  '01/07/' + convert(varchar(20),year(getdate()))
					set @FechaFinalTmp =    DATEADD(day,-1, DATEADD(MONTH,6,@FechaInicioTmp))
				end 
				
				select  @cantidadRealizadas = ISNULL(SUM(CANTIDAD),0) from @TablaRegistrosAplica where FECHAREALIZACION between @FechaInicioTmp and @FechaFinalTmp 
				set @TmpCantidadRealizada_CantidadSolicitada =  @cantidadRealizadas + @CantidadSolicitada
				if @TmpCantidadRealizada_CantidadSolicitada > @Frecuencia begin
					--select '999' as CodeMessage, 'El CUPS ' + @NombreCUPS + ' se ha realizado ' + convert(varchar(20),@cantidadRealizadas) + ' veces en el semestre vigente (cantidad limite es: '+ convert(varchar(20),@Frecuencia)  +'), la ultima vez fue el - ' + convert(varchar(20),@FechaUltimoServicioRealizado,103)  + ' , ¿desea ordenarlo?' as Mensaje
					select '5' as CodigoMensaje, '' as  Mensaje,@NombreCUPS as NombreCUPS, @NombreRuta as NombreRuta,@cantidadRealizadas as cantidadRealizadas, @Frecuencia as Frecuencia, @FechaUltimoServicioRealizado as FechaUltimoServicioRealizado , 0 as CantidadPeriodo, 'semestre' as Periodo, dbo.[Edad](getdate(),@FechaNacimiento) as Edad, @EdadMinima  as EdadMinima, @EdadMaxima as EdadMaxima, @UnidadRangoEdad as UnidadRangoEdad 
					return 
				end

			end if @UnidadFrecuencia = 7 begin  --X En el trimestre vigente
				
				if @MesActual =1 or @MesActual =2 or @MesActual =3  begin
					set @FechaInicioTmp =  '01/01/' + convert(varchar(20),year(getdate()))
					set @FechaFinalTmp =    DATEADD(day,-1, DATEADD(MONTH,3,@FechaInicioTmp))
				end if  @MesActual =4 or @MesActual = 5 or @MesActual = 6 begin
					set @FechaInicioTmp =  '01/04/' + convert(varchar(20),year(getdate()))
					set @FechaFinalTmp =   DATEADD(day,-1, DATEADD(MONTH,3,@FechaInicioTmp))
				end if  @MesActual =7 or @MesActual =8 or @MesActual = 9 begin
					set @FechaInicioTmp =  '01/07/' + convert(varchar(20),year(getdate()))
					set @FechaFinalTmp =   DATEADD(day,-1, DATEADD(MONTH,3,@FechaInicioTmp))
				end if  @MesActual =7 or @MesActual =8 or @MesActual = 9 begin
					set @FechaInicioTmp =  '01/10/' + convert(varchar(20),year(getdate()))
					set @FechaFinalTmp =   DATEADD(day,-1, DATEADD(MONTH,3,@FechaInicioTmp))
				end	

				select  @cantidadRealizadas = SUM(CANTIDAD) from @TablaRegistrosAplica where FECHAREALIZACION between @FechaInicioTmp and @FechaFinalTmp 
				set @TmpCantidadRealizada_CantidadSolicitada =  @cantidadRealizadas + @CantidadSolicitada
				if @TmpCantidadRealizada_CantidadSolicitada > @Frecuencia begin
					--select '999' as CodeMessage, 'El CUPS ' + @NombreCUPS + ' se ha realizado ' + convert(varchar(20),@cantidadRealizadas) + ' veces en el trimestre vigente (cantidad limite es: '+ convert(varchar(20),@Frecuencia)  +'), la ultima vez fue el - ' + convert(varchar(20),@FechaUltimoServicioRealizado,103)  + ' , ¿desea ordenarlo?' as Mensaje
					select '5' as CodigoMensaje,'' as  Mensaje, @NombreCUPS as NombreCUPS, @NombreRuta as NombreRuta,@cantidadRealizadas as cantidadRealizadas, @Frecuencia as Frecuencia, @FechaUltimoServicioRealizado as FechaUltimoServicioRealizado , 0 as CantidadPeriodo, 'trimestre' as Periodo, dbo.[Edad](getdate(),@FechaNacimiento) as Edad, @EdadMinima  as EdadMinima, @EdadMaxima as EdadMaxima, @UnidadRangoEdad as UnidadRangoEdad   
					return 
				end

			end if @UnidadFrecuencia = 8 begin  --X En el mes vigente 
				
				select  @cantidadRealizadas = ISNULL(SUM(CANTIDAD),0) from @TablaRegistrosAplica where month(FECHAREALIZACION) = month(getdate())
				set @TmpCantidadRealizada_CantidadSolicitada =  @cantidadRealizadas + @CantidadSolicitada
				if @TmpCantidadRealizada_CantidadSolicitada > @Frecuencia begin
					--select '999' as CodeMessage, 'El CUPS ' + @NombreCUPS + ' se ha realizado ' + convert(varchar(20),@cantidadRealizadas) + ' veces en el mes vigente (cantidad limite es: '+ convert(varchar(20),@Frecuencia)  +'), la ultima vez fue el - ' + convert(varchar(20),@FechaUltimoServicioRealizado,103)  + ' , ¿desea ordenarlo?' as Mensaje
					select '5' as CodigoMensaje,'' as  Mensaje,  @NombreCUPS as NombreCUPS, @NombreRuta as NombreRuta,@cantidadRealizadas as cantidadRealizadas, @Frecuencia as Frecuencia, @FechaUltimoServicioRealizado as FechaUltimoServicioRealizado ,0 as CantidadPeriodo, 'mes' as Periodo, dbo.[Edad](getdate(),@FechaNacimiento) as Edad, @EdadMinima  as EdadMinima, @EdadMaxima as EdadMaxima, @UnidadRangoEdad as UnidadRangoEdad   
					return 
				end

			end
		end


	end 
	
	select NULL as CodigoMensaje,'El proceso de validacion RIAS se ejecuto satisfactoriamente, no hubo validaciones' as  Mensaje, NULL as NombreCUPS, NULL as NombreRuta, @cantidadRealizadas as cantidadRealizadas, @Frecuencia as Frecuencia, NULL as FechaUltimoServicioRealizado, 0 as CantidadPeriodo,NULL as Periodo, NULL as Edad, NULL  as EdadMinima, NULL as EdadMaxima, @UnidadRangoEdad as UnidadRangoEdad	 

	end try
	begin catch
		select '999' as CodigoMensaje, ERROR_MESSAGE() + ', Linea: ' + cast(ERROR_LINE() as varchar(20)) Mensaje,NULL as NombreCUPS, NULL as NombreRuta, NULL as cantidadRealizadas, NULL as Frecuencia, NULL as FechaUltimoServicioRealizado, 0 as CantidadPeriodo,NULL as Periodo, NULL as Edad, NULL  as EdadMinima, NULL as EdadMaxima, @UnidadRangoEdad as UnidadRangoEdad	
	end catch
	
END