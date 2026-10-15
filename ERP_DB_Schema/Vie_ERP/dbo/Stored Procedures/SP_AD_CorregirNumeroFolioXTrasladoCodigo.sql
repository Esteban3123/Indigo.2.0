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