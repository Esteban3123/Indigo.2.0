
-- =============================================
-- Author:		Juan David Patiño Cabrera
-- Create date: 09-10-2017
-- Description:	Sp que me cargara los Historicos de Revisión por Sistemas.
-- =============================================

CREATE PROCEDURE [dbo].[SP_HC_ListarHistoricosRevisionSistemas]
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

				
		--declare @Table1 as varchar(50), @Table2 as varchar(50), @Table3 as varchar(50)
		--declare @TableLeer as table(id int identity(1,1),TablaLeer varchar(50))
		declare @ValoresaTrasponer as table(IDGRUPO int,GRUPO varchar(200),ID int,VARIABLE varchar(200),VALOR varchar(max), FECHAHC datetime)
				
		--insert into @TableLeer 	
		--select  distinct top 3 TABLE_NAME as TablaLeer  from INFORMATION_SCHEMA.COLUMNS where  TABLE_NAME like '%RSVALORES2%' order by TABLE_NAME desc
		
		--set @Table1 = (select TablaLeer from @TableLeer where id =1)
		--set @Table2 = (select TablaLeer from @TableLeer where id =2)
		--set @Table3 = (select TablaLeer from @TableLeer where id =3)
		
	

		DECLARE @query AS NVARCHAR(MAX);		
		select @query  = ' select g.ID as IDGRUPO , g.NOMBRE as GRUPO, vr.ID , vr.VARIABLE, IIF(L.NOMBRE is null,VALOR,L.NOMBRE) as VALOR , H.FECHISPAC   from dbo.RSVARIABLES vr inner join 
																		dbo.RSGRUPO g on g.ID = vr.IDGRUPO  inner join
																		dbo.RSVALORES v  on vr.ID = v.IDRSVARIABLE inner join 
																		dbo.PRHCXRSGRUPO pg on g.ID = pg.IDRSGRUPO inner join
																		dbo.HCHISPACA h on h.ID = v.IDHCHISPACA left join 
																		dbo.RSVARIABLESL L on L.ID = V.IDITEMLISTA 
																		where pg.IDMODELOHC =  ' + cast(@IDMODELOHC as varchar(20))  + ' AND h.IPCODPACI =''' + @INDPaciente + '''
																		group by g.ID, g.NOMBRE, vr.ID , vr.VARIABLE, v.VALOR , H.FECHISPAC,VR.TIPO,L.NOMBRE' 
						  --UNION 
						  --select g.ID as IDGRUPO,g.NOMBRE as GRUPO, vr.ID , vr.VARIABLE,IIF(L.NOMBRE is null,VALOR,L.NOMBRE) as VALOR, H.FECHISPAC    from dbo.RSVARIABLES vr inner join 
								--										dbo.RSGRUPO g on g.ID = vr.IDGRUPO  inner join
								--										' + @Table2 + ' v  on vr.ID = v.IDRSVARIABLE inner join 
								--										dbo.PRHCXRSGRUPO pg on g.ID = pg.IDRSGRUPO inner join
								--										dbo.HCHISPACA h on h.ID = v.IDHCHISPACA left join 
								--										dbo.RSVARIABLESL L on L.ID = V.IDITEMLISTA
								--										where pg.IDMODELOHC =  ' + cast(@IDMODELOHC as varchar(20)) + ' AND h.IPCODPACI =''' + @INDPaciente + '''
								--										group by g.ID,g.NOMBRE, vr.ID , vr.VARIABLE, v.VALOR, H.FECHISPAC,VR.TIPO,L.NOMBRE 
						  --UNION 
					   --	  select g.ID as IDGRUPO,g.NOMBRE as GRUPO, vr.ID , vr.VARIABLE,IIF(L.NOMBRE is null,VALOR,L.NOMBRE) as VALOR, H.FECHISPAC    from dbo.RSVARIABLES vr inner join 
								--										dbo.RSGRUPO g on g.ID = vr.IDGRUPO  inner join
								--										' + @Table3 + ' v  on vr.ID = v.IDRSVARIABLE inner join 
								--										dbo.PRHCXRSGRUPO pg on g.ID = pg.IDRSGRUPO inner join
								--										dbo.HCHISPACA h on h.ID = v.IDHCHISPACA left join 
								--										dbo.RSVARIABLESL L on L.ID = V.IDITEMLISTA
								--										where pg.IDMODELOHC =  ' + cast(@IDMODELOHC as varchar(20)) + ' AND h.IPCODPACI =''' + @INDPaciente + '''
								--										group by g.ID,g.NOMBRE, vr.ID , vr.VARIABLE, v.VALOR, H.FECHISPAC,VR.TIPO,L.NOMBRE  '
     
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

		    --select distinct vr.VARIABLE from dbo.RSVARIABLES vr inner join dbo.RSVALORES v  on vr.ID = v.IDRSVARIABLE
			
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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que recupera el historial de revisión por sistemas registrado en la historia clínica de un paciente específico, para un modelo de historia clínica determinado. Consolida hasta tres registros históricos por cada variable clínica (síntoma, hallazgo o campo de revisión por sistemas), presentando los tres valores más recientes junto con sus fechas de registro. Se usa para visualizar la evolución clínica del paciente en la sección de revisión por sistemas dentro del módulo de historia clínica, permitiendo comparar cómo han variado los hallazgos en diferentes consultas o atenciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarHistoricosRevisionSistemas';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarHistoricosRevisionSistemas';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve, para un paciente y un modelo de historia clínica, las últimas tres mediciones históricas de cada variable de revisión por sistemas, transpuestas en columnas (VALOR1/2/3 con sus fechas).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistoricosRevisionSistemas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe existir en HCHISPACA (IPCODPACI); El modelo de HC debe tener grupos asociados en PRHCXRSGRUPO; Deben existir variables (RSVARIABLES) y valores (RSVALORES) ligados a folios del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistoricosRevisionSistemas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada combinación (IDGRUPO, IDVARIABLE) almacena como máximo 3 valores históricos (VALOR1, VALOR2, VALOR3); Los valores se asignan en orden descendente por FECHISPAC (el más reciente queda en VALOR1); Si la variable está parametrizada con lista (IDITEMLISTA) se muestra el nombre del ítem en lugar del valor crudo; Solo se consideran grupos asociados al modelo de HC indicado (PRHCXRSGRUPO.IDMODELOHC) y folios del paciente indicado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistoricosRevisionSistemas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Historia clínica; Paciente; Revisión por sistemas; Modelo de historia clínica; Variables clínicas; Grupos de variables; Histórico de mediciones', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistoricosRevisionSistemas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @TableResult: Cuando no existe registro previo para (IDGRUPO, IDVARIABLE) se inserta una nueva fila con el valor y fecha en VALOR1/FECHAVALOR1; [UPDATE] @TableResult: Cuando ya existe registro para (IDGRUPO, IDVARIABLE) y VALOR2 está NULL se actualiza VALOR2 y FECHAVALOR2 con el siguiente registro recorrido; [UPDATE] @TableResult: Cuando ya existe registro y VALOR2 no es NULL, se actualiza VALOR3 y FECHAVALOR3 (tercera ocurrencia); [RETURN_RESULT] (resultset): Al finalizar el cursor retorna SELECT * de @TableResult con los históricos transpuestos; [RETURN_RESULT] (resultset): En CATCH retorna fila con CodeMessage=999 y mensaje + línea de error', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistoricosRevisionSistemas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si No existe fila previa en @TableResult para el grupo+variable → Inserta fila nueva con VALOR1/FECHAVALOR1 else Evalúa si VALOR2 está libre o ya ocupado; si Existe fila y VALOR2 IS NULL → Actualiza VALOR2/FECHAVALOR2 else Actualiza VALOR3/FECHAVALOR3; si RSVARIABLESL.NOMBRE IS NULL (variable no es de lista) → Toma el VALOR crudo de RSVALORES else Toma el NOMBRE del ítem de lista RSVARIABLESL; si En CATCH, si el cursor global ''infoItem'' sigue abierto → CLOSE y DEALLOCATE del cursor', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistoricosRevisionSistemas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.RSVARIABLES; dbo.RSGRUPO; dbo.RSVALORES; dbo.PRHCXRSGRUPO; dbo.HCHISPACA; dbo.RSVARIABLESL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistoricosRevisionSistemas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistoricosRevisionSistemas';
-- GO
