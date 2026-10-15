

CREATE VIEW [dbo].[ViewDashBoardPharmacyMixingSationPatient]
AS
	SELECT  CONCAT(trim(A.IPCODPACI), ' - ', A.ORDTRANUE,'-',A.NUMINGRES ) Id,
			0 AS SelectOption, 
			A.IPCODPACI AS CodigoPaciente,			
            A.CODCENATE AS CodigoCentroAtencion,
			RTRIM(B.IPNOMCOMP) AS NombrePaciente, 
			A.NUMINGRES AS Ingreso, 
			CASE 
               WHEN ORDTRANUE = 1 THEN 'Tratamiento nuevo'
               WHEN ORDTRANUE = 2 THEN 'Tratamiento antiguo'
               WHEN ORDTRANUE = 3 THEN 'Código azul' end AS Tipo, 
			CAST('' AS bit) AS Impresion, 
			--Case When A.MEDICAMENTOVALIDADO Is Null Or A.MEDICAMENTOVALIDADO = 0 Then Cast(0 As Bit) Else Cast(1 As Bit) End As Validado,			
			B.IPFECNACI BirthDay
	FROM [dbo].[ViewPharmacyMixingStationWithOncology] AS A WITH (NOLOCK) 
	JOIN dbo.INPACIENT AS B WITH (NOLOCK) ON A.IPCODPACI = B.IPCODPACI	
	WHERE A.RoutingMP = 1 
	GROUP BY A.NUMINGRES , A.IPCODPACI, B.IPNOMCOMP, A.ORDTRANUE, B.IPFECNACI, A.CODCENATE