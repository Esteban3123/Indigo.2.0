

CREATE VIEW [Billing].[ViewRevenueControl]
AS

	SELECT rc.NUMINGRES AS [Key], AdmissionNumber = rc.NUMINGRES, PatientCode = rc.IPCODPACI, pt.IPNOMCOMP, IESTADOIN = RTRIM(ISNULL(i.IESTADOIN, ''))
	FROM dbo.ADINGRESO rc WITH(NOLOCK)
	LEFT JOIN dbo.[INPACIENT] pt WITH(NOLOCK) ON rc.IPCODPACI = pt.IPCODPACI
	LEFT JOIN dbo.INGRESOS i WITH(NOLOCK) ON i.NUMINGRES = rc.NUMINGRES
	WHERE RTRIM(ISNULL(i.IESTADOIN, '')) in ('','C')
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de control de ingresos con estado de facturación pendiente o sin factura. Consolida los episodios de admisión de pacientes (urgencias, hospitalizaciones, consulta externa) que aún no tienen factura emitida o cuyo estado de ingreso está en blanco o en estado ''C'', combinando datos del ingreso, la identificación del paciente y su nombre completo. Se usa en el módulo de facturación para identificar ingresos pendientes de facturar o en proceso, permitiendo hacer seguimiento al control de cartera y cierre de cuentas por atención prestada.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewRevenueControl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewRevenueControl';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los ingresos/admisiones de pacientes que están activos o cerrados (estado vacío o ''C'') junto con datos básicos del paciente, para control y seguimiento de ingresos facturables.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewRevenueControl';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen registros en ADINGRESO con NUMINGRES asociado.; El estado IESTADOIN en INGRESOS, una vez normalizado (RTRIM/ISNULL), debe ser '''' o ''C'' para que el ingreso sea visible.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewRevenueControl';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen ingresos cuyo estado normalizado sea cadena vacía o ''C''.; El estado IESTADOIN se entrega siempre normalizado con RTRIM e ISNULL a cadena vacía si es NULL.; La unión con paciente e ingresos es LEFT JOIN: un ingreso de ADINGRESO se conserva aunque no exista coincidencia en INPACIENT o INGRESOS (salvo por el filtro de estado).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewRevenueControl';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; ingreso/admisión; estado de ingreso; control de ingresos (facturación)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewRevenueControl';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.ViewRevenueControl: Devuelve un registro por cada ingreso (ADINGRESO) cuyo estado en INGRESOS, tras RTRIM(ISNULL(i.IESTADOIN,'''')), esté en ('''',''C'').', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewRevenueControl';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si RTRIM(ISNULL(i.IESTADOIN,'''')) IN ('''',''C'') → Incluye el ingreso en el resultado. else El ingreso se excluye del resultado.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewRevenueControl';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; dbo.INPACIENT; dbo.INGRESOS', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewRevenueControl';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewRevenueControl';
GO
