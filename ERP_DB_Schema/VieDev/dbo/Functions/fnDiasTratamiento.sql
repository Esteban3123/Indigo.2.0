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