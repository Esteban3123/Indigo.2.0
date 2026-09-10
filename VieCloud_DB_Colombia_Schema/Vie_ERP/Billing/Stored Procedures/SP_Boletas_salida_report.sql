

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Billing].[SP_Boletas_salida_report] 
	-- Add the parameters for the stored procedure here
	@FECHA_INICIAL DATE, 
	@FECHA_FINAL DATE,
	@ID char
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select 
SLI.Code AS 'CODIGO',
SLI.AdmissionNumber AS 'NUMERO DE ADMISION',
SLI.DocumentDate AS 'FECHA',
INP.IPNOMCOMP AS 'NOMBRE DEL PACIENTE',
INP.IPCODPACI AS CEDULA
from Billing.SlipOut AS SLI INNER JOIN
                     dbo.ADINGRESO AS ADI ON ADI.NUMINGRES = SLI.AdmissionNumber INNER JOIN
					 dbo.INPACIENT AS INP ON ADI.IPCODPACI = INP.IPCODPACI
WHERE (SLI.DocumentDate BETWEEN @FECHA_INICIAL AND @FECHA_FINAL) OR  (INP.IPCODPACI = @ID)
ORDER BY SLI.Code	
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de boletas de salida de facturación: genera un listado de comprobantes de egreso o despacho (SlipOut) emitidos en un rango de fechas o para un paciente específico identificado por su cédula. Combina los datos del comprobante (código y fecha del documento) con el ingreso o admisión del paciente (ADINGRESO) y su información personal (INPACIENT), devolviendo el código de la boleta, el número de admisión, la fecha del documento, el nombre completo del paciente y su cédula de identidad. Se usa para consultar y auditar los documentos de salida de facturación por período o por paciente individual.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_Boletas_salida_report';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_Boletas_salida_report';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte de comprobantes de salida (slip out) emitidos en un rango de fechas o asociados a un paciente específico, incluyendo datos del documento y del paciente.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_Boletas_salida_report';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las admisiones referenciadas en Billing.SlipOut deben existir en dbo.ADINGRESO para aparecer en el reporte (INNER JOIN por NUMINGRES = AdmissionNumber).; Las admisiones deben tener un paciente válido en dbo.INPACIENT (INNER JOIN por IPCODPACI).; El parámetro @ID se declara como CHAR sin longitud, por lo que solo conserva el primer carácter; el filtro por paciente solo coincidirá si IPCODPACI tiene un solo carácter.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_Boletas_salida_report';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan comprobantes de salida que tengan admisión y paciente existentes (uso de INNER JOIN excluye huérfanos).; El resultado siempre se ordena por código del comprobante (SLI.Code).; La condición OR hace que si se proporciona un @ID que coincida con algún paciente, sus comprobantes se incluyan aunque estén fuera del rango de fechas.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_Boletas_salida_report';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Comprobante de salida (slip out); Admisión/Ingreso del paciente; Paciente; Cédula/Identificación del paciente', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_Boletas_salida_report';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.SlipOut: Devuelve filas donde SLI.DocumentDate está entre @FECHA_INICIAL y @FECHA_FINAL O bien el paciente coincide con @ID, ordenadas por SLI.Code.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_Boletas_salida_report';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si SLI.DocumentDate BETWEEN @FECHA_INICIAL AND @FECHA_FINAL → Incluye el comprobante de salida en el resultado por estar dentro del rango de fechas. else Solo se incluye si además INP.IPCODPACI = @ID (filtro alternativo por paciente).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_Boletas_salida_report';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.SlipOut; dbo.ADINGRESO; dbo.INPACIENT', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_Boletas_salida_report';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_Boletas_salida_report';
-- GO
