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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los medicamentos, materiales e insumos solicitados para una programación quirúrgica específica (identificada por el ID de programación). Combina dos conjuntos de información: los productos adicionales que no forman parte de un paquete quirúrgico de enfermería, y los productos que sí pertenecen a un paquete quirúrgico (AGPAQUETES), agrupándolos por nombre de paquete. Para cada ítem devuelve cantidades solicitadas, entregadas y pendientes, datos del producto del catálogo farmacéutico (IHLISTPRO), datos del ingreso del paciente (ADINGRESO), entidad pagadora y grupo de contrato (HealthAdministrator, CareGroup), así como el estado de la orden y del detalle, el tipo de solicitud quirúrgica y si el producto está en prescripción activa (HCPRESCRA). Se usa en el módulo de agendamiento quirúrgico para que farmacia gestione el despacho de insumos y medicamentos asociados a una cirugía programada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AGE_ListarMedicamentosQX';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AGE_ListarMedicamentosQX';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los medicamentos, materiales e insumos asociados a una programación quirúrgica, separando los productos adicionales de los que pertenecen a paquetes de enfermería.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarMedicamentosQX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir una programación quirúrgica identificada cuyos pedidos de farmacia (HCFARMEPC/HCFARMEPD) estén asociados a la misma.; Los productos referenciados deben existir en el catálogo IHLISTPRO.; El ingreso del pedido debe existir en ADINGRESO.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarMedicamentosQX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La cantidad entregada de productos adicionales se calcula como CANPEDPRO - CANPENPRO (solicitada menos pendiente).; Un mismo producto-pedido no aparece simultáneamente como ''Productos adicionales'' y como parte de un paquete de enfermería: la pertenencia al paquete lo excluye de los adicionales.; Solo se consideran prescripciones de HCPRESCRA con MANEXTPRO=''0'' (no manejo extra).; El campo ''Unico'' siempre se devuelve como 0 (BIT) y ''MarcarOpcion'' como 0.; Los resultados se filtran siempre por la programación quirúrgica recibida como parámetro.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarMedicamentosQX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Programación quirúrgica; Pedido de farmacia; Medicamento; Materiales e insumos; Paquete quirúrgico; Paquete de enfermería; Prescripción médica; Ingreso del paciente; Contrato y plan de salud; Administradora de salud (entidad); Grupo de atención (CareGroup); Bodega de farmacia; Producto NOPOS; Estado de orden (Solicitada/Entregada/Anulada)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarMedicamentosQX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve dos conjuntos unidos: (1) productos del pedido cuyo CODCONCEC NO está incluido en ningún detalle de paquete de enfermería para esa programación, etiquetados como ''Productos adicionales''; (2) productos cuyo pedido está vinculado a una orden de paquete de enfermería (NursingPackagesOrder), etiquetados con el nombre del paquete (AGPAQUETES.NOMBRE).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarMedicamentosQX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si HCFARMEPC.TIPOSOLQX = 1 → Etiqueta TipoSolicitud como ''Paquete Quirúrgico'' else Si TIPOSOLQX = 2 etiqueta ''Otras solicitudes asociada a la cirugía''; si HCFARMEPC.ORDESTADO en (1,2,3) → Traduce el estado de la orden a ''Solicitada'', ''Entregada'' o ''Anulada'' respectivamente; si HCFARMEPD.PROESTADO en (1,2,3,4) → Traduce el estado del detalle a ''Pendiente'', ''Entregado'', ''Anulada'' o ''Confirmacion de Entregado''; si HCFARMEPD.TIPOREGIS = 1 → Clasifica el producto como ''Medicamento'' else Si TIPOREGIS = 2 lo clasifica como ''Materiales e Insumos''; si HCFARMEPD.CODCONCEC NO existe en NursingPackagesOrder/Detail con mismo CODPRODUC y programación → Se incluye en el grupo ''Productos adicionales'' (primer SELECT) else Se incluye en el grupo del paquete de enfermería correspondiente (segundo SELECT)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarMedicamentosQX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFARMEPC; dbo.HCFARMEPD; dbo.ADINGRESO; dbo.IHLISTPRO; Contract.CareGroup; Contract.HealthAdministrator; dbo.HCPRESCRA; MedicalHistory.NursingPackagesOrder; MedicalHistory.NursingPackagesOrderDetail; dbo.AGPAQUETES', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarMedicamentosQX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarMedicamentosQX';
-- GO
