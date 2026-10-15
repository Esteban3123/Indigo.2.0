CREATE VIEW [dbo].[ViewDashboardPharmacyDetail]
AS

	SELECT t.Id
		, t.FilaSeleccionada
		, t.Opcion
		, t.OpcionAnulado
		, t.Procedimiento
		, t.MarcarOpcion
		, t.Consecutivo
		, t.Ingreso
		, t.NUMEFOLIO
		, t.Entidad
		, t.CodigoEntidad
		, t.CodigoContrato
		, t.CodigoPlan
		, t.ContratoPlan
		, t.CodigoPaciente
		, t.Medico
		, t.NitMedico
		, t.Especialidad
		, t.Producto
		, t.CodProducto
		, t.CantidadPrescrita
		, t.DosisPrescrita
		, t.CantidadAutorizada
		, t.CantidadDispensada
		, t.NumeroNoPBS
		, t.AllowBillingWithoutAuthorization
		, (t.CantidadAutorizada - t.CantidadDispensada) As SaldoAutorizadoMipres
		, CASE WHEN t.NOPOSPROD = 1 
				THEN ISNULL(
					(
						SELECT SI.Color 
						FROM Inventory.SettingInventoryPBSControl SI
						WHERE (t.CantidadAutorizada - t.CantidadDispensada) BETWEEN SI.DoseFrom AND SI.DoseTo
					)
					, 
					(
						CASE WHEN DATEADD(MINUTE, [dbo].[fnConvertirAMinutos](t.ChangeAfter, CAST(t.ChangeAfterTimeUnit AS CHAR(1))), t.FECINIDOS) > Common.GETDATE()
							THEN t.WithoutCurrentAuthorizationColor
							ELSE t.WithoutAuthorizationManagementColor
						END
					)
				)
				ELSE ''
				END As ColorAlertaPBS
		, t.NombrePaqueteEnf
		, CAST(t.Tipo AS VARCHAR(1)) Tipo
		, t.TipoAnterior
		, t.CantidadPaqueteEnf
		, t.CantidadSolicitada
		, t.CantidadEntregada
		, t.CantidadPendiente
		, t.Unico
		, t.NOPOS
		, t.PBS
		, t.UNIRS
		, t.UnidadMedida
		, t.IDETIPHIS
		, t.Estado
		, t.IDAGEPROGQX
		, t.EXTRAMURAL
		, t.Custodia
		, t.ADMINISTRACION
		, t.EntityId
		, t.EntityName
		, t.RoutedTo
		, t.SENDTO
		, t.CodeSusceptibleMixingStation
		, t.Note
		, t.TIPPRODUC
		, t.Observation
		, t.TotalDose
		, t.TotalDoseMeasurement
	FROM (
		SELECT	CAST(NEWID()as varchar(50)) as Id,
			CAST('' AS INT) AS FilaSeleccionada,
			'0' AS Opcion,
			'0' AS OpcionAnulado,
			'' AS Procedimiento,
			CAST('0' AS BIT) AS MarcarOpcion, 
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
			Concat(RTRIM ( SFD.CODPRODUC),' - ' ,RTRIM(DESPRODUC)) AS Producto,
			SFD.CODPRODUC As CodProducto,
			SFD.CANPEDPRO As CantidadPrescrita,
			Case hc.DURACIDOS
				When 'Tratamiento Continuo' Then -1
				When 'Dosis Unica         ' Then 1
				Else PM.CANT_AUTORIZADA
			End As DosisPrescrita,
			PM.CANT_AUTORIZADA AS CantidadAutorizada,
			ISNULL([dbo].[fnCalcularCantidadDispensada](SFD.NUMINGRES, SFD.CODPRODUC), 0) AS CantidadDispensada,
			PM.CODMINSALUD As NumeroNoPBS,
			LP.NOPOSPROD,
			SIV.ChangeAfter,
			SIV.ChangeAfterTimeUnit,
			SFD.FECINIDOS,
			SIV.WithoutCurrentAuthorizationColor,
			SIV.WithoutAuthorizationManagementColor,
			SIV.DispensingWithoutAuthorization as AllowBillingWithoutAuthorization,
			paq.NOMBRE NombrePaqueteEnf,
			IIF(npos.IDHCFARMEPC IS NULL, SFD.TIPOREGIS, '5') AS Tipo,
			CAST(SFD.TIPOREGIS AS VARCHAR(1)) AS TipoAnterior,
			ISNULL(npos.Quantity, 0) AS CantidadPaqueteEnf,
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
									WHEN  hc.DESADMINI IS NULL THEN 
										RTRIM(CAST(ISNULL(SFD.DOSISPROD, 0) AS CHAR)) + ' ' 
											+ RTRIM(CAST(ISNULL(UM2.ABRUNIMED, '') AS CHAR)) 
											+ 
											(CASE WHEN SFD.FRECUENCI IS NULL
												THEN ''
												ELSE 
													' Cada ' + RTRIM(CAST(ISNULL(SFD.FRECUENCI, '') AS CHAR)) + 
													ISNULL(CASE SFD.UNIFRECUE 
														WHEN '1' THEN ' min(s) ' 
														WHEN '2' THEN ' Hora(s) ' 
														WHEN '3' THEN ' Dia(s) ' 
													END, '') 
											END) + ' Vía: ' + (IIF
	(
		(CHARINDEX('vía', REPLACE(ISNULL(hc.DESADMINI, mescla.ADMMEZLIQ),'continuamente', ''))) > 0,
		SUBSTRING(REPLACE(ISNULL(hc.DESADMINI, mescla.ADMMEZLIQ),'continuamente', ''), CHARINDEX('vía',  REPLACE(ISNULL(hc.DESADMINI, mescla.ADMMEZLIQ),'continuamente', '')) + LEN('Via') + 1, LEN( REPLACE(ISNULL(hc.DESADMINI, mescla.ADMMEZLIQ),'continuamente', ''))),
		ISNULL(viaMed.DESVIAADM, via.DESVIAADM)
	)) 
									ELSE hc.DESADMINI 
								END 
							WHEN hc.DURACIDOS ='Dosis Unica' THEN RTRIM(CAST(hc.DOSISPRFN AS CHAR)) + ' ' + RTRIM(CAST(UM.ABRUNIMED AS CHAR)) + ' Dosis Única Via: ' + (IIF
	(
		(CHARINDEX('vía', REPLACE(ISNULL(hc.DESADMINI, mescla.ADMMEZLIQ),'continuamente', ''))) > 0,
		SUBSTRING(REPLACE(ISNULL(hc.DESADMINI, mescla.ADMMEZLIQ),'continuamente', ''), CHARINDEX('vía',  REPLACE(ISNULL(hc.DESADMINI, mescla.ADMMEZLIQ),'continuamente', '')) + LEN('Via') + 1, LEN( REPLACE(ISNULL(hc.DESADMINI, mescla.ADMMEZLIQ),'continuamente', ''))),
		ISNULL(viaMed.DESVIAADM, via.DESVIAADM)
	)) 
							ELSE RTRIM(CAST(hc.DOSISPRFN AS CHAR)) + ' ' + RTRIM(CAST(UM.ABRUNIMED AS CHAR)) + ' Cada ' + RTRIM(CAST(hc.FRECUENCI AS CHAR)) + 
								CASE hc.UNIFRECUE 
									WHEN '1' THEN ' min(s) ' 
									WHEN '2' THEN ' Hora(s) ' 
									WHEN '3' THEN ' Dia(s) ' 
								END + ' Vía: ' + (IIF
	(
		(CHARINDEX('vía', REPLACE(ISNULL(hc.DESADMINI, mescla.ADMMEZLIQ),'continuamente', ''))) > 0,
		SUBSTRING(REPLACE(ISNULL(hc.DESADMINI, mescla.ADMMEZLIQ),'continuamente', ''), CHARINDEX('vía',  REPLACE(ISNULL(hc.DESADMINI, mescla.ADMMEZLIQ),'continuamente', '')) + LEN('Via') + 1, LEN( REPLACE(ISNULL(hc.DESADMINI, mescla.ADMMEZLIQ),'continuamente', ''))),
		ISNULL(viaMed.DESVIAADM, via.DESVIAADM)
	)) 
						END 
				END
			) AS ADMINISTRACION,
			SFD.ID EntityId,
			'HCFARMEPD' EntityName,
			Case isnull(SFD.SENDTO, 1)
			WHEN 0 THEN 'Pendiente'
			WHEN 1 THEN 'Servicio Farmaceutico'
			WHEN 2 THEN 'Central de Mezclas'
			END RoutedTo,
			isnull(SFD.SENDTO, 1) SENDTO,
			CAST(SFD.CodeSusceptibleMixingStation AS VARCHAR(36)) AS CodeSusceptibleMixingStation,
			(SELECT top 1 pdn.Note 
				FROM Inventory.PharmaceuticalDispensingNotes pdn 
				JOIN Inventory.ATC atc on atc.Code = SFD.CODPRODUC
				WHERE pdn.AdmissionNumber = SFD.NUMINGRES and pdn.ATCId = atc.ID  order by pdn.Id DESC
				) Note,
			LP.TIPPRODUC 
			,isnull(hc.INDAPLMED, mescla.INDAPLMED) as Observation
			, CASE
				WHEN psm.Id IS NOT NULL THEN (sfd.dosisprod * psm.ApplicationsNumber)
				WHEN SFD.SourceTable = 'HCPRESCRA' THEN (hcex.NUMEROAPLICACIONINICIAL * ISNULL(sfd.DOSISPROD, hc.DOSISPROD))
				ELSE sfd.DOSISPROD
				END AS TotalDose
			--, IIF(psm.Id IS NOT NULL, sfd.dosisprod * psm.ApplicationsNumber, IIF(sfd.DOSISPROD = 0, NULL, sfd.DOSISPROD)) TotalDose
			,UM3.ABRUNIMED TotalDoseMeasurement
		FROM dbo.HCFARMEPD SFD WITH (NOLOCK)
		JOIN dbo.ADINGRESO ING WITH (NOLOCK) ON SFD.NUMINGRES = ING.NUMINGRES
		JOIN Contract.CareGroup CG WITH (NOLOCK) ON CG.Id = ING.GENCAREGROUP
		JOIN Contract.HealthAdministrator HA WITH (NOLOCK) ON ING.GENCONENTITY = HA.Id
		JOIN dbo.IHLISTPRO LP WITH (NOLOCK) ON SFD.CODPRODUC = LP.CODPRODUC 
		JOIN dbo.INPROFSAL PS WITH (NOLOCK) ON SFD.CODPROSAL = PS.CODPROSAL
		JOIN dbo.INESPECIA E WITH (NOLOCK) ON PS.CODESPEC1 = E.CODESPECI 
		LEFT JOIN MedicalHistory.ProductSusceptibleMixingStation psm ON psm.CodeSusceptibleMixingStation = sfd.CodeSusceptibleMixingStation
		LEFT JOIN EHR.HCORDMEDICAM HDA WITH (NOLOCK) ON SFD.SourceTable = 'HCORDMEDICAM' and hda.ID = sfd.IdSourceTable
		LEFT JOIN dbo.HCPRESCRA hc WITH (NOLOCK) ON SFD.SourceTable = 'HCPRESCRA' AND SFD.IdSourceTable=hc.ID
		LEFT JOIN INUNIMEDI UM3 ON UM3.CODUNIMED = ISNULL(sfd.CODUNIMED, hc.CODUNIMED)
		LEFT JOIN HCPRESCRAEXT hcex WITH(NOLOCK) ON hcex.IDHCPRESCRA = hc.ID
		LEFT JOIN dbo.HCVIAADMI VIA WITH (NOLOCK) ON LP.CODVIAADM = VIA.CODVIAADM
		LEFT JOIN dbo.HCVIAADMI viaMed WITH (NOLOCK) ON viaMed.CODVIAADM = hc.CODVIAADM
		LEFT JOIN dbo.HCINFLIQA mescla WITH (NOLOCK) ON mescla.CONSECUTI = SFD.IdSourceTable AND 'HCINFLIQA' = SFD.SourceTable
		LEFT JOIN dbo.INUNIMEDI UM WITH (NOLOCK) ON hc.CODUNIMFN = UM.CODUNIMED 
		LEFT JOIN dbo.INUNIMEDI UM2 WITH (NOLOCK) ON SFD.CODUNIMED = UM2.CODUNIMED 
		LEFT JOIN (
			SELECT P.NUMINGRES, P.CODPRODUC, isnull(SUM(P.CANPEDPRO), 0) AS CANT_AUTORIZADA, STRING_AGG(CODMINSALUD, ', ') AS CODMINSALUD 
			FROM dbo.HCJUNOPOM P WITH (NOLOCK)
			WHERE p.CODMINSALUD <> ''
			GROUP BY P.NUMINGRES, P.CODPRODUC
		) PM ON PM.NUMINGRES = SFD.NUMINGRES AND SFD.CODPRODUC = PM.CODPRODUC AND LP.NOPOSPROD = 1
		------------- paquete de enfermeria -----------------------------
		LEFT JOIN
		(
			SELECT 
				npo.IDHCFARMEPC,
				npo.IDAGPAQUETES,
				npod.CODPRODUC,
				SUM(npod.Quantity) Quantity
			FROM MedicalHistory.NursingPackagesOrder npo WITH (NOLOCK)
			JOIN MedicalHistory.NursingPackagesOrderDetail npod WITH (NOLOCK) ON npo.Id = npod.IdNursingPackagesOrder
			GROUP BY npo.IDHCFARMEPC, npo.IDAGPAQUETES, npod.CODPRODUC
		) npos ON npos.IDHCFARMEPC = SFD.CODCONCEC AND npos.IDAGPAQUETES = sfd.IDAGPAQUETES AND npos.CODPRODUC = SFD.CODPRODUC
		LEFT JOIN dbo.AGPAQUETES paq WITH (NOLOCK) ON paq.ID = npos.IDAGPAQUETES
		----------------------------------------------------------------
		CROSS APPLY (SELECT TOP 1 * FROM Inventory.SettingInventory si WITH (NOLOCK) where si.OperatingUnitId = CG.OperativeUnitId) SIV
		WHERE SFD.CANPENPRO > 0 AND SFD.IDAGEPROGQX IS NULL 
	) as t
GO