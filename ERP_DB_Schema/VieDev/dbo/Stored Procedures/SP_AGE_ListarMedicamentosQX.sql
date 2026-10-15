CREATE PROCEDURE [dbo].[SP_AGE_ListarMedicamentosQX]
(
@Idprogramacion int
)
AS
BEGIN
	SET NOCOUNT ON;
	 
SELECT 
A.CANPEDPRO AS 'CantidadSolicitada',(isnull(a.CANPEDPRO,0) - isnull(A.CANPENPRO,0)) AS 'CantidadEntregada', A.CANPENPRO AS 'CantidadPendiente',F.IDAGEPROGQX, A.CODCONCEC  as 'IdFarmacia',F.FECHAORDE as 'FechaOrden',
RTRIM(F.NUMINGRES) AS 'Ingreso',F.CODBODEGA as 'CodigoBodega',RTRIM(c.Name) AS 'Entidad',c.Code AS 'CodigoEntidad',CODCONTRA AS 'CodigoContrato',CODPANATE AS 'CodigoPlan',RTRIM(CG.Name) AS 'ContratoPlan',
RTRIM ( A.CODPRODUC) as 'Codigo',RTRIM(DESPRODUC) AS 'Producto',Concat(RTRIM(A.CODPRODUC),' - ' ,RTRIM(DESPRODUC)) AS Producto,A.TIPOREGIS AS 'Tipo',CAST(0 AS BIT) AS 'Unico',A.NOPOSPROD AS 'NOPOS',
A.CODUNIMED AS 'UnidadMedida',A.IDETIPHIS,A.NUMEFOLIO,'0' AS 'Opcion','0' AS 'OpcionAnulado','' AS 'Procedimiento',CAST('0' AS BIT) AS 'MarcarOpcion', RTRIM(A.IPCODPACI) as 'CodigoPaciente',A.CODCONCEC as 'Consecutivo', 
A.PROESTADO as 'Estado' ,case F.TIPOSOLQX when 1 then 'Paquete Quirúrgico' when 2 then 'Otras solicitudes asociada a la cirugía' END 'TipoSolicitud', case F.ORDESTADO when 1 then 'Solicitada' when 2 then 'Entregada' when 3 then 'Anulada' END 'Estado',
A.IDAGEPROGQX, case A.PROESTADO when 1 then 'Pendiente' when 2 then 'Entregado' when 3 then 'Anulada'  when 4 then 'Confirmacion de Entregado' END EstadoDetalle,case A.TIPOREGIS  when 1 then 'Medicamento' when 2 then 'Materiales e Insumos'  END 'TipoProducto', 'Productos adicionales' AS 'Agrupador' 
FROM HCFARMEPC F 
INNER JOIN dbo.HCFARMEPD A on A.CODCONCEC = F.CODCONCEC 
INNER JOIN dbo.ADINGRESO B ON A.NUMINGRES=B.NUMINGRES
INNER JOIN dbo.IHLISTPRO D ON A.CODPRODUC=D.CODPRODUC 
LEFT JOIN Contract.CareGroup AS CG ON CG.Id = B.GENCAREGROUP
LEFT JOIN Contract.HealthAdministrator c ON b.GENCONENTITY = c.Id
LEFT OUTER JOIN dbo.HCPRESCRA AS hc ON A.CODPRODUC=hc.CODPRODUC AND A.NUMEFOLIO=hc.NUMEFOLIO AND A.IPCODPACI = HC.IPCODPACI AND A.NUMINGRES = HC.NUMINGRES AND HC.MANEXTPRO='0'
where A.IDAGEPROGQX = @Idprogramacion AND 
A.CODCONCEC not in (select NP.IDHCFARMEPC from MedicalHistory.NursingPackagesOrderDetail NPD INNER JOIN MedicalHistory.NursingPackagesOrder NP ON NP.Id = NPD.IdNursingPackagesOrder WHERE NPD.CODPRODUC = A.CODPRODUC AND A.IDAGEPROGQX = @Idprogramacion)

UNION

SELECT 
NPD.Quantity AS 'CantidadSolicitada', A.CANENTPRO AS 'CantidadEntregada', A.CANPENPRO AS 'CantidadPendiente',F.IDAGEPROGQX, A.CODCONCEC  as 'IdFarmacia',F.FECHAORDE as 'FechaOrden',RTRIM(F.NUMINGRES) AS 'Ingreso',F.CODBODEGA as 'CodigoBodega',
RTRIM(c.Name) AS 'Entidad',c.Code AS 'CodigoEntidad',CODCONTRA AS 'CodigoContrato',CODPANATE AS 'CodigoPlan',RTRIM(CG.Name) AS 'ContratoPlan',RTRIM ( A.CODPRODUC) as Codigo,RTRIM(DESPRODUC) AS 'Producto',Concat(RTRIM(A.CODPRODUC),' - ' ,RTRIM(DESPRODUC)) AS Producto,
A.TIPOREGIS AS 'Tipo',CAST(0 AS BIT) AS 'Unico',A.NOPOSPROD AS 'NOPOS',A.CODUNIMED AS 'UnidadMedida',A.IDETIPHIS,A.NUMEFOLIO,'0' AS 'Opcion','0' AS 'OpcionAnulado','' AS 'Procedimiento',CAST('0' AS BIT) AS 'MarcarOpcion', RTRIM(A.IPCODPACI) as 'CodigoPaciente',
A.CODCONCEC as 'Consecutivo', A.PROESTADO as 'Estado',case F.TIPOSOLQX when 1 then 'Paquete Quirúrgico' when 2 then 'Otras solicitudes asociada a la cirugía' END 'TipoSolicitud', case F.ORDESTADO when 1 then 'Solicitada' when 2 then 'Entregada' when 3 then 'Anulada' END 'Estado',
A.IDAGEPROGQX, case A.PROESTADO when 1 then 'Pendiente' when 2 then 'Entregado' when 3 then 'Anulada'  when 4 then 'Confirmacion de Entregado' END EstadoDetalle,case A.TIPOREGIS  when 1 then 'Medicamento' when 2 then 'Materiales e Insumos'  END 'TipoProducto', RTRIM(PQ.NOMBRE) AS 'Agrupador' 
FROM HCFARMEPC F 
INNER JOIN dbo.HCFARMEPD A on A.CODCONCEC = F.CODCONCEC 
INNER JOIN dbo.ADINGRESO B ON A.NUMINGRES=B.NUMINGRES
INNER JOIN dbo.IHLISTPRO D ON A.CODPRODUC=D.CODPRODUC 
INNER JOIN MedicalHistory.NursingPackagesOrder NP ON NP.IDHCFARMEPC = F.CODCONCEC
INNER JOIN MedicalHistory.NursingPackagesOrderDetail NPD ON NPD.IdNursingPackagesOrder = NP.Id AND NPD.CODPRODUC = A.CODPRODUC
INNER JOIN dbo.AGPAQUETES PQ ON PQ.ID = NP.IDAGPAQUETES
LEFT JOIN Contract.CareGroup AS CG ON CG.Id = B.GENCAREGROUP
LEFT JOIN Contract.HealthAdministrator c ON b.GENCONENTITY = c.Id
where A.IDAGEPROGQX = @Idprogramacion  

END