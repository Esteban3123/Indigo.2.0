CREATE PROCEDURE [dbo].[SP_ENF_ListarPlanesEnfermeriaHistoriaFinal](
@Ingreso char(10)
,@Paciente varchar(20)
)
AS
BEGIN	
	SET NOCOUNT ON;

-- Primera consulta - trae todos los planes de enfermeria activos al paciente que se refleja en la HISTORIA INICIAL enfermeria
-- se valida que no esten dentro de HCCTRACTP para mostrarlos en la Historia final
	 SELECT 
		DISTINCT
			M.ID AS IdControl,
			P.IDPLANVAL AS IdPlan,
			M.FECHACREA AS 'Fecha Registro',
			K.NOMUSUARI   AS Creador,
			O.IDINTACT,
			M.CODPROSAL,
			0 AS Tipo,
			rtrim(A.CODPLANCENF) as Codigo, 
			Rtrim(A.NOMPLANCENF) as Nombre,
			RTRIM(C.NOMBDIAGENF) AS Diagnostico,
			RTRIM(J.NOMINTPCE	) AS Intervencion,
			RTRIM(ltrim(REPLACE(L.NOMACTPCE, CHAR(9), ''))) AS Actividades,
			'' AS Descripcion,
			'' AS Fecha,
			CAST(0 As bit) As Realizada,  
			CAST(0 As bit) As NoRealizada,  
			CAST(0 As bit) As NoAplicaTurno,
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
					AND M.ID NOT IN (SELECT distinct IdHCPCECONTROL FROM .dbo.HCCTRACTP  WHERE NUMINGRES = @Ingreso AND IdHCPCECONTROL is not null)
	
UNION ALL

-- Segunda consulta, se traen el plan completo del paciente para la HISTORIA FINAL enfermeria
-- pero con las actividades guardados en HCCTRACTP que se guardan cuando se realiza la historia inicial
	SELECT 
		DISTINCT
			M.ID AS IdControl,
			P.IDPLANVAL AS IdPlan,
			T.FECREGIST as 'Fecha Registro' ,
			K.NOMMEDICO   AS Creador,
			T.IDINTACT,
			M.CODPROSAL,
			0 AS Tipo,
			rtrim(A.CODPLANCENF) as Codigo, 
			Rtrim(A.NOMPLANCENF) as Nombre,
			RTRIM(C.NOMBDIAGENF) AS Diagnostico,
			RTRIM(J.NOMINTPCE	) AS Intervencion,
			RTRIM(ltrim(REPLACE(T.ASUACTPEN, CHAR(9), ''))) AS Actividades,
			Rtrim(T.COMGENACT) As Descripcion,

			Case  WHEN T.IDINTACT Is NULL THEN  FECREGIST  
			Else( Select AA.FECHACREA 
					From .dbo.HCPCECONTROL AA with (nolock)   
						INNER Join .dbo.HCPCEPLANVAL BB with (nolock) ON AA.ID = BB.IDPCECONTROL  
						INNER Join .dbo.HCPCEVALDIAG CC with (nolock) ON CC.IDPLANVAL = BB.IDPLANVAL   
						INNER Join .dbo.HCPCEDIAGINT DD with (nolock) ON DD.IDVALDIAG = CC.IDVALDIAG   
						INNER Join .dbo.HCPCEINTACT EE with (nolock) ON EE.IDDIAGINT = DD.IDDIAGINT  
					WHERE EE.IDINTACT = T.IDINTACT) End AS 'Fecha',  

			CASE T.ESTACTACT WHEN 3 THEN 'True' ELSE 'False' END AS 'Realizada',
			CASE T.ESTACTACT WHEN 7 THEN 'True' ELSE 'False' END AS 'NoRealizada',
			CASE T.ESTACTACT WHEN 8 THEN 'True' ELSE 'False' END AS 'NoAplicaTurno',
			CAST('' AS varchar(25)) AS POSICION
	 FROM .dbo.HCCTRACTP T 
			INNER JOIN .dbo.HCPCECONTROL M ON T.IdHCPCECONTROL = M.ID
			INNER JOIN .dbo.HCPLANCUIENF A ON M.CODPLANCENF = A.CODPLANCENF
			INNER JOIN .dbo.HCPCEPLANVAL P ON M.ID = P.IDPCECONTROL
			INNER JOIN .dbo.HCPCEVALDIAG N ON N.IDPLANVAL = P.IDPLANVAL AND M.ID = N.IDHCPCECONTROL 
			INNER JOIN .dbo.HCDIAGENF C On N.CODDIAGENF = C.CODDIAGENF 	
			INNER JOIN .dbo.HCPCEDIAGINT I ON N.IDVALDIAG = I.IDVALDIAG AND M.ID = I.IDHCPCECONTROL 
			INNER JOIN .dbo.HCINTERVENPCE J on I.CODINTPCE = J.CODINTPCE
			INNER JOIN .dbo.HCPCEINTACT O ON O.IDDIAGINT = I.IDDIAGINT AND M.ID = O.IDHCPCECONTROL AND T.IDINTACT = O.IDINTACT
			INNER JOIN .dbo.INPROFSAL As K With (nolock) On T.CODPROSAL = K.CODPROSAL  
	Where  T.NUMINGRES = @Ingreso AND M.ESTADO = 1 And T.Identifier = (SELECT top 1 Identifier From .dbo.HCCTRACTP with (nolock) Where NUMINGRES =@Ingreso ORDER BY FECREGIST DESC)

