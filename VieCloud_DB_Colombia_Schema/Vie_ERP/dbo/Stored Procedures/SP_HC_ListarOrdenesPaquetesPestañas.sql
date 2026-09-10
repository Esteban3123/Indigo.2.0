-- =============================================
-- Author:      <Yezid Garcia Medina>
-- Create Date: <22/07/2021>
-- Description: <SP Listar Ordenes Paquetes Pestañas: Diagnosticos, RIAS, Riesgos, Procedimientos Qx, Procedimientos No Qx>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarOrdenesPaquetesPestañas] 
(
@Pestaña Integer, 
@DiagnosticosPaciente varchar(MAX) , 
@ProcedimientosQx varchar(MAX) , 
@ProcedimientosNoQx varchar(MAX), 
@IDDESCRIPCIONRELACIONADAQx varchar(MAX) , 
@IDDESCRIPCIONRELACIONADANoQx varchar(MAX), 
@IdRiasCups Integer, 
@IdModeloHC Integer 
)
AS
BEGIN
	SET NOCOUNT ON;
 --- @Pestaña : 1=Diagnosticos, 2=RIAS, 3=Riesgos, 4=Procedimientos Qx , 5=Procedimientos No Qx

IF @Pestaña = 1  --- 1 = Diagnosticos
	Begin 
		WITH Datos as (
			SELECT distinct  Convert(BIT,0) As 'Seleccion',Rtrim(C.CODIGOSERVICIO) as 'CodigoServicio',D.SERIPSDASH,D.SERREASIT as 'ServicioRealizaSitio', CODDCIMED AS 'DCI', C.IDDESCRIPCIONRELACIONADA, Descriptions.Code + ' - ' + Descriptions.Name as DescripcionRelacionada,
				 CASE WHEN D.DESSERIPS is not null THEN RTRIM(D.DESSERIPS) WHEN RTRIM(E.DESPRODUC) is not null THEN RTRIM(E.DESPRODUC) ELSE RTRIM(F.DESESPECI) END AS 'Nombre Servicio'  ,
				 CASE WHEN D.TIPSERIPS IS NOT NULL THEN
		 				 case D.TIPSERIPS 
							 when 1 then 'Laboratorios' 
							 when 2 then 'Patologias' 
							 when 3 then 'Imágenes diagnósticas' 
							 when 4 then 'Procedimientos no quirúrgicos'  
							 when 5 then 'Procedimientos quirúrgicos' 
							 --when 6 then 'Interconsultas' 
							 --when 7 then 'Ninguno' 
							 --when 8 then 'Consulta externa' 
							 --when 9 then 'Hemocomponentes'
						  END
					  WHEN E.TIPPRODUC IS NOT NULL THEN
						 CASE E.TIPPRODUC 
							 when 1 then 'Medicamentos' 
							 when 2 then 'Insumos' 
							 when 3 then 'Medicamentos' 
						  END 
					 WHEN CODESPECI IS NOT NULL THEN 'Interconsultas'							
				END AS 'Descripcion Tipo Orden', 
				CASE WHEN D.TIPSERIPS IS NOT NULL THEN
		 				 case D.TIPSERIPS 
							 when 1 then '1' 
							 when 2 then '2' 
							 when 3 then '3' 
							 when 4 then '4'  
							 when 5 then '5'
						  END
					  WHEN E.TIPPRODUC IS NOT NULL THEN
						 CASE E.TIPPRODUC 
							 when 1 then '200' 
							 when 2 then '300' 
							 when 3 then '200' 
						  END 
					 WHEN CODESPECI IS NOT NULL THEN '6'							
				END AS 'Tipo Servicio'
				, RTRIM(A.NOMBRE) as 'Grupo', D.RequiresLaterality AS 'RequiereLateralidad', C.OrderQuantity, RTRIM(A.CODIGO) AS CodigoGrupo 
				, iif ((SELECT COUNT(*) FROM HCPLANDOC planC INNER JOIN HCPLANDOCCUPS planD ON planC.CODCONSEC = planD.IDHCPLANDOC WHERE planC.TIPDOCUME = 0 AND planD.CODSERIPS = C.CODIGOSERVICIO) >= 1, 2, 1) AS ConsentimientoInformado, C.ClinicalDataRelevant AS DatosClinicosRelevantes
			FROM HCPAQORDENESC A 
				INNER JOIN  HCPAQDIAGNOSD B ON A.ID = B.IDHCPAQORDENESC 
				INNER JOIN  HCPAQORDENESD C ON A.ID = C.IDHCPAQORDENESC 
				LEFT JOIN	INCUPSIPS d ON C.CODIGOSERVICIO = D.CODSERIPS
				LEFT JOIN	IHLISTPRO E ON C.CODIGOSERVICIO = E.CODPRODUC
				LEFT JOIN	INESPECIA F ON C.CODIGOSERVICIO = F.CODESPECI 
				left join contract.CUPSEntityContractDescriptions CupsDescriptions with(nolock) on  CupsDescriptions.Id = C.IDDESCRIPCIONRELACIONADA 
				left join contract.ContractDescriptions Descriptions with(nolock) on Descriptions.id = CupsDescriptions.ContractDescriptionId				
			where 
			A.ESTADO = 1 
			--AND	C.TIPOSERVICIO in  (SELECT Value FROM dbo.splitstring(@TipoServicio))
			AND B.CODDIAGNO in  (SELECT Value FROM dbo.splitstring(@DiagnosticosPaciente)) 
		) Select * from Datos WHERE [Tipo Servicio] is not NULL
	End
