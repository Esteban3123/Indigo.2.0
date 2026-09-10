-- =============================================
-- Author:      RafaelPatiño
-- Create Date: 05/08/2022
-- Description: funcion que retorna los dias trascurridos desde la primera aplicacion del medimento
-- =============================================
CREATE FUNCTION [dbo].[fnDiasTratamiento]
(
    -- Add the parameters for the function here
   @Tipo int,
   @IdPrescra int,
   @IDHCINFLIQC int,
   @CodigoProducto varchar(20),
   @Ingreso varchar(20)

)
RETURNS int
AS
BEGIN

    DECLARE @Dias int
	declare @PrimerFechaAplicacion as datetime = null

	if @Tipo = 1 begin --medicamentos
		
		declare @FechaFinDosis as datetime = (select top 1 fecfindos from HCPRESCRA where ID = @IdPrescra and CODPRODUC = @CodigoProducto AND NUMINGRES = @Ingreso )
		/*select @PrimerFechaAplicacion =(select top 1 X.FECAPLMED FROM  (select FECAPLMED from HCHOJAMED where CONSECPRESCRA = @IdPrescra  and CODPRODUC = @CodigoProducto AND NUMINGRES = @Ingreso AND FECAPLMED  IS NOT NUll
																UNION 
																select FECAPLMED from HCHOJAMED where CODPRODUC = @CodigoProducto AND NUMINGRES = @Ingreso  AND FECAPLMED  IS NOT NUll 
														) as X ORDER BY X.FECAPLMED ASC
								) */

		set @PrimerFechaAplicacion =(select top 1 FECAPLMED from HCHOJAMED where CODPRODUC = @CodigoProducto AND NUMINGRES = @Ingreso  AND FECAPLMED  IS NOT NUll order by FECAPLMED asc  )

		SELECT @Dias =  CASE
         WHEN @PrimerFechaAplicacion IS NOT NULL AND @FechaFinDosis IS NULL THEN
			 Datediff(day, @PrimerFechaAplicacion, common.Getdate())
         WHEN @PrimerFechaAplicacion IS NOT NULL AND @FechaFinDosis IS NOT NULL THEN
			 Datediff(day, @PrimerFechaAplicacion, @FechaFinDosis)
         ELSE
			0
		 END 

	end else if @Tipo = 2 begin --mezclas

		select @PrimerFechaAplicacion =(select top 1 FECAPLMED from HCHOJMEZC where IDHCINFLIQC = @IDHCINFLIQC   AND FECAPLMED  IS NOT NUll order by FECAPLMED ASC) 
		SELECT @Dias = CASE
		WHEN @PrimerFechaAplicacion IS NOT NULL THEN		
			DATEDIFF(day,@PrimerFechaAplicacion,Common.GETDATE())
		ELSE
			0
		END

	end

    RETURN @Dias
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calcula la cantidad de días transcurridos desde la primera aplicación de un medicamento o mezcla intravenosa hasta la fecha actual o hasta la fecha de fin de dosis, según el tipo de tratamiento indicado. Para medicamentos simples (Tipo 1), consulta la hoja de medicamentos del ingreso (HCHOJAMED) para obtener la primera fecha de aplicación y la prescripción (HCPRESCRA) para verificar si existe fecha de fin de dosis; si no hay fecha de fin, calcula los días hasta hoy. Para mezclas intravenosas (Tipo 2), consulta la hoja de mezclas (HCHOJMEZC) y calcula los días transcurridos desde la primera aplicación hasta la fecha actual. Se utiliza para monitorear la duración real del tratamiento farmacológico de un paciente durante su ingreso hospitalario, apoyando decisiones clínicas sobre continuidad o suspensión de medicamentos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'fnDiasTratamiento';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'fnDiasTratamiento';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula los días transcurridos de tratamiento farmacológico desde la primera aplicación de un medicamento o mezcla, hasta la fecha fin de dosis o la fecha actual si no existe fin.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnDiasTratamiento';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe indicarse el tipo de tratamiento (1=medicamento, 2=mezcla) para seleccionar la fuente de datos.; Para medicamentos: deben existir registros en HCPRESCRA y/o HCHOJAMED asociados al ingreso y producto.; Para mezclas: debe existir un identificador de infusión líquida válido en HCHOJMEZC.; La función Common.GetDate() debe estar disponible para obtener la fecha actual del sistema.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnDiasTratamiento';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El resultado nunca es NULL: si no hay primera aplicación, retorna 0.; Solo considera registros con FECAPLMED IS NOT NULL como aplicaciones válidas.; La primera aplicación se determina por la fecha mínima (ORDER BY FECAPLMED ASC).; Para mezclas, el cálculo siempre se hace contra la fecha actual, ignorando cualquier fecha fin.; Para medicamentos, el filtro combina producto e ingreso; para mezclas se usa el identificador de infusión.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnDiasTratamiento';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'medicamentos; mezclas intravenosas; prescripción; hoja de medicamentos; aplicación de medicamento; fecha fin de dosis; ingreso hospitalario; duración de tratamiento', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnDiasTratamiento';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Cuando Tipo=1 y existe primera aplicación pero no fecha fin de dosis, retorna DATEDIFF(día, primera aplicación, fecha actual).; [RETURN_RESULT] : Cuando Tipo=1 y existen primera aplicación y fecha fin de dosis, retorna DATEDIFF(día, primera aplicación, fecha fin de dosis).; [RETURN_RESULT] : Cuando Tipo=1 y no hay primera aplicación registrada, retorna 0.; [RETURN_RESULT] : Cuando Tipo=2 y existe primera aplicación de mezcla, retorna DATEDIFF(día, primera aplicación, fecha actual).; [RETURN_RESULT] : Cuando Tipo=2 y no hay primera aplicación de mezcla, retorna 0.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnDiasTratamiento';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Tipo = 1 (medicamentos) → Obtiene fecha fin de dosis desde HCPRESCRA y primera fecha de aplicación desde HCHOJAMED para el producto e ingreso. else Si Tipo = 2 (mezclas), obtiene primera fecha de aplicación desde HCHOJMEZC por IDHCINFLIQC.; si Primera fecha de aplicación es NULL → Días retornados = 0. else Calcula DATEDIFF en días según exista o no fecha fin de dosis.; si Tipo = 1 y fecha fin de dosis IS NULL → Calcula días hasta la fecha actual (tratamiento aún vigente). else Calcula días hasta la fecha fin de dosis (tratamiento finalizado).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnDiasTratamiento';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GetDate', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnDiasTratamiento';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCPRESCRA; dbo.HCHOJAMED; dbo.HCHOJMEZC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnDiasTratamiento';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnDiasTratamiento';
GO
