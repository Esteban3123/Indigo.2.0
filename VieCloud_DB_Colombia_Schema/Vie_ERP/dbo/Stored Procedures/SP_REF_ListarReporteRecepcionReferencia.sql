-- =============================================
-- Author:		<Yezid Garcia Medina, Desarrollador Junior>
-- Create date: <06 Septiembre de 2019>
-- Description:	<Listar Reporte Recepción Referencias>
-- =============================================
CREATE PROCEDURE [dbo].[SP_REF_ListarReporteRecepcionReferencia]
(
	@Centro varchar(20),
	@FechaInicio Datetime ,
	@FechaFin Datetime,
	@TipoVista bit
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	IF @TipoVista = 1
		Begin

			SELECT 
				B.FECREGIS AS 'Fecha solicitud'  ,
				B.ID AS 'ID solicitud',
				CASE
					WHEN IPTIPODOC = 1 THEN 'C.C.'
					WHEN IPTIPODOC = 2 THEN 'C.E.'
					WHEN IPTIPODOC = 3 THEN 'T.I.'
					WHEN IPTIPODOC = 4 THEN 'Registro Civil'
					WHEN IPTIPODOC = 5 THEN 'Pasaporte'
					WHEN IPTIPODOC = 6 THEN 'Adulto Sin Identificación'
					WHEN IPTIPODOC = 7 THEN 'Menor Sin Identificación'
					WHEN IPTIPODOC = 8 THEN 'Número único de identificación personal'
				END AS 'Tipo identificacion' ,
				RTRIM(A.IPCODPACI) AS 'Numero de identificacion' ,
				RTRIM(IPNOMCOMP) AS 'Nombres y Apellidos' ,
				(cast(datediff(dd, IPFECNACI ,[Common].[GETDATE]()) / 365.25 as int))  AS 'Edad' ,
				RTRIM(C.CODENTIDA) + ' - ' + RTRIM(C.NOMENTIDA) AS 'EPS' ,
				RTRIM(D.UBINOMBRE) AS 'Ciudad de origen' ,		
				RTRIM(G.CODIGOIPS) + ' - ' + RTRIM(G.DSCRIPIPS)AS 'IPS Emisora' ,
				RTRIM(E.CODESPECI) + ' - ' + RTRIM(E.DESESPECI) AS 'Especialidad que remite' ,
				RTRIM(B.OBSERVACION) AS 'Observacion' ,
				CASE
					WHEN I.ESTADO = 1 THEN 'Aceptado'
					WHEN I.ESTADO = 2 THEN 'No Aceptado'
					WHEN I.ESTADO = 3 THEN 'Sin Definir Conducta'
					WHEN I.ESTADO = 4 THEN 'Cancelado'			
				END AS 'Gestion' ,
				CASE
				WHEN I.ESTADO = 2 THEN RTRIM(J.CODIGO) + ' - ' + RTRIM(J.DESCRIPCION) 
				ELSE 'N/A'
				END AS 'Motivo' ,
				RTRIM(I.OBSERVACION) AS 'Observacion Registro' ,
				RTRIM(K.NOMMEDICO) AS 'Profesional' ,
				RTRIM(H.UFUCODIGO) + ' - ' + RTRIM(H.UFUDESCRI) AS 'Unidad Funcional' ,
				RTRIM(F.CODESPECI) + ' - ' + RTRIM(F.DESESPECI) AS 'Especialidad a la que remite' ,
				I.FECREGIS AS 'Fecha recepcion IPS y/o contrareferencia' ,
				DATEDIFF(day, B.FECREGIS, I.FECREGIS) AS 'Dias entre solicitud y gestion' ,		
				CASE 
					WHEN B.ESTADO = 3 AND L.FECALTPAC IS NOT NULL  THEN 'Cerrado'
					ELSE 'Abierto' 
				END 'Estado' 

			FROM 
				RCPACIENREF AS A with (nolock)
				INNER JOIN HCREFRECEP AS B with (nolock) ON A.IPCODPACI = B.IPCODPACI
				INNER JOIN INENTIDAD  AS C with (nolock) ON A.CODENTIDA = C.CODENTIDA
				INNER JOIN INUBICACI  AS D with (nolock) ON A.AUUBICACI = D.AUUBICACI
				INNER JOIN INESPECIA  AS E with (nolock) ON B.INESPECIAIDREMITE = E.CODESPECI
				INNER JOIN INESPECIA  AS F with (nolock) ON B.INESPECIAIDAREMITIR = F.CODESPECI
				INNER JOIN ADCONTIPS AS G with (nolock) ON B.ADCONTIPSID = G.CODIGOIPS
				INNER JOIN INUNIFUNC AS H with (nolock) ON B.INUNIFUNCID = H.UFUCODIGO
				INNER JOIN ADCENATEN AS M with (nolock) ON B.ADCENATENID = M.CODCENATE
				LEFT JOIN HCREFRECDE AS I with (nolock) ON B.ID = I.HCREFRECEPID 
				LEFT JOIN HCMOTREFREC AS J with (nolock) ON I.HCMOTREFRECID = J.ID 
				LEFT JOIN INPROFSAL AS K with (nolock) ON I.MEDRECIB =  K.CODPROSAL
				LEFT JOIN HCREGEGRE AS L with (nolock) ON B.NUMINGRES =  L.NUMINGRES
				
			WHERE 
				B.ADCENATENID IN (SELECT Value FROM dbo.SplitString(@Centro))
				AND
				B.FECREGIS BETWEEN  @FechaInicio  AND @FechaFin

		End
	ELSE
		Begin 

			SELECT 
				B.FECREGIS AS 'Fecha solicitud'  ,
				B.ID AS 'ID solicitud',
				CASE
					WHEN IPTIPODOC = 1 THEN 'C.C.'
					WHEN IPTIPODOC = 2 THEN 'C.E.'
					WHEN IPTIPODOC = 3 THEN 'T.I.'
					WHEN IPTIPODOC = 4 THEN 'Registro Civil'
					WHEN IPTIPODOC = 5 THEN 'Pasaporte'
					WHEN IPTIPODOC = 6 THEN 'Adulto Sin Identificación'
					WHEN IPTIPODOC = 7 THEN 'Menor Sin Identificación'
					WHEN IPTIPODOC = 8 THEN 'Número único de identificación personal'
				END AS 'Tipo identificacion' ,
				RTRIM(A.IPCODPACI) AS 'Numero de identificacion' ,
				RTRIM(IPNOMCOMP) AS 'Nombres y Apellidos' ,
				(cast(datediff(dd, IPFECNACI ,[Common].[GETDATE]()) / 365.25 as int))  AS 'Edad' ,
				RTRIM(C.CODENTIDA) + ' - ' + RTRIM(C.NOMENTIDA) AS 'EPS' ,
				RTRIM(D.UBINOMBRE) AS 'Ciudad de origen' ,		
				RTRIM(G.CODIGOIPS) + ' - ' + RTRIM(G.DSCRIPIPS)AS 'IPS Emisora' ,
				RTRIM(E.CODESPECI) + ' - ' + RTRIM(E.DESESPECI) AS 'Especialidad que remite' ,
				RTRIM(B.OBSERVACION) AS 'Observacion' ,
				CASE
					WHEN I.ESTADO = 1 THEN 'Aceptado'
					WHEN I.ESTADO = 2 THEN 'No Aceptado'
					WHEN I.ESTADO = 3 THEN 'Sin Definir Conducta'
					WHEN I.ESTADO = 4 THEN 'Cancelado'			
				END AS 'Gestion' ,
				CASE
				WHEN I.ESTADO = 2 THEN RTRIM(J.CODIGO) + ' - ' + RTRIM(J.DESCRIPCION) 
				ELSE 'N/A'
				END AS 'Motivo' ,
				RTRIM(I.OBSERVACION) AS 'Observacion Registro' ,
				RTRIM(K.NOMMEDICO) AS 'Profesional' ,
				RTRIM(H.UFUCODIGO) + ' - ' + RTRIM(H.UFUDESCRI) AS 'Unidad Funcional' ,
				RTRIM(F.CODESPECI) + ' - ' + RTRIM(F.DESESPECI) AS 'Especialidad a la que remite' ,
				I.FECREGIS AS 'Fecha recepcion IPS y/o contrareferencia' ,
				DATEDIFF(day, B.FECREGIS, I.FECREGIS) AS 'Dias entre solicitud y gestion' ,		
				CASE 
					WHEN B.ESTADO = 3 AND L.FECALTPAC IS NOT NULL  THEN 'Cerrado'
					ELSE 'Abierto' 
				END 'Estado' 

			FROM 
				RCPACIENREF AS A with (nolock)
				INNER JOIN HCREFRECEP AS B with (nolock) ON A.IPCODPACI = B.IPCODPACI
				INNER JOIN INENTIDAD  AS C with (nolock) ON A.CODENTIDA = C.CODENTIDA
				INNER JOIN INUBICACI  AS D with (nolock) ON A.AUUBICACI = D.AUUBICACI
				INNER JOIN INESPECIA  AS E with (nolock) ON B.INESPECIAIDREMITE = E.CODESPECI
				INNER JOIN INESPECIA  AS F with (nolock) ON B.INESPECIAIDAREMITIR = F.CODESPECI
				INNER JOIN ADCONTIPS AS G with (nolock) ON B.ADCONTIPSID = G.CODIGOIPS
				INNER JOIN INUNIFUNC AS H with (nolock) ON B.INUNIFUNCID = H.UFUCODIGO
				INNER JOIN ADCENATEN AS M with (nolock) ON B.ADCENATENID = M.CODCENATE
				LEFT JOIN (	SELECT I1.HCREFRECEPID, MAX(I1.ID) ULTIMOID FROM HCREFRECEP AS B1 INNER JOIN HCREFRECDE AS I1 ON I1.HCREFRECEPID = B1.ID  
				GROUP BY I1.HCREFRECEPID  ) T ON B.ID = T.HCREFRECEPID
				LEFT JOIN HCREFRECDE AS I with (nolock) ON I.ID = T.ULTIMOID
				LEFT JOIN HCMOTREFREC AS J with (nolock) ON I.HCMOTREFRECID = J.ID 
				LEFT JOIN INPROFSAL AS K with (nolock) ON I.MEDRECIB =  K.CODPROSAL
				LEFT JOIN HCREGEGRE AS L with (nolock) ON B.NUMINGRES =  L.NUMINGRES
				
			WHERE 
				B.ADCENATENID IN (SELECT Value FROM dbo.SplitString(@Centro))
				AND
				B.FECREGIS BETWEEN  @FechaInicio  AND @FechaFin

		End 	

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte de recepción de referencias y contrarremisiones médicas para uno o varios centros de atención en un rango de fechas. Integra datos del paciente (identificación, nombre, edad, EPS, ciudad de origen), de la solicitud de referencia (IPS emisora, especialidad que remite, especialidad destino, unidad funcional, observaciones) y del registro de recepción (estado de gestión: aceptado, no aceptado, sin definir conducta o cancelado; motivo de no aceptación, profesional que recibió, fecha de recepción y días transcurridos entre la solicitud y la gestión). El parámetro @TipoVista permite alternar entre dos variantes del reporte (por ejemplo, vista detallada vs. resumida), y el estado final del caso se calcula considerando si el paciente ya tiene registro de egreso. Es utilizado por áreas de referencia y contrarreferencia, coordinación médica y auditoría para hacer seguimiento al flujo de remisiones entre instituciones prestadoras de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_REF_ListarReporteRecepcionReferencia';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_REF_ListarReporteRecepcionReferencia';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'"Genera un reporte de seguimiento de referencias recibidas por uno o varios centros de atención dentro de un rango de fechas, con datos del paciente, entidad, IPS, especialidades, gestión, motivo de no aceptación y estado (abierto/cerrado), en modalidad detallada o resumida según parámetro."', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_ListarReporteRecepcionReferencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro de centros debe ser una cadena parseable por dbo.SplitString que produzca uno o más códigos válidos de ADCENATEN.; El rango de fechas (inicio/fin) debe estar definido y ser coherente para filtrar por la fecha de registro de la referencia.; Debe existir la función [Common].[GETDATE] para el cálculo de la edad.; Las tablas maestras de pacientes, entidades, ubicaciones, especialidades, IPS, unidades funcionales y centros deben tener integridad referencial con HCREFRECEP.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_ListarReporteRecepcionReferencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen referencias cuyo centro de atención esté en la lista recibida (parseada con SplitString) y cuya fecha de registro esté en el rango indicado.; La edad se calcula como diferencia en días entre fecha de nacimiento y fecha actual dividida por 365.25, truncada a entero.; El estado ''Cerrado'' requiere simultáneamente que la referencia esté en estado 3 y exista fecha de alta en el ingreso asociado; en cualquier otro caso es ''Abierto''.; El motivo de la gestión solo se reporta cuando la gestión fue ''No Aceptado'' (ESTADO=2); en los demás casos se fuerza ''N/A''.; En la vista resumida (TipoVista≠1) se garantiza una sola fila de gestión por solicitud usando MAX(ID) sobre HCREFRECDE.; Las consultas se ejecutan con WITH (NOLOCK) sobre todas las tablas, asumiendo lecturas sucias aceptables para el reporte.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_ListarReporteRecepcionReferencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Referencia y contrarreferencia; Paciente; EPS / entidad responsable de pago; IPS emisora; Especialidad médica (que remite y a la que se remite); Unidad funcional; Centro de atención; Profesional de salud receptor; Motivo de no aceptación de referencia; Egreso / alta del paciente; Tipo de documento de identificación; Edad del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_ListarReporteRecepcionReferencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCREFRECEP: Cuando TipoVista=1 retorna un resultset con todas las gestiones (HCREFRECDE) de cada referencia filtradas por centro y rango de fechas de registro.; [RETURN_RESULT] dbo.HCREFRECEP: Cuando TipoVista≠1 retorna un resultset con una sola fila por referencia, tomando únicamente la última gestión (MAX(ID) de HCREFRECDE por HCREFRECEPID).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_ListarReporteRecepcionReferencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TipoVista = 1 → Devuelve el reporte uniendo todos los registros de gestión (HCREFRECDE) asociados a cada solicitud de referencia (vista detallada con múltiples filas por solicitud). else Devuelve el reporte tomando únicamente el último registro de gestión por solicitud (MAX(ID) sobre HCREFRECDE) para mostrar una sola fila resumen por referencia.; si I.ESTADO = 1/2/3/4 → Etiqueta la gestión como ''Aceptado'', ''No Aceptado'', ''Sin Definir Conducta'' o ''Cancelado'' respectivamente.; si I.ESTADO = 2 (No Aceptado) → Muestra el motivo de no aceptación (código y descripción de HCMOTREFREC). else Muestra ''N/A'' como motivo.; si B.ESTADO = 3 y existe fecha de alta del paciente (L.FECALTPAC IS NOT NULL) → Marca la referencia como ''Cerrado''. else Marca la referencia como ''Abierto''.; si IPTIPODOC entre 1 y 8 → Traduce el código numérico al tipo de documento de identificación correspondiente (C.C., C.E., T.I., Registro Civil, Pasaporte, Adulto/Menor sin identificación, NUIP).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_ListarReporteRecepcionReferencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.SplitString', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_ListarReporteRecepcionReferencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.RCPACIENREF; dbo.HCREFRECEP; dbo.INENTIDAD; dbo.INUBICACI; dbo.INESPECIA; dbo.ADCONTIPS; dbo.INUNIFUNC; dbo.ADCENATEN; dbo.HCREFRECDE; dbo.HCMOTREFREC; dbo.INPROFSAL; dbo.HCREGEGRE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_ListarReporteRecepcionReferencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_ListarReporteRecepcionReferencia';
-- GO
