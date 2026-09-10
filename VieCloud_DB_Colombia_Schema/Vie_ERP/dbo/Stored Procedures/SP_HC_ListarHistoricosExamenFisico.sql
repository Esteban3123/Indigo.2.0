
-- =============================================
-- Author:		Juan David Patiño Cabrera
-- Create date: 09-10-2017
-- Description:	Sp que me cargara los Historicos de Examen Físico.
-- =============================================

CREATE PROCEDURE [dbo].[SP_HC_ListarHistoricosExamenFisico]
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
		--select  distinct top 3 TABLE_NAME as TablaLeer  from INFORMATION_SCHEMA.COLUMNS where  TABLE_NAME like '%EXAVALORES2%' order by TABLE_NAME desc
		
		--set @Table1 = (select TablaLeer from @TableLeer where id =1)
		--set @Table2 = (select TablaLeer from @TableLeer where id =2)
		--set @Table3 = (select TablaLeer from @TableLeer where id =3)
		
	

		DECLARE @query AS NVARCHAR(MAX);		
		select @query  = ' select g.ID as IDGRUPO , g.NOMBRE as GRUPO, vr.ID , vr.VARIABLE, IIF(L.NOMBRE is null,VALOR,L.NOMBRE) as VALOR , H.FECHISPAC   from dbo.EXAVARIABLES vr inner join 
																		dbo.EXAGRUPO g on g.ID = vr.IDEXAGRUPO  inner join
																		dbo.EXAVALORES v  on vr.ID = v.IDEXAVARIABLE inner join 
																		dbo.PRHCXEXAGRUPO pg on g.ID = pg.IDEXAGRUPO inner join
																		dbo.HCHISPACA h on h.ID = v.IDHCHISPACA left join
																		dbo.EXAVARIABLESL L on L.ID = V.IDITEMLISTA 
																		where pg.IDMODELOHC =  ' + cast(@IDMODELOHC as varchar(20))  + ' AND h.IPCODPACI =''' + @INDPaciente + '''
																		group by g.ID, g.NOMBRE, vr.ID , vr.VARIABLE, v.VALOR , H.FECHISPAC,VR.TIPO,L.NOMBRE' 
						  --UNION 
						  --select g.ID as IDGRUPO,g.NOMBRE as GRUPO, vr.ID , vr.VARIABLE, IIF(L.NOMBRE is null,VALOR,L.NOMBRE) as VALOR, H.FECHISPAC    from dbo.EXAVARIABLES vr inner join 
								--										dbo.EXAGRUPO g on g.ID = vr.IDEXAGRUPO  inner join
								--										' + @Table2 + ' v  on vr.ID = v.IDEXAVARIABLE inner join 
								--										dbo.PRHCXEXAGRUPO pg on g.ID = pg.IDEXAGRUPO inner join
								--										dbo.HCHISPACA h on h.ID = v.IDHCHISPACA left join
								--										dbo.EXAVARIABLESL L on L.ID = V.IDITEMLISTA 
								--										where pg.IDMODELOHC =  ' + cast(@IDMODELOHC as varchar(20))  + ' AND h.IPCODPACI =''' + @INDPaciente + '''
								--										group by g.ID,g.NOMBRE, vr.ID , vr.VARIABLE, v.VALOR, H.FECHISPAC,VR.TIPO,L.NOMBRE 
						  --UNION 
					   --	  select g.ID as IDGRUPO,g.NOMBRE as GRUPO, vr.ID , vr.VARIABLE, IIF(L.NOMBRE is null,VALOR,L.NOMBRE) as VALOR, H.FECHISPAC    from dbo.EXAVARIABLES vr inner join 
								--										dbo.EXAGRUPO g on g.ID = vr.IDEXAGRUPO  inner join
								--										' + @Table3 + ' v  on vr.ID = v.IDEXAVARIABLE inner join 
								--										dbo.PRHCXEXAGRUPO pg on g.ID = pg.IDEXAGRUPO inner join
								--										dbo.HCHISPACA h on h.ID = v.IDHCHISPACA left join
								--										dbo.EXAVARIABLESL L on L.ID = V.IDITEMLISTA 
								--										where pg.IDMODELOHC =  ' + cast(@IDMODELOHC as varchar(20))  + ' AND h.IPCODPACI =''' + @INDPaciente + '''
								--										group by g.ID,g.NOMBRE, vr.ID , vr.VARIABLE, v.VALOR, H.FECHISPAC,VR.TIPO,L.NOMBRE  '

																	print @query

	insert into @ValoresaTrasponer 
	execute(@query)

	--select * from @ValoresaTrasponer	

	    Declare InfoItemExamenFisico Cursor For 	select * from @ValoresaTrasponer order by FECHAHC desc
		Open InfoItemExamenFisico
		Fetch Next From InfoItemExamenFisico Into @IdGrupo,@Grupo, @IdVariable, @VARIABLE, @VALOR,@FechaHC
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
			Fetch Next From InfoItemExamenFisico Into @IdGrupo,@Grupo, @IdVariable, @VARIABLE, @VALOR,@FechaHC
		End
		Close InfoItemExamenFisico
		Deallocate InfoItemExamenFisico

		 
		select * from @TableResult

	end try
	begin catch
	 select 999 as CodeMessage, ERROR_MESSAGE() + ' Linea ' + cast(ERROR_LINE() as varchar(100)) as Message
			 IF (SELECT CURSOR_STATUS('global', 'InfoItemExamenFisico')) = 1 /*El cursor está abierto*/
			BEGIN
				CLOSE InfoItemExamenFisico		
			END	
			DEALLOCATE InfoItemExamenFisico
	end catch

	

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que recupera el historial de exámenes físicos registrados en la historia clínica de un paciente específico, según un modelo de historia clínica configurado. Consulta los grupos y variables del examen físico junto con sus valores registrados en cada atención, mostrando hasta los tres registros más recientes por cada variable (los dos anteriores al valor actual). Pivota los datos para presentar, en una sola fila por variable, el valor más reciente, el segundo más reciente y el tercero más reciente con sus respectivas fechas de registro, facilitando la comparación histórica de signos y hallazgos del examen físico del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarHistoricosExamenFisico';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarHistoricosExamenFisico';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve los últimos tres registros históricos de variables del examen físico de un paciente, según un modelo de historia clínica, transponiendo los valores y sus fechas en columnas paralelas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistoricosExamenFisico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe tener historias clínicas (HCHISPACA) asociadas con valores en EXAVALORES.; El modelo de historia clínica debe tener grupos de examen configurados en PRHCXEXAGRUPO.; Las variables deben pertenecer a un grupo (EXAGRUPO) y opcionalmente referenciar un ítem de lista (EXAVARIABLESL).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistoricosExamenFisico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Por cada par (grupo, variable) se conservan máximo 3 valores históricos en columnas VALOR1/VALOR2/VALOR3.; Los valores se asignan en orden cronológico descendente: el más reciente queda en VALOR1.; Si la variable está asociada a un ítem de lista, prevalece el nombre de la lista sobre el valor literal.; Solo se incluyen grupos de examen vinculados al modelo de historia clínica indicado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistoricosExamenFisico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Examen físico; Historia clínica; Paciente; Modelo de historia clínica; Variables de examen; Grupos de examen; Histórico de valores clínicos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistoricosExamenFisico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultado: Retorna por cada combinación grupo-variable hasta 3 valores históricos (VALOR1/VALOR2/VALOR3) con sus fechas, ordenados por FECHISPAC descendente.; [RETURN_RESULT] Resultado: Si ocurre un error, devuelve un conjunto con CodeMessage=999 y el mensaje de error junto con la línea.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistoricosExamenFisico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si No existe registro previo para el grupo+variable en el resultado → Inserta el valor como VALOR1 con su fecha en FECHAVALOR1 else Se evalúa si VALOR2 está vacío; si Existe fila para grupo+variable y VALOR2 IS NULL → Actualiza VALOR2 y FECHAVALOR2 con el valor actual else Actualiza VALOR3 y FECHAVALOR3; si EXAVARIABLESL.NOMBRE no es nulo (la variable usa lista) → Se muestra el NOMBRE del ítem de lista como VALOR else Se muestra el VALOR crudo registrado; si El cursor InfoItemExamenFisico permanece abierto al ocurrir un error → Se cierra y desasigna el cursor en el bloque CATCH', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistoricosExamenFisico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.EXAVARIABLES; dbo.EXAGRUPO; dbo.EXAVALORES; dbo.PRHCXEXAGRUPO; dbo.HCHISPACA; dbo.EXAVARIABLESL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistoricosExamenFisico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistoricosExamenFisico';
-- GO
