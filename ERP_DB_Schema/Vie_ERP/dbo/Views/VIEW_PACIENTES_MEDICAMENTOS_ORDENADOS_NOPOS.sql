CREATE VIEW [dbo].[VIEW_PACIENTES_MEDICAMENTOS_ORDENADOS_NOPOS]
AS
     SELECT ipd.Code AS 'Codigo', 
            pfu.Name AS 'UnidadFuncional', 
            ipd.AdmissionNumber AS 'Ingreso', 
            ad.IFECHAING AS 'FechaIngreso',
            CASE
                WHEN p.IPTIPODOC = 1
                THEN 'Cédula de Ciudadanía'
                WHEN p.IPTIPODOC = 2
                THEN 'Cédula de Extranjería'
                WHEN p.IPTIPODOC = 3
                THEN 'Tarjeta de Identidad'
                WHEN p.IPTIPODOC = 4
                THEN 'Registro Civil'
                WHEN p.IPTIPODOC = 5
                THEN 'Pasaporte'
                WHEN p.IPTIPODOC = 6
                THEN 'Adulto Sin Identificación'
                WHEN p.IPTIPODOC = 7
                THEN 'Menor Sin Identificación'
                WHEN p.IPTIPODOC = 8
                THEN 'Número único de identificación personal'
                WHEN p.IPTIPODOC = 9
                THEN 'Certificado Nacido Vivo'
                WHEN p.IPTIPODOC = 10
                THEN 'Carnet Diplomático (Aplica para extranjeros)'
                WHEN p.IPTIPODOC = 11
                THEN 'Salvoconducto (Aplica para extranjeros)'
                ELSE 'Permiso especial de Permanencia (Aplica para extranjeros)'
            END AS 'TipoDocumento', 
            p.IPCODPACI AS 'DocumentoPaciente', 
            p.IPNOMCOMP AS 'NombreCompletoPaciente', 
            ipd.DocumentDate AS 'FechaDispensacion',
            CASE
                WHEN ipd.STATUS = 1
                THEN 'registrado'
                WHEN ipd.STATUS = 2
                THEN 'confirmado'
                ELSE 'anulado'
            END AS 'Estado', 
            ipd.CreationUser AS 'UsuarioCreado', 
            ipd.CreationDate AS 'FechaCreacion', 
            iip.Code AS 'IdProducto', 
            atc.Code AS 'CódigoATC', 
            iip.Name AS 'Producto',
            CASE
                WHEN iip.POSProduct = 1
                THEN 'Pos'
                ELSE 'NoPos'
            END AS 'Pos_NoPos', 
            ipdd.Quantity AS 'Cantidad', 
            ipddv.Quantity AS 'CantidadDevuelta', 
            e.Name AS 'Entidad',
            CASE
                WHEN e.EntityType = 1
                THEN 'EPS Contributivo'
                WHEN e.EntityType = 2
                THEN 'EPS Subsidiado'
                WHEN e.EntityType = 3
                THEN 'ET Vinculados Municipios'
                WHEN e.EntityType = 4
                THEN 'ET Vinculados Departamentos'
                WHEN e.EntityType = 5
                THEN 'ARL Riesgos Laborales'
                WHEN e.EntityType = 6
                THEN 'MP Medicina Prepagada'
                WHEN e.EntityType = 7
                THEN 'IPS Privada'
                WHEN e.EntityType = 8
                THEN 'IPS Publica'
                WHEN e.EntityType = 9
                THEN 'Regimen Especial'
                WHEN e.EntityType = 10
                THEN 'Accidentes de transito'
                WHEN e.EntityType = 11
                THEN 'Fosyga'
                ELSE 'Otros'
            END AS 'TipoEntidad'
     FROM Inventory.PharmaceuticalDispensing AS ipd WITH(NOLOCK)
          LEFT JOIN Inventory.PharmaceuticalDispensingDetail AS ipdd WITH(NOLOCK) ON ipdd.PharmaceuticalDispensingId = ipd.Id
          INNER JOIN Inventory.PharmaceuticalDispensingDetailBatchSerial AS ipddbs WITH(NOLOCK) ON ipddbs.PharmaceuticalDispensingDetailId = ipdd.Id
          LEFT JOIN Inventory.PharmaceuticalDispensingDevolutionDetail AS ipddv WITH(NOLOCK) ON ipddv.PharmaceuticalDispensingDetailBatchSerialId = ipddbs.Id
          INNER JOIN Inventory.InventoryProduct AS iip WITH(NOLOCK) ON ipdd.ProductId = iip.Id
          LEFT JOIN Inventory.ATC AS atc WITH(NOLOCK) ON atc.Id = iip.ATCId
          INNER JOIN Payroll.FunctionalUnit AS pfu WITH(NOLOCK) ON ipdd.FunctionalUnitId = pfu.Id
          LEFT JOIN dbo.ADINGRESO ad WITH(NOLOCK) ON ipd.AdmissionNumber = ad.NUMINGRES
          INNER JOIN dbo.INPACIENT p WITH(NOLOCK) ON p.IPCODPACI = ad.IPCODPACI
          INNER JOIN Contract.HealthAdministrator e WITH(NOLOCK) ON e.Code = ad.CODENTIDA;