
CREATE PROCEDURE [dbo].[SP_HC_ListarHistoricosOtros]
	@INDPaciente varchar(25),
	@IDMODELOHC integer
AS
BEGIN
	SET NOCOUNT ON;

	Begin try
		

		declare @TableResult as table(id int identity(1,1) PRIMARY KEY, IDGRUPO int, GRUPO varchar(200), IDVARIABLE int, VARIABLE varchar(200), VALOR1 varchar(max),VALOR2 varchar(max),VALOR3 varchar(max), FECHAVALOR1 datetime, FECHAVALOR2 datetime, FECHAVALOR3 datetime)

		declare @IdGrupo as int
		declare @Grupo as varchar(200)
		declare @IdVariable as int
		declare @VARIABLE as varchar(200)
		declare @VALOR as varchar(max)
		declare @FechaHC as datetime

				
		declare @Table1 as varchar(50), @Table2 as varchar(50), @Table3 as varchar(50)
		declare @TableLeer as table(id int identity(1,1),TablaLeer varchar(50))
		declare @ValoresaTrasponer as table(IDGRUPO int,GRUPO varchar(200),ID int,VARIABLE varchar(200),VALOR varchar(max), FECHAHC datetime)
				
		--insert into @TableLeer 	
		--select  distinct top 3 TABLE_NAME as TablaLeer  from INFORMATION_SCHEMA.COLUMNS where  TABLE_NAME like '%OTVALORES2%' order by TABLE_NAME desc
		
		--set @Table1 = (select TablaLeer from @TableLeer where id =1)
		--set @Table2 = (select TablaLeer from @TableLeer where id =2)
		--set @Table3 = (select TablaLeer from @TableLeer where id =3)
		
		
		DECLARE @query AS NVARCHAR(MAX);		
		--select @query  = ' select g.ID as IDGRUPO , g.NOMBRE as GRUPO, vr.ID , vr.VARIABLE, IIF(L.NOMBRE is null,VALOR,L.NOMBRE) as VALOR , H.FECHISPAC   from dbo.OTVARIABLES vr inner join 
		--																dbo.OTGRUPO g on g.ID = vr.IDGRUPO  inner join
		--																' + @Table1 + ' v  on vr.ID = v.IDOTVARIABLE inner join 
		--																dbo.PRHCXOTGRUPO pg on g.ID = pg.IDOTGRUPO inner join
		--																dbo.HCHISPACA h on h.ID = v.IDHCHISPACA left join 
		--																dbo.OTVARIABLESL L on L.ID = V.IDITEMLISTA 
		--																where pg.IDMODELOHC =  ' + cast(@IDMODELOHC as varchar(20))  + ' AND h.IPCODPACI =''' + @INDPaciente + '''
		--																group by g.ID, g.NOMBRE, vr.ID , vr.VARIABLE, v.VALOR , H.FECHISPAC,VR.TIPO,L.NOMBRE 
		--				  UNION 
		--				  select g.ID as IDGRUPO,g.NOMBRE as GRUPO, vr.ID , vr.VARIABLE,IIF(L.NOMBRE is null,VALOR,L.NOMBRE) as VALOR, H.FECHISPAC    from dbo.OTVARIABLES vr inner join 
		--																dbo.OTGRUPO g on g.ID = vr.IDGRUPO  inner join
		--																' + @Table2 + ' v  on vr.ID = v.IDOTVARIABLE inner join 
		--																dbo.PRHCXOTGRUPO pg on g.ID = pg.IDOTGRUPO inner join
		--																dbo.HCHISPACA h on h.ID = v.IDHCHISPACA left join 
		--																dbo.OTVARIABLESL L on L.ID = V.IDITEMLISTA
		--																where pg.IDMODELOHC =  ' + cast(@IDMODELOHC as varchar(20)) + ' AND h.IPCODPACI =''' + @INDPaciente + '''
		--																group by g.ID,g.NOMBRE, vr.ID , vr.VARIABLE, v.VALOR, H.FECHISPAC,VR.TIPO,L.NOMBRE 
		--				  UNION 
		--			   	  select g.ID as IDGRUPO,g.NOMBRE as GRUPO, vr.ID , vr.VARIABLE,IIF(L.NOMBRE is null,VALOR,L.NOMBRE) as VALOR, H.FECHISPAC    from dbo.OTVARIABLES vr inner join 
		--																dbo.OTGRUPO g on g.ID = vr.IDGRUPO  inner join
		--																' + @Table3 + ' v  on vr.ID = v.IDOTVARIABLE inner join 
		--																dbo.PRHCXOTGRUPO pg on g.ID = pg.IDOTGRUPO inner join
		--																dbo.HCHISPACA h on h.ID = v.IDHCHISPACA left join 
		--																dbo.OTVARIABLESL L on L.ID = V.IDITEMLISTA
		--																where pg.IDMODELOHC =  ' + cast(@IDMODELOHC as varchar(20)) + ' AND h.IPCODPACI =''' + @INDPaciente + '''
		--																group by g.ID,g.NOMBRE, vr.ID , vr.VARIABLE, v.VALOR, H.FECHISPAC,VR.TIPO,L.NOMBRE  '
     
	 select @query  = ' select g.ID as IDGRUPO , g.NOMBRE as GRUPO, vr.ID , vr.VARIABLE, IIF(L.NOMBRE is null,VALOR,L.NOMBRE) as VALOR , H.FECHISPAC   from dbo.OTVARIABLES vr inner join 
																		dbo.OTGRUPO g on g.ID = vr.IDGRUPO  inner join
																		' + 'OTVALORES' + ' v  on vr.ID = v.IDOTVARIABLE inner join 
																		dbo.PRHCXOTGRUPO pg on g.ID = pg.IDOTGRUPO inner join
																		dbo.HCHISPACA h on h.ID = v.IDHCHISPACA left join 
																		dbo.OTVARIABLESL L on L.ID = V.IDITEMLISTA 
																		where pg.IDMODELOHC =  ' + cast(@IDMODELOHC as varchar(20))  + ' AND h.IPCODPACI =''' + @INDPaciente + '''
																		group by g.ID, g.NOMBRE, vr.ID , vr.VARIABLE, v.VALOR , H.FECHISPAC,VR.TIPO,L.NOMBRE
