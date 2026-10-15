
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