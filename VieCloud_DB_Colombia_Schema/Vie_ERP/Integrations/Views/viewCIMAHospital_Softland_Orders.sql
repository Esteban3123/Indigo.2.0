

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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de integración que consolida todas las órdenes médicas pendientes de sincronización desde Indigo Vie Cloud hacia el ERP Softland, pasando por el sistema legado CIMA Hospital. Combina órdenes de laboratorio, imágenes diagnósticas, patología, procedimientos quirúrgicos y no quirúrgicos, interconsultas tanto hospitalarias como ambulatorias, enriqueciendo cada orden con los datos del paciente (cédula, nombre), el número de ingreso en Vie y su equivalente en el sistema legado, el código de servicio CUPS, la cantidad solicitada y la fecha de la orden médica. Solo expone registros cuya sincronización aún no ha sido procesada (estado pendiente, tipo = orden de laboratorio o imagen u otros tipos de orden), permitiendo que el proceso de integración contable-administrativa envíe a Softland los consumos clínicos generados durante la atención del paciente.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'VIEW', @level1name = N'viewCIMAHospital_Softland_Orders';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'VIEW', @level1name = N'viewCIMAHospital_Softland_Orders';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en una sola lista las órdenes médicas pendientes de sincronización (laboratorio, imágenes, patología, procedimientos QX/no QX e interconsulta) entre el HIS y el ERP Softland, enriquecidas con datos del paciente y de la admisión legada.', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'viewCIMAHospital_Softland_Orders';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en Integrations.CIMAHospital_Softland_Synch con State = 0 (pendiente) cuyo DataId coincida con el AUTO de la orden.; El Type del registro de sincronización debe corresponder al tipo de orden consultada (3=Lab, 4=Imagen, 5=Patología, 6=QX, 7=NoQX, 8=Interconsulta).; La admisión asociada (NUMINGRES) debe estar registrada en Integrations.CIMAHospital_RelatedAdmission para obtener el AdmissionNumberLegacy.; El paciente debe existir en INPACIENT, el servicio en INCUPSIPS, el ingreso en ADINGRESO y el profesional solicitante en INPROFSAL.', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'viewCIMAHospital_Softland_Orders';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Toda orden expuesta tiene asociada una admisión con equivalencia legada en CIMAHospital_RelatedAdmission (INNER JOIN obligatorio).; Solo se exponen órdenes pendientes de sincronizar (State = 0).; Los campos Bodega (''URG''), CodigoMedico (''ND''), Prioridad (''Baja'') y Comentario (''vie'') se entregan siempre con valores fijos, independientemente del origen de la orden.; El tipo de orden expuesto siempre corresponde al Type registrado en la tabla de sincronización; no se infiere de la tabla origen.; Las órdenes ambulatorias solo se exponen para laboratorio, imágenes y patología; los procedimientos QX, no QX e interconsulta solo provienen del ámbito hospitalario.', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'viewCIMAHospital_Softland_Orders';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Admisión (hospitalaria y ambulatoria); Paciente; Orden de laboratorio; Orden de imágenes diagnósticas; Orden de patología; Procedimiento quirúrgico; Procedimiento no quirúrgico; Interconsulta; Profesional de la salud; Sincronización HIS-ERP (Softland); Equivalencia de admisión legacy (CIMA Hospital)', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'viewCIMAHospital_Softland_Orders';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Devuelve únicamente registros con sync.State = 0; las órdenes ya sincronizadas o con otro estado son excluidas.', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'viewCIMAHospital_Softland_Orders';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si sync.Type = 3 sobre HCORDLABO o AMBORDLAB → Se etiqueta la orden como ''LaboratoryOrder'' (hospitalario o ambulatorio).; si sync.Type = 4 sobre HCORDIMAG o AMBORDIMA → Se etiqueta la orden como ''ImageOrder'' (hospitalario o ambulatorio).; si sync.Type = 5 sobre HCORDPATO o AMBORDPAT → Se etiqueta la orden como ''PathologyOrder'' (hospitalario o ambulatorio).; si sync.Type = 6 sobre HCORDPROQ → Se etiqueta la orden como ''SurgicalProcedureOrder''.; si sync.Type = 7 sobre HCORDPRON → Se etiqueta la orden como ''NotSurgicalProcedureOrder''.; si sync.Type = 8 sobre HCORDPRON → Se etiqueta la orden como ''InterconsultationOrder'' (reutiliza la misma tabla de procedimientos no quirúrgicos).; si Action de la sincronización → Se traduce 1→''Insert'', 2→''update'', 3→''delete''.', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'viewCIMAHospital_Softland_Orders';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDLABO; dbo.AMBORDLAB; dbo.HCORDIMAG; dbo.AMBORDIMA; dbo.HCORDPATO; dbo.AMBORDPAT; dbo.HCORDPROQ; dbo.HCORDPRON; dbo.INPACIENT; dbo.INCUPSIPS; dbo.ADINGRESO; dbo.INPROFSAL; Integrations.CIMAHospital_Softland_Synch; Integrations.CIMAHospital_RelatedAdmission', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'viewCIMAHospital_Softland_Orders';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'viewCIMAHospital_Softland_Orders';
GO
