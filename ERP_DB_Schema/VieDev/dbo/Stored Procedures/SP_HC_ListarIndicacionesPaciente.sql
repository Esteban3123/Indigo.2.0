CREATE PROCEDURE [dbo].[SP_HC_ListarIndicacionesPaciente]
(
@Paciente varchar(25),
@Ingreso char(10)
)
AS
BEGIN
	SET NOCOUNT ON;
	  
select NUMINGRES as ingreso, IPCODPACI as Paciente,INDICAMED as IndicacionesMedicas, FECHISPAC as FechaHistoria,Rtrim(B.NOMMEDICO) as NameMedico from HCHISPACA A
	INNER JOIN INPROFSAL B ON A.CODPROSAL = B.CODPROSAL 
WHERE IPCODPACI = @Paciente and NUMINGRES = @Ingreso AND  (INDICAMED <> '' and INDICAMED IS not NULL)
order by FECHISPAC asc
END