'

     

    print @query
	insert into @ValoresaTrasponer 
	execute(@query)

	--select * from @ValoresaTrasponer	

	    Declare InfoItem Cursor For 	select * from @ValoresaTrasponer order by FECHAHC desc
		Open InfoItem
		Fetch Next From InfoItem Into @IdGrupo,@Grupo, @IdVariable, @VARIABLE, @VALOR,@FechaHC
		While @@fetch_status = 0
		Begin

			declare @Existe as int  = (select count(*) from @TableResult where IDGRUPO = @IdGrupo AND IDVARIABLE = @IdVariable)

			if @Existe = 0 begin
				
				insert into @TableResult 
				select @IdGrupo,@Grupo,@IdVariable,@VARIABLE,@VALOR,NULL,NULL,@FechaHC,null,null

			end	else begin 

					set @Existe  = (select count(*) from @TableResult where IDGRUPO = @IdGrupo AND IDVARIABLE = @IdVariable AND VALOR2 IS NULL)
					if @Existe = 0 begin
						update @TableResult set VALOR3 = @VALOR, FECHAVALOR3 = @FechaHC where IDGRUPO = @IdGrupo AND IDVARIABLE = @IdVariable
					end else begin
						update @TableResult set VALOR2 = @VALOR, FECHAVALOR2 = @FechaHC  where IDGRUPO = @IdGrupo AND IDVARIABLE = @IdVariable
					end
										
			end

		    --select distinct vr.VARIABLE from dbo.OTVARIABLES vr inner join dbo.RSVALORES v  on vr.ID = v.IDOTVARIABLE
			
			--Se pasa a la siguiente posicion del cursor
			Fetch Next From InfoItem Into @IdGrupo,@Grupo, @IdVariable, @VARIABLE, @VALOR,@FechaHC
		End
		Close InfoItem
		Deallocate InfoItem

		 
		select * from @TableResult

	end try
	begin catch
	 select 999 as CodeMessage, ERROR_MESSAGE() + ' Linea ' + cast(ERROR_LINE() as varchar(100)) as Message
			 IF (SELECT CURSOR_STATUS('global', 'infoItem')) = 1 /*El cursor está abierto*/
			BEGIN
				CLOSE infoItem		
			END	
			DEALLOCATE infoItem
	end catch

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista el historial de variables clínicas ''otras'' (campos adicionales de historia clínica, distintos de signos vitales o medicamentos estándar) registradas para un paciente en un modelo de historia clínica específico. Consulta los valores almacenados en la tabla OTVALORES cruzando grupos de variables (OTGRUPO), definición de variables (OTVARIABLES) y los ítems de lista (OTVARIABLESL), filtrando por cédula del paciente y el modelo de HC indicado. Para cada variable del paciente, devuelve los tres registros más recientes (valor 1, valor 2 y valor 3 con sus respectivas fechas), permitiendo visualizar la evolución histórica de cada campo clínico personalizado. Se usa en la pantalla de historia clínica para mostrar los antecedentes y evolución de formularios dinámicos o secciones ''otros'' dentro de la atención del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarHistoricosOtros';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarHistoricosOtros';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve los últimos hasta tres valores históricos de las variables de un modelo de historia clínica para un paciente, transpuestos en columnas con sus fechas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistoricosOtros';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir la tabla OTVALORES con columnas IDOTVARIABLE, IDHCHISPACA e IDITEMLISTA.; El modelo de HC indicado debe tener grupos asociados en PRHCXOTGRUPO.; El paciente debe tener registros en HCHISPACA (IPCODPACI).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistoricosOtros';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El cursor procesa los registros ordenados por FECHAHC descendente, por lo que VALOR1 corresponde al valor más reciente.; Cada combinación (IDGRUPO, IDVARIABLE) aparece una sola vez en el resultado final.; Solo se conservan hasta 3 ocurrencias por variable; ocurrencias adicionales sobreescriben VALOR3.; Solo se incluyen variables cuyos grupos pertenezcan al modelo de HC indicado y cuyos registros correspondan al paciente indicado.; Ante una excepción se garantiza el cierre y desasignación del cursor InfoItem si quedó abierto.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistoricosOtros';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Historia clínica; Paciente; Modelo de historia clínica; Variables clínicas; Grupos de variables; Histórico clínico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistoricosOtros';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve un set con IDGRUPO, GRUPO, IDVARIABLE, VARIABLE y hasta 3 valores históricos (VALOR1/VALOR2/VALOR3) con sus fechas (FECHAVALOR1/2/3) por variable.; [RETURN_RESULT] resultset: En caso de error en el TRY, devuelve un set con CodeMessage=999 y el mensaje de error junto al número de línea.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistoricosOtros';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si No existe aún fila para el par (IDGRUPO, IDVARIABLE) en el resultado → Inserta nueva fila con el valor y fecha en VALOR1/FECHAVALOR1; si Ya existe fila y VALOR2 sigue NULL (segunda ocurrencia de la variable) → Actualiza VALOR2 y FECHAVALOR2 con el valor actual; si Ya existe fila y VALOR2 no es NULL (tercera o más ocurrencia) → Actualiza VALOR3 y FECHAVALOR3 con el valor actual, sobreescribiendo si llegan más; si L.NOMBRE (descripción del ítem de lista) no es NULL → Usa L.NOMBRE como VALOR; si es NULL, usa el VALOR crudo de la tabla de valores', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistoricosOtros';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.OTVARIABLES; dbo.OTGRUPO; dbo.PRHCXOTGRUPO; dbo.HCHISPACA; dbo.OTVARIABLESL; dbo.OTVALORES', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistoricosOtros';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistoricosOtros';
-- GO
