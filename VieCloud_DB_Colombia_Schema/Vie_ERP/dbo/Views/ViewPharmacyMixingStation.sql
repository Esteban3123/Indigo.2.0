

CREATE VIEW [dbo].[ViewPharmacyMixingStation]
AS
	SELECT	A.CODCONCEC,
			A.CODCONCEP,
			A.CODCONCES,
			A.FECHAORDE, 
			A.CODCENATE, 
			A.UFUCODIGO,
			A.CODCENCOS,
			A.IPCODPACI,
			A.NUMINGRES,
			A.NUMEFOLIO,
			A.IDETIPHIS,
			A.ORDTRANUE, 
			ISNULL(A.TIPOSOLICITUD, 0) TIPOSOLICITUD,
			A.MEDICAMENTOVALIDADO,
			A.CODBODEGA,
			A.CODPROSAL,
			ING.CODCAMACT,
			A.ORDESTADO,
			ISNULL(cg.ExtramuralPharmaceuticalDispensing,0) TIPOSOLICITUD1, 
			Cg.ExtramuralPharmaceuticalDispensing as PERMITEEXTRA,
			ISNULL((select top 1 1
			from dbo.HCFARMEPD hd
			WHERE hd.CODCONCEC =A.CODCONCEC AND hd.SENDTO IN(0,2) AND hd.CANPENPRO>0 
			),0) as RoutingMP
	FROM dbo.HCFARMEPC AS A WITH (NOLOCK)
	JOIN dbo.ADINGRESO as ING WITH (NOLOCK) ON A.NUMINGRES = ING.NUMINGRES
	LEFT JOIN Contract.CareGroup cg WITH (NOLOCK) ON ING.GENCAREGROUP = cg.Id
	WHERE (A.ORDESTADO = '1' AND ISNULL(A.ORDENQUIMIO, 0) = 0  AND A.IDAGEPROGQX IS NULL AND (ING.IESTADOIN NOT IN('F','C') OR A.TIPOSOLICITUD = 2 ))
