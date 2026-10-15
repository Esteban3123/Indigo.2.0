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