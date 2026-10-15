CREATE PROCEDURE [dbo].[SP_ONCO_ListadoDiagnosticosCancerPaciente]
(
@INDpaciente Varchar(25)
)
AS
BEGIN
	SET NOCOUNT ON;


----------------------------------	
	select  A.ID,a.IPCODPACI as 'Identificacion', A.NUMINGRES as 'Ingreso' ,  A.FECHAREGISTRO,RTRIM(B.CODDIAGNO)AS 'Codigo Diagnostico',RTRIM(B.NOMDIAGNO)AS 'Nombre Diagnostico', CASE TIPO WHEN 1 THEN 'Primario' WHEN 2 THEN 'Otro Primario' WHEN 3 THEN 'Primario Desconocido' END AS 'Tipo',
			Rtrim(C.NOMMEDICO) as 'Nombre Medico', A.NUMEFOLIO AS 'Folio', 
			(Select top 1 RTRIM(X.DESESPECI) from HCHISPACA Z INNER JOIN INESPECIA X ON Z.CODESPTRA = X.CODESPECI where Z.IPCODPACI = @INDpaciente AND A.NUMEFOLIO = Z.NUMEFOLIO) AS 'Especialidad Tratante',
			Rtrim(D.NOMCENATE) as 'Centro Atencion', D.CODCENATE as CODCENATE, Rtrim(E.UFUDESCRI) AS 'Unidad Funcional',G.CODIGOGRUPO AS 'Codigo Grupo' 
	from (
		select distinct tmpX.IDUltimo from ( Select (select top 1 ID from HCGRUPOCANCERPACIC t where t.CODDIAGNO = A.CODDIAGNO AND T.IPCODPACI = @INDpaciente ORDER BY t.FECHAREGISTRO ASC ) as IDUltimo
		FROM   HCGRUPOCANCERPACIC  A where  a.IPCODPACI = @INDpaciente 
		) as tmpX
	) as TMP 
			inner join HCGRUPOCANCERPACIC  A with(nolock) on A.ID = TMP.IDUltimo
			Inner Join INDIAGNOS B with(nolock) on A.CODDIAGNO = B.CODDIAGNO 
			Inner Join INPROFSAL C with(nolock) on A.CODPROSAL = C.CODPROSAL  
			Inner Join ADCENATEN D with(nolock) on A.CODCENATE = D.CODCENATE  
			Inner Join INUNIFUNC E with(nolock) on A.UFUCODIGO = E.UFUCODIGO
			left JOIN ADGRUPOCANCERDIAGND F with(nolock) ON A.CODDIAGNO = F.CODDIAGNO 
			left JOIN ADGRUPOCANCERC G with(nolock) ON F.IDADGRUPOCANCERC = G.ID
end