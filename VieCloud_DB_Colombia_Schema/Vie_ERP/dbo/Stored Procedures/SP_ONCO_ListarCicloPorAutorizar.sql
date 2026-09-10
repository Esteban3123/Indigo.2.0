

CREATE PROCEDURE [dbo].[SP_ONCO_ListarCicloPorAutorizar]
(
@Paciente Varchar(25)
)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
			TMP.IdEsquema AS 'Id Esquema', Rtrim(S.Description) AS 'Nombre Esquema', Rtrim(A.CICLO) as 'Ciclo',Rtrim(A.SEMANA) as 'Semana',Rtrim(A.DIA) As 'Dia',A.ESTADOCICLO as 'Estado Ciclo',A.CICLO As 'Ciclo Actual',B.CICLOS AS 'Total Ciclos',
								  CAST(NULL AS INT) AS 'Autorizar',A.CICLO + 1 As 'Ciclo Autorizar', TMP.idUltimociclo 
			FROM (
						select distinct O.ID as IdEsquema, 
							  (SELECT TOP 1 X.ID   From [EHR].[HCQUICICLOS] x WHERE x.IDHCQUIORDENC =O.ID   AND x.CICLO = O.CICLOACTUAL Order by SEMANA desc, DIA desc  , CICLO DESC  ) AS idUltimociclo,
							  (SELECT TOP 1 X.ESTADOCICLO  From [EHR].[HCQUICICLOS] x WHERE x.IDHCQUIORDENC =O.ID  AND x.CICLO = O.CICLOACTUAL  Order by SEMANA desc, DIA desc  , CICLO DESC  ) AS EstadoUltimoCiclo,
							  (SELECT TOP 1 X.CICLO From [EHR].[HCQUICICLOS] x WHERE x.IDHCQUIORDENC =O.ID   AND x.CICLO = O.CICLOACTUAL  Order by SEMANA desc, DIA desc  , CICLO DESC  ) AS NumeroUltimoCiclo   									       
						from EHR.[HCQUIORDENC] O 
			 ) as TMP  	 INNER JOIN [EHR].[HCQUICICLOS] A with(nolock) on tmp.idUltimociclo = A.ID
						  INNER JOIN [EHR].[HCQUIORDENC] B with(nolock) on A.IDHCQUIORDENC  = B.ID 
						  INNER JOIN [EHR].OncologicalSchemes S with(nolock) on S.Id = B.IDOncologicalSchemes  
			  WHERE TMP.EstadoUltimoCiclo  = 2 and B.ESTADO IN (1,2,3) And b.IPCODPACI = @Paciente  And  A.CICLO <  B.CICLOS
			
					
	
	/*
	select * from (
			Select Top 1 A.IDHCQUIORDENC AS 'Id Esquema', Rtrim(C.Description) AS 'Nombre Esquema', Rtrim(A.CICLO) as 'Ciclo',Rtrim(A.SEMANA) as 'Semana',Rtrim(A.DIA) As 'Dia',A.ESTADOCICLO as 'Estado Ciclo',A.CICLO As 'Ciclo Actual',B.CICLOS AS 'Total Ciclos',
						  CAST(NULL AS INT) AS 'Autorizar',A.CICLO + 1 As 'Ciclo Autorizar'
				   From [EHR].[HCQUICICLOS] A 
					INNER JOIN [EHR].[HCQUIORDENC] B on A.IDHCQUIORDENC = b.ID 
					INNER JOIN [EHR].OncologicalSchemes C on C.Id = b.IDOncologicalSchemes  
			where B.ESTADO IN (1,2,3) And b.IPCODPACI = @Paciente  And  A.CICLO <  B.CICLOS
			Order by SEMANA desc, DIA desc  , CICLO DESC
		) as TMP where TMP.[Estado Ciclo] = 2*/
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los ciclos de quimioterapia pendientes de autorización para un paciente oncológico, identificado por su cédula o código de paciente. Para cada esquema de tratamiento activo del paciente, determina el último ciclo ejecutado consultando los ciclos registrados en HCQUICICLOS y verifica que su estado sea ''completado'' (estado 2), lo que indica que el siguiente ciclo ya puede ser autorizado. Retorna información del esquema oncológico (nombre del protocolo desde OncologicalSchemes), el ciclo actual, semana, día, estado, y calcula cuál es el próximo ciclo a autorizar, siempre que no se haya superado el total de ciclos definidos para la orden. Se usa en el módulo de oncología para que el autorizador médico o administrativo identifique qué pacientes tienen ciclos de quimioterapia listos para aprobar.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ONCO_ListarCicloPorAutorizar';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ONCO_ListarCicloPorAutorizar';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los ciclos de quimioterapia pendientes de autorizar para un paciente, mostrando el último ciclo registrado y el siguiente ciclo a autorizar según el esquema oncológico vigente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarCicloPorAutorizar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe tener al menos una orden oncológica registrada; Debe existir al menos un ciclo asociado a la orden con valor de CicloActual coincidente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarCicloPorAutorizar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El ''Ciclo Autorizar'' siempre es el ciclo actual + 1; Solo se consideran órdenes oncológicas con estado 1, 2 o 3; Solo se consideran ciclos cuyo último estado es 2 (presumiblemente ''finalizado/listo para siguiente''); Nunca devuelve ciclos cuando ya se alcanzó el total de ciclos del esquema; El último ciclo se determina por mayor SEMANA, luego mayor DIA, luego mayor CICLO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarCicloPorAutorizar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Esquema oncológico; Ciclo de quimioterapia; Semana de tratamiento; Día de tratamiento; Orden oncológica; Autorización de ciclo', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarCicloPorAutorizar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve filas solo cuando el último ciclo del paciente tiene EstadoCiclo=2, la orden está en Estado IN (1,2,3) y el ciclo actual es menor que el total de ciclos del esquema (A.CICLO < B.CICLOS)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarCicloPorAutorizar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si EstadoUltimoCiclo = 2 AND Estado de orden IN (1,2,3) AND CICLO < CICLOS → Incluye el registro como ciclo por autorizar, calculando ''Ciclo Autorizar'' = CICLO + 1 else No se incluye en el resultado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarCicloPorAutorizar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'EHR.HCQUICICLOS; EHR.HCQUIORDENC; EHR.OncologicalSchemes', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarCicloPorAutorizar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarCicloPorAutorizar';
-- GO
