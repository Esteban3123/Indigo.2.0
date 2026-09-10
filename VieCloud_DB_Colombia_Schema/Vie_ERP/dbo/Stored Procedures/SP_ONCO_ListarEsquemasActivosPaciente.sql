

CREATE PROCEDURE [dbo].[SP_ONCO_ListarEsquemasActivosPaciente]
(
@Paciente Varchar(25)
)
AS
BEGIN
	SET NOCOUNT ON;
	 
 
	select Rtrim(C.Description) +'   -    N° de Ciclos: '+ Rtrim(A.CICLOS) as 'Nombre Esquema',Rtrim(D.CODPRODUC) AS 'Codigo Producto', Rtrim(D.DESPRODUC) As 'Nombre Producto', convert(numeric(18,2),B.DOSISPROD) as 'Dosis', Rtrim(E.DESVIAADM) as 'Via Administracion',
	   CASE A.ESTADO  WHEN 1 Then 'Solicitado sin cita' when 2 Then 'Solicitado con cita' When 3 Then 'Esquema Iniciado' End as 'Estado Esquema',
	   Rtrim(F.NOMMEDICO) As 'Nombre Medico',A.NUMINGRES AS 'Ingreso',
	   b.TIPOFACTOR as 'Tipo Factor',B.INSTRUADMINIS as 'Instrucciones',B.CICLO,B.DIA As 'Dia'
	from [EHR].[HCORDQUIMIO] A
			INNER JOIN [EHR].[HCORDMEDICAM] B with(nolock) ON A.ID = B.IDHCORDQUIMIO and B.CICLO = A.CICLOACTUAL 
			INNER JOIN [EHR].Schemes C ON A.SchemesId = C.Id 
			INNER JOIN [dbo].IHLISTPRO D with(nolock) ON D.CODPRODUC = B.CODPRODUC 
			INNER JOIN [dbo].HCVIAADMI E with(nolock) ON E.CODVIAADM = b.CODVIAADM
			INNER JOIN [dbo].INPROFSAL F with(nolock) ON F.CODPROSAL = A.CODPROSAL
	Where A.IPCODPACI = @Paciente AND A.ESTADO IN (1,2,3) order by B.CICLO,B.DIA asc

end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todos los esquemas de quimioterapia activos de un paciente oncológico, identificado por su cédula o código de paciente. Combina las órdenes de quimioterapia vigentes (estados: solicitado sin cita, solicitado con cita o esquema iniciado) con el detalle de medicamentos del ciclo actual, mostrando por cada medicamento el nombre del esquema terapéutico, código y nombre del producto farmacéutico, dosis, vía de administración, instrucciones, tipo de factor, día del ciclo y número de ingreso. Incluye además el nombre del médico tratante obtenido del maestro de profesionales de la salud. Se utiliza en el módulo de oncología para que el equipo asistencial consulte rápidamente los protocolos de tratamiento en curso de un paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ONCO_ListarEsquemasActivosPaciente';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ONCO_ListarEsquemasActivosPaciente';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los esquemas de quimioterapia activos (solicitados o iniciados) de un paciente con sus medicamentos, dosis, vía de administración, médico tratante y ciclo/día correspondiente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarEsquemasActivosPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe existir y tener órdenes de quimioterapia registradas con estado 1, 2 o 3.; Cada orden debe tener un ciclo actual definido que coincida con medicamentos en el detalle.; Los medicamentos referenciados deben existir en el catálogo de productos, vías de administración y el médico en el maestro de profesionales.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarEsquemasActivosPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen esquemas activos (estados 1, 2 o 3); se excluyen otros estados (cancelados, finalizados, etc.).; Los medicamentos retornados pertenecen exclusivamente al ciclo actual del esquema, no a ciclos previos ni futuros.; El resultado se ordena ascendentemente por ciclo y por día dentro del ciclo.; La dosis se expone con precisión numérica de 18,2.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarEsquemasActivosPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Esquema de quimioterapia; Ciclo de tratamiento oncológico; Medicamento oncológico; Dosis; Vía de administración; Médico tratante; Paciente; Estado de orden (solicitado/iniciado); Tipo de factor; Instrucciones de administración', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarEsquemasActivosPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] EHR.HCORDQUIMIO: Devuelve solo órdenes de quimioterapia del paciente cuyo ESTADO esté en (1,2,3), traduciéndolo a ''Solicitado sin cita'', ''Solicitado con cita'' o ''Esquema Iniciado''.; [RETURN_RESULT] EHR.HCORDMEDICAM: Solo se incluyen medicamentos cuyo CICLO coincide con el CICLOACTUAL de la orden de quimioterapia.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarEsquemasActivosPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ESTADO de la orden de quimioterapia = 1 → Se etiqueta como ''Solicitado sin cita''; si ESTADO = 2 → Se etiqueta como ''Solicitado con cita''; si ESTADO = 3 → Se etiqueta como ''Esquema Iniciado''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarEsquemasActivosPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'EHR.HCORDQUIMIO; EHR.HCORDMEDICAM; EHR.Schemes; dbo.IHLISTPRO; dbo.HCVIAADMI; dbo.INPROFSAL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarEsquemasActivosPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarEsquemasActivosPaciente';
-- GO
