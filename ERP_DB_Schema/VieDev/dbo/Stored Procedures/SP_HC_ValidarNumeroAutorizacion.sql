-- Stored Procedure


-- =============================================
-- Author:		<Sumit Sarkar>
-- Create date: <23 de Septiembre 2019>
-- Description:	<Description, Averiguar que si el paciente ya tiene el mismo numero de autorizacion con misma entidad>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ValidarNumeroAutorizacion]
(
@PacientId as Varchar(25), @NumeroAutorizacion as Varchar(30), @Entidad as Char(9)
)

AS
BEGIN
	SET NOCOUNT ON;
	select A.NUMAUTORI, C.IPCODPACI,CASE A.CODTIPCIT WHEN 0 THEN 'Primera vez' WHEN 1 THEN 'Control' WHEN 2 THEN 'Pos-operatorio' END AS TIPOCITA,
	D.DESACTMED AS ACTIVIDAD, A.FECHORAIN AS FECHAINICIO, E.NOMENTIDA, 'AGASICITA' AS TABLA  , Rtrim(c.IPNOMCOMP)  as 'Nombre Paciente'
	FROM AGASICITA A  
	INNER JOIN INPACIENT C ON A.IPCODPACI = C.IPCODPACI 
	INNER JOIN AGACTIMED D ON D.CODACTMED = A.CODACTMED INNER JOIN 
	INENTIDAD E ON E.CODENTIDA = C.CODENTIDA 
	INNER JOIN Contract.HealthAdministrator  HEL ON C.GENCONENTITY = HEL.Id 
	WHERE A.NUMAUTORI = @NumeroAutorizacion AND HEL.code = @Entidad
UNION
	select A.NUMAUTORI, C.IPCODPACI AS TIPOCITA, 'Cirugia' AS TIPOCITA, D.DESSERIPS AS ACTIVIDAD, A.FECHORAIN AS FECHAINICIO, E.NOMENTIDA AS NOMENTIDA, 
	'AGEPROGQX' AS TABLA  , Rtrim(c.IPNOMCOMP)  as 'Nombre Paciente'
	FROM AGEPROGQX A  
	INNER JOIN INPACIENT C ON (A.IPCODPACI = C.IPCODPACI) 
	INNER JOIN  INCUPSIPS D ON D.CODSERIPS = A.CODSERIPS 
	INNER JOIN  INENTIDAD E ON C.CODENTIDA = E.CODENTIDA
	INNER JOIN Contract.HealthAdministrator  HEL ON C.GENCONENTITY = HEL.Id 
	WHERE A.NUMAUTORI =@NumeroAutorizacion AND HEL.code = @Entidad
   
END