Else If @Pestaña = 2  --- 2 = RIAS
	Begin
		WITH Datos AS (
			SELECT distinct  Convert(BIT,0) As 'Seleccion',Rtrim(C.CODIGOSERVICIO) as 'CodigoServicio',D.SERIPSDASH,D.SERREASIT as 'ServicioRealizaSitio', CODDCIMED AS 'DCI', C.IDDESCRIPCIONRELACIONADA, Descriptions.Code + ' - ' + Descriptions.Name as DescripcionRelacionada,
				 CASE WHEN D.DESSERIPS is not null THEN RTRIM(D.DESSERIPS) WHEN RTRIM(E.DESPRODUC) is not null THEN RTRIM(E.DESPRODUC) ELSE RTRIM(F.DESESPECI) END AS 'Nombre Servicio'  ,
				 CASE WHEN D.TIPSERIPS IS NOT NULL THEN
		 				 case D.TIPSERIPS 
							 when 1 then 'Laboratorios' 
							 when 2 then 'Patologias' 
							 when 3 then 'Imágenes diagnósticas' 
							 when 4 then 'Procedimientos no quirúrgicos'  
							 when 5 then 'Procedimientos quirúrgicos' 
							 --when 6 then 'Interconsultas' 
							 --when 7 then 'Ninguno' 
							 --when 8 then 'Consulta externa' 
							 --when 9 then 'Hemocomponentes'
						  END
					  WHEN E.TIPPRODUC IS NOT NULL THEN
						 CASE E.TIPPRODUC 
							 when 1 then 'Medicamentos' 
							 when 2 then 'Insumos' 
							 when 3 then 'Medicamentos' 
						  END 
					 WHEN CODESPECI IS NOT NULL THEN 'Interconsultas'							
				END AS 'Descripcion Tipo Orden', 
				CASE WHEN D.TIPSERIPS IS NOT NULL THEN
		 				 case D.TIPSERIPS 
							 when 1 then '1' 
							 when 2 then '2' 
							 when 3 then '3' 
							 when 4 then '4'  
							 when 5 then '5' 
						  END
					  WHEN E.TIPPRODUC IS NOT NULL THEN
						 CASE E.TIPPRODUC 
							 when 1 then '200' 
							 when 2 then '300' 
							 when 3 then '200' 
						  END 
					 WHEN CODESPECI IS NOT NULL THEN '6'							
				END AS 'Tipo Servicio'
				, RTRIM(A.NOMBRE) as 'Grupo', D.RequiresLaterality AS 'RequiereLateralidad', C.OrderQuantity, RTRIM(A.CODIGO) AS CodigoGrupo  
				, iif ((SELECT COUNT(*) FROM HCPLANDOC planC INNER JOIN HCPLANDOCCUPS planD ON planC.CODCONSEC = planD.IDHCPLANDOC WHERE planC.TIPDOCUME = 0 AND planD.CODSERIPS = C.CODIGOSERVICIO) >= 1, 2, 1) AS ConsentimientoInformado, C.ClinicalDataRelevant AS DatosClinicosRelevantes
			FROM HCPAQORDENESC A 
				INNER JOIN  HCPAQORDENESD C ON A.ID = C.IDHCPAQORDENESC 
				LEFT JOIN	INCUPSIPS d ON C.CODIGOSERVICIO = D.CODSERIPS
				LEFT JOIN	IHLISTPRO E ON C.CODIGOSERVICIO = E.CODPRODUC
				LEFT JOIN	INESPECIA F ON C.CODIGOSERVICIO = F.CODESPECI 
				left join contract.CUPSEntityContractDescriptions CupsDescriptions with(nolock) on  CupsDescriptions.Id = C.IDDESCRIPCIONRELACIONADA 
				left join contract.ContractDescriptions Descriptions with(nolock) on Descriptions.id = CupsDescriptions.ContractDescriptionId
				INNER Join HCPAQRIASD I ON I.IDHCPAQORDENESC = A.ID
				INNER JOIN RIAS J On J.ID = I.IDRIAS
				INNER JOIN RIASCUPS K On K.IDRIAS = J.ID			
			where 
			A.ESTADO = 1 
			AND	K.ID = @IdRiasCups
		) Select * From Datos Where [Tipo Servicio] is not NULL
	End
