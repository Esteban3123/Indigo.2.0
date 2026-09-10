
CREATE FUNCTION [dbo].[FechasAplicacionMedicamentoOncologico] 
(
	@Tipo As Integer, ---1=Ultima Fecha Aplicacion ; 2=Proxima Fecha Aplicacion
	@Paciente AS VARCHAR(25),
	@Medicamento as varchar(50),
	@IDHCORDQUIMIO as INT,
	@OpcionConsulta as int -- 1:Programacion Medicamentos ; 2: Aplicación Medicamentos
)
RETURNS VARCHAR(100) 
AS
Begin
		declare @FechaUltimaAplicacion as varchar(100)

IF @OpcionConsulta = 1 begin

					IF @Tipo = 1  --1=Ultima Fecha Aplicacion
							BEGIN
		     			 
								 Declare @resultadoConsulta as varchar(100) = (  select TOP 1  FECAPLMED from HCHOJAMED WHERE IPCODPACI =@Paciente  AND CODPRODUC = @Medicamento AND IDHCORDQUIMIO = @IDHCORDQUIMIO ORDER BY FECAPLMED DESC)
								 if @resultadoConsulta is null or @resultadoConsulta = '' begin
									 set @FechaUltimaAplicacion = 'Sin administrar por primera vez'
								end else begin
									set @FechaUltimaAplicacion = @resultadoConsulta
								 end
	 	 

					END IF @Tipo = 2  --2=Proxima Fecha Aplicacion
							BEGIN

								Declare @resulConsulta as varchar(100) = ( select TOP 1 concat(FECHORAIN,',  Ciclo:', c.CICLO, ',  Dia:', c.dia) as 'Resultado'  from HCHOJAMED a inner join AGASICITA b on a.IDCITA  = B.CODAUTONU inner join EHR.HCORDCICLOSD c on c.ID = a.IDHCORDCICLOSD WHERE A.CODPRODUC = @Medicamento and  a.IDHCORDQUIMIO = @IDHCORDQUIMIO AND FECHORAIN >= GETDATE() ORDER BY FECHORAIN ASC ) 
								if @resulConsulta is null or @resulConsulta = '' begin
									set @FechaUltimaAplicacion = 'Sin programar'
								end else begin
									set @FechaUltimaAplicacion = @resulConsulta	
								end
							
					END

end
else if @OpcionConsulta = 2 begin

				IF @Tipo = 1  --1=Ultima Fecha Aplicacion
							BEGIN
		     			 
								 Declare @ResulQuery as varchar(100) = (  select TOP 1  FECAPLMED from HCHOJAMED WHERE IPCODPACI =@Paciente  AND CODPRODUC = @Medicamento AND IDHCORDQUIMIO = @IDHCORDQUIMIO ORDER BY FECAPLMED DESC)
								 if @ResulQuery is null or @ResulQuery = '' begin
									 set @FechaUltimaAplicacion = 'Sin administrar por primera vez'
								end else begin
									set @FechaUltimaAplicacion = @ResulQuery
								 end
	 	 

					END IF @Tipo = 2  --2=Proxima Fecha Aplicacion
							BEGIN

								Declare @ResultadoQuery as varchar(100) = ( select TOP 1 concat(FECHORAIN,',  Ciclo:', c.CICLO, ',  Dia:', c.dia) as 'Resultado'  from HCHOJAMED a inner join AGASICITA b on a.IDCITA  = B.CODAUTONU inner join EHR.HCORDCICLOSD c on c.ID = a.IDHCORDCICLOSD WHERE A.CODPRODUC = @Medicamento and  a.IDHCORDQUIMIO = @IDHCORDQUIMIO AND FECHORAIN >= GETDATE() ORDER BY FECHORAIN ASC ) 
								if @ResultadoQuery is null or @ResultadoQuery = '' begin
									set @FechaUltimaAplicacion = 'Sin programar'
								end else begin
									set @FechaUltimaAplicacion = @ResultadoQuery	
								end
							
					END

end

				
		Return @FechaUltimaAplicacion 

