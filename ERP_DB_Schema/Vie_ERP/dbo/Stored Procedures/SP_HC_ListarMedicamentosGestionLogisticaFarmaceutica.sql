
CREATE PROCEDURE [dbo].[SP_HC_ListarMedicamentosGestionLogisticaFarmaceutica] 
(
@Idsourcetable as integer,
@Paciente Varchar(25),
@Ingreso  Char(10)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	-------------------

if @Idsourcetable = 0 begin
print '@Idsourcetable = 0'
				SELECT distinct  cast(0 as bit) as Seleccion, FD.SourceTable, A.ID AS 'ID', CAST(A.PREESTADO AS INT) AS 'Estado', A.FECINIDOS AS 'FechaInicial', RTRIM(A.CODPRODUC) AS 'Codigo', RTRIM(E.DESPRODUC) AS 'Medicamentos', A.DESADMINI AS 'Administracion', 
				CONVERT(varchar(10), A.CODVIAADM) AS 'Via', CONVERT(varchar(10), A.CODUNIMED) AS 'UnidadMedida',
				CONVERT(varchar(10), A.CANPEDPRO) AS 'Cantidad', '' AS 'Indicaciones', '' AS 'DuracionFrecuencia', '' AS 'UnidadFrecuencia', '' AS 'TipoDuracion', 
				'' AS 'PesoPaciente', '' AS 'VolumenTotal', '' AS 'VolumenAdministrado', '' AS 'TiempoAdministrado', '' AS 'VelocidadInfusion', 
				(SELECT TOP 1 CASE WHEN FOLIOINIC = NUMEFOLIO THEN '1 - Nuevo' WHEN TRATMODIF= '1' THEN '2 - Modificado' ELSE '3 - En tratamiento' END AS 'LEYENDA' FROM HCPRESCRD WHERE IPCODPACI = A.IPCODPACI AND NUMINGRES = A.NUMINGRES AND CODPRODUC = A.CODPRODUC ORDER BY FECINIDOS DESC) AS 'LEYENDA',
				RTRIM(A.CODPROSAL) AS 'CodigoProfesional', RTRIM(B.NOMMEDICO) AS 'NombreProfesional', Ltrim(RTRIM(N.DESESPECI)) AS 'Especialidad', '1' AS 'Origen', CONVERT(varchar(10), T.NUMEROAPLICACIONINICIAL) AS 'DosisAplicaciones', cast (0 as int) As 'CabeceraProducto'
				FROM .dbo.HCPRESCRA A 
				INNER JOIN .dbo.INPROFSAL B WITH(NOLOCK) ON A.CODPROSAL=B.CODPROSAL 
				INNER JOIN .dbo.IHLISTPRO E WITH(NOLOCK) ON A.CODPRODUC=E.CODPRODUC 
				INNER JOIN .DBO.HCHISPACA AS J WITH(NOLOCK) ON A.NUMEFOLIO=J.NUMEFOLIO AND A.IPCODPACI=J.IPCODPACI 
				INNER JOIN dbo.HCFARMEPD FD with(nolock) ON FD.IdSourceTable = A.ID and SourceTable = 'HCPRESCRA' AND FD.SENDTO = 0 and fd.PROESTADO = 1
				INNER JOIN dbo.HCPRESCRAEXT T with(nolock) ON A.ID = T.IDHCPRESCRA
				LEFT OUTER JOIN .dbo.INESPECIA N WITH(NOLOCK) ON J.CODESPTRA=N.CODESPECI
				where A.IPCODPACI= @Paciente AND A.NUMINGRES = @Ingreso AND A.PREESTADO IN (1,6) AND A.IDESQUEMAONC IS NULL --1: Iniciado: Cuando el Medicamento se Solicita por Primera Vez - 6: Medicamentos Solicitados sin Existencia Actual en el Kardex.

--UNION ALL
--				SELECT distinct  cast(0 as bit) as Seleccion, FD.SourceTable, A.ID AS 'ID', CAST(A.PREESTADO AS INT) AS 'Estado', A.FECINIDOS AS 'FechaInicial', RTRIM(A.CODPRODUC) AS 'Codigo', RTRIM(E.DESPRODUC) AS 'Medicamentos', A.DESADMINI AS 'Administracion',
--				CONVERT(varchar(10), A.CODVIAADM) AS 'Via', CONVERT(varchar(10), A.CODUNIMED) AS 'UnidadMedida', 
--				CONVERT(varchar(10), A.CANPEDPRO) AS 'Cantidad', '' AS 'Indicaciones', '' AS 'DuracionFrecuencia', '' AS 'UnidadFrecuencia', '' AS 'TipoDuracion',
--				'' AS 'PesoPaciente', '' AS 'VolumenTotal', '' AS 'VolumenAdministrado', '' AS 'TiempoAdministrado', '' AS 'VelocidadInfusion', 
--				CASE WHEN C.FOLIOINIC = C.NUMEFOLIO THEN '1 - Nuevo' WHEN C.TRATMODIF = '1' THEN '2 - Modificado' ELSE '3 - En tratamiento' END AS 'LEYENDA', RTRIM(A.CODPROSAL) AS 'CodigoProfesional',
--				RTRIM(B.NOMMEDICO) AS 'NombreProfesional', Ltrim(RTRIM(N.DESESPECI)) AS 'Especialidad', '2' AS 'Origen'
--				FROM .dbo.HCPRESCRA A 
--				INNER JOIN HCPRESCRD C WITH(NOLOCK) ON A.IPCODPACI = C.IPCODPACI AND A.NUMINGRES = C.NUMINGRES AND A.CODPRODUC = C.CODPRODUC AND A.NUMEFOLIO = C.NUMEFOLIO
--				INNER JOIN .dbo.INPROFSAL B WITH(NOLOCK) ON A.CODPROSAL=B.CODPROSAL 
--				INNER JOIN .dbo.IHLISTPRO E WITH(NOLOCK) ON A.CODPRODUC=E.CODPRODUC 
--				INNER JOIN .DBO.HCHISPACA AS J WITH(NOLOCK) ON A.NUMEFOLIO=J.NUMEFOLIO AND A.IPCODPACI=J.IPCODPACI 
--				INNER JOIN dbo.HCFARMEPD FD with(nolock) ON FD.IdSourceTable = A.ID and SourceTable = 'HCPRESCRA' AND FD.SENDTO = 0 and fd.PROESTADO = 1

--				LEFT OUTER JOIN .dbo.INESPECIA N WITH(NOLOCK) ON J.CODESPTRA=N.CODESPECI 
--				where A.IPCODPACI= @Paciente AND A.PREESTADO IN (1,6) AND A.IDESQUEMAONC IS NOT NULL 

UNION ALL
				SELECT distinct  cast(0 as bit) as Seleccion, FD.SourceTable, A.CONSECUTI AS 'ID', CAST(A.PREESTADO AS INT) AS 'Estado', A.FECHAINIC AS 'FechaInicial', RTRIM(A.CODCONCEC) AS 'Codigo', RTRIM(A.MEZLIQPAC) AS 'Medicamentos', A.ADMMEZLIQ AS 'Administracion',
				'' AS 'Via','' AS 'UnidadMedida', E.NUMEROAPLICACIONES AS 'Cantidad', A.INDAPLMED AS 'Indicaciones', CONVERT(varchar(10), A.DURFRECUENCIA) AS 'DuracionFrecuencia', CONVERT(varchar(10), A.UNIDADFRECUENCIA) AS 'UnidadFrecuencia', 
				CONVERT(varchar(10), A.TIPODURACION) AS 'TipoDuracion', '' AS 'PesoPaciente', '' AS 'VolumenTotal', '' AS 'VolumenAdministrado', '' AS 'TiempoAdministrado', '' AS 'VelocidadInfusion',
				CASE WHEN E.FOLIOINIC = E.NUMEFOLIO THEN '1 - Nuevo' WHEN E.TRATMODIF= '1' THEN '2 - Modificado' ELSE '3 - En tratamiento' END AS 'LEYENDA', RTRIM(A.CODPROSAL) AS 'CodigoProfesional',
				RTRIM(C.NOMMEDICO) AS 'NombreProfesional', Ltrim(RTRIM(N.DESESPECI)) AS 'Especialidad','3' AS 'Origen', CONVERT(varchar(10), E.NUMEROAPLICACIONES) AS 'DosisAplicaciones', B.CODCONCEC As 'CabeceraProducto'
				from HCINFLIQA A
				INNER JOIN HCINFLIQC B WITH(NOLOCK) ON A.CODCONCEC = B.CODCONCEC
				LEFT JOIN HCINFLIQD E WITH(NOLOCK) ON A.CODCONCEC = E.CODCONCEC
				INNER JOIN .dbo.INPROFSAL C WITH(NOLOCK) ON B.CODPROSAL = C.CODPROSAL 
				INNER JOIN .DBO.HCHISPACA AS D WITH(NOLOCK) ON A.NUMEFOLIO = D.NUMEFOLIO AND A.IPCODPACI = D.IPCODPACI
				INNER JOIN dbo.HCFARMEPD FD with(nolock) ON FD.IdSourceTable = A.CONSECUTI and SourceTable = 'HCINFLIQA' AND FD.SENDTO = 0 and fd.PROESTADO = 1 
				LEFT OUTER JOIN .dbo.INESPECIA N WITH(NOLOCK) ON D.CODESPTRA = N.CODESPECI 
				where A.IPCODPACI = @Paciente AND A.NUMINGRES = @Ingreso AND A.PREESTADO IN (1,3,5) AND A.TIPMEZLIQ  <> 4 -- 1: Iniciado: Cuando el Medicamento se Solicita por Primera Vez - 3: Tratamiento Modificado: Cuando existe una modificacion en la Dosificacion, Duracion o Frecuencia - 5: Alguno de los Medicamentos de la mezcla estan sin Existencia en el Kardex.

				/* Se comenta ya que desde el perfil farmacoterapeutico no deben caer las NPT (Nutriciones parenterales) estas pasan derecho desde la orden medica a central mezclas
				UNION ALL
				SELECT A.ID AS 'ID', CAST(A.STATUS AS INT) AS 'Estado', A.FECHAORDEN AS 'FechaInicial', RTRIM(A.ID) AS 'Codigo', RTRIM(B.NAME) AS 'Medicamentos', 'Administrar Continuamente ' + CONVERT(varchar(10), A.VOLUTOTAL) + ' ml ' + 'en infusión continua a ' + CONVERT(varchar(10), A.VELINFUSION) + ' ml/hora por ' + CONVERT(varchar(10),A.TEMPOADMIN) + ' Horas'  AS 'Administracion', CONVERT(varchar(10), A.VIADMIN) AS 'Via', '' AS 'UnidadMedida', 
				'1' AS 'Cantidad', '' AS 'Indicaciones', '' AS 'DuracionFrecuencia', '' AS 'UnidadFrecuencia', '' AS 'TipoDuracion', CONVERT(varchar(10), A.PESOPACIE) AS 'PesoPaciente',
				CONVERT(varchar(10), A.VOLUTOTAL) AS 'VolumenTotal', CONVERT(varchar(10), A.VOLUADM) AS 'VolumenAdministrado', CONVERT(varchar(10), A.TEMPOADMIN) AS 'TiempoAdministrado',
				CONVERT(varchar(10), A.VELINFUSION) AS 'VelocidadInfusion', '1 - Nuevo' AS 'LEYENDA', RTRIM(PRO.CODPROSAL) AS 'CodigoProfesional', 
				RTRIM(PRO.NOMMEDICO) AS 'NombreProfesional', Ltrim(RTRIM(ESP.DESESPECI)) AS 'Especialidad', '4' AS 'Origen'
				FROM HCNUTPAREC AS A 
				INNER JOIN HCPARNUTC B ON A.IDHCPARNUTC = B.ID 
				INNER JOIN INPROFSAL PRO ON A.CODPROSAL = PRO.CODPROSAL
				INNER JOIN HCHISPACA HIS ON HIS.ID = A.IDHCHISPACA 
				LEFT OUTER JOIN INESPECIA ESP WITH(NOLOCK) ON HIS.CODESPTRA = ESP.CODESPECI 
				WHERE A.IPCODPACI = @Paciente AND A.NUMINGRES = @Ingreso AND A.STATUS IN (1)
				*/

				UNION ALL
				SELECT distinct  cast(0 as bit) as Seleccion, FD.SourceTable, A.CONSECUTI AS 'ID', CAST(A.PREESTADO AS INT) AS 'Estado', A.FECHAINIC AS 'FechaInicial', RTRIM(A.CODCONCEC) AS 'Codigo', RTRIM(A.MEZLIQPAC) AS 'Medicamentos', A.ADMMEZLIQ AS 'Administracion',
				'' AS 'Via','' AS 'UnidadMedida', E.NUMEROAPLICACIONES AS 'Cantidad', A.INDAPLMED AS 'Indicaciones', CONVERT(varchar(10), A.DURFRECUENCIA) AS 'DuracionFrecuencia', CONVERT(varchar(10), A.UNIDADFRECUENCIA) AS 'UnidadFrecuencia', 
				CONVERT(varchar(10), A.TIPODURACION) AS 'TipoDuracion', '' AS 'PesoPaciente', '' AS 'VolumenTotal', '' AS 'VolumenAdministrado', '' AS 'TiempoAdministrado', '' AS 'VelocidadInfusion',
				CASE WHEN E.FOLIOINIC = E.NUMEFOLIO THEN '1 - Nuevo' WHEN E.TRATMODIF= '1' THEN '2 - Modificado' ELSE '3 - En tratamiento' END AS 'LEYENDA', RTRIM(A.CODPROSAL) AS 'CodigoProfesional',
				RTRIM(C.NOMMEDICO) AS 'NombreProfesional', Ltrim(RTRIM(N.DESESPECI)) AS 'Especialidad','5' AS 'Origen', CONVERT(varchar(10), E.NUMEROAPLICACIONES) AS 'DosisAplicaciones', B.CODCONCEC As 'CabeceraProducto'
				from HCINFLIQA A
				INNER JOIN HCINFLIQC B WITH(NOLOCK) ON A.CODCONCEC = B.CODCONCEC
				LEFT JOIN HCINFLIQD E WITH(NOLOCK) ON A.CODCONCEC = E.CODCONCEC
				INNER JOIN .dbo.INPROFSAL C WITH(NOLOCK) ON B.CODPROSAL = C.CODPROSAL 
				INNER JOIN .DBO.HCHISPACA AS D WITH(NOLOCK) ON A.NUMEFOLIO = D.NUMEFOLIO AND A.IPCODPACI = D.IPCODPACI
				INNER JOIN dbo.HCFARMEPD FD with(nolock) ON FD.IdSourceTable = A.CONSECUTI and SourceTable = 'HCINFLIQA' AND FD.SENDTO = 0 and fd.PROESTADO = 1 
				LEFT OUTER JOIN .dbo.INESPECIA N WITH(NOLOCK) ON D.CODESPTRA = N.CODESPECI 
				where A.IPCODPACI = @Paciente AND A.NUMINGRES = @Ingreso AND A.PREESTADO IN (1,3,5) AND A.TIPMEZLIQ=4 -- 1: Iniciado: Cuando el Medicamento se Solicita por Primera Vez - 3: Tratamiento Modificado: Cuando existe una modificacion en la Dosificacion, Duracion o Frecuencia - 5: Alguno de los Medicamentos de la mezcla estan sin Existencia en el Kardex.
END 
ELSE IF @Idsourcetable <> 0 begin 
	print '@Idsourcetable > 0'
				SELECT cast(0 as bit) as Seleccion, A.ID AS 'ID', CAST(A.STATUS AS INT) AS 'Estado', A.FECHAORDEN AS 'FechaInicial', RTRIM(A.ID) AS 'Codigo', RTRIM(B.NAME) AS 'Medicamentos', 'Administrar Continuamente ' + CONVERT(varchar(10), A.VOLUTOTAL) + ' ml ' + 'en infusión continua a ' + CONVERT(varchar(10), A.VELINFUSION) + ' ml/hora por ' + CONVERT(varchar(10),A.TEMPOADMIN) + ' Horas'  AS 'Administracion', CONVERT(varchar(10), A.VIADMIN) AS 'Via', '' AS 'UnidadMedida', 
				'1' AS 'Cantidad', '' AS 'Indicaciones', '' AS 'DuracionFrecuencia', '' AS 'UnidadFrecuencia', '' AS 'TipoDuracion', CONVERT(varchar(10), A.PESOPACIE) AS 'PesoPaciente',
				CONVERT(varchar(10), A.VOLUTOTAL) AS 'VolumenTotal', CONVERT(varchar(10), A.VOLUADM) AS 'VolumenAdministrado', CONVERT(varchar(10), A.TEMPOADMIN) AS 'TiempoAdministrado',
				CONVERT(varchar(10), A.VELINFUSION) AS 'VelocidadInfusion', '1 - Nuevo' AS 'LEYENDA', RTRIM(PRO.CODPROSAL) AS 'CodigoProfesional', 
				RTRIM(PRO.NOMMEDICO) AS 'NombreProfesional', Ltrim(RTRIM(ESP.DESESPECI)) AS 'Especialidad', '4' AS 'Origen', cast('0' As varchar(10)) AS 'DosisAplicaciones', cast (0 as int) As 'CabeceraProducto'
				FROM HCNUTPAREC AS A 
				INNER JOIN HCPARNUTC B ON A.IDHCPARNUTC = B.ID 
				INNER JOIN INPROFSAL PRO ON A.CODPROSAL = PRO.CODPROSAL
				INNER JOIN HCHISPACA HIS ON HIS.ID = A.IDHCHISPACA 
				LEFT OUTER JOIN INESPECIA ESP WITH(NOLOCK) ON HIS.CODESPTRA = ESP.CODESPECI 
				WHERE A.IPCODPACI = @Paciente AND A.NUMINGRES = @Ingreso AND A.STATUS IN (1) and a.ID = @Idsourcetable

	END
end