

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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que analiza la ocupación actual de las camas hospitalarias de una unidad funcional y centro de atención específicos (unidad N01 del centro 001). Para cada cama, consulta el historial de estancias del paciente (CHREGESTA) y determina quién es el paciente actualmente ocupando la cama, cuándo fue el último egreso del paciente anterior, cuándo inició la estancia actual y cuántas horas lleva la cama en su estado vigente. Sirve para el monitoreo operativo en tiempo real de la ocupación de camas hospitalarias, identificando rápidamente camas con paciente activo y el tiempo transcurrido desde el último movimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_CH_OcupacionCamas';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_CH_OcupacionCamas';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reporta el estado de ocupación de camas hospitalarias mostrando paciente actual, paciente anterior, fecha del último egreso, inicio de la estancia actual y horas transcurridas entre ambos eventos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CH_OcupacionCamas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir camas registradas con código de centro ''001'' y unidad funcional ''N01''.; Debe existir relación entre la cama y registros de estancia para obtener historial.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CH_OcupacionCamas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran camas del centro de atención ''001'' y unidad funcional ''N01''.; Se reportan únicamente las dos últimas estancias por cama (la más reciente como estancia actual y la previa como último egreso).; La diferencia de horas se calcula entre la fecha final de la penúltima estancia y la fecha inicial de la última estancia (tiempo de cama vacía/rotación).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CH_OcupacionCamas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'cama hospitalaria; ocupación de camas; paciente; estancia hospitalaria; egreso; centro de atención; unidad funcional', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CH_OcupacionCamas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ?: Por cada cama del centro ''001'' y unidad ''N01'', se emite un result set con paciente actual, paciente anterior, fecha de último egreso, fecha de estancia actual y diferencia en horas entre egreso anterior e ingreso actual.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CH_OcupacionCamas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.CHCAMASHO; dbo.CHREGESTA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CH_OcupacionCamas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CH_OcupacionCamas';
-- GO
