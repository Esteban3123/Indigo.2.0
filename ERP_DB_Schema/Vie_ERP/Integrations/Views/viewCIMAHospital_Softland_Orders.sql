





CREATE view [Integrations].[viewCIMAHospital_Softland_Orders]
as

--1 - Admission
--2 - Patient
--3 - LaboratoryOrder
--4 - ImageOrder
--5 - PathologyOrder
--6 - SurgicalProcedureOrder
--7 - NotSurgicalProcedureOrder
--8 - InterconsultationOrder
	  /*Laboratorios Hosp. y Ambulatorio*/
		SELECT
		Sync.id,
	    case SYnc.Type when 1 then 'Admission' when 2 then 'Patient' when 3 then 'LaboratoryOrder' when 4 then 'ImageOrder' when 5 then 'PathologyOrder' when 6 then 'SurgicalProcedureOrder' when 7 then 'NotSurgicalProcedureOrder' when 8 then 'InterconsultationOrder'  end Type,
		case SYnc.Action when 1 then 'Insert' when 2 then 'update' when 3 then 'delete' end Action,
		Sync.TransactionDate,
		Sync.State,
		rtrim(ltrim(A.CODSERIPS)) AS CodigoArticulo,
		'URG' as Bodega,
		A.canserips as Cantidad,
		'ND' as CodigoMedico,
		A.FECORDMED AS FechaSolicitud,
		'Baja' as Prioridad,
		'vie' as Comentario,
		A.[AUTO] as IdOrder,
		RTRIM(A.IPCODPACI) AS Identificacion, 
		RTRIM(B.IPNOMCOMP) AS NombrePaciente, 
		A.ESTSERIPS AS Estado,
		A.NUMINGRES AS AdmissionNumberVie,
		RA.AdmissionNumberLegacy
		--(select top 1 AdmissionnumberLegacy from  Integrations.CIMAHospital_RelatedAdmission where AdmissionnumberVie = A.NUMINGRES ) as AdmissionnumberLegacy
		FROM  dbo.HCORDLABO AS A with(nolock)  INNER JOIN
		dbo.INPACIENT AS B with(nolock)  ON A.IPCODPACI = B.IPCODPACI INNER JOIN
		dbo.INCUPSIPS AS D with(nolock)  ON A.CODSERIPS = D .CODSERIPS INNER JOIN
		dbo.ADINGRESO AS E with(nolock)  ON A.IPCODPACI = E.IPCODPACI AND A.NUMINGRES = E.NUMINGRES INNER JOIN
		dbo.INPROFSAL AS H with(nolock)  ON A.CODPROSAL =H.CODPROSAL   INNER JOIN  
		Integrations.CIMAHospital_Softland_Synch sync on sync.DataId = A.AUTO inner join
		Integrations.CIMAHospital_RelatedAdmission RA on RA.AdmissionnumberVie = A.NUMINGRES
		WHERE sync.State = 0 AND sync.Type = 3 -- 3 : laboratorios
		UNION ALL
		SELECT  
		Sync.id,
	    case SYnc.Type when 1 then 'Admission' when 2 then 'Patient' when 3 then 'LaboratoryOrder' when 4 then 'ImageOrder' when 5 then 'PathologyOrder' when 6 then 'SurgicalProcedureOrder' when 7 then 'NotSurgicalProcedureOrder' when 8 then 'InterconsultationOrder'  end Type,
		case SYnc.Action when 1 then 'Insert' when 2 then 'update' when 3 then 'delete' end Action,
		Sync.TransactionDate,
		Sync.State,
		rtrim(ltrim(A.CODSERIPS)) AS CodigoArticulo,
		'URG' as Bodega,
		A.canserips as Cantidad,
		'ND' as CodigoMedico,
		A.FECORDMED AS FechaSolicitud,
		'Baja' as Prioridad,
		'vie' as Comentario,
		A.[AUTO] as IdOrder,
		RTRIM(A.IPCODPACI) AS Identificacion, 
		RTRIM(B.IPNOMCOMP) AS NombrePaciente, 
		A.ESTSERIPS AS Estado,
		A.NUMINGRES AS AdmissionNumberVie,
		RA.AdmissionNumberLegacy
		FROM  dbo.AMBORDLAB AS A with(nolock)  INNER JOIN
		dbo.INPACIENT AS B with(nolock)  ON A.IPCODPACI = B.IPCODPACI INNER JOIN
		dbo.INCUPSIPS AS D with(nolock)  ON A.CODSERIPS = D .CODSERIPS INNER JOIN
		dbo.ADINGRESO AS E with(nolock)  ON A.IPCODPACI = E.IPCODPACI AND A.NUMINGRES = E.NUMINGRES INNER JOIN
		dbo.INPROFSAL AS H with(nolock)  ON A.CODPROSAL =H.CODPROSAL   INNER JOIN  
		Integrations.CIMAHospital_Softland_Synch sync on sync.DataId = A.AUTO inner join
		Integrations.CIMAHospital_RelatedAdmission RA on RA.AdmissionnumberVie = A.NUMINGRES
		WHERE sync.State = 0 AND sync.Type = 3 -- 3 : laboratorios 

	   /*Imagenes  Hosp. y Ambulatorio*/
	   UNION ALL
	   SELECT
		Sync.id,
			    case SYnc.Type when 1 then 'Admission' when 2 then 'Patient' when 3 then 'LaboratoryOrder' when 4 then 'ImageOrder' when 5 then 'PathologyOrder' when 6 then 'SurgicalProcedureOrder' when 7 then 'NotSurgicalProcedureOrder' when 8 then 'InterconsultationOrder'  end Type,
		case SYnc.Action when 1 then 'Insert' when 2 then 'update' when 3 then 'delete' end Action,
		Sync.TransactionDate,
		Sync.State,
		rtrim(ltrim(A.CODSERIPS)) AS CodigoArticulo,
		'URG' as Bodega,
		A.canserips as Cantidad,
		'ND' as CodigoMedico,
		A.FECORDMED AS FechaSolicitud,
		'Baja' as Prioridad,
		'vie' as Comentario,
		A.[AUTO] as IdOrder,
		RTRIM(A.IPCODPACI) AS Identificacion, 
		RTRIM(B.IPNOMCOMP) AS NombrePaciente, 
		A.ESTSERIPS AS Estado,
		A.NUMINGRES AS AdmissionNumberVie,
		RA.AdmissionNumberLegacy		
		FROM  dbo.HCORDIMAG AS A with(nolock)  INNER JOIN
		dbo.INPACIENT AS B with(nolock)  ON A.IPCODPACI = B.IPCODPACI INNER JOIN
		dbo.INCUPSIPS AS D with(nolock)  ON A.CODSERIPS = D .CODSERIPS INNER JOIN
		dbo.ADINGRESO AS E with(nolock)  ON A.IPCODPACI = E.IPCODPACI AND A.NUMINGRES = E.NUMINGRES INNER JOIN
		dbo.INPROFSAL AS H with(nolock)  ON A.CODPROSAL =H.CODPROSAL   INNER JOIN  
		Integrations.CIMAHospital_Softland_Synch sync on sync.DataId = A.AUTO inner join
		Integrations.CIMAHospital_RelatedAdmission RA on RA.AdmissionnumberVie = A.NUMINGRES
		WHERE sync.State = 0 AND sync.Type = 4 -- 4 - ImageOrder
		UNION ALL
		SELECT  
		Sync.id,
		case SYnc.Type when 1 then 'Admission' when 2 then 'Patient' when 3 then 'LaboratoryOrder' when 4 then 'ImageOrder' when 5 then 'PathologyOrder' when 6 then 'SurgicalProcedureOrder' when 7 then 'NotSurgicalProcedureOrder' when 8 then 'InterconsultationOrder'  end Type,
		case SYnc.Action when 1 then 'Insert' when 2 then 'update' when 3 then 'delete' end Action,
		Sync.TransactionDate,
		Sync.State,
		rtrim(ltrim(A.CODSERIPS)) AS CodigoArticulo,
		'URG' as Bodega,
		A.canserips as Cantidad,
		'ND' as CodigoMedico,
		A.FECORDMED AS FechaSolicitud,
		'Baja' as Prioridad,
		'vie' as Comentario,
		A.[AUTO] as IdOrder,
		RTRIM(A.IPCODPACI) AS Identificacion, 
		RTRIM(B.IPNOMCOMP) AS NombrePaciente, 
		A.ESTSERIPS AS Estado,
		A.NUMINGRES AS AdmissionNumberVie,
		RA.AdmissionNumberLegacy
		FROM  dbo.AMBORDIMA AS A with(nolock)  INNER JOIN
		dbo.INPACIENT AS B with(nolock)  ON A.IPCODPACI = B.IPCODPACI INNER JOIN
		dbo.INCUPSIPS AS D with(nolock)  ON A.CODSERIPS = D .CODSERIPS INNER JOIN
		dbo.ADINGRESO AS E with(nolock)  ON A.IPCODPACI = E.IPCODPACI AND A.NUMINGRES = E.NUMINGRES INNER JOIN
		dbo.INPROFSAL AS H with(nolock)  ON A.CODPROSAL =H.CODPROSAL   INNER JOIN  
		Integrations.CIMAHospital_Softland_Synch sync on sync.DataId = A.AUTO inner join
		Integrations.CIMAHospital_RelatedAdmission RA on RA.AdmissionnumberVie = A.NUMINGRES
		WHERE sync.State = 0 AND sync.Type = 4 -- 4 - ImageOrder 

	  /*Patologias  Hosp. y Ambulatorio */
	   UNION ALL
	   SELECT
		Sync.id,
		case SYnc.Type when 1 then 'Admission' when 2 then 'Patient' when 3 then 'LaboratoryOrder' when 4 then 'ImageOrder' when 5 then 'PathologyOrder' when 6 then 'SurgicalProcedureOrder' when 7 then 'NotSurgicalProcedureOrder' when 8 then 'InterconsultationOrder'  end Type,
		case SYnc.Action when 1 then 'Insert' when 2 then 'update' when 3 then 'delete' end Action,
		Sync.TransactionDate,
		Sync.State,
		rtrim(ltrim(A.CODSERIPS)) AS CodigoArticulo,
		'URG' as Bodega,
		A.canserips as Cantidad,
		'ND' as CodigoMedico,
		A.FECORDMED AS FechaSolicitud,
		'Baja' as Prioridad,
		'vie' as Comentario,
		A.[AUTO] as IdOrder,
		RTRIM(A.IPCODPACI) AS Identificacion, 
		RTRIM(B.IPNOMCOMP) AS NombrePaciente, 
		A.ESTSERIPS AS Estado,
		A.NUMINGRES AS AdmissionNumberVie,
		RA.AdmissionNumberLegacy		
		FROM  dbo.HCORDPATO AS A with(nolock)  INNER JOIN
		dbo.INPACIENT AS B with(nolock)  ON A.IPCODPACI = B.IPCODPACI INNER JOIN
		dbo.INCUPSIPS AS D with(nolock)  ON A.CODSERIPS = D .CODSERIPS INNER JOIN
		dbo.ADINGRESO AS E with(nolock)  ON A.IPCODPACI = E.IPCODPACI AND A.NUMINGRES = E.NUMINGRES INNER JOIN
		dbo.INPROFSAL AS H with(nolock)  ON A.CODPROSAL =H.CODPROSAL   INNER JOIN  
		Integrations.CIMAHospital_Softland_Synch sync on sync.DataId = A.AUTO inner join
		Integrations.CIMAHospital_RelatedAdmission RA on RA.AdmissionnumberVie = A.NUMINGRES
		WHERE sync.State = 0 AND sync.Type = 5 -- 5 - PathologyOrder
		UNION ALL
		SELECT  
		Sync.id,
			    case SYnc.Type when 1 then 'Admission' when 2 then 'Patient' when 3 then 'LaboratoryOrder' when 4 then 'ImageOrder' when 5 then 'PathologyOrder' when 6 then 'SurgicalProcedureOrder' when 7 then 'NotSurgicalProcedureOrder' when 8 then 'InterconsultationOrder'  end Type,
		case SYnc.Action when 1 then 'Insert' when 2 then 'update' when 3 then 'delete' end Action,
		Sync.TransactionDate,
		Sync.State,
		rtrim(ltrim(A.CODSERIPS)) AS CodigoArticulo,
		'URG' as Bodega,
		A.canserips as Cantidad,
		'ND' as CodigoMedico,
		A.FECORDMED AS FechaSolicitud,
		'Baja' as Prioridad,
		'vie' as Comentario,
		A.[AUTO] as IdOrder,
		RTRIM(A.IPCODPACI) AS Identificacion, 
		RTRIM(B.IPNOMCOMP) AS NombrePaciente, 
		A.ESTSERIPS AS Estado,
		A.NUMINGRES AS AdmissionNumberVie,
		RA.AdmissionNumberLegacy
		FROM  dbo.AMBORDPAT AS A with(nolock)  INNER JOIN
		dbo.INPACIENT AS B with(nolock)  ON A.IPCODPACI = B.IPCODPACI INNER JOIN
		dbo.INCUPSIPS AS D with(nolock)  ON A.CODSERIPS = D .CODSERIPS INNER JOIN
		dbo.ADINGRESO AS E with(nolock)  ON A.IPCODPACI = E.IPCODPACI AND A.NUMINGRES = E.NUMINGRES INNER JOIN
		dbo.INPROFSAL AS H with(nolock)  ON A.CODPROSAL =H.CODPROSAL   INNER JOIN  
		Integrations.CIMAHospital_Softland_Synch sync on sync.DataId = A.AUTO inner join
		Integrations.CIMAHospital_RelatedAdmission RA on RA.AdmissionnumberVie = A.NUMINGRES
		WHERE sync.State = 0 AND sync.Type = 5 -- 5 - PathologyOrder 

		/*Procedimiento QX */
	   UNION ALL
	   SELECT
		Sync.id,
			    case SYnc.Type when 1 then 'Admission' when 2 then 'Patient' when 3 then 'LaboratoryOrder' when 4 then 'ImageOrder' when 5 then 'PathologyOrder' when 6 then 'SurgicalProcedureOrder' when 7 then 'NotSurgicalProcedureOrder' when 8 then 'InterconsultationOrder'  end Type,
		case SYnc.Action when 1 then 'Insert' when 2 then 'update' when 3 then 'delete' end Action,
		Sync.TransactionDate,
		Sync.State,
		rtrim(ltrim(A.CODSERIPS)) AS CodigoArticulo,
		'URG' as Bodega,
		A.canserips as Cantidad,
		'ND' as CodigoMedico,
		A.FECORDMED AS FechaSolicitud,
		'Baja' as Prioridad,
		'vie' as Comentario,
		A.[AUTO] as IdOrder,
		RTRIM(A.IPCODPACI) AS Identificacion, 
		RTRIM(B.IPNOMCOMP) AS NombrePaciente, 
		A.ESTSERIPS AS Estado,
		A.NUMINGRES AS AdmissionNumberVie,
		RA.AdmissionNumberLegacy		
		FROM  dbo.HCORDPROQ AS A with(nolock)  INNER JOIN
		dbo.INPACIENT AS B with(nolock)  ON A.IPCODPACI = B.IPCODPACI INNER JOIN
		dbo.INCUPSIPS AS D with(nolock)  ON A.CODSERIPS = D .CODSERIPS INNER JOIN
		dbo.ADINGRESO AS E with(nolock)  ON A.IPCODPACI = E.IPCODPACI AND A.NUMINGRES = E.NUMINGRES INNER JOIN
		dbo.INPROFSAL AS H with(nolock)  ON A.CODPROSAL =H.CODPROSAL   INNER JOIN  
		Integrations.CIMAHospital_Softland_Synch sync on sync.DataId = A.AUTO inner join
		Integrations.CIMAHospital_RelatedAdmission RA on RA.AdmissionnumberVie = A.NUMINGRES
		WHERE sync.State = 0 AND sync.Type = 6 -- 6 - SurgicalProcedureOrder
	   
	   /*Procedimiento NO QX */
	   UNION ALL
	   SELECT
		Sync.id,
			    case SYnc.Type when 1 then 'Admission' when 2 then 'Patient' when 3 then 'LaboratoryOrder' when 4 then 'ImageOrder' when 5 then 'PathologyOrder' when 6 then 'SurgicalProcedureOrder' when 7 then 'NotSurgicalProcedureOrder' when 8 then 'InterconsultationOrder'  end Type,
		case SYnc.Action when 1 then 'Insert' when 2 then 'update' when 3 then 'delete' end Action,
		Sync.TransactionDate,
		Sync.State,
		rtrim(ltrim(A.CODSERIPS)) AS CodigoArticulo,
		'URG' as Bodega,
		A.canserips as Cantidad,
		'ND' as CodigoMedico,
		A.FECORDMED AS FechaSolicitud,
		'Baja' as Prioridad,
		'vie' as Comentario,
		A.[AUTO] as IdOrder,
		RTRIM(A.IPCODPACI) AS Identificacion, 
		RTRIM(B.IPNOMCOMP) AS NombrePaciente, 
		A.ESTSERIPS AS Estado,
		A.NUMINGRES AS AdmissionNumberVie,
		RA.AdmissionNumberLegacy		
		FROM  dbo.HCORDPRON AS A with(nolock)  INNER JOIN
		dbo.INPACIENT AS B with(nolock)  ON A.IPCODPACI = B.IPCODPACI INNER JOIN
		dbo.INCUPSIPS AS D with(nolock)  ON A.CODSERIPS = D .CODSERIPS INNER JOIN
		dbo.ADINGRESO AS E with(nolock)  ON A.IPCODPACI = E.IPCODPACI AND A.NUMINGRES = E.NUMINGRES INNER JOIN
		dbo.INPROFSAL AS H with(nolock)  ON A.CODPROSAL =H.CODPROSAL   INNER JOIN  
		Integrations.CIMAHospital_Softland_Synch sync on sync.DataId = A.AUTO inner join
		Integrations.CIMAHospital_RelatedAdmission RA on RA.AdmissionnumberVie = A.NUMINGRES
		WHERE sync.State = 0 AND sync.Type = 7 -- 7 - NotSurgicalProcedureOrder

		/*Interconsulta*/
	   UNION ALL
	   SELECT
		Sync.id,
			    case SYnc.Type when 1 then 'Admission' when 2 then 'Patient' when 3 then 'LaboratoryOrder' when 4 then 'ImageOrder' when 5 then 'PathologyOrder' when 6 then 'SurgicalProcedureOrder' when 7 then 'NotSurgicalProcedureOrder' when 8 then 'InterconsultationOrder'  end Type,
		case SYnc.Action when 1 then 'Insert' when 2 then 'update' when 3 then 'delete' end Action,
		Sync.TransactionDate,
		Sync.State,
		rtrim(ltrim(A.CODSERIPS)) AS CodigoArticulo,
		'URG' as Bodega,
		A.canserips as Cantidad,
		'ND' as CodigoMedico,
		A.FECORDMED AS FechaSolicitud,
		'Baja' as Prioridad,
		'vie' as Comentario,
		A.[AUTO] as IdOrder,
		RTRIM(A.IPCODPACI) AS Identificacion, 
		RTRIM(B.IPNOMCOMP) AS NombrePaciente, 
		A.ESTSERIPS AS Estado,
		A.NUMINGRES AS AdmissionNumberVie,
		RA.AdmissionNumberLegacy		
		FROM  dbo.HCORDPRON AS A with(nolock)  INNER JOIN
		dbo.INPACIENT AS B with(nolock)  ON A.IPCODPACI = B.IPCODPACI INNER JOIN
		dbo.INCUPSIPS AS D with(nolock)  ON A.CODSERIPS = D .CODSERIPS INNER JOIN
		dbo.ADINGRESO AS E with(nolock)  ON A.IPCODPACI = E.IPCODPACI AND A.NUMINGRES = E.NUMINGRES INNER JOIN
		dbo.INPROFSAL AS H with(nolock)  ON A.CODPROSAL =H.CODPROSAL   INNER JOIN  
		Integrations.CIMAHospital_Softland_Synch sync on sync.DataId = A.AUTO inner join
		Integrations.CIMAHospital_RelatedAdmission RA on RA.AdmissionnumberVie = A.NUMINGRES
		WHERE sync.State = 0 AND sync.Type = 8 -- 8 - InterconsultationOrder