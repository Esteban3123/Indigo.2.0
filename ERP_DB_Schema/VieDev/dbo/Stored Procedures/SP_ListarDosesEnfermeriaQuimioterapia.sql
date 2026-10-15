/**** 
SP para listar las dosis que se crean al confirmar la cita de quimioterapia para la construcción de los objetos Doses de UNIHEALTH
****/
CREATE PROCEDURE [dbo].[SP_ListarDosesEnfermeriaQuimioterapia]
(
  @Paciente as varchar(25),
  @Ingreso as char(10),
  @Producto as char(20),
  @IdCita as int
)
WITH RECOMPILE
AS
BEGIN
	SET NOCOUNT ON;
		
			SELECT 
				DOSISPROD as 'CANTIDAD DOSIS', A.FECPROAPL AS 'FECHA INICIO',
				RTRIM(A.CODUNIMED) AS 'COD UNIDAD MEDIDA', H.DESUNIMED AS 'UNIDAD MEDIDA', A.DOSISPROD AS 'DOSIS', 'Medicamento' as 'Tipo', CODPRODUC as 'CODPRODUC'
				,IIF(A.DURACIDOS = 'Dosis Unica', 1, 0) AS 'DOSIS UNICA'
			FROM HCHOJAMED A
			INNER JOIN INUNIMEDI H ON A.CODUNIMED = H.CODUNIMED
			WHERE A.IPCODPACI = @Paciente AND A.NUMINGRES = @Ingreso AND CODPRODUC = @Producto AND A.IDCITA = @IdCita AND MEDESTADO = 1

END