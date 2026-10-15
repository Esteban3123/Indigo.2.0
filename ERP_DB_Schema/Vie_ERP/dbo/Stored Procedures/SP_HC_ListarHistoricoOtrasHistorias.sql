

CREATE PROCEDURE [dbo].[SP_HC_ListarHistoricoOtrasHistorias]
(
@INDPaciente Varchar(25)
)
AS
BEGIN
	SET NOCOUNT ON;

Select c.ID, C.IPCODPACI AS 'Identificacion',C.FECHAREGISTRO AS 'Fecha Cargue',C.NOMBREARCHIVO AS 'Nombre Archivo',RTRIM(C.OBSERVACION) AS 'Observacion',B.DESCATEGO as 'Tipo Documento',
	   C.USUARIOSUBE AS 'Usuario',C.CODCENATE AS 'Centro Atencion'
 From HCDOCHISTORIASCLINICAS C
		LEFT JOIN HCCATDOCU B ON C.TIPODOCUMENTO = B.CODCATEGO 
 Where IPCODPACI = @INDPaciente

end