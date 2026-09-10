
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista las unidades funcionales (servicios o áreas de atención como Urgencias, Hospitalización, UCI, Consulta Externa, Laboratorio, Cirugía, entre otras) disponibles para un centro de atención específico, filtrando únicamente las que el usuario tiene autorizadas según sus permisos individuales o los permisos heredados de su grupo de seguridad. Combina el catálogo maestro de unidades funcionales (INUNIFUNC), la relación entre centros y unidades (INCENUNFU), y las tablas de autorizaciones por usuario y por grupo (INAUTORIU, INAUTORIG, SEGusuaru) para garantizar que cada usuario solo vea las unidades a las que tiene acceso dentro del centro indicado. Se usa típicamente en listas desplegables de selección de servicio o área de atención al momento de registrar ingresos, órdenes, agendamiento u otras operaciones asistenciales.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPMOV_ListarUnidadesFuncionalesPorCentro';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPMOV_ListarUnidadesFuncionalesPorCentro';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las unidades funcionales de un centro de atención sobre las que un usuario tiene autorización, ya sea directa o heredada por grupo, traduciendo el tipo de unidad a su descripción.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarUnidadesFuncionalesPorCentro';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el usuario en SEGusuaru para resolver autorizaciones por grupo; Deben existir registros de autorización (INAUTORIU/INAUTORIG) con PKTIPOCON=''2'' para que el usuario obtenga resultados; El centro de atención debe existir en INCENUNFU asociado a unidades funcionales', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarUnidadesFuncionalesPorCentro';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se devuelven unidades funcionales sobre las que el usuario tiene autorización, ya sea directamente (INAUTORIU) o por pertenecer a un grupo autorizado (INAUTORIG vía SEGusuaru); El tipo de control de autorización siempre se filtra con PKTIPOCON=''2'' (autorización sobre unidades funcionales); Solo se listan unidades funcionales asociadas al centro de atención solicitado (INCENUNFU.CODCENATE); Los códigos y descripciones devueltos están sin espacios a la derecha (RTRIM)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarUnidadesFuncionalesPorCentro';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Unidad Funcional; Centro de Atención; Autorización por usuario; Autorización por grupo de usuarios; Tipo de unidad funcional (Urgencias, Hospitalización, UCI/UCIN, Consulta Externa, Cirugía, Laboratorio, Cardiología, Gineco-Obstetricia, Unidad Renal/Oncológica/Mental/Quemados/Paliativos, etc.)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarUnidadesFuncionalesPorCentro';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve unidades funcionales filtradas por: autorización del usuario (INAUTORIU PKTIPOCON=''2'') o de sus grupos (INAUTORIG PKTIPOCON=''2'' vía SEGusuaru), y por pertenencia al centro indicado (INCENUNFU.CODCENATE = parámetro)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarUnidadesFuncionalesPorCentro';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Valor de UFUTIPUNI (1..24, 30) → Traduce el código numérico a la descripción textual del tipo de unidad funcional (Urgencias, Hospitalización, UCI, Cirugía, Laboratorio, etc.) else Si el valor no coincide con ninguno de los listados, TipoUnidadFuncional queda en NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarUnidadesFuncionalesPorCentro';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INUNIFUNC; dbo.INAUTORIU; dbo.INAUTORIG; dbo.SEGusuaru; dbo.INCENUNFU', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarUnidadesFuncionalesPorCentro';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarUnidadesFuncionalesPorCentro';
-- GO