Else IF @Pestaña = 3  --- 3 = Riesgos
	Begin
		WITH Datos AS (
			SELECT distinct  Convert(BIT,0) As 'Seleccion',Rtrim(C.CODIGOSERVICIO) as 'CodigoServicio',D.SERIPSDASH,D.SERREASIT as 'ServicioRealizaSitio', CODDCIMED AS 'DCI', C.IDDESCRIPCIONRELACIONADA, Descriptions.Code + ' - ' + Descriptions.Name as DescripcionRelacionada,
				 CASE WHEN D.DESSERIPS is not null THEN RTRIM(D.DESSERIPS) WHEN RTRIM(E.DESPRODUC) is not null THEN RTRIM(E.DESPRODUC) ELSE RTRIM(F.DESESPECI) END AS 'Nombre Servicio'  ,
				 CASE WHEN D.TIPSERIPS IS NOT NULL THEN
		 				 case D.TIPSERIPS 
							 when 1 then 'Laboratorios' 
							 when 2 then 'Patologias' 
							 when 3 then 'Imágenes diagnósticas' 
							 when 4 then 'Procedimientos no quirúrgicos'  
							 when 5 then 'Procedimientos quirúrgicos' 
							 --when 6 then 'Interconsultas' 
							 --when 7 then 'Ninguno' 
							 --when 8 then 'Consulta externa' 
							 --when 9 then 'Hemocomponentes'
						  END
					  WHEN E.TIPPRODUC IS NOT NULL THEN
						 CASE E.TIPPRODUC 
							 when 1 then 'Medicamentos' 
							 when 2 then 'Insumos' 
							 when 3 then 'Medicamentos' 
						  END 
					 WHEN CODESPECI IS NOT NULL THEN 'Interconsultas'							
				END AS 'Descripcion Tipo Orden', 
				CASE WHEN D.TIPSERIPS IS NOT NULL THEN
		 				 case D.TIPSERIPS 
							 when 1 then '1' 
							 when 2 then '2' 
							 when 3 then '3' 
							 when 4 then '4'  
							 when 5 then '5' 
						  END
					  WHEN E.TIPPRODUC IS NOT NULL THEN
						 CASE E.TIPPRODUC 
							 when 1 then '200' 
							 when 2 then '300' 
							 when 3 then '200' 
						  END 
					 WHEN CODESPECI IS NOT NULL THEN '6'							
				END AS 'Tipo Servicio'
				, RTRIM(A.NOMBRE) as 'Grupo', D.RequiresLaterality AS 'RequiereLateralidad', C.OrderQuantity, RTRIM(A.CODIGO) AS CodigoGrupo  
				, iif ((SELECT COUNT(*) FROM HCPLANDOC planC INNER JOIN HCPLANDOCCUPS planD ON planC.CODCONSEC = planD.IDHCPLANDOC WHERE planC.TIPDOCUME = 0 AND planD.CODSERIPS = C.CODIGOSERVICIO) >= 1, 2, 1) AS ConsentimientoInformado, C.ClinicalDataRelevant AS DatosClinicosRelevantes
			FROM HCPAQORDENESC A 			
				INNER JOIN  HCPAQORDENESD C ON A.ID = C.IDHCPAQORDENESC 
				LEFT JOIN	INCUPSIPS d ON C.CODIGOSERVICIO = D.CODSERIPS
				LEFT JOIN	IHLISTPRO E ON C.CODIGOSERVICIO = E.CODPRODUC
				LEFT JOIN	INESPECIA F ON C.CODIGOSERVICIO = F.CODESPECI 
				left join contract.CUPSEntityContractDescriptions CupsDescriptions with(nolock) on  CupsDescriptions.Id = C.IDDESCRIPCIONRELACIONADA 
				left join contract.ContractDescriptions Descriptions with(nolock) on Descriptions.id = CupsDescriptions.ContractDescriptionId
				INNER JOIN HCPAQRIESGOSD L ON L.IDHCPAQORDENESC = A.ID 
				INNER JOIN PRHCEXPRES M  ON M.ID = L.IDPRHCEXPRES 
				INNER JOIN PRMODELOHC N ON N.ID = M.IDMODELOHC 

			where 
			A.ESTADO = 1 
			AND	N.ID = @IdModeloHC 	                                
		) Select * From Datos Where [Tipo Servicio] is not NULL
	End
