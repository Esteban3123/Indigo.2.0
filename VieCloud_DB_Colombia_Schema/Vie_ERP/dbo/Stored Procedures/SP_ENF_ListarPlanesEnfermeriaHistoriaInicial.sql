
CREATE PROCEDURE [dbo].[SP_ENF_ListarPlanesEnfermeriaHistoriaInicial](
@Ingreso char(10)
,@Paciente varchar(20)
)
AS
BEGIN	
	SET NOCOUNT ON;
SELECT 
		DISTINCT
			M.ID AS IdControl,
			P.IDPLANVAL AS IdPlan,
			M.FECHACREA AS 'Fecha Registro',
			K.NOMUSUARI   AS Creador,
			O.IDINTACT,
			M.CODPROSAL,
			0 AS Tipo,
			rtrim(A.CODPLANCENF) AS Codigo, 
			Rtrim(A.NOMPLANCENF) AS Nombre,
			RTRIM(C.NOMBDIAGENF) AS Diagnostico,
			RTRIM(J.NOMINTPCE  ) AS Intervencion,
			LTRIM(RTRIM(REPLACE(L.NOMACTPCE, CHAR(9), ''))) AS Actividades,
			Cast(' ' as varchar(300)) AS Descripcion,
			CAST('' AS varchar(25)) AS POSICION
		FROM .dbo.HCPCECONTROL M
			INNER JOIN .dbo.HCPLANCUIENF A ON M.CODPLANCENF = A.CODPLANCENF
			INNER JOIN .dbo.HCPCEPLANVAL P ON M.ID = P.IDPCECONTROL
			INNER JOIN .dbo.HCPCEVALDIAG N ON N.IDPLANVAL = P.IDPLANVAL AND M.ID = N.IDHCPCECONTROL 
			INNER JOIN .dbo.HCVALDIAGPCE B ON M.CODPLANCENF = B.CODPLANCENF
			INNER JOIN .dbo.HCDIAGENF C On N.CODDIAGENF = C.CODDIAGENF 	
			INNER JOIN .dbo.HCPCEVALOBJ G ON M.ID = G.IDHCPCECONTROL
			INNER JOIN .dbo.HCOBJPCE H ON G.CODOBJPCE = H.CODOBJPCE
			INNER JOIN .dbo.HCPCEDIAGINT I ON N.IDVALDIAG = I.IDVALDIAG AND M.ID = I.IDHCPCECONTROL 
			INNER JOIN .dbo.HCINTERVENPCE J on I.CODINTPCE = J.CODINTPCE
			INNER JOIN .dbo.HCPCEINTACT O ON O.IDDIAGINT = I.IDDIAGINT AND M.ID = O.IDHCPCECONTROL 
			INNER JOIN .dbo.HCACTIVIPCE L ON L.CODACTPCE = O.CODACTPCE
			INNER JOIN .dbo.SEGusuaru K with (nolock) ON M.CODUSUCREA = K.CODUSUARI 
		WHERE M.NUMINGRES = @Ingreso AND M.ESTADO IN(1) 
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los planes de cuidado de enfermería registrados en la historia clínica inicial de un paciente, dado su número de ingreso. Para cada plan activo, retorna el nombre del plan de cuidado, los diagnósticos de enfermería asociados, las intervenciones clínicas vinculadas a esos diagnósticos y las actividades específicas de cada intervención, junto con el profesional que lo creó y la fecha de registro. Compone información del catálogo de planes (HCPLANCUIENF), los diagnósticos de enfermería (HCDIAGENF), los objetivos clínicos (HCOBJPCE), las intervenciones (HCINTERVENPCE) y las actividades (HCACTIVIPCE), articulados a través del control de enfermería (HCPCECONTROL) y sus tablas de valoración intermedias. Se usa para visualizar el plan de atención de enfermería completo de un paciente hospitalizado en su historia clínica inicial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ENF_ListarPlanesEnfermeriaHistoriaInicial';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ENF_ListarPlanesEnfermeriaHistoriaInicial';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los planes de cuidado de enfermería activos del ingreso de un paciente, expandidos hasta el detalle de diagnóstico, intervención y actividad, para visualización en la historia clínica inicial.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ENF_ListarPlanesEnfermeriaHistoriaInicial';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos un control de plan de cuidado de enfermería asociado al ingreso recibido y con estado activo (=1).; Cada control debe tener cadena completa: plan valorado, diagnóstico valorado, objetivo valorado, intervención y actividad asociadas.; El usuario creador del control debe existir en el catálogo de usuarios de seguridad.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ENF_ListarPlanesEnfermeriaHistoriaInicial';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen controles del Plan de Cuidado de Enfermería con ESTADO = 1 (activos/vigentes).; El listado se filtra por número de ingreso del paciente.; Se retornan únicamente combinaciones donde existe trazabilidad completa: control → plan valorado → diagnóstico valorado → intervención → actividad.; Se eliminan duplicados mediante DISTINCT en la proyección.; El nombre de actividad se devuelve sin tabulaciones (CHAR(9)) ni espacios laterales.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ENF_ListarPlanesEnfermeriaHistoriaInicial';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Plan de cuidado de enfermería; Diagnóstico de enfermería; Intervención de enfermería (PCE); Actividades de enfermería; Objetivo del plan de cuidado; Ingreso del paciente; Historia clínica inicial; Usuario creador del registro', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ENF_ListarPlanesEnfermeriaHistoriaInicial';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCPCECONTROL: Cuando M.NUMINGRES = @Ingreso y M.ESTADO IN (1), se retorna el conjunto de planes de enfermería con sus diagnósticos, intervenciones y actividades asociadas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ENF_ListarPlanesEnfermeriaHistoriaInicial';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCPCECONTROL; dbo.HCPLANCUIENF; dbo.HCPCEPLANVAL; dbo.HCPCEVALDIAG; dbo.HCVALDIAGPCE; dbo.HCDIAGENF; dbo.HCPCEVALOBJ; dbo.HCOBJPCE; dbo.HCPCEDIAGINT; dbo.HCINTERVENPCE; dbo.HCPCEINTACT; dbo.HCACTIVIPCE; dbo.SEGusuaru', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ENF_ListarPlanesEnfermeriaHistoriaInicial';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ENF_ListarPlanesEnfermeriaHistoriaInicial';
-- GO
