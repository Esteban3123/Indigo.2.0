-- =============================================
-- Author:      Hector Rodriguez Rubiano
-- Create Date: 27/01/2021
-- Description: Unidades funcionales autorizados por usuario o grupo y centro de atencion
-- =============================================
CREATE PROCEDURE [dbo].[SP_SEG_UnidadFuncional_Autorizado]
(
    -- Add the parameters for the stored procedure here
    @Usuario CHAR(20),
    @Grupo CHAR(3),
	@CentroAtencion CHAR(10)
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

    -- Insert statements for procedure here
	SELECT
		RTRIM(A.UFUCODIGO) AS Codigo
	   ,RTRIM(A.UFUDESCRI) AS UnidadFuncional
	   ,A.UFUTIPUNI AS TipoUnidadFuncional
	   ,CASE A.UFUTIPUNI
			WHEN 1 THEN 'Urgencias'
			WHEN 2 THEN 'Hospitalizacion'
			WHEN 3 THEN 'Apoyo Dx'
			WHEN 4 THEN 'Apoyo Terapeutico'
			WHEN 5 THEN 'Unidades de Cuidado Intensivo Adulto'
			WHEN 6 THEN 'Unidades de Cuidado Intermedio Adulto'
			WHEN 7 THEN 'Unidades de Cuidado Intensivo Pediatrica'
			WHEN 8 THEN 'Unidades de Cuidado Intermedio Pediatrica'
			WHEN 9 THEN 'Unidades de Cuidado Intensivo Neonatal'
			WHEN 10 THEN 'Unidades de Cuidado Intermedio Neonatal'
			WHEN 11 THEN 'Unidades de Cuidado Basico Neonatal'
			WHEN 12 THEN 'Unidad Renal'
			WHEN 13 THEN 'Unidad Oncologica Quimioterapia'
			WHEN 14 THEN 'Unidad Medicina Nuclear'
			WHEN 15 THEN 'Consulta Externa'
			WHEN 16 THEN 'Unidad Mental'
			WHEN 17 THEN 'Unidad de Quemados'
			WHEN 18 THEN 'Unidad de Cuidado Paliativo'
			WHEN 19 THEN 'Cirugia'
			WHEN 20 THEN 'Laboratorio'
			WHEN 21 THEN 'Cardiologia No Invasiva'
			WHEN 22 THEN 'Cardiologia Invasiva'
			WHEN 23 THEN 'Gineco-Obstetricia'
			WHEN 24 THEN 'Consulta Externa - Gineco-Obstetricia'
			WHEN 30 THEN 'Otras'
			WHEN 31 THEN 'Consulta Prioritaria'
			WHEN 32 THEN 'Atención domiciliaria'
			WHEN 33 THEN 'Radioterapia'
			WHEN 34 THEN 'Braquiterapia'
			WHEN 35 THEN 'Hemodinamia'
		END AS NombreTipoUnidadFuncional
	   ,RTRIM(A.UFUCODIGO) + ' - ' + RTRIM(A.UFUDESCRI) AS CodigoDescripcion
	FROM dbo.INUNIFUNC A
	INNER JOIN (SELECT
			PKVALORCO
		   ,CAST(1 AS BIT) AS Valor
		   ,'AutUsuario' AS Permiso
		FROM dbo.INAUTORIU
		WHERE PKTIPOCON = '2'
		AND CODUSUARI = @Usuario
		UNION SELECT
			PKVALORCO
		   ,CAST(1 AS BIT) AS Valor
		   ,'AutGrupo' AS Permiso
		FROM dbo.INAUTORIG
		WHERE PKTIPOCON = '2'
		AND CODGRUPOU = @Grupo) AS B
		ON A.UFUCODIGO = B.PKVALORCO
	INNER JOIN dbo.INCENUNFU C
		ON A.UFUCODIGO = C.UFUCODIGO
			AND C.CODCENATE = @CentroAtencion
END