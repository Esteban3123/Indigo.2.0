-- =============================================
-- Author:      <Giovanny Plazas>
-- Create Date: <2023-06-18>
-- Description: <Funcion para buscar si un producto es en custodia>
-- =============================================
CREATE FUNCTION [Inventory].[FlagCustodyProduct]
(
    @CODPRODUC VARCHAR(20),
	@AdmissionNumber varchar(10)
)
RETURNS BIT
AS
BEGIN
	DECLARE @Flag BIT  =0

	IF @CODPRODUC is null OR @CODPRODUC='' or @AdmissionNumber  is null or @AdmissionNumber=''
	begin
		return  0
	end

	SELECT top 1 @Flag = isnull(hcd.MEDICACUSTODIA,0)
	FROM HCFARMEPD hcd WITH(NOLOCK)
	where hcd.NUMINGRES=@AdmissionNumber and hcd.CODPRODUC=@CODPRODUC
	order by hcd.ID DESC

	return @Flag
END