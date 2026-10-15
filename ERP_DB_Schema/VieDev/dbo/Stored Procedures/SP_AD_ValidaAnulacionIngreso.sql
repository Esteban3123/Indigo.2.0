CREATE PROCEDURE [dbo].[SP_AD_ValidaAnulacionIngreso]
(
@Ingreso Char(10),
@Paciente Varchar(25)
)
AS
BEGIN
	SET NOCOUNT ON;
	declare @EsperaDeTriagge as int
	declare @Historias as int
	declare @CodAzul as int
	declare @OrdenesDespacho as int
	
	--El Paciente Esta En Lista de Espera Triage
	set @EsperaDeTriagge = (SELECT COUNT(*) FROM dbo.ADCONTURG A INNER JOIN dbo.INEntidad B ON A.CODENTIDA=b.Codentida LEFT OUTER JOIN dbo.INPACIENT C ON A.IPCODPACI=C.IPCODPACI WHERE A.IPCODPACI=@Paciente AND CONESTADO='4')
	
	--El paciente tiene Historias en estado activo 
	set @Historias = (SELECT DISTINCT  COUNT(*)  FROM dbo.HCHISPACA WHERE NUMINGRES=@Ingreso AND ESTAFOLIO=1)

	--El paciente tiene ordenes de codigo azul
	set @CodAzul = (SELECT DISTINCT  COUNT(*)  FROM dbo.HCCODAZUC WHERE NUMINGRES=@Ingreso)

	--El paciente tiene ordenes de medicamentos 
	set @OrdenesDespacho = (SELECT DISTINCT  COUNT(*)  FROM dbo.HCFARMEPC WHERE NUMINGRES=@Ingreso)

	if @EsperaDeTriagge > 1
		begin
			SELECT 0 AS ESTADO_VALIDACION,'El Paciente Esta En Lista de Espera Triage' AS MENSAJE
		end
	else if @Historias > 1
		begin
			SELECT 0 AS ESTADO_VALIDACION,'El paciente tiene Historias en estado activo' AS MENSAJE
		end
	else if @CodAzul > 1
		begin
			SELECT 0 AS ESTADO_VALIDACION,'El paciente tiene ordenes de codigo azul' AS MENSAJE
		end
	else if @OrdenesDespacho > 1
		begin
			SELECT 0 AS ESTADO_VALIDACION,'El paciente tiene ordenes de medicamentos' AS MENSAJE
		end
	else
		begin
			SELECT 1 AS ESTADO_VALIDACION,'' AS MENSAJE
		end
end