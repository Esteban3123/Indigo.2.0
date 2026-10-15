
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