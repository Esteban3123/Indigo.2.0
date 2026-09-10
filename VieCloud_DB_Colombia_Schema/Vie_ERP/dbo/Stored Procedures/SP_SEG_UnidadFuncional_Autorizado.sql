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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Retorna las unidades funcionales (servicios, salas o áreas de atención) a las que un usuario o grupo tiene acceso autorizado dentro de un centro de atención específico. Cruza el catálogo maestro de unidades funcionales (INUNIFUNC) con las autorizaciones por usuario (INAUTORIU) y por grupo (INAUTORIG) para el tipo de contrato ''2'', filtrando además las unidades que pertenecen al centro de atención indicado según la tabla INCENUNFU. Por cada unidad devuelve su código, nombre, tipo de unidad (urgencias, hospitalización, UCI, consulta externa, laboratorio, cirugía, etc.) y la descripción combinada código-nombre, útil para poblar listas desplegables de seguridad y control de acceso en la interfaz clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_SEG_UnidadFuncional_Autorizado';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_SEG_UnidadFuncional_Autorizado';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las unidades funcionales habilitadas en un centro de atención para las que un usuario o su grupo tienen permiso de acceso, con su tipo descriptivo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SEG_UnidadFuncional_Autorizado';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el usuario y/o grupo con autorizaciones registradas con PKTIPOCON=''2'' (tipo de control correspondiente a unidad funcional).; El centro de atención indicado debe tener unidades funcionales asociadas en INCENUNFU.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SEG_UnidadFuncional_Autorizado';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Sólo considera autorizaciones con PKTIPOCON=''2'' (control sobre unidad funcional).; La autorización puede provenir indistintamente del usuario (INAUTORIU) o del grupo (INAUTORIG); se unifican vía UNION.; Sólo se devuelven unidades funcionales que estén configuradas en el centro de atención solicitado.; Los códigos y descripciones se devuelven sin espacios a la derecha (RTRIM).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SEG_UnidadFuncional_Autorizado';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Unidad funcional; Tipo de unidad funcional (Urgencias, Hospitalización, UCI, Consulta Externa, Cirugía, Laboratorio, Radioterapia, Hemodinamia, etc.); Autorización por usuario; Autorización por grupo; Centro de atención; Permisos de acceso', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SEG_UnidadFuncional_Autorizado';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Retorna unidades funcionales (código, descripción, tipo y nombre del tipo) cuyo código aparezca autorizado para el usuario en INAUTORIU o para el grupo en INAUTORIG (PKTIPOCON=''2'') y que estén vinculadas al centro de atención indicado en INCENUNFU.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SEG_UnidadFuncional_Autorizado';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si UFUTIPUNI según valor 1..35 → Se traduce a una etiqueta de tipo de unidad funcional (Urgencias, Hospitalización, UCI Adulto, Consulta Externa, Cirugía, Radioterapia, etc.) else Si el valor no coincide con ninguno listado, NombreTipoUnidadFuncional queda en NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SEG_UnidadFuncional_Autorizado';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INUNIFUNC; dbo.INAUTORIU; dbo.INAUTORIG; dbo.INCENUNFU', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SEG_UnidadFuncional_Autorizado';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SEG_UnidadFuncional_Autorizado';
-- GO
