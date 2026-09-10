-- =============================================
-- Autor:		     Yezid Garcia Medina - Hector Rubiano
-- Fecha Creación:   13 Enero 2022
-- Description:	     SP que corrige número de folio posterior al Traslado de Código
-- =============================================
CREATE PROCEDURE [dbo].[SP_AD_CorregirNumeroFolioXTrasladoCodigo]
(
@Paciente varchar(25)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
		
	declare @tabla as table
	(foliohispaca nchar(10),
	conshispaca char(10),
	folioing char(10),
	foliourgevo nchar(10),
	consurgevo char(10),
	folionotevo nchar(10),
	consnotevo char(10),
	foliohispacaant nchar(10),
	conshispacaant char(10),
	cambio tinyint,
	consurgevoant char(10),
	consnotevoant char(10)
	)

	insert into @tabla
	SELECT 	
	A.NUMEFOLIO, A.CONSFOLIO, B.NUMEFOLIO, C.NUMEFOLIO, C.CONSFOLIO, D.NUMEFOLIO, D.CONSFOLIO,
	  A.NUMEFOLIO, A.CONSFOLIO, 0, C.CONSFOLIO, D.CONSFOLIO
	FROM HCHISPACA As A with(nolock)
	LEFT JOIN HCURGING1 As B with(nolock) ON A.FECHISPAC = B.FECINIATE AND A.IPCODPACI = B.IPCODPACI AND A.NUMINGRES = B.NUMINGRES
	LEFT JOIN HCURGEVO1 As C with(nolock) ON A.FECHISPAC = C.FECINIATE AND A.IPCODPACI = C.IPCODPACI AND A.NUMINGRES = C.NUMINGRES
	LEFT JOIN HCNOTEVO1 As D with(nolock) ON A.FECHISPAC = D.FECINIATE AND A.IPCODPACI = D.IPCODPACI AND A.NUMINGRES = D.NUMINGRES
	WHERE
	A.IPCODPACI=@Paciente ORDER BY FECHISPAC

	update @tabla set foliohispaca = folioing, cambio = 1 where folioing is not null and foliohispaca <> folioing
	update @tabla set foliohispaca = foliourgevo, cambio = 1 where foliourgevo is not null and foliohispaca <> foliourgevo
	update @tabla set foliohispaca = folionotevo, cambio = 1 where folionotevo is not null and foliohispaca <> folionotevo

	update t set conshispaca = c.foliohispaca, cambio = 2
	from @tabla t
	inner join 
	(select foliohispaca, foliohispacaant from @tabla t where cambio = 1) c
	on t.conshispaca = c.foliohispacaant
	and conshispaca <> c.foliohispaca

	update @tabla set consurgevo = conshispaca, cambio = 3 where consurgevo is not null and consurgevo <> conshispaca
	update @tabla set consnotevo = conshispaca, cambio = 3 where consnotevo is not null and consnotevo <> conshispaca

	--Select * from @tabla order by foliohispaca asc
	--HCURGING1
	UPDATE B SET B.NUMEFOLIO = T.folioing
	FROM @tabla T
	INNER JOIN HCHISPACA As A with(nolock) ON A.NUMEFOLIO = T.foliohispacaant
	INNER JOIN HCURGING1 As B with(nolock) ON A.FECHISPAC = B.FECINIATE AND A.IPCODPACI = B.IPCODPACI AND A.NUMINGRES = B.NUMINGRES
	WHERE
	A.IPCODPACI=@Paciente
	AND B.NUMEFOLIO <> T.folioing

	--HCURGEVO1 NUMEFOLIO
	UPDATE B SET B.NUMEFOLIO = T.foliourgevo 
	FROM @tabla T
	INNER JOIN HCHISPACA As A with(nolock) ON A.NUMEFOLIO = T.foliohispacaant
	INNER JOIN HCURGEVO1 As B with(nolock) ON A.FECHISPAC = B.FECINIATE AND A.IPCODPACI = B.IPCODPACI AND A.NUMINGRES = B.NUMINGRES
	WHERE
	A.IPCODPACI=@Paciente
	AND B.NUMEFOLIO <> T.foliourgevo

	--HCURGEVO1 CONSFOLIO
	UPDATE B SET B.CONSFOLIO = T.consurgevo
	FROM @tabla T
	INNER JOIN HCHISPACA As A with(nolock) ON A.NUMEFOLIO = T.foliohispacaant
	INNER JOIN HCURGEVO1 As B with(nolock) ON A.FECHISPAC = B.FECINIATE AND A.IPCODPACI = B.IPCODPACI AND A.NUMINGRES = B.NUMINGRES
	WHERE
	A.IPCODPACI=@Paciente
	AND B.NUMEFOLIO = T.foliourgevo
	AND B.CONSFOLIO <> T.consurgevo

	--HCNOTEVO1 NUMEFOLIO
	UPDATE B SET B.NUMEFOLIO = T.folionotevo 
	FROM @tabla T
	INNER JOIN HCHISPACA As A with(nolock) ON A.NUMEFOLIO = T.foliohispacaant
	INNER JOIN HCNOTEVO1 As B with(nolock) ON A.FECHISPAC = B.FECINIATE AND A.IPCODPACI = B.IPCODPACI AND A.NUMINGRES = B.NUMINGRES
	WHERE
	A.IPCODPACI=@Paciente
	AND B.NUMEFOLIO <> T.folionotevo

	--HCNOTEVO1 CONSFOLIO
	UPDATE B SET B.CONSFOLIO = T.consnotevo
	FROM @tabla T
	INNER JOIN HCHISPACA As A with(nolock) ON A.NUMEFOLIO = T.foliohispacaant
	INNER JOIN HCNOTEVO1 As B with(nolock) ON A.FECHISPAC = B.FECINIATE AND A.IPCODPACI = B.IPCODPACI AND A.NUMINGRES = B.NUMINGRES
	WHERE
	A.IPCODPACI=@Paciente
	AND B.NUMEFOLIO = T.folionotevo
	AND B.CONSFOLIO <> T.consnotevo

	--HCHISPACA NUMEFOLIO
	UPDATE A SET A.NUMEFOLIO = T.foliohispaca
	FROM @tabla T
	INNER JOIN
	HCHISPACA As A with(nolock) ON A.NUMEFOLIO = T.foliohispacaant
	WHERE
	A.IPCODPACI=@Paciente --ORDER BY FECHISPAC
	AND A.NUMEFOLIO <> T.foliohispaca

	--HCHISPACA CONSFOLIO
	UPDATE A SET A.CONSFOLIO = T.conshispaca
	FROM @tabla T
	INNER JOIN
	HCHISPACA As A with(nolock) ON A.NUMEFOLIO = T.foliohispaca
	WHERE
	A.IPCODPACI=@Paciente --ORDER BY FECHISPAC
	AND A.CONSFOLIO <> T.conshispaca

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Corrige y sincroniza los números de folio de la historia clínica de un paciente después de haber realizado un traslado de código (cambio de identificación del paciente). Recibe la cédula o código del paciente como parámetro y reconcilia los números de folio (NUMEFOLIO) y folios consecutivos (CONSFOLIO) que quedaron desincronizados entre la historia clínica principal (HCHISPACA), la nota inicial de urgencias (HCURGING1), las evoluciones de urgencias (HCURGEVO1) y las notas de evolución clínica (HCNOTEVO1). Garantiza la integridad y trazabilidad del expediente clínico del paciente asegurando que todos los documentos de la historia clínica apunten al número de folio correcto tras el cambio de identificador.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AD_CorregirNumeroFolioXTrasladoCodigo';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AD_CorregirNumeroFolioXTrasladoCodigo';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Sincroniza y corrige los números de folio y consecutivos de folio en las tablas de historia clínica, urgencias y notas de evolución de un paciente, después de un traslado de código.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_CorregirNumeroFolioXTrasladoCodigo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe existir en HCHISPACA con registros asociables por FECHISPAC, IPCODPACI y NUMINGRES a HCURGING1, HCURGEVO1 y HCNOTEVO1.; Debe haberse ejecutado previamente un traslado de código que generó inconsistencia entre los folios de las tablas relacionadas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_CorregirNumeroFolioXTrasladoCodigo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El folio definitivo de HCHISPACA se elige con prioridad: folioing > foliourgevo > folionotevo, sobreescribiendo en orden.; Tras la ejecución, NUMEFOLIO y CONSFOLIO en HCURGING1, HCURGEVO1, HCNOTEVO1 y HCHISPACA quedan alineados al folio/consecutivo unificado para la combinación FECHISPAC/IPCODPACI/NUMINGRES.; Las correcciones se aplican únicamente a los registros del paciente recibido como parámetro.; Los registros que ya están alineados (igualdad ya cumplida) no se modifican.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_CorregirNumeroFolioXTrasladoCodigo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Folio de historia clínica; Consecutivo de folio; Ingreso de urgencias; Evolución de urgencias; Notas de evolución; Traslado de código', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_CorregirNumeroFolioXTrasladoCodigo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] HCURGING1: Cuando B.NUMEFOLIO <> folioing del paciente (unido por FECINIATE/IPCODPACI/NUMINGRES con HCHISPACA cuyo NUMEFOLIO coincide con foliohispacaant), se actualiza NUMEFOLIO al valor folioing.; [UPDATE] HCURGEVO1: Cuando B.NUMEFOLIO <> foliourgevo, se actualiza NUMEFOLIO al valor foliourgevo del registro relacionado.; [UPDATE] HCURGEVO1: Cuando B.NUMEFOLIO = foliourgevo y B.CONSFOLIO <> consurgevo, se actualiza CONSFOLIO al consecutivo recalculado.; [UPDATE] HCNOTEVO1: Cuando B.NUMEFOLIO <> folionotevo, se actualiza NUMEFOLIO al valor folionotevo del registro relacionado.; [UPDATE] HCNOTEVO1: Cuando B.NUMEFOLIO = folionotevo y B.CONSFOLIO <> consnotevo, se actualiza CONSFOLIO al consecutivo recalculado.; [UPDATE] HCHISPACA: Cuando A.NUMEFOLIO (igual al folio anterior foliohispacaant) <> foliohispaca recalculado, se actualiza NUMEFOLIO al nuevo folio unificado.; [UPDATE] HCHISPACA: Cuando A.CONSFOLIO <> conshispaca recalculado (uniendo por nuevo NUMEFOLIO), se actualiza CONSFOLIO con el consecutivo corregido.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_CorregirNumeroFolioXTrasladoCodigo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si folioing no es nulo y foliohispaca <> folioing → El folio maestro de historia se reemplaza con el de ingreso de urgencias (cambio=1).; si foliourgevo no es nulo y foliohispaca <> foliourgevo → El folio maestro se reemplaza con el de evolución de urgencias (cambio=1).; si folionotevo no es nulo y foliohispaca <> folionotevo → El folio maestro se reemplaza con el de notas de evolución (cambio=1).; si Existe un registro con cambio=1 cuyo foliohispacaant coincide con conshispaca de otro registro y dicho conshispaca <> nuevo foliohispaca → Se actualiza conshispaca al nuevo foliohispaca (cambio=2), propagando la corrección a registros que referencian al folio anterior.; si consurgevo no es nulo y consurgevo <> conshispaca → Se sincroniza consurgevo con el conshispaca corregido (cambio=3).; si consnotevo no es nulo y consnotevo <> conshispaca → Se sincroniza consnotevo con el conshispaca corregido (cambio=3).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_CorregirNumeroFolioXTrasladoCodigo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHISPACA; dbo.HCURGING1; dbo.HCURGEVO1; dbo.HCNOTEVO1', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_CorregirNumeroFolioXTrasladoCodigo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_CorregirNumeroFolioXTrasladoCodigo';
-- GO
