-- =============================================
-- Author:		<Author,Juan David Patiño Cabrera,Name>
-- Create date: <Create Date,01-03-2019,>
-- Description:	<Description,Sp que me lista la Información por paciente si ya fue atentiendo en la Institución con el CUPS ,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_AGE_ListarCUPSPaciente]
@Paciente varchar(25),
@CUPS  char(20),
@date as char(10)

AS
BEGIN
	SET NOCOUNT ON;

  	Select so.PatientCode, so.Status, so.OrderDate, ce.Code
		from Billing.ServiceOrder so
			inner join Billing.ServiceOrderDetail sod on sod.ServiceOrderId = so.Id
			inner join Contract.CUPSEntity ce on ce.Id = sod.CUPSEntityId 
	Where so.PatientCode = @Paciente And  so.Status in (1,2) And ce.Code = @CUPS and DATEPART(YEAR, so.OrderDate) = @date and (sod.ApplyRIAS is null or sod.ApplyRIAS = 0)
UNION ALL
	 Select IPCODPACI,CODESTCIT,FECHORAIN,D.CODSERIPS 
	    from AGASICITA a 
		Inner join AGACTIMED D  ON A.CODACTMED=D.CODACTMED
	  Where  a.IPCODPACI = @Paciente And  a.CODESTCIT in (0,1)  And  d.CODSERIPS = @CUPS  and DATEPART(YEAR, a.FECHORAIN) = @date

END