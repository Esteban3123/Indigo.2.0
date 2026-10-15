
CREATE PROCEDURE [dbo].[SPMOV_ListarUnidadesFuncionalesPorCentro]
(
	@Usuario nvarchar(50),
	@CODCENATE nvarchar(50)
)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT  RTRIM(C.CODCENATE) as CODCENATE,
	    RTRIM(A.UFUCODIGO) as Codigo,
		RTRIM(A.UFUDESCRI) as 'UnidadFuncional',
		RTRIM(A.UFUCODIGO)+' - '+RTRIM(A.UFUDESCRI) AS CodigoDescripcion,
		CASE UFUTIPUNI
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
			WHEN 13 THEN 'Unidad Oncologica' 
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
		END AS TipoUnidadFuncional
FROM	INUNIFUNC A 
		INNER JOIN (SELECT PKVALORCO FROM INAUTORIU WHERE PKTIPOCON='2' AND CODUSUARI = @Usuario
					UNION 
					SELECT PKVALORCO FROM INAUTORIG WHERE PKTIPOCON='2' AND CODGRUPOU  in (select codgrupou from dbo.SEGusuaru where CODUSUARI = @Usuario)) AS B ON A.UFUCODIGO=B.PKVALORCO 
		INNER JOIN INCENUNFU C ON A.UFUCODIGO = C.UFUCODIGO AND C.CODCENATE = @CODCENATE
end