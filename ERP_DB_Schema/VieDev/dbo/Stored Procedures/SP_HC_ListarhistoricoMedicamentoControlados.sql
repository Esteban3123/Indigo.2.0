
-- Stored Procedure
-- =============================================
-- Author:		Rafael Patiño
-- Create date: 24/07/2019
-- Description:	Medicamentos controlados
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarhistoricoMedicamentoControlados]
(
  @FechaInicial as date,
  @fechaFinal as date
)

AS
BEGIN
  SET NOCOUNT ON;
 

SELECT
	cast(0 as bit) as Seleccion, 
	PC.ID,
	RIGHT('0000000' +convert(varchar(10),INCONSECUCONTROL),7) as Consecutivo,
	PC.FECHAREGISTRO,
	PC.NUMINGRES as Ingreso,
	PC.NUMEFOLIO as Folio,
	case PC.TIPOSOLICITUD when 1 then 'Orden Medica' when 2 then 'Orden Enfermeria' when 3 then 'Devolutivo' END TIPOSOLICITUD,
	PC.CODCONCECENF as IDEnfermeria,
	PC.CODCONCECMED as IDMedico,
	rtrim(p.CODPRODUC) as CodigoProducto,
	rtrim(p.CODPRODUC) + ' - '+ rtrim(p.DESPRODUC) as Producto,
	rtrim(C.CODCENATE) + ' - '+ rtrim(C.NOMCENATE) as CentroAtencion,
	rtrim(U.UFUCODIGO) + ' - '+ rtrim(U.UFUDESCRI ) as UnidadFuncional,
	rtrim(pa.IPCODPACI) as Identificacion,
	rtrim(pa.IPCODPACI) + ' - '+ rtrim(pa.IPNOMCOMP) as Paciente,
	rtrim(pro.CODPROSAL) + ' - '+ rtrim(pro.NOMMEDICO) as profesional,
	rtrim(usu.CODUSUARI) + ' - '+ rtrim(usu.NOMUSUARI) as UsuarioEnfermeria,
	PC.INCONSECUCONTROL,
	PC.MANEXTPRO AS 'MANEXTPRO',
	CASE PC.MANEXTPRO WHEN 0 THEN 'Intrahospitalario' WHEN 1 THEN 'Extramural' END AS 'Tipo_Manejo' 	

FROM
	HCPRODUCTOSCONTROL PC with(nolock) inner join 
	ADCENATEN C with(nolock) on C.CODCENATE = PC.CODCENATE inner join
	INUNIFUNC U with(nolock) on U.UFUCODIGO = PC.UFUCODIGO inner join
	INPACIENT Pa with(nolock) on PA.IPCODPACI = PC.IPCODPACI inner join
	IHLISTPRO p with(nolock) ON p.CODPRODUC = PC.CODPRODUC inner join
	INPROFSAL PRO with(nolock) on PRO.CODPROSAL = PC.CODPROSAL left join
	SEGusuaru USU with(nolock) on USU.CODUSUARI = PC.USUARIOENFERMRIA
WHERE format(PC.FECHAREGISTRO,'dd/MM/yyyy') BETWEEN @FechaInicial AND @fechaFinal

END