CREATE Procedure [dbo].[SPMOV_ListarDiagnosticosPaciente]
(
@Paciente Varchar(25),
@Ingreso Char(10)
)
AS
SELECT  A.CODDIAGNO as CodigoDX,
		RTRIM(B.NOMDIAGNO) as DescripcionDX,
	    Case 
		when [DIAINGEGR] = 'I' then 'INGRESO'
		when [DIAINGEGR] = 'E' then 'EGRESO'
		when [DIAINGEGR] = 'A' then 'AMBOS'
		END as Tipo,
		CASE
		when B.EXIGENOTI is null then 'No'
		when B.EXIGENOTI = 0 then 'No'
		when B. EXIGENOTI = 1 then 'Si'
		END as Notificación,
		RTRIM(OBSDIAGNO) as Observaciones
  FROM [INDIAGNOH] as A
  inner join INDIAGNOS B on B.CODDIAGNO = A.CODDIAGNO
  where A.IPCODPACI	= @Paciente AND A.NUMINGRES = @Ingreso