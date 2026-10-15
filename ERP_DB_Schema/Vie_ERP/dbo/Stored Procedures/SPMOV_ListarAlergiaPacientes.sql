CREATE Procedure [dbo].[SPMOV_ListarAlergiaPacientes]
(
 @Paciente varchar(25),
 @Ingreso varchar(50)
)
AS
Select
RTRIM(B.CODPRODUC) as 'CodigoProducto',
RTRIM(B.DESPRODUC) as 'Descripcion',
RTRIM(A.MOTSUSMED) as 'Observacion'
from HCMEDRIES A
inner join IHLISTPRO B on B.CODPRODUC = A.CODPRODUC
where A.IPCODPACI = @Paciente AND A.NUMINGRES = @Ingreso