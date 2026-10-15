

CREATE VIEW [dbo].[ViewDashboardPharmacyDetailMixingSation]
AS
SELECT	CAST(NEWID()as varchar(50)) as Id,
		CAST('' AS INT) AS FilaSeleccionada,
		'0' AS Opcion,
		'0' AS OpcionAnulado,
		'' AS Procedimiento,
		0 AS MarcarOpcion, 
		SFD.CODCONCEC as Consecutivo,
		RTRIM(SFD.NUMINGRES) AS Ingreso,
		SFD.NUMEFOLIO,
		RTRIM(HA.Name) AS Entidad,
		HA.Code AS CodigoEntidad,
		CODCONTRA AS CodigoContrato ,
		CODPANATE AS CodigoPlan,
		RTRIM(CG.Name) AS ContratoPlan,
		RTRIM(SFD.IPCODPACI) as CodigoPaciente,
		CONCAT(Rtrim( SFD.CODPROSAL), ' - ',rtrim(PS.NOMMEDICO)) as Medico, 
		PS.CODIGONIT as NitMedico,
		CONCAT(E.CODESPECI ,' - ', E.DESESPECI) as Especialidad,
		RTRIM(DESPRODUC) AS Producto,
		RTRIM ( SFD.CODPRODUC) as CodProducto,
		SFD.TIPOREGIS AS Tipo,
		SFD.CANPEDPRO AS CantidadSolicitada,
		CAST('' AS INT) AS CantidadEntregada, 
		SFD.CANPENPRO AS CantidadPendiente,
		CAST(0 AS BIT) AS Unico,
		SFD.NOPOSPROD AS NOPOS,		
		CASE 
			WHEN SFD.Conditioned = 1 THEN 'Condicionado'
            WHEN LP.NOPOSPROD = 1 THEN 'No'
            ELSE 'Si'
        END AS PBS,
		CASE 
			WHEN SFD.UNIRS = 1 THEN 'Si' 
			ELSE 'No' 
		END AS UNIRS, 
		SFD.CODUNIMED AS UnidadMedida,
		SFD.IDETIPHIS,
		SFD.PROESTADO as Estado,
		SFD.IDAGEPROGQX, 
		convert(bit,isnull(SFD.EXTRAMURAL,0)) as  EXTRAMURAL,
		ISNULL(SFD.MEDICACUSTODIA,CAST(0 AS BIT)) as Custodia,
		RTRIM
		(
			CASE 
				WHEN FORMAPRESCRIBE IS NOT NULL THEN DESADMINI 
				ELSE 
					CASE 
						WHEN hc.DOSISPRFN IS NULL THEN 
							CASE 
								WHEN  hc.DESADMINI IS NULL THEN 'No aplica.' 
								ELSE hc.DESADMINI 
							END 
						WHEN hc.DURACIDOS ='Dosis Unica' THEN RTRIM(CAST(hc.DOSISPRFN AS CHAR)) + ' ' + RTRIM(CAST(ABRUNIMED AS CHAR)) + ' Dosis Única Via: ' + RTRIM(DESVIAADM) 
						ELSE RTRIM(CAST(hc.DOSISPRFN AS CHAR)) + ' ' + RTRIM(CAST(ABRUNIMED AS CHAR)) + ' Cada ' + RTRIM(CAST(hc.FRECUENCI AS CHAR)) + 
							CASE hc.UNIFRECUE 
								WHEN '1' THEN ' min(s) ' 
								WHEN '2' THEN ' Hora(s) ' 
								WHEN '3' THEN ' Dia(s) ' 
							END + 'Vía: ' + RTRIM(DESVIAADM) 
					END 
			END
		) AS ADMINISTRACION,
		SFD.ID EntityId,
		'HCFARMEPD' EntityName,
		Case SFD.SENDTO
		WHEN 0 THEN 'Pendiente'
		WHEN 1 THEN 'Servicio Farmaceutico'
		WHEN 2 THEN 'Central de Mezclas'
		END RoutedTo,
		SFD.SENDTO,
		CAST(SFD.CodeSusceptibleMixingStation AS VARCHAR(36)) AS CodeSusceptibleMixingStation,
		hcp.FinishedProductCode FinishedProductCodeNPT
FROM dbo.HCFARMEPD SFD
JOIN dbo.ADINGRESO ING ON SFD.NUMINGRES = ING.NUMINGRES
JOIN Contract.CareGroup CG ON CG.Id = ING.GENCAREGROUP
JOIN Contract.HealthAdministrator HA ON ING.GENCONENTITY = HA.Id
JOIN dbo.IHLISTPRO LP ON SFD.CODPRODUC = LP.CODPRODUC 
JOIN dbo.INPROFSAL PS ON SFD.CODPROSAL = PS.CODPROSAL
JOIN dbo.INESPECIA E ON PS.CODESPEC1 = E.CODESPECI 
LEFT JOIN HCNUTPAREC hct ON hct.ID = sfd.IdSourceTable AND sfd.SourceTable = 'HCNUTPAREC'
LEFT JOIN HCPARNUTC hcp ON hcp.ID = hct.IDHCPARNUTC
LEFT JOIN dbo.HCVIAADMI VIA ON LP.CODVIAADM = VIA.CODVIAADM
LEFT JOIN dbo.HCPRESCRA hc ON SFD.CODPRODUC = hc.CODPRODUC AND SFD.NUMEFOLIO = hc.NUMEFOLIO AND SFD.IPCODPACI = HC.IPCODPACI AND SFD.NUMINGRES = HC.NUMINGRES AND HC.MANEXTPRO = '0'
LEFT JOIN dbo.INUNIMEDI UM ON hc.CODUNIMFN = UM.CODUNIMED 
where  SFD.CANPENPRO > 0 AND SFD.IDAGEPROGQX IS NULL AND SFD.SENDTO IN (0,2)