Else If @Pestaña = 4 --- 4 = Procedimientos Qx
	Begin
	    WITH Datos AS (
			SELECT distinct  Convert(BIT,0) As 'Seleccion',Rtrim(C.CODIGOSERVICIO) as 'CodigoServicio',D.SERIPSDASH,D.SERREASIT as 'ServicioRealizaSitio', CODDCIMED AS 'DCI', C.IDDESCRIPCIONRELACIONADA, Descriptions.Code + ' - ' + Descriptions.Name as DescripcionRelacionada,
				 CASE WHEN D.DESSERIPS is not null THEN RTRIM(D.DESSERIPS) WHEN RTRIM(E.DESPRODUC) is not null THEN RTRIM(E.DESPRODUC) ELSE RTRIM(F.DESESPECI) END AS 'Nombre Servicio'  ,
				 CASE WHEN D.TIPSERIPS IS NOT NULL THEN
		 				 case D.TIPSERIPS 
							 when 1 then 'Laboratorios' 
							 when 2 then 'Patologias' 
							 when 3 then 'Imágenes diagnósticas' 
							 when 4 then 'Procedimientos no quirúrgicos'  
							 when 5 then 'Procedimientos quirúrgicos' 
							 --when 6 then 'Interconsultas' 
							 --when 7 then 'Ninguno' 
							 --when 8 then 'Consulta externa' 
							 --when 9 then 'Hemocomponentes'
						  END
					  WHEN E.TIPPRODUC IS NOT NULL THEN
						 CASE E.TIPPRODUC 
							 when 1 then 'Medicamentos' 
							 when 2 then 'Insumos' 
							 when 3 then 'Medicamentos' 
						  END 
					 WHEN CODESPECI IS NOT NULL THEN 'Interconsultas'							
				END AS 'Descripcion Tipo Orden', 
				CASE WHEN D.TIPSERIPS IS NOT NULL THEN
		 				 case D.TIPSERIPS 
							 when 1 then '1' 
							 when 2 then '2' 
							 when 3 then '3' 
							 when 4 then '4'  
							 when 5 then '5' 
						  END
					  WHEN E.TIPPRODUC IS NOT NULL THEN
						 CASE E.TIPPRODUC 
							 when 1 then '200' 
							 when 2 then '300' 
							 when 3 then '200' 
						  END 
					 WHEN CODESPECI IS NOT NULL THEN '6'							
				END AS 'Tipo Servicio'
				, RTRIM(A.NOMBRE) as 'Grupo', D.RequiresLaterality AS 'RequiereLateralidad', C.OrderQuantity, RTRIM(A.CODIGO) AS CodigoGrupo   
				, iif ((SELECT COUNT(*) FROM HCPLANDOC planC INNER JOIN HCPLANDOCCUPS planD ON planC.CODCONSEC = planD.IDHCPLANDOC WHERE planC.TIPDOCUME = 0 AND planD.CODSERIPS = C.CODIGOSERVICIO) >= 1, 2, 1) AS ConsentimientoInformado, C.ClinicalDataRelevant AS DatosClinicosRelevantes
			FROM HCPAQORDENESC A 			
				INNER JOIN  HCPAQORDENESD C ON A.ID = C.IDHCPAQORDENESC 
				LEFT JOIN	INCUPSIPS d ON C.CODIGOSERVICIO = D.CODSERIPS
				LEFT JOIN	IHLISTPRO E ON C.CODIGOSERVICIO = E.CODPRODUC
				LEFT JOIN	INESPECIA F ON C.CODIGOSERVICIO = F.CODESPECI 
				left join contract.CUPSEntityContractDescriptions CupsDescriptions with(nolock) on  CupsDescriptions.Id = C.IDDESCRIPCIONRELACIONADA 
				left join contract.ContractDescriptions Descriptions with(nolock) on Descriptions.id = CupsDescriptions.ContractDescriptionId
				Inner join HCPAQORDENPROQXD G0 ON A.ID = G0.IDHCPAQORDENESC 
				Inner Join INCUPSIPS G1 ON G1.CODSERIPS = G0.CODIGOSERVICIO
				left join contract.CUPSEntityContractDescriptions G2 with(nolock) on  G2.Id = G0.IDDESCRIPCIONRELACIONADA 
				left join contract.ContractDescriptions G3 with(nolock) on G3.id = G2.ContractDescriptionId
			
			where 
			A.ESTADO = 1 
			AND (  G0.CODIGOSERVICIO in  (SELECT Value FROM dbo.splitstring(@ProcedimientosQx))  AND (G0.IDDESCRIPCIONRELACIONADA IS NULL OR G0.IDDESCRIPCIONRELACIONADA in  (SELECT Value FROM dbo.splitstring(@IDDESCRIPCIONRELACIONADAQx)) ) )
		) Select * From Datos Where [Tipo Servicio] is not NULL
	End
