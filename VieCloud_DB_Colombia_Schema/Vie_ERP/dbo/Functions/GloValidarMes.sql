-- =============================================
-- Author:		Rafael Patiño
-- Create date: 31/08/2013
-- Description:	Fucion para validar mes cerrado
-- =============================================
CREATE FUNCTION [dbo].[GloValidarMes] 
(
		@Fecha date,
		@Empresa varchar(30)
)
RETURNS bit
AS
BEGIN
	-- Declare the return variable here
	declare @GloValidarMes as bit
	declare @sql nvarchar(1000)
	declare @tableTmp table(CPSFECINI  date, cpscierre  varchar(17) )
	declare @count  Integer

	declare @Mes  Integer = Month(@Fecha)
	declare @Ano  Integer = year(@Fecha)
	declare @MesCerrado  Integer

	-- Add the T-SQL statements to compute the return value here
	set @sql =  'SELECT CPSFECINI, rtrim(cpscierre) as cpscierre FROM DGEMPRES' + @Empresa + '..ctparsis'
	
	
	insert into @tableTmp
	select '13/08/2013' ,2 
	--execute sp_executesql @sql

	select @count = count(*) from @tableTmp

	IF @count > 0 BEGIN
		declare @CPSFECINI as date = (select CPSFECINI from @tableTmp)
		declare @cpscierre varchar(17) = (select cpscierre from @tableTmp)

		declare @MesIni as integer =  Month(@CPSFECINI)
		declare @anoini as integer =  year(@CPSFECINI)
		set @MesCerrado = len(@cpscierre)

		/*If MesCerrado <= 13 And MesCerrado <= Mes Then
       GloValidarMes = True
    Else
       If MesCerrado > 13 Then
          MesCerrado = MesCerrado - 12
          If MesCerrado <= Mes Then
            GloValidarMes = True
          Else
            GloValidarMes = False
          End If
       Else
          difAno = Ano - anoini
          If difAno > 1 Then
             GloValidarMes = True
          Else
             GloValidarMes = False
          End If
       End If
    End If
	end*/

		if @MesCerrado <=13 and @MesCerrado <= @Mes begin
			set @GloValidarMes = 1
		end
	END
	--SELECT CPSFECINI,rtrim(cpscierre) as cpscierre FROM .ctparsis

	-- Return the result of the function
	RETURN @GloValidarMes 

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función que valida si un mes contable está cerrado para una empresa determinada. Recibe una fecha y un código de empresa, y retorna verdadero (1) si el mes de la fecha proporcionada ya fue cerrado contablemente según los parámetros del sistema (tabla de parámetros ctparsis de la empresa). Se usa para bloquear operaciones de glosas, facturación u otros procesos contables sobre períodos que ya no deben modificarse.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'GloValidarMes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'GloValidarMes';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Determina si un mes contable de una empresa se encuentra cerrado, comparando el mes de la fecha consultada contra los parámetros de cierre contable de la empresa.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GloValidarMes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir una empresa identificable cuyos parámetros de cierre contable puedan consultarse; La fecha de entrada debe ser válida para extraer mes y año', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GloValidarMes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El indicador de mes cerrado se deriva de la longitud de la cadena de cierre contable; La función solo retorna 1 (verdadero) o NULL; nunca asigna explícitamente 0; La consulta dinámica a la base de la empresa está deshabilitada y se sustituye por datos fijos de prueba (''13/08/2013'', 2)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GloValidarMes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cierre contable mensual; Empresa; Parámetros de sistema contable', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GloValidarMes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RETURN: Si existen parámetros de cierre y el indicador de mes cerrado es <=13 y <= mes de la fecha consultada, retorna 1 (mes cerrado); en caso contrario retorna NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GloValidarMes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existen registros de parámetros de cierre (@count > 0) → Evalúa el indicador de mes cerrado contra el mes de la fecha else No asigna valor de retorno (queda NULL); si Longitud del valor de cierre <=13 Y <= mes de la fecha → Marca el mes como cerrado (retorna 1) else No marca el mes como cerrado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GloValidarMes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'ctparsis', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GloValidarMes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GloValidarMes';
GO
