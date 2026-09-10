
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista el histórico de medicamentos controlados administrados a pacientes en un rango de fechas. Para cada registro muestra el consecutivo de control, fecha, número de ingreso, folio, tipo de solicitud (orden médica, orden de enfermería o devolutivo), el producto farmacéutico con su descripción, el centro de atención, la unidad funcional, la identificación y nombre del paciente, el profesional de la salud responsable, el usuario de enfermería que registró y si el manejo fue intrahospitalario o extramural. Consolida información de las tablas de control de productos, catálogo de medicamentos, centros de atención, unidades funcionales, pacientes, profesionales de salud y usuarios del sistema para generar un reporte de trazabilidad y auditoría de sustancias controladas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarhistoricoMedicamentoControlados';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarhistoricoMedicamentoControlados';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista el histórico de solicitudes/movimientos de medicamentos controlados dentro de un rango de fechas, enriqueciendo con datos de paciente, profesional, producto, centro de atención y unidad funcional.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarhistoricoMedicamentoControlados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las fechas de inicio y fin deben estar provistas para acotar el rango de FECHAREGISTRO.; Deben existir relaciones válidas con centro de atención, unidad funcional, paciente, producto y profesional (INNER JOIN obligatorio).; El usuario de enfermería puede no existir (LEFT JOIN sobre SEGusuaru).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarhistoricoMedicamentoControlados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen movimientos con paciente, producto, profesional, centro de atención y unidad funcional existentes.; El consecutivo de control siempre se presenta con 7 dígitos rellenando con ceros a la izquierda.; El campo Seleccion siempre se inicializa en 0 (false).; Los tipos de solicitud reconocidos son únicamente 1=Orden Médica, 2=Orden Enfermería, 3=Devolutivo; otros valores quedan en NULL.; El manejo solo se clasifica como Intrahospitalario (0) o Extramural (1).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarhistoricoMedicamentoControlados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Medicamentos controlados; Orden médica; Orden de enfermería; Devolutivo; Paciente; Profesional de salud; Centro de atención; Unidad funcional; Manejo intrahospitalario; Manejo extramural; Folio; Ingreso', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarhistoricoMedicamentoControlados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCPRODUCTOSCONTROL: Devuelve registros de medicamentos controlados cuyo FECHAREGISTRO está entre @FechaInicial y @fechaFinal, con seleccion=0 y consecutivo formateado a 7 dígitos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarhistoricoMedicamentoControlados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si PC.TIPOSOLICITUD = 1 → Se etiqueta como ''Orden Medica'' else Si =2 ''Orden Enfermeria''; si =3 ''Devolutivo''; si PC.MANEXTPRO = 0 → Tipo de manejo se reporta como ''Intrahospitalario'' else Si =1 se reporta como ''Extramural''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarhistoricoMedicamentoControlados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCPRODUCTOSCONTROL; dbo.ADCENATEN; dbo.INUNIFUNC; dbo.INPACIENT; dbo.IHLISTPRO; dbo.INPROFSAL; dbo.SEGusuaru', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarhistoricoMedicamentoControlados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarhistoricoMedicamentoControlados';
-- GO
