CREATE PROCEDURE [dbo].[SP_HC_ListarHistoricoPerfilFarmacoteerapeutico]
(
  @Identificacion as varchar(25)
)

AS
BEGIN
  SET NOCOUNT ON;
 
		select c.ID as IDHCNUTPAREC, FECHAVERIFICA AS Fechaverifica, Rtrim(B.NOMMEDICO) AS Medico,Rtrim(OBSERVERIFICA) as Observacion,c.NUMINGRES  as Ingreso,
		RTRIM(d.DESESPECI) as EspecialidadTratante, RTRIM(v.NOMDIAGNO) as Diagnostico,t.Description as DosisUnitaria, Rtrim(n.UFUDESCRI) AS UFUDESCRI, Rtrim(m.NOMCENATE) AS NOMCENATE
		from dbo.HCNUTPAREC C
			INNER JOIN HCHISPACA x  with(noLock)  ON x.ID = c.IDHCHISPACA 
			INNER JOIN INPROFSAL B with(noLock)  ON C.CODPROSAL = B.CODPROSAL
			INNER JOIN INESPECIA D with(noLock)  ON D.CODESPECI = x.CODESPTRA
			INNER JOIN INDIAGNOS v with(noLock)  ON v.CODDIAGNO = x.CODDIAGNO
			INNER JOIN MixingStation.UnitDoseType t with(noLock)  on t.Id = c.IDUNITDOSETYPE 
			INNER JOIN INUNIFUNC n with(noLock)  on n.UFUCODIGO = x.UFUCODIGO 
			INNER JOIN ADCENATEN m with(noLock)  on m.CODCENATE = x.CODCENATE  
		where AGRUPAQUETE is not null  and c.IPCODPACI = @Identificacion

		UNION ALL 
                SELECT '' as IDHCNUTPAREC, DocumentDate as Fechaverifica,RTRIM(C.NOMMEDICO) AS Medico, '' as Observacion,(A.NUMINGRES) as Ingreso,
				RTRIM(F.DESESPECI) AS EspecialidadTratante, RTRIM(H.NOMDIAGNO) as Diagnostico, '' AS DosisUnitaria, RTRIM(E.UFUDESCRI) AS UFUDESCRI, Rtrim(D.NOMCENATE) AS NOMCENATE               
                FROM MedicalHistory.ChemicalPharmaceuticalNotes AS A
				INNER JOIN INPACIENT B ON A.IPCODPACI=B.IPCODPACI 
				INNER JOIN INPROFSAL C ON A.CODPROSAL=C.CODPROSAL
				INNER JOIN ADCENATEN D ON A.CODCENATE=D.CODCENATE
				INNER JOIN INUNIFUNC E ON A.UFUCODIGO=E.UFUCODIGO
				INNER JOIN INDIAGNOP P on A.NUMINGRES=P.NUMINGRES and A.IPCODPACI=P.IPCODPACI and P.CODDIAPRI = 1 
				INNER JOIN INDIAGNOS H on P.CODDIAGNO= H.CODDIAGNO
				LEFT OUTER JOIN INESPECIA F ON C.CODESPEC1=F.CODESPECI
			    WHERE A.IPCODPACI= @Identificacion
		
END