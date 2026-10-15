-- Stored Procedure


CREATE PROCEDURE [dbo].[SPCH_ListarPacientesAgendadosCitasOncologia]
(
@centroAtencion char(10),
@UnidadFuncional Char(10),
@Fecha as date,
@Usuario as varchar(20)
)
AS
BEGIN
SET NOCOUNT ON;


---Nota para el desarrollador: Guía el sp de SPCH_ListarPacientesAgendadosCitasRenal
end