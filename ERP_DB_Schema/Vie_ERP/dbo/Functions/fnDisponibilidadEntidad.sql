-- =============================================
-- Author:		Juan Patiño
-- Create date: 13/10/2016
-- Description:	Funcion Disponibilidad de dia Entidad Consulta Prioritaria
-- =============================================
CREATE FUNCTION [dbo].[fnDisponibilidadEntidad] 
(
	@FechaTriage as date, @CODEntidad as char(9)
)
RETURNS bit 
AS
BEGIN
	
	declare @NumeroDia as integer = ( SELECT DATEPART(DW,@FechaTriage) )
	declare @Disponible as bit

	Select  @Disponible =
						case @NumeroDia
										when 1 then (select LUN from dbo.ADPARCONPRI where CODENTIDA = @CODEntidad  )
										when 2 then (select MAR  from dbo.ADPARCONPRI where CODENTIDA = @CODEntidad  )
										when 3 then (select MIER  from dbo.ADPARCONPRI where CODENTIDA = @CODEntidad   )
										when 4 then (select JUE from dbo.ADPARCONPRI where CODENTIDA = @CODEntidad   )
										when 5 then (select VIER from dbo.ADPARCONPRI where CODENTIDA = @CODEntidad  )
										when 6 then (select SAB from dbo.ADPARCONPRI where CODENTIDA = @CODEntidad   )
										when 7 then (select DOM from dbo.ADPARCONPRI where CODENTIDA = @CODEntidad  )
						 END 
	
	 
	return @Disponible
END