Else If @Pestaña = 5 --- 5 = Procedimientos No Qx
	Begin
		WITH Datos AS (
			SELECT distinct  Convert(BIT,0) As 'Seleccion',Rtrim(C.CODIGOSERVICIO) as 'CodigoServicio',D.SERIPSDASH,D.SERREASIT as 'ServicioRealizaSitio', CODDCIMED AS 'DCI', C.IDDESCRIPCIONRELACIONADA, Descriptions.Code + ' - ' + Descriptions.Name as DescripcionRelacionada,
				 CASE WHEN D.DESSERIPS is not null THEN RTRIM(D.DESSERIPS) WHEN RTRIM(E.DESPRODUC) is not null THEN RTRIM(E.DESPRODUC) ELSE RTRIM(F.DESESPECI) END AS 'Nombre Servicio'  ,
				 CASE WHEN D.TIPSERIPS IS NOT NULL THEN
		 				 case D.TIPSERIPS 
							 when 1 then 'Laboratorios' 
							 when 2 then 'Patologias' 
							 when 3 then 'Imágenes diagnósticas' 
							 when 4 then 'Procedimientos no quirúrgicos'  
							 when 5 then 'Procedimientos quirúrgicos' 
							 --when 6 then 'Interconsultas' 
							 --when 7 then 'Ninguno' 
							 --when 8 then 'Consulta externa' 
							 --when 9 then 'Hemocomponentes'
						  END
					  WHEN E.TIPPRODUC IS NOT NULL THEN
						 CASE E.TIPPRODUC 
							 when 1 then 'Medicamentos' 
							 when 2 then 'Insumos' 
							 when 3 then 'Medicamentos' 
						  END 
					 WHEN CODESPECI IS NOT NULL THEN 'Interconsultas'							
				END AS 'Descripcion Tipo Orden', 
				CASE WHEN D.TIPSERIPS IS NOT NULL THEN
		 				 case D.TIPSERIPS 
							 when 1 then '1' 
							 when 2 then '2' 
							 when 3 then '3' 
							 when 4 then '4'  
							 when 5 then '5' 
						  END
					  WHEN E.TIPPRODUC IS NOT NULL THEN
						 CASE E.TIPPRODUC 
							 when 1 then '200' 
							 when 2 then '300' 
							 when 3 then '200' 
						  END 
					 WHEN CODESPECI IS NOT NULL THEN '6'							
				END AS 'Tipo Servicio'
				, RTRIM(A.NOMBRE) as 'Grupo', D.RequiresLaterality AS 'RequiereLateralidad', C.OrderQuantity, RTRIM(A.CODIGO) AS CodigoGrupo   
				, iif ((SELECT COUNT(*) FROM HCPLANDOC planC INNER JOIN HCPLANDOCCUPS planD ON planC.CODCONSEC = planD.IDHCPLANDOC WHERE planC.TIPDOCUME = 0 AND planD.CODSERIPS = C.CODIGOSERVICIO) >= 1, 2, 1) AS ConsentimientoInformado, C.ClinicalDataRelevant AS DatosClinicosRelevantes
			FROM HCPAQORDENESC A 			
				INNER JOIN  HCPAQORDENESD C ON A.ID = C.IDHCPAQORDENESC 
				LEFT JOIN	INCUPSIPS d ON C.CODIGOSERVICIO = D.CODSERIPS
				LEFT JOIN	IHLISTPRO E ON C.CODIGOSERVICIO = E.CODPRODUC
				LEFT JOIN	INESPECIA F ON C.CODIGOSERVICIO = F.CODESPECI 
				left join contract.CUPSEntityContractDescriptions CupsDescriptions with(nolock) on  CupsDescriptions.Id = C.IDDESCRIPCIONRELACIONADA 
				left join contract.ContractDescriptions Descriptions with(nolock) on Descriptions.id = CupsDescriptions.ContractDescriptionId
				Inner join HCPAQORDENPRONOQXD H0 ON A.ID = H0.IDHCPAQORDENESC 
				Inner Join INCUPSIPS H1 ON H1.CODSERIPS = H0.CODIGOSERVICIO
				left join contract.CUPSEntityContractDescriptions H2 with(nolock) on  H2.Id = H0.IDDESCRIPCIONRELACIONADA 
				left join contract.ContractDescriptions H3 with(nolock) on H3.id = H2.ContractDescriptionId		
			where 
			A.ESTADO = 1 
			AND ( H0.CODIGOSERVICIO in  (SELECT Value FROM dbo.splitstring(@ProcedimientosNoQx))  AND (H0.IDDESCRIPCIONRELACIONADA IS NULL OR H0.IDDESCRIPCIONRELACIONADA in  (SELECT Value FROM dbo.splitstring(@IDDESCRIPCIONRELACIONADANoQx)) ) )
		) Select * From Datos Where [Tipo Servicio] is not NULL
	End

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento almacenado que lista los servicios y procedimientos disponibles en los paquetes de órdenes médicas de historia clínica, organizados por pestañas temáticas: Diagnósticos, RIAS, Riesgos, Procedimientos Quirúrgicos y Procedimientos No Quirúrgicos. Según la pestaña seleccionada, filtra los ítems del paquete de órdenes (HCPAQORDENESD) cruzando con los diagnósticos del paciente (HCPAQDIAGNOSD), los códigos CUPS/IPS (INCUPSIPS), medicamentos e insumos (IHLISTPRO) y especialidades médicas (INESPECIA), para presentar el nombre del servicio, tipo de orden (laboratorio, imagen, procedimiento quirúrgico, medicamento, interconsulta, entre otros), grupo al que pertenece, si requiere lateralidad, cantidad de la orden, si exige consentimiento informado, datos clínicos relevantes y la descripción del concepto de contrato asociado (ContractDescriptions). Se usa en la interfaz de prescripción médica por paquetes para que el profesional de salud seleccione servicios sugeridos según el diagnóstico o ruta de atención del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarOrdenesPaquetesPestañas';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarOrdenesPaquetesPestañas';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los servicios de paquetes de órdenes asociados a una historia clínica filtrados por una de cinco pestañas: diagnósticos, RIAS, riesgos, procedimientos quirúrgicos o no quirúrgicos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarOrdenesPaquetesPestañas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro de pestaña debe ser 1, 2, 3, 4 o 5; valores fuera de ese rango no devuelven resultados.; Para pestaña 1 se requiere lista de diagnósticos separada por delimitador compatible con dbo.splitstring.; Para pestaña 2 se requiere identificador de RIAS-CUPS válido.; Para pestaña 3 se requiere identificador de modelo de historia clínica válido.; Para pestaña 4 se requieren listas de códigos de procedimientos quirúrgicos y opcionalmente de descripciones contractuales relacionadas.; Para pestaña 5 se requieren listas de códigos de procedimientos no quirúrgicos y opcionalmente de descripciones contractuales relacionadas.; Solo se consideran paquetes de órdenes con ESTADO = 1 (activos).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarOrdenesPaquetesPestañas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se devuelven servicios pertenecientes a paquetes de órdenes con ESTADO = 1.; Las filas cuyo tipo de servicio no pueda determinarse (no es CUPS-IPS, ni producto, ni especialidad) son excluidas del resultado.; El indicador de consentimiento informado siempre vale 2 si hay plantilla documental TIPDOCUME=0 asociada al servicio, y 1 en caso contrario.; El nombre del servicio se resuelve en orden de prioridad: descripción CUPS-IPS, luego producto, y finalmente especialidad.; Las pestañas son mutuamente excluyentes; se ejecuta una sola consulta por invocación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarOrdenesPaquetesPestañas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando @Pestaña=1 se retornan servicios de paquetes cuyos diagnósticos están en la lista @DiagnosticosPaciente y el paquete está activo (ESTADO=1).; [RETURN_RESULT] resultset: Cuando @Pestaña=2 se retornan servicios de paquetes vinculados al RIAS-CUPS indicado por @IdRiasCups con paquete activo.; [RETURN_RESULT] resultset: Cuando @Pestaña=3 se retornan servicios de paquetes asociados a riesgos del modelo de HC @IdModeloHC con paquete activo.; [RETURN_RESULT] resultset: Cuando @Pestaña=4 se retornan procedimientos quirúrgicos del paquete cuyo CODIGOSERVICIO está en @ProcedimientosQx y cuya IDDESCRIPCIONRELACIONADA es NULL o está en @IDDESCRIPCIONRELACIONADAQx.; [RETURN_RESULT] resultset: Cuando @Pestaña=5 se retornan procedimientos no quirúrgicos del paquete cuyo CODIGOSERVICIO está en @ProcedimientosNoQx y cuya IDDESCRIPCIONRELACIONADA es NULL o está en @IDDESCRIPCIONRELACIONADANoQx.; [RETURN_RESULT] resultset: En todas las pestañas se descartan filas cuyo ''Tipo Servicio'' resulte NULL (servicio no clasificable como CUPS-IPS, producto ni especialidad).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarOrdenesPaquetesPestañas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Pestaña = 1 → Filtra por diagnósticos del paciente uniendo HCPAQDIAGNOSD.; si @Pestaña = 2 → Filtra por RIAS-CUPS uniendo HCPAQRIASD, RIAS y RIASCUPS.; si @Pestaña = 3 → Filtra por modelo de HC uniendo HCPAQRIESGOSD, PRHCEXPRES y PRMODELOHC.; si @Pestaña = 4 → Filtra por procedimientos quirúrgicos uniendo HCPAQORDENPROQXD y validando descripción contractual.; si @Pestaña = 5 → Filtra por procedimientos no quirúrgicos uniendo HCPAQORDENPRONOQXD y validando descripción contractual.; si D.TIPSERIPS no es nulo → Clasifica el servicio según TIPSERIPS: 1=Laboratorios, 2=Patologías, 3=Imágenes diagnósticas, 4=Procedimientos no quirúrgicos, 5=Procedimientos quirúrgicos. else Si E.TIPPRODUC no es nulo se clasifica como Medicamentos (1 o 3) o Insumos (2); si CODESPECI no es nulo se clasifica como Interconsultas.; si Existe al menos un HCPLANDOC con TIPDOCUME=0 vinculado al CODIGOSERVICIO mediante HCPLANDOCCUPS → ConsentimientoInformado = 2 (requiere consentimiento). else ConsentimientoInformado = 1.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarOrdenesPaquetesPestañas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCPAQORDENESC; dbo.HCPAQORDENESD; dbo.HCPAQDIAGNOSD; dbo.HCPAQRIASD; dbo.RIAS; dbo.RIASCUPS; dbo.HCPAQRIESGOSD; dbo.PRHCEXPRES; dbo.PRMODELOHC; dbo.HCPAQORDENPROQXD; dbo.HCPAQORDENPRONOQXD; dbo.INCUPSIPS; dbo.IHLISTPRO; dbo.INESPECIA; dbo.HCPLANDOC; dbo.HCPLANDOCCUPS; contract.CUPSEntityContractDescriptions; contract.ContractDescriptions; dbo.splitstring', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarOrdenesPaquetesPestañas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarOrdenesPaquetesPestañas';
-- GO