UNION
	SELECT	A.CODCONCEC,
			A.CODCONCEP,
			A.CODCONCES,
			A.FECHAORDE, 
			A.CODCENATE, 
			A.UFUCODIGO,
			A.CODCENCOS,
			A.IPCODPACI,
			A.NUMINGRES,
			A.NUMEFOLIO,
			A.IDETIPHIS,
			A.ORDTRANUE, 
			ISNULL(A.TIPOSOLICITUD, 1) TIPOSOLICITUD,
			A.MEDICAMENTOVALIDADO,
			A.CODBODEGA,
			A.CODPROSAL,
			'' CODCAMACT,
			A.ORDESTADO,
			CAST(0 AS BIT) TIPOSOLICITUD1, 
			NULL as PERMITEEXTRA,
			ISNULL((select top 1 1
			from dbo.HCFARMEPD hd
			WHERE hd.CODCONCEC =A.CODCONCEC AND hd.SENDTO IN(0,2) AND hd.CANPENPRO>0 
			),0) as RoutingMP
	FROM dbo.HCFARMEPC AS A WITH (NOLOCK)
	JOIN dbo.ADINGRESO as ING WITH (NOLOCK) ON A.NUMINGRES = ING.NUMINGRES AND ING.TRATAESPECIA = 3 AND ING.IESTADOIN <> 'C'
	WHERE (A.ORDESTADO = '1' AND ISNULL(A.ORDENQUIMIO, 0) = 0 AND A.IDAGEPROGQX IS NULL AND A.TIPOSOLICITUD = 2)
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida las órdenes farmacéuticas activas pendientes de preparación en la estación de mezclas (farmacia intrahospitalaria), excluyendo órdenes de quimioterapia y programaciones quirúrgicas. Combina los encabezados de órdenes de medicamentos (HCFARMEPC) con los ingresos del paciente (ADINGRESO) y el grupo de atención del contrato (CareGroup) para determinar si el despacho es extramural y si la orden tiene pendientes de producción en el detalle farmacéutico (HCFARMEPD). Incluye dos escenarios mediante UNION: órdenes estándar de pacientes activos (incluyendo recién nacidos y tratamientos especiales) y órdenes de tipo extramural (TIPOSOLICITUD=2) para pacientes en tratamiento especial. Sirve al módulo de farmacia para listar las mezclas intravenosas u otras preparaciones magistrales que deben ser procesadas, indicando el profesional prescriptor, la bodega, la unidad funcional, el estado de la orden y si puede enrutarse al área de producción de mezclas (RoutingMP).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewPharmacyMixingStation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewPharmacyMixingStation';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone las órdenes farmacéuticas activas pendientes para la estación de mezclas, combinando órdenes de ingresos vigentes con órdenes especiales (tipo solicitud 2) aun cuando el ingreso esté finalizado o cancelado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewPharmacyMixingStation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La orden debe existir en HCFARMEPC con estado ''1'' (activa); La orden no debe ser de quimioterapia (ORDENQUIMIO = 0 o NULL); La orden no debe estar asociada a una agenda/programación quirúrgica (IDAGEPROGQX IS NULL); El ingreso del paciente debe existir en ADINGRESO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewPharmacyMixingStation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Nunca expone órdenes en estado distinto a ''1''; Nunca expone órdenes de quimioterapia (ORDENQUIMIO<>0); Nunca expone órdenes vinculadas a programación quirúrgica (IDAGEPROGQX no nulo); Las órdenes de ingresos finalizados o cancelados solo se exponen si son TIPOSOLICITUD=2; El indicador PERMITEEXTRA proviene del CareGroup del ingreso (ExtramuralPharmaceuticalDispensing); UNION elimina duplicados entre los dos bloques', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewPharmacyMixingStation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden de farmacia; Estación de mezclas farmacéuticas; Quimioterapia; Programación quirúrgica; Ingreso de paciente; Dispensación farmacéutica extramural; Grupo de atención (CareGroup); Tratamiento especial; Cantidad pendiente por procesar; Ruteo de medicamentos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewPharmacyMixingStation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ViewPharmacyMixingStation: Devuelve órdenes farmacéuticas con ORDESTADO=''1'', sin quimio y sin programación quirúrgica, cuyo ingreso no esté finalizado (''F'') ni cancelado (''C''), o que sean de tipo solicitud 2 (extramural/especial); [RETURN_RESULT] ViewPharmacyMixingStation: Marca RoutingMP=1 cuando existe al menos un detalle en HCFARMEPD con SENDTO IN (0,2) y CANPENPRO>0 (cantidad pendiente por procesar); [RETURN_RESULT] ViewPharmacyMixingStation: Incluye adicionalmente órdenes con TIPOSOLICITUD=2 cuyo ingreso tenga TRATAESPECIA=3 e IESTADOIN<>''C'', con TIPOSOLICITUD1=0 y PERMITEEXTRA=NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewPharmacyMixingStation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ING.IESTADOIN NOT IN (''F'',''C'') OR A.TIPOSOLICITUD = 2 → Incluye la orden en el primer bloque con datos del CareGroup del ingreso (PERMITEEXTRA, CODCAMACT) else Se evalúa el segundo bloque solo si TIPOSOLICITUD=2 y el ingreso tiene tratamiento especial=3 y no está cancelado; si Existe HCFARMEPD con SENDTO IN (0,2) y CANPENPRO>0 para la orden → RoutingMP = 1 (requiere ruteo a mezclas) else RoutingMP = 0; si TIPOSOLICITUD es NULL en primer bloque → Asigna TIPOSOLICITUD = 0 por defecto else TIPOSOLICITUD = 1 por defecto en el segundo bloque cuando es NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewPharmacyMixingStation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFARMEPC; dbo.HCFARMEPD; dbo.ADINGRESO; Contract.CareGroup', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewPharmacyMixingStation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewPharmacyMixingStation';
GO
