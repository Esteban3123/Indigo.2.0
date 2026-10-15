

CREATE PROCEDURE [dbo].[SP_CH_OcupacionCamas]

as

declare @CODICAMAS as varchar(20)
			declare @NUMCAMHOS as varchar(20)
            declare CursorHistorias cursor for	
			select distinct H.CODICAMAS,c.NUMCAMHOS  from CHCAMASHO c inner join CHREGESTA h on c.CODICAMAS = h.CODICAMAS  where  c.codcenate in ('001') and c.ufucodigo='N01'
			open CursorHistorias;
			FETCH NEXT FROM CursorHistorias
			INTO @CODICAMAS,@NUMCAMHOS
			WHILE @@FETCH_STATUS = 0  
				BEGIN
					
					
					--Se cuadra el diagnostico principal en indiagnoh
					 
		   			    declare @FechaFinal as datetime
						declare @FechaInicial as datetime
						
						declare @PacienteActual as varchar(25) = ''
						declare @PacienteAntes as varchar(25) =''
			

						select top 1  @FechaInicial = FECFINEST  , @PacienteAntes = IPCODPACI   
						from (
						select top 2 * from CHREGESTA where CODICAMAS =@CODICAMAS order by FECREGSIS desc
						) as sub order by FECREGSIS asc

						select top 1  @FechaFinal = FECINIEST  , @PacienteActual  = IPCODPACI   
						from (
						select top 2 * from CHREGESTA where CODICAMAS =@CODICAMAS order by FECREGSIS desc
						) as sub order by FECREGSIS desc

						select @CODICAMAS as 'Codicamas', @NUMCAMHOS as 'CodigoCama',@PacienteActual as PacienteActual, @FechaInicial as 'UltimoEgreso', @PacienteAntes as pacienteAntes, 
						 @FechaFinal as 'EstanciaActual',DATEDIFF(hour, @FechaInicial,@FechaFinal) as 'HorasDiferencia'

										
					FETCH NEXT FROM CursorHistorias
					INTO @CODICAMAS,@NUMCAMHOS
				END
			CLOSE CursorHistorias;  
			DEALLOCATE CursorHistorias;