UNION ALL

-- Tercera consulta, se traen las actividades agregadas sin padre desde la HISTORIA INICIAL, solo se registra la actividad con su IDINTACT null
	SELECT 
			0 AS IdControl,
			0  AS IdPlan,
			A.FECREGIST AS 'Fecha Registro',
			RTRIM(K.NOMMEDICO) AS Creador,
			A.IDINTACT,
			K.CODPROSAL,
			'' AS Tipo,
			'No aplica' AS Codigo,
			'Actividades adicionales' AS Nombre,
			'No aplica' AS Diagnostico,
			'No aplica' AS Intervencion,
			RTRIM(ltrim(REPLACE(A.ASUACTPEN, CHAR(9), ''))) AS Actividades,
			RTRIM(A.COMGENACT) AS Descripcion,
			''  AS Fecha,
			CASE A.ESTACTACT WHEN 3 THEN 'True' ELSE 'False' END AS 'Realizada',
			CASE A.ESTACTACT WHEN 7 THEN 'True' ELSE 'False' END AS 'NoRealizada',
			CASE A.ESTACTACT WHEN 8 THEN 'True' ELSE 'False' END AS 'NoAplicaTurno',
			CAST('' AS varchar(25)) AS POSICION
	FROM .dbo.HCCTRACTP  A
	INNER Join .dbo.INPROFSAL As K With (nolock) On A.CODPROSAL = K.CODPROSAL 
	WHERE NUMINGRES =@Ingreso AND IDINTACT IS NULL AND A.ESTACTACT IN (1,7,8)
	AND A.Identifier = (SELECT top 1 Identifier From .dbo.HCCTRACTP with (nolock) Where NUMINGRES = @Ingreso ORDER BY FECREGIST DESC)

	ORDER BY Intervencion ASC

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todos los planes de cuidado de enfermería registrados en la historia clínica final de un paciente, identificado por su número de ingreso. Combina tres fuentes de información: los planes activos que aún no han sido ejecutados (historia inicial pendiente), las actividades ya realizadas y registradas formalmente en el seguimiento de enfermería (historia final con trazabilidad de turno), y las actividades adicionales ingresadas sin asociación a un plan específico. Para cada registro devuelve el plan de cuidado, el diagnóstico de enfermería, la intervención, la actividad, el profesional responsable, la fecha y el estado de ejecución de la actividad (realizada, no realizada o no aplica turno), integrando catálogos de planes, diagnósticos, objetivos e intervenciones de enfermería de la historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ENF_ListarPlanesEnfermeriaHistoriaFinal';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ENF_ListarPlanesEnfermeriaHistoriaFinal';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida y lista para la Historia Final de Enfermería los planes de cuidado activos del paciente, sus actividades trazadas en el último registro de seguimiento y las actividades adicionales sin plan padre.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ENF_ListarPlanesEnfermeriaHistoriaFinal';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El ingreso debe existir y tener planes de cuidado de enfermería (HCPCECONTROL) en estado activo (ESTADO=1).; Para las secciones 2 y 3 debe existir al menos un registro en HCCTRACTP para el ingreso, ya que se usa el último Identifier por FECREGIST DESC.; Las relaciones plan→diagnóstico→intervención→actividad deben estar completas en HCPCE* para aparecer en la primera consulta.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ENF_ListarPlanesEnfermeriaHistoriaFinal';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran controles de enfermería con ESTADO=1 (activos).; Para las secciones de seguimiento siempre se toma el último Identifier de HCCTRACTP del ingreso (ORDER BY FECREGIST DESC, TOP 1), garantizando una sola tanda de trazabilidad.; Un plan no se duplica entre la sección inicial y la trazada: si ya tiene registro en HCCTRACTP, sale por la segunda consulta, no por la primera.; Las actividades adicionales solo provienen de estados 1, 7 u 8 (pendiente, no realizada, no aplica turno); el estado 3 (realizada) no se incluye como adicional.; Los textos de actividad y descripción se limpian de tabuladores (CHAR(9)) y espacios.; La primera consulta siempre devuelve estados de Realizada/NoRealizada/NoAplicaTurno en false (bit 0) por ser planes aún no trazados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ENF_ListarPlanesEnfermeriaHistoriaFinal';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Plan de cuidado de enfermería; Diagnóstico de enfermería; Intervención de enfermería (PCE); Actividad de enfermería; Historia inicial de enfermería; Historia final de enfermería; Trazabilidad de actividades por turno; Ingreso del paciente; Profesional de la salud / Usuario creador; Estados de actividad: realizada, no realizada, no aplica turno', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ENF_ListarPlanesEnfermeriaHistoriaFinal';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Devuelve unión de tres conjuntos: (1) planes activos del ingreso aún no trazados en HCCTRACTP, (2) actividades del último Identifier de HCCTRACTP vinculadas a un plan, (3) actividades adicionales del último Identifier sin IDINTACT padre, ordenadas por Intervencion ASC.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ENF_ListarPlanesEnfermeriaHistoriaFinal';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si M.ID NOT IN (SELECT IdHCPCECONTROL FROM HCCTRACTP WHERE NUMINGRES=@Ingreso AND IdHCPCECONTROL IS NOT NULL) → Se incluye el plan en la primera consulta (Historia Inicial sin trazabilidad aún registrada). else El plan se excluye de la primera consulta y aparece a través de la segunda con sus actividades trazadas.; si T.IDINTACT IS NULL → La fecha mostrada es T.FECREGIST. else La fecha se obtiene de HCPCECONTROL.FECHACREA del plan que originó la actividad (vía joins HCPCEPLANVAL→HCPCEVALDIAG→HCPCEDIAGINT→HCPCEINTACT).; si T.ESTACTACT = 3 → La actividad se marca como Realizada=True. else Realizada=False.; si T.ESTACTACT = 7 → La actividad se marca como NoRealizada=True. else NoRealizada=False.; si T.ESTACTACT = 8 → La actividad se marca como NoAplicaTurno=True. else NoAplicaTurno=False.; si IDINTACT IS NULL AND ESTACTACT IN (1,7,8) → Se incluye en la tercera consulta como ''Actividades adicionales'' sin plan/diagnóstico/intervención asociados. else No se considera actividad adicional.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ENF_ListarPlanesEnfermeriaHistoriaFinal';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCPCECONTROL; dbo.HCPLANCUIENF; dbo.HCPCEPLANVAL; dbo.HCPCEVALDIAG; dbo.HCVALDIAGPCE; dbo.HCDIAGENF; dbo.HCPCEVALOBJ; dbo.HCOBJPCE; dbo.HCPCEDIAGINT; dbo.HCINTERVENPCE; dbo.HCPCEINTACT; dbo.HCACTIVIPCE; dbo.SEGusuaru; dbo.HCCTRACTP; dbo.INPROFSAL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ENF_ListarPlanesEnfermeriaHistoriaFinal';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ENF_ListarPlanesEnfermeriaHistoriaFinal';
-- GO
