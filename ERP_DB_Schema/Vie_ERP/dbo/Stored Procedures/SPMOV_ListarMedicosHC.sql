
CREATE Procedure [dbo].[SPMOV_ListarMedicosHC]
(
@Paciente Varchar(25),
@Ingreso Char(10)
)
AS
SELECT DISTINCT 
		RTRIM(B.CODPROSAL) as CodigoMedico,
		RTRIM(B.MEDICFOTO) as Foto
  FROM HCHISPACA AS A
  Inner Join INPROFSAL B on B.CODPROSAL = A.CODPROSAL
  Inner Join INESPECIA C on C.CODESPECI = A.CODESPTRA
  where A.IPCODPACI	= @Paciente AND A.NUMINGRES = @Ingreso