end
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que retorna la fecha de aplicación (última o próxima) de un medicamento oncológico para un paciente específico dentro de una orden de quimioterapia. Recibe como parámetros el tipo de consulta (última fecha administrada o próxima fecha programada), la cédula del paciente, el código del medicamento, el identificador de la orden de quimioterapia y la opción de consulta (programación o aplicación real). Para la última fecha administrada, consulta la hoja de administración de medicamentos (HCHOJAMED) y devuelve la fecha más reciente de aplicación, o el mensaje ''Sin administrar por primera vez'' si no existe registro. Para la próxima fecha, cruza la hoja de medicamentos con las citas agendadas (AGASICITA) y el detalle de días por ciclo de quimioterapia (HCORDCICLOSD), retornando la fecha futura más próxima junto con el número de ciclo y día del tratamiento oncológico, o el mensaje ''Sin programar'' si no hay citas pendientes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'FechasAplicacionMedicamentoOncologico';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'FechasAplicacionMedicamentoOncologico';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve, para un paciente y medicamento dentro de una orden de quimioterapia, la última fecha de aplicación o la próxima fecha programada (con ciclo y día), o un literal informativo si no hay datos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'FechasAplicacionMedicamentoOncologico';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir una orden de quimioterapia identificada para el paciente y medicamento consultados.; Para obtener la próxima fecha, la hoja de medicamento debe tener cita asociada en AGASICITA y un detalle de ciclo/día en EHR.HCORDCICLOSD.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'FechasAplicacionMedicamentoOncologico';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La última fecha de aplicación corresponde siempre al registro con FECAPLMED máxima asociado al paciente, medicamento y orden de quimioterapia.; La próxima fecha de aplicación siempre es estrictamente futura o igual al momento actual (FECHORAIN >= GETDATE()).; Cuando no existe información, la función nunca retorna NULL: entrega los textos ''Sin administrar por primera vez'' o ''Sin programar'' según el caso.; La búsqueda de próxima aplicación exige que la hoja de medicamento esté vinculada a una cita (AGASICITA) y a un ciclo/día de orden oncológica (EHR.HCORDCICLOSD).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'FechasAplicacionMedicamentoOncologico';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Medicamento oncológico; Orden de quimioterapia; Ciclo de quimioterapia; Día del ciclo; Aplicación/administración de medicamento; Programación de medicamentos; Cita médica; Hoja de medicamentos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'FechasAplicacionMedicamentoOncologico';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] function_scalar: Cuando Tipo=1 retorna la mayor FECAPLMED de HCHOJAMED filtrando por paciente, medicamento y orden de quimioterapia; si no hay registros retorna ''Sin administrar por primera vez''.; [RETURN_RESULT] function_scalar: Cuando Tipo=2 retorna la FECHORAIN futura más próxima concatenada con ciclo y día (HCHOJAMED⋈AGASICITA⋈EHR.HCORDCICLOSD, FECHORAIN >= GETDATE()); si no hay registros retorna ''Sin programar''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'FechasAplicacionMedicamentoOncologico';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si OpcionConsulta = 1 o 2 (programación o aplicación de medicamentos) → Ejecuta el mismo flujo de consulta sobre HCHOJAMED, diferenciando solo por contexto funcional; si Tipo = 1 (última fecha de aplicación) → Obtiene la FECAPLMED más reciente del paciente para el medicamento y orden de quimioterapia indicada else Si Tipo = 2, busca la próxima fecha programada (FECHORAIN >= GETDATE()) más cercana, junto con ciclo y día; si Resultado de la consulta de última aplicación es NULL o vacío → Devuelve literal ''Sin administrar por primera vez'' else Devuelve la fecha encontrada; si Resultado de la consulta de próxima aplicación es NULL o vacío → Devuelve literal ''Sin programar'' else Devuelve la fecha programada concatenada con ciclo y día', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'FechasAplicacionMedicamentoOncologico';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHOJAMED; dbo.AGASICITA; EHR.HCORDCICLOSD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'FechasAplicacionMedicamentoOncologico';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'FechasAplicacionMedicamentoOncologico';
GO
