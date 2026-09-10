
CREATE PROCEDURE [dbo].[SP_ONCO_ListarRadioterapiasActividadesReciente]
(
@Identificacion Varchar(25)
)
AS
BEGIN
	SET NOCOUNT ON;

select 
			
			A.ID, ES.ID AS 'IDESQUEMA',A.CODCENATE, ORD.NUMINGRES ,
			rtrim(F.CODPROSAL) as CODPROSAL,
			rtrim(A.CODSERIPS) as CODSERIPS,
			Rtrim(A.IPCODPACI) As Identificacion, A.FECHAORDEN,
			Rtrim(IPNOMCOMP) As NombrePaciente,
			Rtrim(E.CODSERIPS) +' - '+ Rtrim(DESSERIPS) As Procedimiento,
			Rtrim(CE.NOMCENATE ) as CentroAtencion,
			Rtrim(F.NOMMEDICO) as Profesional,
			Rtrim(D.CODDIAGNO) + ' - ' + Rtrim(D.NOMDIAGNO ) as Diagnostico,
			Rtrim(D.CODDIAGNO) as CodigoDiagnostico,
			Rtrim(ESP.DESESPECI) as Especialidad,
			CASE ORD.MANEXTPRO when 1 then 'Ambulatoria' else 'Hospitalario' END as OrigenOrden,
			E.SERIPSDASH as 'TipoServicio',
			Case 
			WHEN  A.ESTADO = 3 Then 'Simulación Realizada' 
			when (A.ESTADO = 4 and ES.ESTADO = 1)  Then 'Planeación Por Realizar' 
			when (A.ESTADO = 4 and ES.ESTADO = 2)  Then 'Planeación Confirmada' 
			when (A.ESTADO = 4 and ES.ESTADO = 3)  Then 'Esquema Iniciado' 
			when (A.ESTADO = 5)  Then 'Finalizado' 
			when (A.ESTADO = 6 or ES.ESTADO = 5 )  Then 'Anulado' 
			when (A.ESTADO = 7)  Then 'Completado' 
			End as 'Estado Esquema',
			ES.DOSTOTAL,ES.NUMSESION, ES.DOSISPORSESION,ORD.NUMINGRES
		from HCRADORDEN A 
		   left JOIN HCRADESQUEMAS ES with(nolock) ON A.ID = ES.IDHCRADORDEN
			INNER JOIN HCORDPRON ORD with(nolock) ON A.IDHCORDPRON = ORD.AUTO
			INNER JOIN INCUPSIPS  E with(nolock) ON A.CODSERIPS = E.CODSERIPS AND E.SERIPSDASH  = 6 --Radioterapia Externa
			INNER JOIN INPACIENT B ON A.IPCODPACI = B.IPCODPACI 
			INNER JOIN ADCENATEN CE with(nolock) ON CE.CODCENATE = A.CODCENATE
			INNER JOIN INPROFSAL   F with(nolock) ON A.CODPROSAL  = F.CODPROSAL
			INNER JOIN INDIAGNOS  D with(nolock) ON A.CODDIAGNO = D.CODDIAGNO
			INNER JOIN INESPECIA ESP with(nolock) ON ESP.CODESPECI  = A.CODESPECI
		WHERE A.IPCODPACI = @Identificacion and A.ESTADO IN (3,4,5,6,7)  
     
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista las órdenes de radioterapia externa y sus esquemas de tratamiento asociados para un paciente específico, identificado por su cédula o documento. Combina información de órdenes médicas (HCRADORDEN), esquemas radioterápicos (HCRADESQUEMAS), el catálogo de servicios CUPS filtrado exclusivamente a radioterapia externa, datos del paciente, centro de atención, profesional tratante, diagnóstico CIE-10 y especialidad médica. Para cada orden activa (en estados de simulación realizada, planeación, en curso, finalizado, anulado o completado) devuelve el número de ingreso, dosis total, número de sesiones, dosis por sesión, origen de la orden (ambulatoria u hospitalaria) y el estado descriptivo del esquema. Se usa en el módulo de oncología para que el personal clínico consulte el historial reciente de radioterapias de un paciente y haga seguimiento al ciclo de vida de cada esquema de tratamiento radioterápico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ONCO_ListarRadioterapiasActividadesReciente';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ONCO_ListarRadioterapiasActividadesReciente';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las órdenes de radioterapia externa recientes de un paciente con su esquema asociado, mostrando estado consolidado, diagnóstico, profesional, centro de atención y dosificación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarRadioterapiasActividadesReciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe proveerse la identificación del paciente.; Las órdenes deben estar vinculadas a un servicio cuyo SERIPSDASH = 6 (Radioterapia Externa).; Solo se consideran órdenes con estado en (3,4,5,6,7).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarRadioterapiasActividadesReciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna servicios de Radioterapia Externa (SERIPSDASH = 6).; Excluye órdenes en estados distintos de 3,4,5,6,7 (p. ej. estado 1 o 2).; El estado mostrado depende conjuntamente del estado de la orden y del estado del esquema.; La consulta no modifica datos; es de solo lectura con NOLOCK en la mayoría de joins.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarRadioterapiasActividadesReciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Orden de radioterapia; Esquema de radioterapia; Radioterapia externa; Simulación; Planeación; Sesiones y dosis (dosis total, dosis por sesión); Diagnóstico; Especialidad médica; Centro de atención; Profesional de salud; Origen ambulatorio/hospitalario', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarRadioterapiasActividadesReciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve órdenes de radioterapia del paciente filtradas por IPCODPACI = @Identificacion y ESTADO IN (3,4,5,6,7), unidas al esquema cuando exista (LEFT JOIN).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarRadioterapiasActividadesReciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si A.ESTADO = 3 → Estado del esquema = ''Simulación Realizada''; si A.ESTADO = 4 AND ES.ESTADO = 1 → Estado del esquema = ''Planeación Por Realizar''; si A.ESTADO = 4 AND ES.ESTADO = 2 → Estado del esquema = ''Planeación Confirmada''; si A.ESTADO = 4 AND ES.ESTADO = 3 → Estado del esquema = ''Esquema Iniciado''; si A.ESTADO = 5 → Estado del esquema = ''Finalizado''; si A.ESTADO = 6 OR ES.ESTADO = 5 → Estado del esquema = ''Anulado''; si A.ESTADO = 7 → Estado del esquema = ''Completado''; si ORD.MANEXTPRO = 1 → Origen de la orden se clasifica como ''Ambulatoria'' else Origen de la orden se clasifica como ''Hospitalario''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarRadioterapiasActividadesReciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCRADORDEN; dbo.HCRADESQUEMAS; dbo.HCORDPRON; dbo.INCUPSIPS; dbo.INPACIENT; dbo.ADCENATEN; dbo.INPROFSAL; dbo.INDIAGNOS; dbo.INESPECIA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarRadioterapiasActividadesReciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarRadioterapiasActividadesReciente';
-- GO
